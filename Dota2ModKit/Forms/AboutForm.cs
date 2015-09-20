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
	public partial class AboutForm : MetroForm {
		MainForm mainForm;

        public AboutForm(MainForm mainForm) {
			this.mainForm = mainForm;

			InitializeComponent();



		}
	}
}
