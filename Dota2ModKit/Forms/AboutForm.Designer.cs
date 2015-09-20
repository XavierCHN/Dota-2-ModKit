namespace Dota2ModKit.Forms {
	partial class AboutForm {
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
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.metroLink13 = new MetroFramework.Controls.MetroLink();
			this.addonNameTextBox = new MetroFramework.Controls.MetroTextBox();
			this.SuspendLayout();
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.metroLabel1.Location = new System.Drawing.Point(23, 72);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(288, 25);
			this.metroLabel1.Style = MetroFramework.MetroColorStyle.Blue;
			this.metroLabel1.TabIndex = 14;
			this.metroLabel1.Text = "Developer: Stephen \"Myll\" Fournier";
			this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// metroLabel2
			// 
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.metroLabel2.Location = new System.Drawing.Point(23, 108);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(116, 25);
			this.metroLabel2.Style = MetroFramework.MetroColorStyle.Blue;
			this.metroLabel2.TabIndex = 15;
			this.metroLabel2.Text = "Contributors:";
			this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLabel2.Visible = false;
			// 
			// metroLink13
			// 
			this.metroLink13.FontSize = MetroFramework.MetroLinkSize.Medium;
			this.metroLink13.Location = new System.Drawing.Point(23, 311);
			this.metroLink13.Name = "metroLink13";
			this.metroLink13.Size = new System.Drawing.Size(103, 26);
			this.metroLink13.TabIndex = 28;
			this.metroLink13.Text = "Report a bug!";
			this.metroLink13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.metroLink13.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLink13.UseSelectable = true;
			// 
			// addonNameTextBox
			// 
			this.addonNameTextBox.Lines = new string[] {
        "Noya: Feature ideas\\r\\nRoyAwesome: KVLib\\r\\npenguinwizzard: Icon\\r\\nToraxXx: Deco" +
            "mpiled particles\\nXavierCHN: Helping with VTEX features\\r\\nSebRut: Ideas and C# " +
            "tips."};
			this.addonNameTextBox.Location = new System.Drawing.Point(23, 136);
			this.addonNameTextBox.MaxLength = 32767;
			this.addonNameTextBox.Multiline = true;
			this.addonNameTextBox.Name = "addonNameTextBox";
			this.addonNameTextBox.PasswordChar = '\0';
			this.addonNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.addonNameTextBox.SelectedText = "";
			this.addonNameTextBox.Size = new System.Drawing.Size(370, 152);
			this.addonNameTextBox.TabIndex = 29;
			this.addonNameTextBox.Text = "Noya: Feature ideas\\r\\nRoyAwesome: KVLib\\r\\npenguinwizzard: Icon\\r\\nToraxXx: Deco" +
    "mpiled particles\\nXavierCHN: Helping with VTEX features\\r\\nSebRut: Ideas and C# " +
    "tips.";
			this.addonNameTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.addonNameTextBox.UseSelectable = true;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(485, 511);
			this.Controls.Add(this.addonNameTextBox);
			this.Controls.Add(this.metroLink13);
			this.Controls.Add(this.metroLabel2);
			this.Controls.Add(this.metroLabel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About D2ModKit";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroLabel metroLabel1;
		private MetroFramework.Controls.MetroLabel metroLabel2;
		private MetroFramework.Controls.MetroLink metroLink13;
		private MetroFramework.Controls.MetroTextBox addonNameTextBox;
	}
}