using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MM.Common
{
    public class MySQLConnectionInfo
    {
        #region Members
        public string Server = "result.ris.com.au";
        public string Database = "risadmin_OnlineResult";
        public string User = "risadmin_result";
        public string Password = "Password001qazxdr5";
        #endregion

        #region Constructor
        public MySQLConnectionInfo()
        {
            
        }
        #endregion

        #region Serialize & Deserialize
        public bool Serialize(string fileName)
        {
            XmlSerializer s = null;
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName);
                s = new XmlSerializer(typeof(MySQLConnectionInfo));

                RijndaelCrypto crypto = new RijndaelCrypto();
                string pass = Password;
                Password = crypto.Encrypt(Password);
                s.Serialize(sw, this);
                Password = pass;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }

                if (s != null)
                    s = null;
            }
        }

        public bool Deserialize(string fileName)
        {
            XmlSerializer s = null;
            StreamReader sr = null;

            try
            {
                s = new XmlSerializer(typeof(MySQLConnectionInfo));
                sr = new StreamReader(fileName);
                MySQLConnectionInfo connectionInfo = (MySQLConnectionInfo)s.Deserialize(sr);
                Server = connectionInfo.Server;
                Database = connectionInfo.Database;
                User = connectionInfo.User;
                Password = connectionInfo.Password;
                RijndaelCrypto crypto = new RijndaelCrypto();
                Password = crypto.Decrypt(connectionInfo.Password);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }

                if (s != null)
                    s = null;
            }
        }
        #endregion
    }
}
