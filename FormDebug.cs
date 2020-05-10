using System;
using System.IO;
using System.Windows.Forms;

namespace CPC
{
	public partial class FormDebug : Form
	{
		private readonly Bot bot;
		private string path;

		public FormDebug(Bot bot)
		{
			InitializeComponent();

			this.bot = bot;
			path = $"{bot.DataPath}/Voice/Default/{bot.MoodModifier}/";

			// This doesn't matter since both should have the same
			foreach (var dir in Directory.GetDirectories(path))
				comboBox1.Items.Add(dir.Substring(dir.LastIndexOf('/') + 1));

			comboBox1.Enabled = true;

			comboBox3.SelectedIndex = 0;
			comboBox3.Enabled = true;
		}

		private string GetMoodModifier()
		{
			switch (comboBox3.SelectedIndex)
			{
				case 1:  return "Bad";
				case 2:  return "Good";
				default: return null;
			}
		}

		private void BtnGo_Click(object sender, EventArgs e) 
			=> bot.Say($"{comboBox1.SelectedItem}/{comboBox2.SelectedItem}", GetMoodModifier());

		private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnGo.Enabled = false;
			comboBox2.Items.Clear();

			foreach (var dir in Directory.GetDirectories($"{path}{comboBox1.SelectedItem}/"))
				comboBox2.Items.Add(dir.Substring(dir.LastIndexOf('/') + 1));

			comboBox2.Enabled = true;
		}

		private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e) 
			=> btnGo.Enabled = comboBox2.SelectedIndex >= 0;

		private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			path = $"{bot.DataPath}/Voice/Default/{GetMoodModifier() ?? bot.MoodModifier}/";
			ComboBox1_SelectedIndexChanged(sender, e);
		}
	}
}
