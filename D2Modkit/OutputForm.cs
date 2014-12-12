using System;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class OutputForm : Form
    {
        private RichTextBox rTextBox;

        public RichTextBox RTextBox
        {
            get { return rTextBox; }
            set { rTextBox = value; }
        }

        public OutputForm()
        {
            InitializeComponent();
            RTextBox = richTextBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}