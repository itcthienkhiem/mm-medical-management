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
    public partial class uKhamHopDong : uBase
    {
        #region Members
        private string _name = string.Empty;
        private int _type = 0;
        private int _doiTuong = 0;
        private string _hopDongGUID = string.Empty;
        private string _patientGUID = string.Empty;
        private string _tenNhanVien = string.Empty;
        private DateTime _beginDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        private bool _isSaved = true;
        #endregion

        #region Constructor
        public uKhamHopDong()
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

        private void ClearCheckList()
        {
            DataTable dtOld = dgService.DataSource as DataTable;
            if (dtOld != null)
            {
                dtOld.Rows.Clear();
                dtOld.Clear();
                dtOld = null;
                dgService.DataSource = null;
            }

            btnLuu.Enabled = false;
        }

        private void UpdateGUI()
        {
            btnLuu.Enabled = AllowEdit;
            dgService.ReadOnly = !AllowEdit;
            chkChecked.Enabled = AllowEdit;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
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
            _beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            _endDate = Global.MaxDateTime;
            string tenHopDong = string.Empty;

            DataTable dt = cboMaHopDong.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", hopDongGUID));
                if (rows == null || rows.Length <= 0) return string.Empty;

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
                dgService.ReadOnly = false;
                chkChecked.Enabled = true;
                chkChecked2.Enabled = true;
                panelDichVuLamThem.Enabled = true;
                dgDichVuLamThem.ReadOnly = false;
            }
            else if (dtNow > _endDate)
            {
                lbThongBao.Text = string.Format("Hợp đồng đã kết thúc ngày {0}.", _endDate.ToString("dd/MM/yyyy"));
                dgService.ReadOnly = true;
                chkChecked.Enabled = false;
                chkChecked2.Enabled = false;
                panelDichVuLamThem.Enabled = false;
                dgDichVuLamThem.ReadOnly = true;
            }
            else
            {
                lbThongBao.Text = string.Format("Hợp đồng này bắt đầu ngày {0}, chưa tới ngày khám.", _beginDate.ToString("dd/MM/yyyy"));
                dgService.ReadOnly = true;
                chkChecked.Enabled = false;
                chkChecked2.Enabled = false;
                panelDichVuLamThem.Enabled = false;
                dgDichVuLamThem.ReadOnly = true;
            }

            return tenHopDong;
        }

        private void OnDisplayDanhSachNhanVien()
        {
            lock (ThisLock)
            {
                Result result = CompanyContractBus.GetContractMemberList(_hopDongGUID, _name, _type, _doiTuong);

                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        dgPatient.DataSource = dt;
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
                else _doiTuong = 3;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDanhSachNhanVientProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayCheckList(string patientGUID)
        {
            ClearCheckList();
            chkChecked.Checked = false;
            Result result = CompanyContractBus.GetCheckList(_hopDongGUID, patientGUID);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                dgService.DataSource = dt;

                foreach (DataGridViewRow row in dgService.Rows)
                {
                    DataRow drRow = (row.DataBoundItem as DataRowView).Row;
                    if (drRow["NguoiChuyenNhuong"] != null && drRow["NguoiChuyenNhuong"] != DBNull.Value &&
                        drRow["NguoiChuyenNhuong"].ToString().Trim() != string.Empty)
                    {
                        DataGridViewDisableCheckBoxCell cell = row.Cells["Using"] as DataGridViewDisableCheckBoxCell;
                        cell.Enabled = false;
                    }
                }

                btnLuu.Enabled = AllowEdit;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
            }
        }

        public void ConfirmSaveCheckList()
        {
            if (_patientGUID != string.Empty && !_isSaved)
            {
                if (MsgBox.Question(Application.ProductName, string.Format("Bạn có muốn lưu checklist của nhân viên: '{0}' ?", _tenNhanVien)) == DialogResult.Yes)
                {
                    SaveCheckList();
                }
            }

            _isSaved = true;
        }

        private void SaveCheckList()
        {
            List<DataRow> checkListRows = new List<DataRow>();
            foreach (DataGridViewRow row in dgService.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["Using"] as DataGridViewDisableCheckBoxCell;
                if (!cell.Enabled) continue;

                DataRow drRow = (row.DataBoundItem as DataRowView).Row;
                checkListRows.Add(drRow);
            }

            if (checkListRows.Count <= 0) return;

            Result result = ServiceHistoryBus.UpdateChecklist(_patientGUID, _hopDongGUID, _beginDate, _endDate, checkListRows);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServiceHistoryBus.UpdateChecklist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.UpdateChecklist"));
            }

            _isSaved = true;
        }
        #endregion

        #region Window Event Handlers
        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;

            ConfirmSaveCheckList();

            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();
            txtTenHopDong.Text = GetTenHopDong(_hopDongGUID);

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

        private void dgPatient_SelectionChanged(object sender, EventArgs e)
        {
            ConfirmSaveCheckList();

            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                ClearCheckList();
                return;
            }

            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            _patientGUID = row["PatientGUID"].ToString();
            _tenNhanVien = row["FullName"].ToString();
            OnDisplayCheckList(_patientGUID);
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                if (row["NguoiChuyenNhuong"] != null && row["NguoiChuyenNhuong"] != DBNull.Value && row["NguoiChuyenNhuong"].ToString().Trim() != string.Empty)
                    continue;

                row["Using"] = chkChecked.Checked;
            }

            _isSaved = false;
        }

        private void dgService_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataRow row = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            bool isUsing = Convert.ToBoolean(row["Using"]);
            DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell)dgService.Rows[e.RowIndex].Cells[0];
            if (!cell.Enabled) return;
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);
            if (isUsing != isChecked)
            {
                _isSaved = false;
                row["Using"] = isChecked;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveCheckList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void chkChecked2_CheckedChanged(object sender, EventArgs e)
        {

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
