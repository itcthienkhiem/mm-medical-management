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
