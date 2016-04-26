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
    public class PrintLabelConfig
    {
        #region Members
        private float _top_1x2 = 12;
        private float _left_1x2 = 45;

        private float _top_2x4 = 0;
        private float _left_2x4 = 8;

        private float _top_5x6 = 0;
        private float _left_5x6 = 5;

        private float _top_5x8 = 0;
        private float _left_5x8 = 8;

        private float _top_5x11 = 1;
        private float _left_5x11 = 7;
        #endregion

        #region Constructor
        public PrintLabelConfig()
        {

        }
        #endregion

        #region Properties
        public float Top_1x2
        {
            get { return _top_1x2; }
            set { _top_1x2 = value; }
        }

        public float Left_1x2
        {
            get { return _left_1x2; }
            set { _left_1x2 = value; }
        }

        public float Top_2x4
        {
            get { return _top_2x4; }
            set { _top_2x4 = value; }
        }

        public float Left_2x4
        {
            get { return _left_2x4; }
            set { _left_2x4 = value; }
        }

        public float Top_5x6
        {
            get { return _top_5x6; }
            set { _top_5x6 = value; }
        }

        public float Left_5x6
        {
            get { return _left_5x6; }
            set { _left_5x6 = value; }
        }

        public float Top_5x8
        {
            get { return _top_5x8; }
            set { _top_5x8 = value; }
        }

        public float Left_5x8
        {
            get { return _left_5x8; }
            set { _left_5x8 = value; }
        }

        public float Top_5x11
        {
            get { return _top_5x11; }
            set { _top_5x11 = value; }
        }

        public float Left_5x11
        {
            get { return _left_5x11; }
            set { _left_5x11 = value; }
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
                PrintLabelConfig printLabelConfig = (PrintLabelConfig)x.Deserialize(sr);
                this.Top_1x2 = printLabelConfig.Top_1x2;
                this.Left_1x2 = printLabelConfig.Left_1x2;

                this.Top_2x4 = printLabelConfig.Top_2x4;
                this.Left_2x4 = printLabelConfig.Left_2x4;

                this.Top_5x6 = printLabelConfig.Top_5x6;
                this.Left_5x6 = printLabelConfig.Left_5x6;

                this.Top_5x8 = printLabelConfig.Top_5x8;
                this.Left_5x8 = printLabelConfig.Left_5x8;

                this.Top_5x11 = printLabelConfig.Top_5x11;
                this.Left_5x11 = printLabelConfig.Left_5x11;
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
