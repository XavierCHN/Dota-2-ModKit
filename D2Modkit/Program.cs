using System;
using System.Windows.Forms;

namespace D2ModKit
{
    internal static class Program
    {
        /// <summary>
        /// The main hero point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}