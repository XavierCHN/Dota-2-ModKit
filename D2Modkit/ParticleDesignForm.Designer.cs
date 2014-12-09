namespace D2ModKit
{
    partial class ParticleDesignForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParticleDesignForm));
            this.recolorButton = new System.Windows.Forms.Button();
            this.resizeButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.renameParticle = new System.Windows.Forms.Button();
            this.submitParticle = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.colorLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.baseNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // recolorButton
            // 
            this.recolorButton.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recolorButton.ForeColor = System.Drawing.Color.Red;
            this.recolorButton.Location = new System.Drawing.Point(12, 12);
            this.recolorButton.Name = "recolorButton";
            this.recolorButton.Size = new System.Drawing.Size(151, 45);
            this.recolorButton.TabIndex = 4;
            this.recolorButton.Text = "Re-Color Particle System";
            this.recolorButton.UseVisualStyleBackColor = true;
            this.recolorButton.Click += new System.EventHandler(this.recolorButton_Click);
            // 
            // resizeButton
            // 
            this.resizeButton.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resizeButton.ForeColor = System.Drawing.Color.Blue;
            this.resizeButton.Location = new System.Drawing.Point(12, 63);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(151, 45);
            this.resizeButton.TabIndex = 5;
            this.resizeButton.Text = "Re-Size Particle System";
            this.resizeButton.UseVisualStyleBackColor = true;
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // renameParticle
            // 
            this.renameParticle.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renameParticle.ForeColor = System.Drawing.Color.Purple;
            this.renameParticle.Location = new System.Drawing.Point(169, 12);
            this.renameParticle.Name = "renameParticle";
            this.renameParticle.Size = new System.Drawing.Size(151, 45);
            this.renameParticle.TabIndex = 6;
            this.renameParticle.Text = "Re-Name Particle System";
            this.renameParticle.UseVisualStyleBackColor = true;
            this.renameParticle.Click += new System.EventHandler(this.renameParticle_Click);
            // 
            // submitParticle
            // 
            this.submitParticle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitParticle.Location = new System.Drawing.Point(94, 138);
            this.submitParticle.Name = "submitParticle";
            this.submitParticle.Size = new System.Drawing.Size(144, 32);
            this.submitParticle.TabIndex = 7;
            this.submitParticle.Text = "Submit";
            this.submitParticle.UseVisualStyleBackColor = true;
            this.submitParticle.Click += new System.EventHandler(this.submitParticle_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorLabel,
            this.baseNameLabel,
            this.sizeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 173);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(332, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // colorLabel
            // 
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(39, 17);
            this.colorLabel.Text = "Color:";
            // 
            // baseNameLabel
            // 
            this.baseNameLabel.Name = "baseNameLabel";
            this.baseNameLabel.Size = new System.Drawing.Size(67, 17);
            this.baseNameLabel.Text = "Base name:";
            // 
            // sizeLabel
            // 
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(30, 17);
            this.sizeLabel.Text = "Size:";
            // 
            // ParticleDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 195);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.submitParticle);
            this.Controls.Add(this.renameParticle);
            this.Controls.Add(this.resizeButton);
            this.Controls.Add(this.recolorButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParticleDesignForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Particle Designer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button recolorButton;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button renameParticle;
        private System.Windows.Forms.Button submitParticle;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel colorLabel;
        private System.Windows.Forms.ToolStripStatusLabel baseNameLabel;
        private System.Windows.Forms.ToolStripStatusLabel sizeLabel;
    }
}