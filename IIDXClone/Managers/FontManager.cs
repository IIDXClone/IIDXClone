using System.Collections.Generic;
using Love;

namespace IIDXClone.Managers {

	internal class FontManager {
		internal static Dictionary<string, Font> Fonts = new Dictionary<string, Font>() {
			{"normal", Graphics.NewFont("Resource/Font/Reglo-Bold.otf", 30)},
			{"big", Graphics.NewFont("Resource/Font/Reglo-Bold.otf", 60)}
		};
	}

}