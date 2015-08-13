namespace Dota2ModKit {
	partial class OptionsForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
			this.loreCheckBox = new MetroFramework.Controls.MetroCheckBox();
			this.note0CheckBox = new MetroFramework.Controls.MetroCheckBox();
			this.tabControl = new MetroFramework.Controls.MetroTabControl();
			this.addonOptionsTab = new MetroFramework.Controls.MetroTabPage();
			this.barebonesLibUpdatesCheckBox = new MetroFramework.Controls.MetroCheckBox();
			this.autoDeleteBinCheckBox = new MetroFramework.Controls.MetroCheckBox();
			this.askToBreakUpCheckBox = new MetroFramework.Controls.MetroCheckBox();
			this.modkitOptionsTab = new MetroFramework.Controls.MetroTabPage();
			this.openChangelogCheckBox = new MetroFramework.Controls.MetroCheckBox();
			this.saveBtn = new MetroFramework.Controls.MetroButton();
			this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
			this.cancelButton = new MetroFramework.Controls.MetroButton();
			this.utf8CheckBox = new MetroFramework.Controls.MetroCheckBox();
			this.tabControl.SuspendLayout();
			this.addonOptionsTab.SuspendLayout();
			this.modkitOptionsTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// loreCheckBox
			// 
			this.loreCheckBox.AutoSize = true;
			this.loreCheckBox.Location = new System.Drawing.Point(0, 34);
			this.loreCheckBox.Name = "loreCheckBox";
			this.loreCheckBox.Size = new System.Drawing.Size(147, 15);
			this.loreCheckBox.TabIndex = 14;
			this.loreCheckBox.Text = "Generate \'Lore\' Tooltips";
			this.loreCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.loreCheckBox.UseSelectable = true;
			// 
			// note0CheckBox
			// 
			this.note0CheckBox.AutoSize = true;
			this.note0CheckBox.Location = new System.Drawing.Point(0, 13);
			this.note0CheckBox.Name = "note0CheckBox";
			this.note0CheckBox.Size = new System.Drawing.Size(156, 15);
			this.note0CheckBox.TabIndex = 13;
			this.note0CheckBox.Text = "Generate \'Note0\' Tooltips";
			this.note0CheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.note0CheckBox.UseSelectable = true;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.addonOptionsTab);
			this.tabControl.Controls.Add(this.modkitOptionsTab);
			this.tabControl.FontSize = MetroFramework.MetroTabControlSize.Tall;
			this.tabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
			this.tabControl.ItemSize = new System.Drawing.Size(10, 34);
			this.tabControl.Location = new System.Drawing.Point(23, 63);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(497, 341);
			this.tabControl.TabIndex = 15;
			this.tabControl.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.tabControl.UseSelectable = true;
			// 
			// addonOptionsTab
			// 
			this.addonOptionsTab.Controls.Add(this.utf8CheckBox);
			this.addonOptionsTab.Controls.Add(this.barebonesLibUpdatesCheckBox);
			this.addonOptionsTab.Controls.Add(this.autoDeleteBinCheckBox);
			this.addonOptionsTab.Controls.Add(this.askToBreakUpCheckBox);
			this.addonOptionsTab.Controls.Add(this.loreCheckBox);
			this.addonOptionsTab.Controls.Add(this.note0CheckBox);
			this.addonOptionsTab.HorizontalScrollbarBarColor = true;
			this.addonOptionsTab.HorizontalScrollbarHighlightOnWheel = false;
			this.addonOptionsTab.HorizontalScrollbarSize = 1;
			this.addonOptionsTab.Location = new System.Drawing.Point(4, 38);
			this.addonOptionsTab.Name = "addonOptionsTab";
			this.addonOptionsTab.Size = new System.Drawing.Size(489, 299);
			this.addonOptionsTab.TabIndex = 0;
			this.addonOptionsTab.Text = "addonname";
			this.addonOptionsTab.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.addonOptionsTab.VerticalScrollbarBarColor = true;
			this.addonOptionsTab.VerticalScrollbarHighlightOnWheel = false;
			this.addonOptionsTab.VerticalScrollbarSize = 2;
			// 
			// barebonesLibUpdatesCheckBox
			// 
			this.barebonesLibUpdatesCheckBox.AutoSize = true;
			this.barebonesLibUpdatesCheckBox.Location = new System.Drawing.Point(0, 97);
			this.barebonesLibUpdatesCheckBox.Name = "barebonesLibUpdatesCheckBox";
			this.barebonesLibUpdatesCheckBox.Size = new System.Drawing.Size(242, 15);
			this.barebonesLibUpdatesCheckBox.TabIndex = 18;
			this.barebonesLibUpdatesCheckBox.Text = "Auto-check for Barebones library updates";
			this.barebonesLibUpdatesCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.barebonesLibUpdatesCheckBox.UseSelectable = true;
			// 
			// autoDeleteBinCheckBox
			// 
			this.autoDeleteBinCheckBox.AutoSize = true;
			this.autoDeleteBinCheckBox.Location = new System.Drawing.Point(0, 76);
			this.autoDeleteBinCheckBox.Name = "autoDeleteBinCheckBox";
			this.autoDeleteBinCheckBox.Size = new System.Drawing.Size(348, 15);
			this.autoDeleteBinCheckBox.TabIndex = 17;
			this.autoDeleteBinCheckBox.Text = "Auto-delete the .bin files in the \'game\' directory of this addon";
			this.autoDeleteBinCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.autoDeleteBinCheckBox.UseSelectable = true;
			// 
			// askToBreakUpCheckBox
			// 
			this.askToBreakUpCheckBox.AutoSize = true;
			this.askToBreakUpCheckBox.Location = new System.Drawing.Point(0, 55);
			this.askToBreakUpCheckBox.Name = "askToBreakUpCheckBox";
			this.askToBreakUpCheckBox.Size = new System.Drawing.Size(282, 15);
			this.askToBreakUpCheckBox.TabIndex = 16;
			this.askToBreakUpCheckBox.Text = "Always ask to break up before combining KV files";
			this.askToBreakUpCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.askToBreakUpCheckBox.UseSelectable = true;
			// 
			// modkitOptionsTab
			// 
			this.modkitOptionsTab.Controls.Add(this.openChangelogCheckBox);
			this.modkitOptionsTab.HorizontalScrollbarBarColor = true;
			this.modkitOptionsTab.HorizontalScrollbarHighlightOnWheel = false;
			this.modkitOptionsTab.HorizontalScrollbarSize = 1;
			this.modkitOptionsTab.Location = new System.Drawing.Point(4, 38);
			this.modkitOptionsTab.Name = "modkitOptionsTab";
			this.modkitOptionsTab.Size = new System.Drawing.Size(489, 299);
			this.modkitOptionsTab.TabIndex = 2;
			this.modkitOptionsTab.Text = "D2ModKit";
			this.modkitOptionsTab.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.modkitOptionsTab.VerticalScrollbarBarColor = true;
			this.modkitOptionsTab.VerticalScrollbarHighlightOnWheel = false;
			this.modkitOptionsTab.VerticalScrollbarSize = 2;
			// 
			// openChangelogCheckBox
			// 
			this.openChangelogCheckBox.AutoSize = true;
			this.openChangelogCheckBox.Location = new System.Drawing.Point(0, 13);
			this.openChangelogCheckBox.Name = "openChangelogCheckBox";
			this.openChangelogCheckBox.Size = new System.Drawing.Size(229, 15);
			this.openChangelogCheckBox.TabIndex = 15;
			this.openChangelogCheckBox.Text = "Open changelog webpage after update";
			this.openChangelogCheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.openChangelogCheckBox.UseSelectable = true;
			// 
			// saveBtn
			// 
			this.saveBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.saveBtn.Location = new System.Drawing.Point(234, 414);
			this.saveBtn.Margin = new System.Windows.Forms.Padding(4);
			this.saveBtn.Name = "saveBtn";
			this.saveBtn.Size = new System.Drawing.Size(135, 35);
			this.saveBtn.TabIndex = 16;
			this.saveBtn.TabStop = false;
			this.saveBtn.Text = "Save";
			this.saveBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.saveBtn.UseSelectable = true;
			this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
			// 
			// metroRadioButton1
			// 
			this.metroRadioButton1.AutoSize = true;
			this.metroRadioButton1.Location = new System.Drawing.Point(10, 0);
			this.metroRadioButton1.Name = "metroRadioButton1";
			this.metroRadioButton1.Size = new System.Drawing.Size(127, 15);
			this.metroRadioButton1.TabIndex = 17;
			this.metroRadioButton1.Text = "metroRadioButton1";
			this.metroRadioButton1.UseSelectable = true;
			this.metroRadioButton1.Visible = false;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FontSize = MetroFramework.MetroButtonSize.Medium;
			this.cancelButton.Location = new System.Drawing.Point(152, 414);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(74, 35);
			this.cancelButton.TabIndex = 18;
			this.cancelButton.TabStop = false;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.cancelButton.UseSelectable = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// utf8CheckBox
			// 
			this.utf8CheckBox.AutoSize = true;
			this.utf8CheckBox.Location = new System.Drawing.Point(0, 118);
			this.utf8CheckBox.Name = "utf8CheckBox";
			this.utf8CheckBox.Size = new System.Drawing.Size(391, 15);
			this.utf8CheckBox.TabIndex = 19;
			this.utf8CheckBox.Text = "Auto-generate UTF-8 addon_language files during tooltips generation";
			this.utf8CheckBox.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.utf8CheckBox.UseSelectable = true;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(543, 473);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.metroRadioButton1);
			this.Controls.Add(this.saveBtn);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Style = MetroFramework.MetroColorStyle.Lime;
			this.Text = "Options";
			this.Theme = MetroFramework.MetroThemeStyle.Dark;
			this.tabControl.ResumeLayout(false);
			this.addonOptionsTab.ResumeLayout(false);
			this.addonOptionsTab.PerformLayout();
			this.modkitOptionsTab.ResumeLayout(false);
			this.modkitOptionsTab.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MetroFramework.Controls.MetroCheckBox loreCheckBox;
		private MetroFramework.Controls.MetroCheckBox note0CheckBox;
		private MetroFramework.Controls.MetroTabControl tabControl;
		private MetroFramework.Controls.MetroTabPage addonOptionsTab;
		private MetroFramework.Controls.MetroTabPage modkitOptionsTab;
		private MetroFramework.Controls.MetroButton saveBtn;
		private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
		private MetroFramework.Controls.MetroCheckBox openChangelogCheckBox;
		private MetroFramework.Controls.MetroCheckBox askToBreakUpCheckBox;
		private MetroFramework.Controls.MetroCheckBox autoDeleteBinCheckBox;
		private MetroFramework.Controls.MetroButton cancelButton;
		private MetroFramework.Controls.MetroCheckBox barebonesLibUpdatesCheckBox;
		private MetroFramework.Controls.MetroCheckBox utf8CheckBox;
	}
}