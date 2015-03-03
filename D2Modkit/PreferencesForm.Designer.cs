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
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ugcLabel = new System.Windows.Forms.Label();
            this.ugcTextBox = new System.Windows.Forms.TextBox();
            this.browseUGCButton = new System.Windows.Forms.Button();
            this.checkForUpdatesCheckbox = new System.Windows.Forms.CheckBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(126, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "D2ModKit Preferences";
            // 
            // ugcLabel
            // 
            this.ugcLabel.AutoSize = true;
            this.ugcLabel.Location = new System.Drawing.Point(11, 48);
            this.ugcLabel.Name = "ugcLabel";
            this.ugcLabel.Size = new System.Drawing.Size(82, 13);
            this.ugcLabel.TabIndex = 3;
            this.ugcLabel.Text = "dota_ugc path: ";
            // 
            // ugcTextBox
            // 
            this.ugcTextBox.Location = new System.Drawing.Point(94, 45);
            this.ugcTextBox.Name = "ugcTextBox";
            this.ugcTextBox.ReadOnly = true;
            this.ugcTextBox.Size = new System.Drawing.Size(281, 20);
            this.ugcTextBox.TabIndex = 4;
            // 
            // browseUGCButton
            // 
            this.browseUGCButton.Location = new System.Drawing.Point(381, 45);
            this.browseUGCButton.Name = "browseUGCButton";
            this.browseUGCButton.Size = new System.Drawing.Size(67, 20);
            this.browseUGCButton.TabIndex = 5;
            this.browseUGCButton.Text = "Browse";
            this.browseUGCButton.UseVisualStyleBackColor = true;
            this.browseUGCButton.Click += new System.EventHandler(this.browseUGCButton_Click);
            // 
            // checkForUpdatesCheckbox
            // 
            this.checkForUpdatesCheckbox.AutoSize = true;
            this.checkForUpdatesCheckbox.Location = new System.Drawing.Point(14, 82);
            this.checkForUpdatesCheckbox.Name = "checkForUpdatesCheckbox";
            this.checkForUpdatesCheckbox.Size = new System.Drawing.Size(113, 17);
            this.checkForUpdatesCheckbox.TabIndex = 48;
            this.checkForUpdatesCheckbox.Text = "Check for updates";
            this.checkForUpdatesCheckbox.UseVisualStyleBackColor = true;
            this.checkForUpdatesCheckbox.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckbox_CheckedChanged);
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.Navy;
            this.submitButton.Location = new System.Drawing.Point(171, 160);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(115, 35);
            this.submitButton.TabIndex = 54;
            this.submitButton.Text = "Save";
            this.submitButton.UseVisualStyleBackColor = true;
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 207);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.checkForUpdatesCheckbox);
            this.Controls.Add(this.browseUGCButton);
            this.Controls.Add(this.ugcTextBox);
            this.Controls.Add(this.ugcLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModKit Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label ugcLabel;
        private System.Windows.Forms.TextBox ugcTextBox;
		private System.Windows.Forms.Button browseUGCButton;
		private System.Windows.Forms.CheckBox checkForUpdatesCheckbox;
		private System.Windows.Forms.Button submitButton;
    }
}