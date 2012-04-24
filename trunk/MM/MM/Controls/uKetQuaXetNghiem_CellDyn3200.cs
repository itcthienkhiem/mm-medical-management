﻿using System;
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

namespace MM.Controls
{
    public partial class uKetQuaXetNghiem_CellDyn3200 : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uKetQuaXetNghiem_CellDyn3200()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
            btnEdit.Enabled = AllowEdit;
            btnCapNhatCTKQXN.Enabled = AllowEdit;
            btnXoaCTKQXN.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaXetNghiemListProc));
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

        private void OnDisplayKetQuaXetNghiemList()
        {
            Result result = XetNghiem_CellDyn3200Bus.GetKetQuaXetNghiemList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgXetNghiem.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetKetQuaXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetKetQuaXetNghiemList"));
            }
        }

        private void OnDisplayChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID)
        {
            Result result = XetNghiem_CellDyn3200Bus.GetChiTietKetQuaXetNghiem(ketQuaXetNghiemGUID);
            if (result.IsOK)
            {
                dgChiTietKQXN.DataSource = result.QueryResult;
                RefreshHighlight();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetChiTietKetQuaXetNghiem"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetChiTietKetQuaXetNghiem"));
            }
        }

        private void RefreshHighlight()
        {
            foreach (DataGridViewRow row in dgChiTietKQXN.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                TinhTrang tinhTrang = (TinhTrang)Convert.ToByte(dr["TinhTrang"]);
                if (tinhTrang == TinhTrang.BatThuong)
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                else
                    row.DefaultCellStyle.BackColor = SystemColors.Window;
            }
        }

        private void OnCapNhatBenhNhan()
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 xét nghiệm để cập nhật bệnh nhân.", IconType.Information);
                return;
            }

            DataRow row = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (row == null) return;

            dlgSelectPatient dlg = new dlgSelectPatient();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    KetQuaXetNghiem_CellDyn3200 kqxn = new KetQuaXetNghiem_CellDyn3200();
                    kqxn.KQXN_CellDyn3200GUID = Guid.Parse(row["KQXN_CellDyn3200GUID"].ToString());
                    kqxn.PatientGUID = Guid.Parse(patientRow["PatientGUID"].ToString());
                    Result result = XetNghiem_CellDyn3200Bus.UpdatePatient(kqxn);
                    if (result.IsOK)
                    {
                        row["PatientGUID"] = patientRow["PatientGUID"];
                        row["FileNum"] = patientRow["FileNum"];
                        row["FullName"] = patientRow["FullName"];
                        row["DobStr"] = patientRow["DobStr"];
                        row["GenderAsStr"] = patientRow["GenderAsStr"];
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdatePatient"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdatePatient"));
                    }
                }
            }
        }

        private void OnDeleteKQXN()
        {
            List<string> deletedKQXNList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQXNList.Add(row["KQXN_CellDyn3200GUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKQXNList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những xét nghiệm bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = XetNghiem_CellDyn3200Bus.DeleteXetNghiem(deletedKQXNList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.DeleteXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.DeleteXetNghiem"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những xét nghiệm cần xóa.", IconType.Information);
        }

        private void OnDeleteChiTietKetQuaXetNghiem()
        {
            List<string> deletedCTKQXNList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgChiTietKQXN.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedCTKQXNList.Add(row["ChiTietKQXN_CellDyn3200GUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedCTKQXNList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những chi tiết kết quả xét nghiệm bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = XetNghiem_CellDyn3200Bus.DeleteChiTietKetQuaXetNghiem(deletedCTKQXNList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.DeleteChiTietKetQuaXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.DeleteChiTietKetQuaXetNghiem"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những chi tiết kết quả xét nghiệm cần xóa.", IconType.Information);
        }

        private void OnCapNhatChiSoKetQuaXetNghiem()
        {
            if (dgChiTietKQXN.SelectedRows == null || dgChiTietKQXN.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 chi tiết kết quả xét nghiệm để cập nhật.", IconType.Information);
                return;
            }

            DataRow row = (dgChiTietKQXN.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (row == null) return;

            dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200 dlg = new dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200(row);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                row["TestResult"] = dlg.ChiTietKQXN.TestResult;

                if (dlg.ChiTietKQXN.TestPercent.HasValue)
                    row["TestPercent"] = dlg.ChiTietKQXN.TestPercent.Value;
                row["TinhTrang"] = dlg.ChiTietKQXN.TinhTrang;

                if ((TinhTrang)dlg.ChiTietKQXN.TinhTrang == TinhTrang.BatThuong)
                    dgChiTietKQXN.SelectedRows[0].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                else
                    dgChiTietKQXN.SelectedRows[0].DefaultCellStyle.BackColor = SystemColors.Window;
            }
        }
        #endregion

        #region Window Event Handlers
        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dgXetNghiem_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnCapNhatBenhNhan();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnCapNhatBenhNhan();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteKQXN();
        }

        private void chkCTKQXNChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgChiTietKQXN.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCTKQXNChecked.Checked;
            }
        }

        private void dgChiTietKQXN_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnCapNhatChiSoKetQuaXetNghiem();
        }

        private void btnCapNhatCTKQXN_Click(object sender, EventArgs e)
        {
            OnCapNhatChiSoKetQuaXetNghiem();
        }

        private void btnXoaCTKQXN_Click(object sender, EventArgs e)
        {
            OnDeleteChiTietKetQuaXetNghiem();
        }

        private void raTuNgayToiNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DisplayAsThread();
        }

        private void dgXetNghiem_SelectionChanged(object sender, EventArgs e)
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                dgChiTietKQXN.DataSource = null;
                return;
            }

            DataRow row = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (row == null)
            {
                dgChiTietKQXN.DataSource = null;
                return;
            }

            OnDisplayChiTietKetQuaXetNghiem(row["KQXN_CellDyn3200GUID"].ToString());
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaXetNghiemListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaXetNghiemList();
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