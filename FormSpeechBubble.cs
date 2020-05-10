using System;
using System.Drawing;
using System.Windows.Forms;

namespace CPC
{
	public partial class FormSpeechBubble : Form
	{
		private readonly Label label;

		public string DisplayMessage
		{
			set => label.Text = value;
		}

		public bool Saying => Visible;

		public FormSpeechBubble()
		{
			InitializeComponent();

			label = new Label
			{
				Location  = new Point(8, 8),
				Name      = "label",
				Size      = new Size(240, 98),
				BackColor = Color.White,
				ForeColor = Color.Black,
				Font      = new Font("Consolas", 9)
			};
			Controls.Add(label);
			label.BringToFront();

			picture.Image = Properties.Resources.Speech;
		}

		protected override void OnShown(EventArgs e)
		{
			var w = Screen.PrimaryScreen.Bounds.Width;
			var h = Screen.PrimaryScreen.Bounds.Height;

			SetBounds(w - 370, (int)(h * 0.75) - 140, Width, Height);

			// Hide background
			BackColor       = Color.LimeGreen;
			TransparencyKey = Color.LimeGreen;

			base.OnShown(e);
		}
	}
}
