using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Action action = new Action(StartClient);
 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Task.Factory.StartNew(action);
            Application.Run(new FormServer());
        }

        static void StartClient()
        {
            FormClient formClient = new FormClient();
            formClient.ShowDialog();
        }
    }
}
