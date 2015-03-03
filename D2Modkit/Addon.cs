using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using KVLib;
using System.Net;
using System.Threading;

namespace D2ModKit
{
    public class Addon
    {
        private string name;
        private string contentPath;
        private string gamePath;
        private string npcPath;
        private string copyPath;
        private string addonEnglishPath;
        private string abilitiesCustomPath;
        private string itemCustomPath;
        private string unitsCustomPath;
        private string heroesCustomPath;
        private string relativeParticlePath;

        private List<AbilityEntry> abilityEntries = new List<AbilityEntry>();
        private List<AbilityEntry> itemEntries = new List<AbilityEntry>();
        private List<UnitEntry> unitEntries = new List<UnitEntry>();
        private List<HeroEntry> heroesEntries = new List<HeroEntry>();
        // HashSet ensures no duplicate modifier entries.
        private HashSet<string> modifierItemKeys = new HashSet<string>();
        private HashSet<string> modifierAbilityKeys = new HashSet<string>();
        private HashSet<string> hiddenModifierKeys = new HashSet<string>();

        // for storing the addon_language kvs
        private HashSet<string> alreadyHasKeys = new HashSet<string>();

        public List<AbilityEntry> AbilityEntries
        {
            get { return abilityEntries; }
            set { abilityEntries = value; }
        }
        private List<string> particlePaths;

        public class KVFileToCombine
        {
            public string path;
            public bool activated;
            public string name;
            public KVFileToCombine(string name)
            {
                this.name = name;
            }
        }

        private List<KVFileToCombine> kvFilesToCombine;

        public List<KVFileToCombine> KVFilesToCombine
        {
            get { return kvFilesToCombine; }
            set { kvFilesToCombine = value; }
        }

        public class ModdingLibrary
        {
            public string name;
            public string localLink;
            public object webVers;
            public object localVers;

            public ModdingLibrary(string name)
            {
                // TODO: Complete member initialization
                this.name = name;
            }

            public string webLink { get; set; }

            public string version { get; set; }

            /*internal bool isOutOfDate()
            {
                ThreadStart childref = new ThreadStart(IsOutOfDate);
                Thread thread = new Thread(childref);
                thread.Start();
            }

            private void IsOutOfDate()
            {
                string webVers = getWebVers();
                string localVers = getLocalVers();
                if (webVers != localVers)
                {
                    return true;
                }
                return false;
            }*/

            internal string getVers(string[] lines)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (i > 9)
                    {
                        break;
                    }
                    string trimmed = line.Trim();
                    if (trimmed.StartsWith("Version: "))
                    {
                        return trimmed.Substring(9);
                    }
                }
                return "";
            }

            internal string getWebVers()
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        Byte[] responseBytes = client.DownloadData(webLink);
                        string source = System.Text.Encoding.ASCII.GetString(responseBytes);
                        return getVers(source.Split('\n'));
                    }
                    catch (Exception) { }
                }
                return "";
            }

            internal string getLocalVers()
            {
                string[] lines = File.ReadAllLines(localLink);
                return getVers(lines);
            }
        }

        public List<ModdingLibrary> moddingLibraries = new List<ModdingLibrary>();

        private void getMorePaths()
        {
            AddonEnglishPath = Path.Combine(GamePath, "resource", "addon_english.txt");
            AbilitiesCustomPath = Path.Combine(GamePath, "scripts", "npc", "npc_abilities_custom.txt");
            ItemsCustomPath = Path.Combine(GamePath, "scripts", "npc", "npc_items_custom.txt");
            UnitsCustomPath = Path.Combine(GamePath, "scripts", "npc", "npc_units_custom.txt");
            HeroesCustomPath = Path.Combine(GamePath, "scripts", "npc", "npc_heroes_custom.txt");
        }

        public Addon(string _gamePath)
        {
            gamePath = _gamePath;
            name = gamePath.Substring(gamePath.LastIndexOf('\\') + 1);
            getMorePaths();
            kvFilesToCombine = new List<KVFileToCombine>();
        }

        public string ItemsCustomPath
        {
            get { return itemCustomPath; }
            set { itemCustomPath = value; }
        }

        public string UnitsCustomPath
        {
            get { return unitsCustomPath; }
            set { unitsCustomPath = value; }
        }

        public string HeroesCustomPath
        {
            get { return heroesCustomPath; }
            set { heroesCustomPath = value; }
        }

        public string AddonEnglishPath
        {
            get { return addonEnglishPath; }
            set { addonEnglishPath = value; }
        }

        public string AbilitiesCustomPath
        {
            get { return abilitiesCustomPath; }
            set { abilitiesCustomPath = value; }
        }

        public string RelativeParticlePath
        {
            get { return relativeParticlePath; }
            set { relativeParticlePath = value; }
        }

        public string CopyPath
        {
            get { return copyPath; }
            set { copyPath = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ContentPath
        {
            get { return contentPath; }
            set { contentPath = value; }
        }

        public string GamePath
        {
            get { return gamePath; }
            set { gamePath = value; }
        }

        public string NPCPath
        {
            get { return npcPath; }
            set { npcPath = value; }
        }

        public List<string> ParticlePaths
        {
            get { return particlePaths; }
            set { particlePaths = value; }
        }

        public KeyValue KVData { get; set; }

		public string gds_modID = "";
		public string gds_rank = "";
		public string gds_link = "";
		public string steam_link = "";
		public string workshop_id = "";
		public bool create_note0_lore = false;

        public void deserializePreferences()
        {
            KeyValue pref = null;
            KeyValue kv_files = null;
            KeyValue libraries = null;

			/*KeyValue gds_modID = null;
			KeyValue gds_rank = null;
			KeyValue gds_link = null;
			KeyValue steam_link = null;
			KeyValue workshop_id = null;*/

			if (KVData != null) {
				foreach (KeyValue kv in KVData.Children) {
					if (kv.Key == "preferences") {
						pref = kv;
					} else if (kv.Key == "libraries") {
						libraries = kv;
					} else if (kv.Key == "kv_files") {
						kv_files = kv;
					} else if (kv.Key == "gds_modID") {
						gds_modID = kv.Children.ElementAt(0).Key;
					} else if (kv.Key == "gds_rank") {
						gds_rank = kv.Children.ElementAt(0).Key;
					} else if (kv.Key == "gds_link") {
						gds_link = kv.Children.ElementAt(0).Key;
					} else if (kv.Key == "steam_link") {
						steam_link = kv.Children.ElementAt(0).Key;
					} else if (kv.Key == "workshop_id") {
						workshop_id = kv.Children.ElementAt(0).Key;
					}
				}

				foreach (KeyValue kv in pref.Children) {
					if (kv.Key == "create_note0_lore") {
						if (kv.Children.ElementAt(0).Key == "true") {
							create_note0_lore = true;
						}
					}
				}
			}

            if (kv_files != null)
            {
                foreach (KeyValue kv in kv_files.Children)
                {
                    KVFileToCombine cf = new KVFileToCombine(kv.Key);
                    foreach (KeyValue kv2 in kv.Children)
                    {
                        if (kv2.Key == "path")
                        {
                            cf.path = kv2.Children.ElementAt(0).Key;
                        }
                        if (kv2.Key == "activated")
                        {
                            if (kv2.Children.ElementAt(0).Key == "true")
                            {
                                cf.activated = true;
                            }
                            else
                            {
                                cf.activated = false;
                            }
                        }
                    }
                    kvFilesToCombine.Add(cf);
                }
			} else { // initiate to defaults.
                kv_files = new KeyValue("kv_files");
                string[] npcFiles = { "Heroes", "Units", "Items", "Abilities" };
				foreach (string s in npcFiles) {
					KVFileToCombine cf = new KVFileToCombine(s);
					cf.path = Path.Combine(gamePath, "scripts", "npc", "npc_" + s.ToLower() + "_custom.txt");
					cf.activated = true;
					kvFilesToCombine.Add(cf);
				}
			}
            if (libraries != null)
            {
                foreach (KeyValue kv in libraries.Children)
                {
                    string name = kv.Key;
                    ModdingLibrary ml = new ModdingLibrary(name);
                    foreach (KeyValue kv2 in kv.Children)
                    {
                        if (kv2.Key == "path")
                        {
                            ml.webLink = kv2.Children.ElementAt(0).Key;
                        }
                        else if (kv2.Key == "version")
                        {
                            ml.version = kv2.Children.ElementAt(0).Key;
                        }
                    }
                    moddingLibraries.Add(ml);
                }
            } // no defaults for libs
        }

		public KeyValue serializePreferences() {
			KeyValue master = new KeyValue(name.ToLower());
			KeyValue preferences = new KeyValue("preferences");
			KeyValue create_note0_lore = new KeyValue("create_note0_lore");
			KeyValue kv_files = new KeyValue("kv_files");
			KeyValue libraries = new KeyValue("libraries");

			foreach (KVFileToCombine kvFileToCombine in kvFilesToCombine) {
				KeyValue kvFile = new KeyValue(kvFileToCombine.name);
				KeyValue activated = new KeyValue("activated");
				activated.AddChild(new KeyValue(kvFileToCombine.activated.ToString().ToLower()));
				KeyValue path = new KeyValue("path");
				path.AddChild(new KeyValue(kvFileToCombine.path));
				kvFile.AddChild(activated);
				kvFile.AddChild(path);
				kv_files.AddChild(kvFile);
			}

			foreach (ModdingLibrary ml in moddingLibraries) {
				KeyValue lib = new KeyValue(ml.name);
				KeyValue path = new KeyValue("path");
				KeyValue version = new KeyValue("version");
				path.AddChild(new KeyValue(ml.webLink));
				version.AddChild(new KeyValue(ml.version));
				lib.AddChild(path);
				lib.AddChild(version);
				libraries.AddChild(lib);

			}

			if (this.gds_modID != "") {
				KeyValue gds_modID = new KeyValue("gds_modID");
				gds_modID.AddChild(new KeyValue(this.gds_modID));
				master.AddChild(gds_modID);
			}
			if (this.gds_rank != "") {
				KeyValue gds_rank = new KeyValue("gds_rank");
				gds_rank.AddChild(new KeyValue(this.gds_rank));
				master.AddChild(gds_rank);
			}
			if (this.gds_link != "") {
				KeyValue gds_link = new KeyValue("gds_link");
				gds_link.AddChild(new KeyValue(this.gds_link));
				master.AddChild(gds_link);
			}
			if (this.steam_link != "") {
				KeyValue steam_link = new KeyValue("steam_link");
				steam_link.AddChild(new KeyValue(this.steam_link));
				master.AddChild(steam_link);
			}
			if (this.workshop_id != "") {
				KeyValue workshop_id = new KeyValue("workshop_id");
				workshop_id.AddChild(new KeyValue(this.workshop_id));
				master.AddChild(workshop_id);
			}

			if (this.create_note0_lore) {
				create_note0_lore.AddChild(new KeyValue("true"));
			} else {
				create_note0_lore.AddChild(new KeyValue("false"));
			}

			preferences.AddChild(create_note0_lore);
			master.AddChild(kv_files);
			master.AddChild(preferences);
			master.AddChild(libraries);
			return master;
		}

        public void getCurrentAddonEnglish()
        {
            // Parse addon_english.txt KV
            KeyValue[] addonEnglishKeyVals = KVParser.KV1.ParseAll(File.ReadAllText(addonEnglishPath));
        }

        public void getAbilityTooltips(bool items)
        {
            if (items)
            {
                itemEntries.Clear();
                modifierItemKeys.Clear();

                if (!File.Exists(itemCustomPath))
                {
                    return;
                }
            }
            else
            {
                AbilityEntries.Clear();
                modifierAbilityKeys.Clear();

                if (!File.Exists(abilitiesCustomPath))
                {
                    return;
                }
            }

            // Parse abilities_custom.txt KV

            KeyValue[] abilitiesCustomKeyVals = KVParser.KV1.ParseAll(File.ReadAllText(AbilitiesCustomPath));
            if (items)
            {
                abilitiesCustomKeyVals = KVParser.KV1.ParseAll(File.ReadAllText(ItemsCustomPath));
            }

            IEnumerable<KeyValue> abilityNames = null;
            try
            {
                abilityNames = abilitiesCustomKeyVals[0].Children;
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }

            for (int i = 0; i < abilityNames.Count(); i++)
            {
                KeyValue ability = abilityNames.ElementAt(i);
                if (ability.Key == "Version")
                {
                    continue;
                }
                if (ability.HasChildren)
                {
                    // added will remain false if ability has no AbilitySpecials.
                    bool added = false;

                    IEnumerable<KeyValue> children = ability.Children;
                    // Find the abilityspecial stuff.
                    for (int j = 0; j < children.Count(); j++)
                    {
                        KeyValue child = children.ElementAt(j);
                        if (child.Key == "AbilitySpecial" || child.Key == "Modifiers")
                        {
                            bool modifiers = false;
                            if (child.Key == "Modifiers")
                            {
                                modifiers = true;
                            }

                            // We have the AbilitySpecial now. See if there is actually anything in it.
                            if (child.HasChildren)
                            {
                                List<string> kvs = new List<string>();
                                IEnumerable<KeyValue> children2 = child.Children;
                                for (int k = 0; k < children2.Count(); k++)
                                {
                                    KeyValue child2 = children2.ElementAt(k);
                                    bool isHidden = false;
                                    if (child2.HasChildren)
                                    {
                                        IEnumerable<KeyValue> children3 = child2.Children;
                                        for (int l = 0; l < children3.Count(); l++)
                                        {
                                            KeyValue child3 = children3.ElementAt(l);
                                            if (modifiers)
                                            {
                                                if (child3.Key == "IsHidden")
                                                {
                                                    // Ensure it's actually hidden.
                                                    if (child3.GetString() == "1")
                                                    {
                                                        isHidden = true;
                                                    }
                                                }
                                            }
                                            else // we have a modifier, not ability.
                                            {
                                                // Map item modName to its item specials.
                                                if (child3.Key != "var_type")
                                                {
                                                    kvs.Add(child3.Key);
                                                }
                                            }
                                        }
                                    }
                                    // we're done going through all the rootChildren of this ability/modifier.
                                    if (modifiers)
                                    {
                                        if (!isHidden)
                                        {
                                            if (items)
                                            {
                                                if (!modifierItemKeys.Contains(child2.Key))
                                                {
                                                    modifierItemKeys.Add(child2.Key);
                                                }
                                            }
                                            else
                                            {
                                                if (!modifierAbilityKeys.Contains(child2.Key))
                                                {
                                                    modifierAbilityKeys.Add(child2.Key);
                                                }
                                            }
                                            added = true;
                                        }
                                    }
                                }
                                if (!modifiers)
                                {
                                    if (items)
                                    {
                                        itemEntries.Add(new AbilityEntry(this, ability.Key, kvs));
                                    }
                                    else
                                    {
                                        abilityEntries.Add(new AbilityEntry(this, ability.Key, kvs));
                                    }
                                    added = true;
                                }
                            }
                        }
                    }
                    // this ability has no AbilitySpecials.
                    if (!added)
                    {
                        if (items)
                        {
                            itemEntries.Add(new AbilityEntry(this, ability.Key, null));
                        }
                        else
                        {
                            abilityEntries.Add(new AbilityEntry(this, ability.Key, null));
                        }
                    }
                }
            }
        }

        public void getHeroesTooltips()
        {
            heroesEntries.Clear();

            if (!File.Exists(heroesCustomPath))
            {
                return;
            }

            KeyValue[] heroesKeyVals = KVParser.KV1.ParseAll(File.ReadAllText(heroesCustomPath));
            IEnumerable<KeyValue> children = heroesKeyVals[0].Children;
            for (int i = 0; i < children.Count(); i++)
            {
                KeyValue child = children.ElementAt(i);
                if (child.HasChildren)
                {
                    IEnumerable<KeyValue> children2 = child.Children;
                    for (int j = 0; j < children2.Count(); j++)
                    {
                        KeyValue child2 = children2.ElementAt(j);
                        if (child2.Key == "override_hero")
                        {
                            heroesEntries.Add(new HeroEntry(child2.GetString()));
                        }
                    }
                }
            }
        }

        public void getUnitTooltips()
        {
            unitEntries.Clear();

            if (!File.Exists(unitsCustomPath))
            {
                return;
            }

            KeyValue[] unitsKeyVals = KVParser.KV1.ParseAll(File.ReadAllText(unitsCustomPath));
            IEnumerable<KeyValue> children = unitsKeyVals[0].Children;
            for (int i = 0; i < children.Count(); i++)
            {
                string unit = children.ElementAt(i).Key;
                if (unit != "Version")
                {
                    unitEntries.Add(new UnitEntry(unit));
                }
            }
        }

        public void writeTooltips()
        {
            List<string> langFiles = getAddonLangPaths();

            for (int l = 0; l < langFiles.Count(); l++)
            {
                string file = langFiles.ElementAt(l);

                string thisLang = file.Substring(file.LastIndexOf('\\') + 1);
                string thisLangCopy = thisLang;
                thisLang = thisLang.Substring(thisLang.LastIndexOf('_') + 1);
                string outputPath = Path.Combine(GamePath, "resource", "tooltips_" + thisLang);

                alreadyHasKeys.Clear();

                KeyValue[] addon_lang = KVParser.KV1.ParseAll(File.ReadAllText(file));

                if (addon_lang.Count() > 0)
                {
                    IEnumerable<KeyValue> rootChildren = addon_lang[0].Children;

                    for (int k = 0; k < rootChildren.Count(); k++)
                    {
                        KeyValue child = rootChildren.ElementAt(k);
                        if (child.HasChildren)
                        {
                            IEnumerable<KeyValue> children2 = child.Children;
                            for (int j = 0; j < children2.Count(); j++)
                            {
                                KeyValue child2 = children2.ElementAt(j);
                                alreadyHasKeys.Add(child2.Key.ToLower());
                            }
                        }
                    }
                }

				StringBuilder content = new StringBuilder();

                // WriteAllText will clear the contents of this file first
                string header =
                    "// **********************************************************************************************************************\n" +
                    "// This file contains generated tooltips created from the files in the scripts/npc directory of this mod.\n" +
                    "// It does not contain tooltips already defined in " + thisLangCopy +
                    ", nor modifiers with the property \"IsHidden\" \"1\".\n" +
                    "// **********************************************************************************************************************\n";
				content.Append(header);


                string head1 = "\n\t\t// ******************** HEROES ********************\n";
				content.Append(head1);
                for (int i = 0; i < heroesEntries.Count(); i++)
                {
                    HeroEntry hero = heroesEntries.ElementAt(i);
                    if (!alreadyHasKeys.Contains(hero.Name.Key.ToLower()))
                    {
						content.Append(hero.ToString());
                    }
                }

                string head2 = "\n\t\t// ******************** UNITS ********************\n";
				content.Append(head2);
                for (int i = 0; i < unitEntries.Count(); i++)
                {
                    UnitEntry unit = unitEntries.ElementAt(i);
                    if (!alreadyHasKeys.Contains(unit.Name.Key.ToLower()))
                    {
						content.Append(unit.ToString());
                    }
                }

                string head3 = "\n\t\t// ******************** ABILITY MODIFIERS ********************\n";
				content.Append(head3);
                for (int i = 0; i < modifierAbilityKeys.Count(); i++)
                {
                    ModifierEntry mod = new ModifierEntry(modifierAbilityKeys.ElementAt(i));
                    if (!alreadyHasKeys.Contains(mod.Name.Key.ToLower()))
                    {
						content.Append(mod + "\n");
                    }
                }

                string head6 = "\n\t\t// ******************** ITEM MODIFIERS ********************\n";
				content.Append(head6);
                for (int i = 0; i < modifierItemKeys.Count(); i++)
                {
                    //ModifierEntry mod = modifierItemEntries.ElementAt(i);
                    ModifierEntry mod = new ModifierEntry(modifierItemKeys.ElementAt(i));
                    if (!alreadyHasKeys.Contains(mod.Name.Key.ToLower()))
                    {
						content.Append(mod + "\n");
                    }
                }

                string head4 = "\n\t\t// ******************** ABILITIES ********************\n";
				content.Append(head4);
                for (int i = 0; i < abilityEntries.Count(); i++)
                {
                    AbilityEntry abil = abilityEntries.ElementAt(i);
                    if (!alreadyHasKeys.Contains(abil.Name.Key.ToLower()))
                    {
						content.Append(abil + "\n");
                    }
                    else
                    {
                        // the addon_language already has this ability. but let's check
                        // if there are any new AbilitySpecials.
						bool missingAbilSpecials = false;
                        foreach (Pair p in abil.AbilitySpecials)
                        {
                            if (!alreadyHasKeys.Contains(p.Key.ToLower()))
                            {
                                // the addon_language doesn't contain this abil special.
								content.Append(p.ToString());
								missingAbilSpecials = true;
                            }
                        }
						if (missingAbilSpecials) {
							content.Append("\n");	
						}
                    }
                }

                string head5 = "\n\t\t// ******************** ITEMS ********************\n";
				content.Append(head5);
                for (int i = 0; i < itemEntries.Count(); i++)
                {
                    AbilityEntry item = itemEntries.ElementAt(i);
                    if (!alreadyHasKeys.Contains(item.Name.Key.ToLower()))
                    {
						content.Append(item + "\n");
                    }
                    else
                    {
                        // the addon_language already has this ability. but let's check
                        // if there are any new AbilitySpecials.
						bool missingAbilSpecials = false;
                        foreach (Pair p in item.AbilitySpecials)
                        {
                            if (!alreadyHasKeys.Contains(p.Key.ToLower()))
                            {
                                // the addon_language doesn't contain this abil special.
								content.Append(p.ToString());
								missingAbilSpecials = true;
                            }
                        }
						if (missingAbilSpecials) {
							content.Append("\n");
						}
                    }
                }

                // open the tooltips.txt in a text editor
				File.WriteAllText(outputPath, content.ToString(), Encoding.Unicode);
                Process.Start(outputPath);
            }
        }

        public List<string> getAddonLangPaths()
        {
            string[] resourceFiles = Directory.GetFiles(Path.Combine(GamePath, "resource"));
            List<string> langFiles = new List<string>();

            // only take the addon_language files
            for (int i = 0; i < resourceFiles.Count(); i++)
            {
                if (resourceFiles[i].Contains("addon_") && resourceFiles[i].EndsWith(".txt"))
                {
                    langFiles.Add(resourceFiles[i]);
                }
            }
            return langFiles;
        }

        internal bool hasLibrary(string p)
        {
            foreach (ModdingLibrary ml in moddingLibraries)
            {
                if (ml.name == p)
                {
                    return true;
                }
            }
            return false;
        }
    }
}