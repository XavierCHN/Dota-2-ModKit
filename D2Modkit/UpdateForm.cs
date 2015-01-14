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
            // delete the nested D2ModKit_temp folder if it exists.
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "D2ModKit_temp")))
            {
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, "D2ModKit_temp"), true);
            }

            // extract it now
            string zipPath = Path.Combine(Environment.CurrentDirectory, "D2ModKit.zip");
            ZipFile.ExtractToDirectory(zipPath, Path.Combine(Environment.CurrentDirectory, "D2ModKit_temp"));
            //TODO: need to fix this. it's extracting the Templates directory, and gives an
            // error if Templates directory already exists.

            // get the new D2ModKit.exe.
            string path = Path.Combine(Environment.CurrentDirectory, "D2ModKit_temp", "D2ModKit.exe");

            // delete D2ModKit_new.exe if it exists.
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "D2ModKit_new.exe")))
            {
                File.Delete(Path.Combine(Environment.CurrentDirectory, "D2ModKit_new.exe"));
            }

            // move the new d2modkit.exe to the main folder, and rename it.
            File.Move(path, Path.Combine(Environment.CurrentDirectory, "D2ModKit_new.exe"));

            //delete other (nested) D2ModKit folder.
            try {
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, "D2ModKit_temp"), true);
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
            Process.Start(batPath);
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = progressBar1.Value + (e.ProgressPercentage-progressBar1.Value);
        }
    }
}
