using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Exports;
using SpreadsheetGear;
using SpreadsheetGear.Windows.Forms;

namespace MM.Dialogs
{
    public partial class dlgInvoiceInfo : Form
    {
        #region Members
        private DataRow _drInvoice;
        private double _totalPrice = 0;
        private bool _isPrinted = false;
        private string _invoiceCode = string.Empty;
        private bool _isView = false;
        private bool _flag = true;
        private bool _flag2 = true;
        private bool _flag3 = true;
        private double _oldTotalPayment = 0;
        private double _totalPayment = 0;
        private List<DataRow> _receiptList = null;
        private Invoice _invoice = new Invoice();
        private ComboBox _cboBox = null;
        private DataTable _dtServices = null;
        #endregion

        #region Constructor
        public dlgInvoiceInfo(List<DataRow> receiptList)
        {
            InitializeComponent();
            _receiptList = receiptList;
            cboHinhThucThanhToan.SelectedIndex = 0;
            btnExportAndPrint.Enabled = Global.AllowPrintHoaDonDichVu;
        }

        public dlgInvoiceInfo(DataRow drInvoice, bool isView)
        {
            InitializeComponent();
            _drInvoice = drInvoice;
            _isView = isView;
            cboHinhThucThanhToan.SelectedIndex = 0;
            btnExportAndPrint.Enabled = Global.AllowPrintHoaDonDichVu;

            if (_isView)
            {
                cboTenNguoiMuaHang.Visible = false;
                cboTenDonVi.Visible = false;
                cboMaDonVi.Visible = false;
                cboHinhThucThanhToan.Visible = false;
                txtTenNguoiMuaHang.Visible = true;
                txtMaDonVi.Visible = true;
                txtTenDonVi.Visible = true;
                txtHinhThucThanhToan.Visible = true;

                btnXoaTenKhachHang.Visible = false;
                btnXoaMaDonVi.Visible = false;
                btnXoaTenDonVi.Visible = false;

                btnExportInvoice.Visible = false;
                btnExportAndPrint.Visible = false;
                btnCancel.Visible = false;
                btnPrint.Visible = true;
                btnClose2.Visible = true;
                
                txtMaSoThue.ReadOnly = true;
                txtAddress.ReadOnly = true;
                txtSoTaiKhoan.ReadOnly = true;
                txtGhiChu.ReadOnly = true;
                
                numVAT.Enabled = false;
                dtpkNgay.Enabled = false;

                raKhachTuLay.Enabled = false;
                raGuiQuaBuuDien.Enabled = false;

                btnOK.Visible = true;
                if (Global.StaffType == StaffType.Admin)
                {
                    chkDaThuTien.Enabled = true;
                    //raKhachTuLay.Enabled = true;
                    //raGuiQuaBuuDien.Enabled = true;
                    btnOK.Enabled = true;
                }
                else
                {
                    chkDaThuTien.Enabled = false;
                    //raKhachTuLay.Enabled = false;
                    //raGuiQuaBuuDien.Enabled = false;
                    btnOK.Enabled = false;
                }
            }
        }
        #endregion

        #region Properties
        public Invoice Invoice
        {
            get { return _invoice; }
            set { _invoice = value; }
        }
        #endregion

        #region UI Command
        private void DisplayThongTinKhachHang()
        {
            _flag3 = false;
            Result result = ThongTinKhachHangBus.GetThongTinKhachHangList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                cboTenNguoiMuaHang.DisplayMember = "TenKhachHang";
                cboTenNguoiMuaHang.ValueMember = "ThongTinKhachHangGUID";
                cboTenNguoiMuaHang.DataSource = dt;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinKhachHangList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinKhachHangList"));
            }

            _flag3 = true;
        }

        private void DisplayTenDonVi()
        {
            _flag3 = false;
            Result result = CompanyBus.GetTenCongTyList();//ThongTinKhachHangBus.GetTenDonViList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow[0] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);

                cboTenDonVi.DisplayMember = "TenCty";//"TenDonVi";
                cboTenDonVi.ValueMember = "TenCty";//"TenDonVi";
                cboTenDonVi.DataSource = dt;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetTenCongTyList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetTenCongTyList"));
            }
            _flag3 = true;
        }

        private void DisplayMaDonVi()
        {
            _flag3 = false;
            Result result = CompanyBus.GetMaCongTyList();//ThongTinKhachHangBus.GetMaDonViList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow[0] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);

                cboMaDonVi.DisplayMember = "MaCty";//"MaDonVi";
                cboMaDonVi.ValueMember = "MaCty";//"MaDonVi";
                cboMaDonVi.DataSource = dt;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetMaCongTyList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetMaCongTyList"));
            }
            _flag3 = true;
        }

        private void RefreshThongTinKhachHang(string thongTinKhachHangGUID)
        {
            _flag3 = false;
            cboMaDonVi.Text = string.Empty;
            cboTenDonVi.Text = string.Empty;
            txtMaSoThue.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtSoTaiKhoan.Text = string.Empty;

            if (thongTinKhachHangGUID.Trim() != string.Empty)
            {
                Result result = ThongTinKhachHangBus.GetThongTinKhachHang(thongTinKhachHangGUID);
                if (result.IsOK)
                {
                    ThongTinKhachHang ttkh = result.QueryResult as ThongTinKhachHang;
                    if (ttkh == null)
                    {
                        _flag3 = true;
                        return;
                    }

                    result = ThongTinKhachHangBus.GetThongTinMaDonVi(ttkh.MaDonVi);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinMaDonVi"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinMaDonVi"));
                        _flag3 = true;
                        return;
                    }

                    ThongTinKhachHang ttkh2 = result.QueryResult as ThongTinKhachHang;
                    if (ttkh2 != null)
                    {
                        cboTenDonVi.Text = ttkh2.TenDonVi;
                        cboMaDonVi.Text = ttkh2.MaDonVi;
                        txtMaSoThue.Text = ttkh2.MaSoThue;
                        txtAddress.Text = ttkh2.DiaChi;
                    }
                    else
                    {
                        result = ThongTinKhachHangBus.GetThongTinDonVi(ttkh.TenDonVi);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinDonVi"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinDonVi"));
                            _flag3 = true;
                            return;
                        }

                        ttkh2 = result.QueryResult as ThongTinKhachHang;
                        if (ttkh2 != null)
                        {
                            cboTenDonVi.Text = ttkh2.TenDonVi;
                            cboMaDonVi.Text = ttkh2.MaDonVi;
                            txtMaSoThue.Text = ttkh2.MaSoThue;
                            txtAddress.Text = ttkh2.DiaChi;
                        }
                    }
                    
                    txtSoTaiKhoan.Text = ttkh.SoTaiKhoan;
                    cboHinhThucThanhToan.SelectedIndex = ttkh.HinhThucThanhToan.Value;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinKhachHang"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinKhachHang"));
                }
            }
            _flag3 = true;
        }

        private void RefreshThongTinDonVi(string tenDonVi)
        {
            _flag3 = false;
            txtMaSoThue.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtSoTaiKhoan.Text = string.Empty;
            cboMaDonVi.Text = string.Empty;

            Result result = ThongTinKhachHangBus.GetThongTinDonVi(tenDonVi);
            if (result.IsOK)
            {
                ThongTinKhachHang ttkh = result.QueryResult as ThongTinKhachHang;
                if (ttkh == null)
                {
                    _flag3 = true;
                    return;
                }

                txtMaSoThue.Text = ttkh.MaSoThue;
                txtAddress.Text = ttkh.DiaChi;
                txtSoTaiKhoan.Text = ttkh.SoTaiKhoan;
                cboHinhThucThanhToan.SelectedIndex = ttkh.HinhThucThanhToan.Value;
                cboMaDonVi.Text = ttkh.MaDonVi;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinDonVi"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinDonVi"));
            }

            _flag3 = true;
        }

        private void RefreshThongTinMaDonVi(string maDonVi)
        {
            _flag3 = false;
            txtMaSoThue.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtSoTaiKhoan.Text = string.Empty;
            cboTenDonVi.Text = string.Empty;

            if (cboMaDonVi.Text.Trim() != string.Empty)
            {
                Result result = ThongTinKhachHangBus.GetThongTinMaDonVi(maDonVi);
                if (result.IsOK)
                {
                    ThongTinKhachHang ttkh = result.QueryResult as ThongTinKhachHang;
                    if (ttkh == null)
                    {
                        _flag3 = true;
                        return;
                    }

                    txtMaSoThue.Text = ttkh.MaSoThue;
                    txtAddress.Text = ttkh.DiaChi;
                    txtSoTaiKhoan.Text = ttkh.SoTaiKhoan;
                    cboHinhThucThanhToan.SelectedIndex = ttkh.HinhThucThanhToan.Value;
                    cboTenDonVi.Text = ttkh.TenDonVi;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinMaDonVi"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.GetThongTinMaDonVi"));
                }
            }

            _flag3 = true;
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = QuanLySoHoaDonBus.GetSoHoaDon();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                _invoiceCode = Utility.GetCode(string.Empty, count, 7);
                lbInvoiceCode.Text = string.Format("Số: {0}", _invoiceCode);

                SetMinMaxNgayXuatHoaDon(count);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("QuanLySoHoaDonBus.GetSoHoaDon"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetSoHoaDon"));
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void SetMinMaxNgayXuatHoaDon(int soHoaDon)
        {
            DateTime minDate = DateTime.Now;
            DateTime maxDate = DateTime.Now;
            Result result = QuanLySoHoaDonBus.GetMinMaxNgayXuatHoaDon(soHoaDon, ref minDate, ref maxDate);
            if (result.IsOK)
            {
                if (dtpkNgay.Value < minDate) dtpkNgay.Value = minDate;
                if (dtpkNgay.Value > maxDate) dtpkNgay.Value = maxDate;

                dtpkNgay.MinDate = minDate;
                dtpkNgay.MaxDate = maxDate;
            }
        }

        private void RefreshNo()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgDetail.Rows)
            {
                row.Cells["STT"].Value = index++;
            }
        }

        private void RefreshNo2()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgDetail2.Rows)
            {
                row.Cells["STT2"].Value = index++;
            }
        }

        private void DisplayInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_isView)
            {
                dgDetail.AllowUserToAddRows = false;
                dgDetail.AllowUserToDeleteRows = false;
                dgDetail.ReadOnly = true;

                lbMauSo.Text = string.Format("Mẫu số: {0}", _drInvoice["MauSo"].ToString());
                lbKiHieu.Text = string.Format("Kí hiệu: {0}", _drInvoice["KiHieu"].ToString());
                lbInvoiceCode.Text = string.Format("Số: {0}", _drInvoice["InvoiceCode"].ToString());
                dtpkNgay.Value = Convert.ToDateTime(_drInvoice["InvoiceDate"]);
                //DateTime dt = Convert.ToDateTime(_drInvoice["InvoiceDate"]);
                //string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                //string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                //string strYear = dt.Year.ToString();
                //lbDate.Text = string.Format("Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);
                cboTenDonVi.Text = _drInvoice["TenDonVi"].ToString();
                txtTenDonVi.Text = cboTenDonVi.Text;

                cboMaDonVi.Text = _drInvoice["MaDonVi"] as string;
                txtMaDonVi.Text = cboMaDonVi.Text;

                txtGhiChu.Text = _drInvoice["Notes"] as string;

                if (_drInvoice["MaSoThue"] != null && _drInvoice["MaSoThue"] != DBNull.Value)
                    txtMaSoThue.Text = _drInvoice["MaSoThue"].ToString();

                txtSoTaiKhoan.Text = _drInvoice["SoTaiKhoan"].ToString();
                cboHinhThucThanhToan.SelectedIndex = Convert.ToByte(_drInvoice["HinhThucThanhToan"]);
                txtHinhThucThanhToan.Text = cboHinhThucThanhToan.Text;
                numVAT.Value = (Decimal)Convert.ToDouble(_drInvoice["VAT"]);

                if (_drInvoice["TenNguoiMuaHang"] != null && _drInvoice["TenNguoiMuaHang"] != DBNull.Value &&
                    _drInvoice["TenNguoiMuaHang"].ToString().Trim() != string.Empty)
                {
                    cboTenNguoiMuaHang.Text = _drInvoice["TenNguoiMuaHang"].ToString();
                    txtTenNguoiMuaHang.Text = cboTenNguoiMuaHang.Text;
                }

                if (_drInvoice["DiaChi"] != null && _drInvoice["DiaChi"] != DBNull.Value)
                    txtAddress.Text = _drInvoice["DiaChi"].ToString();

                chkDaThuTien.Checked = Convert.ToBoolean(_drInvoice["DaThuTien"]);

                if (_drInvoice["HinhThucNhanHoaDon"] != null && _drInvoice["HinhThucNhanHoaDon"] != DBNull.Value)
                {
                    string hinhThucNhanHoaDon = _drInvoice["HinhThucNhanHoaDon"].ToString();
                    if (hinhThucNhanHoaDon.ToLower() == "khách tự lấy")
                    {
                        raKhachTuLay.Checked = true;
                        raGuiQuaBuuDien.Checked = false;
                    }
                    else
                    {
                        raKhachTuLay.Checked = false;
                        raGuiQuaBuuDien.Checked = true;
                    }
                }

                Result result = InvoiceBus.GetInvoiceDetail(_drInvoice["InvoiceGUID"].ToString());
                if (result.IsOK)
                {
                    DataTable dataSource = result.QueryResult as DataTable;
                    dgDetail.DataSource = dataSource;

                    foreach (DataRow row in dataSource.Rows)
                    {
                        double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                        _totalPrice += thanhTien;
                    }

                    if (_totalPrice > 0)
                        lbTotalAmount.Text = string.Format("{0}", _totalPrice.ToString("#,###"));

                    double vat = ((double)numVAT.Value * _totalPrice) / 100;
                    if (vat > 0)
                        lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));

                    _totalPayment = _totalPrice + vat;
                    _oldTotalPayment = _totalPayment;
                    lbTotalPayment.Text = string.Format("{0}", _totalPayment.ToString("#,###"));
                    lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)_totalPayment));

                    RefreshNo();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceDetail"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceDetail"));
                }
            }
            else
            {
                GenerateCode();

                lbMauSo.Text = string.Format("Mẫu số: {0}", Global.MauSoSauCung);
                lbKiHieu.Text = string.Format("Kí hiệu: {0}", Global.KiHieuSauCung);

                Result result = InvoiceBus.GetNgayXuatHoaDon(_invoiceCode);
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        DateTime dt = Convert.ToDateTime(result.QueryResult);
                        if (dt < dtpkNgay.MinDate) dt = dtpkNgay.MinDate;
                        if (dt > dtpkNgay.MaxDate) dt = dtpkNgay.MaxDate;

                        dtpkNgay.Value = dt;
                    }
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetNgayXuatHoaDon"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetNgayXuatHoaDon"));
                }

                //DateTime dt = DateTime.Now;
                //string strDay = dt.Day >= 10 ? dt.Day.ToString() : string.Format("0{0}", dt.Day);
                //string strMonth = dt.Month >= 10 ? dt.Month.ToString() : string.Format("0{0}", dt.Month);
                //string strYear = dt.Year.ToString();
                //lbDate.Text = string.Format("Ngày {0} tháng {1} năm {2}", strDay, strMonth, strYear);

                if (_receiptList != null && _receiptList.Count == 1)
                {
                    cboTenNguoiMuaHang.Text = _receiptList[0]["FullName"].ToString();
                    txtAddress.Text = _receiptList[0]["Address"].ToString();
                }

                result = InvoiceBus.GetChiTietPhieuThuDichVuList(_receiptList);
                if (result.IsOK)
                {
                    DataTable dataSource = result.QueryResult as DataTable;
                    dgDetail.DataSource = dataSource;

                    if (dataSource != null)
                    {
                        foreach (DataRow row in dataSource.Rows)
                        {
                            double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                            _totalPrice += thanhTien;
                        }
                    }

                    if (_totalPrice > 0)
                    lbTotalAmount.Text = string.Format("{0}", _totalPrice.ToString("#,###"));

                    double vat = ((double)numVAT.Value * _totalPrice) / 100;
                    if (vat > 0)
                        lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));

                    _totalPayment = _totalPrice + vat;
                    _oldTotalPayment = _totalPayment;
                    lbTotalPayment.Text = string.Format("{0}", _totalPayment.ToString("#,###"));
                    lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)_totalPayment));

                    RefreshNo();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetChiTietPhieuThuDichVuList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetChiTietPhieuThuDichVuList"));
                }
            }
        }

        private void CalculateThanhTien()
        {
            int rowIndex = dgDetail.CurrentCell.RowIndex;
            int colIndex = dgDetail.CurrentCell.ColumnIndex;

            if (rowIndex < 0 || colIndex < 0) return;

            int soLuong = 1;
            string strValue = dgDetail[3, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                soLuong = Convert.ToInt32(strValue);

            strValue = dgDetail[4, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            int donGia = 0;
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                donGia = Convert.ToInt32(strValue);

            strValue = dgDetail[5, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            int giam = 0;
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                giam = Convert.ToInt32(strValue);
            
            double thanhTien = soLuong * donGia;
            dgDetail[5, rowIndex].Value = thanhTien;

            CalculateTongTien();
        }

        private void CalculateThanhTien2()
        {
            int rowIndex = dgDetail2.CurrentCell.RowIndex;
            int colIndex = dgDetail2.CurrentCell.ColumnIndex;

            if (rowIndex < 0 || colIndex < 0) return;

            int soLuong = 1;
            string strValue = dgDetail2[3, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                soLuong = Convert.ToInt32(strValue);

            strValue = dgDetail2[4, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            int donGia = 0;
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                donGia = Convert.ToInt32(strValue);

            strValue = dgDetail2[5, rowIndex].EditedFormattedValue.ToString().Replace(",", "").Replace(".", "");
            int giam = 0;
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                giam = Convert.ToInt32(strValue);

            double thanhTien = soLuong * donGia;
            dgDetail2[5, rowIndex].Value = thanhTien;

            CalculateTongTien();
        }

        private void CalculateTongTien()
        {
            int rowCount = dgDetail.RowCount;//_isNew ? dgChiTiet.RowCount - 1 : dgChiTiet.RowCount;
            _totalPrice = 0;
            for (int i = 0; i < rowCount; i++)
            {
                double tt = 0;
                if (dgDetail[5, i].Value != null && dgDetail[5, i].Value != DBNull.Value)
                    tt = Convert.ToDouble(dgDetail[5, i].Value);
                _totalPrice += tt;
            }

            rowCount = dgDetail2.RowCount;//_isNew ? dgChiTiet.RowCount - 1 : dgChiTiet.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                double tt = 0;
                if (dgDetail2[5, i].Value != null && dgDetail2[5, i].Value != DBNull.Value)
                    tt = Convert.ToDouble(dgDetail2[5, i].Value);
                _totalPrice += tt;
            }

            if (_totalPrice == 0)
                lbTotalAmount.Text = _totalPrice.ToString();
            else
                lbTotalAmount.Text = string.Format("{0}", _totalPrice.ToString("#,###"));

            double vat = ((double)numVAT.Value * _totalPrice) / 100;
            if (vat > 0)
                lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));
            else
                lbVAT.Text = vat.ToString();

            _totalPayment = _totalPrice + vat;
            lbTotalPayment.Text = string.Format("{0}", _totalPayment.ToString("#,###"));
            lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)_totalPayment));
        }

        private bool OnPrint(string invoiceGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            dlgPrintType dlg = new dlgPrintType();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string exportFileName = string.Format("{0}\\Temp\\HDGTGT.xls", Application.StartupPath);
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.Lien1)
                    {
                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 1: Lưu"))
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, null);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return false;
                            }
                        }
                        else
                            return false;
                    }

                    if (dlg.Lien2)
                    {
                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                               Liên 2: Giao cho người mua"))
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, null);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return false;
                            }
                        }
                        else
                            return false;
                    }

                    if (dlg.Lien3)
                    {
                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 3: Nội bộ"))
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, null);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return false;
                            }
                        }
                        else
                            return false;
                    }
                }
            }

            return true;
        }

        private bool CheckInfo()
        {
            Result result = InvoiceBus.CheckInvoiceExistCode(Convert.ToInt32(_invoiceCode));
            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Số hóa đơn này đã được xuất rồi. Chương trình sẽ tạo lại số hóa đơn khác.", IconType.Information);
                    GenerateCode();
                    //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    //this.Close();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("InvoiceBus.CheckInvoiceExistCode"), IconType.Error);
                return false;
            }

            if (cboTenNguoiMuaHang.Text.Trim() == string.Empty && cboTenDonVi.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên người mua hàng hoặc đơn vị.", IconType.Information);
                cboTenNguoiMuaHang.Focus();
                return false;
            }

            if (dgDetail.RowCount <= 1 && dgDetail2.RowCount <= 1)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập ít nhất 1 dịch vụ hoặc thuốc để xuất hóa đơn.", IconType.Information);
                return false;
            }

            for (int i = 0; i < dgDetail.RowCount - 1; i++)
            {
                DataGridViewRow row = dgDetail.Rows[i];

                if (row.Cells[2].Value == null || row.Cells[2].Value == DBNull.Value)
                    row.Cells[2].Value = "Lần";

                if (row.Cells[1].Value == null || row.Cells[1].Value == DBNull.Value || row.Cells[1].Value.ToString().Trim() == string.Empty)
                {
                    tabDetail.SelectedIndex = 0;
                    MsgBox.Show(this.Text, "Vui lòng nhập tên dịch vụ.", IconType.Information);
                    return false;
                }

                if (row.Cells[2].Value.ToString().Trim() == string.Empty)
                {
                    tabDetail.SelectedIndex = 0;
                    MsgBox.Show(this.Text, "Vui lòng nhập đơn vị tính.", IconType.Information);
                    return false;
                }

                int donGia = 0;
                if (row.Cells[4].Value != null && row.Cells[4].Value != DBNull.Value && row.Cells[4].Value.ToString().Trim() != string.Empty)
                    donGia = Convert.ToInt32(row.Cells[4].Value);

                if (donGia <= 0)
                {
                    tabDetail.SelectedIndex = 0;
                    MsgBox.Show(this.Text, "Vui lòng nhập đơn giá.", IconType.Information);
                    return false;
                }
            }

            for (int i = 0; i < dgDetail2.RowCount - 1; i++)
            {
                DataGridViewRow row = dgDetail2.Rows[i];

                if (row.Cells[1].Value == null || row.Cells[1].Value == DBNull.Value || row.Cells[1].Value.ToString().Trim() == string.Empty)
                {
                    tabDetail.SelectedIndex = 1;
                    MsgBox.Show(this.Text, "Vui lòng nhập tên thuốc.", IconType.Information);
                    return false;
                }

                if (row.Cells[2].Value.ToString().Trim() == string.Empty)
                {
                    tabDetail.SelectedIndex = 1;
                    MsgBox.Show(this.Text, "Vui lòng nhập đơn vị tính.", IconType.Information);
                    return false;
                }

                int donGia = 0;
                if (row.Cells[4].Value != null && row.Cells[4].Value != DBNull.Value && row.Cells[4].Value.ToString().Trim() != string.Empty)
                    donGia = Convert.ToInt32(row.Cells[4].Value);

                if (donGia <= 0)
                {
                    tabDetail.SelectedIndex = 1;
                    MsgBox.Show(this.Text, "Vui lòng nhập đơn giá.", IconType.Information);
                    return false;
                }
            }

            if (_receiptList != null && _receiptList.Count > 0 && _totalPayment > _oldTotalPayment)
            {
                MsgBox.Show(this.Text, "Tổng số tiền xuất hóa đơn không được vượt quá tổng số tiền trong phiếu thu.", IconType.Information);
                return false;
            }

            return true;
        }

        private string GetReceiptGUIDListStr()
        {
            if (_receiptList == null || _receiptList.Count <= 0) return string.Empty;

            string str = string.Empty;
            foreach (DataRow row in _receiptList)
            {
                str += string.Format("{0},", row["ReceiptGUID"].ToString());
            }

            str = str.Substring(0, str.Length - 1);

            return str;
        }

        private bool ExportInvoice()
        {
            try
            {
                if (!CheckInfo()) return false;

                Invoice invoice = new Invoice();
                invoice.ReceiptGUIDList = GetReceiptGUIDListStr();
                invoice.InvoiceCode = _invoiceCode;
                invoice.InvoiceDate = dtpkNgay.Value;
                invoice.TenNguoiMuaHang = cboTenNguoiMuaHang.Text;
                invoice.DiaChi = txtAddress.Text;
                invoice.TenDonVi = cboTenDonVi.Text;
                invoice.MaDonVi = cboMaDonVi.Text;
                invoice.MaSoThue = txtMaSoThue.Text;
                invoice.SoTaiKhoan = txtSoTaiKhoan.Text;
                invoice.HinhThucThanhToan = (byte)cboHinhThucThanhToan.SelectedIndex;
                invoice.VAT = (double)numVAT.Value;
                invoice.CreatedDate = DateTime.Now;
                invoice.CreatedBy = Guid.Parse(Global.UserGUID);
                invoice.Status = (byte)Status.Actived;
                invoice.ChuaThuTien = !chkDaThuTien.Checked;
                invoice.MauSo = Global.MauSoSauCung;
                invoice.KiHieu = Global.KiHieuSauCung;
                invoice.HinhThucNhanHoaDon = raKhachTuLay.Checked ? "Khách tự lấy" : "Gởi qua bưu điện";
                invoice.Notes = txtGhiChu.Text;

                List<InvoiceDetail> addedDetails = new List<InvoiceDetail>();
                for (int i = 0; i < dgDetail.RowCount - 1; i++)
                {
                    DataGridViewRow row = dgDetail.Rows[i];
                    InvoiceDetail detail = new InvoiceDetail();
                    detail.CreatedDate = DateTime.Now;
                    detail.CreateBy = Guid.Parse(Global.UserGUID);
                    detail.TenDichVu = row.Cells["TenDichVu"].Value.ToString();

                    if (row.Cells["DonViTinh"].Value != null)
                        detail.DonViTinh = row.Cells["DonViTinh"].Value.ToString();
                    else
                        detail.DonViTinh = "Lần";

                    int soLuong = 1;
                    if (row.Cells["SoLuong"].Value != null && row.Cells["SoLuong"].Value != DBNull.Value)
                        soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    detail.SoLuong = soLuong;

                    int donGia = 0;
                    if (row.Cells["DonGia"].Value != null && row.Cells["DonGia"].Value != DBNull.Value)
                        donGia = Convert.ToInt32(row.Cells["DonGia"].Value);

                    detail.DonGia = donGia;

                    int thanhTien = 0;
                    if (row.Cells["ThanhTien"].Value != null && row.Cells["ThanhTien"].Value != DBNull.Value)
                        thanhTien = Convert.ToInt32(row.Cells["ThanhTien"].Value);

                    detail.ThanhTien = thanhTien;
                    detail.Loai = 0;

                    addedDetails.Add(detail);
                }

                for (int i = 0; i < dgDetail2.RowCount - 1; i++)
                {
                    DataGridViewRow row = dgDetail2.Rows[i];
                    InvoiceDetail detail = new InvoiceDetail();
                    detail.CreatedDate = DateTime.Now;
                    detail.CreateBy = Guid.Parse(Global.UserGUID);
                    detail.TenDichVu = row.Cells["TenThuoc"].Value.ToString();
                    detail.DonViTinh = row.Cells["DonViTinh2"].Value.ToString();

                    int soLuong = 1;
                    if (row.Cells["SoLuong2"].Value != null && row.Cells["SoLuong2"].Value != DBNull.Value)
                        soLuong = Convert.ToInt32(row.Cells["SoLuong2"].Value);
                    detail.SoLuong = soLuong;

                    int donGia = 0;
                    if (row.Cells["DonGia2"].Value != null && row.Cells["DonGia2"].Value != DBNull.Value)
                        donGia = Convert.ToInt32(row.Cells["DonGia2"].Value);

                    detail.DonGia = donGia;

                    int thanhTien = 0;
                    if (row.Cells["ThanhTien2"].Value != null && row.Cells["ThanhTien2"].Value != DBNull.Value)
                        thanhTien = Convert.ToInt32(row.Cells["ThanhTien2"].Value);

                    detail.ThanhTien = thanhTien;
                    detail.Loai = 1;

                    addedDetails.Add(detail);
                }

                Result result = InvoiceBus.InsertInvoice(invoice, addedDetails);
                if (result.IsOK)
                {
                    //Insert thông tin khách hàng
                    ThongTinKhachHang thongTinKhachHang = new ThongTinKhachHang();
                    thongTinKhachHang.TenKhachHang = invoice.TenNguoiMuaHang;
                    thongTinKhachHang.MaDonVi = invoice.MaDonVi;
                    //thongTinKhachHang.TenDonVi = invoice.TenDonVi;
                    //thongTinKhachHang.MaSoThue = invoice.MaSoThue;
                    //thongTinKhachHang.DiaChi = invoice.DiaChi;
                    thongTinKhachHang.SoTaiKhoan = invoice.SoTaiKhoan;
                    thongTinKhachHang.HinhThucThanhToan = invoice.HinhThucThanhToan;
                    result = ThongTinKhachHangBus.InsertThongTinKhachHang(thongTinKhachHang);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThongTinKhachHangBus.InsertThongTinKhachHang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.InsertThongTinKhachHang"));
                    }

                    _invoice = invoice;
                    if (!_isPrinted) return true;
                    OnPrint(invoice.InvoiceGUID.ToString());
                    return true;
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.InsertInvoice"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.InsertInvoice"));
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }

            return false;
        }

        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow row = dt.NewRow();
                row["ThuocGUID"] = Guid.Empty.ToString();
                row["TenThuoc"] = " ";
                dt.Rows.InsertAt(row, 0);

                TenThuoc.DataSource = dt;
                TenThuoc.DisplayMember = "TenThuoc";
                TenThuoc.ValueMember = "TenThuoc";
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
                this.Close();
            }
        }

        private string GetDonViTinh(string tenThuoc)
        {
            DataTable dt = TenThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("TenThuoc='{0}'", tenThuoc));
            if (rows != null && rows.Length > 0)
                return rows[0]["DonViTinh"].ToString();

            return string.Empty;
        }

        private void GetDanhSachDichVu()
        {
            Result result = ServicesBus.GetServicesList();
            if (result.IsOK)
                _dtServices = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
            }
        }

        private void XoaTenKhachHang()
        {
            if (cboTenNguoiMuaHang.Text.Trim() == string.Empty) return;

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa tên người mua hàng ?") == System.Windows.Forms.DialogResult.No) return;

            string thongTinKhachHangGUID = string.Empty;
            if (cboTenNguoiMuaHang.SelectedValue != null)
                thongTinKhachHangGUID = cboTenNguoiMuaHang.SelectedValue.ToString();
            Result result = ThongTinKhachHangBus.DeleteTenKhachHang(thongTinKhachHangGUID);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.DeleteTenKhachHang"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.DeleteTenKhachHang"));
                return;
            }

            _flag3 = false;
            DataTable dt = cboTenNguoiMuaHang.DataSource as DataTable;
            DataRow[] rows = dt.Select(string.Format("ThongTinKhachHangGUID='{0}'", thongTinKhachHangGUID));
            if (rows != null && rows.Length > 0)
            {
                dt.Rows.Remove(rows[0]);
            }
            //foreach (var item in cboTenNguoiMuaHang.Items)
            //{
            //    if (item.ToString() == tenKhachHang)
            //    {
            //        cboTenNguoiMuaHang.Items.Remove(item);
            //        break;
            //    }
            //}

            cboTenNguoiMuaHang.Text = string.Empty;

            _flag3 = true;
        }

        private void XoaMaDonVi()
        {
            if (cboMaDonVi.Text.Trim() == string.Empty) return;

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa mã đơn vị ?") == System.Windows.Forms.DialogResult.No) return;

            string maDonVi = cboMaDonVi.Text;
            Result result = ThongTinKhachHangBus.DeleteMaDonVi(maDonVi);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.DeleteMaDonVi"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.DeleteMaDonVi"));
                return;
            }

            _flag3 = false;
            foreach (var item in cboMaDonVi.Items)
            {
                if (item.ToString() == maDonVi)
                {
                    cboMaDonVi.Items.Remove(item);
                    break;
                }
            }

            cboMaDonVi.Text = string.Empty;

            _flag3 = true;
        }

        private void XoaTenDonVi()
        {
            if (cboTenDonVi.Text.Trim() == string.Empty) return;

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa tên đơn vị ?") == System.Windows.Forms.DialogResult.No) return;

            string tenDonVi = cboTenDonVi.Text;
            Result result = ThongTinKhachHangBus.DeleteTenDonVi(tenDonVi);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThongTinKhachHangBus.DeleteTenDonVi"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThongTinKhachHangBus.DeleteTenDonVi"));
                return;
            }

            _flag3 = false;
            foreach (var item in cboTenDonVi.Items)
            {
                if (item.ToString() == tenDonVi)
                {
                    cboTenDonVi.Items.Remove(item);
                    break;
                }
            }

            cboTenDonVi.Text = string.Empty;

            _flag3 = true;
        }
        #endregion

        #region Window Event Handlers
        private void btnXoaTenKhachHang_Click(object sender, EventArgs e)
        {
            XoaTenKhachHang();
        }

        private void btnXoaMaDonVi_Click(object sender, EventArgs e)
        {
            XoaMaDonVi();
        }

        private void btnXoaTenDonVi_Click(object sender, EventArgs e)
        {
            XoaTenDonVi();
        }

        private void dlgInvoiceInfo_Load(object sender, EventArgs e)
        {
            InitData();
            dtpkNgay.Value = DateTime.Now;
            DisplayThongTinKhachHang();
            DisplayTenDonVi();
            DisplayMaDonVi();
            DisplayInfo();

            if (!_isView) GetDanhSachDichVu();
        }

        private void dlgInvoiceInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!_isView)
                {
                    if (!ExportInvoice()) e.Cancel = true;
                    else if (_receiptList != null && _receiptList.Count > 0)
                    {
                        foreach (DataRow row in _receiptList)
                        {
                            row["IsExportedInvoice"] = true;
                        }
                    }
                }
            }
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            _isPrinted = false;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnExportAndPrint_Click(object sender, EventArgs e)
        {
            _isPrinted = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dgDetail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //RefreshNo();
        }

        private void numVAT_ValueChanged(object sender, EventArgs e)
        {
            double vat = ((double)numVAT.Value * _totalPrice) / 100;
            vat = Math.Round(vat + 0.05);
            if (vat > 0)
                lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));
            else
                lbVAT.Text = vat.ToString();

            double totalPayment = _totalPrice + vat;
            lbTotalPayment.Text = string.Format("{0}", totalPayment.ToString("#,###"));
            lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment).ToUpper());
        }

        private void numVAT_Leave(object sender, EventArgs e)
        {
            double vat = ((double)numVAT.Value * _totalPrice) / 100;
            vat = Math.Round(vat + 0.05);
            if (vat > 0)
                lbVAT.Text = string.Format("{0}", vat.ToString("#,###"));

            double totalPayment = _totalPrice + vat;
            lbTotalPayment.Text = string.Format("{0}", totalPayment.ToString("#,###"));
            lbBangChu.Text = string.Format("Số tiền viết bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPayment));
        }

        private void numVAT_KeyUp(object sender, KeyEventArgs e)
        {
            string strVat = numVAT.Text;
            if (e.KeyCode == Keys.Enter)
            {
                if (strVat == null || strVat.Trim() == string.Empty)
                {
                    numVAT.Value = 0;
                    numVAT.Text = "0.0";
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(_drInvoice["InvoiceGUID"].ToString());
        }

        private void dgDetail_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgDetail_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgDetail.CurrentCell.ColumnIndex >= 3 && dgDetail.CurrentCell.ColumnIndex <= 4)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress -= new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged -= new EventHandler(textBox_TextChanged);
                    textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged += new EventHandler(textBox_TextChanged);
                    _flag = true;
                }
            }

            if (dgDetail.CurrentCell.ColumnIndex == 1 && !_isView)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.DoubleClick -= new EventHandler(textBox_DoubleClick);
                    textBox.DoubleClick += new EventHandler(textBox_DoubleClick);
                }
            }
        }

        private void textBox_DoubleClick(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            dlgSelectSingleDichVu dlg = new dlgSelectSingleDichVu(_dtServices);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = dlg.ServiceName;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            TextBox textBox = (TextBox)sender;
            int colIndex = dgDetail.CurrentCell.ColumnIndex;
            if (colIndex < 3) return;

            if (textBox.Text == null || textBox.Text.Trim() == string.Empty)
            {
                if (colIndex == 3)
                    textBox.Text = "1";
                else
                    textBox.Text = "0";
            }

            string strValue = textBox.Text.Replace(",", "").Replace(".", "");

            try
            {
                int value = int.Parse(strValue);

                if (colIndex == 3 && value == 0)
                    textBox.Text = "1";
            }
            catch
            {
                textBox.Text = int.MaxValue.ToString();
            }

            CalculateThanhTien();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int colIndex = dgDetail.CurrentCell.ColumnIndex;
            if (colIndex != 3 && colIndex != 4) return;
            
            DataGridViewTextBoxEditingControl textBox = (DataGridViewTextBoxEditingControl)sender;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
            }
        }

        private void dgDetail_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            _flag = false;
            try
            {

                if (e.RowIndex < 0) return;
                dgDetail.CurrentCell = dgDetail[e.ColumnIndex, e.RowIndex];
                dgDetail.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex)
            {

            }

            _flag = true;
        }

        private void dgDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgDetail_Leave(object sender, EventArgs e)
        {
            if (!_isView)
            {
                int rowIndex = dgDetail.CurrentRow.Index;
                if (rowIndex < 0) return;
                dgDetail.CurrentCell = dgDetail[0, rowIndex];
                dgDetail.Rows[rowIndex].Selected = true;
            }
        }

        private void dgDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 5)
            {
                if (e.Value == null || e.Value.ToString() == string.Empty || e.Value == DBNull.Value)
                {
                    if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                        e.Value = "0";
                    else if (e.ColumnIndex == 3)
                        e.Value = "1";
                    else
                        e.Value = "Lần";
                }
            }
        }

        private void dgDetail_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            _flag = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string inviceGUID = _drInvoice["InvoiceGUID"].ToString();
            bool daThuTien = chkDaThuTien.Checked;
            Result result = InvoiceBus.UpdateDaThuTienInvoice(inviceGUID, daThuTien);
            if (result.IsOK)
            {
                _drInvoice["DaThuTien"] = daThuTien;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.UpdateDaThuTienInvoice"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.UpdateDaThuTienInvoice"));
            }
        }

        private void cboTenNguoiMuaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isView || !_flag3) return;
            string thongTinKhachHangGUID = string.Empty;
            if (cboTenNguoiMuaHang.SelectedValue != null)
                thongTinKhachHangGUID = cboTenNguoiMuaHang.SelectedValue.ToString();
            RefreshThongTinKhachHang(thongTinKhachHangGUID);
        }

        private void cboTenDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isView || !_flag3) return;
            RefreshThongTinDonVi(cboTenDonVi.Text);
        }

        private void cboMaDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isView || !_flag3) return;
            RefreshThongTinMaDonVi(cboMaDonVi.Text);
        }

        private void dgDetail2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.ColumnIndex <= 5)
            {
                if (e.Value == null || e.Value.ToString() == string.Empty || e.Value == DBNull.Value)
                {
                    if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                        e.Value = "0";
                    else if (e.ColumnIndex == 3)
                        e.Value = "1";
                }
            }
        }

        private void dgDetail2_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            _flag2 = false;
        }

        private void dgDetail2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            _flag2 = false;
            try
            {

                if (e.RowIndex < 0) return;
                dgDetail2.CurrentCell = dgDetail[e.ColumnIndex, e.RowIndex];
                dgDetail2.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex)
            {

            }

            _flag2 = true;
        }

        private void dgDetail2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgDetail2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgDetail2.CurrentCell.ColumnIndex >= 3 && dgDetail2.CurrentCell.ColumnIndex <= 4)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress -= new KeyPressEventHandler(textBox_KeyPress2);
                    textBox.TextChanged -= new EventHandler(textBox_TextChanged2);
                    textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress2);
                    textBox.TextChanged += new EventHandler(textBox_TextChanged2);
                    _flag2 = true;
                }
            }

            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
                cbo.DropDownStyle = ComboBoxStyle.DropDown;
                cbo.AutoCompleteMode = AutoCompleteMode.Suggest;
                _cboBox = e.Control as ComboBox;
                _cboBox.SelectedValueChanged -= new EventHandler(cmbox_SelectedValueChanged);
                _cboBox.SelectedValueChanged += new EventHandler(cmbox_SelectedValueChanged);
            }
        }

        private void cmbox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_flag2) return;

            if (dgDetail2.CurrentCell.ColumnIndex == 1)
            {
                _flag2 = false;
                DataGridViewComboBoxEditingControl cbo = (DataGridViewComboBoxEditingControl)sender;
                if (cbo.SelectedValue == null || cbo.SelectedValue.ToString() == "System.Data.DataRowView") return;
                string tenThuoc = cbo.SelectedValue.ToString();
                string donViTinh = GetDonViTinh(tenThuoc);
                dgDetail2.Rows[dgDetail2.CurrentRow.Index].Cells[2].Value = donViTinh;
                _flag2 = true;
            }
        }

        private void textBox_TextChanged2(object sender, EventArgs e)
        {
            if (!_flag2) return;
            TextBox textBox = (TextBox)sender;
            int colIndex = dgDetail2.CurrentCell.ColumnIndex;
            if (colIndex < 3) return;

            if (textBox.Text == null || textBox.Text.Trim() == string.Empty)
            {
                if (colIndex == 3)
                    textBox.Text = "1";
                else
                    textBox.Text = "0";
            }

            string strValue = textBox.Text.Replace(",", "").Replace(".", "");

            try
            {
                int value = int.Parse(strValue);

                if (colIndex == 3 && value == 0)
                    textBox.Text = "1";
            }
            catch
            {
                textBox.Text = int.MaxValue.ToString();
            }

            CalculateThanhTien2();
        }

        private void textBox_KeyPress2(object sender, KeyPressEventArgs e)
        {
            int colIndex = dgDetail2.CurrentCell.ColumnIndex;
            if (colIndex != 3 && colIndex != 4) return;

            DataGridViewTextBoxEditingControl textBox = (DataGridViewTextBoxEditingControl)sender;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
            }
        }

        private void dgDetail2_Leave(object sender, EventArgs e)
        {
            //if (!_isView)
            //{
            //    int rowIndex = dgDetail2.CurrentRow.Index;
            //    if (rowIndex < 0) return;
            //    dgDetail2.CurrentCell = dgDetail[0, rowIndex];
            //    dgDetail2.Rows[rowIndex].Selected = true;
            //}
        }

        private void dgDetail2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo2();
        }

        private void dgDetail2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo2();
        }
        #endregion

        
        
    }
}
