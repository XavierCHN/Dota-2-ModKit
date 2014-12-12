using System.Windows.Forms;

namespace D2ModKit
{
    public partial class BarebonesDLProgress : Form
    {
        private ProgressBar barebonesProgressBar;

        public ProgressBar BarebonesProgressBar
        {
            get { return barebonesProgressBar; }
            set { barebonesProgressBar = value; }
        }

        private int currentPercentage;

        public int CurrentPercentage
        {
            get { return currentPercentage; }
            set { currentPercentage = value; }
        }

        public BarebonesDLProgress()
        {
            InitializeComponent();
            BarebonesProgressBar = progressBar1;
        }
    }
}