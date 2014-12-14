using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace D2ModKit
{
    public partial class ParticleDesignForm : Form
    {
        private bool submitClicked;

        public bool SubmitClicked
        {
            get { return submitClicked; }
            set { submitClicked = value; }
        }

        private bool renamed;

        public bool Renamed
        {
            get { return renamed; }
            set { renamed = value; }
        }

        private bool resized;

        public bool Resized
        {
            get { return resized; }
            set { resized = value; }
        }

        private string[] rgb;

        public string[] Rgb
        {
            get { return rgb; }
            set { rgb = value; }
        }

        private string baseName;

        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; }
        }

        private bool colorPicked;

        public bool ColorPicked
        {
            get { return colorPicked; }
            set { colorPicked = value; }
        }

        private int resizeValue;

        public int ResizeValue
        {
            get { return resizeValue; }
            set { resizeValue = value; }
        }

        private ParticleSystem ps = null;

        public ParticleSystem Ps
        {
            get { return ps; }
            set { ps = value; }
        }

        private bool formCanceled;

        public bool FormCanceled
        {
            get { return formCanceled; }
            set { formCanceled = value; }
        }

        public ParticleDesignForm(Addon addon)
        {
            // We need a particle system to work with.
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Path.Combine(addon.ContentPath, "particles");
            fd.Multiselect = true;
            fd.Title = "Select Particles To Design";
            DialogResult res = fd.ShowDialog();
            if (res == DialogResult.OK)
            {
                Ps = new ParticleSystem(fd.FileNames);
            }
            else
            {
                FormCanceled = true;
                return;
            }
            InitializeComponent();
            initiate();
        }

        public ParticleDesignForm(ParticleSystem ps)
        {
            Ps = ps;
            InitializeComponent();
            initiate();
        }

        private void initiate()
        {
            textBox1.TextChanged += textBox1_TextChanged;

            resizeScrollBar.Scroll += resizeScrollBar_Scroll;
            resizeScrollBar.Maximum = 200 + resizeScrollBar.LargeChange - 1;
            resizeScrollBar.Minimum = -200;
            resizeScrollBar.Value = 0;
            sizeLabel.Text = "Size: +0%";
        }

        private void resizeScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ResizeValue = e.NewValue;
            // modify the label at the bottom of the design form.
            if (ResizeValue >= 0)
            {
                sizeLabel.Text = "Size: +" + ResizeValue + "%";
            }
            else
            {
                sizeLabel.Text = "Size: " + ResizeValue + "%";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BaseName = textBox1.Text;
            baseNameLabel.Text = "Base name: " + BaseName;
        }

        private void recolorButton_Click(object sender, EventArgs e)
        {
            string[] rgb = new string[3];
            colorDialog1 = new ColorDialog();
            colorDialog1.AnyColor = true;
            colorDialog1.AllowFullOpen = true;
            //colorDialog1.FullOpen = true;
            DialogResult re = colorDialog1.ShowDialog();
            if (re == DialogResult.OK)
            {
                Color picked = colorDialog1.Color;
                rgb[0] = picked.R.ToString();
                rgb[1] = picked.G.ToString();
                rgb[2] = picked.B.ToString();
                Rgb = rgb;

                // print a little label at the bottom of ParticleDesignForm.
                string rgb_output = "R: " + Rgb[0] + " G: " + Rgb[1] + " B: " + Rgb[2];
                colorLabel.Text = "Color: " + rgb_output;
                colorPicked = true;
            }
        }

        private void submitParticle_Click(object sender, EventArgs e)
        {
            // ensure we have a particle system.
            if (Ps == null)
            {
                return;
            }

            SubmitClicked = true;

            string output = "Particle Designer Output:\n";

            if (ColorPicked)
            {
                Ps.changeColor(Rgb);
                string rgb_output = "R: " + Rgb[0] + " G: " + Rgb[1] + " B: " + Rgb[2];
                output += "Changed color to: " + rgb_output + "\n\n";
            }

            if (ResizeValue != 0)
            {
                Resized = true;
                Ps.resize(ResizeValue);
            }

            if (BaseName != "" && BaseName != null)
            {
                Renamed = true;
                Ps.rename(BaseName);
                output += "Renamed particle system to: " + BaseName + "\n\n";
            }
            output += "New particle system:\n";
            for (int i = 0; i < Ps.Particles.Length; i++)
            {
                Particle p = ps.Particles[i];
                System.IO.File.WriteAllText(p.Path, p.ToString());
                output += i + ". " + p.Path + "\n\n";
            }

            output += "No errors detected.\n";

            if (Renamed)
            {
                // display ending note.
                MessageBox.Show("Particle system successfully modified.", "Particle Designer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // open up a window to where the modified particles are.
                Process.Start(Ps.Paths[0].Substring(0, Ps.Paths[0].LastIndexOf('\\')));
            }
            else
            {
                MessageBox.Show("Particle system successfully modified.", "Particle Designer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //close form.
            this.Close();

            /*OutputForm o = new OutputForm();
            o.RTextBox.SelectedText = output;
            o.ShowDialog();*/
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SubmitClicked = false;
            this.Close();
        }
    }
}