using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MM.Common
{
    public class HDGTGTSettings
    {
        #region Members
        private double _rowHeight = 12.75;
        private string _page1Range = "A1:A47";
        private string _page2Range = "A48:A78";
        #endregion

        #region Constructor
        public HDGTGTSettings()
        {

        }
        #endregion

        #region Properties
        public double RowHeight
        {
            get { return _rowHeight; }
            set { _rowHeight = value; }
        }

        public string Page1Range
        {
            get { return _page1Range; }
            set { _page1Range = value; }
        }

        public string Page2Range
        {
            get { return _page2Range; }
            set { _page2Range = value; }
        }
        #endregion

        #region Serialize & Deserialize
        public bool Serialize(string fileName)
        {
            XmlSerializer x = null;
            StreamWriter sw = null;
            try
            {
                x = new XmlSerializer(this.GetType());
                sw = new StreamWriter(fileName);
                x.Serialize(sw, this);
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

                if (x != null)
                    x = null;
            }
        }

        public bool Deserialize(string fileName)
        {
            XmlSerializer x = null;
            StreamReader sr = null;
            try
            {
                x = new XmlSerializer(this.GetType());
                sr = new StreamReader(fileName);
                HDGTGTSettings settings = (HDGTGTSettings)x.Deserialize(sr);
                this._rowHeight = settings._rowHeight;
                this._page1Range = settings._page1Range;
                this._page2Range = settings._page2Range;
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

                if (x != null)
                    x = null;
            }
        }
        #endregion
    }
}
