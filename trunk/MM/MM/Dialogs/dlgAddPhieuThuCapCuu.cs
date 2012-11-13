using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddPhieuThuCapCuu : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private bool _flag = true;
        private PhieuThuCapCuu _phieuThuCapCuu = new PhieuThuCapCuu();
        private DataRow _drPhieuThu = null;
        private string _tenCongTy = string.Empty;
        private DataTable _dataSourceBenhNhan = null;
        private ComboBox _cboBox = null;
        private bool _isExportedInvoice = false;
        private DataTable _dtKhoCapCuu = null;
        #endregion

        #region Constructor
        public dlgAddPhieuThuCapCuu()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddPhieuThuCapCuu(DataRow drPhieuThu, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _drPhieuThu = drPhieuThu;

            if (!allowEdit)
            {
                btnOK.Enabled = false;
                chkDaThuTien.Enabled = false;
            }

            cboMaToaCapCuu.Visible = false;
            txtMaToaCapCuu.Visible = true;

            //if (Global.StaffType != StaffType.Admin)
            //{
            //    btnOK.Enabled = false;
            //    chkDaThuTien.Enabled = false;
            //}
            //else
            //    chkDaXuatHD.Enabled = true;

            dgChiTiet.AllowUserToAddRows = false;
            dgChiTiet.AllowUserToDeleteRows = false;
            dgChiTiet.ReadOnly = true;
            txtMaPhieuThu.ReadOnly = true;
            dtpkNgayThu.Enabled = false;
            txtMaBenhNhan.ReadOnly = true;
            txtTenBenhNhan.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtGhiChu.ReadOnly = true;
            txtLyDoGiam.ReadOnly = true;
            btnChonBenhNhan.Enabled = false;
            
            this.Text = "Xem phieu thu";
        }
        #endregion

        #region Properties
        public PhieuThuCapCuu PhieuThuCapCuu
        {
            get { return _phieuThuCapCuu; }
        }

        public bool IsExportedInvoice
        {
            get { return _isExportedInvoice; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            if (_isNew)
                btnExportInvoice.Enabled = false;
            else
            {
                bool isExportedInvoice = Convert.ToBoolean(_drPhieuThu["IsExported"]);
                //btnExportInvoice.Enabled = Global.AllowExportHoaDonCap && !isExportedInvoice;
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = PhieuThuCapCuuBus.GetPhieuThuCapCuuCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaPhieuThu.Text = Utility.GetCode("PTCC", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuCapCuuBus.GetPhieuThuCapCuuCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuCapCuuBus.GetPhieuThuCapCuuCount"));
            }
        }

        private void InitData()
        {
            dtpkNgayThu.Value = DateTime.Now;
            OnDisplayKhoCapCuu();
            OnDisplayToaCapCuu();
            //OnGetSanhSachBenhNhan();
        }

        private void OnDisplayToaCapCuu()
        {
            Result result = KeToaCapCuuBus.GetToaCapCuuChuaXuatPhieuThuList();
            if (result.IsOK)
            {
                _flag = false;
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["ToaCapCuuGUID"] = Guid.Empty;
                newRow["MaToaCapCuu"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboMaToaCapCuu.DataSource = dt;
                _flag = true;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaCapCuuBus.GetToaCapCuuChuaXuatPhieuThuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaCapCuuBus.GetToaCapCuuChuaXuatPhieuThuList"));
            }
        }

        private void OnGetSanhSachBenhNhan()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
                _dataSourceBenhNhan = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void DisplayInfo(DataRow drPhieuThu)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaToaCapCuu.Text = drPhieuThu["MaToaCapCuu"] as string;
                txtMaPhieuThu.Text = drPhieuThu["MaPhieuThuCapCuu"] as string;
                dtpkNgayThu.Value = Convert.ToDateTime(drPhieuThu["NgayThu"]);
                txtMaBenhNhan.Text = drPhieuThu["MaBenhNhan"] as string;
                txtTenBenhNhan.Text = drPhieuThu["TenBenhNhan"] as string;
                txtDiaChi.Text = drPhieuThu["DiaChi"] as string;
                txtGhiChu.Text = drPhieuThu["Notes"] as string;
                txtLyDoGiam.Text = drPhieuThu["LyDoGiam"] as string;
                chkDaThuTien.Checked = Convert.ToBoolean(drPhieuThu["DaThuTien"]);
                chkDaXuatHD.Checked = Convert.ToBoolean(drPhieuThu["IsExported"]);

                _phieuThuCapCuu.PhieuThuCapCuuGUID = Guid.Parse(drPhieuThu["PhieuThuCapCuuGUID"].ToString());

                if (drPhieuThu["CreatedDate"] != null && drPhieuThu["CreatedDate"] != DBNull.Value)
                    _phieuThuCapCuu.CreatedDate = Convert.ToDateTime(drPhieuThu["CreatedDate"]);

                if (drPhieuThu["CreatedBy"] != null && drPhieuThu["CreatedBy"] != DBNull.Value)
                    _phieuThuCapCuu.CreatedBy = Guid.Parse(drPhieuThu["CreatedBy"].ToString());

                if (drPhieuThu["UpdatedDate"] != null && drPhieuThu["UpdatedDate"] != DBNull.Value)
                    _phieuThuCapCuu.UpdatedDate = Convert.ToDateTime(drPhieuThu["UpdatedDate"]);

                if (drPhieuThu["UpdatedBy"] != null && drPhieuThu["UpdatedBy"] != DBNull.Value)
                    _phieuThuCapCuu.UpdatedBy = Guid.Parse(drPhieuThu["UpdatedBy"].ToString());

                if (drPhieuThu["DeletedDate"] != null && drPhieuThu["DeletedDate"] != DBNull.Value)
                    _phieuThuCapCuu.DeletedDate = Convert.ToDateTime(drPhieuThu["DeletedDate"]);

                if (drPhieuThu["DeletedBy"] != null && drPhieuThu["DeletedBy"] != DBNull.Value)
                    _phieuThuCapCuu.DeletedBy = Guid.Parse(drPhieuThu["DeletedBy"].ToString());

                _phieuThuCapCuu.Status = Convert.ToByte(drPhieuThu["Status"]);

                OnGetChiTietPhieuThuCapCuu(_phieuThuCapCuu.PhieuThuCapCuuGUID.ToString());
                CalculateTongTien();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayKhoCapCuu()
        {
            Result result = KhoCapCuuBus.GetDanhSachCapCuu();
            if (result.IsOK)
                //KhoCapCuuGUID.DataSource = result.QueryResult;
                _dtKhoCapCuu = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"));
            }
        }

        private void RefreshNo()
        {
            for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
            {
                dgChiTiet[0, i].Value = i + 1;
            }
        }

        private void OnGetChiTietPhieuThuCapCuu(string phieuThuCapCuuGUID)
        {
            Result result = PhieuThuCapCuuBus.GetChiTietPhieuThuCapCuu(phieuThuCapCuuGUID);
            if (result.IsOK)
            {
                dgChiTiet.DataSource = result.QueryResult;

                UpdateDataSourceDonGia();
                UpdateNgayHetHanVaSoLuongTon();

                RefreshNo();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuCapCuuBus.GetChiTietPhieuThuCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuCapCuuBus.GetChiTietPhieuThuCapCuu"));
            }
        }

        private void UpdateDataSourceDonGia()
        {
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                if (row.DataBoundItem == null) continue;
                DataRow dr = (row.DataBoundItem as DataRowView).Row;

                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells[4];
                DataTable dtDonGia = cell.DataSource as DataTable;
                if (dtDonGia == null)
                {
                    dtDonGia = new DataTable();
                    dtDonGia.Columns.Add("DonGia", typeof(double));
                }
                else
                    dtDonGia.Rows.Clear();

                string khoCapCuuGUID = dr["KhoCapCuuGUID"].ToString();
                List<double> donGiaList = GetGiaCapCuu(khoCapCuuGUID);
                double donGia = 0;
                if (donGiaList != null && donGiaList.Count > 0)
                {
                    donGia = donGiaList[donGiaList.Count - 1];
                    foreach (double gt in donGiaList)
                    {
                        DataRow newRow = dtDonGia.NewRow();
                        newRow[0] = gt;
                        dtDonGia.Rows.Add(newRow);
                    }
                }
                else
                {
                    DataRow newRow = dtDonGia.NewRow();
                    newRow[0] = 0;
                    dtDonGia.Rows.Add(newRow);
                }
                
                cell.DataSource = dtDonGia;
                cell.DisplayMember = "DonGia";
                cell.ValueMember = "DonGia";

                row.Cells[4].Value = dr["DonGia"];
            }
        }

        private void UpdateNgayHetHanVaSoLuongTon()
        {
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                if (row.Cells["KhoCapCuuGUID"].Value == null || row.Cells["KhoCapCuuGUID"].Value == DBNull.Value)
                    continue;

                string khoCapCuuGUID = row.Cells["KhoCapCuuGUID"].Value.ToString();
                Result result = NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu(khoCapCuuGUID);
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        DateTime ngayHetHan = Convert.ToDateTime(result.QueryResult);
                        row.Cells[7].Value = ngayHetHan;
                    }
                    else
                        row.Cells[7].Value = DBNull.Value;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"));
                }

                result = NhapKhoCapCuuBus.GetKhoCapCuuTonKho(khoCapCuuGUID);
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        int soLuongTon = Convert.ToInt32(result.QueryResult);
                        row.Cells[8].Value = soLuongTon;
                    }
                    else
                        row.Cells[8].Value = DBNull.Value;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"));
                }
            }
        }

        private string GetDonViTinh(string khoCapCuuGUID)
        {
            DataTable dt = _dtKhoCapCuu;//KhoCapCuuGUID.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["DonViTinh"].ToString();

            return string.Empty;
        }

        private void CalculateTongTien()
        {
            int rowCount = dgChiTiet.RowCount;//_isNew ? dgChiTiet.RowCount - 1 : dgChiTiet.RowCount;
            double tongTien = 0;
            for (int i = 0; i < rowCount; i++)
            {
                double tt = 0;
                if (dgChiTiet[6, i].Value != null && dgChiTiet[6, i].Value != DBNull.Value)
                    tt = Convert.ToDouble(dgChiTiet[6, i].Value);
                tongTien += tt;
            }

            if (tongTien == 0)
                lbTongTien.Text = "Tổng tiền: 0 VNĐ";
            else
                lbTongTien.Text = string.Format("Tổng tiền: {0} VNĐ", tongTien.ToString("#,###"));
        }

        private void CalculateThanhTien()
        {
            int rowIndex = dgChiTiet.CurrentCell.RowIndex;
            int colIndex = dgChiTiet.CurrentCell.ColumnIndex;

            if (rowIndex < 0 || colIndex < 0) return;

            double soLuong = 1;
            string strValue = dgChiTiet[3, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                soLuong = Convert.ToDouble(strValue);

            strValue = dgChiTiet[4, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            double donGia = 0;
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                donGia = Convert.ToDouble(strValue);

            double giam = 0;
            strValue = dgChiTiet[5, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                giam = Convert.ToDouble(strValue);

            double tienGiam = Math.Round(((double)soLuong * (double)donGia * (double)giam) / (double)100);
            double thanhTien = (double)soLuong * (double)donGia - tienGiam;
            dgChiTiet[6, rowIndex].Value = thanhTien;

            CalculateTongTien();
        }

        private List<double> GetGiaCapCuu(string khoCapCuuGUID)
        {
            Result result = GiaCapCuuBus.GetGiaCapCuuMoiNhat(khoCapCuuGUID);
            List<double> giaThuocList = new List<double>();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        giaThuocList.Add(Convert.ToDouble(row["GiaBan"]));
                    }

                    giaThuocList.Sort();
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("GiaCapCuuBus.GetGiaCapCuuMoiNhat"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GiaCapCuuBus.GetGiaCapCuuMoiNhat"));
            }

            return giaThuocList;
        }

        private bool CheckInfo()
        {
            if (txtMaPhieuThu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã phiếu thu.", IconType.Information);
                txtMaPhieuThu.Focus();
                return false;
            }

            if (cboMaToaCapCuu.SelectedValue == null || cboMaToaCapCuu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã toa thuốc.", IconType.Information);
                cboMaToaCapCuu.Focus();
                return false;
            }

            //if (txtTenBenhNhan.Text.Trim() == string.Empty)
            //{
            //    MsgBox.Show(this.Text, "Vui lòng chọn tên bệnh nhân.", IconType.Information);
            //    txtTenBenhNhan.Focus();
            //    return false;
            //}

            string phieuThuCapCuuGUID = _isNew ? string.Empty : _phieuThuCapCuu.PhieuThuCapCuuGUID.ToString();
            Result result = PhieuThuCapCuuBus.CheckPhieuThuCapCuuExistCode(phieuThuCapCuuGUID, txtMaPhieuThu.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã phiếu thu này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaPhieuThu.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuCapCuuBus.CheckPhieuThuCapCuuExistCode"), IconType.Error);
                return false;
            }

            if (dgChiTiet.RowCount > 0)
            {
                for (int i = 0; i < dgChiTiet.RowCount; i++)
                {
                    DataGridViewRow row = dgChiTiet.Rows[i];

                    //if (row.Cells["KhoCapCuuGUID"].Value == null || row.Cells["KhoCapCuuGUID"].Value == DBNull.Value || 
                    //row.Cells["KhoCapCuuGUID"].Value.ToString() == string.Empty)
                    //{
                    //    MsgBox.Show(this.Text, "Vui lòng chọn cấp cứu để xuất phiếu thu.", IconType.Information);
                    //    return false;
                    //}

                    string khoCapCuuGUID = row.Cells["KhoCapCuuGUID"].Value.ToString();
                    string tenCapCuu = GetTenCapCuu(khoCapCuuGUID);

                    if (row.Cells[4].Value.ToString() == "0")
                    {
                        MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' chưa có nhập giá bán. Vui lòng chọn cấp cứu khác.", tenCapCuu), IconType.Information);
                        return false;
                    }

                    int soLuong = 1;
                    if (row.Cells[3].Value != null && row.Cells[3].Value != DBNull.Value)
                        soLuong = Convert.ToInt32(row.Cells[3].Value);

                    Result r = NhapKhoCapCuuBus.CheckKhoCapCuuTonKho(khoCapCuuGUID, soLuong);
                    if (r.IsOK)
                    {
                        if (!Convert.ToBoolean(r.QueryResult))
                        {
                            MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã hết hoặc không đủ số lượng để bán. Vui lòng chọn cấp cứu khác.", tenCapCuu), IconType.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuTonKho"), IconType.Error);
                        Utility.WriteToTraceLog(r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuTonKho"));
                        return false;
                    }

                    r = NhapKhoCapCuuBus.CheckKhoCapCuuHetHan(khoCapCuuGUID);
                    if (r.IsOK)
                    {
                        if (Convert.ToBoolean(r.QueryResult))
                        {
                            MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã hết hạn sử dụng. Vui lòng chọn cấp cứu khác.", tenCapCuu), IconType.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuHetHan"), IconType.Error);
                        Utility.WriteToTraceLog(r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuHetHan"));
                        return false;
                    }
                }
            }
            else
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 cấp cứu.", IconType.Information);
                return false;
            }

            //if (dgChiTiet.RowCount > 1)
            //{
            //    for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
            //    {
            //        DataGridViewRow row1 = dgChiTiet.Rows[i];
            //        for (int j = i + 1; j < dgChiTiet.RowCount; j++)
            //        {
            //            DataGridViewRow row2 = dgChiTiet.Rows[j];
            //            if (row1.Cells["KhoCapCuuGUID"].Value.ToString() == row2.Cells["KhoCapCuuGUID"].Value.ToString())
            //            {
            //                string tenCapCuu = GetTenCapCuu(row1.Cells["KhoCapCuuGUID"].Value.ToString());
            //                MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã tồn tại rồi. Vui lòng chọn cấp cứu khác", tenCapCuu), IconType.Information);
            //                return false;
            //            }
            //        }
            //    }
            //}

            return true;
        }

        private string GetTenCapCuu(string khoCapCuuGUID)
        {
            DataTable dt = _dtKhoCapCuu;//KhoCapCuuGUID.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["TenCapCuu"].ToString();

            return string.Empty;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                MethodInvoker method = delegate
                {
                    _phieuThuCapCuu.MaPhieuThuCapCuu = txtMaPhieuThu.Text;
                    _phieuThuCapCuu.ToaCapCuuGUID = Guid.Parse(cboMaToaCapCuu.SelectedValue.ToString());
                    _phieuThuCapCuu.NgayThu = dtpkNgayThu.Value;
                    _phieuThuCapCuu.MaBenhNhan = txtMaBenhNhan.Text;
                    _phieuThuCapCuu.TenBenhNhan = txtTenBenhNhan.Text;
                    _phieuThuCapCuu.DiaChi = txtDiaChi.Text;
                    _phieuThuCapCuu.TenCongTy = _tenCongTy;
                    _phieuThuCapCuu.Status = (byte)Status.Actived;
                    _phieuThuCapCuu.ChuaThuTien = !chkDaThuTien.Checked;
                    _phieuThuCapCuu.Notes = txtGhiChu.Text;
                    _phieuThuCapCuu.LyDoGiam = txtLyDoGiam.Text;

                    if (_isNew)
                    {
                        _phieuThuCapCuu.CreatedDate = DateTime.Now;
                        _phieuThuCapCuu.CreatedBy = Guid.Parse(Global.UserGUID);
                    }

                    List<ChiTietPhieuThuCapCuu> addedList = new List<ChiTietPhieuThuCapCuu>();
                    for (int i = 0; i < dgChiTiet.RowCount; i++)
                    {
                        DataGridViewRow row = dgChiTiet.Rows[i];
                        ChiTietPhieuThuCapCuu ctptcc = new ChiTietPhieuThuCapCuu();
                        ctptcc.CreatedDate = DateTime.Now;
                        ctptcc.CreatedBy = Guid.Parse(Global.UserGUID);

                        ctptcc.KhoCapCuuGUID = Guid.Parse(row.Cells["KhoCapCuuGUID"].Value.ToString());
                        ctptcc.DonGia = Convert.ToDouble(row.Cells["DonGia"].Value);

                        if (row.Cells["SoLuong"].Value != null && row.Cells["SoLuong"].Value != DBNull.Value)
                            ctptcc.SoLuong = Convert.ToDouble(row.Cells["SoLuong"].Value);
                        else
                            ctptcc.SoLuong = 1;

                        if (row.Cells["Giam"].Value != null && row.Cells["Giam"].Value != DBNull.Value)
                            ctptcc.Giam = Convert.ToDouble(row.Cells["Giam"].Value);
                        else
                            ctptcc.Giam = 0;

                        ctptcc.ThanhTien = Convert.ToDouble(row.Cells["ThanhTien"].Value);
                        ctptcc.Status = (byte)Status.Actived;
                        addedList.Add(ctptcc);
                    }

                    Result result = PhieuThuCapCuuBus.InsertPhieuThuCapCuu(_phieuThuCapCuu, addedList);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuCapCuuBus.InsertPhieuThuCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuCapCuuBus.InsertPhieuThuCapCuu"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnExportInvoice()
        {
            //List<DataRow> phieuThuThuocList = new List<DataRow>();
            //phieuThuThuocList.Add(_drPhieuThu);
            //dlgHoaDonThuoc dlg = new dlgHoaDonThuoc(phieuThuThuocList);
            //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    _isExportedInvoice = true;
            //    btnExportInvoice.Enabled = false;
            //}
        }

        private void ClearDataSource()
        {
            DataTable dt = dgChiTiet.DataSource as DataTable;
            dt.Rows.Clear();
        }

        private DataRow GetToaCapCuuRow(string toaCapCuuGUID)
        {
            DataTable dt = cboMaToaCapCuu.DataSource as DataTable;
            DataRow[] rows = dt.Select(string.Format("ToaCapCuuGUID='{0}'", toaCapCuuGUID));
            if (rows != null && rows.Length > 0)
                return rows[0];

            return null;
        }

        private void OnDisplayChiTietToaCapCuu(string toaCapCuuGUID)
        {
            Result result = KeToaCapCuuBus.GetChiTietToaCapCuu(toaCapCuuGUID);
            if (result.IsOK)
            {
                DataTable dtChiTiet = dgChiTiet.DataSource as DataTable;
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtChiTiet.NewRow();
                    string khoCapCuuGUID = row["KhoCapCuuGUID"].ToString();
                    newRow["KhoCapCuuGUID"] = khoCapCuuGUID;
                    newRow["TenCapCuu"] = row["TenCapCuu"].ToString();
                    string donViTinh = GetDonViTinh(khoCapCuuGUID);
                    List<double> donGiaList = GetGiaCapCuu(khoCapCuuGUID);
                    double donGia = 0;
                    if (donGiaList != null && donGiaList.Count > 0)
                        donGia = donGiaList[donGiaList.Count - 1];

                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    newRow["DonViTinh"] = donViTinh;
                    newRow["SoLuong"] = soLuong;
                    newRow["DonGia"] = donGia;
                    newRow["Giam"] = 0;
                    newRow["ThanhTien"] = soLuong * donGia;
                    dtChiTiet.Rows.Add(newRow);
                }

                UpdateDataSourceDonGia();
                UpdateNgayHetHanVaSoLuongTon();

                CalculateTongTien();
                RefreshNo();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaCapCuuBus.GetChiTietToaCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaCapCuuBus.GetChiTietToaCapCuu"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddPhieuThuThuoc_Load(object sender, EventArgs e)
        {

            if (_isNew)
            {
                InitData();
                OnGetChiTietPhieuThuCapCuu(Guid.Empty.ToString());
            }
            else
                DisplayInfo(_drPhieuThu);

            UpdateGUI();
        }

        private void dlgAddPhieuThuThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (_isNew)
                {
                    if (CheckInfo())
                        SaveInfoAsThread();
                    else
                        e.Cancel = true;
                }
                else
                {
                    Result result = PhieuThuCapCuuBus.CapNhatTrangThaiPhieuThu(_phieuThuCapCuu.PhieuThuCapCuuGUID.ToString(), chkDaXuatHD.Checked, chkDaThuTien.Checked);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuCapCuuBus.CapNhatTrangThaiPhieuThu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuCapCuuBus.CapNhatTrangThaiPhieuThu"));
                        e.Cancel = true;
                    }
                    else
                    {
                        _drPhieuThu["IsExported"] = chkDaXuatHD.Checked;
                        _drPhieuThu["DaThuTien"] = chkDaThuTien.Checked;
                    }
                }
            }
            else
            {
                if (_isNew)
                {
                    if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin phiếu thu cấp cứu ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (CheckInfo())
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            SaveInfoAsThread();
                        }
                        else
                            e.Cancel = true;
                    }
                }
            }
        }

        private void dgChiTiet_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgChiTiet_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgChiTiet_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            _flag = false;
            try
            {

                if (e.RowIndex < 0) return;
                dgChiTiet.CurrentCell = dgChiTiet[e.ColumnIndex, e.RowIndex];
                dgChiTiet.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex)
            {

            }

            _flag = true;
        }

        private void dgChiTiet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgChiTiet.CurrentCell.ColumnIndex == 5)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress -= new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged -= new EventHandler(textBox_TextChanged);
                    textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged += new EventHandler(textBox_TextChanged);
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            TextBox textBox = (TextBox)sender;
            
            int colIndex = dgChiTiet.CurrentCell.ColumnIndex;
            if (colIndex != 5) return;

            if (textBox.Text == string.Empty)
                textBox.Text = "0";

            string strValue = textBox.Text.Replace(",", "").Replace(".", "");

            try
            {
                int value = int.Parse(strValue);
                if (value > 100) textBox.Text = "100";
            }
            catch
            {
                if (colIndex == 5) textBox.Text = "100";
            }

            CalculateThanhTien();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int colIndex = dgChiTiet.CurrentCell.ColumnIndex;
            if (colIndex != 5) return;

            DataGridViewTextBoxEditingControl textBox = (DataGridViewTextBoxEditingControl)sender;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
            }
        }

        private void dgChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if (e.Value == null || e.Value.ToString() == string.Empty || e.Value == DBNull.Value)
                    e.Value = "0";
            }
        }

        private void dgChiTiet_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshNo();
        }

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(_dataSourceBenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    txtMaBenhNhan.Text = patientRow["FileNum"].ToString();
                    txtTenBenhNhan.Text = patientRow["FullName"].ToString();
                    txtDiaChi.Text = patientRow["Address"].ToString();
                }
            }
        }

        private void dgChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgChiTiet_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //int i = 0;
        }

        private void dgChiTiet_Leave(object sender, EventArgs e)
        {
            if (_isNew)
            {
                if (dgChiTiet.CurrentRow == null) return;
                int rowIndex = dgChiTiet.CurrentRow.Index;
                if (rowIndex < 0) return;
                dgChiTiet.CurrentCell = dgChiTiet[0, rowIndex];
                dgChiTiet.Rows[rowIndex].Selected = true;

            }
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            OnExportInvoice();
        }

        private void cboMaToaCapCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isNew) return;
            if (!_flag) return;

            ClearDataSource();

            if (cboMaToaCapCuu.SelectedValue == null || cboMaToaCapCuu.Text.Trim() == string.Empty)
            {
                txtMaBenhNhan.Text = string.Empty;
                txtTenBenhNhan.Text = string.Empty;
                txtDiaChi.Text = string.Empty;
            }
            else
            {
                string toaCapCuuGUID = cboMaToaCapCuu.SelectedValue.ToString();
                DataRow row = GetToaCapCuuRow(toaCapCuuGUID);
                if (row != null)
                {
                    txtMaBenhNhan.Text = row["MaBenhNhan"] as string;
                    txtTenBenhNhan.Text = row["TenBenhNhan"].ToString();
                    txtDiaChi.Text = row["DiaChi"] as string;
                    OnDisplayChiTietToaCapCuu(toaCapCuuGUID);
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
