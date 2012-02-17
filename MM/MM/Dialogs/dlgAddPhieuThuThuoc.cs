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
    public partial class dlgAddPhieuThuThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private bool _flag = true;
        private PhieuThuThuoc _phieuThuThuoc = new PhieuThuThuoc();
        private DataRow _drPhieuThu = null;
        private string _tenCongTy = string.Empty;
        private DataTable _dataSourceBenhNhan = null;
        private ComboBox _cboBox = null;
        private TextBox _textBox = null;
        #endregion

        #region Constructor
        public dlgAddPhieuThuThuoc()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddPhieuThuThuoc(DataRow drPhieuThu)
        {
            InitializeComponent();
            _isNew = false;
            _drPhieuThu = drPhieuThu;
            btnOK.Enabled = false;
            dgChiTiet.AllowUserToAddRows = false;
            dgChiTiet.AllowUserToDeleteRows = false;
            dgChiTiet.ReadOnly = true;
            txtMaPhieuThu.ReadOnly = true;
            cboMaToaThuoc.Enabled = false;
            dtpkNgayThu.Enabled = false;
            txtMaBenhNhan.ReadOnly = true;
            txtTenBenhNhan.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            this.Text = "Xem phieu thu";
        }
        #endregion

        #region Properties
        public PhieuThuThuoc PhieuThuThuoc
        {
            get { return _phieuThuThuoc; }
        }
        #endregion

        #region UI Command
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
            dtpkNgayThu.Value = DateTime.Now;
            OnDisplayToaThuocList();
            OnDisplayThuoc();
            OnGetSanhSachBenhNhan();
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
                txtMaPhieuThu.Text = drPhieuThu["MaPhieuThuThuoc"] as string;
                cboMaToaThuoc.SelectedValue = drPhieuThu["ToaThuocGUID"].ToString();
                dtpkNgayThu.Value = Convert.ToDateTime(drPhieuThu["NgayThu"]);
                txtMaBenhNhan.Text = drPhieuThu["MaBenhNhan"] as string;
                txtTenBenhNhan.Text = drPhieuThu["TenBenhNhan"] as string;
                txtDiaChi.Text = drPhieuThu["DiaChi"] as string;

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
                CalculateTongTien();
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
                thuocGUIDDataGridViewTextBoxColumn.DataSource = result.QueryResult;
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
                newRow["MaToaThuoc"] = "----Không có----";
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
            for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
            {
                dgChiTiet[0, i].Value = i + 1;
            }
        }

        private void OnGetChiTietPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = PhieuThuThuocBus.GetChiTietPhieuThuThuoc(phieuThuThuocGUID);
            if (result.IsOK)
            {
                dgChiTiet.DataSource = result.QueryResult;

                UpdateDataSourceDonGia();

                RefreshNo();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetChiTietPhieuThuThuoc"));
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

                string thuocGUID = dr["ThuocGUID"].ToString();
                List<double> donGiaList = GetGiaThuoc(thuocGUID);
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

        private void OnDisplayChiTietToaThuoc(string toaThuocGUID)
        {
            Result result = KeToaBus.GetChiTietToaThuocList(toaThuocGUID);
            if (result.IsOK)
            {
                DataTable dtChiTiet = dgChiTiet.DataSource as DataTable;
                dtChiTiet.Rows.Clear();
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtChiTiet.NewRow();
                    string thuocGUID = row["ThuocGUID"].ToString();
                    newRow["ThuocGUID"] = thuocGUID;
                    string donViTinh = GetDonViTinh(thuocGUID);
                    List<double> donGiaList = GetGiaThuoc(thuocGUID);
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

                CalculateTongTien();
                RefreshNo();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"));
            }
        }

        private string GetDonViTinh(string thuocGUID)
        {
            DataTable dt = thuocGUIDDataGridViewTextBoxColumn.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["DonViTinh"].ToString();

            return string.Empty;
        }

        private void CalculateTongTien()
        {
            int rowCount = _isNew ? dgChiTiet.RowCount - 1 : dgChiTiet.RowCount;
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

            int soLuong = Convert.ToInt32(dgChiTiet[3, rowIndex].EditedFormattedValue.ToString().Replace(",", ""));

            string strValue = dgChiTiet[4, rowIndex].EditedFormattedValue.ToString().Replace(",", "");
            int donGia = 0;
            if (strValue != string.Empty && strValue != "System.Data.DataRowView")
                donGia = Convert.ToInt32(strValue);

            int giam = Convert.ToInt32(dgChiTiet[5, rowIndex].EditedFormattedValue.ToString().Replace(",", ""));

            double tienGiam = Math.Round((soLuong * donGia * giam / (double)100));
            double thanhTien = soLuong * donGia - tienGiam;
            dgChiTiet[6, rowIndex].Value = thanhTien;

            CalculateTongTien();
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

            if (dgChiTiet.RowCount > 1)
            {
                for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
                {
                    DataGridViewRow row = dgChiTiet.Rows[i];
                    
                    
                    if (row.Cells[1].Value == null || row.Cells[1].Value == DBNull.Value || row.Cells[1].Value.ToString() == string.Empty)
                    {
                        MsgBox.Show(this.Text, "Vui lòng chọn thuốc để xuất phiếu thu.", IconType.Information);
                        return false;
                    }

                    string thuocGUID = row.Cells[1].Value.ToString();
                    string tenThuoc = GetTenThuoc(thuocGUID);

                    if (row.Cells[4].Value.ToString() == "0")
                    {
                        MsgBox.Show(this.Text, string.Format("Thuốc '{0}' chưa có nhập giá bán. Vui lòng chọn thuốc khác.", tenThuoc), IconType.Information);
                        return false;
                    }


                    int soLuong = 1;
                    if (row.Cells[3].Value != null && row.Cells[3].Value != DBNull.Value)
                        soLuong = Convert.ToInt32(row.Cells[3].Value);

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

            if (dgChiTiet.RowCount > 2)
            {
                for (int i = 0; i < dgChiTiet.RowCount - 2; i++)
                {
                    DataGridViewRow row1 = dgChiTiet.Rows[i];
                    for (int j = i + 1; j < dgChiTiet.RowCount - 1; j++)
                    {
                        DataGridViewRow row2 = dgChiTiet.Rows[j];
                        if (row1.Cells[1].Value.ToString() == row2.Cells[1].Value.ToString())
                        {
                            string tenThuoc = GetTenThuoc(row1.Cells[1].Value.ToString());
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
            DataTable dt = thuocGUIDDataGridViewTextBoxColumn.DataSource as DataTable;
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
                    if (cboMaToaThuoc.SelectedValue.ToString() != Guid.Empty.ToString())
                        _phieuThuThuoc.ToaThuocGUID = Guid.Parse(cboMaToaThuoc.SelectedValue.ToString());
                    else
                        _phieuThuThuoc.ToaThuocGUID = Guid.Empty;

                    _phieuThuThuoc.NgayThu = dtpkNgayThu.Value;
                    _phieuThuThuoc.MaBenhNhan = txtMaBenhNhan.Text;
                    _phieuThuThuoc.TenBenhNhan = txtTenBenhNhan.Text;
                    _phieuThuThuoc.DiaChi = txtDiaChi.Text;
                    _phieuThuThuoc.TenCongTy = _tenCongTy;
                    _phieuThuThuoc.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _phieuThuThuoc.CreatedDate = DateTime.Now;
                        _phieuThuThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    }

                    DataTable dt = dgChiTiet.DataSource as DataTable;
                    List<ChiTietPhieuThuThuoc> addedList = new List<ChiTietPhieuThuThuoc>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached) continue;
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

                        ctptt.ThanhTien = Convert.ToDouble(row["ThanhTien"]);
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
        #endregion

        #region Window Event Handlers
        private void dlgAddPhieuThuThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (_isNew)
                OnGetChiTietPhieuThuThuoc(Guid.Empty.ToString());
            else
                DisplayInfo(_drPhieuThu);
        }

        private void dlgAddPhieuThuThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
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

        private void dgChiTiet_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgChiTiet_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgChiTiet_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

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
            if (dgChiTiet.CurrentCell.ColumnIndex == 1 || dgChiTiet.CurrentCell.ColumnIndex == 4)
            {
                _cboBox = e.Control as ComboBox;
                _cboBox.SelectedValueChanged -= new EventHandler(cmbox_SelectedValueChanged);
                _cboBox.SelectedValueChanged += new EventHandler(cmbox_SelectedValueChanged);
            }
            else if (dgChiTiet.CurrentCell.ColumnIndex >= 3 && dgChiTiet.CurrentCell.ColumnIndex <= 5)
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
            if (textBox.Text == string.Empty)
            {
                if (colIndex == 5)
                    textBox.Text = "0";
                else
                    textBox.Text = "1";
            }

            try
            {
                int value = int.Parse(textBox.Text);
                if (colIndex == 5 && value > 100)
                    textBox.Text = "100";
            }
            catch
            {
                if (colIndex == 5)
                    textBox.Text = "100";
                else
                    textBox.Text = int.MaxValue.ToString();
            }

            CalculateThanhTien();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl textBox = (DataGridViewTextBoxEditingControl)sender;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
            }
        }

        private void cmbox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_flag) return;

            if (dgChiTiet.CurrentCell.ColumnIndex == 1)
            {
                _flag = false;
                DataGridViewComboBoxEditingControl cbo = (DataGridViewComboBoxEditingControl)sender;
                if (cbo.SelectedValue == null || cbo.SelectedValue.ToString() == "System.Data.DataRowView") return;
                string thuocGUID = cbo.SelectedValue.ToString();
                string donViTinh = GetDonViTinh(thuocGUID);
                List<double> giaThuocList = GetGiaThuoc(thuocGUID);
                double giaThuoc = 0;
                if (giaThuocList != null && giaThuocList.Count > 0)
                    giaThuoc = giaThuocList[giaThuocList.Count - 1];

                dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[2].Value = donViTinh;

                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[4];
                DataTable dt = cell.DataSource as DataTable;
                if (dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add("DonGia", typeof(double));
                }
                else
                    dt.Rows.Clear();

                if (giaThuocList != null && giaThuocList.Count > 0)
                {
                    foreach (double gt in giaThuocList)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[0] = gt;
                        dt.Rows.Add(newRow);
                    }
                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = giaThuoc;
                    dt.Rows.Add(newRow);
                }

                cell.DataSource = dt;
                cell.DisplayMember = "DonGia";
                cell.ValueMember = "DonGia";

                dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[4].Value = giaThuoc;

                CalculateThanhTien();
                _flag = true;
            }
            else
                CalculateThanhTien();
        }

        private void dgChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.ColumnIndex <= 6)
            {
                if (e.Value == null || e.Value.ToString() == string.Empty || e.Value == DBNull.Value)
                {
                    if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                        e.Value = "0";
                    else
                        e.Value = "1";
                }
            }
        }

        private void thuocThayTheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgChiTiet.SelectedRows == null || dgChiTiet.SelectedRows.Count <= 0) return;
            int rowIndex = dgChiTiet.SelectedRows[0].Index;
            if (rowIndex == dgChiTiet.RowCount - 1) return;

            dgChiTiet.EndEdit();
            if (dgChiTiet.SelectedRows[0].Cells[1].Value == null || dgChiTiet.SelectedRows[0].Cells[1].Value == DBNull.Value) return;
            string thuocGUID = dgChiTiet.SelectedRows[0].Cells[1].Value.ToString();
            dlgThuocThayThe dlg = new dlgThuocThayThe(thuocGUID);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                dgChiTiet.SelectedRows[0].Cells[1].Value = dlg.ThuocThayThe;
                dgChiTiet.RefreshEdit();
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
