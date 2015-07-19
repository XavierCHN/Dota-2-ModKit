using MetroFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit {
	class Updater {
		Thread autoUpdateThread;
		MainForm mainForm;
		string version;

		public Updater(MainForm mainForm) {
			this.mainForm = mainForm;
			version = mainForm.version;
        }

		public void checkForUpdates() {
			Debug.WriteLine("CheckForUpdates");
			ThreadStart childref = new ThreadStart(CheckForUpdatesThread);
			autoUpdateThread = new Thread(childref);
			autoUpdateThread.Start();

		}

		private void CheckForUpdatesThread() {
			// use these to test version updater.
			//newVers = "1.3.2";
			//url = "https://github.com/stephenfournier/Dota-2-ModKit/releases/download/v1.3.2/D2ModKit.zip";

			// remember to keep the version naming consistent!
			//  you can go from 1.3.4.4 to 1.3.5.0, OR 1.3.4.0 to 1.3.5.0

			int count = 1;
			string url = "";
			string newVers = "";
			bool newVersFound = false;
			int j = 0;
			while (true) {
				newVers = Util.incrementVers(version, count + j);
				url = "https://github.com/stephenfournier/Dota-2-ModKit/releases/download/v";
				url += newVers + "/D2ModKit.zip";
				WebClient wc = new WebClient();

				try {
					byte[] responseBytes = wc.DownloadData("https://github.com/stephenfournier/Dota-2-ModKit/releases/tag/v" + newVers);
					string source = System.Text.Encoding.ASCII.GetString(responseBytes);
				} catch (Exception) {
					if (j < 10) {
						j++;
						continue;
					}
					break;
				}

				newVersFound = true;
				count += j + 1;
				j = 0;
			}

			if (!newVersFound) {
				Debug.WriteLine("No new vers available.");
				return;
			}

			newVers = Util.incrementVers(version, count - 1);
			url = "https://github.com/stephenfournier/Dota-2-ModKit/releases/download/v";
			url += newVers + "/D2ModKit.zip";

			mainForm.newVers = newVers;
			mainForm.newVersUrl = url;

			DialogResult dr = MetroMessageBox.Show(mainForm, "Version " + newVers + " of Dota 2 ModKit is now available. Would you like to update now?",
				"Update is available",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Information);

			if (dr == DialogResult.Yes) {
				Debug.WriteLine("Url: " + url);
				UpdateForm uf = new UpdateForm(mainForm);
				uf.ShowDialog();
			}
		}
	}
}
