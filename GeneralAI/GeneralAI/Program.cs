using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GeneralAI
{
    static class Program
    {
        static public Panel panel;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            panel = new Panel();

            Application.Run(panel);
        }
    }
}
