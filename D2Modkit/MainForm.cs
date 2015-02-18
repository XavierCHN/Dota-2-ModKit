using D2ModKit.Properties;
using KVLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class MainForm : Form
    {
        private List<string> addonNames;
        private List<string> gameAddonPaths;
        private List<string> contentAddonPaths;

        private List<Addon> addons = new List<Addon>();
        private Addon currAddon;

        private string ugcPath = "";

        private bool hasSettings;

        private bool HasSettings
        {
            get { return hasSettings; }
            set { hasSettings = value; }
        }

        public List<string> ContentAddonPaths
        {
            get
            {
                List<string> paths = new List<string>();
                foreach (Addon a in addons)
                {
                    paths.Add(a.ContentPath);
                }
                return paths;
            }
            set { contentAddonPaths = value; }
        }

        public List<string> GameAddonPaths
        {
            get
            {
                List<string> paths = new List<string>();
                foreach (Addon a in addons)
                {
                    paths.Add(a.GamePath);
                }
                return paths;
            }
            set { gameAddonPaths = value; }
        }

        public List<string> AddonNames
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Addon a in addons)
                {
                    names.Add(a.Name);
                }
                return names;
            }
            set { addonNames = value; }
        }

        public string UGCPath
        {
            get { return ugcPath; }
            set { ugcPath = value; }
        }

        public Addon CurrentAddon
        {
            get { return currAddon; }
            set { currAddon = value; }
        }

        public List<Addon> Addons
        {
            get { return addons; }
            set { addons = value; }
        }

        public System.Collections.Specialized.StringCollection AddonInfos
        {
            get { return Settings.Default.AddonInfos; }
        }

        private ParticleSystem currParticleSystem;

        public ParticleSystem ps
        {
            get { return currParticleSystem; }
            set { currParticleSystem = value; }
        }

        private Thread autoUpdateThread;
		private Thread gdsThread;
		private Dictionary<string, string> modRanks = new Dictionary<string, string>();

        private string Vers = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		private Dictionary<string, string> libraries = new Dictionary<string, string>();

        public MainForm()
        {
            // Check if application is already running.
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) System.Diagnostics.Process.GetCurrentProcess().Kill();

            // Check for settings updates.
            if (Settings.Default.UpdateRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpdateRequired = false;
                Settings.Default.Save();
                // open up changelog
                Process.Start("https://github.com/Myll/Dota-2-ModKit/releases");
            }
			
			// populate libraries dictionary
			addLibraries();

            if (AddonInfos == null)
            {
                Settings.Default.AddonInfos = new System.Collections.Specialized.StringCollection();
            }

            InitializeComponent();

            // check stuff in the checkbox
            var items = kvFileCheckbox.Items;
            for (int i = 0; i < items.Count; i++)
            {
                kvFileCheckbox.SetItemChecked(i, true);
            }

            combineSuccessLabel.Text = "";

            // check for updates in a new thread.
            ThreadStart childref = new ThreadStart(CheckForUpdatesThread);
            autoUpdateThread = new Thread(childref);
            autoUpdateThread.Start();

            // hook for when user selects a different addon.
            addonDropDown.SelectedIndexChanged += addonDropDown_SelectedIndexChanged;

            this.FormClosed += MainForm_FormClosed;

            this.Text = "Dota 2 ModKit - " + "v" + Vers;
            if (Properties.Settings.Default.UGCPath != "")
            {
                UGCPath = Properties.Settings.Default.UGCPath;
                if (Directory.Exists(UGCPath))
                {
                    HasSettings = true;
                }
            }

            if (HasSettings)
            {
                // and use that to find the game and content dirs.
                ensureSameDrives();
                getAddons();
            }
            else
            {
                getUGCPath();
                ensureSameDrives();
            }

			// get the gds ranks for the mods
			ThreadStart childref2 = new ThreadStart(GetGDSRanks);
			gdsThread = new Thread(childref2);
			gdsThread.Start();

            selectCurrentAddon(Properties.Settings.Default.CurrAddon);
        }

		private void addLibraries() {
			libraries.Add("buildinghelper.lua", "https://raw.githubusercontent.com/Myll/Dota-2-Building-Helper/master/game/dota_addons/samplerts/scripts/vscripts/buildinghelper.lua");
			libraries.Add("physics.lua", "https://raw.githubusercontent.com/bmddota/barebones/source2/game/dota_addons/barebones/scripts/vscripts/physics.lua");
			//libraries.Add("physics.lua", "https://raw.githubusercontent.com/bmddota/barebones/source2/game/dota_addons/barebones/scripts/vscripts/physics.lua");
		}

		private void GetGDSRanks() {
			WebClient wc = new WebClient();
			try {
				Byte[] responseBytes = wc.DownloadData("http://getdotastats.com/d2mods/api/popular_mods.php");
				string source = System.Text.Encoding.ASCII.GetString(responseBytes);
				foreach (Addon a in addons) {
					string modID = getVal(a.Name, "gds_modID");
					a.GDS_modID = modID;
					if (modID == "") {
						continue;
					}
					string x = source.Substring(source.IndexOf("\"modID\":" + modID));
					// accomdate for at most 5 digits
					string x2 = x.Substring(x.IndexOf("\"popularityRank\":"), 17 + 5);
					string rank = x2.Substring(17, x2.IndexOf(',') - 17);
					a.GDS_rank = rank;
				}

			} catch (Exception) { }
			if (this.InvokeRequired) {
				m_SetGDSButtonText setGDSButtonText = SetGDSButtonText;
				Invoke(setGDSButtonText);
			}
		}

		private void SetGDSButtonText() {
			if (currAddon.GDS_rank != "") {
				gdsButton.Text = "#" + currAddon.GDS_rank;
			}
		}

		delegate void m_SetGDSButtonText();

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ensureSameDrives()
        {
            // D2ModKit must be ran from the same drive as dota or else things will break.
            char modkitDrive = Environment.CurrentDirectory[0];
            char ugcDrive = UGCPath[0];
            if (modkitDrive != ugcDrive)
            {
                DialogResult res = MessageBox.Show("D2ModKit must be ran from the same drive as Dota 2 or else errors will occur. " +
                    " Please move D2ModKit to Drive " + ugcDrive + " and create a shortcut to it.",
                    "D2ModKit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
                Environment.Exit(0);
            }
        }

        private void CheckForUpdatesThread()
        {
            // use these to test version updater.
            //newVers = "1.3.2";
            //url = "https://github.com/Myll/Dota-2-ModKit/releases/download/v1.3.2/D2ModKit.zip";

            // remember to keep the version naming consistent!
            //  you can go from 1.3.4.4 to 1.3.5.0, OR 1.3.4.0 to 1.3.5.0

            int count = 1;
            string url = "";
            string newVers = "";
            bool newVersFound = false;
            bool checkOtherFormat = false;
            bool checkOtherFormat2 = false;
            int j = 0;
            while (true)
            {
                newVers = convertVers(Vers, count + j);
                url = "https://github.com/Myll/Dota-2-ModKit/releases/download/v";
                url += newVers + "/D2ModKit.zip";

                WebClient wc = new WebClient();
                try
                {
                    Byte[] responseBytes = null;
                    string source = null;

                    if (checkOtherFormat2)
                    {
                        string otherFormatVers = newVers.Substring(0, 5);
                        checkOtherFormat2 = false;
                        responseBytes = wc.DownloadData("https://github.com/Myll/Dota-2-ModKit/releases/tag/v" + otherFormatVers);
                        source = System.Text.Encoding.ASCII.GetString(responseBytes);
                    }
                    else
                    {
                        if (newVers.EndsWith("0"))
                        {
                            checkOtherFormat = true;
                        }
                        else
                        {
                            checkOtherFormat = false;
                        }
                        responseBytes = wc.DownloadData("https://github.com/Myll/Dota-2-ModKit/releases/tag/v" + newVers);
                        source = System.Text.Encoding.ASCII.GetString(responseBytes);
                    }
                }
                catch (Exception)
                {
                    if (checkOtherFormat)
                    {
                        checkOtherFormat2 = true;
                        checkOtherFormat = false;
                        continue;
                    }
                    if (j < 10)
                    {
                        j++;
                        //count++;
                        continue;
                    }
                    break;
                }
                newVersFound = true;
                count += j + 1;
                j = 0;
            }
            if (!newVersFound)
            {
                Debug.WriteLine("No new vers available.");
                return;
            }
            newVers = convertVers(Vers, count - 1);
            url = "https://github.com/Myll/Dota-2-ModKit/releases/download/v";
            url += newVers + "/D2ModKit.zip";

            if (newVers.EndsWith("0"))
            {
                Byte[] responseBytes = null;
                string source = null;
                string otherFormatVers = newVers.Substring(0, 5);
                WebClient wc = new WebClient();
                try
                {
                    responseBytes = wc.DownloadData("https://github.com/Myll/Dota-2-ModKit/releases/tag/v" + newVers);
                    source = System.Text.Encoding.ASCII.GetString(responseBytes);
                }
                catch (Exception)
                {
                    url = "https://github.com/Myll/Dota-2-ModKit/releases/download/v";
                    url += otherFormatVers + "/D2ModKit.zip";
                }
            }

            DialogResult r = MessageBox.Show("Version " + newVers + " of D2ModKit is now available. Would you like to update now?",
                "D2ModKit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (r == DialogResult.Yes)
            {
                //Debug.WriteLine("Url: " + url);
                UpdateForm uf = new UpdateForm(url, newVers);
                uf.ShowDialog();
            }
        }

        string convertVers(string vers, int add)
        {
            //Debug.WriteLine("input: " + vers);
            // check for new Vers
            string[] numStrings = vers.Split('.');
            int thousands = Int32.Parse(numStrings[0]) * 1000;
            int hundreds = Int32.Parse(numStrings[1]) * 100;
            int tens = Int32.Parse(numStrings[2]) * 10;
            int ones = Int32.Parse(numStrings[3]);
            int num = thousands + hundreds + tens + ones + add;

            //Debug.WriteLine("new num: " + num);
            int newThousands = num / 1000;
            int newHundreds = (num - newThousands * 1000) / 100;
            int newTens = (num - newThousands * 1000 - newHundreds * 100) / 10;
            int newOnes = num - newThousands * 1000 - newHundreds * 100 - newTens * 10;
            string newVers = newThousands + "." + newHundreds + "." + newTens + "." + newOnes;
            //Debug.WriteLine("New vers: " + newVers);
            return newVers;
        }

        void addonDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = addonDropDown.GetItemText(addonDropDown.SelectedItem);
            if (AddonNames.Contains(text))
            {
                selectCurrentAddon(text);
            }
        }

        private void getUGCPath()
        {
            while (!HasSettings)
            {
                // Auto-find the dota_ugc path.
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine;
                try
                {
                    regKey =
                        regKey.OpenSubKey(
                            @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 570");
                    if (regKey != null)
                    {
                        string dir = regKey.GetValue("InstallLocation").ToString();
                        UGCPath = Path.Combine(dir, "dota_ugc");
                        Debug.WriteLine("Directory: " + dir);
                        HasSettings = true;
                    }
                }
                catch (Exception) { }

                if (!HasSettings)
                {
                    MessageBox.Show("Please select the path to your dota_ugc folder.", "D2ModKit", MessageBoxButtons.OK);
                    FolderBrowserDialog dialog = new FolderBrowserDialog();

                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        Debug.WriteLine("DialogResult OK");
                    }

                    ugcPath = dialog.SelectedPath;
                    // check if this is valid.
                    string ugc = ugcPath.Substring(ugcPath.LastIndexOf('\\') + 1);
                    if (ugc != "dota_ugc")
                    {
                        DialogResult res = MessageBox.Show("That is not a path to your dota_ugc folder.", "Error",
                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                        if (res == DialogResult.Retry)
                        {
                            continue;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        HasSettings = true;
                    }
                }

                Properties.Settings.Default.UGCPath = UGCPath;
                Properties.Settings.Default.Save();

                // get the game and content dirs from the ugc path.
                getAddons();
            }
        }

        private string[] getRGB()
        {
            string[] rgb = new string[3];
            ColorDialog color = new ColorDialog();
            color.AnyColor = true;
            color.AllowFullOpen = true;
            DialogResult re = color.ShowDialog();
            if (re == DialogResult.OK)
            {
                Color picked = color.Color;
                rgb[0] = picked.R.ToString();
                rgb[1] = picked.G.ToString();
                rgb[2] = picked.B.ToString();
            }
            else
            {
                return null;
            }
            return rgb;
        }

        private void newParticles_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UGCPath == "")
            {
                MessageBox.Show("You need to select your dota_ugc path before you can use this.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "decompiled_particles")))
            {
                DialogResult result = MessageBox.Show(
                    "No decompiled_particles folder detected in the D2ModKit folder. Would you like to download the decompiled particles now?",
                    "D2ModKit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Hand);

                if (result == DialogResult.Yes)
                {
                    Process.Start("http://moddota.com/resources/decompiled_particles.zip");
                }
                return;
            }

            OpenFileDialog fileDialog = new OpenFileDialog();
            Debug.WriteLine("Current directory: " + Environment.CurrentDirectory);
            fileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "decompiled_particles");
            fileDialog.Multiselect = true;
            fileDialog.Title = "Select Particles To Copy";
            DialogResult res = fileDialog.ShowDialog();
            // check if we actually have filenames, or the user closed the box.
            if (res != DialogResult.OK)
            {
                return;
            }
            string[] particlePaths = fileDialog.FileNames;
            FolderBrowserDialog browser = new FolderBrowserDialog();
            // RootFolder needs to be defined for auto-scrolling to work apparently.
            browser.RootFolder = getRootFolder();
            // let the user see the particles directory first.
            string initialPath = Path.Combine(currAddon.ContentPath, "particles");
            browser.SelectedPath = initialPath;
            browser.ShowNewFolderButton = true;
            browser.Description =
                "Browse to where the particles will be copied to. They must be placed in the particles directory.";
            DialogResult browserResult = browser.ShowDialog();

            if (browserResult == DialogResult.Cancel)
            {
                return;
            }

            string newFolder = browser.SelectedPath;

            string folderName = newFolder.Substring(newFolder.LastIndexOf('\\') + 1);
            List<Particle> particles = new List<Particle>();
            foreach (string path in particlePaths)
            {
                bool overwriteAllowed = true;
                string particleName = path.Substring(path.LastIndexOf('\\') + 1);
                string targetPath = Path.Combine(newFolder, particleName);

                try
                {
                    File.Copy(path, targetPath);
                }
                catch (IOException) { }

                if (overwriteAllowed)
                {
                    particles.Add(new Particle(targetPath));
                }
            }

            ParticleSystem ps = new ParticleSystem(particles);

            ParticleDesignForm PDF = new ParticleDesignForm(ps);
            DialogResult r = PDF.ShowDialog();
            if (!PDF.SubmitClicked)
            {
                // User doesn't want to fork particles, so undo what we already did.
                // Delete the particles that were just copied.
                for (int i = 0; i < particles.Count(); i++)
                {
                    File.Delete(particles.ElementAt(i).Path);
                }

                return;
            }

            // if particle wasn't renamed, fix the child references.
            if (!PDF.Renamed)
            {
                ps.fixChildRefs(newFolder);
            }

            for (int i = 0; i < ps.Particles.Count(); i++)
            {
                Particle p = ps.Particles.ElementAt(i);
                File.WriteAllText(p.Path, p.ToString());
            }
        }

        private void getAddons()
        {
            string[] dirs = Directory.GetDirectories(UGCPath);
            foreach (string str in dirs)
            {
                if (str.Contains("game"))
                {
                    string dota_addons = Path.Combine(str, "dota_addons");
                    if (Directory.Exists(dota_addons))
                    {
                        string[] dirs2 = Directory.GetDirectories(dota_addons);
                        foreach (string str2 in dirs2)
                        {
                            // ensure both the game + content dirs exist for this mod.
                            Addon a = new Addon(str2);
                            a.ContentPath = Path.Combine(UGCPath, "content", "dota_addons", a.Name);
                            if (isValidAddon(a))
                            {
                                addons.Add(a);
                            }
                        }
                    }
                }
            }
			setAddonInfos();
            setAddonNames();
        }

        private void setAddonNames()
        {
            addonDropDown.Items.Clear();
            foreach (string name in AddonNames)
            {
                addonDropDown.Items.Add(name);
            }
        }

        private Addon getAddonFromName(string name)
        {
            foreach (Addon a in addons)
            {
                if (isValidAddon(a) && a.Name == name)
                {
                    return a;
                }
            }
            foreach (Addon a in addons)
            {
                if (isValidAddon(a))
                {
                    return a;
                }
            }
            return null;
        }

        private bool isValidAddon(Addon a)
        {
            if (Directory.Exists(a.ContentPath) && Directory.Exists(a.GamePath))
            {
                return true;
            }
            return false;
        }

        private void selectCurrentAddon(string addon)
        {
            currAddon = getAddonFromName(addon);
            Properties.Settings.Default.CurrAddon = currAddon.Name;
			SaveSettings();
            Debug.WriteLine("Current addon: " + currAddon.Name);
            addonDropDown.Text = currAddon.Name;
            calculateSize();
			if (currAddon.GDS_rank != "") {
				gdsButton.Text = "#" + currAddon.GDS_rank;
			} else {
				gdsButton.Text = "";
			}
			//checkForNewLibraryVersions();
        }

		//TODO:
		private void checkForNewLibraryVersions() {
			string vscripts = Path.Combine(currAddon.GamePath, "scripts", "vscripts");
			string[] luaFiles = Directory.GetFiles(vscripts, "*.lua", SearchOption.AllDirectories);
			foreach (string luaFile in luaFiles) {
				if (libraries.ContainsKey(luaFile)) {
					// this addon uses a known library
					string url = libraries[luaFile];

				}
			}
		}

        private void calculateSize()
        {
            long gameSize = GetDirectorySize(currAddon.GamePath);
            long contentSize = GetDirectorySize(currAddon.ContentPath);
            gameSizeLabel.Text = "Game Size: " + gameSize / 1000000 + " MB";
            contentSizeLabel.Text = "Content Size: " + contentSize / 1000000 + " MB";
        }

        private void generateAddonEnglish_Click(object sender, EventArgs e)
        {
            currAddon.getAbilityTooltips(false);
            currAddon.getAbilityTooltips(true);
            currAddon.getUnitTooltips();
            currAddon.getHeroesTooltips();
            currAddon.writeTooltips();
        }


        private void copyToFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            f.Description = "Enter the folder to copy this addon's game and content directories to:";
            DialogResult res = f.ShowDialog();
            if (res == DialogResult.OK)
            {
                currAddon.CopyPath = f.SelectedPath;
            }
            else
            {
                return;
            }
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = @"C:\WINDOWS\system32\xcopy.exe";
            Debug.WriteLine("Content: " + currAddon.ContentPath);
            Debug.WriteLine("Path: " + currAddon.CopyPath);
            proc.StartInfo.Arguments = "\"" + currAddon.ContentPath + "\" \"" + Path.Combine(currAddon.CopyPath, "content") + "\" /D /E /I /Y";
            proc.Start();
            proc.StartInfo.Arguments = "\"" + currAddon.GamePath + "\" \"" + Path.Combine(currAddon.CopyPath, "game") + "\" /D /E /I /Y";
            proc.Start();
        }

        private void gameDir_Click(object sender, EventArgs e)
        {
            Process.Start(currAddon.GamePath);
        }

        private void contentDir_Click(object sender, EventArgs e)
        {
            Process.Start(currAddon.ContentPath);
        }

        private void diff_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Path.Combine(currAddon.ContentPath, "particles");
            fd.Title = "First particle:";
            DialogResult res = fd.ShowDialog();
            Particle ps1 = null;
            Particle ps2 = null;
            if (res == DialogResult.OK)
            {
                string path = fd.FileName;
                ps1 = new Particle(path);
            }

            fd.Title = "Second particle:";
            res = fd.ShowDialog();
            if (res == DialogResult.OK)
            {
                string path = fd.FileName;
                ps2 = new Particle(path);
            }
            ps1.diff(ps2);
        }

        private string promptForD2Extract()
        {
            MessageBox.Show(
                "Please enter the path to your extracted Dota 2 contents (ex. Extracted Dota 2 from GCFScape))",
                "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "Enter the path to your extracted Dota 2 contents (Top-most level)";
            DialogResult res = fd.ShowDialog();
            if (res == DialogResult.OK)
            {
                string path = fd.SelectedPath;
                Properties.Settings.Default.S2DotaExtractPath = path;
                return path;
            }
            return null;
        }

        /*
         * BAREBONES FORK CODE
         */

        NewAddonForm addonForm;

        public NewAddonForm AddonForm
        {
            get { return addonForm; }
            set { addonForm = value; }
        }

        private void forkAddon(string version)
        {
            // ensure a "barebones" folder is in the current directory, and it has game and content in it.
            string barebonesDir = Path.Combine(Environment.CurrentDirectory, "barebones");
            if (!Directory.Exists(barebonesDir))
            {
                DialogResult res = MessageBox.Show("No 'barebones' directory found in the D2ModKit folder. Download barebones now?",
                    "D2ModKit",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (res != DialogResult.OK)
                {
                    return;
                }

                BarebonesDLForm dl = new BarebonesDLForm(version);
                dl.ShowDialog();
            }

            if (Directory.Exists(barebonesDir))
            {
                if (!Directory.Exists(Path.Combine(barebonesDir, "game")) || !Directory.Exists(Path.Combine(barebonesDir, "content")))
                {
                    MessageBox.Show("Invalid structure in the 'barebones' directory. It must have a 'game' and 'content' folder.",
                        "D2ModKit",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            // at this point we have a valid 'barebones' directory.
            AddonForm = new NewAddonForm(version);
            AddonForm.Submit.Click += NewAddonSubmit_Click;
            AddonForm.CommentCheckBox.Checked = true;
            AddonForm.RemoveItemsCheckbox.Checked = true;
            AddonForm.RemoveHeroesCheckBox.Checked = true;
            AddonForm.ShowDialog();
        }

        private void bmdBarebones_Click(object sender, EventArgs e)
        {
            forkAddon("bmd");
        }

        void NewAddonSubmit_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            AddonForm.SubmitClicked = true;
            AddonForm.Close();
            parameters.Add("remove_print", AddonForm.CommentCheckBox.Checked.ToString().ToLower());
            parameters.Add("remove_heroes", AddonForm.RemoveHeroesCheckBox.Checked.ToString().ToLower());
            parameters.Add("remove_items", AddonForm.RemoveItemsCheckbox.Checked.ToString().ToLower());
			parameters.Add("version", AddonForm.Version);
            forkBarebones(parameters);
        }

        void forkBarebones(Dictionary<string, string> parameters)
        {
            string modName = AddonForm._TextBox.Text;
            ForkBarebones fork = new ForkBarebones(modName, parameters);

            // now move the directories to their appropriate places.
            string lower = modName.ToLower();

            string newG = Path.Combine(UGCPath, "game", "dota_addons", lower);
            string newC = Path.Combine(UGCPath, "content", "dota_addons", lower);

            string game = Path.Combine(Environment.CurrentDirectory, lower, "game", "dota_addons", lower);
            string content = Path.Combine(Environment.CurrentDirectory, lower, "content", "dota_addons", lower);
            Directory.Move(game, newG);
            Directory.Move(content, newC);
            // delete the old dir now.
            Directory.Delete(Path.Combine(Environment.CurrentDirectory, lower), true);

            Addon a = new Addon(newC, newG);
            addons.Add(a);
            // redo the tooltip addon names.
            setAddonNames();
			// add the addon to the AddonInfos
			AddonInfos.Add(a.Name.ToLower());
			SaveSettings();
            // make the active addon this one.
            selectCurrentAddon(lower);
            MessageBox.Show("The addon " + modName + " was successfully forked from Barebones.", "D2ModKit",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            Process.Start(Path.Combine(a.GamePath, "scripts", "vscripts"));
        }

		private void SaveSettings() {
			Settings.Default.Save();
		}

        private void particleDesigner_Click(object sender, EventArgs e)
        {
            ParticleDesignForm pdf = new ParticleDesignForm(currAddon);
            if (pdf.FormCanceled)
            {
                //user clicked cancel when picking particle files.
                pdf.Close();
                return;
            }
            DialogResult r = pdf.ShowDialog();
        }

        private void removeAddon_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you sure you want to delete '" + currAddon.Name + "'? " +
                "This will permanently delete the 'content' and 'game' directories of this addon.",
                "D2ModKit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (r == DialogResult.OK)
            {
                try
                {
                    Directory.Delete(currAddon.GamePath, true);
                    Directory.Delete(currAddon.ContentPath, true);
                }
                catch (IOException)
                {
                    MessageBox.Show("Could not delete this addon. Please close all programs that are using files related to this addon, " +
                    "and all related Windows Explorer processes.", "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                addons.Remove(currAddon);
                MessageBox.Show("The addon '" + currAddon.Name + "' was successfully removed.",
                    "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // reset currAddon.
                setAddonNames();
                selectCurrentAddon(addons[0].Name);
            }

        }

        private void generateWiki_Click(object sender, EventArgs e)
        {
            List<string> langPaths = currAddon.getAddonLangPaths();
            for (int i = 0; i < langPaths.Count(); i++)
            {
                WikiGeneration.Wiki wiki = new WikiGeneration.Wiki(currAddon, langPaths.ElementAt(i));
            }
            MessageBox.Show("Wikis for '" + currAddon.Name + "' have been successfully generated.",
                    "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Process.Start(Environment.CurrentDirectory);
        }

        private void templatesButton_Click(object sender, EventArgs e)
        {
            if (CheckOpened("Templates"))
            {
                return;
            }
            TemplatesForm tf = new TemplatesForm();
            tf.Show();
        }

        // check if form is already opened.
        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    frm.BringToFront();
                    return true;
                }
            }
            return false;
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private void overrideParticlesToBeNull_Click(object sender, EventArgs e)
        {
            string extractPath = Path.Combine(Environment.CurrentDirectory, "decompiled_particles");
            if (!Directory.Exists(extractPath))
            {
                DialogResult result = MessageBox.Show(
                    "No decompiled_particles folder detected in the D2ModKit folder. Would you like to download the decompiled particles now?",
                    "D2ModKit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Hand);

                if (result == DialogResult.Yes)
                {
                    Process.Start("https://mega.co.nz/#!cpgkSQbY!_xjYFGgkL2yhv0l8MPjEfESjN7B1S0cVP-QXsx3c-7M");
                }
                return;
            }

            // get the null particle contents.
            string nullParticlePath = Path.Combine(Environment.CurrentDirectory, "stubs", "null_particle.vpcf");
            string nullParticleContents = "";
            if (File.Exists(nullParticlePath))
            {
                nullParticleContents = File.ReadAllText(nullParticlePath);
            }

            // We need a particle system to work with.
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = extractPath;
            fd.Multiselect = true;
            fd.Title = "Select Particles To Override";
            DialogResult res = fd.ShowDialog();
            ParticleSystem Ps = null;

            if (res == DialogResult.OK)
            {
                Ps = new ParticleSystem(fd.FileNames);
            }
            else
            {
                return;
            }
            Particle[] particles = Ps.Particles;
            string path = particles[0].Path;
            int len = path.LastIndexOf('\\') - path.IndexOf("decompiled_particles");
            string folderStructure = path.Substring(path.IndexOf("decompiled_particles"), len);
            //folderStructure = folderStructure.Replace("\\", ".");
            string[] folds = folderStructure.Split('\\');
            // starting at 1 to forget about the first string, which is "decompiled_particles"
            string path2 = Path.Combine(CurrentAddon.ContentPath, "particles");
            for (int i = 1; i < folds.Length; i++)
            {
                path2 = Path.Combine(path2, folds[i]);
            }
            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }
            for (int i = 0; i < particles.Count(); i++)
            {
                Particle p = particles[i];
                p.Name = p.Name.Replace(".vpcf_c", ".vpcf");
                string newPath = Path.Combine(path2, p.Name);
                File.Copy(p.Path, newPath);
                File.WriteAllText(newPath, nullParticleContents);
            }
            Process.Start(path2);
        }

        private void vscriptsDir_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(currAddon.GamePath, "scripts", "vscripts");
            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        private void npcDir_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(currAddon.GamePath, "scripts", "npc");
            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        private void flash3Dir_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(currAddon.GamePath, "resource", "flash3");
            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        public static long GetDirectorySize(string p)
        {
            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles(p, "*.*", SearchOption.AllDirectories);

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // 4.
            // Return total size
            return b;
        }

        private void totalSize_Click(object sender, EventArgs e)
        {
            //Debug.WriteLine("Calc size.");
            calculateSize();
        }

        public static Environment.SpecialFolder getRootFolder()
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)))
            {
                return Environment.SpecialFolder.ProgramFilesX86;
            }
            else if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)))
            {
                return Environment.SpecialFolder.ProgramFiles;
            }
            return Environment.SpecialFolder.MyComputer;
        }

        private void breakUp(string itemStr)
        {
            string file = Path.Combine(currAddon.GamePath, "scripts", "npc", "npc_abilities_custom.txt");
            string folderPath = Path.Combine(currAddon.GamePath, "scripts", "npc", "abilities");
            if (itemStr == "Items")
            {
                file = Path.Combine(currAddon.GamePath, "scripts", "npc", "npc_items_custom.txt");
                folderPath = Path.Combine(currAddon.GamePath, "scripts", "npc", "items");
            }
            else if (itemStr == "Heroes")
            {
                file = Path.Combine(currAddon.GamePath, "scripts", "npc", "npc_heroes_custom.txt");
                folderPath = Path.Combine(currAddon.GamePath, "scripts", "npc", "heroes");
            }
            else if (itemStr == "Units")
            {
                file = Path.Combine(currAddon.GamePath, "scripts", "npc", "npc_units_custom.txt");
                folderPath = Path.Combine(currAddon.GamePath, "scripts", "npc", "units");
            }

            string folderName = file.Substring(file.LastIndexOf('\\') + 1);
            // get rid of extension.
            folderName = folderName.Substring(0, folderName.LastIndexOf('.'));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string allText = File.ReadAllText(file);
            KeyValue[] kvs = KVLib.KVParser.KV1.ParseAll(allText);
            foreach (KeyValue kv in kvs)
            {
                if (kv.Key == "DOTAAbilities" || kv.Key == "DOTAHeroes" || kv.Key == "DOTAUnits")
                {
                    // skip this first nextKey, go straight to children.
                    if (kv.HasChildren)
                    {
                        IEnumerable<KeyValue> kvs2 = kv.Children;
                        KeyValue[] kvArr = kvs2.ToArray();
                        // record start line number and end line number of each Key-Value block
                        int[] startLineNumber = new int[kvArr.Length];
                        int[] endLineNumber = new int[kvArr.Length];

                        // catch the start pointer, ignore all "Version"s
                        int ptr = 0;
                        while (kvArr[ptr].Key == "Version" && ptr < kvArr.Length)
                            ptr++;

                        // store the start pointer
                        int startPtr = ptr;

                        // init the first key
                        string key = kvArr[ptr].Key;

                        // loop over all lines to record the start/end of all kvs
                        string[] lines = allText.Split('\n');
                        for (int index = 0; index < lines.Length; index++)
                        {
                            string line = lines[index];
                            if (line.Trim().StartsWith("\"" + key))
                            {
                                int ind = index - 1;
                                // go back to add all comments/empty lines to this block
                                while ((lines[ind].Trim() == "" || lines[ind].Trim().StartsWith("//")) && (ind > 0))
                                    ind--;
                                startLineNumber[ptr] = ind + 1;
                                // record the end of the block for last pointer
                                if (ptr > 0)
                                    endLineNumber[ptr - 1] = ind;
                                if (ptr < kvArr.Length - 1)
                                {
                                    ptr++;
                                    key = kvArr[ptr].Key;
                                }
                            }
                        }
                        // deal with very last pointer
                        int lastInd = lines.Length - 1;
                        while (lastInd > 0 && lines[lastInd].Contains("}") && (lines[lastInd].IndexOf("//") > lines[lastInd].IndexOf("}")))
                            lastInd--;
                        endLineNumber[kvArr.Length - 1] = lastInd;

                        // generate break-down kv files and write text
                        for (int p = startPtr; p < kvArr.Length; p++)
                        {
                            string filePath = Path.Combine(folderPath, kvArr[p].Key + ".txt");
                            File.Create(filePath).Close();
                            StringBuilder sb = new StringBuilder();

                            for (int p1 = startLineNumber[p]; p1 <= endLineNumber[p]; p1++)
                            {
                                string line = lines[p1];
                                // remove first tab
                                if (line.StartsWith("\t"))
                                {
                                    line = line.Substring(1);
                                }
                                sb.Append(line);
                            }

                            string output = sb.ToString();
                            // remove beginning newline
                            output = output.TrimStart();

                            // if last file, we need to remove the last }
                            if (p == kvArr.Length - 1)
                            {
                                output = output.TrimEnd();
                                if (output.EndsWith("}"))
                                {
                                    output = output.Substring(0, output.Length - 1);
                                }
                            }
                            File.WriteAllText(filePath, output);
                        }
                    }
                }
            }
        }

        private void combineBtn_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedItemCollection items = kvFileCheckbox.CheckedItems;
            for (int i = 0; i < items.Count; i++)
            {
                string itemStr = items[i].ToString();

                string fold = Path.Combine(currAddon.GamePath, "scripts", "npc", "abilities");
                if (itemStr == "Items")
                {
                    fold = Path.Combine(currAddon.GamePath, "scripts", "npc", "items");
                }
                else if (itemStr == "Heroes")
                {
                    fold = Path.Combine(currAddon.GamePath, "scripts", "npc", "heroes");
                }
                else if (itemStr == "Units")
                {
                    fold = Path.Combine(currAddon.GamePath, "scripts", "npc", "units");
                }

                if (!Directory.Exists(fold))
                {
                    DialogResult res = MessageBox.Show("npc_" + itemStr.ToLower() + "_custom has not been broken up. Break it up now?",
                        "D2ModKit",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information);
                    if (res != DialogResult.OK)
                    {
                        continue;
                    }
                    breakUp(itemStr);
                }

                string foldName = fold.Substring(fold.LastIndexOf('\\') + 1);
                string parentFolder = fold.Substring(0, fold.LastIndexOf('\\'));
                string bigKVPath = Path.Combine(parentFolder, "npc_" + foldName + "_custom.txt");

                //create backups dir if doesn't exist.
                string backupsDir = Path.Combine(parentFolder, "backups");
                if (!Directory.Exists(backupsDir))
                {
                    Directory.CreateDirectory(backupsDir);
                }

                string backupPath = Path.Combine(backupsDir, "npc_" + foldName + "_custom.txt");
                if (File.Exists(bigKVPath))
                {
                    // Delete old backups.
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }
                    // back it up
                    File.Move(bigKVPath, backupPath);
                    File.Create(bigKVPath).Close();
                }
                // so now we have the big KV file created and ready to be populated.

                string[] files = Directory.GetFiles(fold);
                StringBuilder text = new StringBuilder("\"DOTAAbilities\"" + "\n{\n");
                if (foldName == "heroes")
                {
                    text = new StringBuilder("\"DOTAHeroes\"" + "\n{\n");
                }
                else if (foldName == "units")
                {
                    text = new StringBuilder("\"DOTAUnits\"" + "\n{\n");
                }
                bool hasPrecacheEverything = false;
                foreach (string file in files)
                {
                    if (file.Contains("npc_precache_everything.txt"))
                    {
                        // skip this, save it for last
                        hasPrecacheEverything = true;
                        continue;
                    }
                    bool addTab = false;
                    string[] lines = File.ReadAllLines(file);
                    for (int j = 0; j < lines.Length; j++)
                    {
                        string line = lines[j];
                        if (j == 0 && line.StartsWith("\t") == false && line.StartsWith("  ") == false)
                        {
                            addTab = true;
                        }
                        string newLine = line;
                        if (addTab)
                        {
                            newLine = "\t" + line;
                        }
                        text.AppendLine(newLine);
                    }
                }
                // now do npc_precache_everything.txt
                if (hasPrecacheEverything)
                {
                    bool addTab = false;
                    string[] lines = File.ReadAllLines(Path.Combine(fold, "npc_precache_everything.txt"));
                    for (int j = 0; j < lines.Length; j++)
                    {
                        string line = lines[j];
                        if (j == 0 && line.StartsWith("\t") == false && line.StartsWith("  ") == false)
                        {
                            addTab = true;
                        }
                        string newLine = line;
                        if (addTab)
                        {
                            newLine = "\t" + line;
                        }
                        text.AppendLine(newLine);
                    }
                }
                text.Append("}");
                File.WriteAllText(bigKVPath, text.ToString());
            }
            System.Timers.Timer kvLabelTimer = new System.Timers.Timer(800);
            kvLabelTimer.SynchronizingObject = this;
            kvLabelTimer.AutoReset = false;
            kvLabelTimer.Start();
            kvLabelTimer.Elapsed += kvLabelTimer_Elapsed;
            combineSuccessLabel.Text = "Combine\nsuccess";
        }

        void kvLabelTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            combineSuccessLabel.Text = "";
        }

        private void myllsBarebones_Click(object sender, EventArgs e)
        {
            forkAddon("myll");
        }

        #region vtex

        private void compileVtex_Click(object sender, EventArgs e)
        {
            // show dialog to open .tga or .mks file
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.Filter = "TGA|*.tga|MKS|*.mks";

            string materialsPath = Path.Combine(currAddon.ContentPath, "materials");
            if (!Directory.Exists(materialsPath))
            {
                Directory.CreateDirectory(materialsPath);
            }
            fd.InitialDirectory = materialsPath;


            if (!(fd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            string[] filePaths = fd.FileNames;

            foreach (string file in filePaths)
            {
                string f = file;
                // ensure it's inside content path
                if (!(f.Contains("content\\dota_addons")))
                {
                    MessageBox.Show("Source file should be inside your content path",
                        "D2ModKit",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                f = f.Substring(f.IndexOf("content\\dota_addons") + 20);
                f = f.Substring(f.IndexOf("\\") + 1);
                f = f.Replace('\\', '/');
                // todo: maybe we should put this into some resource text files
                string[] vtexFileStrings = 
                {
                    "<!-- dmx encoding keyvalues2_noids 1 format vtex 1 -->",
                    "\"CDmeVtex\"",
                    "{",
                    "	\"m_inputTextureArray\" \"element_array\" ",
                    "	[",
                    "		\"CDmeInputTexture\"",
                    "		{",
                    "			\"m_name\" \"string\" \"0\"",
                    "			\"m_fileName\" \"string\" \"" + f + "\"",
                    "			\"m_colorSpace\" \"string\" \"srgb\"",
                    "			\"m_typeString\" \"string\" \"2D\"",
                    "		}",
                    "	]",
                    "	\"m_outputTypeString\" \"string\" \"2D\"",
                    "	\"m_outputFormat\" \"string\" \"DXT5\"",
                    "	\"m_textureOutputChannelArray\" \"element_array\"",
                    "	[",
                    "		\"CDmeTextureOutputChannel\"",
                    "		{",
                    "			\"m_inputTextureArray\" \"string_array\"",
                    "				[",
                    "					\"0\"",
                    "				]",
                    "			\"m_srcChannels\" \"string\" \"rgba\"",
                    "			\"m_dstChannels\" \"string\" \"rgba\"",
                    "			\"m_mipAlgorithm\" \"CDmeImageProcessor\"",
                    "			{",
                    "				\"m_algorithm\" \"string\" \"\"",
                    "				\"m_stringArg\" \"string\" \"\"",
                    "				\"m_vFloat4Arg\" \"vector4\" \"0 0 0 0\"",
                    "			}",
                    "			\"m_outputColorSpace\" \"string\" \"srgb\"",
                    "		}",
                    "	]",
                    "}"
                };
            }
            //Workshop Tools will automatic compile it to .vtex_c file when addon start up or change detected
            string dir = filePaths[0];
            dir = dir.Substring(0,dir.LastIndexOf('\\'));
            Process.Start(dir);
        }

        private void decompileVtex_Click(object sender, EventArgs e)
        {
            string extract = Path.Combine(UGCPath, "game", "dota_imported", "materials");
            if (!Directory.Exists(extract))
            {
                MessageBox.Show("You must extract the 'materials' folder from " + Path.Combine("dota_ugc", "game", "dota_imported", "pak01_dir.vpk") +
                    " into " + Path.Combine("dota_ugc", "game", "dota_imported") + " using GCFScape before using this feature.",
                    "D2ModKit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            fd.InitialDirectory = Path.Combine(UGCPath, "game", "dota_imported", "materials");
            fd.Filter = "Valve Texture File(*.vtex_c)|*.vtex_c";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string[] vtexCPaths = fd.FileNames;
                foreach (string path in vtexCPaths)
                {
                    //resourceinfo.exe -i <your vtex_c file> -debug tga -mip
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.UseShellExecute = true;
                    startInfo.FileName = "cmd.exe";
                    startInfo.WorkingDirectory = Path.Combine(Path.Combine(UGCPath, "game"), "bin", "win64");
                    startInfo.Arguments = "/c resourceinfo.exe -i \"" + path + "\" -debug tga -mip";
                    Debug.WriteLine(startInfo.Arguments);
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                }
                // Prepare to move the tga files to the addon's content dir.
                string materialsPath = Path.Combine(currAddon.ContentPath, "materials");
                if (!Directory.Exists(materialsPath))
                {
                    Directory.CreateDirectory(materialsPath);
                }
                //move the tga files to the addon's content dir.
                string[] tgaFiles = Directory.GetFiles(Path.Combine(Path.Combine(UGCPath, "game"), "bin", "win64"), "*.tga");
                foreach (string file in tgaFiles)
                {
                    string fileName = file.Remove(0,file.LastIndexOf('\\')+1);
                    File.Move(file, Path.Combine(materialsPath, fileName));
                }
                Process.Start(materialsPath);
            }
        }

        #endregion vtex

        private void modDotaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("https://moddota.com/forums/");
        }

        private void tutorialsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://moddota.com/forums/tutorial-index");
        }

        private void ircToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://moddota.com/forums/chat");
        }

		private string getVal(string addonName, string key) {
			foreach (string s in AddonInfos) {
				if (s.StartsWith(addonName.ToLower())) {
					string[] parts = s.Split(';');
					for (int i = 0; i < parts.Length; i++) {
						// skip the first and last parts. last parts will be empty, and
						// first part won't have the = sign, it's addon name.
						if (i == 0 || i == parts.Length-1) {
							continue;
						}
						string p = parts[i];
						if (p.Substring(0, p.IndexOf('=')) == key) {
							return p.Substring(p.IndexOf('=') + 1);
						}
					}
				}
			}
			return "";
		}

		private void add(string addonName, string key, string val) {
			for (int i = 0; i < AddonInfos.Count; i++) {
				string s = AddonInfos[i];
				if (s.StartsWith(addonName.ToLower())) {
					AddonInfos[i] += key + "=" + val + ";";
				}
			}
		}

        private void gdsButton_Click(object sender, EventArgs e)
        {
			string link = getVal(currAddon.Name, "gds_link");
			if (link != "") {
				Process.Start(link);
			}
            else
            {
                EnterLinkForm elf = new EnterLinkForm(currAddon.Name, "gds");
                DialogResult res = elf.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    return;
                }
				link = elf.link;
                string modID = link.Substring(link.LastIndexOf('=')+1);
				add(currAddon.Name, "gds_link", link);
				add(currAddon.Name, "gds_modID", modID);
				SaveSettings();
            }
        }

		private void steamButton_Click(object sender, EventArgs e) {
			string link = getVal(currAddon.Name, "workshop_link");
			if (link != "") {
				Process.Start(link);
			} else {
				EnterLinkForm elf = new EnterLinkForm(currAddon.Name, "steam");
				DialogResult res = elf.ShowDialog();
				if (res == DialogResult.Cancel) {
					return;
				}
				link = elf.link;
				string workshop_id = link.Substring(link.LastIndexOf('=') + 1);
				add(currAddon.Name, "workshop_id", workshop_id);
				add(currAddon.Name, "workshop_link", link);
				SaveSettings();
			}
		}

        private void setAddonInfos()
        {
            foreach (Addon a in Addons)
            {
				bool found = false;
                foreach (string s in AddonInfos)
                {
                    string[] parts = s.Split(';');
                    string name = parts[0];
					if (a.Name.ToLower() == name.ToLower()) {
						found = true;
					}
                }
				if (!found) {
					// this is a valid addon but not in AddonInfos
					AddonInfos.Add(a.Name.ToLower() + ";");
				}
            }
			SaveSettings();
        }

        private void chineseBarebones_Click(object sender, EventArgs e)
        {

        }

        private void otherToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://moddota.com/forums/tools");
        }

        private void rdota2moddingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.reddit.com/r/dota2modding/");
        }

		private void preferencesToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void beginnersBarebonesToolStripMenuItem_Click(object sender, EventArgs e) {
			forkAddon("noya");
		}

        /*
        private void overrideSoundsToBeNullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.S2DotaExtractPath == "")
            {
                DialogResult r = MessageBox.Show("No Source 2 Dota Extract path defined. Set the path now?",
                        "D2ModKit", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (r == DialogResult.Cancel)
                {
                    return;
                }
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                // let the user see the particles directory first.
                fbd.Description =
                    "Browse to your Extracted Source 2 Dota path.";
                DialogResult fbd_res = fbd.ShowDialog();
                if (fbd_res == DialogResult.OK)
                {
                    Properties.Settings.Default.S2DotaExtractPath = fbd.SelectedPath;
                    Settings.Default.Save();
                }
                else
                {
                    return;
                }
            }

            // get the null sound contents.
            string nullSoundPath = Path.Combine(Environment.CurrentDirectory, "stubs", "null_sound.vsndevts");
            string[] nullSoundContents = null;
            if (File.Exists(nullSoundPath))
            {
                nullSoundContents = File.ReadAllLines(nullSoundPath);
            }

            string extractPath = Properties.Settings.Default.S2DotaExtractPath;

            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Path.Combine(extractPath, "scripts");
            fd.Multiselect = true;
            fd.Title = "Select Sound Scripts To Override";
            DialogResult res = fd.ShowDialog();
            string[] fileNames = null;

            if (res == DialogResult.OK)
            {
                fileNames = fd.FileNames;
            }
            else
            {
                return;
            }

            for (int i = 0; i < fileNames.Length; i++)
			{
                string path = fileNames[i];
                // for this file we need the folder structure.
                int len = path.LastIndexOf('\\') - path.IndexOf("scripts");
                string folderStructure = path.Substring(path.IndexOf("scripts"), len);
                //folderStructure = folderStructure.Replace("\\", ".");
                string[] folds = folderStructure.Split('\\');
                // starting at 1 to forget about the first string, which is "scripts"
                string path2 = Path.Combine(CurrentAddon.ContentPath, "soundevents");
                for (int j = 1; j < folds.Length; j++)
                {
                    path2 = Path.Combine(path2, folds[j]);
                }
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
                string soundFileName = path.Substring(path.LastIndexOf('\\') + 1);
                soundFileName = soundFileName.Replace(".txt", ".vsndevts");
                string newPath = Path.Combine(path2, soundFileName);
                File.Create(newPath).Close();
                KeyValue[] rootKVs = KVParser.KV1.ParseAll(File.ReadAllText(path));
                for (int j = 0; j < rootKVs.Length; j++)
                {
                    string soundName = rootKVs[j].Key;
                    nullSoundContents[0] = "\"" + soundName + "\"";
                    File.AppendAllLines(newPath, nullSoundContents);
                }
			}
        }*/
    }
}