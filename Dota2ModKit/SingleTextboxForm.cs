using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace Dota2ModKit {
	public partial class SingleTextboxForm : MetroForm {
		public MetroTextBox textBox;
		public MetroLabel label;
		public MetroButton btn;

		public SingleTextboxForm() {
			InitializeComponent();

			textBox = metroTextBox1;
			label = metroLabel1;
			btn = metroButton1;

			metroButton1.Click += MetroButton1_Click;

		}

		private void MetroButton1_Click(object sender, EventArgs e) {
			metroRadioButton1.Select();

			if (textBox.Text == null || textBox.Text == "") {
				MetroMessageBox.Show(this, "Invalid text!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				this.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void SingleTextboxForm_Load(object sender, EventArgs e) {

		}
	}
}
