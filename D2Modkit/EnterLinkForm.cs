using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class EnterLinkForm : Form
    {

		public TextBox Textbox {
			get { return textBox1; }
			set { textBox1 = value; }
		}

        public EnterLinkForm(string addonName, string linkTo)
        {
            InitializeComponent();
            if (linkTo == "steam")
            {
                label1.Text = "Enter the link to " + addonName + "'s Steam Workshop page:";
            }
            else if (linkTo == "gds")
            {
                label1.Text = "Enter the link to " + addonName + "'s GetDotaStats page:";
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
			string txt = textBox1.Text;
            if (txt != null && txt != "" && txt.StartsWith("http"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else 
            {
                MessageBox.Show("Invalid link!");
            }
        }
    }
}
