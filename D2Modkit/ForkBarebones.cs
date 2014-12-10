using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2ModKit
{
    public class ForkBarebones
    {
        private string newAddonName;

        public string NewAddonName
        {
            get { return newAddonName; }
            set { newAddonName = value; }
        }

        private string temp;

        public string Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        public ForkBarebones(string newAddonName)
        {
            NewAddonName = newAddonName;
            unzipBarebones();
        }

        private void unzipBarebones()
        {
            string zipPath = Path.Combine(Environment.CurrentDirectory, "barebones.zip");
            if (!File.Exists(zipPath))
            {
                return;
            }
            string extractPath = Path.Combine(Environment.CurrentDirectory);
            try
            {
                ZipFile.ExtractToDirectory(zipPath, Environment.CurrentDirectory);
            }
            catch (IOException) { }

            string unZippedPath = Path.Combine(Environment.CurrentDirectory, "barebones-source2");
            if (!Directory.Exists(unZippedPath))
            {
                return;
            }

            // delete the .zip since we don't need it anymore.
            try
            {
                File.Delete(zipPath);
            }
            catch (IOException) { }

            rewriteBarebones(unZippedPath);
        }

        private void rewriteBarebones(string unZippedPath)
        {
            Debug.WriteLine("Rewriting barebones.");

            // first move the root directory.
            string newRoot = unZippedPath.Replace("barebones-source2", NewAddonName.ToLower());
            if (Directory.Exists(newRoot))
            {
                try
                {
                    Directory.Delete(newRoot);
                }
                catch (IOException) { }
            }

            Temp = newRoot;
            try
            {
                Directory.Move(unZippedPath, newRoot);
            }
            catch (IOException) { }

            // next modify subdirectory names.
            string[] dirs = Directory.GetDirectories(newRoot, "*barebones*", SearchOption.AllDirectories);
            for (int i = 0; i < dirs.Count(); i++)
            {
                string newDir = dirs[i].Replace("barebones", NewAddonName.ToLower());
                Directory.Move(dirs[i], newDir);
            }

            // now modify the files.
            List<string> files = getFiles(newRoot, "*.txt;*.lua");
            for (int i = 0; i < files.Count(); i++)
            {
                // let's change the file modName first, before modifying the contents.
                string newFileName = files[i].Replace("barebones", NewAddonName.ToLower());
                newFileName = newFileName.Replace("reflex", NewAddonName.ToLower());

                File.Move(files[i], newFileName);
                files[i] = newFileName;

                string[] lines = File.ReadAllLines(files[i]);
                for (int j = 0; j < lines.Count(); j++)
                {
                    lines[j] = lines[j].Replace("barebones", NewAddonName.ToLower());
                    lines[j] = lines[j].Replace("BAREBONES", NewAddonName.ToUpper());
                    lines[j] = lines[j].Replace("Barebones", NewAddonName);
                    lines[j] = lines[j].Replace("BareBones", NewAddonName);
                    lines[j] = lines[j].Replace("GameMode", NewAddonName + "GameMode");
                }
                File.WriteAllLines(files[i], lines);
            }
        }

        public List<string> getFiles(string directory, string searchPattern)
        {
            List<string> allFiles = new List<string>();
            string[] exts = searchPattern.Split(';');
            for (int i = 0; i < exts.Count(); i++)
            {
                string[] foundFiles = Directory.GetFiles(directory, exts[i], SearchOption.AllDirectories);
                for (int j = 0; j < foundFiles.Count(); j++)
                {
                    allFiles.Add(foundFiles[j]);
                }

            }
            return allFiles;
        }

    }
}
