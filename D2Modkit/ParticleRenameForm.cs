using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class ParticleRenameForm : Form
    {
        private string[] paths;

        public string[] Paths
        {
            get { return paths; }
            set { paths = value; }
        }

        private Button submit;

        public Button Submit
        {
            get { return button1; }
            set { submit = value; }
        }

        private bool submitClicked;

        public bool SubmitClicked
        {
            get { return submitClicked; }
            set { submitClicked = value; }
        }

        private TextBox pTextBox;

        public TextBox PTextBox
        {
            get { return textBox1; }
            set { pTextBox = value; }
        }

        public ParticleRenameForm()
        {
            InitializeComponent();
            Submit = button1;
            PTextBox.KeyDown += PTextBox_KeyDown;
        }

        void PTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Submit.PerformClick();
            }
        }
    }
}
