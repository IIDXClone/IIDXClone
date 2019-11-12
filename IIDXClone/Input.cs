using System;
using System.Collections.Generic;
using Love;

namespace IIDXClone {

	internal class Input {
		public static Dictionary<KeyConstant, IIDXAction> Actions = new Dictionary<KeyConstant, IIDXAction>(){
			{KeyConstant.Return, IIDXAction.P1Start},
			{KeyConstant.LShift, IIDXAction.P1TTUp},
			{KeyConstant.LCtrl, IIDXAction.P1TTDown},
			{KeyConstant.Z, IIDXAction.P11},
			{KeyConstant.S, IIDXAction.P12},
			{KeyConstant.X, IIDXAction.P13},
			{KeyConstant.D, IIDXAction.P14},
			{KeyConstant.C, IIDXAction.P15},
			{KeyConstant.F, IIDXAction.P16},
			{KeyConstant.V, IIDXAction.P17}
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
		P1TTDown,
		P11,
		P12,
		P13,
		P14,
		P15,
		P16,
		P17
	}

}