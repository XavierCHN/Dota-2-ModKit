using MetroFramework;
using System.IO;
using System.Windows.Forms;

namespace Dota2ModKit.Features {
	public class ParticleFeatures {
		MainForm mainForm;

		public ParticleFeatures(MainForm mainForm) {
			this.mainForm = mainForm;
		}

		internal void design() {
			string particleDir = Path.Combine(mainForm.currAddon.contentPath, "particles");

			if (!Directory.Exists(particleDir)) {
				MetroMessageBox.Show(mainForm, particleDir + " doesn't exist!",
				"Error",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
				return;
			}
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.InitialDirectory = particleDir;
			ofd.Multiselect = true;
			ofd.Title = strings.SelectParticlesToDesign;
			ofd.Filter = strings.ParticleFiles + " (*.vpcf)|*.vpcf";
			DialogResult dr = ofd.ShowDialog();

			if (dr != DialogResult.OK) {
				return;
			}

			string[] particlePaths = ofd.FileNames;

			ParticleDesignForm pdf = new ParticleDesignForm(mainForm, particlePaths);
			dr = pdf.ShowDialog();
		}
	}
}
