using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using Microsoft.SqlServer.Management.Smo;

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

        public static void GetSurNameFirstNameFromFullName(string fullName, ref string surName, ref string firstName)
        {
            fullName = fullName.Trim();
            int index = fullName.LastIndexOf(" ");

            if (index >= 0)
            {
                surName = fullName.Substring(0, index);
                firstName = fullName.Substring(index + 1, fullName.Length - index - 1);
            }
            else
            {
                surName = string.Empty;
                firstName = fullName;
            }
        }

        public static int GetAge(string dobStr)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/MM/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/MM/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/M/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/M/yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/MM/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/MM/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d/M/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd/M/yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-MM-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-MM-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-M-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-M-yyyy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-MM-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-MM-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "d-M-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                DateTime dt = DateTime.ParseExact(dobStr, "dd-M-yy", null);
                return DateTime.Now.Year - dt.Year;
            }
            catch
            {
            }

            try
            {
                int year = int.Parse(dobStr);
                return DateTime.Now.Year - year;
            }
            catch
            {
            }

            return 0;
        }

        public static bool IsValidDOB(string dobStr)
        {
            try
            {
                DateTime.ParseExact(dobStr, "dd/MM/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/MM/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/M/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd/M/yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd/MM/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/MM/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d/M/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd/M/yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-MM-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-MM-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-M-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-M-yyyy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-MM-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-MM-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "d-M-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                DateTime.ParseExact(dobStr, "dd-M-yy", null);
                return true;
            }
            catch
            {
            }

            try
            {
                int year = int.Parse(dobStr);
                if (year > 0 && year < DateTime.Now.Year) return true;
            }
            catch
            {
            }
            
            return false;
        }

        public static List<string> GetSQLServerInstances()
        {
            List<string> instances = new List<string>();
            DataTable dtSQLServer = SmoApplication.EnumAvailableSqlServers(false);

            foreach (DataRow row in dtSQLServer.Rows)
            {
                string serverName = row[0].ToString().Trim();
                if (serverName == string.Empty) continue;
                instances.Add(serverName);
            }

            return instances;
        }

        public static string ConvertVNI2Unicode(string strInput)
        {
            string c = "";

            bool db = false;

            int[] maAcii = new int[134] { 7845, 7847, 7849, 7851, 7853, 226, 7843, 227, 7841, 7855, 7857, 7859, 7861, 7863, 259, 250, 249, 7911, 361, 7909, 7913, 7915, 7917, 7919, 7921, 432, 7871, 7873, 7875, 7877, 7879, 234, 233, 232, 7867, 7869, 7865, 7889, 7891, 7893, 7895, 7897, 7887, 245, 7885, 7899, 7901, 7903, 7905, 7907, 417, 237, 236, 7881, 297, 7883, 253, 7923, 7927, 7929, 7925, 273, 7844, 7846, 7848, 7850, 7852, 194, 7842, 195, 7840, 7854, 7856, 7858, 7860, 7862, 258, 218, 217, 7910, 360, 7908, 7912, 7914, 7916, 7918, 7920, 431, 7870, 7872, 7874, 7876, 7878, 202, 201, 200, 7866, 7868, 7864, 7888, 7890, 7892, 7894, 7896, 7886, 213, 7884, 7898, 7900, 7902, 7904, 7906, 416, 205, 204, 7880, 296, 7882, 221, 7922, 7926, 7928, 7924, 272, 225, 224, 244, 243, 242, 193, 192, 212, 211, 210 };
            string[] Vni = new string[134]{"aá", "aà", "aå", "aã", "aä", "aâ", "aû", "aõ", "aï", "aé", "aè",
                            "aú", "aü", "aë", "aê", "uù", "uø", "uû", "uõ", "uï", "öù", "öø", "öû", "öõ",
                            "öï", "ö", "eá", "eà", "eå", "eã", "eä", "eâ", "eù", "eø", "eû", "eõ", "eï",
                            "oá", "oà", "oå", "oã", "oä", "oû", "oõ", "oï", "ôù", "ôø",
                            "ôû", "ôõ", "ôï", "ô", "í", "ì", "æ", "ó", "ò", "yù", "yø", "yû", "yõ", "î",
                            "ñ", "AÁ", "AÀ", "AÅ", "AÃ", "AÄ", "AÂ", "AÛ", "AÕ",
                            "AÏ", "AÉ", "AÈ", "AÚ", "AÜ", "AË", "AÊ", "UÙ", "UØ", "UÛ", "UÕ",
                            "UÏ", "ÖÙ", "ÖØ", "ÖÛ", "ÖÕ", "ÖÏ", "Ö", "EÁ", "EÀ", "EÅ",
                            "EÃ", "EÄ", "EÂ", "EÙ", "EØ", "EÛ", "EÕ", "EÏ", "OÁ", "OÀ", "OÅ",
                            "OÃ", "OÄ", "OÛ", "OÕ", "OÏ", "ÔÙ", "ÔØ", "ÔÛ",
                            "ÔÕ", "ÔÏ", "Ô", "Í", "Ì", "Æ", "Ó", "Ò", "YÙ", "YØ", "YÛ", "YÕ",
                            "Î", "Ñ", "aù", "aø", "oâ", "où", "oø", "AÙ", "AØ", "OÂ", "OÙ", "OØ"};

            string result = strInput;
            for (int i = 0; i < 134; i++)
            {
                result = result.Replace(Vni[i], Convert.ToChar(maAcii[i]).ToString());
            }

            return result;
        }

        public static string GetCode(string prefix, int value, int lenght)
        {
            string strValue = value.ToString();
            if (strValue.Length > lenght)
                return string.Format("{0}{1}", prefix, value);

            string s = string.Empty;
            int index = lenght - 1;
            for (int i = strValue.Length - 1; i >= 0; i--)
            {
                s = strValue[i].ToString() + s;
                index--;                      
            }

            for (int i = 0; i <= index; i++)
                s = "0" + s;

            return string.Format("{0}{1}", prefix, s);
        }

        private static string docso(int i, int x, string n)
        {

            string s = "";
            switch (x)
            {
                case 0: if (i % 3 == 0 && (n.Substring(n.Length - i + 1, 2) != "00"))
                        s = "không ";
                    else s = "";
                    break;
                case 1:
                    if (i % 3 == 2)
                        s = "";
                    else
                    {
                        if ((n.Length >= i + 1 && (n[n.Length - i - 1] == '1' || n[n.Length - i - 1] == '0')) || n.Length == i || i % 3 == 0)
                        {
                            s = "một ";
                        }
                        else
                        {
                            s = "mốt ";
                        }
                    }
                    break;
                case 2:
                    s = "hai ";
                    break;
                case 3:
                    s = "ba ";
                    break;
                case 4:
                    s = "bốn ";
                    break;
                case 5:
                    if (n.Length != i && i % 3 == 1 && n.Substring(n.Length - i - 1, 1) != "0")
                        s = "lăm ";
                    else
                        s = "năm ";
                    break;
                case 6:
                    s = "sáu ";
                    break;
                case 7:
                    s = "bảy ";
                    break;
                case 8:
                    s = "tám ";
                    break;
                case 9:
                    s = "chín ";
                    break;
            }
            return s;
        }

        private static string hang(int i, int x, string n)
        {
            string s = "";
            int t = i % 3;
            switch (t)
            {
                case 0: if (n.Substring(n.Length - i, 3) != "000")
                        s = "trăm ";
                    else s = "";
                    break;
                case 1:
                    if (i % 9 == 1)
                    {
                        if (i - 1 == 0)
                            s = "";
                        else
                            s = "tỷ ";
                    }
                    else if (i % 6 == 1)
                        if (n.Length > 9 && n.Substring(n.Length - i - 2, 3) == "000")
                            s = "";
                        else
                            s = "triệu ";
                    else
                        if (n.Length > 6 && n.Substring(n.Length - i - 2, 3) == "000")
                            s = "";
                        else
                            s = "ngàn ";
                    break;
                case 2:
                    if (x == 0 && n.Substring(n.Length - i + 1, 1) != "0")
                        s = "linh ";
                    else
                        if (n.Substring(n.Length - i, 2) == "00")
                            s = "";
                        else
                        {
                            if (i % 3 == 2 && n[n.Length - i] == '1')
                                s = "mười ";
                            else
                                s = "mươi ";
                        }
                    break;
            }
            return s;
        }

        public static string ReadNumberAsString(long so)
        {
            int i;
            string s="";
            string n = so.ToString();
            int[] A = new int[n.Length + 1];
            for (i = n.Length; i > 0; i--)
            {
                A[i] = Int32.Parse(n.Substring(n.Length - i, 1));
                s += docso(i, A[i], n) + hang(i, A[i], n);
            }
            s = s + " đồng";
            //capital first letter
            if (s.Length > 0)
            {
                s = char.ToUpper(s[0]) + s.Substring(1);
            }
            return s;
        }
    }
}


