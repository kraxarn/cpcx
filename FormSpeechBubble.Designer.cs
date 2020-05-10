namespace CPC
{
	partial class FormSpeechBubble
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.picture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
			this.SuspendLayout();
			// 
			// picture
			// 
			this.picture.Location = new System.Drawing.Point(0, 0);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(256, 128);
			this.picture.TabIndex = 0;
			this.picture.TabStop = false;
			// 
			// FormSpeechBubble
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(256, 128);
			this.Controls.Add(this.picture);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormSpeechBubble";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picture;
	}
}