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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uPhongChoList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPhongChoList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsEnableBtnRaPhongCho
        {
            set 
            { 
                btnDelete.Enabled = value;
                chkChecked.Enabled = value;

                if (!value)
                {
                    dgPatient.DataSource = null;
                    StopTimer();
                }
                else
                    StartTimer();
            }
        }
        #endregion

        #region UI Command
        public void StartTimer()
        {
            _timer.Enabled = true;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
            _timer.Enabled = false;
        }

        private void RefreshNo()
        {
            int stt = 1;
            foreach (DataGridViewRow row in dgPatient.Rows)
            {
                row.Cells["STT"].Value = stt++;
            }
        }

        public void ClearData()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgPatient.DataSource = null;
            }
        }

        private void OnDisplayPhongChoList()
        {
            Result result = PhongChoBus.GetPhongChoList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    
                    DataTable dt = dgPatient.DataSource as DataTable;
                    if (dt == null)
                        dgPatient.DataSource = result.QueryResult;
                    else
                    {
                        int selectedIndex = 0;
                        if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                            selectedIndex = dgPatient.SelectedRows[0].Index;

                        DataTable dt2 = result.QueryResult as DataTable;
                        foreach (DataRow row in dt2.Rows)
                        {
                            string patientGUID = row["PatientGUID"].ToString();
                            DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", patientGUID));
                            if (rows != null && rows.Length > 0)
                            {
                                row["Checked"] = rows[0]["Checked"];
                            }
                        }

                        ClearData();
                        dgPatient.DataSource = dt2;

                        if (selectedIndex <= dgPatient.RowCount - 1)
                        {
                            dgPatient.CurrentCell = dgPatient[1, selectedIndex];
                            dgPatient.Rows[selectedIndex].Selected = true;
                        }
                    }

                    RefreshNo();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhongChoBus.GetPhongChoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhongChoBus.GetPhongChoList"));
            }
        }

        private void OnRaPhongCho()
        {
            List<string> deletedPhongChoList = new List<string>();
            DataTable dt = dgPatient.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedPhongChoList.Add(row["PhongChoGUID"].ToString());
                }
            }

            if (deletedPhongChoList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn cho những bệnh nhân đã đánh dấu ra khỏi phòng chờ ?") == DialogResult.Yes)
                {
                    Result result = PhongChoBus.DeletePhongCho(deletedPhongChoList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhongChoBus.DeletePhongCho"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhongChoBus.DeletePhongCho"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân ra khỏi phòng chờ.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void _timer_Tick(object sender, EventArgs e)
        {
            OnDisplayPhongChoList();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnRaPhongCho();
        }

        private void dgPatient_DoubleClick(object sender, EventArgs e)
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0) return;

            DataRow patientRow = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;

            if (patientRow != null)
            {
                string patientGUID = patientRow["PatientGUID"].ToString();
                DataRow[] rows = Global.dtOpenPatient.Select(string.Format("PatientGUID='{0}'", patientGUID));
                if (rows == null || rows.Length <= 0)
                {
                    DataRow newRow = Global.dtOpenPatient.NewRow();
                    newRow["PatientGUID"] = patientRow["PatientGUID"];
                    newRow["FileNum"] = patientRow["FileNum"];
                    newRow["FullName"] = patientRow["FullName"];
                    newRow["GenderAsStr"] = patientRow["GenderAsStr"];
                    newRow["DobStr"] = patientRow["DobStr"];
                    newRow["IdentityCard"] = patientRow["IdentityCard"];
                    newRow["WorkPhone"] = patientRow["WorkPhone"];
                    newRow["Mobile"] = patientRow["Mobile"];
                    newRow["Email"] = patientRow["Email"];
                    newRow["Address"] = patientRow["Address"];
                    newRow["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                    Global.dtOpenPatient.Rows.Add(newRow);
                    base.RaiseOpentPatient(newRow);
                }
                else
                {
                    rows[0]["PatientGUID"] = patientRow["PatientGUID"];
                    rows[0]["FileNum"] = patientRow["FileNum"];
                    rows[0]["FullName"] = patientRow["FullName"];
                    rows[0]["GenderAsStr"] = patientRow["GenderAsStr"];
                    rows[0]["DobStr"] = patientRow["DobStr"];
                    rows[0]["IdentityCard"] = patientRow["IdentityCard"];
                    rows[0]["WorkPhone"] = patientRow["WorkPhone"];
                    rows[0]["Mobile"] = patientRow["Mobile"];
                    rows[0]["Email"] = patientRow["Email"];
                    rows[0]["Address"] = patientRow["Address"];
                    rows[0]["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                    base.RaiseOpentPatient(rows[0]);
                }
            }
        }

        private void raPhongChoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnRaPhongCho();
        }
        #endregion

        
    }
}
