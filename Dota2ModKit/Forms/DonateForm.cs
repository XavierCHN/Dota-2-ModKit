using MetroFramework.Forms;
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

namespace Dota2ModKit.Forms {
	public partial class DonateForm : MetroForm {
		MainForm mainForm;

		public DonateForm(MainForm mainForm) {
			this.mainForm = mainForm;

			InitializeComponent();

			metroTextBox1.Text =
				"Hi! I’m Stephen \"Myll\" Fournier, the creator of Dota 2 ModKit. I created D2ModKit to 1) Learn C# and GUI development, and 2) To ease several otherwise time-consuming tasks related to Dota 2 custom game development. I know that D2ModKit has greatly helped hundreds of modders to this date. Many have expressed their gratitude towards me over the past year for creating this program. The reactions of some people when I first showed off the tooltips generator were priceless! This has always been a completely self-motivated project of mine, and I really love developing it!" +

				"\r\n\r\nThough donations are not necessary to me, they do help greatly in motivating me and re-assuring me that all the time spent creating D2ModKit was time well spent. I’m just a poor college student that spent a lot of free-time, not at college parties, but creating this program… FOR YOU! The Dota modder! If this program greatly helped you in your custom game development endeavors, please consider a donation!" +

				"\r\n\r\nThank you so much!\r\n- Myll";

			label1.Select();
		}

		private void donatePictureBox_Click(object sender, EventArgs e) {
			Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=BPRL87NAKFP9N&lc=US&item_name=Stephen%20Fournier%2c%20D2ModKit%20Creator&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted");
		}
	}
}
