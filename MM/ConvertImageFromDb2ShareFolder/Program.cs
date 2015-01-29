using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Transactions;
using System.Text;
using System.IO;
using MM.Common;
using MM.Databasae;

namespace ConvertImageFromDb2ShareFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(Global.AppConfig))
            {
                Configuration.LoadData(Global.AppConfig);

                object obj = Configuration.GetValues(Const.ServerNameKey);
                if (obj != null) Global.ConnectionInfo.ServerName = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.DatabaseNameKey);
                if (obj != null) Global.ConnectionInfo.DatabaseName = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.AuthenticationKey);
                if (obj != null) Global.ConnectionInfo.Authentication = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.UserNameKey);
                if (obj != null) Global.ConnectionInfo.UserName = Convert.ToString(obj);

                obj = Configuration.GetValues(Const.PasswordKey);
                if (obj != null)
                {
                    string password = Convert.ToString(obj);
                    RijndaelCrypto crypto = new RijndaelCrypto();
                    Global.ConnectionInfo.Password = crypto.Decrypt(password);
                }

                obj = Configuration.GetValues(Const.ShareFolderKey);
                if (obj != null) Global.ShareFolder = obj.ToString();

                if (!Directory.Exists(Global.ShareFolder))
                {
                    Console.WriteLine(string.Format("Share folder: '{0}' is not exists", Global.ShareFolder));
                    return;
                }

                if (!Utility.ValidateNetworkPath(Global.ShareFolder))
                {
                    Console.WriteLine(string.Format("Share folder: '{0}' must be the network path", Global.ShareFolder));
                    return;
                }

                Console.WriteLine("Converting Ket Qua Noi Soi...");
                ConvertKetQuaNoiSoi2ShareFolder();

                Console.WriteLine("Converting Ket Qua Sieu Am...");
                ConvertKetQuaSieuAm2ShareFolder();

                Console.WriteLine("Converting Ket Qua Soi CTC...");
                ConvertKetQuaSoiCTC2ShareFolder();
            }
        }

        static void ConvertKetQuaNoiSoi2ShareFolder()
        {
            MMOverride db = null;
            try
            {
                db = new MMOverride();
                bool isSubmit = false;
                foreach (var kq in db.KetQuaNoiSois)
                {
                    string key = kq.KetQuaNoiSoiGUID.ToString();
                    if (kq.Hinh1 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_1.png", key));
                        Utility.SaveImage(kq.Hinh1.ToArray(), fileName);
                        kq.ImageName1 = string.Format("{0}_1.png", key);
                        //kq.Hinh1 = null;
                        isSubmit = true;
                    }

                    if (kq.Hinh2 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_2.png", key));
                        Utility.SaveImage(kq.Hinh2.ToArray(), fileName);
                        kq.ImageName2 = string.Format("{0}_2.png", key);
                        //kq.Hinh2 = null;
                        isSubmit = true;
                    }

                    if (kq.Hinh3 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_3.png", key));
                        Utility.SaveImage(kq.Hinh3.ToArray(), fileName);
                        kq.ImageName3 = string.Format("{0}_3.png", key);
                        //kq.Hinh3 = null;
                        isSubmit = true;
                    }

                    if (kq.Hinh4 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_4.png", key));
                        Utility.SaveImage(kq.Hinh4.ToArray(), fileName);
                        kq.ImageName4 = string.Format("{0}_4.png", key);
                        //kq.Hinh4 = null;
                        isSubmit = true;
                    }
                }

                if (isSubmit) db.SubmitChanges();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                Console.WriteLine(se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        static void ConvertKetQuaSieuAm2ShareFolder()
        {
            MMOverride db = null;
            try
            {
                db = new MMOverride();
                bool isSubmit = false;
                foreach (var kq in db.KetQuaSieuAms)
                {
                    string key = kq.KetQuaSieuAmGUID.ToString();
                    if (kq.Hinh1 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_1.png", key));
                        Utility.SaveImage(kq.Hinh1.ToArray(), fileName);
                        kq.ImageName1 = string.Format("{0}_1.png", key);
                        //kq.Hinh1 = null;
                        isSubmit = true;
                    }

                    if (kq.Hinh2 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_2.png", key));
                        Utility.SaveImage(kq.Hinh2.ToArray(), fileName);
                        kq.ImageName2 = string.Format("{0}_2.png", key);
                        //kq.Hinh2 = null;
                        isSubmit = true;
                    }
                }

                if (isSubmit) db.SubmitChanges();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                Console.WriteLine(se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        static void ConvertKetQuaSoiCTC2ShareFolder()
        {
            MMOverride db = null;
            try
            {
                db = new MMOverride();
                bool isSubmit = false;
                foreach (var kq in db.KetQuaSoiCTCs)
                {
                    string key = kq.KetQuaSoiCTCGUID.ToString();
                    if (kq.Hinh1 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_1.png", key));
                        Utility.SaveImage(kq.Hinh1.ToArray(), fileName);
                        kq.ImageName1 = string.Format("{0}_1.png", key);
                        //kq.Hinh1 = null;
                        isSubmit = true;
                    }

                    if (kq.Hinh2 != null)
                    {
                        string fileName = Path.Combine(Global.ShareFolder, string.Format("{0}_2.png", key));
                        Utility.SaveImage(kq.Hinh2.ToArray(), fileName);
                        kq.ImageName2 = string.Format("{0}_2.png", key);
                        //kq.Hinh2 = null;
                        isSubmit = true;
                    }
                }

                if (isSubmit) db.SubmitChanges();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                Console.WriteLine(se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
    }
}
