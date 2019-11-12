namespace IIDXClone.Scenes {

	internal class SongSelect : Base {
		public override void ActionStarted(Input.InputEventArgs action) {
			switch (action.Action) {
				case IIDXAction.P12:
				case IIDXAction.P14:
				case IIDXAction.P16:
					SwitchScenes(new Menu());
					break;
			}
		}
	}

}