using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using D2ModKit.Properties;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
            ugcTextBox.Text = Settings.Default.UGCPath;
            this.AcceptButton = submitButton;
        }

        private void browseUGCButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult r = fbd.ShowDialog();
            if (r != DialogResult.OK)
            {
                return;
            }
            string path = fbd.SelectedPath;
            Settings.Default.UGCPath = path;
            this.ugcTextBox.Text = path;
        }

        private void note0LoreCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GenNote0Lore = note0LoreCheckBox.Checked;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
