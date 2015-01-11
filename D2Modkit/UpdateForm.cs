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
    public partial class UpdateForm : Form
    {
        public UpdateForm(string url, string newVers)
        {
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            InitializeComponent();
            Debug.WriteLine("Downloading new vers.");

            // delete D2ModKit.zip if exists.
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "D2ModKit.zip")))
            {
                File.Delete(Path.Combine(Environment.CurrentDirectory, "D2ModKit.zip"));
            }
            label1.Text = "Updating D2ModKit to v" + newVers + " ...";
            // start downloading.
            wc.DownloadFileAsync(new Uri(url),
                Path.Combine(Environment.CurrentDirectory, "D2ModKit.zip"));

        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // delete the nested D2ModKit folder if it exists.
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "D2ModKit")))
            {
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, "D2ModKit"), true);
            }

            // extract it now
            string zipPath = Path.Combine(Environment.CurrentDirectory, "D2ModKit.zip");
            ZipFile.ExtractToDirectory(zipPath, Environment.CurrentDirectory);

            // move D2ModKit.exe over to the main folder.
            string path = Path.Combine(Environment.CurrentDirectory, "D2ModKit", "D2ModKit.exe");

            // delete D2ModKit_new.exe if it exists.
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "D2ModKit_new.exe")))
            {
                File.Delete(Path.Combine(Environment.CurrentDirectory, "D2ModKit_new.exe"));
            }

            File.Move(path, Path.Combine(Environment.CurrentDirectory, "D2ModKit_new.exe"));

            //delete other (nested) D2ModKit folder.
            try {
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, "D2ModKit"), true);
                // delete .zip
                File.Delete(zipPath);
            }
            catch (Exception) {  }

            // now run our other process to quit this application, and replace the .exe's.
            string batPath = Path.Combine(Environment.CurrentDirectory, "updater.bat");

            // let's always have a fresh batch file.
            if (File.Exists(batPath))
            {
                File.Delete(batPath);
            }

            // Create a file to write to.
            string orig = Path.Combine(Environment.CurrentDirectory, "D2ModKit_new.exe");
            string dest = Path.Combine(Environment.CurrentDirectory, "D2ModKit.exe");
            using (StreamWriter sw = File.CreateText(batPath)) 
            {
                sw.WriteLine("taskkill /f /im \"D2ModKit.exe\"");
                sw.WriteLine("SLEEP 1");
                sw.WriteLine("DEL /Q " + dest);
                sw.WriteLine("MOVE /Y \"d2modkit_new.exe\" \"D2ModKit.exe\"");
                sw.WriteLine("start d2modkit.exe");
                sw.WriteLine("DEL /Q updater.bat");
            }
            //this.Close();
            Process.Start(batPath);
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = progressBar1.Value + (e.ProgressPercentage-progressBar1.Value);
        }
    }
}
