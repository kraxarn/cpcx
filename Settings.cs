using System;
using System.Collections.Generic;
using System.IO;

namespace CPC
{
	public class Settings
	{
		public enum Voices
		{
			Default,
			Computer
		}

		public enum Personalities
		{
			Friendly,
			Snarky,
			Homicidal,
			Overkill
		}

		public enum Notifications
		{
			Both,
			Sound,
			SpeechBubble
		}

		public enum ComplainAbouts
		{
			Both,
			Apps,
			Resources
		}

		public bool LowerOtherSounds, IgnoreWhenFullscreen;

		public Personalities Personality;

		public Voices Voice;

		public Notifications Notification;

		public ComplainAbouts ComplainAbout;

		public Settings()
		{
			LowerOtherSounds     = false;
			IgnoreWhenFullscreen = false;

			Personality   = Personalities.Homicidal;
			Voice         = Voices.Default;
			Notification  = Notifications.Both;
			ComplainAbout = ComplainAbouts.Both;
		}

		public void Load()
		{
			if (!File.Exists("./config.txt"))
				return;

			foreach (var line in File.ReadAllLines("./config.txt"))
			{
				var l = line.Split('=');

				switch (l[0])
				{
					case "LowerOtherSounds":     LowerOtherSounds     = bool.Parse(l[1]); break;
					case "IgnoreWhenFullscreen": IgnoreWhenFullscreen = bool.Parse(l[1]); break;

					case "Personality":   Personality   = GetPersonality(l[1]);   break;
					case "Voice":         Voice         = GetVoice(l[1]);         break;
					case "Notification":  Notification  = GetNotification(l[1]);  break;
					case "ComplainAbout": ComplainAbout = GetComplainAbout(l[1]); break;

					default:
						Console.WriteLine($@"Warning: Unknown setting: {l[0]}");
						break;
				}
			}
		}

		public void Save()
		{
			var lines = new List<string>
			{
				$"LowerOtherSounds={LowerOtherSounds}",
				$"IgnoreWhenFullscreen={IgnoreWhenFullscreen}",
				$"Personality={Personality}",
				$"Voice={Voice}",
				$"Notification={Notification}",
				$"ComplainAbout={ComplainAbout}"
			};

			File.WriteAllLines("./config.txt", lines);
		}

		private static Personalities GetPersonality(string value) 
			=> Enum.TryParse(value, out Personalities p) ? p : Personalities.Homicidal;

		private static Voices GetVoice(string value)
			=> Enum.TryParse(value, out Voices v) ? v : Voices.Default;

		private static Notifications GetNotification(string value)
			=> Enum.TryParse(value, out Notifications n) ? n : Notifications.Both;

		private static ComplainAbouts GetComplainAbout(string value)
			=> Enum.TryParse(value, out ComplainAbouts cb) ? cb : ComplainAbouts.Both;
	}
}