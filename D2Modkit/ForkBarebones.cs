using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

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
            fork();
        }

        private void fork()
        {
            // we assume we have a valid 'barebones' dir with 'game' and 'content' due to our checks earlier.

            // first move the root dir.
            string rootDir = Path.Combine(Environment.CurrentDirectory, "barebones");
            string newRootDir = rootDir.Replace("barebones", NewAddonName.ToLower());
            Directory.Move(rootDir, newRootDir);

            // next modify subdirectory names.
            string[] dirs = Directory.GetDirectories(newRootDir, "*barebones*", SearchOption.AllDirectories);
            for (int i = 0; i < dirs.Count(); i++)
            {
                string newDir = dirs[i].Replace("barebones", NewAddonName.ToLower());
                Directory.Move(dirs[i], newDir);
            }

            // now modify the files.
            List<string> files = getFiles(newRootDir, "*.txt;*.lua");
            for (int i = 0; i < files.Count(); i++)
            {
                // let's change the filename first, before modifying the contents.
                string newFileName = files[i].Replace("barebones", NewAddonName.ToLower());
                newFileName = newFileName.Replace("reflex", NewAddonName.ToLower());

                File.Move(files[i], newFileName);
                files[i] = newFileName;

                string[] lines = File.ReadAllLines(files[i]);
                for (int j = 0; j < lines.Count(); j++)
                {
                    string l = lines[j];
                    l = l.Replace("barebones", NewAddonName.ToLower());
                    l = l.Replace("BAREBONES", NewAddonName.ToUpper());
                    l = l.Replace("Barebones", NewAddonName);
                    l = l.Replace("BareBones", NewAddonName);
                    l = l.Replace("reflex", NewAddonName.ToLower());
                    l = l.Replace("Reflex", NewAddonName);
                    l = l.Replace("REFLEX", NewAddonName.ToUpper());
                    if (l.Contains("GameMode") && !l.Contains("GetGameModeEntity"))
                    {
                        l = l.Replace("GameMode", NewAddonName);
                    }
                    lines[j] = l;
                }
                if (files[i].EndsWith(".lua"))
                {
                    File.WriteAllLines(files[i], lines);
                }
                else
                {
                    File.WriteAllLines(files[i], lines, System.Text.Encoding.Unicode);
                }
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