using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit.Forms {
	public partial class FindSoundForm : MetroForm {
		private MainForm mainForm;
		private Dictionary<string, List<string>> vsndToName;

		public FindSoundForm(MainForm mainForm) {
			this.mainForm = mainForm;
			vsndToName = mainForm.vsndToName;

			InitializeComponent();
			//soundNamesTextBox.ScrollBars = metroScrollBar1;

			soundNamesTextBox.Clear();
			vsndTextBox.Clear();
			soundNamesTextBox.Text = "No sound names found.";

			vsndTextBox.TextChanged += VsndTextBox_TextChanged;

		}

		private void VsndTextBox_TextChanged(object sender, EventArgs e) {
			soundNamesTextBox.Clear();

			if (vsndToName.ContainsKey(vsndTextBox.Text)) {
				string text = "";
				foreach (string soundName in vsndToName[vsndTextBox.Text]) {
					text += soundName + "\r\n";
				}
				soundNamesTextBox.Text = text;
			} else {
				soundNamesTextBox.Text = "No sound names found.";
				//Debug.WriteLine(vsndTextBox.Text + " key not found.");
			}

		}

		private void okBtn_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
