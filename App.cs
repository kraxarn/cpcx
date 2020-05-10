namespace CPC
{
	public class App
	{
		public enum AppType
		{
			None,
			Music,
			Productivity,
			TextEditor,
			OtherEditor,
			Code,
			WebBrowser,
			Viewer,
			Communication,
			Utility,
			Game,
			Other
		}

		public string  Name { get; }
		public AppType Type { get; }

		private uint secondsRunning;

		/// <summary> Time app has been running in HH:MM </summary>
		public string TimeRunning
		{
			get
			{
				var s = secondsRunning;
				var m = 0;

				while (s >= 60)
				{
					m++;
					s -= 60;
				}

				var mstr = s < 10 ? $"0{s}" : $"{s}";
				var hstr = m < 10 ? $"0{m}" : $"{m}";

				return $"{hstr}:{mstr}";
			}
		}

		public uint MinutesRunning => secondsRunning / 60;

		public App(string name, AppType type)
		{
			Name = name;
			Type = type;
			secondsRunning = 0;
		}

		public App(string name) : this(name, GetType(name)) { }

		public void AddRuntime(uint seconds)
			=> secondsRunning += seconds;

		public static AppType GetType(string appName)
		{
			switch (appName)
			{
				#region Music
				case "iTunes":		// iTunes
				case "Spotify":		// Spotify
				case "wmplayer":	// Windows Media Player
					return AppType.Music;
				#endregion
				
				#region Productivity
				case "audacity":			// Audacity
				case "AfterFX":				// After Effects
				case "Illustrator":			// Illustrator
				case "Photoshop":			// Photoshop
				case "Adobe Premiere Pro":	// Premiere
				case "gimp-2.10":			// Gimp
				case "vegas140":			// Vegas
					return AppType.Productivity;
				#endregion

				#region Text editor
				case "WINWORD":		// Word
				case "libreoffice":	// TODO: LibreOffice
					return AppType.TextEditor;
				#endregion

				#region Other editor
				// TODO: Add this or move to productivity?
				case "POWERPNT":	// PowerPoint
				case "EXCEL":		// Excel
					return AppType.OtherEditor;
				#endregion

				#region Code
				case "codeblocks":		// CodeBlocks
				case "devenv":			// Visual Studio
				case "processing":		// Processing
				case "studio64":		// Android Studio
				case "idea64":			// IntelliJ
					return AppType.Code;
				#endregion

				#region Web browsers
				case "iexplore":	// Internet Explorer
				case "firefox":     // Firefox
				case "chrome":		// Google Chrome
					return AppType.WebBrowser;
				#endregion

				#region Viewer
				//case "Microsoft.Photos":	// Photos
				case "vlc":					// VLC
					return AppType.Viewer;
				#endregion

				#region Communication
				case "skype":			// TODO: Skype
				case "Discord":			// Discord
				case "HxOutlook":		// Mail
				case "ts3client_win64":	// TeamSpeak 3
					return AppType.Communication;
				#endregion

				#region Utility
				case "CCleaner64":	// CCleaner
					return AppType.Utility;
				#endregion

				#region Games
				case "Steam":		// Steam
				case "minecraft":	// TODO: Minecraft
					return AppType.Game;
				#endregion

				#region Other
				//case "SystemSettings":	// Settings
				case "VirtualBox":		// VirtualBox
				case "vmplayer":		// VMWare Player
					return AppType.Other;
				#endregion
				
				default:
					return AppType.None;
			}
		}

		public override string ToString() 
			=> $"{Name} ({TimeRunning})";
	}
}