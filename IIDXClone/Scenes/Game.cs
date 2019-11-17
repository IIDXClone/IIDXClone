using System.Numerics;
using IIDXClone.Managers;
using Love;

namespace IIDXClone.Scenes {

	internal class Game : Base {
		private readonly SongData _songData;
		private readonly float _barTime;

		private readonly Image[] _noteGraphics = {
			Resource.NewImage("Resource/Image/Note/White.png"),
			Resource.NewImage("Resource/Image/Note/Black.png"),
			Resource.NewImage("Resource/Image/Note/Scratch.png")
		};

		internal Game(SongInfo info) {
			_songData = SongManager.GetBMEData(info);
			_barTime = 1f.BarToSeconds(info.BPM);
			TimeSinceStart = -1f;
		}

		public override void Draw() {
			var currentBar = (int) (TimeSinceStart / _barTime);
			for (var i = currentBar - 1; i < currentBar + 4; i++) {
				if(currentBar > _songData.TimeSections.Count) SwitchScenes(new SongSelect());
				if (i < 0 || i > _songData.TimeSections.Count - 1) continue;
				

				foreach (var a in _songData.TimeSections[i].BGM) {
					var y = (a.Time - TimeSinceStart);
					if (y <= 0) {
						a.Audio.Play();
					}
				}
				
				foreach (var n in _songData.TimeSections[i].Notes) {
					Image image;
					int lane;

					if (n.Lane != 7) {
						if (n.Lane % 2 == 0) {
							image = _noteGraphics[0];
							lane = (image.GetWidth() + _noteGraphics[1].GetWidth()) * (n.Lane / 2) + _noteGraphics[2].GetWidth();
						} else {
							image = _noteGraphics[1];
							lane = _noteGraphics[0].GetWidth() +
							       (image.GetWidth() + _noteGraphics[0].GetWidth()) * ((n.Lane) / 2) + _noteGraphics[2].GetWidth();
						}
					} else {
						image = _noteGraphics[2];
						lane = 0;
					}

					var y = (n.Time - TimeSinceStart);

					Graphics.Draw(
						image,
						lane,
						Graphics.GetHeight() - (y * 500)
					);

					if (y <= 0) {
						n.Audio.Play();
					}
				}
			}
		}
	}

}