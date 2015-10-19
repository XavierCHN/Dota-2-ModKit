using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit.Forms {
	public partial class LibraryManagerForm : MetroForm {
		private MainForm mainForm;
		Addon addon;

		public LibraryManagerForm(MainForm mainForm) {
			this.mainForm = mainForm;
			addon = mainForm.currAddon;

			InitializeComponent();
			this.Text = "Library Manager - " + addon.name;

			foreach (KeyValuePair<string, Addon.Library> kv in addon.libraries) {
				Addon.Library lib = kv.Value;
				object[] objs = new object[] {
					lib.local,
					lib.remote,
				};

				int rowNum = metroGrid1.Rows.Add(objs);
				lib.gridRow = rowNum;
				lib.grid = metroGrid1;

				lib.checkForUpdates();
			}

		}

		private void LibrariesForm_Load(object sender, EventArgs e) {

		}

		private void okBtn_Click(object sender, EventArgs e) {
			List<Addon.Library> libraries = new List<Addon.Library>();

			foreach (DataGridViewRow row in metroGrid1.Rows) {
				Addon.Library lib = new Addon.Library("", addon);
				for (int i = 0; i < row.Cells.Count; i++) {
					DataGridViewCell cell = row.Cells[i];
					if (cell == null || cell.Value == null) { continue; }
					string val = cell.Value.ToString();
					if (val == "") { continue; }

					if (i == 0) {
						// local path
						if (File.Exists(val)) {
							lib.local = val;
						}
					} else if (i == 1) {
						// remote
						if (val.StartsWith("http")) {
							lib.remote = val;
						}

					}

                }
				if (lib.local != "" && lib.remote != "") {
					libraries.Add(lib);
				}
			}

			//addon.libraries = libraries;
			Close();
		}

		private void addNewLibraryToolStripMenuItem_Click(object sender, EventArgs e) {

		}
	}
}
