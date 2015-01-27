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

        private CheckBox _commentCheckBox;

        public CheckBox CommentCheckBox
        {
            get { return _commentCheckBox; }
            set { _commentCheckBox = value; }
        }
        private CheckBox _removeHeroesCheckBox;

        public CheckBox RemoveHeroesCheckBox
        {
            get { return _removeHeroesCheckBox; }
            set { _removeHeroesCheckBox = value; }
        }

        private CheckBox _removeItemsCheckbox;

        public CheckBox RemoveItemsCheckbox
        {
            get { return _removeItemsCheckbox; }
            set { _removeItemsCheckbox = value; }
        }

        public NewAddonForm()
        {
            InitializeComponent();
            Submit = button1;
            CommentCheckBox = commentCheckBox;
            RemoveHeroesCheckBox = removeHeroesCheckBox;
            RemoveItemsCheckbox = removeItemsCheckBox;
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
