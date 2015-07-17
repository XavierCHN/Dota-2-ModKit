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
		string vsnd_to_soundname_Path = Path.Combine(Environment.CurrentDirectory, "vsnd_to_soundname_v2.txt");

		public SoundFeatures(MainForm mainForm) {
			this.mainForm = mainForm;
			vsndToName = mainForm.vsndToName;
		}

		public void findSoundName() {
			if (!File.Exists(vsnd_to_soundname_Path)) {
				DialogResult dr = MetroMessageBox.Show(mainForm,
					"Couldn't find " + vsnd_to_soundname_Path + ". A link will now open to DL the file. In the future, this will be auto-generated.",
					"Couldn't find sound mapping file.",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Error);

				if (dr != DialogResult.OK) {
					return;
				}

				Process.Start("https://mega.co.nz/#!VxYnQQBK!HewGiE2idCqaELGffHVbv1ihhg0U7se-BAdkynRAulU");
				return; //remove this for auto-generate
				//extractScripts();
				//generateSoundMapFile();
			}

			// we have a vsnd_to_soundname.txt at this point.
			if (vsndToName.Count == 0) {
				populateVsndToName();
			}

			FindSoundForm fsf = new FindSoundForm(mainForm);
			DialogResult dr2 = fsf.ShowDialog();

		}

		private void populateVsndToName() {
			KeyValue root = KVParser.KV1.ParseAll(File.ReadAllText(vsnd_to_soundname_Path))[0];
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
					if (file.Contains("sounds") && !file.Contains("phonemes")) {
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
													string vsnd = fixWave(val4);
													//Debug.WriteLine(val4 + " | " + soundName);
													string val = soundName + "|" + file.Replace(Path.Combine(Environment.CurrentDirectory, "possible_sscripts"), "soundevents");
                                                    if (!vsndToName.ContainsKey(vsnd)) {
														List<string> soundNames = new List<string>();
														soundNames.Add(val);
														vsndToName[vsnd] = soundNames;
													} else {
														if (!vsndToName[vsnd].Contains(val)) {
															vsndToName[vsnd].Add(val);
														}
													}
												}

											}
										} else {
											string val3 = kv3.GetString();
											if (val3.EndsWith(".wav") || val3.EndsWith(".mp3")) {
												string vsnd = fixWave(val3);
												//Debug.WriteLine(val3 + " | " + soundName);
												string val = soundName + "|" + file.Replace(Path.Combine(Environment.CurrentDirectory, "possible_sscripts"), "soundevents");
												if (!vsndToName.ContainsKey(vsnd)) {
													List<string> soundNames = new List<string>();
													soundNames.Add(val);
													vsndToName[vsnd] = soundNames;
												} else {
													if (!vsndToName[vsnd].Contains(val)) {
														vsndToName[vsnd].Add(val);
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
				string soundVsnd = m.Key;
				List<string> soundInfos = m.Value;
				KeyValue soundKV = new KeyValue(soundVsnd);
				root.AddChild(soundKV);

				foreach (string soundInfo in soundInfos) {
					string[] parts = soundInfo.Split('|');
					string soundName = parts[0];
					string vsndevts = parts[1];

					vsndevts = vsndevts.Replace("\\", "/");
					vsndevts = vsndevts.Replace(".txt", ".vsndevts");

					KeyValue waveKV = new KeyValue(soundName + "|" + vsndevts);
					soundKV.AddChild(waveKV);
				}
			}

			File.WriteAllText(vsnd_to_soundname_Path, root.ToString());
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
