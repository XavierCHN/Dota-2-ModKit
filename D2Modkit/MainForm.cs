using D2ModKit.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class MainForm : Form
    {
        private string gameDirectory;
        private string contentDirectory;
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

        public string GameDirectory
        {
            get { return gameDirectory; }
            set { gameDirectory = value; }
        }

        public string ContentDirectory
        {
            get { return contentDirectory; }
            set { contentDirectory = value; }
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

        private ParticleSystem currParticleSystem;

        public ParticleSystem ps
        {
            get { return currParticleSystem; }
            set { currParticleSystem = value; }
        }

        private bool displayChangelog = false;

        private string vers = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public MainForm()
        {
            // Check if application is already running.
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) System.Diagnostics.Process.GetCurrentProcess().Kill();

            string changelog = vers + " Changelog:\n\n" +
                "Forgot to add the latest feature to items too. Tooltips generator now checks if an item has new AbilitySpecials.\n";

            // Check for settings updates.
            if (Settings.Default.UpdateRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpdateRequired = false;
                Settings.Default.Save();
                displayChangelog = true;
            }

            InitializeComponent();

            // check if changelog should be displayed.
            if (displayChangelog)
            {
                OutputForm of = new OutputForm();
                of.RTextBox.Text = changelog;
                //of.RTextBox.SelectionFont
                of.ShowDialog();
            }

            // check for updates in a new thread.
            ThreadStart childref = new ThreadStart(CheckForUpdatesThread);
            Console.WriteLine("In Main: Creating the Child thread");
            Thread childThread = new Thread(childref);
            childThread.Start();

            // hook for when user selects a different addon.
            addonDropDown.SelectedIndexChanged += addonDropDown_SelectedIndexChanged;

            this.Text = "D2 ModKit - " + "v" + vers;
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
                getAddons();
            }
            else
            {
                getUGCPath();
            }
            selectCurrentAddon(Properties.Settings.Default.CurrAddon);
        }

        private void CheckForUpdatesThread()
        {
            Debug.WriteLine("Child thread starts");
            // ghetto way of checking for new vers
            string[] numStrings = vers.Split('.');
            int hundreds = Int32.Parse(numStrings[0]) * 100;
            int tens = Int32.Parse(numStrings[1]) * 10;
            int ones = Int32.Parse(numStrings[2]);
            int num = hundreds + tens + ones + 1;
            Debug.WriteLine("new num: " + num);
            int newHundreds = num / 100;
            int newTens = (num - newHundreds * 100) / 10;
            int newOnes = num - newHundreds * 100 - newTens * 10;
            string newVers = newHundreds + "." + newTens + "." + newOnes;
            Debug.WriteLine("New vers would be: " + newVers);
            // check for a new version
            string url = "https://github.com/Myll/Dota-2-ModKit/releases/download/v";
            url += newVers + "/D2ModKit.zip";

            // use these to test version updater.
            //newVers = "1.3.2";
            //url = "https://github.com/Myll/Dota-2-ModKit/releases/download/v1.3.2/D2ModKit.zip";

            // remember to keep the version naming consistent!
            // i.e. 1.3.8, 1.3.9, 1.4.0

            WebClient wc = new WebClient();
            try {
                Byte[] responseBytes = wc.DownloadData("https://github.com/Myll/Dota-2-ModKit/releases/tag/v" + newVers);
                string source = System.Text.Encoding.ASCII.GetString(responseBytes);
            }
            catch (Exception e)
            {
                Debug.WriteLine("No new vers available.");
                return;
            }
            DialogResult r = MessageBox.Show("Version " + newVers + " of D2ModKit is now available. Would you like to update now?", "New version available",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {
                // hide the mainform.
                //this.Hide();
                Debug.WriteLine("Url: " + url);
                UpdateForm uf = new UpdateForm(url, newVers);
                uf.ShowDialog();
            }
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
                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);

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
                    "D2ModKit", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    Process.Start("https://mega.co.nz/#!cpgkSQbY!_xjYFGgkL2yhv0l8MPjEfESjN7B1S0cVP-QXsx3c-7M");
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
            browser.RootFolder = Environment.SpecialFolder.ProgramFilesX86;
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
                    GameDirectory = str;
                    string dota_addons = Path.Combine(GameDirectory, "dota_addons");
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
            Properties.Settings.Default.Save();
            Debug.WriteLine("Current addon: " + currAddon.Name);
            addonDropDown.Text = currAddon.Name;
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

        private void checkForUpdates_Click(object sender, EventArgs e)
        {
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
                Properties.Settings.Default.Dota2ExtractPath = path;
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

        private void forkBarebones_Click(object sender, EventArgs e)
        {
            // ensure a "barebones" folder is in the current directory, and it has game and content in it.
            string barebonesDir = Path.Combine(Environment.CurrentDirectory, "barebones");
            if (!Directory.Exists(barebonesDir))
            {
                DialogResult res = MessageBox.Show("No 'barebones' directory found in the D2ModKit folder. Download barebones?",
                    "D2ModKit",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (res != DialogResult.OK)
                {
                    return;
                }

                BarebonesDLForm dl = new BarebonesDLForm();
                dl.ShowDialog();
            }

            if (Directory.Exists(barebonesDir))
            {
                if (!Directory.Exists(Path.Combine(barebonesDir, "game")) || !Directory.Exists(Path.Combine(barebonesDir, "content")))
                {
                    MessageBox.Show("Invalid structure in the 'barebones' directory. It must have a 'game' and 'content' folder.",
                        "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // at this point we have a valid 'barebones' directory.
            AddonForm = new NewAddonForm();
            AddonForm.Submit.Click += NewAddonSubmit_Click;
            AddonForm.ShowDialog();
        }

        void NewAddonSubmit_Click(object sender, EventArgs e)
        {
            AddonForm.SubmitClicked = true;
            AddonForm.Close();
            forkBarebones();
        }

        void forkBarebones()
        {
            string modName = AddonForm._TextBox.Text;
            ForkBarebones fork = new ForkBarebones(modName);

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
            // make the active addon this one.
            selectCurrentAddon(lower);
            MessageBox.Show("The addon " + lower + " was successfully forked from Barebones.", "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(Path.Combine(a.GamePath, "scripts", "vscripts"));
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
                selectCurrentAddon(addons[0].Name);
                setAddonNames();
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

        /// <summary>
        /// This method will check a url to see that it does not return server or protocol errors
        /// </summary>
        /// <param name="url">The path to check</param>
        /// <returns></returns>
        public bool isUrlValid(string url)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "HEAD"; //Get only the header information -- no need to download any content

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                int statusCode = (int)response.StatusCode;
                if (statusCode >= 100 && statusCode < 400) //Good requests
                {
                    return true;
                }
                else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                {
                    Debug.WriteLine(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                    return false;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    return false;
                }
                else
                {
                    Debug.WriteLine(String.Format("Unhandled status [{0}] returned for url: {1}", ex.Status, url), ex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(String.Format("Could not test url {0}.", url), ex);
            }
            return false;
        }

    }
}