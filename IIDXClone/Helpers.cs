using System;
using System.Collections.Generic;
using IIDXClone.Managers;
using Love;

namespace IIDXClone {

	public static class Helpers {
		public static int CenterX(this Text drawable) {
			return Graphics.GetWidth() / 2 - drawable.GetWidth() / 2;
		}
		public static int CenterY(this Text drawable) {
			return Graphics.GetHeight() / 2 - drawable.GetHeight() / 2;
		}

		public static int CenterX(this string drawable, Font font) {
			return Graphics.GetWidth() / 2 - font.GetWidth(drawable) / 2;
		}

		public static int FromBase36(this string input) {
			if (input.Length > 2) return -1;
			var splitInput = input.ToCharArray();
			var output = 0;
			
			if (char.IsNumber(splitInput[0])) { 
				output = splitInput[0] - 0x30;
			} else if (char.IsLetter(splitInput[0])) {
				output = splitInput[0] - 0x37;
			} else {
				return -1;
			}

			output *= 36;
			
			if (char.IsNumber(splitInput[1])) {
				output += splitInput[1] - 0x30;
			} else if (char.IsLetter(splitInput[1])) {
				output += splitInput[1] - 0x37;
			} else {
				return -1;
			}

			return output;
		}
		public static int FromBase16(this string input) {
			if (input.Length > 2) return -1;
			var splitInput = input.ToCharArray();
			var output = 0;
			
			if (char.IsNumber(splitInput[0])) { 
				output = splitInput[0] - 0x30;
			} else if (char.IsLetter(splitInput[0])) {
				output = splitInput[0] - 0x37;
			} else {
				return -1;
			}

			output *= 16;
			
			if (char.IsNumber(splitInput[1])) {
				output += splitInput[1] - 0x30;
			} else if (char.IsLetter(splitInput[1])) {
				output += splitInput[1] - 0x37;
			} else {
				return -1;
			}

			return output;
		}
		
		public static string[] Split(this string input, int length) {
			var strings = new List<string>();

			var tempString = "";
			foreach (var v in input.ToCharArray()) {
				if (tempString.Length >= length) {
					strings.Add(tempString);
					tempString = "";
				}

				tempString += v;
			}
			
			return strings.ToArray();
		}

		internal static byte ToLane(this Channel input) {
			if (input >= Channel.P1Scratch) {
				if (input == Channel.P1Scratch) {
					return 8;
				} else {
					return (byte) (input - 0x12);
				}
			} else {
				return (byte) (input - 0x11);
			}
		}

		internal static IIDXAction FromKeyConstant(this KeyConstant key) {
			try {
				return Input.Actions[key];
			} catch (Exception e) {
				return IIDXAction.Blank;
			}
		}
	}

}