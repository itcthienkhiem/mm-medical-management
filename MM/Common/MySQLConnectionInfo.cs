/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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

        #region Properties
        public string ConnectionString
        {
            get
            {
                return string.Format("server={0};username={2};password={3};database={1};persist security info=False",
                    Server, Database, User, Password);
            }
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
