using System;
using System.Collections.Generic;
using Love;

namespace IIDXClone.Scenes {

	internal class Menu : Base {

		private int _selectedIndex = 0;
		private readonly List<MenuOption> _options = new List<MenuOption>();

		private void Play() {
			
		}

		private void Exit() {
			Environment.Exit(0);
		}

		internal Menu() {
			_options.Add(new MenuOption("Play", Play));
			_options.Add(new MenuOption("Exit", Exit));
		}

		public override void ActionStarted(Input.InputEventArgs action) {
			switch (action.Action) {
				case IIDXAction.P1Start:
					_options[_selectedIndex].Action();
					break;
				case IIDXAction.P1TTUp:
					if (--_selectedIndex < 0)
						_selectedIndex = _options.Count - 1;
					break;
				case IIDXAction.P1TTDown:
					if (++_selectedIndex > _options.Count - 1)
						_selectedIndex = 0;
					break;
			}
		}

		public override void Draw() {
			var offset = 0;
			foreach (var option in _options) {
				if (offset != _selectedIndex) {
					Graphics.SetColor(1f, 1f, 1f);
				} else {
					Graphics.SetColor(.75f, .75f, 1f);
				}

				Graphics.Draw(
					option.Text, 
					option.Text.CenterX(),
					option.Text.CenterY() + offset * option.Text.GetHeight());
				offset++;
			}
		}

		private struct MenuOption {
			private string _name;
			internal readonly Text Text;
			internal readonly Action Action;

			internal MenuOption(string name, Action action) {
				_name = name;
				Action = action;
				Text = Graphics.NewText(Graphics.GetFont(), name);
			}
		}
	}

}