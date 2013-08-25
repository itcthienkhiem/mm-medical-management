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
    public partial class dlgAddPhieuThuThuoc2 : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private bool _flag = true;
        private PhieuThuThuoc _phieuThuThuoc = new PhieuThuThuoc();
        private DataRow _drPhieuThu = null;
        private string _tenCongTy = string.Empty;
        private ComboBox _cboBox = null;
        private TextBox _textBox = null;
        private bool _isExportedInvoice = false;
        private double _donGia = 0;
        #endregion

        #region Constructor
        public dlgAddPhieuThuThuoc2()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddPhieuThuThuoc2(DataRow drPhieuThu)
        {
            InitializeComponent();
            _isNew = false;
            _drPhieuThu = drPhieuThu;
            if (Global.StaffType != StaffType.Admin)
            {
                btnOK.Enabled = false;
                chkDaThuTien.Enabled = false;
                chkDaXuatHD.Enabled = false;
                cboHinhThucThanhToan.Enabled = false;
            }
            else
            {
                chkDaThuTien.Enabled = true;
                chkDaXuatHD.Enabled = true;
                cboHinhThucThanhToan.Enabled = true;
            }

            gridViewPTT.OptionsBehavior.Editable = false;
            gridViewPTT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            txtMaPhieuThu.ReadOnly = true;
            cboMaToaThuoc.Enabled = false;
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
        public PhieuThuThuoc PhieuThuThuoc
        {
            get { return _phieuThuThuoc; }
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
                btnExportInvoice.Enabled = Global.AllowExportHoaDonThuoc && !isExportedInvoice;
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = PhieuThuThuocBus.GetPhieuThuThuocCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaPhieuThu.Text = Utility.GetCode("PTT", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocCount"));
            }
        }

        private void InitData()
        {
            cboHinhThucThanhToan.SelectedIndex = 0;
            dtpkNgayThu.Value = DateTime.Now;
            OnDisplayToaThuocList();
            OnDisplayThuoc();
        }

        private void DisplayInfo(DataRow drPhieuThu)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaPhieuThu.Text = drPhieuThu["MaPhieuThuThuoc"] as string;
                cboMaToaThuoc.SelectedValue = drPhieuThu["ToaThuocGUID"].ToString();
                dtpkNgayThu.Value = Convert.ToDateTime(drPhieuThu["NgayThu"]);
                txtMaBenhNhan.Text = drPhieuThu["MaBenhNhan"] as string;
                txtTenBenhNhan.Text = drPhieuThu["TenBenhNhan"] as string;
                txtDiaChi.Text = drPhieuThu["DiaChi"] as string;
                txtGhiChu.Text = drPhieuThu["Notes"] as string;
                txtLyDoGiam.Text = drPhieuThu["LyDoGiam"] as string;
                chkDaThuTien.Checked = Convert.ToBoolean(drPhieuThu["DaThuTien"]);
                chkDaXuatHD.Checked = Convert.ToBoolean(drPhieuThu["IsExported"]);
                cboHinhThucThanhToan.SelectedIndex = Convert.ToInt32(drPhieuThu["HinhThucThanhToan"]);

                _phieuThuThuoc.PhieuThuThuocGUID = Guid.Parse(drPhieuThu["PhieuThuThuocGUID"].ToString());

                if (drPhieuThu["CreatedDate"] != null && drPhieuThu["CreatedDate"] != DBNull.Value)
                    _phieuThuThuoc.CreatedDate = Convert.ToDateTime(drPhieuThu["CreatedDate"]);

                if (drPhieuThu["CreatedBy"] != null && drPhieuThu["CreatedBy"] != DBNull.Value)
                    _phieuThuThuoc.CreatedBy = Guid.Parse(drPhieuThu["CreatedBy"].ToString());

                if (drPhieuThu["UpdatedDate"] != null && drPhieuThu["UpdatedDate"] != DBNull.Value)
                    _phieuThuThuoc.UpdatedDate = Convert.ToDateTime(drPhieuThu["UpdatedDate"]);

                if (drPhieuThu["UpdatedBy"] != null && drPhieuThu["UpdatedBy"] != DBNull.Value)
                    _phieuThuThuoc.UpdatedBy = Guid.Parse(drPhieuThu["UpdatedBy"].ToString());

                if (drPhieuThu["DeletedDate"] != null && drPhieuThu["DeletedDate"] != DBNull.Value)
                    _phieuThuThuoc.DeletedDate = Convert.ToDateTime(drPhieuThu["DeletedDate"]);

                if (drPhieuThu["DeletedBy"] != null && drPhieuThu["DeletedBy"] != DBNull.Value)
                    _phieuThuThuoc.DeletedBy = Guid.Parse(drPhieuThu["DeletedBy"].ToString());

                _phieuThuThuoc.Status = Convert.ToByte(drPhieuThu["Status"]);

                OnGetChiTietPhieuThuThuoc(_phieuThuThuoc.PhieuThuThuocGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayThuoc()
        {
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
            {
                repositoryItemLookUpEditTenThuoc.DataSource = result.QueryResult;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
            }
        }

        private void OnDisplayToaThuocList()
        {
            Result result = KeToaBus.GetToaThuocList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["ToaThuocGUID"] = Guid.Empty.ToString();
                newRow["MaToaThuoc"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboMaToaThuoc.DataSource = dt;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuocList"));
            }
        }

        private DataRow GetToaThuocRow(string toaThuocGUID)
        {
            DataTable dt = cboMaToaThuoc.DataSource as DataTable;
            DataRow[] rows = dt.Select(string.Format("ToaThuocGUID='{0}'", toaThuocGUID));
            if (rows != null && rows.Length > 0)
                return rows[0];

            return null;
        }

        private void RefreshNo()
        {
            gridViewPTT.UpdateCurrentRow();
            for (int i = 0; i < gridViewPTT.RowCount; i++)
            {
                DataRow row = gridViewPTT.GetDataRow(i);
                if (row != null)
                    row["STT"] = i + 1;
            }
        }

        private void OnGetChiTietPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = PhieuThuThuocBus.GetChiTietPhieuThuThuoc(phieuThuThuocGUID);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;

                if (!_isNew)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        UpdateNgayHetHan(row);
                        UpdateSLTon(row);

                    }

                    gridPTT.DataSource = dt;
                    UpdateTongTien();
                }
                else
                    gridPTT.DataSource = dt;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"));
            }
        }

        private void OnDisplayChiTietToaThuoc(string toaThuocGUID)
        {
            Result result = KeToaBus.GetChiTietToaThuocListWithoutThuocNgoai(toaThuocGUID);
            if (result.IsOK)
            {
                DataTable dtChiTiet = (gridViewPTT.DataSource as DataView).Table;
                dtChiTiet.Rows.Clear();
                DataTable dt = result.QueryResult as DataTable;
                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtChiTiet.NewRow();
                    dtChiTiet.Rows.Add(newRow);
                    string thuocGUID = row["ThuocGUID"].ToString();
                    newRow["STT"] = stt++;
                    newRow["ThuocGUID"] = thuocGUID;
                    double soLuong = Convert.ToDouble(row["SoLuong"]);
                    newRow["SoLuong"] = soLuong;
                    UpdateThongTinThuoc(newRow);
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"));
            }
        }

        private string GetDonViTinh(string thuocGUID)
        {
            DataTable dt = repositoryItemLookUpEditTenThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["DonViTinh"].ToString();

            return string.Empty;
        }

        private List<double> GetGiaThuoc(string thuocGUID)
        {
            Result result = GiaThuocBus.GetGiaThuocMoiNhat(thuocGUID);
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
                MsgBox.Show(this.Text, result.GetErrorAsString("GiaThuocBus.GetGiaThuocMoiNhat"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GiaThuocBus.GetGiaThuocMoiNhat"));
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

            if (txtTenBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn tên bệnh nhân.", IconType.Information);
                txtTenBenhNhan.Focus();
                return false;
            }

            string phieuThuThuocGUID = _isNew ? string.Empty : _phieuThuThuoc.PhieuThuThuocGUID.ToString();
            Result result = PhieuThuThuocBus.CheckPhieuThuThuocExistCode(phieuThuThuocGUID, txtMaPhieuThu.Text);

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
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuThuocBus.CheckPhieuThuThuocExistCode"), IconType.Error);
                return false;
            }

            gridViewPTT.UpdateCurrentRow();
            DataTable dt = (gridViewPTT.DataSource as DataView).Table;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ThuocGUID"] == null || row["ThuocGUID"] == DBNull.Value)
                    {
                        MsgBox.Show(this.Text, "Vui lòng chọn thuốc để xuất phiếu thu.", IconType.Information);
                        return false;
                    }

                    string thuocGUID = row["ThuocGUID"].ToString();
                    string tenThuoc = GetTenThuoc(thuocGUID);

                    if (row["DonGia"] == null || row["DonGia"] == DBNull.Value || Convert.ToDouble(row["DonGia"]) == 0)
                    {
                        MsgBox.Show(this.Text, string.Format("Thuốc '{0}' chưa có nhập giá bán. Vui lòng chọn thuốc khác.", tenThuoc), IconType.Information);
                        return false;
                    }

                    int soLuong = 1;
                    if (row["SoLuong"] != null && row["SoLuong"] != DBNull.Value)
                        soLuong = Convert.ToInt32(row["SoLuong"]);

                    Result r = LoThuocBus.CheckThuocTonKho(thuocGUID, soLuong);
                    if (r.IsOK)
                    {
                        if (!Convert.ToBoolean(r.QueryResult))
                        {
                            MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã hết hoặc không đủ số lượng để bán. Vui lòng chọn thuốc khác.", tenThuoc), IconType.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, r.GetErrorAsString("LoThuocBus.CheckThuocTonKho"), IconType.Error);
                        Utility.WriteToTraceLog(r.GetErrorAsString("LoThuocBus.CheckThuocTonKho"));
                        return false;
                    }

                    r = LoThuocBus.CheckThuocHetHan(thuocGUID);
                    if (r.IsOK)
                    {
                        if (Convert.ToBoolean(r.QueryResult))
                        {
                            MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã hết hạn sử dụng. Vui lòng chọn thuốc khác.", tenThuoc), IconType.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, r.GetErrorAsString("LoThuocBus.CheckThuocHetHan"), IconType.Error);
                        Utility.WriteToTraceLog(r.GetErrorAsString("LoThuocBus.CheckThuocHetHan"));
                        return false;
                    }
                }
            }
            else
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 thuốc.", IconType.Information);
                return false;
            }

            if (dt.Rows.Count > 1)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    DataRow row1 = dt.Rows[i];
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        DataRow row2 = dt.Rows[j];
                        if (row1["ThuocGUID"].ToString() == row2["ThuocGUID"].ToString())
                        {
                            string tenThuoc = GetTenThuoc(row1["ThuocGUID"].ToString());
                            MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã tồn tại rồi. Vui lòng chọn thuốc khác", tenThuoc), IconType.Information);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private string GetTenThuoc(string thuocGUID)
        {
            DataTable dt = repositoryItemLookUpEditTenThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["TenThuoc"].ToString();

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
                    _phieuThuThuoc.MaPhieuThuThuoc = txtMaPhieuThu.Text;
                    if (cboMaToaThuoc.SelectedValue != null && cboMaToaThuoc.SelectedValue.ToString() != Guid.Empty.ToString())
                        _phieuThuThuoc.ToaThuocGUID = Guid.Parse(cboMaToaThuoc.SelectedValue.ToString());
                    else
                        _phieuThuThuoc.ToaThuocGUID = Guid.Empty;

                    _phieuThuThuoc.NgayThu = dtpkNgayThu.Value;
                    _phieuThuThuoc.MaBenhNhan = txtMaBenhNhan.Text;
                    _phieuThuThuoc.TenBenhNhan = txtTenBenhNhan.Text;
                    _phieuThuThuoc.DiaChi = txtDiaChi.Text;
                    _phieuThuThuoc.TenCongTy = _tenCongTy;
                    _phieuThuThuoc.Status = (byte)Status.Actived;
                    _phieuThuThuoc.ChuaThuTien = !chkDaThuTien.Checked;
                    _phieuThuThuoc.Notes = txtGhiChu.Text;
                    _phieuThuThuoc.LyDoGiam = txtLyDoGiam.Text;
                    _phieuThuThuoc.HinhThucThanhToan = (byte)cboHinhThucThanhToan.SelectedIndex;

                    if (_isNew)
                    {
                        _phieuThuThuoc.CreatedDate = DateTime.Now;
                        _phieuThuThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    }

                    List<ChiTietPhieuThuThuoc> addedList = new List<ChiTietPhieuThuThuoc>();
                    gridViewPTT.UpdateCurrentRow();
                    DataTable dt = (gridViewPTT.DataSource as DataView).Table;

                    foreach (DataRow row in dt.Rows)
                    {
                        ChiTietPhieuThuThuoc ctptt = new ChiTietPhieuThuThuoc();
                        ctptt.CreatedDate = DateTime.Now;
                        ctptt.CreatedBy = Guid.Parse(Global.UserGUID);

                        ctptt.ThuocGUID = Guid.Parse(row["ThuocGUID"].ToString());
                        ctptt.DonGia = Convert.ToDouble(row["DonGia"]);

                        if (row["SoLuong"] != null && row["SoLuong"] != DBNull.Value)
                            ctptt.SoLuong = Convert.ToDouble(row["SoLuong"]);
                        else
                            ctptt.SoLuong = 1;

                        if (row["Giam"] != null && row["Giam"] != DBNull.Value)
                            ctptt.Giam = Convert.ToDouble(row["Giam"]);
                        else
                            ctptt.Giam = 0;

                        double tienGiam = Math.Round(((double)ctptt.SoLuong * (double)ctptt.DonGia * (double)ctptt.Giam) / (double)100);
                        double thanhTien = (double)ctptt.SoLuong * (double)ctptt.DonGia - tienGiam;

                        ctptt.ThanhTien = thanhTien;
                        ctptt.Status = (byte)Status.Actived;
                        addedList.Add(ctptt);
                    }

                    Result result = PhieuThuThuocBus.InsertPhieuThuThuoc(_phieuThuThuoc, addedList);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuThuocBus.InsertPhieuThuThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.InsertPhieuThuThuoc"));
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
            List<DataRow> phieuThuThuocList = new List<DataRow>();
            phieuThuThuocList.Add(_drPhieuThu);
            dlgHoaDonThuoc dlg = new dlgHoaDonThuoc(phieuThuThuocList);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _isExportedInvoice = true;
                btnExportInvoice.Enabled = false;
            }
        }

        private void UpdateThanhTien(DataRow row)
        {
            string thuocGUID = row["ThuocGUID"].ToString();

            double donGia = 0;
            if (row["DonGia"] != null && row["DonGia"] != DBNull.Value)
                donGia = Convert.ToDouble(row["DonGia"]);

            double soLuong = 1;
            if (row["SoLuong"] != null && row["SoLuong"] != DBNull.Value)
                soLuong = Convert.ToDouble(row["SoLuong"]);

            double giam = 0;
            if (row["Giam"] != null && row["Giam"] != DBNull.Value)
                giam = Convert.ToDouble(row["Giam"]);

            row["ThanhTien"] = Math.Round((soLuong * donGia - (soLuong * donGia * giam / 100)), 0);

            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            gridViewPTT.UpdateCurrentRow();
            DataTable dt = (gridViewPTT.DataSource as DataView).Table;

            double tongTien = 0;
            foreach (DataRow row in dt.Rows)
            {
                double thanhTien = 0;
                if (row["ThanhTien"] != null && row["ThanhTien"] != DBNull.Value)
                    thanhTien = Convert.ToDouble(row["ThanhTien"]);

                tongTien += thanhTien;
            }

            if (tongTien == 0)
                lbTongTien.Text = "Tổng tiền: 0 VNĐ";
            else
                lbTongTien.Text = string.Format("Tổng tiền: {0} VNĐ", tongTien.ToString("#,###"));
        }

        private void UpdateSLTon(DataRow row)
        {
            string thuocGUID = row["ThuocGUID"].ToString();
            Result result = LoThuocBus.GetThuocTonKho(thuocGUID);
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    int soLuongTon = Convert.ToInt32(result.QueryResult);
                    row["SLTon"] = soLuongTon;
                }
                else
                    row["SLTon"] = DBNull.Value;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.GetNgayHetHanCuaThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetNgayHetHanCuaThuoc"));
            }
        }

        private void UpdateNgayHetHan(DataRow row)
        {
            string thuocGUID = row["ThuocGUID"].ToString();
            Result result = LoThuocBus.GetNgayHetHanCuaThuoc(thuocGUID);
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    DateTime ngayHetHan = Convert.ToDateTime(result.QueryResult);
                    row["NgayHetHan"] = ngayHetHan.ToString("dd/MM/yyyy");
                }
                else
                    row["NgayHetHan"] = DBNull.Value;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.GetNgayHetHanCuaThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetNgayHetHanCuaThuoc"));
            }
        }

        private void UpdateThongTinThuoc(DataRow row)
        {
            string thuocGUID = row["ThuocGUID"].ToString();

            //Đơn vị tính
            string donViTinh = GetDonViTinh(thuocGUID);
            row["DonViTinh"] = donViTinh;

            //Đơn giá
            List<double> donGiaList = GetGiaThuoc(thuocGUID);
            double donGia = 0;
            if (donGiaList != null && donGiaList.Count > 0)
            {
                donGia = donGiaList[donGiaList.Count - 1];
            }

            row["DonGia"] = donGia;

            //Thành tiền
            UpdateThanhTien(row);

            //Ngày hết hạn
            UpdateNgayHetHan(row);

            //SL tồn
            UpdateSLTon(row);
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddPhieuThuThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (_isNew)
                OnGetChiTietPhieuThuThuoc(Guid.Empty.ToString());
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
                else if (Global.StaffType == StaffType.Admin)
                {
                    Result result = PhieuThuThuocBus.CapNhatTrangThaiPhieuThu(_phieuThuThuoc.PhieuThuThuocGUID.ToString(), 
                        chkDaXuatHD.Checked, chkDaThuTien.Checked, (byte)cboHinhThucThanhToan.SelectedIndex);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.CapNhatTrangThaiPhieuThu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.CapNhatTrangThaiPhieuThu"));
                        e.Cancel = true;
                    }
                    else
                    {
                        _drPhieuThu["IsExported"] = chkDaXuatHD.Checked;
                        _drPhieuThu["DaThuTien"] = chkDaThuTien.Checked;
                        _drPhieuThu["HinhThucThanhToan"] = (byte)cboHinhThucThanhToan.SelectedIndex;
                        _drPhieuThu["HinhThucThanhToanStr"] = cboHinhThucThanhToan.Text;
                    }
                }
            }
            else
            {
                if (_isNew)
                {
                    if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin phiếu thu thuốc ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void cboMaToaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isNew) return;
            if (cboMaToaThuoc.SelectedValue == null) return;
            string toaThuocGUID = cboMaToaThuoc.SelectedValue.ToString();
            if (toaThuocGUID == Guid.Empty.ToString())
            {
                txtMaBenhNhan.ReadOnly = true;
                txtMaBenhNhan.Text = string.Empty;
                txtTenBenhNhan.Text = string.Empty;
                txtDiaChi.Text = string.Empty;
                _tenCongTy = "Tự túc";
            }
            else
            {
                txtMaBenhNhan.ReadOnly = false;
                DataRow row = GetToaThuocRow(toaThuocGUID);
                if (row != null)
                {
                    txtMaBenhNhan.Text = row["FileNum"].ToString();
                    txtTenBenhNhan.Text = row["TenBenhNhan"].ToString();
                    txtDiaChi.Text = row["Address"].ToString();
                    if (row["CompanyName"] != null && row["CompanyName"] != DBNull.Value)
                        _tenCongTy = row["CompanyName"].ToString();
                    else
                        _tenCongTy = "Tự túc";

                    OnDisplayChiTietToaThuoc(toaThuocGUID);
                }
                else
                    _tenCongTy = "Tự túc";
            }
        }

        private void thuocThayTheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow row = gridViewPTT.GetFocusedDataRow();
            if (row == null || row["ThuocGUID"] == null || row["ThuocGUID"] == DBNull.Value) return;
            string thuocGUID = row["ThuocGUID"].ToString();
            dlgThuocThayThe dlg = new dlgThuocThayThe(thuocGUID);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                row["ThuocGUID"] = dlg.ThuocThayThe;
                UpdateThongTinThuoc(row);
                gridViewPTT.UpdateCurrentRow();
            }
        }

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(PatientSearchType.BenhNhan);
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

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            OnExportInvoice();
        }

        private void gridViewPTT_KeyUp(object sender, KeyEventArgs e)
        {
            if (!_isNew) return;
            if (e.KeyCode == Keys.Delete)
            {
                gridViewPTT.DeleteSelectedRows();
                UpdateTongTien();
            }
        }

        private void repositoryItemLookUpEditTenThuoc_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (e.Value == null) return;

            bool isNewRow = gridViewPTT.IsNewItemRow(gridViewPTT.FocusedRowHandle);
            if (isNewRow)
            {
                if (gridViewPTT.GetFocusedDataRow() == null)
                    gridViewPTT.AddNewRow();
            }

            DataRow row = gridViewPTT.GetFocusedDataRow();
            string thuocGUID = string.Empty;
            if (row["ThuocGUID"] != null && row["ThuocGUID"] != DBNull.Value) thuocGUID = row["ThuocGUID"].ToString();
            if (thuocGUID == e.Value.ToString()) return;

            row["ThuocGUID"] = e.Value;
            UpdateThongTinThuoc(row);
            gridViewPTT.UpdateCurrentRow();
            RefreshNo();
        }

        private void gridViewPTT_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //RefreshNo();
        }

        private void gridViewPTT_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.ColumnHandle == 4)
            {
                if (e.CellValue != null)
                    _donGia = Convert.ToDouble(e.CellValue);
                else
                    _donGia = 0;

                colDonGia.ColumnEdit.BeginUpdate();
                DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryComboBox = (e.RepositoryItem as DevExpress.XtraEditors.Repository.RepositoryItemComboBox);
                repositoryComboBox.Items.Clear();
                DataRow row = gridViewPTT.GetFocusedDataRow();
                if (row != null && row["ThuocGUID"] != null && row["ThuocGUID"] != DBNull.Value)
                {
                    string thuocGUID = row["ThuocGUID"].ToString();
                    List<double> donGiaList = GetGiaThuoc(thuocGUID);
                    if (donGiaList != null && donGiaList.Count > 0)
                    {
                        foreach (double donGia in donGiaList)
                        {
                            repositoryComboBox.Items.Add(donGia);
                        }
                    }
                }
                colDonGia.ColumnEdit.CancelUpdate();
            }
        }

        private void gridViewPTT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.AbsoluteIndex == 4) //Đơn giá
            {
                DataRow row = gridViewPTT.GetFocusedDataRow();
                if (row == null) return;
                double oldDonGia = Convert.ToDouble(row["DonGia"]);

                colDonGia.ColumnEdit.BeginUpdate();
                bool isCancel = true;
                for (int i = 0; i < repositoryItemComboBoxDonGia.Items.Count; i++)
                {
                    double value = Convert.ToDouble(repositoryItemComboBoxDonGia.Items[i]);
                    if (value == oldDonGia)
                    {
                        isCancel = false;
                        break;
                    }
                }
                colDonGia.ColumnEdit.CancelUpdate();

                if (isCancel)
                {
                    row["DonGia"] = _donGia;
                    gridViewPTT.UpdateCurrentRow();
                }

                UpdateThanhTien(row);
            }
            else if (e.Column.AbsoluteIndex == 3 || e.Column.AbsoluteIndex == 5) //Số lượng & Giảm
            {
                DataRow row = gridViewPTT.GetFocusedDataRow();
                if (row == null) return;
                UpdateThanhTien(row);
            }

            RefreshNo();
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
