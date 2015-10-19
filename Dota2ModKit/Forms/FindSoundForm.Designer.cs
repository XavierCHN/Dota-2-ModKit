namespace Dota2ModKit.Forms {
	partial class FindSoundForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindSoundForm));
			this.soundNamesTextBox = new MetroFramework.Controls.MetroTextBox();
			this.vsndLabel = new MetroFramework.Controls.MetroLabel();
			this.vsndTextBox = new MetroFramework.Controls.MetroTextBox();
			this.addonNameLabel = new MetroFramework.Controls.MetroLabel();
			this.okBtn = new MetroFramework.Controls.MetroButton();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.metroScrollBar1 = new MetroFramework.Controls.MetroScrollBar();
			this.SuspendLayout();
			// 
			// soundNamesTextBox
			// 
			this.soundNamesTextBox.Lines = new string[] {
        "metroTextBox1"};
			this.soundNamesTextBox.Location = new System.Drawing.Point(23, 152);
			this.soundNamesTextBox.MaxLength = 32767;
			this.soundNamesTextBox.Multiline = true;
			this.soundNamesTextBox.Name = "soundNamesTextBox";
			this.soundNamesTextBox.PasswordChar = '\0';
			this.soundNamesTextBox.ReadOnly = true;
			this.soundNamesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.soundNamesTextBox.SelectedText = "";
			this.soundNamesTextBox.Size = new System.Drawing.Size(459, 232);
			this.soundNamesTextBox.TabIndex = 0;
			this.soundNamesTextBox.TabStop = false;
			this.soundNamesTextBox.Text = "metroTextBox1";
			this.soundNamesTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.soundNamesTextBox.UseSelectable = true;
			// 
			// vsndLabel
			// 
			this.vsndLabel.AutoSize = true;
			this.vsndLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.vsndLabel.Location = new System.Drawing.Point(23, 60);
			this.vsndLabel.Name = "vsndLabel";
			this.vsndLabel.Size = new System.Drawing.Size(160, 19);
			this.vsndLabel.Style = MetroFramework.MetroColorStyle.Blue;
			this.vsndLabel.TabIndex = 10;
			this.vsndLabel.Text = "Enter .vsnd relative path:";
			this.vsndLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// vsndTextBox
			// 
			this.vsndTextBox.Lines = new string[0];
			this.vsndTextBox.Location = new System.Drawing.Point(23, 104);
			this.vsndTextBox.MaxLength = 32767;
			this.vsndTextBox.Name = "vsndTextBox";
			this.vsndTextBox.PasswordChar = '\0';
			this.vsndTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.vsndTextBox.SelectedText = "";
			this.vsndTextBox.Size = new System.Drawing.Size(459, 23);
			this.vsndTextBox.TabIndex = 11;
			this.vsndTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.vsndTextBox.UseSelectable = true;
			// 
			// addonNameLabel
			// 
			this.addonNameLabel.AutoSize = true;
			this.addonNameLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.addonNameLabel.Location = new System.Drawing.Point(23, 130);
			this.addonNameLabel.Name = "addonNameLabel";
			this.addonNameLabel.Size = new System.Drawing.Size(97, 19);
			this.addonNameLabel.Style = MetroFramework.MetroColorStyle.Blue;
			this.addonNameLabel.TabIndex = 12;
			this.addonNameLabel.Text = "Sound Names:";
			this.addonNameLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// okBtn
			// 
			this.okBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.okBtn.Location = new System.Drawing.Point(196, 398);
			this.okBtn.Margin = new System.Windows.Forms.Padding(4);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(125, 33);
			this.okBtn.TabIndex = 13;
			this.okBtn.TabStop = false;
			this.okBtn.Text = "OK";
			this.okBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.okBtn.UseSelectable = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.metroLabel1.Location = new System.Drawing.Point(23, 82);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(251, 19);
			this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
			this.metroLabel1.TabIndex = 14;
			this.metroLabel1.Text = "(ex: sounds/ambient/horn_radiant.vsnd)";
			this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// metroScrollBar1
			// 
			this.metroScrollBar1.LargeChange = 10;
			this.metroScrollBar1.Location = new System.Drawing.Point(472, 152);
			this.metroScrollBar1.Maximum = 100;
			this.metroScrollBar1.Minimum = 0;
			this.metroScrollBar1.MouseWheelBarPartitions = 10;
			this.metroScrollBar1.Name = "metroScrollBar1";
			this.metroScrollBar1.Orientation = MetroFramework.Controls.MetroScrollOrientation.Vertical;
			this.metroScrollBar1.ScrollbarSize = 10;
			this.metroScrollBar1.Size = new System.Drawing.Size(10, 232);
			this.metroScrollBar1.TabIndex = 15;
			this.metroScrollBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroScrollBar1.UseSelectable = true;
			this.metroScrollBar1.Visible = false;
			// 
			// FindSoundForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(505, 455);
			this.Controls.Add(this.metroScrollBar1);
			this.Controls.Add(this.metroLabel1);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.addonNameLabel);
			this.Controls.Add(this.vsndTextBox);
			this.Controls.Add(this.vsndLabel);
			this.Controls.Add(this.soundNamesTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindSoundForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Style = MetroFramework.MetroColorStyle.Red;
			this.Text = "Find Sound Name";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroTextBox soundNamesTextBox;
		private MetroFramework.Controls.MetroLabel vsndLabel;
		private MetroFramework.Controls.MetroTextBox vsndTextBox;
		private MetroFramework.Controls.MetroLabel addonNameLabel;
		private MetroFramework.Controls.MetroButton okBtn;
		private MetroFramework.Controls.MetroLabel metroLabel1;
		private MetroFramework.Controls.MetroScrollBar metroScrollBar1;
	}
}