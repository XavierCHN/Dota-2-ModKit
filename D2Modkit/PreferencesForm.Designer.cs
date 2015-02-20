namespace D2ModKit
{
    partial class PreferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.note0LoreCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ugcLabel = new System.Windows.Forms.Label();
            this.ugcTextBox = new System.Windows.Forms.TextBox();
            this.browseUGCButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.kvFileCheckbox = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addKVButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // note0LoreCheckBox
            // 
            this.note0LoreCheckBox.AutoSize = true;
            this.note0LoreCheckBox.Location = new System.Drawing.Point(15, 60);
            this.note0LoreCheckBox.Name = "note0LoreCheckBox";
            this.note0LoreCheckBox.Size = new System.Drawing.Size(170, 17);
            this.note0LoreCheckBox.TabIndex = 1;
            this.note0LoreCheckBox.Text = "Create Note0 and Lore tooltips";
            this.note0LoreCheckBox.UseVisualStyleBackColor = true;
            this.note0LoreCheckBox.CheckedChanged += new System.EventHandler(this.note0LoreCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(129, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "D2ModKit Preferences";
            // 
            // ugcLabel
            // 
            this.ugcLabel.AutoSize = true;
            this.ugcLabel.Location = new System.Drawing.Point(12, 37);
            this.ugcLabel.Name = "ugcLabel";
            this.ugcLabel.Size = new System.Drawing.Size(82, 13);
            this.ugcLabel.TabIndex = 3;
            this.ugcLabel.Text = "dota_ugc path: ";
            // 
            // ugcTextBox
            // 
            this.ugcTextBox.Location = new System.Drawing.Point(100, 34);
            this.ugcTextBox.Name = "ugcTextBox";
            this.ugcTextBox.ReadOnly = true;
            this.ugcTextBox.Size = new System.Drawing.Size(281, 20);
            this.ugcTextBox.TabIndex = 4;
            // 
            // browseUGCButton
            // 
            this.browseUGCButton.Location = new System.Drawing.Point(387, 34);
            this.browseUGCButton.Name = "browseUGCButton";
            this.browseUGCButton.Size = new System.Drawing.Size(56, 20);
            this.browseUGCButton.TabIndex = 5;
            this.browseUGCButton.Text = "Browse";
            this.browseUGCButton.UseVisualStyleBackColor = true;
            this.browseUGCButton.Click += new System.EventHandler(this.browseUGCButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.Navy;
            this.submitButton.Location = new System.Drawing.Point(214, 186);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(92, 34);
            this.submitButton.TabIndex = 6;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // kvFileCheckbox
            // 
            this.kvFileCheckbox.CheckOnClick = true;
            this.kvFileCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kvFileCheckbox.FormattingEnabled = true;
            this.kvFileCheckbox.Items.AddRange(new object[] {
            "Abilities",
            "Items",
            "Units",
            "Heroes"});
            this.kvFileCheckbox.Location = new System.Drawing.Point(11, 101);
            this.kvFileCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.kvFileCheckbox.Name = "kvFileCheckbox";
            this.kvFileCheckbox.Size = new System.Drawing.Size(106, 68);
            this.kvFileCheckbox.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(57, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 19);
            this.label4.TabIndex = 34;
            this.label4.Text = "KV Files";
            // 
            // addKVButton
            // 
            this.addKVButton.Location = new System.Drawing.Point(122, 101);
            this.addKVButton.Name = "addKVButton";
            this.addKVButton.Size = new System.Drawing.Size(46, 23);
            this.addKVButton.TabIndex = 46;
            this.addKVButton.Text = "Add";
            this.addKVButton.UseVisualStyleBackColor = true;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 232);
            this.Controls.Add(this.addKVButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.kvFileCheckbox);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.browseUGCButton);
            this.Controls.Add(this.ugcTextBox);
            this.Controls.Add(this.ugcLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.note0LoreCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.Text = "Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox note0LoreCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label ugcLabel;
        private System.Windows.Forms.TextBox ugcTextBox;
        private System.Windows.Forms.Button browseUGCButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.CheckedListBox kvFileCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addKVButton;
    }
}