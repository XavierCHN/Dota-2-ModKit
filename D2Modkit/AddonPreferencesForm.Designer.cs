namespace D2ModKit {
	partial class AddonPreferencesForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddonPreferencesForm));
			this.addLibraryBtn = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.preferencesForLabel = new System.Windows.Forms.Label();
			this.addKVButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.kvFileCheckedListbox = new System.Windows.Forms.CheckedListBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.note0LoreCheckBox = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// addLibraryBtn
			// 
			this.addLibraryBtn.Location = new System.Drawing.Point(280, 85);
			this.addLibraryBtn.Name = "addLibraryBtn";
			this.addLibraryBtn.Size = new System.Drawing.Size(48, 25);
			this.addLibraryBtn.TabIndex = 60;
			this.addLibraryBtn.Text = "Add";
			this.addLibraryBtn.UseVisualStyleBackColor = true;
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(174, 86);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(100, 121);
			this.listBox1.TabIndex = 59;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Maroon;
			this.label2.Location = new System.Drawing.Point(170, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 19);
			this.label2.TabIndex = 58;
			this.label2.Text = "Libraries Used";
			// 
			// preferencesForLabel
			// 
			this.preferencesForLabel.AutoSize = true;
			this.preferencesForLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.preferencesForLabel.ForeColor = System.Drawing.Color.Navy;
			this.preferencesForLabel.Location = new System.Drawing.Point(63, 9);
			this.preferencesForLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
			this.preferencesForLabel.Name = "preferencesForLabel";
			this.preferencesForLabel.Size = new System.Drawing.Size(268, 25);
			this.preferencesForLabel.TabIndex = 57;
			this.preferencesForLabel.Text = "Preferences for addon_name";
			// 
			// addKVButton
			// 
			this.addKVButton.Location = new System.Drawing.Point(116, 85);
			this.addKVButton.Name = "addKVButton";
			this.addKVButton.Size = new System.Drawing.Size(48, 25);
			this.addKVButton.TabIndex = 56;
			this.addKVButton.Text = "Add";
			this.toolTip1.SetToolTip(this.addKVButton, "Add KV File to use for the \"Combine KV Files\" button");
			this.addKVButton.UseVisualStyleBackColor = true;
			this.addKVButton.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Maroon;
			this.label4.Location = new System.Drawing.Point(38, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 19);
			this.label4.TabIndex = 55;
			this.label4.Text = "KV Files";
			this.toolTip1.SetToolTip(this.label4, "\r\n");
			// 
			// kvFileCheckedListbox
			// 
			this.kvFileCheckedListbox.CheckOnClick = true;
			this.kvFileCheckedListbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kvFileCheckedListbox.FormattingEnabled = true;
			this.kvFileCheckedListbox.Location = new System.Drawing.Point(11, 85);
			this.kvFileCheckedListbox.Margin = new System.Windows.Forms.Padding(2);
			this.kvFileCheckedListbox.Name = "kvFileCheckedListbox";
			this.kvFileCheckedListbox.Size = new System.Drawing.Size(100, 116);
			this.kvFileCheckedListbox.TabIndex = 54;
			// 
			// submitButton
			// 
			this.submitButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.submitButton.ForeColor = System.Drawing.Color.Navy;
			this.submitButton.Location = new System.Drawing.Point(130, 343);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(115, 35);
			this.submitButton.TabIndex = 53;
			this.submitButton.Text = "Save";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// note0LoreCheckBox
			// 
			this.note0LoreCheckBox.AutoSize = true;
			this.note0LoreCheckBox.Location = new System.Drawing.Point(15, 42);
			this.note0LoreCheckBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
			this.note0LoreCheckBox.Name = "note0LoreCheckBox";
			this.note0LoreCheckBox.Size = new System.Drawing.Size(170, 17);
			this.note0LoreCheckBox.TabIndex = 52;
			this.note0LoreCheckBox.Text = "Create Note0 and Lore tooltips";
			this.note0LoreCheckBox.UseVisualStyleBackColor = true;
			// 
			// AddonPreferencesForm
			// 
			this.AcceptButton = this.submitButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(375, 390);
			this.Controls.Add(this.addLibraryBtn);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.preferencesForLabel);
			this.Controls.Add(this.addKVButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.kvFileCheckedListbox);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.note0LoreCheckBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddonPreferencesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AddonPreferencesForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button addLibraryBtn;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label preferencesForLabel;
		private System.Windows.Forms.Button addKVButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckedListBox kvFileCheckedListbox;
		private System.Windows.Forms.Button submitButton;
		private System.Windows.Forms.CheckBox note0LoreCheckBox;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}