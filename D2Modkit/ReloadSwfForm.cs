using D2ModKit.Properties;
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

namespace D2ModKit {
	public partial class ReloadSwfForm : Form {
		private Addon currAddon;

		public ReloadSwfForm() {
			InitializeComponent();
		}

		public ReloadSwfForm(Addon currAddon) {

			if (Settings.Default.SwfFilesToIgnore == null)
			{
				Settings.Default.SwfFilesToIgnore = new System.Collections.Specialized.StringCollection();
			}

			InitializeComponent();

			this.currAddon = currAddon;

			string flash3 = Path.Combine(currAddon.GamePath, "resource", "flash3");
			string custom_ui = Path.Combine(flash3, "custom_ui.txt");
			if (!Directory.Exists(flash3) || !File.Exists(custom_ui)) {
				return;
			}
			string[] swfFilesArr = Directory.GetFiles(flash3, "*.swf");
			HashSet<string> swfFiles = new HashSet<string>();
			//HashSet<string> swfFiles = new HashSet<string>();
			foreach (string swf in swfFilesArr) {
				swfFiles.Add(swf);
			}

			foreach (string swfToIgnore in Settings.Default.SwfFilesToIgnore) {
				if (swfFiles.Contains(swfToIgnore)) {
					swfFiles.Remove(swfToIgnore);
				}
			}

			foreach (string swf in swfFiles) {
				string swfName = swf.Substring(swf.LastIndexOf('\\') + 1);
				swfName = swfName.Replace(".swf", "");
				listView1.Items.Add(swfName);
			}

			

		}
	}
}
