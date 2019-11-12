using System.Collections.Generic;
using Love;

namespace IIDXClone.Managers {

	public class FontManager {
		public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>() {
			{"normal", Graphics.NewFont("Resource/Font/Reglo-Bold.otf", 30)},
			{"big", Graphics.NewFont("Resource/Font/Reglo-Bold.otf", 60)}
		};
	}

}