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
