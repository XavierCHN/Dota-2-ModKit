namespace Dota2ModKit {
	partial class ParticleDesignForm {
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
			this.metroTrackBar1 = new MetroFramework.Controls.MetroTrackBar();
			this.bulkResizeLabel = new MetroFramework.Controls.MetroLabel();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
			this.bulkRecolorBtn = new MetroFramework.Controls.MetroButton();
			this.submitBtn = new MetroFramework.Controls.MetroButton();
			this.particlesSelectedLabel = new MetroFramework.Controls.MetroLabel();
			this.sizeLabel = new MetroFramework.Controls.MetroLabel();
			this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
			this.rLabel = new MetroFramework.Controls.MetroLabel();
			this.gLabel = new MetroFramework.Controls.MetroLabel();
			this.bLabel = new MetroFramework.Controls.MetroLabel();
			this.SuspendLayout();
			// 
			// metroTrackBar1
			// 
			this.metroTrackBar1.BackColor = System.Drawing.Color.Transparent;
			this.metroTrackBar1.Location = new System.Drawing.Point(80, 156);
			this.metroTrackBar1.Name = "metroTrackBar1";
			this.metroTrackBar1.Size = new System.Drawing.Size(243, 19);
			this.metroTrackBar1.TabIndex = 0;
			this.metroTrackBar1.Text = "metroTrackBar1";
			this.metroTrackBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// bulkResizeLabel
			// 
			this.bulkResizeLabel.AutoSize = true;
			this.bulkResizeLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.bulkResizeLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.bulkResizeLabel.Location = new System.Drawing.Point(20, 112);
			this.bulkResizeLabel.Name = "bulkResizeLabel";
			this.bulkResizeLabel.Size = new System.Drawing.Size(102, 25);
			this.bulkResizeLabel.Style = MetroFramework.MetroColorStyle.Yellow;
			this.bulkResizeLabel.TabIndex = 1;
			this.bulkResizeLabel.Text = "Bulk Resize:";
			this.bulkResizeLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.bulkResizeLabel.UseStyleColors = true;
			// 
			// metroLabel2
			// 
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.metroLabel2.Location = new System.Drawing.Point(24, 156);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(50, 19);
			this.metroLabel2.TabIndex = 2;
			this.metroLabel2.Text = "-100%";
			this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// metroLabel3
			// 
			this.metroLabel3.AutoSize = true;
			this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.metroLabel3.Location = new System.Drawing.Point(329, 156);
			this.metroLabel3.Name = "metroLabel3";
			this.metroLabel3.Size = new System.Drawing.Size(54, 19);
			this.metroLabel3.TabIndex = 3;
			this.metroLabel3.Text = "+200%";
			this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// bulkRecolorBtn
			// 
			this.bulkRecolorBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.bulkRecolorBtn.Location = new System.Drawing.Point(24, 196);
			this.bulkRecolorBtn.Margin = new System.Windows.Forms.Padding(4);
			this.bulkRecolorBtn.Name = "bulkRecolorBtn";
			this.bulkRecolorBtn.Size = new System.Drawing.Size(197, 41);
			this.bulkRecolorBtn.TabIndex = 5;
			this.bulkRecolorBtn.TabStop = false;
			this.bulkRecolorBtn.Text = "Bulk Re-Color";
			this.bulkRecolorBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.bulkRecolorBtn.UseSelectable = true;
			this.bulkRecolorBtn.Click += new System.EventHandler(this.recolorBtn_Click);
			// 
			// submitBtn
			// 
			this.submitBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.submitBtn.Location = new System.Drawing.Point(117, 295);
			this.submitBtn.Margin = new System.Windows.Forms.Padding(4);
			this.submitBtn.Name = "submitBtn";
			this.submitBtn.Size = new System.Drawing.Size(169, 41);
			this.submitBtn.TabIndex = 6;
			this.submitBtn.TabStop = false;
			this.submitBtn.Text = "Submit";
			this.submitBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.submitBtn.UseSelectable = true;
			this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
			// 
			// particlesSelectedLabel
			// 
			this.particlesSelectedLabel.AutoSize = true;
			this.particlesSelectedLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.particlesSelectedLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.particlesSelectedLabel.Location = new System.Drawing.Point(20, 68);
			this.particlesSelectedLabel.Name = "particlesSelectedLabel";
			this.particlesSelectedLabel.Size = new System.Drawing.Size(161, 25);
			this.particlesSelectedLabel.Style = MetroFramework.MetroColorStyle.Blue;
			this.particlesSelectedLabel.TabIndex = 7;
			this.particlesSelectedLabel.Text = "0 particles selected";
			this.particlesSelectedLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// sizeLabel
			// 
			this.sizeLabel.AutoSize = true;
			this.sizeLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.sizeLabel.Location = new System.Drawing.Point(139, 134);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(116, 19);
			this.sizeLabel.TabIndex = 8;
			this.sizeLabel.Text = "Size change: +0%";
			this.sizeLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			// 
			// metroRadioButton1
			// 
			this.metroRadioButton1.AutoSize = true;
			this.metroRadioButton1.Location = new System.Drawing.Point(10, 5);
			this.metroRadioButton1.Name = "metroRadioButton1";
			this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
			this.metroRadioButton1.TabIndex = 9;
			this.metroRadioButton1.Text = "metroRadioButton1";
			this.metroRadioButton1.UseSelectable = true;
			this.metroRadioButton1.Visible = false;
			// 
			// rLabel
			// 
			this.rLabel.AutoSize = true;
			this.rLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.rLabel.Location = new System.Drawing.Point(228, 188);
			this.rLabel.Name = "rLabel";
			this.rLabel.Size = new System.Drawing.Size(20, 19);
			this.rLabel.Style = MetroFramework.MetroColorStyle.Red;
			this.rLabel.TabIndex = 10;
			this.rLabel.Text = "R:";
			this.rLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.rLabel.UseStyleColors = true;
			// 
			// gLabel
			// 
			this.gLabel.AutoSize = true;
			this.gLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.gLabel.Location = new System.Drawing.Point(228, 208);
			this.gLabel.Name = "gLabel";
			this.gLabel.Size = new System.Drawing.Size(22, 19);
			this.gLabel.Style = MetroFramework.MetroColorStyle.Green;
			this.gLabel.TabIndex = 11;
			this.gLabel.Text = "G:";
			this.gLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.gLabel.UseStyleColors = true;
			// 
			// bLabel
			// 
			this.bLabel.AutoSize = true;
			this.bLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.bLabel.Location = new System.Drawing.Point(228, 228);
			this.bLabel.Name = "bLabel";
			this.bLabel.Size = new System.Drawing.Size(20, 19);
			this.bLabel.Style = MetroFramework.MetroColorStyle.Blue;
			this.bLabel.TabIndex = 12;
			this.bLabel.Text = "B:";
			this.bLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.bLabel.UseStyleColors = true;
			// 
			// ParticleDesignForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(396, 360);
			this.Controls.Add(this.bLabel);
			this.Controls.Add(this.gLabel);
			this.Controls.Add(this.rLabel);
			this.Controls.Add(this.metroRadioButton1);
			this.Controls.Add(this.sizeLabel);
			this.Controls.Add(this.particlesSelectedLabel);
			this.Controls.Add(this.submitBtn);
			this.Controls.Add(this.bulkRecolorBtn);
			this.Controls.Add(this.metroLabel3);
			this.Controls.Add(this.metroLabel2);
			this.Controls.Add(this.bulkResizeLabel);
			this.Controls.Add(this.metroTrackBar1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ParticleDesignForm";
			this.Resizable = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Style = MetroFramework.MetroColorStyle.Yellow;
			this.Text = "Particle Designer";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroTrackBar metroTrackBar1;
		private MetroFramework.Controls.MetroLabel bulkResizeLabel;
		private MetroFramework.Controls.MetroLabel metroLabel2;
		private MetroFramework.Controls.MetroLabel metroLabel3;
		private MetroFramework.Controls.MetroButton bulkRecolorBtn;
		private MetroFramework.Controls.MetroButton submitBtn;
		private MetroFramework.Controls.MetroLabel particlesSelectedLabel;
		private MetroFramework.Controls.MetroLabel sizeLabel;
		private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
		private MetroFramework.Controls.MetroLabel rLabel;
		private MetroFramework.Controls.MetroLabel gLabel;
		private MetroFramework.Controls.MetroLabel bLabel;
	}
}