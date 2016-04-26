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
using MM.Bussiness;
using MM.Common;
using MM.Exports;
using System.Threading;

namespace MM.Controls
{
    public partial class uCapNhatNhanhChecklist : uBase
    {
        #region Members
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string, DataRow>();
        private string _name = string.Empty;
        private int _type = 0;
        private string _hopDongGUID = Guid.Empty.ToString();
        private int _type2 = 0;
        private DataTable _dtTemp = null;
        private bool _isAscending = true;
        private DateTime _beginDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        #endregion

        #region Constructor
        public uCapNhatNhanhChecklist()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnEdit.Enabled = AllowEdit;
            editToolStripMenuItem.Enabled = AllowEdit;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            ClearData();

            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                DataTable dtContract = result.QueryResult as DataTable;
                DataRow newRow = dtContract.NewRow();
                newRow["CompanyContractGUID"] = Guid.Empty.ToString();
                newRow["ContractName"] = string.Empty;
                dtContract.Rows.InsertAt(newRow, 0);
                cboHopDong.DataSource = dtContract;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
            }
        }

        public void ClearData()
        {
            DataTable dt = cboHopDong.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            //cboHopDong.DataSource = null;

            ClearData2();
        }

        private void ClearData2()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgPatient.DataSource = null;
        }

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;

                if (cboHopDong.SelectedValue != null)
                    _hopDongGUID = cboHopDong.SelectedValue.ToString();
                else
                    _hopDongGUID = Guid.Empty.ToString();

                _name = txtSearchPatient.Text.Trim();
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;

                if (raTatCa.Checked) _type2 = -1;
                else if (raChuaDenKham.Checked) _type2 = 0;
                else if (raKhamChuaDu.Checked) _type2 = 1;
                else if (raDaKhamDu.Checked) _type2 = 2;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["PatientGUID"].ToString();
                if (_dictPatient.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnDisplayPatientList()
        {
            lock (ThisLock)
            {
                Result result = CompanyContractBus.GetDanhSachNhanVien(_hopDongGUID, _type2, _name, _type);
                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData2();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgPatient.DataSource = dt;

                        lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
                }
            }
        }

        private void CapNhatAllChecklistAsThread()
        {
            try
            {
                if (_hopDongGUID == string.Empty || _hopDongGUID == Guid.Empty.ToString()) return;

                List<string> deletedPatientList = new List<string>();
                List<DataRow> deletedRows = _dictPatient.Values.ToList();
                foreach (DataRow row in deletedRows)
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    deletedPatientList.Add(patientGUID);
                }

                if (deletedPatientList.Count > 0)
                {
                    if (MsgBox.Question(Application.ProductName, "Bạn có muốn cập nhật tất cả checklist của những nhân viên được chọn ?") == DialogResult.Yes)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(OnCapNhatAllChecklistProc), deletedPatientList);
                        base.ShowWaiting();
                        SearchAsThread();
                    }
                }
                else
                    MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhân viên cần cập nhật.", IconType.Information);
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnCapNhatAllChecklist(List<string> checkedPatientList)
        {
            foreach (string patientGUID in checkedPatientList)
            {
                Result result = CompanyContractBus.GetCheckList(_hopDongGUID, patientGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
                    return;
                }

                DataTable dtChecklist = result.QueryResult as DataTable;
                List<DataRow> checkListRows = new List<DataRow>();
                foreach (DataRow row in dtChecklist.Rows)
                {
                    bool isUsing = Convert.ToBoolean(row["Using"]);
                    if (isUsing) continue;
                    row["Using"] = true;
                    checkListRows.Add(row);
                }

                if (checkListRows.Count <= 0) continue;
                result = ServiceHistoryBus.UpdateChecklist(patientGUID, _hopDongGUID, _beginDate, _endDate, checkListRows);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("ServiceHistoryBus.UpdateChecklist"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.UpdateChecklist"));
                    return;
                }
            }

            MsgBox.Show(this.Text, "Đã cập nhật thành công.", IconType.Information);
        }

        private void ValidateHopDong(string hopDongGUID)
        {
            if (hopDongGUID == string.Empty || hopDongGUID == Guid.Empty.ToString())
            {
                lbThongBao.Text = string.Empty;
                dgPatient.ReadOnly = false;
                chkChecked.Enabled = true;
                panel2.Enabled = true;
                return;
            }

            _beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            _endDate = Global.MaxDateTime;
            string tenHopDong = string.Empty;

            DataTable dt = cboHopDong.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", hopDongGUID));
                if (rows == null || rows.Length <= 0)
                {
                    lbThongBao.Text = string.Empty;
                    dgPatient.ReadOnly = false;
                    chkChecked.Enabled = true;
                    panel2.Enabled = true;
                    return;
                }

                _beginDate = Convert.ToDateTime(rows[0]["BeginDate"]);
                _beginDate = new DateTime(_beginDate.Year, _beginDate.Month, _beginDate.Day, 0, 0, 0);
                if (rows[0]["EndDate"] != null && rows[0]["EndDate"] != DBNull.Value)
                {
                    _endDate = Convert.ToDateTime(rows[0]["EndDate"]);
                    _endDate = new DateTime(_endDate.Year, _endDate.Month, _endDate.Day, 23, 59, 59);
                }

                tenHopDong = rows[0]["ContractName"].ToString();
            }

            DateTime dtNow = DateTime.Now;
            if (dtNow >= _beginDate && dtNow <= _endDate)
            {
                lbThongBao.Text = string.Empty;
                dgPatient.ReadOnly = false;
                chkChecked.Enabled = true;
                panel2.Enabled = true;
            }
            else if (dtNow > _endDate)
            {
                lbThongBao.Text = string.Format("Hợp đồng đã kết thúc ngày {0}.", _endDate.ToString("dd/MM/yyyy"));
                dgPatient.ReadOnly = true;
                chkChecked.Enabled = false;
                panel2.Enabled = false;
            }
            else
            {
                lbThongBao.Text = string.Format("Hợp đồng này bắt đầu ngày {0}, chưa tới ngày khám.", _beginDate.ToString("dd/MM/yyyy"));
                dgPatient.ReadOnly = true;
                chkChecked.Enabled = false;
                panel2.Enabled = false;
            }
        }
        #endregion

        #region Window Event Handlers
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapNhatAllChecklistAsThread();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            CapNhatAllChecklistAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string patientGUID = row["PatientGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictPatient.ContainsKey(patientGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictPatient.ContainsKey(patientGUID))
                    {
                        _dictPatient.Remove(patientGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void cboHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hopDongGUID = string.Empty;
            if (cboHopDong.SelectedValue != null)
                hopDongGUID = cboHopDong.SelectedValue.ToString();

            ValidateHopDong(hopDongGUID);

            SearchAsThread();
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void raTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (raTatCa.Checked) SearchAsThread();
        }

        private void chkChuaDenKham_CheckedChanged(object sender, EventArgs e)
        {
            if (raChuaDenKham.Checked) SearchAsThread();
        }

        private void raKhamChuaDu_CheckedChanged(object sender, EventArgs e)
        {
            if (raKhamChuaDu.Checked) SearchAsThread();
        }

        private void raDaKhamDu_CheckedChanged(object sender, EventArgs e)
        {
            if (raDaKhamDu.Checked) SearchAsThread();
        }

        private void dgPatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgPatient.DataSource as DataTable;
                DataTable newDataSource = null;

                if (_isAscending)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }

                dgPatient.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending = false;
        }

        private void dgPatient_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgPatient.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictPatient.ContainsKey(patientGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictPatient.ContainsKey(patientGUID))
                {
                    _dictPatient.Remove(patientGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayPatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnCapNhatAllChecklistProc(object state)
        {
            try
            {
                List<string> checkedPatientList = (List<string>)state;
                OnCapNhatAllChecklist(checkedPatientList);
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
