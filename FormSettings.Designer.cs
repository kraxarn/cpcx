namespace CPC
{
	partial class FormSettings
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.boxVoice = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.boxNotifications = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.boxIgnoreFullscreen = new System.Windows.Forms.CheckBox();
			this.boxLowerSounds = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.labelPersonality = new System.Windows.Forms.Label();
			this.barPersonality = new System.Windows.Forms.TrackBar();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.boxComplain = new System.Windows.Forms.ComboBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barPersonality)).BeginInit();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.boxVoice);
			this.groupBox1.Location = new System.Drawing.Point(154, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(136, 52);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "🎤 Voice";
			// 
			// boxVoice
			// 
			this.boxVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.boxVoice.FormattingEnabled = true;
			this.boxVoice.Items.AddRange(new object[] {
            "Default",
            "Computer"});
			this.boxVoice.Location = new System.Drawing.Point(6, 19);
			this.boxVoice.Name = "boxVoice";
			this.boxVoice.Size = new System.Drawing.Size(121, 21);
			this.boxVoice.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.boxNotifications);
			this.groupBox2.Location = new System.Drawing.Point(154, 67);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(136, 50);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "✉️ Notifications";
			// 
			// boxNotifications
			// 
			this.boxNotifications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.boxNotifications.FormattingEnabled = true;
			this.boxNotifications.Items.AddRange(new object[] {
            "All",
            "Sound",
            "Speech bubble"});
			this.boxNotifications.Location = new System.Drawing.Point(6, 19);
			this.boxNotifications.Name = "boxNotifications";
			this.boxNotifications.Size = new System.Drawing.Size(121, 21);
			this.boxNotifications.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.boxIgnoreFullscreen);
			this.groupBox3.Controls.Add(this.boxLowerSounds);
			this.groupBox3.Location = new System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(136, 118);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "🔧 General";
			// 
			// boxIgnoreFullscreen
			// 
			this.boxIgnoreFullscreen.AutoSize = true;
			this.boxIgnoreFullscreen.Location = new System.Drawing.Point(6, 42);
			this.boxIgnoreFullscreen.Name = "boxIgnoreFullscreen";
			this.boxIgnoreFullscreen.Size = new System.Drawing.Size(104, 17);
			this.boxIgnoreFullscreen.TabIndex = 3;
			this.boxIgnoreFullscreen.Text = "Ignore fullscreen";
			this.boxIgnoreFullscreen.UseVisualStyleBackColor = true;
			// 
			// boxLowerSounds
			// 
			this.boxLowerSounds.AutoSize = true;
			this.boxLowerSounds.Location = new System.Drawing.Point(6, 19);
			this.boxLowerSounds.Name = "boxLowerSounds";
			this.boxLowerSounds.Size = new System.Drawing.Size(119, 17);
			this.boxLowerSounds.TabIndex = 2;
			this.boxLowerSounds.Text = "Lower other sounds";
			this.boxLowerSounds.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.labelPersonality);
			this.groupBox4.Controls.Add(this.barPersonality);
			this.groupBox4.Location = new System.Drawing.Point(12, 136);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(136, 74);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "😝 Personality";
			// 
			// labelPersonality
			// 
			this.labelPersonality.AutoSize = true;
			this.labelPersonality.Location = new System.Drawing.Point(6, 51);
			this.labelPersonality.Name = "labelPersonality";
			this.labelPersonality.Size = new System.Drawing.Size(42, 13);
			this.labelPersonality.TabIndex = 1;
			this.labelPersonality.Text = "Overkill";
			// 
			// barPersonality
			// 
			this.barPersonality.Cursor = System.Windows.Forms.Cursors.Default;
			this.barPersonality.Location = new System.Drawing.Point(6, 19);
			this.barPersonality.Maximum = 3;
			this.barPersonality.Name = "barPersonality";
			this.barPersonality.Size = new System.Drawing.Size(121, 45);
			this.barPersonality.TabIndex = 0;
			this.barPersonality.Value = 3;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.boxComplain);
			this.groupBox5.Location = new System.Drawing.Point(154, 123);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(136, 53);
			this.groupBox5.TabIndex = 4;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "😈 Complain About";
			// 
			// boxComplain
			// 
			this.boxComplain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.boxComplain.FormattingEnabled = true;
			this.boxComplain.Items.AddRange(new object[] {
            "All",
            "Apps",
            "Resources"});
			this.boxComplain.Location = new System.Drawing.Point(9, 19);
			this.boxComplain.Name = "boxComplain";
			this.boxComplain.Size = new System.Drawing.Size(121, 21);
			this.boxComplain.TabIndex = 0;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(215, 187);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 5;
			this.buttonSave.Text = "💾 Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
			// 
			// FormSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(301, 220);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSettings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "FormSettings";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.barPersonality)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox boxVoice;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox boxNotifications;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label labelPersonality;
		private System.Windows.Forms.TrackBar barPersonality;
		private System.Windows.Forms.CheckBox boxIgnoreFullscreen;
		private System.Windows.Forms.CheckBox boxLowerSounds;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.ComboBox boxComplain;
		private System.Windows.Forms.Button buttonSave;
	}
}