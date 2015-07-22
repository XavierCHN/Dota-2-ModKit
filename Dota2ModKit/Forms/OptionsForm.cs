using Dota2ModKit.Properties;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit {
	public partial class OptionsForm : MetroForm {
		MainForm mainForm;
		Addon currAddon;

		public OptionsForm(MainForm mainForm) {
			this.mainForm = mainForm;
			currAddon = mainForm.currAddon;

			InitializeComponent();
			tabControl.SelectedIndex = 0;

			TabControl.TabPageCollection tpc = tabControl.TabPages;
			tpc[0].Text = currAddon.name;
			getAddonOptions();

		}

		private void getAddonOptions() {
			note0CheckBox.Checked = currAddon.generateNote0;
			loreCheckBox.Checked = currAddon.generateLore;
			openChangelogCheckBox.Checked = Settings.Default.OpenChangelog;
			askToBreakUpCheckBox.Checked = currAddon.askToBreakUp;

		}

		private void saveBtn_Click(object sender, EventArgs e) {
			metroRadioButton1.Select();

			currAddon.generateLore = loreCheckBox.Checked;
			currAddon.generateNote0 = note0CheckBox.Checked;
			currAddon.askToBreakUp = askToBreakUpCheckBox.Checked;

			Settings.Default.OpenChangelog = openChangelogCheckBox.Checked;

			// save stuff
			mainForm.serializeSettings();
			Settings.Default.Save();

			// close options form
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
