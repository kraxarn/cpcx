using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace CPC
{
	public partial class Form1 : Form, IUserInterface
	{
		private int screenWidth, screenHeight;

		public Settings Config;

		public Bitmap Background
		{
			get => new Bitmap(pictureBox1.Image);
			set => pictureBox1.Image = value;
		}

		public bool IsVisible
		{
			get => Visible;
			set => Visible = value;
		}

		public Form1()
		{
			InitializeComponent();

			TransparencyKey = Color.LimeGreen;

			Config = new Settings();
			Config.Load();

			var bot = new Bot(this, Config);

			// Notification icon
			var icon = new NotifyIcon
			{
				Icon = new Icon(Icon, 40, 40),
				Text = @"CPC: Beta",
				Visible = true
			};

			// Menu buttons
			var stats = new MenuItem("📊 Stats");
			stats.Click += (sender, args) =>
			{
				var form = new FormStats(bot.RunningApps, bot.CurrentMoodString, bot.CurrentEnergyString, bot);
				form.Show();
			};

			var settings = new MenuItem("⚙️ Settings");
			settings.Click += (sender, args) =>
			{
				var form = new FormSettings(Config);
				form.Show();
			};

			var close = new MenuItem("❌ Close");
			close.Click += (sender, args) => Close();

			var dbgTest = new MenuItem("📢 Test voice lines");
			dbgTest.Click += (sender, args) =>
			{
				var form = new FormDebug(bot);
				form.Show();
			};

			var build = new FileInfo(Application.ExecutablePath).LastWriteTime;
			var version = new MenuItem
			{
				Text = $@"Version {build.Year}.{build.Month}.{build.Day} ({build.Hour})",
				Enabled = false
			};

			var menu = new ContextMenu(new []
			{
				stats,
				settings,
				dbgTest,
				new MenuItem("-"), 
				version,
				close
			});

			icon.ContextMenu = menu;

			screenWidth  = Screen.PrimaryScreen.Bounds.Width;
			screenHeight = Screen.PrimaryScreen.Bounds.Height;
		}

		protected override void OnShown(EventArgs e)
		{
			SetBounds(screenWidth, (int)(screenHeight * 0.75), 192, 192);
			Hide();
			base.OnShown(e);
		}

		public void MoveWindow(int x, int y)
		{
			//Console.WriteLine($"Moving window, invoke required: {InvokeRequired}");

			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(delegate
				{
					Left += x;
					Top += y;
				}));
			}
			else
			{
				Left += x;
				Top += y;
			}
		}
	}
}
