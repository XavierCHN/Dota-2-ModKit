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

		public SpellLibraryForm(MainForm mainForm) {
			this.mainForm = mainForm;
			npcPath = Path.Combine(spellLibPath, "game", "scripts", "npc");

			InitializeComponent();
			metroScrollBar1.Region = textBox1.Region;
			//textBox1.AutoScrollOffset

			if (!Directory.Exists(spellLibPath)) {
				DialogResult dr = MetroMessageBox.Show(mainForm, "SpellLibrary will now be cloned into " + spellLibPath,
					"SpellLibrary not found",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Information);

				if (dr != DialogResult.OK) {
					return;
				}

				mainForm._spellLibBtn.Enabled = false;
				mainForm._progressSpinner1.Value = 60;
				mainForm._progressSpinner1.Visible = true;
				mainForm.text_notification("Cloning SpellLibrary...", MetroColorStyle.Blue, 999999);

				using (var cloneWorker = new BackgroundWorker()) {
					cloneWorker.RunWorkerCompleted += CloneWorker_RunWorkerCompleted;
					cloneWorker.DoWork += CloneWorker_DoWork;
					cloneWorker.RunWorkerAsync();
				}

			} else {
				initTreeView();
			}
		}

		private void CloneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Console.WriteLine("Cloneworker ocmpleted.");
			mainForm.text_notification("Clone complete!", MetroColorStyle.Green, 2500);
			mainForm._progressSpinner1.Visible = false;
			mainForm._spellLibBtn.Enabled = true;

			initTreeView();
		}

		private void CloneWorker_DoWork(object sender, DoWorkEventArgs e) {
			string gitPath = Repository.Clone("https://github.com/Pizzalol/SpellLibrary", spellLibPath);
			Console.WriteLine("repo path:" + gitPath);

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

			treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
			treeView1.AfterSelect += TreeView1_AfterSelect;
			treeView1.KeyDown += TreeView1_KeyDown;

			treeView1.ExpandAll();
			this.ShowDialog();
		}

		private void TreeView1_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter && treeView1.SelectedNode != null) {
				treeView1.SelectedNode.Expand();
			}
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) {
			TreeNode node = e.Node;
			string abilName = node.Text;
			//Console.WriteLine("Node click: " + node.Text);

			if (node.Parent != null && node.Parent.Parent != null) {
				if (node.Parent.Parent.Text == "abilities") {
					string heroName = node.Parent.Text;
					string[] possiblePaths = new string[]
						{
							Path.Combine(npcPath, "abilities", heroName + "_" + abilName + "_datadriven.txt"),
							Path.Combine(npcPath, "abilities", heroName + "_" + abilName + ".txt"),
							Path.Combine(npcPath, "abilities", heroName + "_datadriven.txt"),
							Path.Combine(npcPath, "abilities", heroName + ".txt"),
                            Path.Combine(npcPath, "abilities", abilName + "_datadriven.txt"),
						};

					bool found = false;
					foreach (string p in possiblePaths) {
						if (File.Exists(p)) {
							textBox1.Text = File.ReadAllText(p);
							found = true;
							break;
						}
					}
					if (!found) {
						Console.WriteLine(abilName + " path wasn't found!");
					}
				}
			}
		}

		private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {

		}

		private void populateTreeView() {
			TreeNode abilities = treeView1.Nodes.Add("abilities");
			TreeNode items = treeView1.Nodes.Add("items");

			string[] abils = Directory.GetFiles(Path.Combine(npcPath, "abilities"), "*.txt");
			Dictionary<string, TreeNode> heroNames = new Dictionary<string, TreeNode>();
			string currHeroName = "";
			List<string> abilArr = new List<string>();

			foreach (string abil in abils) {
				string abilName = abil.Substring(abil.LastIndexOf('\\') + 1);
				abilName = abilName.Replace(".txt", "");
				abilName = abilName.Replace("_datadriven", "");
				string heroName = abilName.Substring(0, abilName.IndexOf('_'));

				if (currHeroName == "") {
					currHeroName = heroName;
				}

				if (heroName.StartsWith(currHeroName)) {
					abilArr.Add(abil.Substring(abil.LastIndexOf('\\') + 1));
				} else {
					currHeroName = heroName;

					string commonHeroName = Util.findCommonBeginning(abilArr.ToArray());
					if (commonHeroName.EndsWith("_")) {
						commonHeroName = commonHeroName.Substring(0, commonHeroName.Length - 1);
					}
					commonHeroName = commonHeroName.Replace(".txt", "").Replace("_datadriven", "");


					foreach (string abilName2 in abilArr) {
						string abilName3 = abilName2.Replace("_datadriven", "").Replace(".txt", "").Replace(commonHeroName + "_", "");

						if (!heroNames.ContainsKey(commonHeroName)) {
							heroNames.Add(commonHeroName, abilities.Nodes.Add(commonHeroName));
						}

						// add the abil to the hero node
						TreeNode abilNode = heroNames[commonHeroName].Nodes.Add(abilName3);
					}

					abilArr.Clear();
					abilArr.Add(abil.Substring(abil.LastIndexOf('\\') + 1));
				}

				//KeyValue root = KVParser.KV1.Parse(File.ReadAllText(abil));


			}
		}

		private void copySpellBtn_Click(object sender, EventArgs e) {
			Clipboard.SetText(textBox1.Text);
		}

		private void luaBtn_Click(object sender, EventArgs e) {

		}

		private void metroScrollBar1_Scroll(object sender, ScrollEventArgs e) {
			Console.WriteLine(e.NewValue);
			Console.WriteLine(textBox1.AutoScrollOffset.X + ", " + textBox1.AutoScrollOffset.Y);
		}
	}
}
