namespace Dota2ModKit.Forms {
	partial class DonateForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DonateForm));
			this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
			this.donatePictureBox = new System.Windows.Forms.PictureBox();
			this.label1 = new MetroFramework.Controls.MetroLabel();
			((System.ComponentModel.ISupportInitialize)(this.donatePictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// metroTextBox1
			// 
			this.metroTextBox1.FontSize = MetroFramework.MetroTextBoxSize.Medium;
			this.metroTextBox1.Lines = new string[0];
			this.metroTextBox1.Location = new System.Drawing.Point(20, 88);
			this.metroTextBox1.MaxLength = 32767;
			this.metroTextBox1.Multiline = true;
			this.metroTextBox1.Name = "metroTextBox1";
			this.metroTextBox1.PasswordChar = '\0';
			this.metroTextBox1.ReadOnly = true;
			this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.metroTextBox1.SelectedText = "";
			this.metroTextBox1.Size = new System.Drawing.Size(544, 404);
			this.metroTextBox1.TabIndex = 10;
			this.metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroTextBox1.UseSelectable = true;
			// 
			// donatePictureBox
			// 
			this.donatePictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.donatePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("donatePictureBox.Image")));
			this.donatePictureBox.Location = new System.Drawing.Point(180, 504);
			this.donatePictureBox.Name = "donatePictureBox";
			this.donatePictureBox.Size = new System.Drawing.Size(212, 88);
			this.donatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.donatePictureBox.TabIndex = 11;
			this.donatePictureBox.TabStop = false;
			this.donatePictureBox.Click += new System.EventHandler(this.donatePictureBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.label1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
			this.label1.Location = new System.Drawing.Point(28, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(262, 25);
			this.label1.Style = MetroFramework.MetroColorStyle.Teal;
			this.label1.TabIndex = 12;
			this.label1.Text = "A message from the creator...";
			this.label1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.label1.UseStyleColors = true;
			// 
			// DonateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(587, 608);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.donatePictureBox);
			this.Controls.Add(this.metroTextBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DonateForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Donate!";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			((System.ComponentModel.ISupportInitialize)(this.donatePictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroTextBox metroTextBox1;
		private System.Windows.Forms.PictureBox donatePictureBox;
		private MetroFramework.Controls.MetroLabel label1;
	}
}