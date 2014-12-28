using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace D2ModKit
{
    public partial class TemplatesForm : Form
    {
        private static string[] templateNames = 
            { "ability", 
              "item", 
              "unit",
              "hero",
              "modifier",
            };

        private Template currentTemplate;
        private Template.Entry currTemplateEntry;
        private Dictionary<string, Template.Entry> currentMap;
        private string keyBeingEdited = "";
        private Dictionary<string, Template> templates = new Dictionary<string, Template>();

        public TemplatesForm()
        {
            InitializeComponent();

            // make the file structure if we don't have it.
            string dir = Path.Combine(Environment.CurrentDirectory, "Templates");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            for (int i = 0; i < templateNames.Length; i++)
            {
                templates.Add(templateNames[i], new Template(templateNames[i]));
            }

            copyLabel.Text = "";

            // set hooks
            listView1.ItemSelectionChanged += listView1_ItemSelectionChanged;
            listView1.AfterLabelEdit += listView1_AfterLabelEdit;
            this.FormClosing += TemplatesForm_FormClosing;

            // load the metadata


            // load the ability templates first
            load("ability");
        }

        private void load(string templateName)
        {
            currentTemplate = templates[templateName];
            currentMap = currentTemplate.Map;

            resetList();
        }

        void TemplatesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // re-write the metadata file so the custom template info is saved.
            string metadataPath = Path.Combine(Environment.CurrentDirectory, "Templates", "metadata.txt");
            if (!File.Exists(metadataPath))
            {
                File.Create(metadataPath).Close();
            }
            
            string str = "";
            for (int i = 0; i < templates.Count; i++)
            {
                KeyValuePair<string, Template> kv = templates.ElementAt(i);
                str += kv.Value.getData();
            }
            File.WriteAllText(metadataPath, str);
        }

        private void rename()
        {
            if (listView1.SelectedItems.Count > 0 && keyBeingEdited == "")
            {
                keyBeingEdited = listView1.SelectedItems[0].Text;
                listView1.SelectedItems[0].BeginEdit();
            }
        }

        void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // ensure the user didn't press the rename button and not rename it.
            if (e.Label != null && e.Label != "")
            {
                if (currentMap.ContainsKey(e.Label))
                {
                    keyBeingEdited = "";
                    e.CancelEdit = true;
                    return;
                }

                Template.Entry old = currentMap[keyBeingEdited];
                Template.Entry entry = new Template.Entry(old.Key, old.Val);
                currentMap.Add(e.Label, entry);
                currentMap.Remove(keyBeingEdited);
            }
            keyBeingEdited = "";
        }

        void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            copyLabel.Text = "";

            string item = e.Item.Text;
            if (currentMap.ContainsKey(item))
            {
                richTextBox1.Text = currentMap[item].Val;
                currTemplateEntry = currentMap[item];
            }
        }

        private void addTemplateButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.ReadOnly = false;
            richTextBox1.Text = "Paste your template here.";
        }

        private void deleteTemplateButton_Click(object sender, EventArgs e)
        {

        }

        private void abilityTemplates_Click(object sender, EventArgs e)
        {
            load("ability");
        }

        private void itemTemplates_Click(object sender, EventArgs e)
        {
            load("item");
        }

        private void heroTemplates_Click(object sender, EventArgs e)
        {
            load("hero");
        }

        private void unitTemplates_Click(object sender, EventArgs e)
        {
            load("unit");
        }

        private void modifierTemplates_Click(object sender, EventArgs e)
        {
            load("modifier");
        }

        private void resetList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < currentMap.Count(); i++)
            {
                KeyValuePair<string, Template.Entry> entry = currentMap.ElementAt(i);
                listView1.Items.Add(entry.Key);
            }
            // Auto-select the first item.
            if (listView1.Items.Count > 0)
            {
                listView1.Select();
                listView1.Items[0].Selected = true;
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {

        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(currTemplateEntry.Val);
            copyLabel.Text = "Template copied to\nthe clipboard.";
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Environment.CurrentDirectory, "Templates"));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            rename();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rename();
        }

    }
}
