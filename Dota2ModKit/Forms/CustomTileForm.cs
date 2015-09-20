using Dota2ModKit.Properties;
using MetroFramework.Controls;
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

namespace Dota2ModKit.Forms {
	public partial class CustomTileForm : MetroForm {
		MainForm mainForm;
		CustomTile customTile;
		MetroTile tile;
		string path = "";
		int tileNum = -1;

        public CustomTileForm(MainForm mainForm, CustomTile customTile) {
			this.mainForm = mainForm;
			this.customTile = customTile;
			tileNum = customTile.tileNum;
			tile = customTile.tile;

			InitializeComponent();
			folderRadioButton.Checked = true;

		}

		private void okButton_Click(object sender, EventArgs e) {
			metroRadioButton1.Select();

			if (path == "" || customTileTitleTextBox.Text == "") {
				Close();
				return;
			}

			customTile.setPath(path);
			tile.Text = customTileTitleTextBox.Text;
			customTile.serialize();

			mainForm.text_notification("Tile created!", MetroFramework.MetroColorStyle.Green, 2000);
			Close();
		}

		private void browseBtn_Click(object sender, EventArgs e) {
			metroRadioButton1.Select();

			if (fileRadioButton.Checked) {
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() != DialogResult.OK) {
					return;
				}
				ofd.Multiselect = false;
				path = ofd.FileName;

			} else if (folderRadioButton.Checked) {
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				if (fbd.ShowDialog() != DialogResult.OK) {
					return;
				}
				path = fbd.SelectedPath;

			}
			pathTextBox1.Text = path;
		}
	}
}
