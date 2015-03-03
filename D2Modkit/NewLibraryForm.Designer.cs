namespace D2ModKit
{
    partial class NewLibraryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewLibraryForm));
            this.linkToMainFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.submitBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.libraryNameTextBox = new System.Windows.Forms.TextBox();
            this.updatesCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.libraryListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.browseLocalBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.LocalTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // linkToMainFileTextBox
            // 
            this.linkToMainFileTextBox.Location = new System.Drawing.Point(42, 108);
            this.linkToMainFileTextBox.Name = "linkToMainFileTextBox";
            this.linkToMainFileTextBox.Size = new System.Drawing.Size(243, 20);
            this.linkToMainFileTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Web link to file that contains the version info:\r\n(ex. the main .lua or .as file)" +
    "";
            // 
            // submitBtn
            // 
            this.submitBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitBtn.ForeColor = System.Drawing.Color.Navy;
            this.submitBtn.Location = new System.Drawing.Point(250, 311);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(98, 38);
            this.submitBtn.TabIndex = 2;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Library name:";
            // 
            // libraryNameTextBox
            // 
            this.libraryNameTextBox.Location = new System.Drawing.Point(42, 56);
            this.libraryNameTextBox.Name = "libraryNameTextBox";
            this.libraryNameTextBox.Size = new System.Drawing.Size(166, 20);
            this.libraryNameTextBox.TabIndex = 4;
            // 
            // updatesCheckBox
            // 
            this.updatesCheckBox.AutoSize = true;
            this.updatesCheckBox.Location = new System.Drawing.Point(131, 233);
            this.updatesCheckBox.Name = "updatesCheckBox";
            this.updatesCheckBox.Size = new System.Drawing.Size(137, 17);
            this.updatesCheckBox.TabIndex = 5;
            this.updatesCheckBox.Text = "Auto check for updates";
            this.updatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(299, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "OR";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(379, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Choose from already supported libraries:\r\n(multiselect is enabled)";
            // 
            // libraryListBox
            // 
            this.libraryListBox.FormattingEnabled = true;
            this.libraryListBox.Items.AddRange(new object[] {
            "BMD Physics",
            "BuildingHelper",
            "Popups"});
            this.libraryListBox.Location = new System.Drawing.Point(382, 69);
            this.libraryListBox.Name = "libraryListBox";
            this.libraryListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.libraryListBox.Size = new System.Drawing.Size(117, 121);
            this.libraryListBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(220, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 21);
            this.label5.TabIndex = 50;
            this.label5.Text = "Library Preferences";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(95, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(328, 21);
            this.label6.TabIndex = 51;
            this.label6.Text = "Choose libraries to add version control to:";
            // 
            // browseLocalBtn
            // 
            this.browseLocalBtn.Location = new System.Drawing.Point(42, 160);
            this.browseLocalBtn.Name = "browseLocalBtn";
            this.browseLocalBtn.Size = new System.Drawing.Size(62, 20);
            this.browseLocalBtn.TabIndex = 52;
            this.browseLocalBtn.Text = "Browse";
            this.browseLocalBtn.UseVisualStyleBackColor = true;
            this.browseLocalBtn.Click += new System.EventHandler(this.browseLocalBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 26);
            this.label7.TabIndex = 53;
            this.label7.Text = "Local path to file that contains the version info:\r\n(ex. the main .lua or .as fil" +
    "e)";
            // 
            // LocalTextBox
            // 
            this.LocalTextBox.Location = new System.Drawing.Point(109, 160);
            this.LocalTextBox.Name = "LocalTextBox";
            this.LocalTextBox.ReadOnly = true;
            this.LocalTextBox.Size = new System.Drawing.Size(239, 20);
            this.LocalTextBox.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label8.Location = new System.Drawing.Point(13, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 21);
            this.label8.TabIndex = 55;
            this.label8.Text = "1.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label9.Location = new System.Drawing.Point(13, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 21);
            this.label9.TabIndex = 56;
            this.label9.Text = "2.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label10.Location = new System.Drawing.Point(13, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 21);
            this.label10.TabIndex = 57;
            this.label10.Text = "3.";
            // 
            // NewLibraryForm
            // 
            this.AcceptButton = this.submitBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 365);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.LocalTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.browseLocalBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.libraryListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.updatesCheckBox);
            this.Controls.Add(this.libraryNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkToMainFileTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLibraryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Library";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox linkToMainFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox libraryNameTextBox;
        private System.Windows.Forms.CheckBox updatesCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox libraryListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button browseLocalBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox LocalTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}