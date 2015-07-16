namespace Dota2ModKit {
	partial class UpdateForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
			this.progressLabel = new MetroFramework.Controls.MetroLabel();
			this.metroProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
			this.SuspendLayout();
			// 
			// progressLabel
			// 
			this.progressLabel.AutoSize = true;
			this.progressLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.progressLabel.Location = new System.Drawing.Point(148, 60);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(151, 19);
			this.progressLabel.Style = MetroFramework.MetroColorStyle.Blue;
			this.progressLabel.TabIndex = 13;
			this.progressLabel.Text = "Downloading v2.0.0.0...";
			this.progressLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// metroProgressBar1
			// 
			this.metroProgressBar1.Location = new System.Drawing.Point(23, 82);
			this.metroProgressBar1.Name = "metroProgressBar1";
			this.metroProgressBar1.Size = new System.Drawing.Size(396, 22);
			this.metroProgressBar1.TabIndex = 12;
			this.metroProgressBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 128);
			this.ControlBox = false;
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.metroProgressBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Style = MetroFramework.MetroColorStyle.Red;
			this.Text = "Updating Dota 2 ModKit";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroLabel progressLabel;
		private MetroFramework.Controls.MetroProgressBar metroProgressBar1;
	}
}