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

			string vsndPath = vsndTextBox.Text;
            if (vsndToName.ContainsKey(vsndPath)) {
				string text = "";

				List<string> vsndevtsPaths = new List<string>();

				foreach (string soundInfo in vsndToName[vsndPath]) {
					string[] soundInfoArr = soundInfo.Split('|');
					string soundName = soundInfoArr[0];
					string vsndevtsPath = soundInfoArr[1];

					vsndevtsPaths.Add(vsndevtsPath);

					text += vsndevtsPaths.Count + ". " + soundName + "\r\n";
				}
				text += "\r\nMatch the numbers above with the numbers below:\r\n\r\n";

				for (int i = 0; i < vsndevtsPaths.Count; i++) {
					int ptr = i + 1;
					text += ptr + ". " + vsndevtsPaths[i] + "\r\n";
				}
				soundNamesTextBox.Text = text;
			} else {
				soundNamesTextBox.Text = "No sound names found.";
				//Debug.WriteLine(vsndTextBox.Text + " key not found.");
			}

		}

		private void okBtn_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
			// clear memory. this still won't decrease the memory usage # in task manager :/
			vsndToName.Clear();

			this.Close();
		}
	}
}
