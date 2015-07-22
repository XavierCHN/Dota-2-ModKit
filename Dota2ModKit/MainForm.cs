using System;
using System.Collections.Generic;
using System.Linq;
using MetroFramework.Forms;
using Dota2ModKit.Properties;
using System.Diagnostics;
using System.Windows.Forms;
using MetroFramework;
using System.IO;
using MetroFramework.Controls;
using System.Drawing;
using KVLib;
using System.Media;
using System.Reflection;
using System.Text;
using Dota2ModKit.Features;
using VPKExtract;
using Dota2ModKit.Forms;

namespace Dota2ModKit {
	public partial class MainForm : MetroForm {
        public Addon currAddon;
		public Dictionary<string, Addon> addons;
		public string dotaDir = "";
		public string gamePath = "";
		public string contentPath = "";
		string firstAddonName = "";
		public System.Media.SoundPlayer player = new System.Media.SoundPlayer();
		public string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public Dictionary<string, List<string>> vsndToName = new Dictionary<string, List<string>>();
		KVFeatures kvFeatures;
		VTEXFeatures vtexFeatures;
		ParticleFeatures particleFeatures;
		SoundFeatures soundFeatures;
		string logPath = Path.Combine(Environment.CurrentDirectory, "debug_log.txt");

		// for updating modkit
		Updater updater;
		public string newVers = "";
		public string newVersUrl = "";

		// helpers to make things accessible in other forms.
		public MetroTile _addonTile;
		public MetroProgressSpinner _progressSpinner1;
		public MetroButton _spellLibBtn;

		public MainForm() {
			// refresh the debug_log
			refreshDebugLog();

			// bring up the UI
			InitializeComponent();

			// Check for settings updates.
			if (Settings.Default.UpdateRequired) {
				Settings.Default.Upgrade();
				Settings.Default.UpdateRequired = false;
				Settings.Default.Save();
				// open up changelog
				if (Settings.Default.OpenChangelog) {
					Process.Start("https://github.com/Myll/Dota-2-ModKit/releases");
				}
			}

			// setup hooks
			FormClosing += MainForm_FormClosing;
			tabControl.Selected += TabControl_Selected;
			githubTextBox.KeyDown += GithubTextBox_KeyDown;
			tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

			// check for updates
			updater = new Updater(this);
			updater.checkForUpdates();

			// allow public accessibility to these
			_addonTile = addonTile;
			_progressSpinner1 = progressSpinner1;
			_spellLibBtn = spellLibraryBtn;

			// init mainform controls stuff
			Size size = new Size(steamTile.Width, steamTile.Height);
			steamTile.TileImage = (Image)new Bitmap(Resources.steam_icon, size);
			luaRadioBtn.Checked = true;
			tabControl.SelectedIndex = 0;
			notificationLabel.Text = "";
			versionLabel.Text = "v" + version;

			retrieveDotaDir();

			// at this point assume valid dota dir.
			Debug.WriteLine("Directory: " + dotaDir);

            // save the dota dir
            Settings.Default.DotaDir = dotaDir;

			gamePath = Path.Combine(dotaDir, "game", "dota_addons");
            contentPath = Path.Combine(dotaDir, "content", "dota_addons");

			// create these dirs if they don't exist.
			if (!Directory.Exists(gamePath)) {
                Directory.CreateDirectory(gamePath);
            }
            if (!Directory.Exists(contentPath))
            {
                Directory.CreateDirectory(contentPath);
            }

			// get all the addons in the 'game' dir.
			addons = getAddons();

			// does this computer have any dota addons?
			if (addons.Count == 0) {
				MetroMessageBox.Show(this, "No Dota 2 addons detected. There must be one addon for D2ModKit to function properly. Exiting.",
					"No Dota 2 addons detected.",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				Environment.Exit(0);
			}

			// deserialize settings
			deserializeSettings();

			// auto-retrieve the workshop IDs for published addons if there are any.
			tryGetWorkshopIDs();

			// set currAddon to the addon that was last opened in last run of modkit.
			if (Settings.Default.LastAddon != "") {
				Addon a = getAddonFromName(Settings.Default.LastAddon);
				if (a != null) {
					changeCurrAddon(a);
				}
            }

			// basically, if this is first run of modkit, set the currAddon to w/e the default addon is in the workshop tools.
			if (currAddon == null) {
				changeCurrAddon(addons[getDefaultAddonName()]);
			}

			// init our features of Modkit
			kvFeatures = new KVFeatures(this);
			vtexFeatures = new VTEXFeatures(this);
			particleFeatures = new ParticleFeatures(this);
			soundFeatures = new SoundFeatures(this);
		}

		private void retrieveDotaDir() {
			// start process of retrieving dota dir
			dotaDir = Settings.Default.DotaDir;

			if (Settings.Default.DotaDir == "") {
				// this is first run of application

				// try to auto-get the dir
				dotaDir = Util.getDotaDir();

				// ensure valid dota dir retrieved.
				if (dotaDir == "") {
					MetroMessageBox.Show(this, "Could not auto-detect the Dota 2 directory. " +
						"Please browse to the Dota 2 folder which houses the 'game' and 'content' directories " +
						"(i.e. 'dota 2 beta').",
						"Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);

					FolderBrowserDialog fbd = new FolderBrowserDialog();
					DialogResult dr = fbd.ShowDialog();

					if (dr != DialogResult.OK) {
						MetroMessageBox.Show(this, "No folder selected. Exiting.",
							"Error",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error);

						Environment.Exit(0);
					}

					string p = fbd.SelectedPath;
					dotaDir = p;
				}
			}

			// ModKit must ran in the same drive as the dota dir.
			if (!Util.hasSameDrives(Environment.CurrentDirectory, dotaDir)) {
				MetroMessageBox.Show(this, "Dota 2 ModKit must be ran from the same drive as Dota 2 or else errors " +
					"will occur. Please move Dota 2 ModKit to the '" + dotaDir[0] + "' Drive and create a shortcut to it. Exiting.",
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);

				Environment.Exit(0);
			}

			// trying to read vpk practice. this works currently
			/*
			string s2vpkPath = Path.Combine(dotaDir, "game", "dota_imported", "pak01_dir.vpk");
			using (var vpk = new VpkFile(s2vpkPath)) {
				vpk.Open();
				Debug.WriteLine("Got VPK version {0}", vpk.Version);
				VpkNode node = vpk.GetFile("scripts/npc/npc_units.txt");
				using (var inputStream = VPKUtil.GetInputStream(s2vpkPath, node)) {
					var pathPieces = node.FilePath.Split('/');
					var directory = pathPieces.Take(pathPieces.Count() - 1);
					var fileName = pathPieces.Last();

					//EnsureDirectoryExists(Path.Combine(directory.ToArray()));

					using (var fsout = File.OpenWrite(Path.Combine(Environment.CurrentDirectory, "something.txt"))) {
						var buffer = new byte[1024];
						int amtToRead = (int)node.EntryLength;
						int read;

						while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0 && amtToRead > 0) {
							fsout.Write(buffer, 0, Math.Min(amtToRead, read));
							amtToRead -= read;
						}
					}
				}
			}*/
		}

		private string getDefaultAddonName() {
			string dota2cfgPath = Path.Combine(gamePath, "dota2cfg.cfg");
			string defaultAddonName = firstAddonName;

			try {
				if (File.Exists(dota2cfgPath)) {
					KeyValue kv = KVLib.KVParser.KV1.ParseAll(File.ReadAllText(dota2cfgPath))[0];
					if (kv.HasChildren) {
						foreach (KeyValue kv2 in kv.Children) {
							if (kv2.Key == "default") {
								if (addons.ContainsKey(kv2.GetString())) {
									defaultAddonName = kv2.GetString();
								}
							}
						}
					}
				}
			} catch (Exception ex) { }

			return defaultAddonName;
		}

		private void TabControl_SelectedIndexChanged(object sender, EventArgs e) {
			
		}

		private void GithubTextBox_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				doGithubSearch();
			}
		}

		private void TabControl_Selected(object sender, TabControlEventArgs e) {
			//PlaySound(Properties.Resources.browser_click_navigate);
		}

		public void PlaySound(Stream sound) {
			sound.Position = 0;     // Manually rewind stream 
			player.Stream = null;    // Then we have to set stream to null 
			player.Stream = sound;  // And set it again, to force it to be loaded again... 
			player.Play();          // Yes! We can play the sound! 
		}

		private void tryGetWorkshopIDs() {
			string vpksPath = Path.Combine(gamePath, "vpks");

			if (Directory.Exists(vpksPath)) {
				string[] dirs = Directory.GetDirectories(vpksPath);
				foreach (string dir in dirs) {
					string wIDs = dir.Substring(dir.LastIndexOf('\\') + 1);
					int wID;
					if (Int32.TryParse(wIDs, out wID)) {
						string publishDataPath = Path.Combine(dir, "publish_data.txt");
						string[] lines = File.ReadAllLines(publishDataPath);

						KeyValue publish_data = KVLib.KVParser.KV1.ParseAll(File.ReadAllText(publishDataPath))[0];

						foreach (KeyValue kv in publish_data.Children) {
							if (kv.Key == "source_folder") {
								string name = kv.GetString();
								Addon a = getAddonFromName(name);
								if (a != null) {
									a.workshopID = wID;
								}
							}
						}
					}
				}
			}
		}

		private void deserializeSettings() {
			string addonSettings = Settings.Default.AddonsKV;
            if (addonSettings == "") {
				// no addon settings to deserialize.
				return;
			}

			KeyValue rootKV = KVParser.KV1.ParseAll(addonSettings)[0];

			foreach (KeyValue kv in rootKV.Children) {
				string addonName = kv.Key;
				Addon addon = getAddonFromName(addonName);

				// this can occur if addon is deleted and program doesn't exit correctly.
				if (addon == null) {
					continue;
				}

				addon.deserializeSettings(kv);
			}
		}

		public void serializeSettings() {
			KeyValue rootKV = new KeyValue("Addons");
			foreach (KeyValuePair<string, Addon> a in addons) {
				string addonName = a.Key;
				Addon addon = a.Value;
				KeyValue addonKV = new KeyValue(addonName);
				addon.serializeSettings(addonKV);
				rootKV.AddChild(addonKV);
			}
			Settings.Default.AddonsKV = rootKV.ToString();
        }

		/// <summary>
		/// String -> Addon object conversion
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Addon getAddonFromName(string name) {
			Addon a;
			if (addons.TryGetValue(name, out a)) {
				return a;
			}

			return null;
		}

		public void changeCurrAddon(Addon a) {
			addonTile.Text = a.name;

			if (a.image != null) {
				addonTile.UseTileImage = true;
				addonTile.TileImage = a.image;
			} else {
				Image thumbnail = findAddonThumbnail(a);
				if (thumbnail != null) {
					addonTile.UseTileImage = true;
					addonTile.TileImage = thumbnail;
				} else {
					addonTile.UseTileImage = false;
					a.doesntHaveThumbnail = true;
					addonTile.Style = a.tileColor;
					//Debug.WriteLine(a.tileColor.GetType().ToString());
				}
			}

			currAddon = a;
			Settings.Default.LastAddon = a.name;
			//text_notification("Selected addon: " + a.name, MetroColorStyle.Green, 2500);
		}

		public Image findAddonThumbnail(Addon a) {
			string thumbnailDir = Path.Combine(dotaDir, "game", "bin", "win64");

			if (Directory.Exists(thumbnailDir) && a.workshopID != 0) {
				string imagePath = Path.Combine(thumbnailDir, a.workshopID + "_thumb.jpg");

				if (File.Exists(imagePath)) {
					Debug.WriteLine(imagePath + " found!");
					Image thumbnail = Image.FromFile(imagePath, true);
					Size size = new Size(addonTile.Width, addonTile.Height);
					thumbnail = (Image)new Bitmap(thumbnail, size);

					a.image = thumbnail;
					return thumbnail;
				}
			}
			return null;
		}

		private Dictionary<string, Addon> getAddons() {
			Dictionary<string, Addon> addons = new Dictionary<string, Addon>();
			string[] dirs = Directory.GetDirectories(gamePath);
			string addons_constructed = "Addons constructed:\n";
			foreach (string s in dirs) {
				// construct a new addon from this dir path.
				Addon a = new Addon(s);
				addons_constructed += s + "\n";
				// skip the dirs that we know aren't addons.
				if (a.name == "vpks") {
					continue;
				}

				// if constructor didn't return null, we have a valid addon.
				if (a != null) {
					addons.Add(a.name, a);
					if (firstAddonName == "") {
						firstAddonName = a.name;
					}
				}
			}

			log(addons_constructed);
			return addons;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			serializeSettings();
			Settings.Default.Save();
		}

        private void Form1_Load(object sender, EventArgs e) {

        }

		/// <summary>
		/// Opens up info about all addons on the computer. User is able to change addon here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addonTile_Click(object sender, EventArgs e) {
			AddonsForm addonsForm = new AddonsForm(this);
			// ShowDialog() will not allow the user to go back to parent dialog until this dialog is dealt with.
			// Show() still allows the user to go back to the parent dialog.
			addonsForm.ShowDialog();
		}

		private void generateAddonLangsBtn_Click(object sender, EventArgs e) {
			fixButton();

			bool success = true;

			try {
				success = currAddon.generateAddonLangs(this);
			} catch (Exception ex) {
				var ex2 = ex;
				ex2 = ex2 as KeyValueParsingException;

				if (ex2 == null) {
					ex2 = ex;
				}
				MetroMessageBox.Show(this, ex2.Message,
				ex2.ToString(),
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
				success = false;
			}

			if (success) {
				text_notification("Tooltips successfully generated", MetroColorStyle.Green, 2500);
			} else {
				text_notification("Tooltip generation failed", MetroColorStyle.Red, 2500);
			}
		}

		private void workshopPageBtn_Click(object sender, EventArgs e) {
			fixButton();

			if (currAddon.workshopID != 0) {
				Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=" + currAddon.workshopID);
				return;
			}

			SingleTextboxForm stf = new SingleTextboxForm();
			stf.Text = "Workshop ID";
			stf.textBox.Text = "";
			stf.btn.Text = "OK";
			stf.label.Text = "Enter the workshop ID (ex. 427193566):";

			DialogResult dr = stf.ShowDialog();
			if (dr == DialogResult.OK) {
				int id;
				if (Int32.TryParse(stf.textBox.Text, out id)) {
					currAddon.workshopID = id;
					Settings.Default.AddonNameToWorkshopID += currAddon.name + "=" + id + ";";
					// perform a refresh
					changeCurrAddon(currAddon);
				} else {
					MetroMessageBox.Show(this,
						"Couldn't parse ID!",
						"",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
			}
		}

		public void text_notification(string text, MetroColorStyle color, int duration) {
			System.Timers.Timer notificationLabelTimer = new System.Timers.Timer(duration);
			notificationLabelTimer.SynchronizingObject = this;
			notificationLabelTimer.AutoReset = false;
			notificationLabelTimer.Start();
			notificationLabelTimer.Elapsed += notificationLabelTimer_Elapsed;
			notificationLabel.Style = color;
			notificationLabel.Text = text;
		}

		private void notificationLabelTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			notificationLabel.Text = "";
		}

		private void combineKVBtn_Click(object sender, EventArgs e) {
			fixButton();
			//try {
				kvFeatures.combine();
			/*} catch (Exception ex) {
				MetroMessageBox.Show(this,
					ex.Message,
					ex.ToString(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}*/
		}

		/// <summary>
		/// there is a bug with Metro where pressing a button in dark theme will keep it grey.
		/// </summary>
		public void fixButton() {
			metroRadioButton1.Select();
		}

		private void particleDesignBtn_Click(object sender, EventArgs e) {
			fixButton();
			particleFeatures.design();
		}

		private void deleteAddonBtn_Click(object sender, EventArgs e) {
			fixButton();

			DialogResult dr = MetroMessageBox.Show(this,
				"Are you sure you want to delete the addon '" + currAddon.name + "'? " +
				"This will permanently delete the 'content' and 'game' directories of this addon.",
				"Warning",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning);

			if (dr == DialogResult.OK) {
				try {
					Directory.Delete(currAddon.gamePath, true);
					Directory.Delete(currAddon.contentPath, true);
				} catch (Exception ex) {
					MetroMessageBox.Show(this, "Please close all programs that are using files related to this addon, " +
					"including all related Windows Explorer processes, and try again.",
					"Could not fully delete addon",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
					/*MetroMessageBox.Show(this, ex.Message,
					ex.ToString(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);*/
					return;
				}

				string removed = currAddon.name;
				addons.Remove(currAddon.name);

				// reset currAddon
				foreach (KeyValuePair<string, Addon> a in addons) {
					// pick the first one and break
					changeCurrAddon(a.Value);
					break;
				}

				text_notification("The addon '" + removed + "' was successfully deleted.", MetroColorStyle.Green, 2500);
			}
		}

		private void onLink_Click(object sender, EventArgs e) {
			Debug.WriteLine(sender.ToString());
			string text = sender.ToString();
			if (text.EndsWith("Lua API")) {
				Process.Start("https://developer.valvesoftware.com/wiki/Dota_2_Workshop_Tools/Scripting/API");
			} else if (text.EndsWith("Panorama API")) {
				Process.Start("https://developer.valvesoftware.com/wiki/Dota_2_Workshop_Tools/Panorama/Javascript/API");
			} else if (text.EndsWith("r/Dota2Modding")) {
				Process.Start("https://www.reddit.com/r/dota2modding/");
			} else if (text.EndsWith("VPK")) {
				Process.Start("https://github.com/dotabuff/d2vpk/tree/master/dota_pak01");
			} else if (text.EndsWith("IRC")) {
				Process.Start("https://moddota.com/forums/chat");
			} else if (text.EndsWith("Lua Modifiers")) {
				Process.Start("https://developer.valvesoftware.com/wiki/Dota_2_Workshop_Tools/Scripting/Built-In_Modifier_Names");
			} else if (text.EndsWith("ModDota")) {
				Process.Start("https://moddota.com/forums");
			} else if (text.EndsWith("Ability Names")) {
				Process.Start("https://developer.valvesoftware.com/wiki/Dota_2_Workshop_Tools/Scripting/Built-In_Ability_Names");
			} else if (text.EndsWith("r/Dota2Modding")) {
				Process.Start("https://www.reddit.com/r/dota2modding/");
			} else if (text.EndsWith("SpellLibrary")) {
				Process.Start("https://github.com/Pizzalol/SpellLibrary");
			} else if (text.EndsWith("Workshop")) {
				Process.Start("http://steamcommunity.com/app/570/workshop/");
			} else if (text.EndsWith("GetDotaStats")) {
				Process.Start("http://getdotastats.com/#source2__beta_changes");
			} else if (text.EndsWith("dev.dota")) {
				Process.Start("http://dev.dota2.com/");
			}
		}

		private void goBtn_Click(object sender, EventArgs e) {
			fixButton();
			doGithubSearch();
		}

		private void doGithubSearch() {
			string query = githubTextBox.Text;

			if (query == "") {
				MetroMessageBox.Show(this, "",
				"No text inputted.",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
				return;
			}

			string lang = "lua";
			if (textRadioButton.Checked) {
				lang = "text";
			} else if (jsRadioButton.Checked) {
				lang = "js";
			}
			//string url = "https://github.com/search?l=" + lang + "&q=\"" + query + "\"+dota&ref=searchresults&type=Code&utf8=%E2%9C%93";
			string url = "https://github.com/search?l=" + lang + "&q=" + query + "&ref=searchresults&s=indexed&type=Code&utf8=%E2%9C%93";
			Process.Start(url);
		}

		private void shortcutTile_Click(object sender, EventArgs e) {
			try {
				string text = sender.ToString();
				Debug.WriteLine(text);
				if (text.EndsWith("G")) {
					Process.Start(currAddon.gamePath);
				} else if (text.EndsWith("C")) {
					Process.Start(currAddon.contentPath);
				} else if (text.EndsWith("VS")) {
					Process.Start(Path.Combine(currAddon.gamePath, "scripts", "vscripts"));
				} else if (text.EndsWith("N")) {
					Process.Start(Path.Combine(currAddon.gamePath, "scripts", "npc"));
				} else if (text.EndsWith("P")) {
					Process.Start(Path.Combine(currAddon.contentPath, "panorama"));
				} else if (text.EndsWith("R")) {
					Process.Start(Path.Combine(currAddon.gamePath, "resource"));
				} else if (text.EndsWith("S1V")) {
					Process.Start(Path.Combine(dotaDir, "dota", "pak01_dir.vpk"));
				} else if (text.EndsWith("S2V")) {
					Process.Start(Path.Combine(dotaDir, "game", "dota_imported", "pak01_dir.vpk"));
				} else if (text.EndsWith("E")) {
					//dota_english.txt
				}
			} catch (Exception ex) {
				// likely directoryNotFound exceptions.
				MetroMessageBox.Show(this, ex.Message,
				ex.ToString(),
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
			}
		}

		private void changePictureToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void compileVtexButton_Click(object sender, EventArgs e) {
			fixButton();
			try {
				vtexFeatures.compileVTEX();
			} catch (Exception ex) {
				MetroMessageBox.Show(this, ex.Message,
					ex.ToString(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

		}

		private void decompileVtexButton_Click(object sender, EventArgs e) {
			fixButton();
			try {
				vtexFeatures.decompileVTEX();
			} catch (Exception ex) {
				MetroMessageBox.Show(this, ex.Message,
					ex.ToString(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		private void reportBugBtn_Click(object sender, EventArgs e) {
			Process.Start("https://github.com/Myll/Dota-2-ModKit/issues");
		}

		private void optionsBtn_Click(object sender, EventArgs e) {
			onOptionsClick();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
			onOptionsClick();
		}

		void onOptionsClick() {
			OptionsForm of = new OptionsForm(this);
			DialogResult dr = of.ShowDialog();

			if (dr != DialogResult.OK) {
				return;
			}

			text_notification("Options saved", MetroColorStyle.Green, 2500);
		}

		private void findSoundNameBtn_Click(object sender, EventArgs e) {
			fixButton();

			try {
				soundFeatures.findSoundName();
			} catch (Exception ex) {
				MetroMessageBox.Show(this, ex.Message,
					ex.ToString(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		void log(string text) {
			try {
				if (!File.Exists(logPath)) {
					File.Create(logPath).Close();
				}

				string logText = File.ReadAllText(logPath);

				logText += text + "\n\n";
				File.WriteAllText(logPath, logText);
			} catch (Exception) { }
		}

		private void refreshDebugLog() {
			try {
				if (File.Exists(logPath)) {
					File.Delete(logPath);
				}
			} catch (Exception) { }
		}

		private void spellLibraryBtn_Click(object sender, EventArgs e) {
			fixButton();

			if (Application.OpenForms["SpellLibraryForm"] != null) {
				Application.OpenForms["SpellLibraryForm"].BringToFront();
                Application.OpenForms["SpellLibraryForm"].WindowState = FormWindowState.Normal;
				return;
            }

			//try {
				SpellLibraryForm slf = new SpellLibraryForm(this);
			/*} catch (Exception ex) {
				MetroMessageBox.Show(this, ex.Message,
					ex.ToString(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}*/
		}
	}
}
