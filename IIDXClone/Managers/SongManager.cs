using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IIDXClone.Managers {

	internal class SongManager {
		internal static void InitializeSongDirectory(string path) {
			Program.Log($"Scanning for songs...");
			
			foreach (var sub in Directory.EnumerateDirectories(path)) {
				foreach (var file in Directory.EnumerateFiles(sub)) {
					if (Path.GetExtension(file) == ".bme") { //TODO: This needs to get replaced with a system that allows for reader methods/classes to be assigned to filetypes.
						Program.Log($"Found file in song directory {sub} : {file}");		
					}
				}
				
				Program.Log($"Found song directory : {sub}");
			}
		}
	}

}