using System.Windows.Forms;

namespace D2ModKit
{
    public partial class NewAddonForm : Form 
    {

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

        private TextBox textBox;

        public TextBox _TextBox
        {
            get { return textBox1; }
            set { textBox = value; }
        }

        public NewAddonForm()
        {
            InitializeComponent();
            Submit = button1;
            _TextBox.KeyDown += TextBox_KeyDown;
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Submit.PerformClick();
            }
        }
    }
}
