using KVLib;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit.Features {
	class KVFeatures {
		MainForm mainForm;

		public KVFeatures(MainForm mainForm) {
			this.mainForm = mainForm;
		}

		internal void combine() {
			string[] items = { "Heroes", "Units", "Items", "Abilities" };
			for (int i = 0; i < items.Length; i++) {
				string itemStr = items[i].ToLowerInvariant();
				string fold = Path.Combine(mainForm.currAddon.gamePath, "scripts", "npc", itemStr);
				string foldName = fold.Substring(fold.LastIndexOf('\\') + 1);
				string parentFolder = fold.Substring(0, fold.LastIndexOf('\\'));
				string bigKVPath = Path.Combine(parentFolder, "npc_" + foldName + "_custom.txt");

				if (!File.Exists(bigKVPath)) {
					continue;
				}

				if (!Directory.Exists(fold)) {
					DialogResult dr = MetroMessageBox.Show(mainForm, "npc_" + itemStr + "_custom.txt has not been broken up. Break it up now?",
						"Break up KV file",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Information);
					if (dr != DialogResult.OK) {
						continue;
					}
					breakUp(itemStr);
				}

				string currText = File.ReadAllText(bigKVPath);

				// so now we have the big KV file created and ready to be populated.

				string[] files = Directory.GetFiles(fold);
				StringBuilder text = new StringBuilder("\"DOTAAbilities\"" + "\n{\n");
				if (foldName == "heroes") {
					text = new StringBuilder("\"DOTAHeroes\"" + "\n{\n");
				} else if (foldName == "units") {
					text = new StringBuilder("\"DOTAUnits\"" + "\n{\n");
				}
				bool hasPrecacheEverything = false;
				foreach (string file in files) {
					if (file.Contains("npc_precache_everything.txt")) {
						// skip this, save it for last
						hasPrecacheEverything = true;
						continue;
					}
					bool addTab = false;
					string[] lines = File.ReadAllLines(file);
					for (int j = 0; j < lines.Length; j++) {
						string line = lines[j];
						if (j == 0 && line.StartsWith("\t") == false && line.StartsWith("  ") == false) {
							addTab = true;
						}
						string newLine = line;
						if (addTab) {
							newLine = "\t" + line;
						}
						text.AppendLine(newLine);
					}
				}
				// now do npc_precache_everything.txt
				if (hasPrecacheEverything) {
					bool addTab = false;
					string[] lines = File.ReadAllLines(Path.Combine(fold, "npc_precache_everything.txt"));
					for (int j = 0; j < lines.Length; j++) {
						string line = lines[j];
						if (j == 0 && line.StartsWith("\t") == false && line.StartsWith("  ") == false) {
							addTab = true;
						}
						string newLine = line;
						if (addTab) {
							newLine = "\t" + line;
						}
						text.AppendLine(newLine);
					}
				}
				text.Append("}");

				// check if they're different before writing
				string txt = text.ToString();
				if (txt.Trim() != currText.Trim()) {
					File.WriteAllText(bigKVPath, txt);
				} else {
					Debug.WriteLine("Not overwriting.");
				}
			}

			mainForm.text_notification("Combine success", MetroColorStyle.Green, 1500);
		}

		private void breakUp(string itemStr) {
			string file = Path.Combine(mainForm.currAddon.gamePath, "scripts", "npc", "npc_" + itemStr + "_custom.txt");
			string folderPath = Path.Combine(mainForm.currAddon.gamePath, "scripts", "npc", itemStr);

			// Ensure the npc_ file exists.
			if (!File.Exists(file)) {
				return;
			}

			string folderName = file.Substring(file.LastIndexOf('\\') + 1);
			// get rid of extension.
			folderName = folderName.Substring(0, folderName.LastIndexOf('.'));
			if (!Directory.Exists(folderPath)) {
				Directory.CreateDirectory(folderPath);
			}
			string allText = File.ReadAllText(file);
			allText = allText.Replace("\r", "");
			KeyValue[] kvs = KVLib.KVParser.KV1.ParseAll(allText);
			foreach (KeyValue kv in kvs) {
				if (kv.Key == "DOTAAbilities" || kv.Key == "DOTAHeroes" || kv.Key == "DOTAUnits") {
					// skip this first nextKey, go straight to children.
					if (kv.HasChildren) {
						IEnumerable<KeyValue> kvs2 = kv.Children;
						KeyValue[] kvArr = kvs2.ToArray();

						if (kvArr.Length == 0) {
							// This kv file is screwed up.
							break;
						}

						// record start line number and end line number of each Key-Value block
						int[] startLineNumber = new int[kvArr.Length];
						int[] endLineNumber = new int[kvArr.Length];

						// catch the start pointer, ignore all "Version"s
						int ptr = 0;
						while (kvArr[ptr].Key == "Version" && ptr < kvArr.Length)
							ptr++;

						// store the start pointer
						int startPtr = ptr;

						// init the first key
						string key = kvArr[ptr].Key;

						// loop over all lines to record the start/end of all kvs
						string[] lines = allText.Split('\n');
						for (int index = 0; index < lines.Length; index++) {
							string line = lines[index];
							if (line.Trim().StartsWith("\"" + key)) {
								int ind = index - 1;
								// go back to add all comments/empty lines to this block
								while ((lines[ind].Trim() == "" || lines[ind].Trim().StartsWith("//")) && (ind > 0))
									ind--;
								startLineNumber[ptr] = ind + 1;
								// record the end of the block for last pointer
								if (ptr > 0)
									endLineNumber[ptr - 1] = ind;
								if (ptr < kvArr.Length - 1) {
									ptr++;
									key = kvArr[ptr].Key;
								}
							}
						}
						// deal with very last pointer
						int lastInd = lines.Length - 1;
						while (lastInd > 0 && lines[lastInd].Contains("}") && (lines[lastInd].IndexOf("//") > lines[lastInd].IndexOf("}")))
							lastInd--;
						endLineNumber[kvArr.Length - 1] = lastInd;

						// generate break-down kv files and write text
						for (int p = startPtr; p < kvArr.Length; p++) {
							string filePath = Path.Combine(folderPath, kvArr[p].Key + ".txt");
							File.Create(filePath).Close();
							StringBuilder sb = new StringBuilder();

							for (int p1 = startLineNumber[p]; p1 <= endLineNumber[p]; p1++) {
								string line = lines[p1];
								// remove first tab
								if (line.StartsWith("\t")) {
									line = line.Substring(1);
								}
								sb.AppendLine(line);
							}

							string output = sb.ToString();
							// remove beginning newline
							output = output.TrimStart();

							// if last file, we need to remove the last }
							if (p == kvArr.Length - 1) {
								output = output.TrimEnd();
								if (output.EndsWith("}")) {
									output = output.Substring(0, output.Length - 1);
								}
							}
							File.WriteAllText(filePath, output);
						}
					}
				}
			}
		}

	}
}
