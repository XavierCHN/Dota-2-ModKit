using Dota2ModKit.Properties;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using MetroFramework;
using System;
using KVLib;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace Dota2ModKit
{
	public class Addon {
		public string gamePath;
		public string contentPath;
		public string name;
		public bool hasContentPath = true;
		public int libraryPos;
		internal int libraryPage;
		internal int workshopID;
		internal Image image;
		internal bool doesntHaveThumbnail;
		internal MetroColorStyle tileColor = MetroColorStyle.Green;

		// for generate addon_lang files
		HashSet<string> abilityModifierNames = new HashSet<string>();
		HashSet<string> itemModifierNames = new HashSet<string>();
		List<AbilityEntry> abilityEntries = new List<AbilityEntry>();
		List<AbilityEntry> itemEntries = new List<AbilityEntry>();
		List<UnitEntry> unitEntries = new List<UnitEntry>();
		List<HeroEntry> heroEntries = new List<HeroEntry>();
		HashSet<string> alreadyHasKeys = new HashSet<string>();

		public Addon(string gamePath) {
			this.gamePath = gamePath;

			// extract other info from the gamePath
			name = gamePath.Substring(gamePath.LastIndexOf('\\')+1);
			Debug.WriteLine("New Addon detected: " + name);

			string dotaDir = Settings.Default.DotaDir;

			contentPath = Path.Combine(dotaDir, "content", "dota_addons", name);

			if (!Directory.Exists(contentPath)) {
				try {
					Directory.CreateDirectory(contentPath);
				} catch (Exception) {
					Debug.WriteLine("Couldn't auto-create content path for " + name);
					hasContentPath = false;
				}

			}

		}

		internal bool generateAddonLangs(MainForm mainForm) {
			abilityModifierNames.Clear();
			itemModifierNames.Clear();
			abilityEntries.Clear();
			itemEntries.Clear();
			unitEntries.Clear();
			heroEntries.Clear();
			alreadyHasKeys.Clear();

			try {
				// these functions populate the data structures with the tooltips before writing to the addon_lang file.
				// items
				generateAbilityTooltips(true);
				// abils
				generateAbilityTooltips(false);
				generateUnitTooltips();
				generateHeroTooltips();

				writeTooltips();
			} catch(Exception ex) {
				MetroMessageBox.Show(mainForm, ex.Message,
				ex.ToString(), //"Error while generating tooltips"
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private void writeTooltips() {
			foreach (string path in getAddonLangPaths()) {

				alreadyHasKeys.Clear();

				string thisLang = path.Substring(path.LastIndexOf('\\') + 1);

				string thisLangCopy = thisLang;
				thisLang = thisLang.Substring(thisLang.LastIndexOf('_') + 1);

				string outputPath = Path.Combine(contentPath, "tooltips_" + thisLang);

				KeyValue kv = KVParser.KV1.ParseAll(File.ReadAllText(path, Encoding.Unicode))[0];

				foreach (KeyValue kv2 in kv.Children) {
					if (kv2.Key == "Tokens") {
						foreach (KeyValue kv3 in kv2.Children) {
							alreadyHasKeys.Add(kv3.Key.ToLowerInvariant());
						}
					}
				}

				StringBuilder content = new StringBuilder();

				string head0 =
				"\t\t// DOTA 2 MODKIT GENERATED TOOLTIPS FOR: " + this.name + "\n" +
				"\t\t// Keys already defined in " + thisLangCopy + " are not listed, nor are Modifiers with the property \"IsHidden\" \"1\".\n";
				content.Append(head0);

				string head1 = "\n\t\t// ******************** HEROES ********************\n";
				content.Append(head1);
				foreach (HeroEntry he in heroEntries) {
					if (!alreadyHasKeys.Contains(he.name.key.ToLowerInvariant())) {
						content.Append(he);
					}
				}

				string head2 = "\n\t\t// ******************** UNITS ********************\n";
				content.Append(head2);
				foreach (UnitEntry ue in unitEntries) {
					if (!alreadyHasKeys.Contains(ue.name.key.ToLowerInvariant())) {
						content.Append(ue);
					}
				}

				string head3 = "\n\t\t// ******************** ABILITY MODIFIERS ********************\n";
				content.Append(head3);
				foreach (string amn in abilityModifierNames) {
					ModifierEntry me = new ModifierEntry(amn);
					if (!alreadyHasKeys.Contains(me.name.key.ToLowerInvariant())) {
						content.Append(me + "\n");
					}
				}

				string head4 = "\n\t\t// ******************** ITEM MODIFIERS ********************\n";
				content.Append(head4);
				foreach (string imn in itemModifierNames) {
					ModifierEntry me = new ModifierEntry(imn);
					if (!alreadyHasKeys.Contains(me.name.key.ToLowerInvariant())) {
						content.Append(me + "\n");
					}
				}

				string head5 = "\n\t\t// ******************** ABILITIES ********************\n";
				content.Append(head5);
				foreach (AbilityEntry ae in abilityEntries) {
					if (!alreadyHasKeys.Contains(ae.name.key.ToLowerInvariant())) {
						content.Append(ae + "\n");
					} else {
						// the addon_language already has this ability. but let's check
						// if there are any new AbilitySpecials.
						bool missingAbilSpecials = false;
						foreach (Pair p in ae.abilitySpecials) {
							if (!alreadyHasKeys.Contains(p.key.ToLowerInvariant())) {
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

				string head6 = "\n\t\t// ******************** ITEMS ********************\n";
				content.Append(head6);
				foreach (AbilityEntry ae in itemEntries) {
					if (!alreadyHasKeys.Contains(ae.name.key.ToLowerInvariant())) {
						content.Append(ae + "\n");
					} else {
						// the addon_language already has this ability. but let's check
						// if there are any new AbilitySpecials.
						bool missingAbilSpecials = false;
						foreach (Pair p in ae.abilitySpecials) {
							if (!alreadyHasKeys.Contains(p.key.ToLowerInvariant())) {
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

				File.WriteAllText(outputPath, content.ToString(), Encoding.Unicode);
				Process.Start(outputPath);
			}


		}

		private List<string> getAddonLangPaths() {
			string[] resourceFiles = Directory.GetFiles(Path.Combine(gamePath, "resource"));
			List<string> langFiles = new List<string>();

			// only take the addon_language files
			for (int i = 0; i < resourceFiles.Length; i++) {
				string resourceFile = resourceFiles[i];
				if (resourceFile.Contains("addon_") && resourceFile.EndsWith(".txt") && !resourceFile.EndsWith("utf8.txt")) {
					langFiles.Add(resourceFile);
				}
			}
			return langFiles;
		}

		#region private generate tooltip functions

		private void generateHeroTooltips() {
			string path = Path.Combine(gamePath, "scripts", "npc", "npc_heroes_custom.txt");

			if (!File.Exists(path)) {
				return;
			}

			try {
				KeyValue kvs = kvs = KVParser.KV1.ParseAll(File.ReadAllText(path))[0];

				foreach (KeyValue kv in kvs.Children) {
					if (kv.Key == "Version") {
						continue;
					}

					string name = kv.Key;

					foreach (KeyValue kv2 in kv.Children) {
						if (kv2.Key == "override_hero") {
							heroEntries.Add(new HeroEntry(kv2.GetString(), name));
							break;
						}

					}

					unitEntries.Add(new UnitEntry(kv.Key));
				}

			} catch (Exception e) {
				Debug.WriteLine("Error: " + e.StackTrace);
				/*MetroMessageBox.Show(this, 
					"Error parsing npc_abilities_custom.txt: " + e.StackTrace, 
					"", 
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);*/
			}

		}

		private void generateUnitTooltips() {
			string path = Path.Combine(gamePath, "scripts", "npc", "npc_units_custom.txt");

			if (!File.Exists(path)) {
				return;
			}

			try {
				KeyValue kvs = kvs = KVParser.KV1.ParseAll(File.ReadAllText(path))[0];

				foreach (KeyValue kv in kvs.Children) {
					if (kv.Key == "Version") {
						continue;
					}
					unitEntries.Add(new UnitEntry(kv.Key));
				}

			} catch (Exception e) {
				Debug.WriteLine("Error: " + e.StackTrace);
				/*MetroMessageBox.Show(this, 
					"Error parsing npc_abilities_custom.txt: " + e.StackTrace, 
					"", 
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);*/
			}


		}

		private void generateAbilityTooltips(bool item) {
			string path = Path.Combine(gamePath, "scripts", "npc", "npc_abilities_custom.txt");

			if (item) {
				path = Path.Combine(gamePath, "scripts", "npc", "npc_items_custom.txt");
			}

			if (!File.Exists(path)) {
				return;
			}

			try {
				KeyValue kvs = kvs = KVParser.KV1.ParseAll(File.ReadAllText(path))[0];

				foreach (KeyValue kv in kvs.Children) {
					if (kv.Key == "Version") {
						continue;
					}

					string abilName = kv.Key;
					List<string> abilitySpecialNames = new List<string>();

					foreach (KeyValue kv2 in kv.Children) {
						if (kv2.Key == "AbilitySpecial") {
							foreach (KeyValue kv3 in kv2.Children) {
								foreach (KeyValue kv4 in kv3.Children) {
									if (kv4.Key != "var_type") {
										string abilitySpecialName = kv4.Key;
										abilitySpecialNames.Add(abilitySpecialName);
									}
								}

							}
						} else if (kv2.Key == "Modifiers") {
							foreach (KeyValue kv3 in kv2.Children) {
								string modifierName = kv3.Key;
								bool hiddenModifier = false;
								foreach (KeyValue kv4 in kv3.Children) {
									if (kv4.Key == "IsHidden" && kv4.GetString() == "1") {
										hiddenModifier = true;
									}
								}
								if (!hiddenModifier) {
									if (!item) {
										abilityModifierNames.Add(modifierName);
									} else {
										itemModifierNames.Add(modifierName);
									}
								}

							}
						}
					}
					if (!item) {
						abilityEntries.Add(new AbilityEntry(abilName, abilitySpecialNames));
					} else {
						itemEntries.Add(new AbilityEntry(abilName, abilitySpecialNames));
					}
				}
			} catch (Exception e) {
				Debug.WriteLine("Error: " + e.StackTrace);
				/*MetroMessageBox.Show(this, 
					"Error parsing npc_abilities_custom.txt: " + e.StackTrace, 
					"", 
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);*/
			}
		}

		#endregion

	}
}