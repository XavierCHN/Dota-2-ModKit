namespace Dota2ModKit {
	partial class SingleTextboxForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleTextboxForm));
			this.metroButton1 = new MetroFramework.Controls.MetroButton();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
			this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
			this.SuspendLayout();
			// 
			// metroButton1
			// 
			this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.metroButton1.Location = new System.Drawing.Point(83, 144);
			this.metroButton1.Name = "metroButton1";
			this.metroButton1.Size = new System.Drawing.Size(132, 27);
			this.metroButton1.TabIndex = 0;
			this.metroButton1.TabStop = false;
			this.metroButton1.Text = "metroButton1";
			this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroButton1.UseSelectable = true;
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.Location = new System.Drawing.Point(23, 69);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(83, 19);
			this.metroLabel1.TabIndex = 3;
			this.metroLabel1.Text = "metroLabel1";
			this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroLabel1.WrapToLine = true;
			// 
			// metroTextBox1
			// 
			this.metroTextBox1.Lines = new string[] {
        "metroTextBox1"};
			this.metroTextBox1.Location = new System.Drawing.Point(23, 103);
			this.metroTextBox1.MaxLength = 32767;
			this.metroTextBox1.Name = "metroTextBox1";
			this.metroTextBox1.PasswordChar = '\0';
			this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.metroTextBox1.SelectedText = "";
			this.metroTextBox1.Size = new System.Drawing.Size(256, 23);
			this.metroTextBox1.TabIndex = 4;
			this.metroTextBox1.Text = "metroTextBox1";
			this.metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroTextBox1.UseSelectable = true;
			// 
			// metroRadioButton1
			// 
			this.metroRadioButton1.AutoSize = true;
			this.metroRadioButton1.Location = new System.Drawing.Point(170, 9);
			this.metroRadioButton1.Name = "metroRadioButton1";
			this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
			this.metroRadioButton1.TabIndex = 7;
			this.metroRadioButton1.Text = "metroRadioButton1";
			this.metroRadioButton1.UseSelectable = true;
			this.metroRadioButton1.Visible = false;
			// 
			// SingleTextboxForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(302, 194);
			this.Controls.Add(this.metroRadioButton1);
			this.Controls.Add(this.metroTextBox1);
			this.Controls.Add(this.metroLabel1);
			this.Controls.Add(this.metroButton1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SingleTextboxForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SingleTextboxForm";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.Load += new System.EventHandler(this.SingleTextboxForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroButton metroButton1;
		private MetroFramework.Controls.MetroLabel metroLabel1;
		private MetroFramework.Controls.MetroTextBox metroTextBox1;
		private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
	}
}