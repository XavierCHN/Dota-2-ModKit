using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace D2ModKit
{
    public partial class BarebonesDLForm : Form
    {
        public BarebonesDLForm(string version)
        {
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;

            InitializeComponent();

            // delete barebones.zip if it already exists.
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "barebones.zip")))
            {
                File.Delete(Path.Combine(Environment.CurrentDirectory, "barebones.zip"));
            }

            if (version == "bmd")
            {
                wc.DownloadFileAsync(new Uri("https://github.com/bmddota/barebones/archive/source2.zip"),
                    Path.Combine(Environment.CurrentDirectory, "barebones.zip"));
            } else if (version == "myll")
            {
                label1.Text = "Downloading Barebones from:\nhttps://github.com/Myll/barebones/archive/source2.zip";

                wc.DownloadFileAsync(new Uri("https://github.com/Myll/barebones/archive/source2.zip"),
                    Path.Combine(Environment.CurrentDirectory, "barebones.zip"));
            } else if (version == "noya") {
				label1.Text = "Downloading Barebones from:\nhttps://github.com/MNoya/barebones/archive/source2.zip";

				wc.DownloadFileAsync(new Uri("https://github.com/MNoya/barebones/archive/source2.zip"),
					Path.Combine(Environment.CurrentDirectory, "barebones.zip"));
			}
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // extract it now
            string zipPath = Path.Combine(Environment.CurrentDirectory, "barebones.zip");
            ZipFile.ExtractToDirectory(zipPath, Environment.CurrentDirectory);

            // rename it
            string oldPath = Path.Combine(Environment.CurrentDirectory, "barebones-source2");
            Directory.Move(oldPath, Path.Combine(Environment.CurrentDirectory, "barebones"));

            // delete the zip
            try
            {
                File.Delete(zipPath);
            }
            catch (IOException)
            {
                Debug.WriteLine("Couldn't delete zipfile.");
            }

            this.Close();
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = progressBar1.Value + (e.ProgressPercentage-progressBar1.Value);
        }
    }
}
