using Love;
using static IIDXClone.Input;

namespace IIDXClone.Scenes {

	/// <summary>
	/// Class representing any screens/states.
	/// </summary>
	internal class Base : Scene {
		protected float TimeSinceStart = 0f;

		protected void Log(object message) => SceneHolder.Instance.Log(message);
		protected void SwitchScenes(Base scene) => SceneHolder.Instance.ActiveScene = scene;
		
		public override void Update(float dt) {
			TimeSinceStart += dt;
		}

		public virtual void ActionStarted(InputEventArgs action) { }
		public virtual void ActionEnded(InputEventArgs action) { }
	}

}