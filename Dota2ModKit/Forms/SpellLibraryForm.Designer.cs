namespace Dota2ModKit.Forms {
	partial class SpellLibraryForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpellLibraryForm));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.textBox1 = new MetroFramework.Controls.MetroTextBox();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.SystemColors.ControlText;
			this.treeView1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.treeView1.Location = new System.Drawing.Point(23, 63);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(267, 673);
			this.treeView1.TabIndex = 0;
			// 
			// textBox1
			// 
			this.textBox1.Lines = new string[0];
			this.textBox1.Location = new System.Drawing.Point(456, 63);
			this.textBox1.MaxLength = 32767;
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = '\0';
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.SelectedText = "";
			this.textBox1.Size = new System.Drawing.Size(699, 673);
			this.textBox1.TabIndex = 1;
			this.textBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.textBox1.UseSelectable = true;
			this.textBox1.WordWrap = false;
			// 
			// SpellLibraryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1178, 759);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.treeView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SpellLibraryForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Spell Library Browser";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private MetroFramework.Controls.MetroTextBox textBox1;
	}
}