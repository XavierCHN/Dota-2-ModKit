using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class NewLibraryForm : Form
    {
        public List<string> chosenLibs = new List<string>();
        public string libName;
        public string linkToLib;
        public string localLink;
        private Addon addon;

        public NewLibraryForm(Addon a)
        {
            addon = a;
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            libName = libraryNameTextBox.Text;
            linkToLib = linkToMainFileTextBox.Text;
            bool error = false;

            foreach (var item in libraryListBox.SelectedItems)
            {
                if (!addon.hasLibrary(item.ToString()))
                {
                    chosenLibs.Add(item.ToString());
                }
            }

            if ((libName == "" || linkToLib == "" || localLink == "") && chosenLibs.Count == 0)
            {
                this.DialogResult = DialogResult.Cancel;
            }

        }

        private void browseLocalBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult r = fbd.ShowDialog();
            if (r != DialogResult.OK)
            {
                return;
            }
            string path = fbd.SelectedPath;
            LocalTextBox.Text = path;
            localLink = path;
            
        }
    }
}
