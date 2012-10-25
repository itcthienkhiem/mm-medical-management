using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;
using Microsoft.SqlServer.Management.Smo;
using System.ServiceProcess;
using System.Threading;
using SpreadsheetGear;

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

        public static bool IsValidDateTime(string dateTimeStr, ref string format)
        {
            List<string> dateFormats = new List<string>();
            dateFormats.Add("dd/MM/yyyy");
            dateFormats.Add("dd/MM/yy");
            dateFormats.Add("dd/M/yyyy");
            dateFormats.Add("dd/M/yy");
            dateFormats.Add("d/MM/yyyy");
            dateFormats.Add("d/MM/yy");
            dateFormats.Add("d/M/yy");
            dateFormats.Add("d/M/yyyy");

            List<string> timeFormats = new List<string>();
            timeFormats.Add("HH:mm:ss");
            timeFormats.Add("HH:mm:s");
            timeFormats.Add("HH:m:s");
            timeFormats.Add("HH:m:ss");
            timeFormats.Add("H:m:ss");
            timeFormats.Add("H:mm:ss");
            timeFormats.Add("H:mm:s");
            timeFormats.Add("H:m:s");

            foreach (var dateFormat in dateFormats)
            {
                foreach (var timeFormat in timeFormats)
                {
                    try
                    {
                        DateTime.ParseExact(dateTimeStr, string.Format("{0} {1}", dateFormat, timeFormat), null);
                        format = string.Format("{0} {1}", dateFormat, timeFormat);
                        return true;
                    }
                    catch
                    {
                    }
                }
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

        public static string ParseCoQuanEnumToName(CoQuan coQuan)
        {
            switch (coQuan)
            {
                case CoQuan.Mat:
                    return "Eyes (Mắt)";
                case CoQuan.TaiMuiHong:
                    return "Ear, Nose, Throat (Tai, mũi, họng)";
                case CoQuan.RangHamMat:
                    return "Odontology (Răng, hàm, mặt)";
                case CoQuan.HoHap:
                    return "Respiratory system (Hô hấp)";
                case CoQuan.TimMach:
                    return "Cardiovascular system (Tim mạch)";
                case CoQuan.TieuHoa:
                    return "Gastro - intestinal system (Tiêu hóa)";
                case CoQuan.TietNieuSinhDuc:
                    return "Genitourinary system (Tiết niệu, sinh dục)";
                case CoQuan.CoXuongKhop:
                    return "Musculoskeletal system (Cơ, xương, khớp)";
                case CoQuan.DaLieu:
                    return "Dermatology (Da liễu)";
                case CoQuan.ThanKinh:
                    return "Neurological system (Thần kinh)";
                case CoQuan.NoiTiet:
                    return "Endocrine system (Nội tiết)";
                case CoQuan.Khac:
                    return "Orthers (Các cơ quan khác)";
                case CoQuan.KhamPhuKhoa:
                    return "Gynecology (Khám phụ khoa)";
            }

            return string.Empty;
        }

        public static string ParseStaffTypeEnumToName(StaffType type)
        {
            switch (type)
            {
                case StaffType.BacSi:
                    return "Bác sĩ";
                case StaffType.BacSiSieuAm:
                    return "Bác sĩ siêu âm";
                case StaffType.BacSiNgoaiTongQuat:
                    return "Bác sĩ ngoại tổng quát";
                case StaffType.BacSiNoiTongQuat:
                    return "Bác sĩ nội tổng quát";
                case StaffType.DieuDuong:
                    return "Điều dưỡng";
                case StaffType.LeTan:
                    return "Lễ tân";
                case StaffType.BenhNhan:
                    return "Bệnh nhân";
                case StaffType.Admin:
                    return "Admin";
                case StaffType.XetNghiem:
                    return "Xét nghiệm";
                case StaffType.ThuKyYKhoa:
                    return "Thư ký y khoa";
                case StaffType.Sale:
                    return "Sale";
                case StaffType.KeToan:
                    return "Kế toán";
                case StaffType.BacSiPhuKhoa:
                    return "Bác sĩ phụ khoa";
                case StaffType.None:
                    return string.Empty;
            }

            return string.Empty;
        }

        public static string ParseLoaiNoiSoiEnumToName(LoaiNoiSoi type)
        {
            switch (type)
            {
                case LoaiNoiSoi.Tai:
                    return "Tai";
                case LoaiNoiSoi.Mui:
                    return "Mũi";
                case LoaiNoiSoi.Hong_ThanhQuan:
                    return "Họng - Thanh quản";
                case LoaiNoiSoi.TaiMuiHong:
                    return "Tai - Mũi - Họng";
                case LoaiNoiSoi.TongQuat:
                    return "Tổng quát";
            }

            return string.Empty;
        }

        public static int FixedPrice(int price)
        {
            int fixedPrice = 0;

            int div = price / 1000;
            int mod = price % 1000;

            if (mod == 0 || mod == 500) return price;

            if (mod < 500) fixedPrice = div * 1000 + 500;
            if (mod > 500) fixedPrice = (div + 1) * 1000;

            return fixedPrice;
        }

        public static string ParseHinhThucThanhToanToStr(PaymentType type)
        {
            if (type == PaymentType.TienMat) return "TM";
            else if (type == PaymentType.ChuyenKhoan) return "CK";
            else return "TM/CK";
        }

        public static void ResetMMSerivice()
        {
            try
            {
                string serviceName = "MMServices";
                int delaystop = 10;//second
                ServiceController sc = new ServiceController(serviceName);
                sc.Stop();//Stop target services
                Thread.Sleep(delaystop * 1000);
                sc.Start();
            }
            catch
            {
                
            }
        }

        public static void CreateFolder(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch
            {

            }
        }

        public static string GeneratePassword()
        {
            string[] a = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

            Random rnd = new Random();
            string password = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int index = rnd.Next(36);
                password += a[index];
            }

            return password;
        }

        public static string GetMaCongTy(string maBenhNhan)
        {
            string maCongTy = string.Empty;

            for (int i = 0; i < maBenhNhan.Length; i++)
            {
                char c = maBenhNhan[i];
                if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' ||
                    c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                    break;

                maCongTy += c.ToString();
            }

            return maCongTy;
        }

        public static PageSetup GetPageSetup(string template)
        {
            IWorkbook workBook = null;
            string fileName = string.Empty;
            PageSetup p = null;

            try
            {
                switch (template)
                {
                    case "Theo dõi thực hiện":
                        fileName = string.Format("{0}\\Templates\\CheckListTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chi tiết phiếu thu dịch vụ":
                        fileName = string.Format("{0}\\Templates\\ChiTietPhieuThuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Chi tiết phiếu thu thuốc":
                        fileName = string.Format("{0}\\Templates\\ChiTietPhieuThuThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách bệnh nhân đến khám":
                        fileName = string.Format("{0}\\Templates\\DanhSachBenhNhanDenKhamTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách bệnh nhân":
                        fileName = string.Format("{0}\\Templates\\DanhSachBenhNhanTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Dịch vụ hợp đồng":
                        fileName = string.Format("{0}\\Templates\\DichVuHopDongTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Dịch vụ tự túc":
                        fileName = string.Format("{0}\\Templates\\DichVuTuTucTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Doanh thu theo ngày":
                        fileName = string.Format("{0}\\Templates\\DoanhThuTheoNgayTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Giá vốn dịch vụ":
                        fileName = string.Format("{0}\\Templates\\GiaVonDichVuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Hóa đơn giá trị gia tăng":
                        fileName = string.Format("{0}\\Templates\\HDGTGTTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi thanh quản":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiHongThanhQuanTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi mũi":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiMuiTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tai mũi họng":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiMuiHongTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tai":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTaiTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi tổng quát":
                        fileName = string.Format("{0}\\Templates\\KetQuaNoiSoiTongQuatTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả nội soi cổ tử cung":
                        fileName = string.Format("{0}\\Templates\\KetQuaSoiCTCTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Khám sức khỏe tổng quát":
                        fileName = string.Format("{0}\\Templates\\KhamSucKhoeTongQuatTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Nhật kí liên hệ công ty":
                        fileName = string.Format("{0}\\Templates\\NhatKyLienHeCongTyTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Phiếu thu thuốc":
                        fileName = string.Format("{0}\\Templates\\PhieuThuThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Phiếu thu dịch vụ":
                        fileName = string.Format("{0}\\Templates\\ReceiptTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Triệu chứng":
                        fileName = string.Format("{0}\\Templates\\SymptomTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Thuốc tồn kho theo khoảng thời gian":
                        fileName = string.Format("{0}\\Templates\\ThuocTonKhoTheoKhoangThoiGianTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc":
                        fileName = string.Format("{0}\\Templates\\ToaThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc chung":
                        fileName = string.Format("{0}\\Templates\\ToaThuocChungTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Toa thuốc sản khoa":
                        fileName = string.Format("{0}\\Templates\\ToaThuocSanKhoaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Ý kiến khách hàng":
                        fileName = string.Format("{0}\\Templates\\YKienKhachHangTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả xét nghiệm CellDyn3200":
                        fileName = string.Format("{0}\\Templates\\KetQuaXetNghiemCellDyn3200Template.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Kết quả xét nghiệm sinh hóa":
                        fileName = string.Format("{0}\\Templates\\KetQuaXetNghiemSinhHoaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách dịch vụ xuất phiếu thu":
                        fileName = string.Format("{0}\\Templates\\DanhSachDichVuXuatPhieuThuTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách dịch vụ":
                        if (Global.AllowShowServiePrice)
                            fileName = string.Format("{0}\\Templates\\DanhSachDichVuCoGiaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        else
                            fileName = string.Format("{0}\\Templates\\DanhSachDichVuKhongGiaTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách nhân viên":
                        fileName = string.Format("{0}\\Templates\\DanhSachNhanVienTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Danh sách thuốc":
                        fileName = string.Format("{0}\\Templates\\DanhSachThuocTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Công tác ngoài giờ":
                        fileName = string.Format("{0}\\Templates\\CongTacNgoaiGioTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;

                    case "Lịch khám":
                        fileName = string.Format("{0}\\Templates\\LichKhamTemplate.xls", AppDomain.CurrentDomain.BaseDirectory);
                        break;
                }

                if (fileName != string.Empty && File.Exists(fileName))
                {
                    workBook = SpreadsheetGear.Factory.GetWorkbook(fileName);
                    IWorksheet workSheet = workBook.Worksheets[0];

                    p = new PageSetup();
                    p.LeftMargin = workSheet.PageSetup.LeftMargin / 72;
                    p.RightMargin = workSheet.PageSetup.RightMargin / 72;
                    p.TopMargin = workSheet.PageSetup.TopMargin / 72;
                    p.BottomMargin = workSheet.PageSetup.BottomMargin / 72;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return p;
        }

        public static string GetLoaiXetNghiem(string type)
        {
            switch (type)
            {
                case "Urine":
                    return "Nước tiểu";
                case "MienDich":
                    return "Miễn dịch";
                case "Khac":
                    return "Khác";
                case "Haematology":
                    return "Huyết học";
                case "Biochemistry":
                    return "Sinh hóa";
            }

            return string.Empty;
        }

        public static Image ParseImage(byte[] buffer, int width, int height)
        {
            Bitmap bmp = null;
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                bmp = new Bitmap(bmp, new Size(width, height));

                return bmp;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }
        }

        public static Image ParseImage(byte[] buffer)
        {
            Bitmap bmp = null;
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                
                return bmp;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }
        }

        public static byte[] GetBinaryFromImage(Image img)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.GetBuffer();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }
        }

        public static void RunPlayCapProcess(bool isShowCapture)
        {
            string path = string.Format("{0}\\PlayCap.exe", AppDomain.CurrentDomain.BaseDirectory);
            Process.Start(path, isShowCapture.ToString());
        }

        public static void KillPlayCapProcess()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName("PlayCap");
                if (processList != null && processList.Length > 0)
                {
                    foreach (Process p in processList)
                    {
                        p.Kill();
                    }
                }
            }
            catch
            {
                
            }
        }

        public static bool CheckPlayCapProcessExist()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName("PlayCap");
                if (processList != null && processList.Length > 0)
                    return true;
            }
            catch
            {
                
            }

            return false;
        }

        public static string GetDayOfWeek(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "Thứ 6";
                case DayOfWeek.Monday:
                    return "Thứ 2";
                case DayOfWeek.Saturday:
                    return "Thứ 7";
                case DayOfWeek.Sunday:
                    return "CN";
                case DayOfWeek.Thursday:
                    return "Thứ 5";
                case DayOfWeek.Tuesday:
                    return "Thứ 3";
                case DayOfWeek.Wednesday:
                    return "Thứ 4";
            }

            return string.Empty;
        }

        public static byte[] GetBytesFromFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fileName);
                int count = (int)fs.Length;
                byte[] buff = new byte[count];
                fs.Read(buff, 0, count);
                return buff;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
        }

        public static void SaveFileFromBytes(string fileName, byte[] buff)
        {
            try
            {
                File.WriteAllBytes(fileName, buff);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ExecuteFile(string fileName)
        {
            try
            {
                Process.Start(fileName);
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
    }
}


