using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pc_dcsWatcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string progName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
            if (Process.GetProcessesByName(progName).Count() > 1)
            {
                MessageBox.Show(string.Format("{0} is already running.", progName), 
                        "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PcdcsWatcher());
        }
    }
}
