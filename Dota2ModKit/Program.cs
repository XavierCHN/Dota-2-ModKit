using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Dota2ModKit {
	static class Program
    {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main()
        {
			// refresh the debug log
			//Util.Log("", true);

			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			// Check if application is already running.
			if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1) {
				MessageBox.Show("An instance of D2ModKit is already running. Exiting.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

				Process.GetCurrentProcess().Kill();
			}

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			MainForm mainForm = new MainForm();
            Application.Run(mainForm);
        }

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			if (e.IsTerminating) {
				Exception ex = e.ExceptionObject as Exception;
				WriteCrash(ex);
			}
		}

		private static void WriteCrash(Exception ex) {
			Util.LogException(ex);

			MessageBox.Show("D2ModKit has crashed. A crash report has been created in " + Util.logPath,
				"Error",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);

			Application.Exit();
		}
	}
}
