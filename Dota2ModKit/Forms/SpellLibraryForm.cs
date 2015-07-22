using KVLib;
using LibGit2Sharp;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit.Forms {
	public partial class SpellLibraryForm : MetroForm {
		private MainForm mainForm;
		string spellLibPath = Path.Combine(Environment.CurrentDirectory, "SpellLibrary");
		string npcPath = "";
		private string currKVPath = "";
		private string luaHeroesPath;
		private string luaItemsPath;
		private string currLuaPath = "";

		public SpellLibraryForm(MainForm mainForm) {
			this.mainForm = mainForm;
			npcPath = Path.Combine(spellLibPath, "game", "scripts", "npc");
			luaHeroesPath = Path.Combine(spellLibPath, "game", "scripts", "vscripts", "heroes");
			luaItemsPath = Path.Combine(spellLibPath, "game", "scripts", "vscripts", "items");

			InitializeComponent();
			notificationLabel.Text = "";
			textBox1.KeyDown += TextBox1_KeyDown;
			//luaKVBtn.Visible = false;

			if (!Directory.Exists(spellLibPath)) {
				DialogResult dr = MetroMessageBox.Show(mainForm, "SpellLibrary will now be cloned into " + spellLibPath,
					"SpellLibrary not found",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Information);

				if (dr != DialogResult.OK) {
					return;
				}
			}
			// user wants to continue, clone if necessary, and pull
			mainForm._spellLibBtn.Enabled = false;
			mainForm._progressSpinner1.Value = 60;
			mainForm._progressSpinner1.Visible = true;

			if (!Directory.Exists(spellLibPath)) {
				mainForm.text_notification("Cloning SpellLibrary...", MetroColorStyle.Blue, 999999);
			} else {
				mainForm.text_notification("Pulling SpellLibrary...", MetroColorStyle.Blue, 999999);
			}

			using (var cloneWorker = new BackgroundWorker()) {
				cloneWorker.RunWorkerCompleted += CloneWorker_RunWorkerCompleted;
				cloneWorker.DoWork += CloneWorker_DoWork;
				cloneWorker.RunWorkerAsync();
			}
		}

		private void TextBox1_KeyDown(object sender, KeyEventArgs e) {
			if (e.Control && (e.KeyCode == Keys.A)) {
				textBox1.SelectAll();
			}
		}

		private void CloneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Console.WriteLine("Cloneworker completed.");
			mainForm.text_notification("", MetroColorStyle.Blue, 1000);
			mainForm._progressSpinner1.Visible = false;
			mainForm._spellLibBtn.Enabled = true;

			initTreeView();
		}

		private void CloneWorker_DoWork(object sender, DoWorkEventArgs e) {
			if (!Directory.Exists(spellLibPath)) {
				string gitPath = Repository.Clone("https://github.com/Pizzalol/SpellLibrary", spellLibPath);
				Console.WriteLine("repo path:" + gitPath);

				return;
			}

			// pull from the repo
			using (var repo = new Repository(spellLibPath)) {
				//var remote = repo.Network.Remotes["origin"];
				MergeResult mr = repo.Network.Pull(new Signature(new Identity("myname", "myname@email.com"), new DateTimeOffset()), new PullOptions());
				MergeStatus ms = mr.Status;
				Console.WriteLine("MergeStatus: " + ms.ToString());
			}
		}

		private void initTreeView() {
			populateTreeView();
			treeView1.Nodes[0].Expand();
			treeView1.Nodes[1].Expand();

			treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
			treeView1.AfterSelect += TreeView1_AfterSelect;
			treeView1.KeyDown += TreeView1_KeyDown;

			//treeView1.ExpandAll();
			this.Show();
		}

		private void TreeView1_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter && treeView1.SelectedNode != null) {
				treeView1.SelectedNode.Expand();
			}
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) {
			TreeNode node = e.Node;

			if (node.Parent != null && node.Parent.Parent != null && node.Parent.Parent.Text == "Abilities") {
				string abilName = node.Name;
				string heroName = node.Parent.Name;
				string p = Path.Combine(npcPath, "abilities", abilName);
                if (File.Exists(p)) {
					textBox1.Text = File.ReadAllText(p);
					currKVPath = p;

					string lua = Path.Combine(luaHeroesPath, "hero_" + heroName, abilName + ".lua");
					if (!File.Exists(lua)) {
						luaKVBtn.Enabled = false;

					} else {
						luaKVBtn.Enabled = true;
						luaKVBtn.Text = "Lua Script";
						currLuaPath = lua;
					}
				} else {
					Console.WriteLine(abilName + " path wasn't found!");
				}
				
			} else if (node.Parent != null && node.Parent.Text == "Items") {
				string itemName = node.Name;
				string p = Path.Combine(npcPath, "items", itemName);
				if (File.Exists(p)) {
					textBox1.Text = File.ReadAllText(p);
					currKVPath = p;

					string lua = Path.Combine(luaItemsPath, itemName.Replace("_datadriven.txt", ".lua"));
					if (!File.Exists(lua)) {
						luaKVBtn.Enabled = false;

					} else {
						luaKVBtn.Enabled = true;
						luaKVBtn.Text = "Lua Script";
						currLuaPath = lua;
					}
				} else {
					Console.WriteLine(itemName + " path wasn't found!");
				}


            }
		}

		private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {

		}

		private void populateTreeView() {
			TreeNode abilities = treeView1.Nodes.Add("Abilities");
			TreeNode items = treeView1.Nodes.Add("Items");

			string[] abilPaths = Directory.GetFiles(Path.Combine(npcPath, "abilities"), "*.txt");
			string[] itemPaths = Directory.GetFiles(Path.Combine(npcPath, "items"), "*.txt");
            Dictionary<string, TreeNode> heroNames = new Dictionary<string, TreeNode>();
			string currHeroName = "";
			List<string> abilArr = new List<string>();
			//StringBuilder allAbils = new StringBuilder();

			foreach (string abilPath in abilPaths) {
				string abilName = abilPath.Substring(abilPath.LastIndexOf('\\') + 1);

				//allAbils.AppendLine("\"" + abilName + "\"");

				abilName = abilName.Replace(".txt", "").Replace("_datadriven", "");
				string heroName = abilName.Substring(0, abilName.IndexOf('_'));

				if (heroName =="phantom" || heroName == "shadow") {
					// hardcode these
					string[] split = abilName.Split('_');
					heroName = split[0] + "_" + split[1];

				}

				if (currHeroName == "") {
					currHeroName = heroName;
				}

				if (heroName.StartsWith(currHeroName)) {
					abilArr.Add(abilPath.Substring(abilPath.LastIndexOf('\\') + 1));
				} else {
					currHeroName = heroName;

					string commonHeroName = Util.findCommonBeginning(abilArr.ToArray());
					if (commonHeroName.EndsWith("_")) {
						commonHeroName = commonHeroName.Substring(0, commonHeroName.Length - 1);
					}
					commonHeroName = commonHeroName.Replace(".txt", "").Replace("_datadriven", "");

					// has this KV file actually been modified in spell library?
					bool hasBeenModified = false;
					TreeNode heroNode = null;
					for (int i = 0; i < abilArr.Count; i++) {
						string abilName2 = abilArr[i];
						string abilName3 = abilName2.Replace("_datadriven", "").Replace(".txt", "");
						string path2 = Path.Combine(npcPath, "abilities", abilName2);

						if (!hasBeenModified) {
							string txt = File.ReadAllText(path2);
                            if (txt.StartsWith("//") || txt.Contains("precache") || txt.Contains("RunScript")) {
								hasBeenModified = true;
							}
                        }

						string com = commonHeroName + "_";
						if (abilName3.StartsWith(com)) {
							abilName3 = abilName3.Substring(com.Length, abilName3.Length - com.Length);
						}

						if (!heroNames.ContainsKey(commonHeroName)) {
							//Console.WriteLine("\"" + commonHeroName + "\"");
							//Console.WriteLine("\"" + Util.MakeUnderscoreStringNice(commonHeroName) + "\"");
							heroNode = abilities.Nodes.Add(Util.MakeUnderscoreStringNice(commonHeroName));
							heroNode.Name = commonHeroName;
                            heroNames.Add(commonHeroName, heroNode);
						}

						// is it even worth showing this hero in the treeView?
						if (!hasBeenModified && i == abilArr.Count-1) {
							// this hero has 0 modified abils.
							heroNode.Remove();
						}

						// expand the heroNode if an ability inside it has been modified.
						if (hasBeenModified && heroNode != null && !heroNode.IsExpanded) {
							heroNode.Expand();
						}

						// add the abil to the hero node
						TreeNode abilNode = heroNames[commonHeroName].Nodes.Add(Util.MakeUnderscoreStringNice(abilName3));
						abilNode.Name = abilName2;
					}

					abilArr.Clear();
					abilArr.Add(abilPath.Substring(abilPath.LastIndexOf('\\') + 1));
				}
			}

			// done with abils. now onto items.
			foreach (string itemPath in itemPaths) {
				string itemName = itemPath.Substring(itemPath.LastIndexOf('\\') + 1);
				string niceName = Util.MakeUnderscoreStringNice(itemName.Replace(".txt", "").Replace("_datadriven", "").Replace("item_", ""));

				string txt = File.ReadAllText(itemPath);
				// ensure this item was actually completed by devs of spell library.
				if (txt.StartsWith("//") || txt.Contains("precache") || txt.Contains("RunScript")) {
					TreeNode itemNode = items.Nodes.Add(niceName);
					itemNode.Name = itemName;
					itemNode.Expand();
				}
			}
		}

		private void copySpellBtn_Click(object sender, EventArgs e) {
			metroRadioButton1.Select();

			Clipboard.SetText(textBox1.Text);
			text_notification("Copied!", MetroColorStyle.Blue, 1500);
		}

		private void luaKVBtn_Click(object sender, EventArgs e) {
			metroRadioButton1.Select();

			if (luaKVBtn.Text == "Lua Script") {
				// open lua
				textBox1.Text = File.ReadAllText(currLuaPath);
				luaKVBtn.Text = "KeyValues";
				metroToolTip1.SetToolTip(luaKVBtn, "Open the KV entry for this spell");

			} else {
				// open kv
				textBox1.Text = File.ReadAllText(currKVPath);
				luaKVBtn.Text = "Lua Script";
				metroToolTip1.SetToolTip(luaKVBtn, "Opens the Lua script for this spell");
			}

		}

		private void metroScrollBar1_Scroll(object sender, ScrollEventArgs e) {
			Console.WriteLine(e.NewValue);
			Console.WriteLine(textBox1.AutoScrollOffset.X + ", " + textBox1.AutoScrollOffset.Y);
		}

		public void text_notification(string text, MetroColorStyle color, int duration) {
			System.Timers.Timer notificationLabelTimer = new System.Timers.Timer(duration);
			notificationLabelTimer.SynchronizingObject = this;
			notificationLabelTimer.AutoReset = false;
			notificationLabelTimer.Start();
			notificationLabelTimer.Elapsed += kvLabelTimer_Elapsed;
			notificationLabel.Style = color;
			notificationLabel.Text = text;
		}

		private void kvLabelTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			notificationLabel.Text = "";
		}

	}
}
