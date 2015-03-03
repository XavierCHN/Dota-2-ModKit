using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2ModKit {
	public partial class AddonPreferencesForm : Form {
		Addon addon;
		private bool needsRestart = false;

		public AddonPreferencesForm(Addon a) {
			addon = a;

			InitializeComponent();
			preferencesForLabel.Text = "Preferences for " + a.Name;
			this.Text = "Preferences for " + a.Name;
			note0LoreCheckBox.Checked = a.create_note0_lore;

			foreach (Addon.KVFileToCombine kvFileToCombine in a.KVFilesToCombine) {
				kvFileCheckedListbox.Items.Add(kvFileToCombine.name, kvFileToCombine.activated);
			}
			foreach (Addon.ModdingLibrary moddingLibrary in a.moddingLibraries) {
				listBox1.Items.Add(moddingLibrary.name);
			}

		}

		private void addKVButton_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Select KV file";
			ofd.InitialDirectory = addon.GamePath;
			DialogResult dr = ofd.ShowDialog();

			if (dr != DialogResult.OK) {
				return;
			}

			string path = ofd.FileName;
			string fileName = path.Substring(path.LastIndexOf("\\") + 1);
			string fileNameWithExt = fileName;
			fileName = fileName.Remove(fileName.LastIndexOf("."));
			fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
			kvFileCheckedListbox.Items.Add(fileName, true);
			Addon.KVFileToCombine ftc = new Addon.KVFileToCombine(fileName);
			ftc.path = path;
			ftc.activated = true;
			addon.KVFilesToCombine.Add(ftc);
		}

		private void addLibraryBtn_Click(object sender, EventArgs e) {
			NewLibraryForm nlf = new NewLibraryForm(addon);
			DialogResult dr = nlf.ShowDialog();
            if (dr != DialogResult.OK)
            {
                return;
            }
            if ((nlf.linkToLib == "" || nlf.libName == ""))
            {
                foreach (string item in nlf.chosenLibs)
                {
                    Addon.ModdingLibrary ml = new Addon.ModdingLibrary(item);
                    ml.webLink = nlf.linkToLib;
                    ml.localLink = nlf.localLink;
                    ml.webVers = ml.getWebVers();
                    ml.localVers = ml.getLocalVers();
                    ml.version = ""; // TODO
                }
            }

		}

		private void submitButton_Click(object sender, EventArgs e) {
			foreach (Addon.KVFileToCombine kvFileToCombine in addon.KVFilesToCombine) {
				bool found = false;
				foreach (var item in kvFileCheckedListbox.CheckedItems) {
					if (item.ToString() == kvFileToCombine.name) {
						found = true;
						kvFileToCombine.activated = true;
						break;
					}
				}
				if (!found) {
					kvFileToCombine.activated = false;
				}
			}

			addon.create_note0_lore = note0LoreCheckBox.Checked;

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
	}
}
