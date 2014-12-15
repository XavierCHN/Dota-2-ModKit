using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KVLib;
using System.IO;
using System.Diagnostics;

namespace D2ModKit.WikiGeneration
{
    public class Wiki
    {
        private Addon addon;

        public Addon Addon
        {
            get { return addon; }
            set { addon = value; }
        }
        private string lang;

        public string Lang
        {
            get { return lang; }
            set { lang = value; }
        }

        private HeroesPage heroesPage;

        public HeroesPage HeroesPage
        {
            get { return heroesPage; }
            set { heroesPage = value; }
        }

        private ItemsPage itemsPage;

        public ItemsPage ItemsPage
        {
            get { return itemsPage; }
            set { itemsPage = value; }
        }

        private UnitsPage unitsPage;

        public UnitsPage UnitsPage
        {
            get { return unitsPage; }
            set { unitsPage = value; }
        }

        private StartPage startPage;

        public StartPage StartPage
        {
            get { return startPage; }
            set { startPage = value; }
        }

        private IEnumerable<KeyValue> langKV;

        public IEnumerable<KeyValue> LangKV
        {
            get { return langKV; }
            set { langKV = value; }
        }

        private string addonName;

        public string AddonName
        {
            get { return addonName; }
            set { addonName = value; }
        }

        private string languageName;

        public string LanguageName
        {
            get { return languageName; }
            set { languageName = value; }
        }

        private string wikiPath;

        public string WikiPath
        {
            get { return wikiPath; }
            set { wikiPath = value; }
        }

        public Wiki(Addon addon, string lang)
        {
            Addon = addon;
            Lang = lang;

            parseLanguageFile();
            // create a directory structure to store this wiki.
            createDirectories();

            StartPage = new StartPage(this);
            HeroesPage = new HeroesPage(this);
            UnitsPage = new UnitsPage(this);
            ItemsPage = new ItemsPage(this);


        }

        private void createDirectories()
        {
            if (languageName == null || languageName == "")
            {
                Debug.WriteLine("Language not found.");
                return;
            }

            wikiPath = Path.Combine(Environment.CurrentDirectory, Addon.Name + "_Wiki_" + languageName);
            // delete the existing wiki if there is one.
            if (Directory.Exists(wikiPath))
            {
                try
                {
                    Directory.Delete(wikiPath, true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }

            Directory.CreateDirectory(wikiPath);
            Directory.CreateDirectory(Path.Combine(wikiPath, "heroes"));
            Directory.CreateDirectory(Path.Combine(wikiPath, "units"));

        }

        private void parseLanguageFile()
        {
            KeyValue[] addon_lang = KVParser.ParseAllKVRootNodes(File.ReadAllText(Lang));

            if (addon_lang.Count() > 0)
            {
                IEnumerable<KeyValue> rootChildren = addon_lang[0].Children;

                for (int k = 0; k < rootChildren.Count(); k++)
                {
                    KeyValue child = rootChildren.ElementAt(k);
                    if (child.Key == "Language")
                    {
                        // extract language name
                        languageName = child.GetString();
                    }
                    if (child.Key == "Tokens")
                    {
                        if (child.HasChildren)
                        {
                            // store the main addon_language keys and vals.
                            LangKV = child.Children;
                            // extract some basic info while we're here
                            for (int i = 0; i < LangKV.Count(); i++)
                            {
                                KeyValue kv = LangKV.ElementAt(i);
                                string v = kv.GetString();
                                if (kv.Key == "addon_game_name")
                                {
                                    AddonName = v;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
