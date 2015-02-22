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

		public PreferencesForm(Addon a)
        {
			addon = a;
            InitializeComponent();

			foreach (Addon.CombineKVFile cf in a.combineKVFiles) {
				int index = kvFileCheckbox.Items.Add(cf.name);
				//kvFileCheckbox.
				
			}

			note0LoreCheckBox.Checked = Settings.Default.GenNote0Lore;

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
        }

        private void note0LoreCheckBox_CheckedChanged(object sender, EventArgs e)
        {
			Settings.Default.GenNote0Lore = note0LoreCheckBox.Checked;
            addon.create_note0_lore = note0LoreCheckBox.Checked;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

		private void addKVButton_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.InitialDirectory = addon.GamePath;
			DialogResult dr = ofd.ShowDialog();

			if (dr != DialogResult.OK) {
				return;
			}

			string path = ofd.FileName;
			string fileName = path.Substring(path.LastIndexOf("\\") + 1);
			string fileNameExt = fileName;
			fileName = fileName.Remove(fileName.LastIndexOf("."));
			fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
			kvFileCheckbox.Items.Add(fileName);
		}
    }
}
