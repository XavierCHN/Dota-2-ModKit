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
			openChangelogCheckBox.Checked = Settings.Default.OpenChangelog;

			note0CheckBox.Checked = currAddon.generateNote0;
			loreCheckBox.Checked = currAddon.generateLore;
			askToBreakUpCheckBox.Checked = currAddon.askToBreakUp;
			autoDeleteBinCheckBox.Checked = currAddon.autoDeleteBin;
			barebonesLibUpdatesCheckBox.Checked = currAddon.barebonesLibUpdates;

		}

		private void saveBtn_Click(object sender, EventArgs e) {
			metroRadioButton1.Select();

			// d2modkit options
			Settings.Default.OpenChangelog = openChangelogCheckBox.Checked;

			// addon options
			currAddon.generateLore = loreCheckBox.Checked;
			currAddon.generateNote0 = note0CheckBox.Checked;
			currAddon.askToBreakUp = askToBreakUpCheckBox.Checked;
			currAddon.autoDeleteBin = autoDeleteBinCheckBox.Checked;
			currAddon.barebonesLibUpdates = barebonesLibUpdatesCheckBox.Checked;

			// save stuff
			mainForm.serializeSettings();

			// close options form
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
