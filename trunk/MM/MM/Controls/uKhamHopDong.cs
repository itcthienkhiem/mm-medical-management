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
        private string _contractMemberGUID = string.Empty;
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

            lbTongTienChecklist.Text = "Tổng tiền: 0 (VNĐ)";

            //btnLuu.Enabled = false;
        }

        private void ClearDichVuLamThem()
        {
            DataTable dt = dgDichVuLamThem.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgDichVuLamThem.DataSource = null;
            }

            lbTongTienLamThem.Text = "Tổng tiền: 0 (VNĐ)";
        }

        private void UpdateGUI()
        {
            btnLuu.Enabled = AllowEdit;
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;

            dgService.ReadOnly = !AllowEdit;
            chkChecked.Enabled = AllowEdit;

            saveToolStripMenuItem.Enabled = AllowEdit;
            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;

            fixedPriceDataGridViewTextBoxColumn.Visible = Global.AllowShowServiePrice;
            colThanhTien.Visible = Global.AllowShowServiePrice;

            btnOpenPatient.Enabled = AllowOpenPatient;
            moBenhNhanToolStripMenuItem.Enabled = AllowOpenPatient;
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

        private void OnTinhTongTienDichVuLamThem()
        {
            try
            {
                DataTable dt = dgDichVuLamThem.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                {
                    lbTongTienLamThem.Text = "Tổng tiền: 0 (VNĐ)";
                    return;
                }

                double tongTien = 0;
                foreach (DataRow row in dt.Rows)
                {
                    bool daThuTien = Convert.ToBoolean(row["DaThuTien"]);
                    if (daThuTien) continue;
                    double soTien = Convert.ToDouble(row["Amount"]);
                    tongTien += soTien;
                }

                if (tongTien == 0) lbTongTienLamThem.Text = "Tổng tiền: 0 (VNĐ)";
                else lbTongTienLamThem.Text = string.Format("Tổng tiền: {0} (VNĐ)", tongTien.ToString("#,###"));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnTinhTongTienChecklist(string patientGUID)
        {
            try
            {
                DataTable dt = dgService.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                {
                    lbTongTienChecklist.Text = "Tổng tiền: 0 (VNĐ)";
                    return;
                }

                double tongTien = 0;
                foreach (DataRow row in dt.Rows)
                {
                    bool isUsing = Convert.ToBoolean(row["Using"]);
                    if (!isUsing) continue;
                    string serviceGUID = row["ServiceGUID"].ToString();
                    string nguoiChuyenNhuong = row["NguoiChuyenNhuong"] as string;

                    Result result = null;

                    if (nguoiChuyenNhuong == null || nguoiChuyenNhuong.Trim() == string.Empty)
                    {
                        result = PhieuThuHopDongBus.GetThanhTienDichVuKhamTheoHopDong(_hopDongGUID, serviceGUID, patientGUID);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuHopDongBus.GetGiaDichVuKhamTheoHopDong"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetGiaDichVuKhamTheoHopDong"));
                            return;
                        }
                    }
                    else
                    {
                        result = PhieuThuHopDongBus.GetThanhTienDichVuKhamChuyenNhuong(_hopDongGUID, serviceGUID, patientGUID);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuHopDongBus.GetThanhTienDichVuKhamChuyenNhuong"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetThanhTienDichVuKhamChuyenNhuong"));
                            return;
                        }
                    }

                    double soTien = Convert.ToDouble(result.QueryResult);
                    tongTien += soTien;
                }

                if (tongTien == 0) lbTongTienChecklist.Text = "Tổng tiền: 0 (VNĐ)";
                else lbTongTienChecklist.Text = string.Format("Tổng tiền: {0} (VNĐ)", tongTien.ToString("#,###"));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
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

                OnTinhTongTienChecklist(patientGUID);
                //btnLuu.Enabled = AllowEdit;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
            }
        }

        private void OnDisplayDichVuLamThem(string contractMemberGUID)
        {
            ClearDichVuLamThem();
            chkChecked2.Checked = false;
            Result result = DichVuLamThemBus.GetDichVuLamThemList(contractMemberGUID);
            if (result.IsOK)
            {
                dgDichVuLamThem.DataSource = result.QueryResult;

                OnTinhTongTienDichVuLamThem();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DichVuLamThemBus.GetDichVuLamThemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DichVuLamThemBus.GetDichVuLamThemList"));
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
            else
            {
                MsgBox.Show(this.Text, "Đã lưu thành công.", IconType.Information);
                _isSaved = true;
                OnTinhTongTienChecklist(_patientGUID);
            }

            
        }

        private void OnAddDichVuLamThem()
        {
            if (_contractMemberGUID == string.Empty) return;
            dlgAddDichVuLamThem dlg = new dlgAddDichVuLamThem(_contractMemberGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgDichVuLamThem.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["DichVuLamThemGUID"] = dlg.DichVuLamThem.DichVuLamThemGUID.ToString();
                newRow["ContractMemberGUID"] = _contractMemberGUID;
                newRow["ServiceGUID"] = dlg.DichVuLamThem.ServiceGUID.ToString();
                newRow["Name"] = dlg.ServiceName;
                newRow["FixedPrice"] = dlg.DichVuLamThem.Price;
                newRow["Discount"] = dlg.DichVuLamThem.Discount;
                newRow["ActiveDate"] = dlg.DichVuLamThem.ActiveDate;
                newRow["Amount"] = dlg.DichVuLamThem.Price - ((dlg.DichVuLamThem.Price * dlg.DichVuLamThem.Discount) / 100);
                newRow["DaThuTien"] = dlg.DichVuLamThem.DaThuTien;

                if (dlg.DichVuLamThem.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.DichVuLamThem.CreatedDate;

                if (dlg.DichVuLamThem.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.DichVuLamThem.CreatedBy.ToString();

                if (dlg.DichVuLamThem.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.DichVuLamThem.UpdatedDate;

                if (dlg.DichVuLamThem.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.DichVuLamThem.UpdatedBy.ToString();

                if (dlg.DichVuLamThem.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.DichVuLamThem.DeletedDate;

                if (dlg.DichVuLamThem.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.DichVuLamThem.DeletedBy.ToString();

                newRow["Status"] = dlg.DichVuLamThem.Status;

                dt.Rows.Add(newRow);

                OnTinhTongTienDichVuLamThem();
            }
        }

        private void OnEditDichVuLamThem()
        {
            if (dgDichVuLamThem.SelectedRows == null || dgDichVuLamThem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ làm thêm cần sửa.", IconType.Information);
                return;
            }

            DataRow drDichVuLamThem = (dgDichVuLamThem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drDichVuLamThem == null) return;
            if (_contractMemberGUID == string.Empty) return;

            dlgAddDichVuLamThem dlg = new dlgAddDichVuLamThem(_contractMemberGUID, drDichVuLamThem);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                drDichVuLamThem["ServiceGUID"] = dlg.DichVuLamThem.ServiceGUID.ToString();
                drDichVuLamThem["Name"] = dlg.ServiceName;
                drDichVuLamThem["FixedPrice"] = dlg.DichVuLamThem.Price;
                drDichVuLamThem["Discount"] = dlg.DichVuLamThem.Discount;
                drDichVuLamThem["ActiveDate"] = dlg.DichVuLamThem.ActiveDate;
                drDichVuLamThem["Amount"] = dlg.DichVuLamThem.Price - ((dlg.DichVuLamThem.Price * dlg.DichVuLamThem.Discount) / 100);
                drDichVuLamThem["DaThuTien"] = dlg.DichVuLamThem.DaThuTien;

                if (dlg.DichVuLamThem.CreatedDate.HasValue)
                    drDichVuLamThem["CreatedDate"] = dlg.DichVuLamThem.CreatedDate;

                if (dlg.DichVuLamThem.CreatedBy.HasValue)
                    drDichVuLamThem["CreatedBy"] = dlg.DichVuLamThem.CreatedBy.ToString();

                if (dlg.DichVuLamThem.UpdatedDate.HasValue)
                    drDichVuLamThem["UpdatedDate"] = dlg.DichVuLamThem.UpdatedDate;

                if (dlg.DichVuLamThem.UpdatedBy.HasValue)
                    drDichVuLamThem["UpdatedBy"] = dlg.DichVuLamThem.UpdatedBy.ToString();

                if (dlg.DichVuLamThem.DeletedDate.HasValue)
                    drDichVuLamThem["DeletedDate"] = dlg.DichVuLamThem.DeletedDate;

                if (dlg.DichVuLamThem.DeletedBy.HasValue)
                    drDichVuLamThem["DeletedBy"] = dlg.DichVuLamThem.DeletedBy.ToString();

                drDichVuLamThem["Status"] = dlg.DichVuLamThem.Status;

                OnTinhTongTienDichVuLamThem();
            }
        }

        private void OnDeleteDichVuLamThem()
        {
            List<string> deletedKeyList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgDichVuLamThem.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKeyList.Add(row["DichVuLamThemGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKeyList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ làm thêm mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = DichVuLamThemBus.DeleteDichVuLamThem(deletedKeyList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }

                        OnTinhTongTienDichVuLamThem();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("DichVuLamThemBus.DeleteDichVuLamThem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("DichVuLamThemBus.DeleteDichVuLamThem"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ làm thêm cần xóa.", IconType.Information);
        }

        private void OnOpenPatient()
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.", IconType.Information);
                return;
            }

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
            _contractMemberGUID = row["ContractMemberGUID"].ToString();
            OnDisplayCheckList(_patientGUID);
            OnDisplayDichVuLamThem(_contractMemberGUID);
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
            OnAddDichVuLamThem();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditDichVuLamThem();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteDichVuLamThem();
        }

        private void dgDichVuLamThem_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditDichVuLamThem();
        }

        private void chkChecked2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgDichVuLamThem.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked2.Checked;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCheckList();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddDichVuLamThem();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditDichVuLamThem();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteDichVuLamThem();
        }

        private void moBenhNhanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnOpenPatient();
        }

        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            OnOpenPatient();
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
