using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class ParticleResizeForm : Form
    {
        private int currValue;

        public int CurrValue
        {
            get { return currValue; }
            set { currValue = value; }
        }

        private bool submitClicked = false;

        public bool SubmitClicked
        {
            get { return submitClicked; }
            set { submitClicked = value; }
        }

        public ParticleResizeForm()
        {
            InitializeComponent();
            resizeScrollBar.Scroll += resizeScrollBar_Scroll;
            resizeScrollBar.Maximum = 300 + resizeScrollBar.LargeChange-1;
            resizeScrollBar.Minimum = -300;
            resizeScrollBar.Value = 0;
            sizeLabel.Text = "Size: +0%";
        }

        void resizeScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            CurrValue = e.NewValue;
            Debug.WriteLine(e.NewValue);
            if (CurrValue >= 0)
            {
                sizeLabel.Text = "Size: +" + CurrValue + "%";
            }
            else
            {
                sizeLabel.Text = "Size: " + CurrValue + "%";
            }
        }

        private void resizeOK_Click(object sender, EventArgs e)
        {
            SubmitClicked = true;
            this.Close();
        }
    }
}
