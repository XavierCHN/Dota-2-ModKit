using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KVLib;

namespace D2ModKit
{
    public class Template
    {
        public class Entry
        {
            // this is the custom name for the template entry.
            private string key;

            public string Key
            {
                get { return key; }
                set { key = value; }
            }
            /*private string originalName;

            public string OriginalName
            {
                get { return originalName; }
                set { originalName = value; }
            }*/

            private string val;

            public string Val
            {
                get { return val; }
                set { val = value; }
            }

            public Entry(string key, string val)
            {
                this.key = key;
                this.val = val;
            }
        }

        private string name;
        private string path;
        private string templatesDir = Path.Combine(Environment.CurrentDirectory, "Templates");
        private Dictionary<string, string> metadata = new Dictionary<string, string>();
        // this maps a string keys to template entries.
        // ex. "Linear Projectile" maps to a template entry.
        private Dictionary<string, Entry> map = new Dictionary<string, Entry>();
        // TODO: Data structure that maps original names to keys.

        public Dictionary<string, Entry> Map
        {
            get { return map; }
            set { map = value; }
        }

        // expects _name to be like 'ability', 'item', 'hero', etc
        public Template(string _name)
        {
            name = _name + "_templates.txt";
            path = Path.Combine(templatesDir, name);
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            load();
        }

        private bool load()
        {
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException)
            {
                // the user must have this file opened.
                Debug.WriteLine("IOException");
                return false;
            }

            map.Clear();

            Entry e = null;
            string key = "";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("//+Template"))
                {
                    // save the current template.
                    if (e != null)
                    {
                        map.Add(key, e);
                        e = null;
                    }
                    continue;
                }

                // Check if we're at the end of the file.
                if (i == lines.Length - 1)
                {
                    e.Val += lines[i];
                    // save the current template.
                    if (e != null)
                    {
                        map.Add(key, e);
                    }
                    return true;
                }

                // check if the entry has been defined yet.
                if (e == null)
                {
                    e = new Entry("", "");
                    e.Key = lines[i];
                    e.Val += lines[i] + "\n";
                    e.Key = e.Key.Replace("\"", "");
                    e.Key = e.Key.Trim();
                    key = e.Key;
                    continue;
                }
                e.Val += lines[i] + "\n";
            }
            return true;
        }


        internal void write()
        {
            string str = "";
            for (int i = 0; i < map.Count; i++)
            {
                KeyValuePair<string, Entry> kv = map.ElementAt(i);
                string customName = kv.Key;
                Entry e = kv.Value;

                str += "//+Template\n";
                //str += "//+CustomName= " + customName + "\n";
                str += e.Val + "\n";
            }
            File.WriteAllText(path, str);
        }
    }
}
