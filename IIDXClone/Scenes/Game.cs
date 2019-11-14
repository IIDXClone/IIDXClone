using System.Numerics;
using IIDXClone.Managers;
using Love;

namespace IIDXClone.Scenes {

	internal class Game : Base {
		private readonly SongData _songData;

		private readonly Image[] _noteGraphics = {
			Resource.NewImage("Resource/Image/Note/White.png"),
			Resource.NewImage("Resource/Image/Note/Black.png"),
			Resource.NewImage("Resource/Image/Note/Scratch.png")
		};

		internal Game(string songPath) {
			_songData = SongManager.GetBMEData(songPath);
		}

		public override void Update(float dt) { }

		public override void Draw() {
			foreach (var n in _songData.TimeSections[17].Notes) {
				Image image;
				int lane;

				if (n.Lane != 7) {
					if (n.Lane % 2 == 0) {
						image = _noteGraphics[0];
						lane =  (image.GetWidth() + _noteGraphics[1].GetWidth()) * (n.Lane / 2);
					} else {
						image = _noteGraphics[1];
						lane = _noteGraphics[0].GetWidth() + (image.GetWidth() + _noteGraphics[0].GetWidth()) * ((n.Lane) / 2);
					}
				} else {
					image = _noteGraphics[2];
					lane = _noteGraphics[0].GetWidth() * 4 + _noteGraphics[1].GetWidth() * 3;
				}

				Graphics.Draw(
					image,
					lane,
					n.Time * 25
				);
			}
		}
	}

}