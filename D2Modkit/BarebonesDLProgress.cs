using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
