using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace MM.Common
{
    public class Utility
    {
        #region WriteToTraceLog
        public static string fpTraceLog = "";
        public static string GenTraceLogFilename()
        {
            const long maxsize = 1024 * 1024;//1MB
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOG")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOG"));

            if (fpTraceLog == "")
                fpTraceLog = String.Format("{0}\\LOG\\TraceLog_{1}_{2}.txt", AppDomain.CurrentDomain.BaseDirectory, System.Diagnostics.Process.GetCurrentProcess().Id, DateTime.Now.ToString("yy_MM_dd_HH_mm_ss"));
            else if (File.Exists(fpTraceLog))
            {
                //check size
                FileInfo ofi = new FileInfo(fpTraceLog);
                if (ofi.Length > maxsize)
                {
                    //create new file name
                    fpTraceLog = String.Format("{0}\\LOG\\TraceLog_{1}_{2}.txt", AppDomain.CurrentDomain.BaseDirectory, System.Diagnostics.Process.GetCurrentProcess().Id, DateTime.Now.ToString("yy_MM_dd_HH_mm_ss"));
                }
                ofi = null;
            }

            if (!File.Exists(fpTraceLog))
            {
                FileStream fs = null;
                StreamWriter sw = null;
                Assembly executingAssembly = null;
                try
                {
                    fs = new FileStream(fpTraceLog, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);

                    executingAssembly = Assembly.GetExecutingAssembly();

                    //Add Version
                    string sversion = string.Format("==========================================================\r\n TraceLog Version on Date : {0}\r\n==========================================================\r\n", File.GetLastWriteTime(AppDomain.CurrentDomain.BaseDirectory).ToString("yyyy-MM-dd"));
                    sw.WriteLine(sversion);
                    sversion = null;
                }
                catch
                {
                }
                finally
                {
                    if (sw != null)
                        sw.Close();
                    if (fs != null)
                        fs.Close();

                    executingAssembly = null;
                }
            }

            return fpTraceLog;
        }

        public static void WriteToTraceLog(object my_obj, string Desc)
        {
            TextWriterTraceListener oListener = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(GenTraceLogFilename(), FileMode.Append, FileAccess.Write);
                oListener = new TextWriterTraceListener(fs);
                System.Diagnostics.Trace.Listeners.Clear();
                System.Diagnostics.Trace.Listeners.Add(oListener);

                Exception EX = null;
                if (my_obj != null)
                    EX = (Exception)my_obj;


                oListener.WriteLine(DateTime.Now.ToString() + "----------------------------------------------------");

                if (EX != null)
                {
                    oListener.WriteLine("(Message) : " + EX.Message);
                    if (EX.TargetSite != null)
                        oListener.WriteLine("(TargetSite) : " + EX.TargetSite.Name);
                    oListener.WriteLine("(StackTrace) : " + EX.StackTrace);
                    oListener.WriteLine("(ToString) : " + EX.ToString());
                }

                oListener.WriteLine("(Desc) : " + Desc);
                //System.Diagnostics.Trace.WriteLine("(Schema) : " + GlobalData.m_SchemaCtrl.m_strDocumentFileName);
                oListener.WriteLine(DateTime.Now.ToString() + "----------------------------------------------------");

                System.Diagnostics.Trace.Flush();
            }
            catch //( Exception ex )
            {
                //string a ="";
            }
            finally
            {
                System.Diagnostics.Trace.Listeners.Remove(oListener);
                if (oListener != null)
                    oListener.Close();
                if (fs != null)
                    fs.Close();
            }
        }

        public static void WriteToTraceLog(string Desc)
        {
            WriteToTraceLog(null, Desc);
        }

        #endregion

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.
	    	(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";

            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = false;

            if (string.IsNullOrEmpty(email))
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(email);
            }

            return valid;
        }

        public static bool IsValidPassword(string password)
        {
            string pattern = "(?!^[0-9][a-zA-Z]*$).{4,12}$";
            Regex check = new Regex(pattern);
            bool valid = false;

            if (string.IsNullOrEmpty(password))
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(password);
            }

            return valid;
        }

        public static bool IsValidUsername(string username)
        {
            string pattern = "(?!^[0-9][a-zA-Z]*$).{2,12}$";
            Regex check = new Regex(pattern);
            bool valid = false;

            if (string.IsNullOrEmpty(username))
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(username);
            }

            return valid;
        }
    }
}
