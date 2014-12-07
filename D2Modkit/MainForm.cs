using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using KVLib;

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

        public ParticleSystem CurrParticleSystem
        {
            get { return currParticleSystem; }
            set { currParticleSystem = value; }
        }

        private ParticleRenameForm pRenameForm;

        public ParticleRenameForm PRF
        {
            get { return pRenameForm; }
            set { pRenameForm = value; }
        }

        public MainForm()
        {
            InitializeComponent();
            //sparkle = new Sparkle("");
            currentAddonDropDown.DropDownItemClicked += currentAddonDropDown_DropDownItemClicked;
            versionLabel.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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

            if (browserResult == DialogResult.Cancel || browserResult == DialogResult.Abort)
            {
                return;
            }

            string folderPath = browser.SelectedPath;

            // prompt user if he wants to change particle's color
            DialogResult r = MessageBox.Show("Would you like to change the color of this particle system?", "D2ModKit", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            bool changeColor = false, rename = false;
            string[] rgb = new string[3];

            if (r == DialogResult.Yes)
            {
                changeColor = true;
                rgb = getRGB();
                if (rgb == null)
                {
                    changeColor = false;
                }
            }

            DialogResult r2 = MessageBox.Show("Would you like to change the name of this particle system?", "D2ModKit", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r2 == DialogResult.Yes)
            {
                rename = true;
            }



            string folderName = folderPath.Substring(folderPath.LastIndexOf('\\') + 1);
            List<Particle> particles = new List<Particle>();
            foreach (string path in particlePaths)
            {
                bool overwriteAllowed = true;
                string particleName = path.Substring(path.LastIndexOf('\\') + 1);
                string targetPath = Path.Combine(folderPath, particleName);

                try
                {
                    System.IO.File.Copy(path, targetPath);
                }
                catch (IOException overwriteException)
                {
                    string warnMsg = "You are about to overwrite " + targetPath + ". Procede?";
                    DialogResult result = MessageBox.Show(warnMsg, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (!result.Equals(DialogResult.Yes))
                    {
                        overwriteAllowed = false;
                    }
                }

                if (overwriteAllowed)
                {
                    particles.Add(new Particle(targetPath));
                }
            }

            // fix child refs.
            CurrParticleSystem = new ParticleSystem(particles);
            if (changeColor)
            {
                CurrParticleSystem.changeColor(rgb);
            }

            if (rename) // renaming also fixes child refs.
            {
                string[] paths = CurrParticleSystem.Paths;
                PRF = new ParticleRenameForm();
                PRF.Submit.Click += Submit_Click;
                DialogResult r3 = PRF.ShowDialog();
            }
            else
            {
                CurrParticleSystem.fixChildRefs(folderPath);
            }

            for (int i = 0; i < CurrParticleSystem.Particles.Count(); i++)
            {
                Particle p = CurrParticleSystem.Particles.ElementAt(i);
                System.IO.File.WriteAllText(p.Path, p.ToString());
            }
            
            MessageBox.Show("Particles have been successfully copied.",
                "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            Addon a = new Addon(str2);
                            a.ContentPath = Path.Combine(UGCPath, "content", "dota_addons", a.Name);
                            addons.Add(a);
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
                item.Font = new Font("Segoe UI",12f, FontStyle.Bold, GraphicsUnit.Pixel);
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

            //DialogResult r = MessageBox.Show("Would you like this addon to copy to this location everytime the \"Copy To Folder\" button is clicked?", "D2ModKit",
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

        private void recolorParticles_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            Debug.WriteLine("Current directory: " + Environment.CurrentDirectory);
            fd.InitialDirectory = Path.Combine(currAddon.ContentPath, "particles");
            fd.Multiselect = true;
            fd.Title = "Select particles to recolor";
            DialogResult res = fd.ShowDialog();
            if (res == DialogResult.OK)
            {
                string[] rgb = getRGB();
                // make sure user didn't close the color dialog box.
                if (rgb == null)
                {
                    return;
                }
                ParticleSystem ps = new ParticleSystem(fd.FileNames);
                ps.changeColor(rgb);
                for (int i = 0; i < ps.Particles.Count(); i++)
                {
                    Particle p = ps.Particles.ElementAt(i);
                    System.IO.File.WriteAllText(p.Path, p.ToString());
                }

                string rgb_output = "R: " + rgb[0] + " G: " + rgb[1] + " B: " + rgb[2];
                MessageBox.Show("Particles successfully recolored to: " + rgb_output, "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void renameParticles_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            Debug.WriteLine("Current directory: " + Environment.CurrentDirectory);
            fd.InitialDirectory = Path.Combine(currAddon.ContentPath, "particles");
            fd.Multiselect = true;
            fd.Title = "Select particles to rename";
            DialogResult res = fd.ShowDialog();
            if (res == DialogResult.OK)
            {
                string[] paths = fd.FileNames;
                CurrParticleSystem = new ParticleSystem(paths);
                PRF = new ParticleRenameForm();
                PRF.Submit.Click += Submit_Click;
                DialogResult r = PRF.ShowDialog();

                if (!PRF.SubmitClicked)
                {
                    return;
                }

                for (int i = 0; i < paths.Length; i++)
                {
                    Particle p = CurrParticleSystem.Particles[i];
                    System.IO.File.WriteAllText(p.Path, p.ToString());
                }

                //MessageBox.Show("Particles successfully renamed with base name: " + PRF.PTextBox.Text, "D2ModKit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void Submit_Click(object sender, EventArgs e)
        {
            PRF.SubmitClicked = true;
            PRF.Close();
            string[] paths = CurrParticleSystem.Paths;
            string newBase = PRF.PTextBox.Text;
            CurrParticleSystem.rename(newBase);
            Process.Start(paths[0].Substring(0, paths[0].LastIndexOf('\\')));
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
    }
}
