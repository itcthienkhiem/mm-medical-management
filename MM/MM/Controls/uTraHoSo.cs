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
using MM.Dialogs;
using MM.Exports;
using System.IO;

namespace MM.Controls
{
    public partial class uTraHoSo : uBase
    {
        #region Members
        private string _name = string.Empty;
        private int _type = 0;
        private int _doiTuong = 0;
        private int _traHoSo = 0; //0: Tất cả; 1: Đã trả; 2: Chưa trả
        private string _hopDongGUID = string.Empty;
        private string _patientGUID = string.Empty;
        private string _tenNhanVien = string.Empty;
        private string _contractMemberGUID = string.Empty;
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string, DataRow>();
        private DataTable _dtTemp = null;
        private bool _isAscending = true;
        private bool _flag = false;
        #endregion

        #region Constructor
        public uTraHoSo()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dtOld = dgPatient.DataSource as DataTable;
            if (dtOld != null)
            {
                dtOld.Rows.Clear();
                dtOld.Clear();
                dtOld = null;
                dgPatient.DataSource = null;
            }
        }

        private void UpdateGUI()
        {
            btnTraHoSo.Enabled = AllowEdit;
            btnHuyTraHoSo.Enabled = AllowEdit;
            traHoSoToolStripMenuItem.Enabled = AllowEdit;
            huyTraHoSoToolStripMenuItem.Enabled = AllowEdit;

            if (AllowEdit)
            {
                if (raTatCa2.Checked)
                {
                    btnTraHoSo.Enabled = true;
                    btnHuyTraHoSo.Enabled = true;
                    traHoSoToolStripMenuItem.Enabled = true;
                    huyTraHoSoToolStripMenuItem.Enabled = true;
                }
                else if (raDaTra.Checked)
                {
                    btnTraHoSo.Enabled = true;
                    btnHuyTraHoSo.Enabled = true;
                    traHoSoToolStripMenuItem.Enabled = true;
                    huyTraHoSoToolStripMenuItem.Enabled = true;
                }
                else
                {
                    btnTraHoSo.Enabled = true;
                    btnHuyTraHoSo.Enabled = false;
                    traHoSoToolStripMenuItem.Enabled = true;
                    huyTraHoSoToolStripMenuItem.Enabled = false;
                }
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayHopDongListProc));
                base.ShowWaiting();
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

        private void OnDisplayHopDongList()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    cboMaHopDong.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetNoCompletedContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetNoCompletedContractList"));
            }
        }

        private string GetTenHopDong(string hopDongGUID)
        {
            string tenHopDong = string.Empty;

            DataTable dt = cboMaHopDong.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", hopDongGUID));
                if (rows == null || rows.Length <= 0) return string.Empty;

                tenHopDong = rows[0]["ContractName"].ToString();
            }

            return tenHopDong;
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

        private void OnDisplayDanhSachNhanVien()
        {
            lock (ThisLock)
            {
                Result result = CompanyContractBus.GetContractMemberList(_hopDongGUID, _name, _type, _doiTuong, _traHoSo);

                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        
                        dgPatient.DataSource = dt;
                        UpdateChecked(dt);
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractMemberList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
                }
            }
        }

        public override void SearchAsThread()
        {
            try
            {
                _name = txtSearchPatient.Text;
                if (chkMaBenhNhan.Checked) _type = 1;
                else _type = 0;

                if (raAll.Checked) _doiTuong = 0;
                else if (raNam.Checked) _doiTuong = 1;
                else if (raNu.Checked) _doiTuong = 2;
                else if (raNuCoGiaDinh.Checked) _doiTuong = 3;
                else if (raNamTren40.Checked) _doiTuong = 4;
                else _doiTuong = 5;

                if (raTatCa2.Checked) _traHoSo = 0;
                else if (raDaTra.Checked) _traHoSo = 1;
                else _traHoSo = 2;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDanhSachNhanVientProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnCheckAll(bool check)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = check;
                string patientGUID = row["PatientGUID"].ToString();
                if (check)
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

        private void OnTraHoSo()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                dlgNgayTraHoSo dlg = new dlgNgayTraHoSo();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    foreach (DataRow row in checkedRows)
                    {
                        string contractMemberGUID = row["ContractMemberGUID"].ToString();
                        string tenHopDong = cboMaHopDong.Text;
                        string tenBenhNhan = row["FullName"].ToString();
                        string patientGUID = row["PatientGUID"].ToString();
                        Result result = CompanyContractBus.TraHoSo(contractMemberGUID, patientGUID, tenBenhNhan, _hopDongGUID, tenHopDong, true, dlg.NgayTra);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.TraHoSo"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.TraHoSo"));
                            return;
                        }
                    }

                    MsgBox.Show(Application.ProductName, "Đã trả hồ sơ hoàn tất.", IconType.Information);
                    SearchAsThread();
                }
                
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần trả hồ sơ.", IconType.Information);
        }

        private void OnHuyTraHoSo()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn hủy trả hồ những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        string contractMemberGUID = row["ContractMemberGUID"].ToString();
                        string tenHopDong = cboMaHopDong.Text;
                        string tenBenhNhan = row["FullName"].ToString();
                        string patientGUID = row["PatientGUID"].ToString();
                        Result result = CompanyContractBus.TraHoSo(contractMemberGUID, patientGUID, tenBenhNhan, _hopDongGUID, tenHopDong, false, DateTime.Now);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.TraHoSo"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.TraHoSo"));
                            return;
                        }
                    }

                    MsgBox.Show(Application.ProductName, "Đã hủy trả hồ sơ hoàn tất.", IconType.Information);
                    SearchAsThread();
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần hủy trả hồ sơ.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;

            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();
            txtTenHopDong.Text = GetTenHopDong(_hopDongGUID);

            _dictPatient.Clear();
            if (_dtTemp != null) _dtTemp.Rows.Clear();
            SearchAsThread();
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            if (raAll.Checked) SearchAsThread();
        }

        private void raNam_CheckedChanged(object sender, EventArgs e)
        {
            if (raNam.Checked) SearchAsThread();
        }

        private void raNu_CheckedChanged(object sender, EventArgs e)
        {
            if (raNu.Checked) SearchAsThread();
        }

        private void raNuCoGiaDinh_CheckedChanged(object sender, EventArgs e)
        {
            if (raNuCoGiaDinh.Checked) SearchAsThread();
        }

        private void raNamTren40_CheckedChanged(object sender, EventArgs e)
        {
            if (raNamTren40.Checked) SearchAsThread();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (_flag) return;
            OnCheckAll(chkChecked.Checked);
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

        private void raNuTren40_CheckedChanged(object sender, EventArgs e)
        {
            if (raNuTren40.Checked) SearchAsThread();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (raTatCa2.Checked)
            {
                SearchAsThread();

                if (AllowEdit)
                {
                    btnTraHoSo.Enabled = true;
                    btnHuyTraHoSo.Enabled = true;
                    traHoSoToolStripMenuItem.Enabled = true;
                    huyTraHoSoToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void raDaTra_CheckedChanged(object sender, EventArgs e)
        {
            if (raDaTra.Checked)
            {
                SearchAsThread();

                if (AllowEdit)
                {
                    btnTraHoSo.Enabled = true;
                    btnHuyTraHoSo.Enabled = true;
                    traHoSoToolStripMenuItem.Enabled = true;
                    huyTraHoSoToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void raChuaTra_CheckedChanged(object sender, EventArgs e)
        {
            if (raChuaTra.Checked)
            {
                SearchAsThread();

                if (AllowEdit)
                {
                    btnTraHoSo.Enabled = true;
                    btnHuyTraHoSo.Enabled = false;
                    traHoSoToolStripMenuItem.Enabled = true;
                    huyTraHoSoToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void traHoSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnTraHoSo();
        }

        private void huyTraHoSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnHuyTraHoSo();
        }

        private void btnTraHoSo_Click(object sender, EventArgs e)
        {
            OnTraHoSo();
        }

        private void btnHuyTraHoSo_Click(object sender, EventArgs e)
        {
            OnHuyTraHoSo();
        }
        #endregion

        #region Working Thread
        private void OnDisplayHopDongListProc(object state)
        {
            try
            {
                OnDisplayHopDongList();
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

        private void OnDisplayDanhSachNhanVientProc(object state)
        {
            try
            {
                OnDisplayDanhSachNhanVien();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        

        

        

        
    }
}
