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
    public class HDGTGTSettings
    {
        #region Members
        private double _rowHeight = 12.75;
        private string _page1Range = "A1:A47";
        private string _page2Range = "A48:A78";
        private int _rowOfPage = 47;
        #endregion

        #region Constructor
        public HDGTGTSettings()
        {

        }
        #endregion

        #region Properties
        public int RowOfPage
        {
            get { return _rowOfPage; }
            set { _rowOfPage = value; }
        }

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
                this._rowOfPage = settings._rowOfPage;
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
