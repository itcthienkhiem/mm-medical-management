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
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgSelectPatient : dlgBase
    {
        #region Constructor
        public dlgSelectPatient(PatientSearchType patientSearchType)
        {
            InitializeComponent();
            _uSearchPatient.PatientSearchType = patientSearchType;
            if (patientSearchType == PatientSearchType.BenhNhanKhongThanThuoc) 
                this.IsMulti = true;
            else
                _uSearchPatient.OnOpenPatientEvent += new MM.Controls.OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _uSearchPatient.PatientRow; }
        }

        public bool IsMulti
        {
            get { return _uSearchPatient.IsMulti; }
            set { _uSearchPatient.IsMulti = value; }
        }

        public List<DataRow> CheckedPatientRows
        {
            get { return _uSearchPatient.CheckedPatientRows; }
        }
        #endregion

        #region Window Event Handlers
        private void _uSearchPatient_OnOpenPatient(object patientRow)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        
    }
}
