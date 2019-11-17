using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Love;
using File = System.IO.File;

namespace IIDXClone.Managers {

	internal class SongManager {
		internal static List<SongInfo> Songs = new List<SongInfo>();
		
		internal static void InitializeSongDirectory() {
			Program.Log($"Scanning for songs...");
			
			foreach (var sub in Directory.EnumerateDirectories("Songs")) {
				Program.Log($"Found song directory : {sub}");
				
				foreach (var file in Directory.EnumerateFiles(sub)) {
					if (Path.GetExtension(file) == ".bme") { //TODO: This needs to get replaced with a system that allows for reader methods/classes to be assigned to filetypes.
						Program.Log($"Found file : {file}");
						GetBMEInfo(file);
					}
				}
			}
		}
		
		internal static void GetBMEInfo(string file){
			var commands = new Dictionary<string, string>();
			foreach (var line in File.ReadLines(file)) {
				if (!line.StartsWith("#")) continue;

				var command = line.ToUpper().Substring(1).Split(" ").ToList();
				if(command.Count > 1)
					commands.Add(command[0], string.Join(" ", command.GetRange(1, command.Count - 1)));
			}

			if (commands["PLAYER"] == "1") {
				SongInfo songInfo = new SongInfo();
				songInfo.Artist = commands["ARTIST"];
				songInfo.BPM = float.Parse(commands["BPM"]);
				songInfo.Difficulty = (Difficulty) int.Parse(commands["DIFFICULTY"]);
				songInfo.Genre = commands["GENRE"];
				songInfo.Path = file;
				songInfo.PlayLevel = byte.Parse(commands["PLAYLEVEL"]);
				songInfo.Title = commands["TITLE"];
				songInfo.Total = float.Parse(commands["TOTAL"]);
				
				Songs.Add(songInfo);
			}
		}

		internal static SongData GetBMEData(SongInfo info) {
			var file = info.Path;
			var data = new SongData();
			data.TimeSections = new List<TimeSection>();
			data.Audio = new Dictionary<int, Source>();
			
			var lines = new List<string>[1000];
			var path = Directory.GetParent(file);

			foreach (var line in File.ReadLines(file)) {
				if (!line.StartsWith("#")) continue;
				if (int.TryParse(line.Substring(1, 3), out var barIndex)) {
					if (lines[barIndex] == null)
						lines[barIndex] = new List<string>();

					lines[barIndex].Add(line);
				} else if (line.StartsWith("#WAV")) {
					var audio = $"{path}\\{line.Substring(7)}";
					if (!File.Exists(audio)) {
						if (!File.Exists($"{Directory.GetParent(audio)}\\{Path.GetFileNameWithoutExtension(audio)}.ogg")) {
							continue;
						}
						
						audio = $"{Directory.GetParent(audio)}\\{Path.GetFileNameWithoutExtension(audio)}.ogg";
					}
					
					Program.Log($"Loading audio file : {audio}");
					data.Audio.Add(line.Substring(4, 2).FromBase36(), Resource.NewSource(audio, SourceType.Static));
				}
			}

			foreach (var lineGroup in lines) {
				if (lineGroup == null) continue;

				var timeSection = new TimeSection(0);

				foreach (var line in lineGroup) {
					var measure = int.Parse(line.Substring(1, 3));
					var channel = (Channel) line.Substring(4, 2).FromBase16();

					if (channel == Channel.BGM) {
						var split = line.Substring(7).Split(2);
						for (var index = 0; index < split.Length; index++) {
							var audio = split[index];
							if (audio.FromBase36() > 0) {
								timeSection.BGM.Add(new TimedAudio((measure + (float)index / (split.Length)).BarToSeconds(info.BPM), data.Audio[audio.FromBase36()].Clone()));
							}
						}
					}
					
					if (channel >= Channel.P1Key1 && channel <= Channel.P1Key7) {
						var split = line.Substring(7).Split(2);
						for (var index = 0; index < split.Length; index++) {
							var audio = split[index];
							if (audio.FromBase36() > 0) {
								timeSection.Notes.Add(new Note((measure + (float)index / (split.Length)).BarToSeconds(info.BPM), channel.ToLane(), data.Audio[audio.FromBase36()].Clone(), true));
							}
						}
					}
				}

				data.TimeSections.Add(timeSection);
			}

			return data;
		}
	}

	internal struct SongInfo {
		internal string Path;
		internal string Title;
		internal string Artist;
		internal string Genre;
		internal byte PlayLevel;
		internal float Total;
		internal Difficulty Difficulty;
		internal float BPM;
	}

	internal struct SongData {
		internal List<TimeSection> TimeSections;
		internal Dictionary<int, Source> Audio;
	}

	internal struct TimeSection {
		internal float Time;
		internal List<Note> Notes;
		internal List<TimedAudio> BGM;

		internal TimeSection(float time = 0f) {
			Time = time;
			Notes = new List<Note>();
			BGM = new List<TimedAudio>();
		}
	}

	internal struct TimedAudio {
		internal float Time;
		internal OneshotAudio Audio;

		internal TimedAudio(float time, Source audio) {
			Time = time;
			Audio = new OneshotAudio(audio);
		}
	}
	
	internal struct Note {
		internal float Time;
		internal byte Lane;
		internal OneshotAudio Audio;
		internal bool Visible;

		internal Note(float time, byte lane, Source audio, bool visible) {
			Time = time;
			Lane = lane;
			Audio = new OneshotAudio(audio);
			Visible = visible;
		}
	}
	
	internal class OneshotAudio {
	    internal Source Audio;
	    private bool _played;

	    internal OneshotAudio(Source audio) {
		    Audio = audio;
		    _played = false;
	    }

	    internal void Play() {
		    if (_played) return;
		    
		    Audio.Play();
		    _played = true;
	    }
	}

	internal enum Channel {
		BGM = 0x1,
		MeasureLength = 0x2,
		BPM = 0x3,
		BGABase = 0x4,
		BGAPoor = 0x6,
		P1Key1 = 0x11,
		P1Key2 = 0x12,
		P1Key3 = 0x13,
		P1Key4 = 0x14,
		P1Key5 = 0x15,
		P1Scratch = 0x16,
		P1Key6 = 0x18,
		P1Key7 = 0x19,
		P1Invis1 = 0x31,
		P1Invis2 = 0x32,
		P1Invis3 = 0x33,
		P1Invis4 = 0x34,
		P1Invis5 = 0x35,
		P1InvisScratch = 0x36,
		P1Invis6 = 0x38,
		P1Invis7 = 0x39,
	}
	
	internal enum Difficulty {
		Beginner = 1,
		Normal = 2,
		Hyper = 3,
		Another = 4,
		Leggendaria = 5
	}
}