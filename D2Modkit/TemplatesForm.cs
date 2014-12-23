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
        private string templatesDir;
        private string[] lines;
        private Dictionary<string, string> map = new Dictionary<string, string>();
        private string currentTemplate;

        public TemplatesForm()
        {
            InitializeComponent();
            // make the file structure if we don't have it.
            string dir = Path.Combine(Environment.CurrentDirectory, "Templates");
            templatesDir = dir;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(Path.Combine(dir, "ability_templates.txt")))
            {
                File.Create(Path.Combine(dir, "ability_templates.txt")).Close();
            }

            if (!File.Exists(Path.Combine(dir, "item_templates.txt")))
            {
                File.Create(Path.Combine(dir, "item_templates.txt")).Close();
            }

            if (!File.Exists(Path.Combine(dir, "unit_templates.txt")))
            {
                File.Create(Path.Combine(dir, "unit_templates.txt")).Close();
            }

            if (!File.Exists(Path.Combine(dir, "hero_templates.txt")))
            {
                File.Create(Path.Combine(dir, "hero_templates.txt")).Close();
            }

            copyLabel.Text = "";

            // set hooks
            listBox1.SelectedValueChanged += listBox1_SelectedValueChanged;

            // load the ability templates first
            if (load("ability"))
            {

            }
            else
            {

            }
        }

        void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }

            copyLabel.Text = "";

            string item = (string)listBox1.SelectedItem;
            if (map.ContainsKey(item))
            {
                richTextBox1.Text = map[item];
                currentTemplate = richTextBox1.Text;
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

        private bool load(string template)
        {
            // expects template to be 'unit', 'ability', 'item', or 'hero'.
            string fileName = template + "_templates.txt";
            string path = Path.Combine(templatesDir, fileName);
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

            string content = "";
            string templateName = "";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("//+Template"))
                {
                    // save the current template.
                    if (templateName != "" && content != "")
                    {
                        map.Add(templateName, content);
                        templateName = "";
                        content = "";
                    }
                    continue;
                }

                // Check if we're at the end of the file.
                if (i == lines.Length - 1)
                {
                    content += lines[i];
                    // save the current template.
                    if (templateName != "" && content != "")
                    {
                        map.Add(templateName, content);
                    }
                    resetList();
                    return true;
                }

                // check if the name has been defined yet.
                if (templateName == "")
                {
                    templateName = lines[i];
                    content += lines[i] + "\n";
                    templateName = templateName.Replace("\"", "");
                    templateName = templateName.Trim();
                    continue;
                }

                content += lines[i] + "\n";

            }
            return true;
        }

        private void resetList()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < map.Count; i++)
            {
                KeyValuePair<string, string> entry = map.ElementAt(i);
                listBox1.Items.Add(entry.Key);
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {

        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(currentTemplate);
            copyLabel.Text = "Template copied to\nthe clipboard.";
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Process.Start(templatesDir);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
