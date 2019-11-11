using System;
using IIDXClone.Scenes;
using Love;
using static IIDXClone.Input;

namespace IIDXClone {

	public class Program {
		private static readonly BootConfig Config = new BootConfig {
			WindowResizable = false,
			WindowWidth = 1280,
			WindowHeight = 720,
			WindowVsync = false,
			WindowTitle = "IIDX",
			WindowDisplay = 0
		};

		public static void Main(string[] args) {
			Boot.Init(Config);
			Boot.Run(new SceneHolder(new Splash()));
		}
	}

	internal class SceneHolder : Scene {
		internal static SceneHolder Instance { get; private set; }

		internal Base ActiveScene;

		internal SceneHolder(Base startScene) {
			if (Instance != null)
				return;
			Instance = this;

			Graphics.SetFont(FontManager.Fonts["normal"]);
			
			ActiveScene = startScene;
		}

		internal void Log(string source, object message) {
			Console.WriteLine($"[{source}] {message}");
		}

		public override void Draw() => ActiveScene?.Draw();
		public override void Update(float dt) => ActiveScene?.Update(dt);
		public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat) => ActiveScene?.ActionStarted(new InputEventArgs(key.FromKeyConstant(), 0f));
	}

}