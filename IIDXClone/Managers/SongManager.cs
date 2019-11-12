using System.Collections.Generic;
using System.IO;
using System.Linq;

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
						ReadBME(file);
					}
				}
			}
		}
		
		internal static void ReadBME(string file){
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

	internal enum Difficulty {
		Beginner = 1,
		Normal = 2,
		Hyper = 3,
		Another = 4,
		Leggendaria = 5
	}
}