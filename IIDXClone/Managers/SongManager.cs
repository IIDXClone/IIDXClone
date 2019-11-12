using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IIDXClone.Managers {

	internal class SongManager {
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
			foreach (var line in File.ReadLines(file)) {
				if (!line.StartsWith("#")) continue;

				var command = line.ToUpper().Substring(1).Split(" ");
				if (command[0] == "PLAYER" && command[1] == "1") {
					Program.Log($"Found non-DP file : {file}");
					return;
				}
			}
		}
	}
}