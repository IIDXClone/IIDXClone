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

		internal static IIDXAction FromKeyConstant(this KeyConstant key) {
			try {
				return Input.Actions[key];
			} catch (Exception e) {
				return IIDXAction.Blank;
			}
		}
	}

}