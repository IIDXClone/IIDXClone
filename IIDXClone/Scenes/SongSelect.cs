using System.Collections.Generic;
using IIDXClone.Managers;
using Love;
using static Love.Graphics;

namespace IIDXClone.Scenes {

	internal class SongSelect : Base {
		private int _selectedIndex = 0;

		public override void ActionStarted(Input.InputEventArgs action) {
			switch (action.Action) {
				case IIDXAction.P12:
				case IIDXAction.P14:
				case IIDXAction.P16:
					SwitchScenes(new Menu());
					break;
				case IIDXAction.P1Start:
					SceneHolder.Instance.ActiveScene = new Game(SongManager.Songs[_selectedIndex]);
					break;
				case IIDXAction.P1TTUp:
					if (--_selectedIndex < 0)
						_selectedIndex = SongManager.Songs.Count - 1;
					break;
				case IIDXAction.P1TTDown:
					if (++_selectedIndex > SongManager.Songs.Count - 1)
						_selectedIndex = 0;
					break;
			}
		}

		public override void Draw() {
			for (int i = 0; i < SongManager.Songs.Count; i++) {
				var song = SongManager.Songs[i];
				
				if (i != _selectedIndex) {
					SetColor(1f, 1f, 1f);
				} else {
					SetColor(.75f, .75f, 1f);
				}
				
				Print(song.Title,
					song.Title.CenterX(FontManager.Fonts["normal"]),
					FontManager.Fonts["normal"].GetHeight() * i
					
				);
			}
		}
	}

}