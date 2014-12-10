using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

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

        private bool hasSettings = false;

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

        public MainForm()
        {
            InitializeComponent();
            //sparkle = new Sparkle("");
            currentAddonDropDown.DropDownItemClicked += currentAddonDropDown_DropDownItemClicked;
            versionLabel.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " by Myll";
            if (Properties.Settings.Default.UGCPath != "")
            {
                UGCPath = Properties.Settings.Default.UGCPath;
                if (Directory.Exists(UGCPath))
                {
                    HasSettings = true;
                }
            }

            if (HasSettings) {
                // and use that to find the game and content dirs.
                getAddons();
            }
            else
            {
                getUGCPath();
            }
            selectCurrentAddon(Properties.Settings.Default.CurrAddon);

        }

        private bool getUGCPath()
        {
            while (!HasSettings)
            {
                // Auto-find the dota_ugc path.
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine;
                try
                {
                    regKey = regKey.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 570");
                    if (regKey != null)
                    {
                        string dir = regKey.GetValue("InstallLocation").ToString();
                        UGCPath = Path.Combine(dir, "dota_ugc");
                        Debug.WriteLine("Directory: " + dir);
                    }
                }
                catch (Exception ex)
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
                        DialogResult res = MessageBox.Show("That is not a path to your dota_ugc folder.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand);

                        if (res == DialogResult.Retry)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Properties.Settings.Default.UGCPath = UGCPath;
                Properties.Settings.Default.Save();

                // get the game and content dirs from the ugc path.
                getAddons();
                HasSettings = true;
                return true;
            }
            return false;
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
                MessageBox.Show("You need to select your dota_ugc path before you can use this.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "decompiled_particles")))
            {
                MessageBox.Show("No decompiled_particles folder detected. Please place a decompiled_particles folder into the D2ModKit folder before proceding.", 
                    "D2ModKit",MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // let the user see the particles directory first.
            string initialPath = Path.Combine(currAddon.ContentPath,"particles");
            browser.SelectedPath = initialPath;
            browser.ShowNewFolderButton = true;
            browser.Description = "Browse to where the particles will be copied to. They must be placed in the particles directory.";
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
                    System.IO.File.Copy(path, targetPath);
                }
                catch (IOException)
                {
                    /*
                    string warnMsg = "You are about to overwrite " + targetPath + ". Procede?";
                    DialogResult result = MessageBox.Show(warnMsg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (!result.Equals(DialogResult.Yes))
                    {
                        overwriteAllowed = false;
                    }*/
                }

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
                System.IO.File.WriteAllText(p.Path, p.ToString());
            }
            
            //MessageBox.Show("Particles have been successfully copied.",
            //    "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            if (isValidAddon(a)) {
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
            currentAddonDropDown.DropDownItems.Clear();
            foreach (string name in AddonNames)
            {
                ToolStripItem item = currentAddonDropDown.DropDownItems.Add(name);
                item.Font = new Font("Calibri",13f, FontStyle.Bold, GraphicsUnit.Pixel);
            }
        }

        void currentAddonDropDown_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (AddonNames.Contains(e.ClickedItem.Text))
            {
                selectCurrentAddon(e.ClickedItem.Text);
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

        void selectCurrentAddon(string addon)
        {
            currAddon = getAddonFromName(addon);
            Properties.Settings.Default.CurrAddon = currAddon.Name;
            Properties.Settings.Default.Save();
            Debug.WriteLine("Current addon: " + currAddon.Name);
            currentAddonDropDown.Text = "Addon: " + currAddon.Name;
            //ApplicationAssembly.GetName().Version.ToString()
            this.Text = "D2 ModKit - " + currAddon.Name;
        }

        private void generateAddonEnglish_Click(object sender, EventArgs e)
        {
            //first take the existing addon_english and store the keys and values, so we don't override the ones already defined.
            //currAddon.getCurrentAddonEnglish();
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
            proc.StartInfo.Arguments = "\"" + currAddon.ContentPath + "\" \"" + Path.Combine(currAddon.CopyPath, "content") + "\" /D /E /I /Y"; //@"C:\source C:\destination /E /I";
            proc.Start();
            proc.StartInfo.Arguments = "\"" + currAddon.GamePath + "\" \"" + Path.Combine(currAddon.CopyPath, "game") + "\" /D /E /I /Y";
            proc.Start();
            //proc.Close();

            //DialogResult r2 = MessageBox.Show("Would you like this addon to copy to this location everytime the \"Copy To Folder\" button is clicked?", "D2ModKit",
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
            MessageBox.Show("Please enter the path to your extracted Dota 2 contents (ex. Extracted Dota 2 from GCFScape))", "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        private void overrideSounds_Click(object sender, EventArgs e)
        {
            string extract = Properties.Settings.Default.Dota2ExtractPath;
            if (extract == "")
            {
                extract = promptForD2Extract();
                if (extract == null)
                {
                    return;
                }
            }
        }


        /*
         * BAREBONES FORK CODE
         */
        /*
        private BarebonesDLProgress barebonesDLProgressForm;

        public BarebonesDLProgress BarebonesProgressForm
        {
            get { return barebonesDLProgressForm; }
            set { barebonesDLProgressForm = value; }
        }

        NewAddonForm addonForm;

        public NewAddonForm AddonForm
        {
            get { return addonForm; }
            set { addonForm = value; }
        }

        private void downloadBarebones()
        {
            WebClient wc = new WebClient();
            string path = Path.Combine(Environment.CurrentDirectory, "barebones.zip");
            // Delete barebones.zip if it exists in this dir.
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            wc.DownloadFileTaskAsync(new Uri("https://github.com/bmddota/barebones/archive/source2.zip"), path);
            BarebonesProgressForm = new BarebonesDLProgress();
            BarebonesProgressForm.ShowDialog();
        }

        private void forkAddon_Click(object sender, EventArgs e)
        {
            AddonForm = new NewAddonForm();
            AddonForm.Submit.Click += NewAddonSubmit_Click;
            DialogResult res = AddonForm.ShowDialog();

            if (res == DialogResult.Cancel && !AddonForm.SubmitClicked)
            {
                Debug.WriteLine("Returning.");
                return;
            }
        }

        void NewAddonSubmit_Click(object sender, EventArgs e)
        {
            AddonForm.SubmitClicked = true;
            AddonForm.Close();
            downloadBarebones();
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            BarebonesProgressForm.Close();
            string modName = AddonForm._TextBox.Text;
            ForkBarebones fork = new ForkBarebones(modName);
            string currLoc = Path.Combine(Environment.CurrentDirectory, modName.ToLower());
            string game = Path.Combine(currLoc, "game", "dota_addons", modName.ToLower());
            string content = Path.Combine(currLoc, "content", "dota_addons", modName.ToLower());

            string newContent = Path.Combine(UGCPath, "content", "dota_addons", modName.ToLower());
            string newGame = Path.Combine(UGCPath, "game", "dota_addons", modName.ToLower());
            Directory.Move(game, newGame);
            Directory.Move(content, newContent);

            // we don't need this dir anymore.
            if (Directory.Exists(fork.Temp))
            {
                try
                {
                    Directory.Delete(fork.Temp);
                }
                catch (IOException) { }
            }

            MessageBox.Show(modName.ToLower() + " has been successfully created.", "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Addon a = new Addon(newContent, newGame);
            addons.Add(a);
            setAddonNames();
            selectCurrentAddon(a.Name);

        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int currPercentage = BarebonesProgressForm.CurrentPercentage;
            int newPercentage = e.ProgressPercentage;
            if (newPercentage - currPercentage > 0)
            {
                BarebonesProgressForm.BarebonesProgressBar.Increment(newPercentage - currPercentage);
            }
        }*/

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

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
    }
}
