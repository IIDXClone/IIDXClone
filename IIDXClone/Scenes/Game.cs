using IIDXClone.Managers;

namespace IIDXClone.Scenes {

	internal class Game : Base {
		internal Game(string songPath) {
			SongManager.GetBMEData(songPath);
		}
	}

}