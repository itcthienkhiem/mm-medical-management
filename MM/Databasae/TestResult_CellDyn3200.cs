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

namespace MM.Databasae
{
    public class TestResult_CellDyn3200
    {
        #region Members
        private KetQuaXetNghiem_CellDyn3200 _ketQuaXetNghiem = new KetQuaXetNghiem_CellDyn3200();
        private List<ChiTietKetQuaXetNghiem_CellDyn3200> _chiTietKetQuaXetNghiem = new List<ChiTietKetQuaXetNghiem_CellDyn3200>();
        #endregion

        #region Constructor
        public TestResult_CellDyn3200()
        {

        }
        #endregion

        #region Properties
        public KetQuaXetNghiem_CellDyn3200 KetQuaXetNghiem
        {
            get { return _ketQuaXetNghiem; }
            set { _ketQuaXetNghiem = value; }
        }

        public List<ChiTietKetQuaXetNghiem_CellDyn3200> ChiTietKetQuaXetNghiem
        {
            get { return _chiTietKetQuaXetNghiem; }
            set { _chiTietKetQuaXetNghiem = value; }
        }
        #endregion
    }
}
