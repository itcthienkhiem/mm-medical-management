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
using MM.Bussiness;
using MM.Common;
using MM.Controls;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgOpentPatient : dlgBase
    {
        #region Members
        private DataRow _patientRow = null;
        #endregion

        #region Constructor
        public dlgOpentPatient()
        {
            InitializeComponent();
            _uSearchPatient.OnOpenPatientEvent += new OpenPatientHandler(_uSearchPatient_OnOpenPatient);
            btnVaoPhongCho.Enabled = Global.AllowAddPhongCho;
            vaoPhongChoToolStripMenuItem.Enabled = Global.AllowAddPhongCho;
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        private void OnOpentPatient(DataRow patientRow)
        {
            _patientRow = patientRow;
            if (_patientRow != null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void OnVaoPhongCho()
        {
            _patientRow = _uSearchPatient.PatientRow;
            if (_patientRow != null)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn thêm bệnh nhân đã chọn vào phòng chờ ?") == DialogResult.Yes)
                {
                    List<string> addedPatientList = new List<string>();
                    addedPatientList.Add(((DataRow)_patientRow)["PatientGUID"].ToString());
                    Result result = PhongChoBus.AddPhongCho(addedPatientList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhongChoBus.AddPhongCho"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhongChoBus.AddPhongCho"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân mà bạn muốn đưa vào phòng chờ.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            OnOpentPatient(_uSearchPatient.PatientRow);
        }

        private void _uSearchPatient_OnOpenPatient(DataRow patientRow)
        {
            OnOpentPatient(patientRow);
        }

        private void btnVaoPhongCho_Click(object sender, EventArgs e)
        {
            OnVaoPhongCho();
        }

        private void openPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnOpentPatient(_uSearchPatient.PatientRow);
        }

        private void vaoPhongChoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnVaoPhongCho();
        }
        #endregion

        
    }
}
