using System;
using System.Globalization;
using System.IO;
using System.Linq;
using IIDXClone.Managers;
using IIDXClone.Scenes;
using Love;
using static IIDXClone.Input;
using static Love.Graphics;
using File = System.IO.File;

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

		public static void Log(string message, LogLevel level = LogLevel.Info) {
			var prefix = "";
			switch (level) {
				case LogLevel.Info:
					Console.ForegroundColor = ConsoleColor.White;
					prefix = "[INFO]";
					break;
				case LogLevel.Warn:
					Console.ForegroundColor = ConsoleColor.Yellow;
					prefix = "[INFO]";
					break;
				case LogLevel.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					prefix = "[INFO]";
					break;
			}

			Console.WriteLine($"{prefix} {message}");
			File.AppendAllText("log.txt", $"{prefix} {message} \n");
		}

		public static void Main(string[] args) {
			File.WriteAllText("log.txt", $"IIDXClone Log for {DateTime.Now.ToString(CultureInfo.CurrentCulture)} \n ---------------------------------- \n");
			
			if (!Directory.Exists("Songs")) {
				Log("Songs directory does not exist, creating...");
				Log($"Created song directory : {Directory.CreateDirectory("Songs").FullName}");
			} else {
				Log($"Songs directory found : {Path.GetFullPath("Songs")}");
			}
			
			SongManager.InitializeSongDirectory("Songs");
			
			Boot.Init(Config);
			Boot.Run(new SceneHolder(args.Contains("--skipSplash") ? (Base) new Menu() : new Splash()));
		}

		public enum LogLevel {
			Info,
			Warn,
			Error
		}
	}

	internal class SceneHolder : Scene {
		internal static SceneHolder Instance { get; private set; }

		internal Base ActiveScene;

		internal SceneHolder(Base startScene) {
			if (Instance == null)
				Instance = this;
			
			ActiveScene = startScene;
		}

		internal void Log(object message, Program.LogLevel level = Program.LogLevel.Info) {
			Program.Log(message.ToString(), level);
		}

		public override void Draw() {
			ActiveScene?.Draw();

			var fps = $"FPS {Timer.GetFPS()}";
			SetColor(0f, 0f, 0f);
			Rectangle(DrawMode.Fill, 0, 0, GetFont().GetWidth(fps), GetFont().GetHeight());
			SetColor(1f, 1f, 1f);
			Print(fps);
		}
		public override void Update(float dt) => ActiveScene?.Update(dt);
		public override void KeyPressed(KeyConstant key, Scancode scancode, bool isRepeat) => ActiveScene?.ActionStarted(new InputEventArgs(key.FromKeyConstant(), 0f));
	}

}