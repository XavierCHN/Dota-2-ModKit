using Dota2ModKit.Forms;
using KVLib;
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
	class SoundFeatures {
		private MainForm mainForm;
		private Dictionary<string, List<string>> vsndToName;

		public SoundFeatures(MainForm mainForm) {
			this.mainForm = mainForm;
			vsndToName = mainForm.vsndToName;
		}

		public void findSoundName() {
			string vsnd_to_soundname_Path = Path.Combine(Environment.CurrentDirectory, "vsnd_to_soundname.txt");

			if (!File.Exists(vsnd_to_soundname_Path)) {
				DialogResult dr = MetroMessageBox.Show(mainForm,
					"Couldn't find " + vsnd_to_soundname_Path + ". A link will now open to DL the file. In the future, this will be auto-generated.",
					"Couldn't find sound mapping file.",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Error);

				if (dr != DialogResult.OK) {
					return;
				}
				Process.Start("https://mega.co.nz/#!E0wwlIhb!_BzVyHi5PXOPCkTCUKNvoi_E-cCrZ1sLMHXybLCjMVY");

				return; //remove this for auto-generate
				extractScripts();
				generateSoundMapFile();
			}

			// we have a vsnd_to_soundname.txt at this point.
			if (vsndToName.Count == 0) {
				populateVsndToName();
			}

			FindSoundForm fsf = new FindSoundForm(mainForm);
			DialogResult dr2 = fsf.ShowDialog();

		}

		private void populateVsndToName() {
			KeyValue root = KVParser.KV1.ParseAll(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "vsnd_to_soundname.txt")))[0];
			foreach (KeyValue kv in root.Children) {
				string vsndPath = kv.Key;
				List<string> soundNames = new List<string>();
				if (kv.HasChildren) {
					foreach (KeyValue kv2 in kv.Children) {
						soundNames.Add(kv2.Key);
					}
					vsndToName.Add(vsndPath, soundNames);
				}
			}


		}

		private void extractScripts() {
			
		}

		private void generateSoundMapFile() {
			string[] files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "possible_sscripts"), "*.txt", SearchOption.AllDirectories);
			vsndToName = new Dictionary<string, List<string>>();

			foreach (string file in files) {
				try {
					if (file.Contains("sounds")) {
						KeyValue[] roots = KVParser.KV1.ParseAll(File.ReadAllText(file));
						if (roots == null || roots.Count() == 0) {
							continue;
						}

						foreach (KeyValue kv2 in roots) {
							string soundName = kv2.Key;


							if (kv2.HasChildren) {
								foreach (KeyValue kv3 in kv2.Children) {
									if (kv3.Key.Contains("wave")) {
										if (kv3.HasChildren) {
											foreach (KeyValue kv4 in kv3.Children) {
												string val4 = kv4.GetString();
												if (val4.EndsWith(".wav") || val4.EndsWith(".mp3")) {
													string wav = fixWave(val4);
													//Debug.WriteLine(val4 + " | " + soundName);
													if (!vsndToName.ContainsKey(wav)) {
														List<string> soundNames = new List<string>();
														soundNames.Add(soundName);
														soundNames.Add("path: " + file.Replace(Path.Combine(Environment.CurrentDirectory, "possible_sscripts"), "soundevents"));
														vsndToName[wav] = soundNames;
													} else {
														if (!vsndToName[wav].Contains(soundName)) {
															vsndToName[wav].Add(soundName);
														}
													}
												}

											}
										} else {
											string val3 = kv3.GetString();
											if (val3.EndsWith(".wav") || val3.EndsWith(".mp3")) {
												string wav = fixWave(val3);
												//Debug.WriteLine(val3 + " | " + soundName);
												if (!vsndToName.ContainsKey(wav)) {
													List<string> soundNames = new List<string>();
													soundNames.Add(soundName);
													soundNames.Add("path: " + file.Replace(Path.Combine(Environment.CurrentDirectory, "possible_sscripts"), "soundevents"));
													vsndToName[wav] = soundNames;
												} else {
													if (!vsndToName[wav].Contains(soundName)) {
														vsndToName[wav].Add(soundName);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				} catch (Exception ex) {
					Debug.WriteLine("Skipping " + file + ":");
					//Debug.WriteLine(ex.ToString());
					continue;
				}
			}

			KeyValue root = new KeyValue("Sounds");
			foreach (KeyValuePair<string, List<string>> m in vsndToName) {
				string soundName = m.Key;
				List<string> waves = m.Value;
				KeyValue soundNameKV = new KeyValue(soundName);
				root.AddChild(soundNameKV);

				string path = "";
				foreach (string wave in waves) {
					if (wave.StartsWith("path:")) {
						path = wave;
						continue;
					}
					KeyValue waveKV = new KeyValue(wave);
					soundNameKV.AddChild(waveKV);
				}

				path = path.Replace("\\", "/");
				path = path.Replace(".txt", ".vsndevts");
				KeyValue pathKV = new KeyValue(path);
				soundNameKV.AddChild(pathKV);
			}
			File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "vsnd_to_soundname.txt"), root.ToString());
		}

		private string fixWave(string wave) {
			string wav = wave;
			wav = wav.Replace("\\", "/");
			wav = wav.Replace("*", "");
			wav = wav.Replace("#", "");
			wav = wav.Replace("(", "");
			wav = wav.Replace(")", "");
			wav = wav.Replace(".wav", ".vsnd");
			wav = wav.Replace(".mp3", ".vsnd");
			wav = "sounds/" + wav;

			return wav;
		}

	}
}
