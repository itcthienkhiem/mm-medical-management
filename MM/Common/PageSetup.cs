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

namespace MM.Common
{
    public class PageSetup
    {
        #region Members
        private double _const = 72;
        private string _template = string.Empty;
        private double _leftMargin = 0;
        private double _topMargin = 0;
        private double _rightMargin = 0;
        private double _bottomMargin = 0;
        private double _headerMargin = 0;
        private double _footerMargin = 0;
        #endregion

        #region Contructor
        public PageSetup()
        {

        }
        #endregion

        #region Properties
        public string Template
        {
            get { return _template; }
            set { _template = value; }
        }

        public double LeftMargin
        {
            get { return _leftMargin; }
            set { _leftMargin = value; }
        }

        public double TopMargin
        {
            get { return _topMargin; }
            set { _topMargin = value; }
        }

        public double RightMargin
        {
            get { return _rightMargin; }
            set { _rightMargin = value; }
        }

        public double BottomMargin
        {
            get { return _bottomMargin; }
            set { _bottomMargin = value; }
        }

        public double HeaderMargin
        {
            get { return _headerMargin; }
            set { _headerMargin = value; }
        }

        public double FooterMargin
        {
            get { return _footerMargin; }
            set { _footerMargin = value; }
        }
        #endregion

        #region Methods
        public double GetTopMargin()
        {
            return _topMargin * _const;
        }

        public double GetLeftMargin()
        {
            return _leftMargin * _const;
        }

        public double GetRightMargin()
        {
            return _rightMargin * _const;
        }

        public double GetBottomMargin()
        {
            return _bottomMargin * _const;
        }
        #endregion
    }
}
