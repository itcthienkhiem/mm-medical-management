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
    public partial class dlgAddLoThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private LoThuoc _loThuoc = new LoThuoc();
        private DataRow _drLoThuoc = null;
        private bool _allowEdit = true;
        private string _ghiChu = string.Empty;
        #endregion

        #region Constructor
        public dlgAddLoThuoc()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddLoThuoc(DataRow drLoThuoc, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _allowEdit = allowEdit;
            this.Text = "Sua lo thuoc";
            _drLoThuoc = drLoThuoc;
        }

        #endregion

        #region Properties
        public LoThuoc LoThuoc
        {
            get { return _loThuoc; }
        }

        public string TenThuoc
        {
            get { return cboThuoc.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgaySanXuat.Value = DateTime.Now;
            dtpkNgayHetHan.Value = DateTime.Now;
            OnDisplayThuocList();
            OnDisplayNhaPhanPhoiList();
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = LoThuocBus.GetLoThuocCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaLoThuoc.Text = Utility.GetCode("L", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.GetLoThuocCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetLoThuocCount"));
            }
        }
        
        private void OnDisplayThuocList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
                cboThuoc.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
            }
        }

        private void OnDisplayNhaPhanPhoiList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = LoThuocBus.GetNhaPhanPhoiList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["NhaPhanPhoi"] == null || row["NhaPhanPhoi"] == DBNull.Value || row["NhaPhanPhoi"].ToString().Trim() == string.Empty)
                        continue;

                    cboNhaPhanPhoi.Items.Add(row["NhaPhanPhoi"].ToString());
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.GetNhaPhanPhoiList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetNhaPhanPhoiList"));
            }
        }

        private void RefreshDonViTinh()
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = cboThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string thuocGUID = cboThuoc.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows == null || rows.Length <= 0) return;

            string donViTinh = rows[0]["DonViTinh"].ToString();
            txtDonViTinhQuiDoi.Text = donViTinh;

            string oldDonViTinh = cboDonViTinhNhap.Text;
            cboDonViTinhNhap.Items.Clear();
            cboDonViTinhNhap.Items.Add("Hộp");

            if (donViTinh == "Viên")
                cboDonViTinhNhap.Items.Add("Vỉ");

            cboDonViTinhNhap.Items.Add(donViTinh);

            if (cboDonViTinhNhap.Items.Contains(oldDonViTinh))
                cboDonViTinhNhap.Text = oldDonViTinh;
            else
                cboDonViTinhNhap.SelectedIndex = 0;
        }

        private void DisplayInfo(DataRow drLoThuoc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaLoThuoc.Text = drLoThuoc["MaLoThuoc"] as string;
                txtTenLoThuoc.Text = drLoThuoc["TenLoThuoc"] as string;
                cboThuoc.SelectedValue = drLoThuoc["ThuocGUID"].ToString();
                txtSoDangKy.Text = drLoThuoc["SoDangKy"] as string;
                dtpkNgaySanXuat.Value = Convert.ToDateTime(drLoThuoc["NgaySanXuat"]);
                dtpkNgayHetHan.Value = Convert.ToDateTime(drLoThuoc["NgayHetHan"]);
                txtHangSanXuat.Text = drLoThuoc["HangSanXuat"] as string;
                cboNhaPhanPhoi.Text = drLoThuoc["NhaPhanPhoi"] as string;
                numSoLuongNhap.Value = (Decimal)Convert.ToInt32(drLoThuoc["SoLuongNhap"]);
                numGiaNhap.Value = (Decimal)Convert.ToDouble(drLoThuoc["GiaNhap"]);
                cboDonViTinhNhap.Text = drLoThuoc["DonViTinhNhap"] as string;
                txtDonViTinhQuiDoi.Text = drLoThuoc["DonViTinhQuiDoi"] as string;
                numSoLuongQuiDoi.Value = (Decimal)Convert.ToInt32(drLoThuoc["SoLuongQuiDoi"]);

                _loThuoc.LoThuocGUID = Guid.Parse(drLoThuoc["LoThuocGUID"].ToString());

                if (drLoThuoc["CreatedDate"] != null && drLoThuoc["CreatedDate"] != DBNull.Value)
                    _loThuoc.CreatedDate = Convert.ToDateTime(drLoThuoc["CreatedDate"]);

                if (drLoThuoc["CreatedBy"] != null && drLoThuoc["CreatedBy"] != DBNull.Value)
                    _loThuoc.CreatedBy = Guid.Parse(drLoThuoc["CreatedBy"].ToString());

                if (drLoThuoc["UpdatedDate"] != null && drLoThuoc["UpdatedDate"] != DBNull.Value)
                    _loThuoc.UpdatedDate = Convert.ToDateTime(drLoThuoc["UpdatedDate"]);

                if (drLoThuoc["UpdatedBy"] != null && drLoThuoc["UpdatedBy"] != DBNull.Value)
                    _loThuoc.UpdatedBy = Guid.Parse(drLoThuoc["UpdatedBy"].ToString());

                if (drLoThuoc["DeletedDate"] != null && drLoThuoc["DeletedDate"] != DBNull.Value)
                    _loThuoc.DeletedDate = Convert.ToDateTime(drLoThuoc["DeletedDate"]);

                if (drLoThuoc["DeletedBy"] != null && drLoThuoc["DeletedBy"] != DBNull.Value)
                    _loThuoc.DeletedBy = Guid.Parse(drLoThuoc["DeletedBy"].ToString());

                _loThuoc.Status = Convert.ToByte(drLoThuoc["LoThuocStatus"]);
                _loThuoc.Note = drLoThuoc["Note"] as string;
                txtGhiChu.Text = _loThuoc.Note;
                _loThuoc.MaLoThuoc = drLoThuoc["MaLoThuoc"] as string;


                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    groupBox1.Enabled = _allowEdit;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtMaLoThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã lô thuốc.", IconType.Information);
                txtMaLoThuoc.Focus();
                return false;
            }

            if (txtTenLoThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên lô thuốc.", IconType.Information);
                txtTenLoThuoc.Focus();
                return false;
            }

            if (cboThuoc.SelectedValue == null || cboThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn thuốc.", IconType.Information);
                cboThuoc.Focus();
                return false;
            }

            if (dtpkNgaySanXuat.Value >= dtpkNgayHetHan.Value)
            {
                MsgBox.Show(this.Text, "Ngày sản xuất phải nhỏ hơn ngày hết hạn", IconType.Information);
                dtpkNgaySanXuat.Focus();
                return false;
            }

            if (!_isNew)
            {
                int soLuongNhap = (int)numSoLuongNhap.Value * (int)numSoLuongQuiDoi.Value;
                Result rs = LoThuocBus.GetLoThuoc(_loThuoc.LoThuocGUID.ToString());
                if (!rs.IsOK)
                {
                    MsgBox.Show(this.Text, rs.GetErrorAsString("LoThuocBus.GetLoThuoc"), IconType.Error);
                    return false;
                }

                int soLuongXuat = (rs.QueryResult as LoThuoc).SoLuongXuat;

                if (soLuongNhap < soLuongXuat)
                {
                    MsgBox.Show(this.Text, "Số lượng nhập phải lớn hơn hoặc bằng số lượng xuất.", IconType.Information);
                    numSoLuongNhap.Focus();
                    return false;
                }
            }
            
            string loThuocGUID = _isNew ? string.Empty : _loThuoc.LoThuocGUID.ToString();
            Result result = LoThuocBus.CheckLoThuocExistCode(loThuocGUID, txtMaLoThuoc.Text);
            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã lô thuốc này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaLoThuoc.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.CheckLoThuocExistCode"), IconType.Error);
                return false;
            }

            return true;
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
                    _loThuoc.MaLoThuoc = txtMaLoThuoc.Text;
                    _loThuoc.TenLoThuoc = txtTenLoThuoc.Text;
                    _loThuoc.ThuocGUID = Guid.Parse(cboThuoc.SelectedValue.ToString());
                    _loThuoc.SoDangKy = txtSoDangKy.Text;
                    _loThuoc.NgaySanXuat = dtpkNgaySanXuat.Value;
                    _loThuoc.NgayHetHan = dtpkNgayHetHan.Value;
                    _loThuoc.HangSanXuat = txtHangSanXuat.Text;
                    _loThuoc.NhaPhanPhoi = cboNhaPhanPhoi.Text;
                    _loThuoc.SoLuongNhap = (int)numSoLuongNhap.Value;
                    _loThuoc.GiaNhap = (double)numGiaNhap.Value;
                    _loThuoc.DonViTinhNhap = cboDonViTinhNhap.Text;
                    _loThuoc.DonViTinhQuiDoi = txtDonViTinhQuiDoi.Text;
                    _loThuoc.SoLuongQuiDoi = (int)numSoLuongQuiDoi.Value;
                    _loThuoc.GiaNhapQuiDoi = (double)numGiaNhapQuiDoi.Value;
                    _loThuoc.SoLuongXuat = 0;

                    _loThuoc.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _loThuoc.CreatedDate = DateTime.Now;
                        _loThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _loThuoc.UpdatedDate = DateTime.Now;
                        _loThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);

                        if (_loThuoc.Note != null && _loThuoc.Note.Trim() != string.Empty)
                            _loThuoc.Note += string.Format("\r\nSửa {0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), _ghiChu);
                        else
                            _loThuoc.Note = string.Format("Sửa {0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), _ghiChu);
                    }     

                    Result result = LoThuocBus.InsertLoThuoc(_loThuoc);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.InsertLoThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.InsertLoThuoc"));
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

        private void RefreshGiaNhapQuiDoi()
        {
            double giaQuiDoi = (double)numGiaNhap.Value / (double)numSoLuongQuiDoi.Value;
            giaQuiDoi = Math.Round(giaQuiDoi, 0);
            //giaQuiDoi = Utility.FixedPrice(Convert.ToInt32(giaQuiDoi));
            lbGiaNhapQuiDoi.Text = string.Format("Giá nhập mỗi {0}:", txtDonViTinhQuiDoi.Text.ToLower());
            numGiaNhapQuiDoi.Value = (Decimal)giaQuiDoi;
        }

        private void GenerateTenLoThuoc()
        {
            txtTenLoThuoc.Text = string.Format("{0}-{1}", cboThuoc.Text, DateTime.Now.ToString("yyyyMMdd"));
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddLoThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew)
                DisplayInfo(_drLoThuoc);
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDonViTinh();
            RefreshGiaNhapQuiDoi();

            if (_isNew)
                GenerateTenLoThuoc();
        }

        private void cboDonViTinhNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDonViTinhNhap.Text == txtDonViTinhQuiDoi.Text)
            {
                numSoLuongQuiDoi.Enabled = false;
                numSoLuongQuiDoi.Value = 1;
            }
            else
                numSoLuongQuiDoi.Enabled = true;

            txtDVTNhap.Text = cboDonViTinhNhap.Text;
        }

        private void dlgAddLoThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                {
                    if (_isNew) SaveInfoAsThread();
                    else
                    {
                        dlgLyDoXoa dlg = new dlgLyDoXoa(_loThuoc.MaLoThuoc, 3, false);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            _ghiChu = dlg.Notes;
                            SaveInfoAsThread();
                        }
                        else
                            e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
            else if (_allowEdit)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin lô thuốc ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;

                        if (_isNew) SaveInfoAsThread();
                        else
                        {
                            dlgLyDoXoa dlg = new dlgLyDoXoa(_loThuoc.MaLoThuoc, 3, false);
                            if (dlg.ShowDialog(this) == DialogResult.OK)
                            {
                                _ghiChu = dlg.Notes;
                                SaveInfoAsThread();
                            }
                            else
                                e.Cancel = true;
                        }
                    }
                    else
                        e.Cancel = true;
                }
            }
        }

        private void numGiaNhap_ValueChanged(object sender, EventArgs e)
        {
            RefreshGiaNhapQuiDoi();
        }

        private void numSoLuongQuiDoi_ValueChanged(object sender, EventArgs e)
        {
            RefreshGiaNhapQuiDoi();
        }

        private void numGiaNhap_Leave(object sender, EventArgs e)
        {
            if (numGiaNhap.Text == string.Empty)
            {
                numGiaNhap.Text = "0";
                numGiaNhap.Value = 0;
            }
        }

        private void numSoLuongQuiDoi_Leave(object sender, EventArgs e)
        {
            if (numSoLuongQuiDoi.Text == string.Empty)
            {
                numSoLuongQuiDoi.Text = "1";
                numSoLuongQuiDoi.Value = 1;
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
