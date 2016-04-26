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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Dialogs
{
    public partial class dlgPrintKetQuaSieuAm : dlgBase
    {
        #region Members
        private DataRow _drKetQuaSieuAm = null;
        #endregion

        #region Constructor
        public dlgPrintKetQuaSieuAm(DataRow patientRow, DataRow drKetQuaSieuAm)
        {
            InitializeComponent();

            _uPrintKetQuaSieuAm.PatientRow = patientRow;
            _drKetQuaSieuAm = drKetQuaSieuAm;
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgPrintKetQuaSieuAm_Load(object sender, EventArgs e)
        {
            _uPrintKetQuaSieuAm.PrintPreview(_drKetQuaSieuAm);
        }
        #endregion
    }
}
