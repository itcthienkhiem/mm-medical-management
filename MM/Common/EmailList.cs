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
    public class EmailList
    {
        #region Members
        [XmlElement("Emails", typeof(Email))]
        public List<Email> Emails = new List<Email>();
        #endregion

        #region Constructor
        public EmailList()
        {

        }
        #endregion

        #region Methods
        public List<string> GetEmails()
        {
            List<string> emails = new List<string>();
            foreach (var email in Emails)
            {
                emails.Add(email.EmailAddress);
            }

            return emails;
        }

        public bool CheckEmailExist(string email)
        {
            foreach (var e in Emails)
            {
                if (e.EmailAddress.Trim().ToLower() == email.Trim().ToLower())
                    return true;
            }

            return false;
        }

        public void Add(string email)
        {
            if (!CheckEmailExist(email))
            {
                Email em = new Email();
                em.Name = email;
                em.EmailAddress = email;
                Emails.Add(em);
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
                s = new XmlSerializer(typeof(EmailList));
                s.Serialize(sw, this);
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
                s = new XmlSerializer(typeof(EmailList));
                sr = new StreamReader(fileName);
                EmailList emailList = (EmailList)s.Deserialize(sr);
                Emails = emailList.Emails;
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

    public class Email
    {
        #region Members
        public string Name = string.Empty;
        public string EmailAddress = string.Empty;
        #endregion

        #region Constructor
        public Email()
        {

        }
        #endregion
    }
}
