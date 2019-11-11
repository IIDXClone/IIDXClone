using System;
using System.Collections.Generic;
using Love;

namespace IIDXClone {

	internal class Input {
		public static Dictionary<KeyConstant, IIDXAction> Actions = new Dictionary<KeyConstant, IIDXAction>(){
			{KeyConstant.Return, IIDXAction.P1Start},
			{KeyConstant.LShift, IIDXAction.P1TTUp},
			{KeyConstant.LCtrl, IIDXAction.P1TTDown}
		};

		internal class InputEventArgs : EventArgs {
			internal IIDXAction Action;
			internal float? Value;

			internal InputEventArgs(IIDXAction action, float? value) {
				Action = action;
				Value = value;
			}
		}
	}

	internal enum IIDXAction {
		Blank,
		P1Start,
		P2Start,
		P1TTUp,
		P1TTDown
	}

}