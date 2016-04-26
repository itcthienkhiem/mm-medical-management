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
    public partial class uKhamHopDong : uBase
    {
        #region Members
        private string _name = string.Empty;
        private int _type = 0;
        private int _doiTuong = 0;
        private string _hopDongGUID = string.Empty;
        private string _patientGUID = string.Empty;
        private string _tenNhanVien = string.Empty;
        private DataRow _patientRow = null;
        private string _contractMemberGUID = string.Empty;
        private DateTime _beginDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        private bool _isSaved = true;
        private double _giamGiaNam = 0;
        private double _giamGiaNu = 0;
        private double _giamGiaNuCoGD = 0;
        #endregion

        #region Constructor
        public uKhamHopDong()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<DataRow> CheckedServiceRows
        {
            get
            {
                if (dgDichVuLamThem.RowCount <= 0) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgDichVuLamThem.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }
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
            btnExportReceipt.Enabled = Global.AllowExportPhieuThuDichVu;

            dgService.ReadOnly = !AllowEdit;
            chkChecked.Enabled = AllowEdit;

            saveToolStripMenuItem.Enabled = AllowEdit;
            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;

            fixedPriceDataGridViewTextBoxColumn.Visible = Global.AllowShowServiePrice;
            colThanhTien.Visible = Global.AllowShowServiePrice;
            lbTongTienLamThem.Visible = Global.AllowShowServiePrice;
            lbTongTienChecklist.Visible = Global.AllowShowServiePrice;

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

                _giamGiaNam = Convert.ToDouble(rows[0]["GiamGiaNam"]);
                _giamGiaNu = Convert.ToDouble(rows[0]["GiamGiaNu"]);
                _giamGiaNuCoGD = Convert.ToDouble(rows[0]["GiamGiaNuCoGD"]);
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
                else if (raNuCoGiaDinh.Checked) _doiTuong = 3;
                else if (raNamTren40.Checked) _doiTuong = 4;
                else _doiTuong = 5;

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

        private void OnTinhTongTienChecklist(DataRow patientRow)
        {
            try
            {
                DataTable dt = dgService.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                {
                    lbTongTienChecklist.Text = "Tổng tiền: 0 (VNĐ)";
                    return;
                }

                string patientGUID = patientRow["PatientGUID"].ToString();
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

                string gioiTinh = patientRow["GenderAsStr"].ToString();
                string tinhTrangGiaDinh = string.Empty;
                if (patientRow["Tinh_Trang_Gia_Dinh"] != null && patientRow["Tinh_Trang_Gia_Dinh"] != DBNull.Value)
                    tinhTrangGiaDinh = patientRow["Tinh_Trang_Gia_Dinh"].ToString();

                if (gioiTinh.ToLower() == "nam")
                {
                    if (_giamGiaNam > 0)
                        tongTien = Math.Round((tongTien * (100 - _giamGiaNam)) / 100, 0);
                }
                else
                {
                    if (tinhTrangGiaDinh.ToLower() == "có gia đình")
                    {
                        if (_giamGiaNuCoGD > 0)
                            tongTien = Math.Round((tongTien * (100 - _giamGiaNuCoGD)) / 100, 0);
                    }
                    else
                    {
                        if (_giamGiaNu > 0)
                            tongTien = Math.Round((tongTien * (100 - _giamGiaNu)) / 100, 0);
                    }
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

        private void OnDisplayCheckList(DataRow patientRow)
        {
            ClearCheckList();
            chkChecked.Checked = false;
            Result result = CompanyContractBus.GetCheckList(_hopDongGUID, patientRow["PatientGUID"].ToString());
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

                OnTinhTongTienChecklist(patientRow);
                //btnLuu.Enabled = AllowEdit;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
            }
        }

        private void OnDisplayDichVuLamThem(string patientGUID)
        {
            ClearDichVuLamThem();
            chkChecked2.Checked = false;
            Result result = ServiceHistoryBus.GetDichVuLamThemList(patientGUID, _hopDongGUID);
            if (result.IsOK)
            {
                dgDichVuLamThem.DataSource = result.QueryResult;
                HighlightPaidServices();
                OnTinhTongTienDichVuLamThem();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServiceHistoryBus.GetDichVuLamThemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.GetDichVuLamThemList"));
            }
        }

        public void HighlightPaidServices()
        {
            foreach (DataGridViewRow row in dgDichVuLamThem.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                bool isExported = Convert.ToBoolean(dr["IsExported"]);
                if (isExported)
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
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
                OnTinhTongTienChecklist(_patientRow);
                txtSearchPatient.Focus();
            }
        }

        private void OnAddDichVuLamThem()
        {
            if (_patientGUID == string.Empty) return;
            dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientGUID);
            dlg.HopDongGUID = _hopDongGUID;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                OnDisplayDichVuLamThem(_patientGUID);
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
            if (_patientGUID == string.Empty) return;

            dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientGUID, drDichVuLamThem, AllowEdit);
            dlg.HopDongGUID = _hopDongGUID;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                OnDisplayDichVuLamThem(_patientGUID);

                OnTinhTongTienDichVuLamThem();
            }
        }

        private void OnDeleteDichVuLamThem()
        {
            List<string> deletedServiceHistoryList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgDichVuLamThem.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    bool isExported = Convert.ToBoolean(row["IsExported"]);

                    if (!isExported)
                    {
                        deletedServiceHistoryList.Add(row["ServiceHistoryGUID"].ToString());
                        deletedRows.Add(row);
                    }
                    else
                    {
                        string srvHistoryGUID = row["ServiceHistoryGUID"].ToString();
                        Result r = ServiceHistoryBus.GetPhieuThuByServiceHistoryGUID(srvHistoryGUID);
                        if (r.IsOK)
                        {
                            string soPhieuThu = string.Empty;
                            if (r.QueryResult != null)
                            {
                                Receipt receipt = r.QueryResult as Receipt;
                                soPhieuThu = receipt.ReceiptCode;
                            }

                            string srvName = row["Name"].ToString();
                            MsgBox.Show(Application.ProductName, string.Format("Dịch vụ: '{0}' không thể xóa vì đã xuất phiếu thu ({1}). Vui lòng chọn lại.", srvName, soPhieuThu),
                                IconType.Information);
                            return;
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, r.GetErrorAsString("ServiceHistoryBus.GetPhieuThuByServiceHistoryGUID"), IconType.Error);
                            Utility.WriteToTraceLog(r.GetErrorAsString("ServiceHistoryBus.GetPhieuThuByServiceHistoryGUID"));
                            return;
                        }
                    }
                }
            }

            if (deletedServiceHistoryList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ làm thêm mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ServiceHistoryBus.DeleteServiceHistory(deletedServiceHistoryList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.DeleteServiceHistory"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.DeleteServiceHistory"));
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

        private string GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ReceiptBus.GetReceiptCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                return Utility.GetCode("PT", count + 1, 4);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ReceiptBus.GetReceiptCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptCount"));
                return string.Empty;
            }
        }

        private void OnPrintPhieuThu(string receiptGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgDichVuLamThem.RowCount <= 0) return;

            string exportFileName = string.Format("{0}\\Temp\\Receipt.xls", Application.StartupPath);
            if (ExportExcel.ExportReceiptToExcel(exportFileName, receiptGUID))
            {
                try
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                        ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.PhieuThuDichVuTemplate));
                }
                catch (Exception ex)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                }
            }
        }

        private void OnXuatPhieuThu()
        {
            if (dgDichVuLamThem.RowCount <= 0 ||
                CheckedServiceRows == null || CheckedServiceRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 dịch vụ làm thêm cần xuất phiếu thu.", IconType.Information);
                return;
            }

            List<DataRow> paidServiceList = new List<DataRow>();
            List<DataRow> noPaidServiceList = new List<DataRow>();
            List<DataRow> checkedRows = CheckedServiceRows;

            foreach (DataRow row in checkedRows)
            {
                bool isKhamTuTuc = Convert.ToBoolean(row["KhamTuTuc"]);
                string serviceName = row["Name"].ToString();
                bool isExported = Convert.ToBoolean(row["IsExported"]);
                if (!isExported)
                    noPaidServiceList.Add(row);
                else
                    paidServiceList.Add(row);
            }

            if (paidServiceList.Count > 0)
            {
                MsgBox.Show(Application.ProductName, "(Một số) dịch vụ đã xuất phiếu thu rồi. Vui lòng kiểm tra lại.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xuất phiếu thu ?") == DialogResult.No) return;

            dlgConfirmThuTien dlg = new dlgConfirmThuTien(noPaidServiceList);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (paidServiceList.Count <= 0)
                {
                    List<ReceiptDetail> receiptDetails = new List<ReceiptDetail>();
                    foreach (DataGridViewRow row in dlg.DataGridViewDetail.Rows)
                    {
                        ReceiptDetail detail = new ReceiptDetail();
                        detail.ServiceHistoryGUID = Guid.Parse(row.Tag.ToString());
                        detail.SoLuong = Convert.ToInt32(row.Cells[2].Value);
                        detail.CreatedDate = DateTime.Now;
                        detail.CreatedBy = Guid.Parse(Global.UserGUID);
                        detail.Status = (byte)Status.Actived;
                        receiptDetails.Add(detail);
                    }

                    Receipt receipt = new Receipt();
                    receipt.ReceiptCode = GenerateCode();
                    receipt.PatientGUID = Guid.Parse(_patientGUID);
                    receipt.ReceiptDate = dlg.NgayXuat;
                    receipt.Status = (byte)Status.Actived;
                    receipt.CreatedDate = DateTime.Now;
                    receipt.CreatedBy = Guid.Parse(Global.UserGUID);
                    receipt.IsExportedInVoice = false;
                    receipt.ChuaThuTien = !dlg.DaThuTien;
                    receipt.Notes = dlg.GhiChu;
                    receipt.LyDoGiam = dlg.LyDoGiam;

                    Result result = ReceiptBus.InsertReceipt(receipt, receiptDetails);
                    if (result.IsOK)
                    {
                        OnDisplayDichVuLamThem(_patientGUID);

                        if (Global.AllowPrintPhieuThuDichVu)
                        {
                            if (MsgBox.Question(Application.ProductName, "Bạn có muốn in phiếu thu ?") == DialogResult.Yes)
                                OnPrintPhieuThu(receipt.ReceiptGUID.ToString());
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.InsertReceipt"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.InsertReceipt"));
                    }
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

        private void raNamTren40_CheckedChanged(object sender, EventArgs e)
        {
            if (raNamTren40.Checked) SearchAsThread();
        }

        private void raNuTren40_CheckedChanged(object sender, EventArgs e)
        {
            if (raNuTren40.Checked) SearchAsThread();
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
            _patientRow = row;
            _patientGUID = row["PatientGUID"].ToString();
            _tenNhanVien = row["FullName"].ToString();
            OnDisplayCheckList(row);
            OnDisplayDichVuLamThem(_patientGUID);
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

        private void xuatPhieuThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnXuatPhieuThu();
        }

        private void btnExportReceipt_Click(object sender, EventArgs e)
        {
            OnXuatPhieuThu();
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
