using D2ModKit.Properties;
using KVLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class PreferencesForm : Form
    {
		Addon addon;
		private bool needsRestart = false;

		public PreferencesForm(Addon a)
        {
			addon = a;
            InitializeComponent();

			checkForUpdatesCheckbox.Checked = Settings.Default.CheckForUpdates;

            ugcTextBox.Text = Settings.Default.UGCPath;
            this.AcceptButton = submitButton;
        }

        private void browseUGCButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult r = fbd.ShowDialog();
            if (r != DialogResult.OK)
            {
                return;
            }
            string path = fbd.SelectedPath;
            Settings.Default.UGCPath = path;
            this.ugcTextBox.Text = path;
			needsRestart = true;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
			if (needsRestart) {
				MessageBox.Show("Settings saved. D2ModKit needs to be restarted to procede. Quitting.",
					"D2ModKit",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				Environment.Exit(0);

			} else {
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
        }

		private void checkForUpdatesCheckbox_CheckedChanged(object sender, EventArgs e) {
			Settings.Default.CheckForUpdates = checkForUpdatesCheckbox.Checked;
        }
    }
}
