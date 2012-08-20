using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlayCap
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                bool isShowCapture = true;
                if (args != null && args.Length > 0)
                    isShowCapture = Convert.ToBoolean(args[0]);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PlayCapForm(isShowCapture));
            }
            catch (Exception e)
            {
                
            }
            
        }
    }
}
