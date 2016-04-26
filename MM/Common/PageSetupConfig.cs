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
using System.IO;
using System.Xml.Serialization;

namespace MM.Common
{
    public class PageSetupConfig
    {
        #region Members
        private List<PageSetup> _pageSetupList = new List<PageSetup>();
        #endregion

        #region Constructor
        public PageSetupConfig()
        {

        }
        #endregion

        #region Properties
        [XmlElement("PageSetupList", typeof(PageSetup))]
        public List<PageSetup> PageSetupList
        {
            get { return _pageSetupList; }
            set { _pageSetupList = value; }
        }
        #endregion

        #region Methods
        public PageSetup GetPageSetup(string template)
        {
            foreach (PageSetup p in _pageSetupList)
            {
                if (p.Template.ToLower() == template.ToLower())
                    return p;
            }

            return null;
        }

        public void AddPageSetup(PageSetup p)
        {
            _pageSetupList.Add(p);
        }

        public void ClearAll()
        {
            _pageSetupList.Clear();
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
                s = new XmlSerializer(typeof(PageSetupConfig));
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
                s = new XmlSerializer(typeof(PageSetupConfig));
                sr = new StreamReader(fileName);
                PageSetupConfig pageSetupConfig = (PageSetupConfig)s.Deserialize(sr);
                _pageSetupList = pageSetupConfig.PageSetupList;
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
