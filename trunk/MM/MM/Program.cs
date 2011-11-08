using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MM.Common;

namespace MM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);   
            }
        }
    }
}
