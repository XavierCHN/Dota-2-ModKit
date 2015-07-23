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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpellLibraryForm));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.copySpellBtn = new MetroFramework.Controls.MetroButton();
			this.luaKVBtn = new MetroFramework.Controls.MetroButton();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.notificationLabel = new MetroFramework.Controls.MetroLabel();
			this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
			this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
			this.metroButton1 = new MetroFramework.Controls.MetroButton();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.textBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
			((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
			this.treeView1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeView1.ForeColor = System.Drawing.Color.White;
			this.treeView1.LineColor = System.Drawing.Color.White;
			this.treeView1.Location = new System.Drawing.Point(23, 63);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(228, 673);
			this.treeView1.TabIndex = 0;
			// 
			// copySpellBtn
			// 
			this.copySpellBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.copySpellBtn.Location = new System.Drawing.Point(258, 181);
			this.copySpellBtn.Margin = new System.Windows.Forms.Padding(4);
			this.copySpellBtn.Name = "copySpellBtn";
			this.copySpellBtn.Size = new System.Drawing.Size(120, 40);
			this.copySpellBtn.TabIndex = 14;
			this.copySpellBtn.TabStop = false;
			this.copySpellBtn.Text = "Copy";
			this.copySpellBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.copySpellBtn, "Copies the contents on the right to the clipboard");
			this.copySpellBtn.UseSelectable = true;
			this.copySpellBtn.Click += new System.EventHandler(this.copySpellBtn_Click);
			// 
			// luaKVBtn
			// 
			this.luaKVBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.luaKVBtn.Location = new System.Drawing.Point(258, 229);
			this.luaKVBtn.Margin = new System.Windows.Forms.Padding(4);
			this.luaKVBtn.Name = "luaKVBtn";
			this.luaKVBtn.Size = new System.Drawing.Size(120, 40);
			this.luaKVBtn.TabIndex = 15;
			this.luaKVBtn.TabStop = false;
			this.luaKVBtn.Text = "Lua Script";
			this.luaKVBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.luaKVBtn, "Opens the Lua script for this spell");
			this.luaKVBtn.UseSelectable = true;
			this.luaKVBtn.Click += new System.EventHandler(this.luaKVBtn_Click);
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.metroLabel1.Location = new System.Drawing.Point(782, 35);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(76, 25);
			this.metroLabel1.TabIndex = 17;
			this.metroLabel1.Text = "Preview:";
			this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// notificationLabel
			// 
			this.notificationLabel.AutoSize = true;
			this.notificationLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.notificationLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.notificationLabel.Location = new System.Drawing.Point(257, 273);
			this.notificationLabel.Name = "notificationLabel";
			this.notificationLabel.Size = new System.Drawing.Size(101, 25);
			this.notificationLabel.TabIndex = 18;
			this.notificationLabel.Text = "notification";
			this.notificationLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.notificationLabel.UseStyleColors = true;
			// 
			// metroRadioButton1
			// 
			this.metroRadioButton1.AutoSize = true;
			this.metroRadioButton1.Location = new System.Drawing.Point(84, -1);
			this.metroRadioButton1.Name = "metroRadioButton1";
			this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
			this.metroRadioButton1.TabIndex = 19;
			this.metroRadioButton1.Text = "metroRadioButton1";
			this.metroRadioButton1.UseSelectable = true;
			this.metroRadioButton1.Visible = false;
			// 
			// metroToolTip1
			// 
			this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
			this.metroToolTip1.StyleManager = null;
			this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
			// 
			// metroButton1
			// 
			this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.metroButton1.Location = new System.Drawing.Point(258, 582);
			this.metroButton1.Margin = new System.Windows.Forms.Padding(4);
			this.metroButton1.Name = "metroButton1";
			this.metroButton1.Size = new System.Drawing.Size(120, 40);
			this.metroButton1.TabIndex = 20;
			this.metroButton1.TabStop = false;
			this.metroButton1.Text = "Open File";
			this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.metroToolTip1.SetToolTip(this.metroButton1, "Opens the file associated with the contents in the\r\ntextbox on the right. NOTE: D" +
        "on\'t change anything\r\nin the file!");
			this.metroButton1.UseSelectable = true;
			this.metroButton1.Visible = false;
			// 
			// metroLabel2
			// 
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.Location = new System.Drawing.Point(257, 63);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(101, 114);
			this.metroLabel2.TabIndex = 21;
			this.metroLabel2.Text = "Note: Only the\r\nentries currently\r\nshown have\r\nbeen worked\r\non in\r\nSpellLibrary.";
			this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// textBox1
			// 
			this.textBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
			this.textBox1.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
			this.textBox1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
			this.textBox1.BackBrush = null;
			this.textBox1.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
			this.textBox1.CharHeight = 14;
			this.textBox1.CharWidth = 8;
			this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.textBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.textBox1.IsReplaceMode = false;
			this.textBox1.Language = FastColoredTextBoxNS.Language.JS;
			this.textBox1.LeftBracket = '(';
			this.textBox1.LeftBracket2 = '{';
			this.textBox1.Location = new System.Drawing.Point(385, 63);
			this.textBox1.Name = "textBox1";
			this.textBox1.Paddings = new System.Windows.Forms.Padding(0);
			this.textBox1.ReadOnly = true;
			this.textBox1.RightBracket = ')';
			this.textBox1.RightBracket2 = '}';
			this.textBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.textBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBox1.ServiceColors")));
			this.textBox1.ShowFoldingLines = true;
			this.textBox1.Size = new System.Drawing.Size(855, 673);
			this.textBox1.TabIndex = 23;
			this.textBox1.Zoom = 100;
			// 
			// SpellLibraryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1263, 759);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.metroLabel2);
			this.Controls.Add(this.metroButton1);
			this.Controls.Add(this.metroRadioButton1);
			this.Controls.Add(this.notificationLabel);
			this.Controls.Add(this.metroLabel1);
			this.Controls.Add(this.luaKVBtn);
			this.Controls.Add(this.copySpellBtn);
			this.Controls.Add(this.treeView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SpellLibraryForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Style = MetroFramework.MetroColorStyle.Magenta;
			this.Text = "Spell Library Browser";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private MetroFramework.Controls.MetroButton copySpellBtn;
		private MetroFramework.Controls.MetroButton luaKVBtn;
		private MetroFramework.Controls.MetroLabel metroLabel1;
		private MetroFramework.Controls.MetroLabel notificationLabel;
		private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
		private MetroFramework.Components.MetroToolTip metroToolTip1;
		private MetroFramework.Controls.MetroButton metroButton1;
		private MetroFramework.Controls.MetroLabel metroLabel2;
		private FastColoredTextBoxNS.FastColoredTextBox textBox1;
	}
}