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
    public class TVHomeConfig
    {
        #region Members
        private string _path = string.Empty;
        private bool _suDungSoiCTC = false;
        private bool _suDungSieuAm = false;
        private TVHomeImageFormat _format = TVHomeImageFormat.JPG;
        #endregion

        #region Constructor
        public TVHomeConfig()
        {

        }
        #endregion

        #region Properties
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public bool SuDungSoiCTC
        {
            get { return _suDungSoiCTC; }
            set { _suDungSoiCTC = value; }
        }

        public bool SuDungSieuAm
        {
            get { return _suDungSieuAm; }
            set { _suDungSieuAm = value; }
        }

        public TVHomeImageFormat Format
        {
            get { return _format; }
            set { _format = value; }
        }
        #endregion
    }
}
