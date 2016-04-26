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
    public class MailTemplate
    {
        #region Members
        public int TemplateKey = 0;
        public string TemplateName = string.Empty;
        public string Subject = string.Empty;
        public string Body = string.Empty;
        #endregion

        #region Constructor
        public MailTemplate()
        {

        }
        #endregion
    }

    public class MailTemplateList
    {
        #region Members
        [XmlElement("TemplateList", typeof(MailTemplate))]
        public List<MailTemplate> TemplateList = new List<MailTemplate>();
        #endregion

        #region Constructor
        public MailTemplateList()
        {

        }
        #endregion

        #region Methods
        public MailTemplate GetMailTemplate(string templateName)
        {
            foreach (var template in TemplateList)
            {
                if (template.TemplateName.Trim().ToUpper() == templateName.Trim().ToUpper())
                    return template;
            }

            return null;
        }

        public bool CheckTemplateNameExist(string templateName, int templateKey)
        {
            foreach (var template in TemplateList)
            {
                if (template.TemplateKey != templateKey && 
                    template.TemplateName.Trim().ToUpper() == templateName.Trim().ToUpper())
                    return true;
            }

            return false;
        }

        public int GetNextTemplateKey()
        {
            int key = 0;
            foreach (var template in TemplateList)
            {
                if (key < template.TemplateKey)
                    key = template.TemplateKey;
            }

            return key + 1;
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
                s = new XmlSerializer(typeof(MailTemplateList));
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
                s = new XmlSerializer(typeof(MailTemplateList));
                sr = new StreamReader(fileName);
                MailTemplateList templateList = (MailTemplateList)s.Deserialize(sr);
                TemplateList = templateList.TemplateList;
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
