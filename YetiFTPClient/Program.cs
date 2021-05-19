using System;
using System.Windows.Forms;

namespace YetiFTPClient
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Show loading screen until initial scan has run then close loading screen
            Loading.ShowLoading();
            MainForm mainForm = new MainForm();
            Loading.CloseForm();
            Application.Run(mainForm);
        }
    }
}
