using System;
using System.Collections.Generic;
using Love;
using static Love.Graphics;

namespace IIDXClone.Scenes {

	internal class Splash : Base {
		private const float FadeTime = 0.5f;
		private const float SplashTime = 4f;

		private readonly Image[] _splashImages = {  
			Resource.NewImage("Resource/Image/Splash/Main.png")
		};

		public override void Draw() {
			if (Mathf.FloorToInt(TimeSinceStart / SplashTime) < _splashImages.Length) {
				var splashIndex = Mathf.Floor(TimeSinceStart / SplashTime);
				var currentSplash = _splashImages[(int) splashIndex];

				SetColor(1, 1, 1, Mathf.Lerp(0, 1, (TimeSinceStart - splashIndex * SplashTime) / FadeTime));
				Graphics.Draw(
					currentSplash,
					sx: (float) GetWidth() / currentSplash.GetWidth(),
					sy: (float) GetHeight() / currentSplash.GetHeight());
			}
			else {
				SwitchScenes(new Menu());
			}
		}

		public override void ActionStarted(Input.InputEventArgs action) {
			if (action.Action == IIDXAction.P1Start) {
				Log("Skipping splash screens...");
				SwitchScenes(new Menu());
			}
		}
	}

}