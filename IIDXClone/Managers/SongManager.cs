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

		internal static SongData GetBMEData(string file) {
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

				var timeSection = new TimeSection {
					Notes = new List<Note>()
				};

				foreach (var line in lineGroup) {
					var channel = (Channel) line.Substring(4, 2).FromBase36();
					switch (channel) {
						case Channel.P1Key1:
							
							break;
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
	}
	
	internal struct Note {
		internal float Time;
		internal byte Lane;
		internal int Audio;
		internal bool Visible;

		internal Note(float time, byte lane, int audio, bool visible) {
			Time = time;
			Lane = lane;
			Audio = audio;
			Visible = visible;
		}
	}

	internal enum Channel {
		BGM = 1,
		MeasureLength = 2,
		BPM = 3,
		BGABase = 4,
		BGAPoor = 6,
		P1Key1 = 11,
		P1Key2 = 12,
		P1Key3 = 13,
		P1Key4 = 14,
		P1Key5 = 15,
		P1Scratch = 16,
		P1Key6 = 18,
		P1Key7 = 19,
		P1Invis1 = 31,
		P1Invis2 = 32,
		P1Invis3 = 33,
		P1Invis4 = 34,
		P1Invis5 = 35,
		P1InvisScratch = 36,
		P1Invis6 = 38,
		P1Invis7 = 39,
	}
	
	internal enum Difficulty {
		Beginner = 1,
		Normal = 2,
		Hyper = 3,
		Another = 4,
		Leggendaria = 5
	}
}