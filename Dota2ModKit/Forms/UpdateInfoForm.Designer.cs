namespace Dota2ModKit.Forms {
	partial class UpdateInfoForm {
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
			this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
			this.changelogTextBox = new MetroFramework.Controls.MetroTextBox();
			this.progressLabel = new MetroFramework.Controls.MetroLabel();
			this.metroProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
			this.updateBtn = new MetroFramework.Controls.MetroButton();
			this.dontUpdateBtn = new MetroFramework.Controls.MetroButton();
			this.SuspendLayout();
			// 
			// metroRadioButton1
			// 
			this.metroRadioButton1.AutoSize = true;
			this.metroRadioButton1.Location = new System.Drawing.Point(268, 4);
			this.metroRadioButton1.Name = "metroRadioButton1";
			this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
			this.metroRadioButton1.TabIndex = 20;
			this.metroRadioButton1.Text = "metroRadioButton1";
			this.metroRadioButton1.UseSelectable = true;
			this.metroRadioButton1.Visible = false;
			// 
			// changelogTextBox
			// 
			this.changelogTextBox.Lines = new string[0];
			this.changelogTextBox.Location = new System.Drawing.Point(23, 63);
			this.changelogTextBox.MaxLength = 32767;
			this.changelogTextBox.Multiline = true;
			this.changelogTextBox.Name = "changelogTextBox";
			this.changelogTextBox.PasswordChar = '\0';
			this.changelogTextBox.ReadOnly = true;
			this.changelogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.changelogTextBox.SelectedText = "";
			this.changelogTextBox.Size = new System.Drawing.Size(350, 211);
			this.changelogTextBox.TabIndex = 21;
			this.changelogTextBox.TabStop = false;
			this.changelogTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.changelogTextBox.UseSelectable = true;
			// 
			// progressLabel
			// 
			this.progressLabel.AutoSize = true;
			this.progressLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.progressLabel.Location = new System.Drawing.Point(125, 281);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(151, 19);
			this.progressLabel.TabIndex = 23;
			this.progressLabel.Text = "Downloading v2.0.0.0...";
			this.progressLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.progressLabel.Visible = false;
			// 
			// metroProgressBar1
			// 
			this.metroProgressBar1.Location = new System.Drawing.Point(23, 305);
			this.metroProgressBar1.Name = "metroProgressBar1";
			this.metroProgressBar1.Size = new System.Drawing.Size(350, 31);
			this.metroProgressBar1.TabIndex = 22;
			this.metroProgressBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroProgressBar1.Visible = false;
			// 
			// updateBtn
			// 
			this.updateBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.updateBtn.Location = new System.Drawing.Point(117, 345);
			this.updateBtn.Margin = new System.Windows.Forms.Padding(4);
			this.updateBtn.Name = "updateBtn";
			this.updateBtn.Size = new System.Drawing.Size(95, 40);
			this.updateBtn.Style = MetroFramework.MetroColorStyle.Yellow;
			this.updateBtn.TabIndex = 24;
			this.updateBtn.TabStop = false;
			this.updateBtn.Text = "Update!";
			this.updateBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.updateBtn.UseSelectable = true;
			this.updateBtn.UseStyleColors = true;
			this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
			// 
			// dontUpdateBtn
			// 
			this.dontUpdateBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.dontUpdateBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.dontUpdateBtn.Location = new System.Drawing.Point(217, 345);
			this.dontUpdateBtn.Margin = new System.Windows.Forms.Padding(4);
			this.dontUpdateBtn.Name = "dontUpdateBtn";
			this.dontUpdateBtn.Size = new System.Drawing.Size(67, 40);
			this.dontUpdateBtn.TabIndex = 25;
			this.dontUpdateBtn.TabStop = false;
			this.dontUpdateBtn.Text = "Cancel";
			this.dontUpdateBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.dontUpdateBtn.UseSelectable = true;
			this.dontUpdateBtn.Click += new System.EventHandler(this.dontUpdateBtn_Click);
			// 
			// UpdateInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.dontUpdateBtn;
			this.ClientSize = new System.Drawing.Size(398, 400);
			this.ControlBox = false;
			this.Controls.Add(this.dontUpdateBtn);
			this.Controls.Add(this.updateBtn);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.metroProgressBar1);
			this.Controls.Add(this.changelogTextBox);
			this.Controls.Add(this.metroRadioButton1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateInfoForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Style = MetroFramework.MetroColorStyle.Red;
			this.Text = "Update Available!";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
		private MetroFramework.Controls.MetroTextBox changelogTextBox;
		private MetroFramework.Controls.MetroLabel progressLabel;
		private MetroFramework.Controls.MetroProgressBar metroProgressBar1;
		private MetroFramework.Controls.MetroButton updateBtn;
		private MetroFramework.Controls.MetroButton dontUpdateBtn;
	}
}