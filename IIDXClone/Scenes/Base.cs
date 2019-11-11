using Love;
using static IIDXClone.Input;

namespace IIDXClone.Scenes {

	internal class Base : Scene {
		protected float TimeSinceStart = 0f;

		protected void Log(string source, object message) => SceneHolder.Instance.Log(source, message);
		
		public override void Update(float dt) {
			TimeSinceStart += dt;
		}

		public virtual void ActionStarted(InputEventArgs action) { }
		public virtual void ActionEnded(InputEventArgs action) { }
	}

}