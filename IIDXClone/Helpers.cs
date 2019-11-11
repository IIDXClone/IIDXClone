using Love;

namespace IIDXClone {

	public static class Helpers {
		public static int CenterX(this Text drawable) {
			return Graphics.GetWidth() / 2 - drawable.GetWidth() / 2;
		}

		public static int CenterY(this Text drawable) {
			return Graphics.GetHeight() / 2 - drawable.GetHeight() / 2;
		}
	}

}