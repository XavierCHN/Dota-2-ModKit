using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit.Forms {
	public partial class MasterToolboxForm : MetroForm {
		private MainForm mainForm;

		public MasterToolboxForm(MainForm mainForm) {
			this.mainForm = mainForm;

			InitializeComponent();
		}

		private void MasterToolboxForm_Load(object sender, EventArgs e) {

		}
	}
}
