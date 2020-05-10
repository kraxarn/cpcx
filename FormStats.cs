using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CPC
{
	public class FormStats : Form
	{
		private DataGridView viewApps;
		private Label labelMood, labelEnergy;
		private readonly Bot bot;

		public FormStats(IEnumerable<App> apps, string mood, string energy, Bot bot)
		{
			this.bot = bot;

			AddElements();

			foreach (var app in apps)
				viewApps.Rows.Add(app.Name, app.TimeRunning);

			labelMood.Text   = $@"Mood: {mood}";
			labelEnergy.Text = $@"Energy: {energy}%";

			bot.EnergyChanged += UpdateEnergy;
			bot.MoodChanged   += UpdateMood;
			bot.AppsChanged   += UpdateAppList;
		}

		private void UpdateEnergy(string s)
			=> labelEnergy.Text = $@"Energy: {s}%";

		private void UpdateMood(string s)
			=> labelMood.Text = $@"Mood: {s}";

		private void UpdateAppList(IEnumerable<App> a)
		{
			viewApps.Rows.Clear();
				foreach (var app in a)
					viewApps.Rows.Add(app.Name, app.TimeRunning);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			bot.EnergyChanged -= UpdateEnergy;
			bot.MoodChanged   -= UpdateMood;
			bot.AppsChanged   -= UpdateAppList;

			base.OnClosing(e);
		}

		private void AddElements()
		{
			SuspendLayout();
			
			// ViewApps
			viewApps = new DataGridView
			{
				Location = new Point(8, 40),
				Name = "viewApps",
				Size = new Size(164, 192),
				TabIndex = 2,
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
				ReadOnly = true,
				Enabled = false,
				RowHeadersVisible = false
			};
			viewApps.Columns.Add("AppName",     "Name");
			viewApps.Columns.Add("TimeRunning", "Time");
			viewApps.Columns[0].Width = 81;
			viewApps.Columns[1].Width = 80;
			viewApps.DefaultCellStyle.SelectionBackColor = viewApps.DefaultCellStyle.BackColor;
			viewApps.DefaultCellStyle.SelectionForeColor = viewApps.DefaultCellStyle.ForeColor;

			// LabelMood
			labelMood = new Label
			{
				Size = new Size(128, 16),
				Location = new Point(8, 8),
				Name = "labelMood",
				TabIndex = 0
			};

			// LabelEnergy
			labelEnergy = new Label
			{
				Size = new Size(128, 16),
				Location = new Point(8, 22),
				Name = "labelEnergy",
				TabIndex = 1
			};

			// FormStats
			ClientSize = new Size(180, 240);
			Controls.Add(viewApps);
			Controls.Add(labelMood);
			Controls.Add(labelEnergy);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormStats";
			StartPosition = FormStartPosition.CenterScreen;
			ShowIcon = false;
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Text = @"Stats";
			ResumeLayout(false);
		}
	}
}
