using MetroFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota2ModKit.Features {
	class VTEXFeatures {
		public MainForm mainForm;
		string dotaDir;

		public VTEXFeatures(MainForm mainForm) {
			this.mainForm = mainForm;
			dotaDir = mainForm.dotaDir;

		}

		public void compileVTEX() {
			// show dialog to open .tga or .mks file
			OpenFileDialog fd = new OpenFileDialog();
			fd.Multiselect = true;
			fd.Filter = "TGA|*.tga|MKS|*.mks";
			fd.Title = "Select .tga Files To Compile";

			string materialsPath = Path.Combine(mainForm.currAddon.contentPath, "materials");
			if (!Directory.Exists(materialsPath)) {
				Directory.CreateDirectory(materialsPath);
			}
			fd.InitialDirectory = materialsPath;


			if (!(fd.ShowDialog() == DialogResult.OK)) {
				return;
			}

			string[] filePaths = fd.FileNames;

			string saveFolder = materialsPath;

			// Start the conversion process
			foreach (string file in filePaths) {
				string f = file;
				// ensure it's inside content path
				if (!(f.Contains(Path.Combine("content", "dota_addons")))) {
					MetroMessageBox.Show(mainForm,
						f + " must be inside the content directory of " + mainForm.currAddon.name,
						"Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);

					return;
				}
				f = f.Substring(f.IndexOf(Path.Combine("content", "dota_addons")) + 20);
				f = f.Substring(f.IndexOf("\\") + 1);
				f = f.Replace('\\', '/');
				// todo: maybe we should put this into some resource text files
				string[] vtexFileStrings =
				{
					"<!-- dmx encoding keyvalues2_noids 1 format vtex 1 -->",
					"\"CDmeVtex\"",
					"{",
					"	\"m_inputTextureArray\" \"element_array\" ",
					"	[",
					"		\"CDmeInputTexture\"",
					"		{",
					"			\"m_name\" \"string\" \"0\"",
					"			\"m_fileName\" \"string\" \"" + f + "\"",
					"			\"m_colorSpace\" \"string\" \"srgb\"",
					"			\"m_typeString\" \"string\" \"2D\"",
					"		}",
					"	]",
					"	\"m_outputTypeString\" \"string\" \"2D\"",
					"	\"m_outputFormat\" \"string\" \"DXT5\"",
					"	\"m_textureOutputChannelArray\" \"element_array\"",
					"	[",
					"		\"CDmeTextureOutputChannel\"",
					"		{",
					"			\"m_inputTextureArray\" \"string_array\"",
					"				[",
					"					\"0\"",
					"				]",
					"			\"m_srcChannels\" \"string\" \"rgba\"",
					"			\"m_dstChannels\" \"string\" \"rgba\"",
					"			\"m_mipAlgorithm\" \"CDmeImageProcessor\"",
					"			{",
					"				\"m_algorithm\" \"string\" \"\"",
					"				\"m_stringArg\" \"string\" \"\"",
					"				\"m_vFloat4Arg\" \"vector4\" \"0 0 0 0\"",
					"			}",
					"			\"m_outputColorSpace\" \"string\" \"srgb\"",
					"		}",
					"	]",
					"}"
				};

				string name = file.Substring(file.LastIndexOf('\\') + 1);
				name = name.Substring(0, name.IndexOf('.'));
				string path = Path.Combine(saveFolder, name + ".vtex");
				File.Create(path).Close();
				File.WriteAllLines(path, vtexFileStrings);
				// resource compiler path
				string resourceCompilerPath = Path.Combine(dotaDir, "game", "bin", "win64", "resourcecompiler.exe");

				Process proc = new Process {
					StartInfo = new ProcessStartInfo {
						FileName = resourceCompilerPath,
						Arguments = "-i \"" + path + "\"",
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					}
				};
				proc.Start();
			}

			mainForm.text_notification(filePaths.Length + " .tga files successfully compiled.", MetroFramework.MetroColorStyle.Green, 2500);
		}

		public void decompileVTEX() {
			string extract = Path.Combine(dotaDir, "game", "dota_imported", "materials");

			if (!Directory.Exists(extract)) {
				Directory.CreateDirectory(extract);
			}

			if (Directory.GetFiles(extract, "*.vtex_c").Length == 0) {
				Process.Start(extract);
				MetroMessageBox.Show(mainForm, ".vtex_c files must be present in " + extract + " for this to work. Use GCFScape to extract .vtex_c files into this folder.",
					"Note",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}

			OpenFileDialog fd = new OpenFileDialog();
			fd.Title = "Select vtex_c Files To Decompile";
			fd.Multiselect = true;
			fd.InitialDirectory = Path.Combine(dotaDir, "game", "dota_imported", "materials");
			fd.Filter = "Valve Texture File(*.vtex_c)|*.vtex_c";
			if (fd.ShowDialog() == DialogResult.OK) {
				string[] vtexCPaths = fd.FileNames;
				foreach (string path in vtexCPaths) {
					//resourceinfo.exe -i <your vtex_c file> -debug tga -mip
					System.Diagnostics.Process process = new System.Diagnostics.Process();
					System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
					startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
					startInfo.UseShellExecute = true;
					startInfo.FileName = "cmd.exe";
					startInfo.WorkingDirectory = Path.Combine(Path.Combine(dotaDir, "game"), "bin", "win64");
					startInfo.Arguments = "/c resourceinfo.exe -i \"" + path + "\" -debug tga -mip";
					Debug.WriteLine(startInfo.Arguments);
					process.StartInfo = startInfo;
					process.Start();
					process.WaitForExit();
				}

				// Prepare to move the tga files to the addon's content dir.
				string materialsPath = Path.Combine(mainForm.currAddon.contentPath, "materials");
				if (!Directory.Exists(materialsPath)) {
					Directory.CreateDirectory(materialsPath);
				}
				//move the tga files to the addon's content dir.
				string[] tgaFiles = Directory.GetFiles(Path.Combine(Path.Combine(dotaDir, "game"), "bin", "win64"), "*.tga");
				foreach (string file in tgaFiles) {
					string fileName = file.Remove(0, file.LastIndexOf('\\') + 1);
					// prepare for moving file
					if (File.Exists(fileName)) {
						File.Delete(fileName);
					}
					File.Move(file, Path.Combine(materialsPath, fileName));
				}
				Process.Start(materialsPath);
			}
		}


	}
}
