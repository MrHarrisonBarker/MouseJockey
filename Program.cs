using System;
using System.Windows.Forms;

namespace MouseJockey
{
    static class Program
    {
        public static Jockey Jockey;
        public static Controllables Controllables;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Jockey = new Jockey();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var controlGlass = new ControlGlass();

            Controllables = new Controllables(controlGlass);

            Application.Run(controlGlass);
        }
    }
}
