using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private TextBox pTextBox;

        private bool submitClicked;

        public bool SubmitClicked
        {
            get { return submitClicked; }
            set { submitClicked = value; }
        }

        public TextBox PTextBox
        {
            get { return textBox1; }
            set { pTextBox = value; }
        }

        public ParticleRenameForm()
        {
            InitializeComponent();
            textBox1.TextChanged += particleRenameTextBox_TextChanged;
            Submit = button1;
        }

        void particleRenameTextBox_TextChanged(object sender, EventArgs e)
        {
           
        }
        

    }
}
