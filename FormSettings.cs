using System;
using System.Windows.Forms;
using NAudio.Wave;

namespace CPC
{
	public partial class FormSettings : Form
	{
		private readonly Settings cfg;

		public FormSettings(Settings cfg)
		{
			InitializeComponent();

			this.cfg = cfg;

			barPersonality.Scroll += BarPersonalityOnScroll;

			// Default values
			boxVoice.SelectedIndex = (int) cfg.Voice;
			boxNotifications.SelectedIndex = 0;
			boxComplain.SelectedIndex = 0;

			buttonSave.Click += (sender, args) => Close();
		}

		private void BarPersonalityOnScroll(object sender, EventArgs e) 
			=> labelPersonality.Text = GetPersonalityName();

		private string GetPersonalityName()
		{
			switch (barPersonality.Value)
			{
				case 0:  return "Friendly";
				case 1:  return "Snarky";
				case 2:  return "Homicidal";
				case 3:  return "Overkill";
				default: return "???";
			}
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			// General
			cfg.LowerOtherSounds = boxLowerSounds.Checked;
			cfg.IgnoreWhenFullscreen = boxIgnoreFullscreen.Checked;

			// Personality
			var p = Settings.Personalities.Homicidal;

			switch (barPersonality.Value)
			{
				case 0: p = Settings.Personalities.Friendly;  break;
				case 1: p = Settings.Personalities.Snarky;    break;
				case 2: p = Settings.Personalities.Homicidal; break;
				case 3: p = Settings.Personalities.Overkill;  break;
			}

			cfg.Personality = p;

			// Voice
			cfg.Voice = boxVoice.SelectedIndex == 0 
				? Settings.Voices.Default 
				: Settings.Voices.Computer;

			// Notifications
			var n = Settings.Notifications.Both;

			switch (boxNotifications.SelectedIndex)
			{
				case 0: n = Settings.Notifications.Both;         break;
				case 1: n = Settings.Notifications.Sound;        break;
				case 2: n = Settings.Notifications.SpeechBubble; break;
			}

			cfg.Notification = n;

			// Complain about
			var c = Settings.ComplainAbouts.Both;

			switch (boxComplain.SelectedIndex)
			{
				case 0: c = Settings.ComplainAbouts.Both;      break;
				case 1: c = Settings.ComplainAbouts.Apps;      break;
				case 2: c = Settings.ComplainAbouts.Resources; break;
			}

			cfg.ComplainAbout = c;

			// Save it
			cfg.Save();
		}
	}
}
