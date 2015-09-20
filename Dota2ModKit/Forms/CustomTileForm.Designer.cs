namespace Dota2ModKit.Forms {
	partial class CustomTileForm {
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
			this.folderRadioButton = new MetroFramework.Controls.MetroRadioButton();
			this.fileRadioButton = new MetroFramework.Controls.MetroRadioButton();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.browseBtn = new MetroFramework.Controls.MetroButton();
			this.okButton = new MetroFramework.Controls.MetroButton();
			this.customTileTitleTextBox = new MetroFramework.Controls.MetroTextBox();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
			this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
			this.pathTextBox1 = new MetroFramework.Controls.MetroTextBox();
			this.metroPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// folderRadioButton
			// 
			this.folderRadioButton.AutoSize = true;
			this.folderRadioButton.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
			this.folderRadioButton.Location = new System.Drawing.Point(92, 69);
			this.folderRadioButton.Name = "folderRadioButton";
			this.folderRadioButton.Size = new System.Drawing.Size(98, 19);
			this.folderRadioButton.TabIndex = 0;
			this.folderRadioButton.Text = "Open folder";
			this.folderRadioButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.folderRadioButton.UseSelectable = true;
			// 
			// fileRadioButton
			// 
			this.fileRadioButton.AutoSize = true;
			this.fileRadioButton.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
			this.fileRadioButton.Location = new System.Drawing.Point(196, 69);
			this.fileRadioButton.Name = "fileRadioButton";
			this.fileRadioButton.Size = new System.Drawing.Size(80, 19);
			this.fileRadioButton.TabIndex = 1;
			this.fileRadioButton.Text = "Open file";
			this.fileRadioButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.fileRadioButton.UseSelectable = true;
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
			this.metroLabel1.Location = new System.Drawing.Point(19, 69);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(67, 19);
			this.metroLabel1.TabIndex = 2;
			this.metroLabel1.Text = "On click:";
			this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// browseBtn
			// 
			this.browseBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.browseBtn.Location = new System.Drawing.Point(23, 97);
			this.browseBtn.Margin = new System.Windows.Forms.Padding(4);
			this.browseBtn.Name = "browseBtn";
			this.browseBtn.Size = new System.Drawing.Size(88, 28);
			this.browseBtn.TabIndex = 15;
			this.browseBtn.TabStop = false;
			this.browseBtn.Text = "Browse";
			this.browseBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.browseBtn.UseSelectable = true;
			this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
			// 
			// okButton
			// 
			this.okButton.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.okButton.Location = new System.Drawing.Point(97, 217);
			this.okButton.Margin = new System.Windows.Forms.Padding(4);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(87, 36);
			this.okButton.TabIndex = 17;
			this.okButton.TabStop = false;
			this.okButton.Text = "OK";
			this.okButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.okButton.UseSelectable = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// customTileTitleTextBox
			// 
			this.customTileTitleTextBox.Lines = new string[0];
			this.customTileTitleTextBox.Location = new System.Drawing.Point(219, 168);
			this.customTileTitleTextBox.MaxLength = 2;
			this.customTileTitleTextBox.Name = "customTileTitleTextBox";
			this.customTileTitleTextBox.PasswordChar = '\0';
			this.customTileTitleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.customTileTitleTextBox.SelectedText = "";
			this.customTileTitleTextBox.Size = new System.Drawing.Size(30, 23);
			this.customTileTitleTextBox.TabIndex = 18;
			this.customTileTitleTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.customTileTitleTextBox.UseSelectable = true;
			// 
			// metroLabel2
			// 
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.metroLabel2.Location = new System.Drawing.Point(15, 168);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(198, 19);
			this.metroLabel2.TabIndex = 19;
			this.metroLabel2.Text = "Enter custom tile title (2 chars):";
			this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLabel2.WrapToLine = true;
			// 
			// metroRadioButton1
			// 
			this.metroRadioButton1.AutoSize = true;
			this.metroRadioButton1.Location = new System.Drawing.Point(18, 10);
			this.metroRadioButton1.Name = "metroRadioButton1";
			this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
			this.metroRadioButton1.TabIndex = 20;
			this.metroRadioButton1.Text = "metroRadioButton1";
			this.metroRadioButton1.UseSelectable = true;
			this.metroRadioButton1.Visible = false;
			// 
			// metroPanel1
			// 
			this.metroPanel1.Controls.Add(this.metroRadioButton1);
			this.metroPanel1.HorizontalScrollbarBarColor = true;
			this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
			this.metroPanel1.HorizontalScrollbarSize = 10;
			this.metroPanel1.Location = new System.Drawing.Point(108, 4);
			this.metroPanel1.Name = "metroPanel1";
			this.metroPanel1.Size = new System.Drawing.Size(152, 29);
			this.metroPanel1.TabIndex = 21;
			this.metroPanel1.VerticalScrollbarBarColor = true;
			this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
			this.metroPanel1.VerticalScrollbarSize = 10;
			this.metroPanel1.Visible = false;
			// 
			// pathTextBox1
			// 
			this.pathTextBox1.Lines = new string[0];
			this.pathTextBox1.Location = new System.Drawing.Point(15, 135);
			this.pathTextBox1.MaxLength = 32767;
			this.pathTextBox1.Name = "pathTextBox1";
			this.pathTextBox1.PasswordChar = '\0';
			this.pathTextBox1.ReadOnly = true;
			this.pathTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.pathTextBox1.SelectedText = "";
			this.pathTextBox1.Size = new System.Drawing.Size(254, 23);
			this.pathTextBox1.TabIndex = 22;
			this.pathTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.pathTextBox1.UseSelectable = true;
			// 
			// CustomTileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 266);
			this.Controls.Add(this.pathTextBox1);
			this.Controls.Add(this.metroPanel1);
			this.Controls.Add(this.metroLabel2);
			this.Controls.Add(this.customTileTitleTextBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.browseBtn);
			this.Controls.Add(this.metroLabel1);
			this.Controls.Add(this.fileRadioButton);
			this.Controls.Add(this.folderRadioButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CustomTileForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Style = MetroFramework.MetroColorStyle.Yellow;
			this.Text = "Custom Tile Creator";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroPanel1.ResumeLayout(false);
			this.metroPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroRadioButton folderRadioButton;
		private MetroFramework.Controls.MetroRadioButton fileRadioButton;
		private MetroFramework.Controls.MetroLabel metroLabel1;
		private MetroFramework.Controls.MetroButton browseBtn;
		private MetroFramework.Controls.MetroButton okButton;
		private MetroFramework.Controls.MetroTextBox customTileTitleTextBox;
		private MetroFramework.Controls.MetroLabel metroLabel2;
		private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
		private MetroFramework.Controls.MetroPanel metroPanel1;
		private MetroFramework.Controls.MetroTextBox pathTextBox1;
	}
}