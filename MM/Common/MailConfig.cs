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
    public class MailConfig
    {
        #region Members
        private string _senderMail = string.Empty;
        private bool _useSMTPServer = true;
        private string _server = string.Empty;
        private int _port = 0;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _signature = string.Empty;
        #endregion

        #region Constructors
        public MailConfig()
        {

        }
        #endregion

        #region Properties
        public string SenderMail
        {
            get { return _senderMail; }
            set { _senderMail = value; }
        }

        public bool UseSMTPServer
        {
            get { return _useSMTPServer; }
            set { _useSMTPServer = value; }
        }

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
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
                s = new XmlSerializer(typeof(MailConfig));

                RijndaelCrypto crypto = new RijndaelCrypto();
                string pass = _password;
                _password = crypto.Encrypt(_password);
                s.Serialize(sw, this);
                _password = pass;
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
                s = new XmlSerializer(typeof(MailConfig));
                sr = new StreamReader(fileName);
                MailConfig mailConfig = (MailConfig)s.Deserialize(sr);
                _senderMail = mailConfig.SenderMail;
                _useSMTPServer = mailConfig.UseSMTPServer;
                _server = mailConfig.Server;
                _port = mailConfig.Port;
                _username = mailConfig.Username;
                RijndaelCrypto crypto = new RijndaelCrypto();
                _password = crypto.Decrypt(mailConfig.Password);
                _signature = mailConfig.Signature;

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
