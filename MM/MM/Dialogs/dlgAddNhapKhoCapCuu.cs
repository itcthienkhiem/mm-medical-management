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
    public partial class dlgAddNhapKhoCapCuu : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private NhapKhoCapCuu _nhapKhoCapCuu = new NhapKhoCapCuu();
        private DataRow _drNhapKhoCapCuu = null;
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddNhapKhoCapCuu()
        {
            InitializeComponent();
        }

        public dlgAddNhapKhoCapCuu(DataRow drNhapKhoCapCuu, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _allowEdit = allowEdit;
            this.Text = "Sua nhap kho cap cuu";
            _drNhapKhoCapCuu = drNhapKhoCapCuu;
        }

        #endregion

        #region Properties
        public NhapKhoCapCuu NhapKhoCapCuu
        {
            get { return _nhapKhoCapCuu; }
        }

        public string TenCapCuu
        {
            get { return cboKhoCapCuu.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayNhap.Value = DateTime.Now;
            dtpkNgaySanXuat.Value = DateTime.Now;
            dtpkNgayHetHan.Value = DateTime.Now;
            OnDisplayKhoCapCuuList();
            OnDisplayNhaPhanPhoiList();
        }

        private void OnDisplayKhoCapCuuList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = KhoCapCuuBus.GetDanhSachCapCuu();
            if (result.IsOK)
                cboKhoCapCuu.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"));
            }
        }

        private void OnDisplayNhaPhanPhoiList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = NhapKhoCapCuuBus.GetNhaPhanPhoiList();
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
                MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetNhaPhanPhoiList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetNhaPhanPhoiList"));
            }
        }

        private void RefreshDonViTinh()
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = cboKhoCapCuu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string khoCapCuuGUID = cboKhoCapCuu.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID));
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

        private void DisplayInfo(DataRow drNhapKhoCapCuu)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dtpkNgayNhap.Value = Convert.ToDateTime(drNhapKhoCapCuu["NgayNhap"]);
                cboKhoCapCuu.SelectedValue = drNhapKhoCapCuu["KhoCapCuuGUID"].ToString();
                txtSoDangKy.Text = drNhapKhoCapCuu["SoDangKy"] as string;
                dtpkNgaySanXuat.Value = Convert.ToDateTime(drNhapKhoCapCuu["NgaySanXuat"]);
                dtpkNgayHetHan.Value = Convert.ToDateTime(drNhapKhoCapCuu["NgayHetHan"]);
                txtHangSanXuat.Text = drNhapKhoCapCuu["HangSanXuat"] as string;
                cboNhaPhanPhoi.Text = drNhapKhoCapCuu["NhaPhanPhoi"] as string;
                numSoLuongNhap.Value = (Decimal)Convert.ToInt32(drNhapKhoCapCuu["SoLuongNhap"]);
                numGiaNhap.Value = (Decimal)Convert.ToDouble(drNhapKhoCapCuu["GiaNhap"]);
                cboDonViTinhNhap.Text = drNhapKhoCapCuu["DonViTinhNhap"] as string;
                txtDonViTinhQuiDoi.Text = drNhapKhoCapCuu["DonViTinhQuiDoi"] as string;
                numSoLuongQuiDoi.Value = (Decimal)Convert.ToInt32(drNhapKhoCapCuu["SoLuongQuiDoi"]);

                _nhapKhoCapCuu.NhapKhoCapCuuGUID = Guid.Parse(drNhapKhoCapCuu["NhapKhoCapCuuGUID"].ToString());

                if (drNhapKhoCapCuu["CreatedDate"] != null && drNhapKhoCapCuu["CreatedDate"] != DBNull.Value)
                    _nhapKhoCapCuu.CreatedDate = Convert.ToDateTime(drNhapKhoCapCuu["CreatedDate"]);

                if (drNhapKhoCapCuu["CreatedBy"] != null && drNhapKhoCapCuu["CreatedBy"] != DBNull.Value)
                    _nhapKhoCapCuu.CreatedBy = Guid.Parse(drNhapKhoCapCuu["CreatedBy"].ToString());

                if (drNhapKhoCapCuu["UpdatedDate"] != null && drNhapKhoCapCuu["UpdatedDate"] != DBNull.Value)
                    _nhapKhoCapCuu.UpdatedDate = Convert.ToDateTime(drNhapKhoCapCuu["UpdatedDate"]);

                if (drNhapKhoCapCuu["UpdatedBy"] != null && drNhapKhoCapCuu["UpdatedBy"] != DBNull.Value)
                    _nhapKhoCapCuu.UpdatedBy = Guid.Parse(drNhapKhoCapCuu["UpdatedBy"].ToString());

                if (drNhapKhoCapCuu["DeletedDate"] != null && drNhapKhoCapCuu["DeletedDate"] != DBNull.Value)
                    _nhapKhoCapCuu.DeletedDate = Convert.ToDateTime(drNhapKhoCapCuu["DeletedDate"]);

                if (drNhapKhoCapCuu["DeletedBy"] != null && drNhapKhoCapCuu["DeletedBy"] != DBNull.Value)
                    _nhapKhoCapCuu.DeletedBy = Guid.Parse(drNhapKhoCapCuu["DeletedBy"].ToString());

                _nhapKhoCapCuu.Status = Convert.ToByte(drNhapKhoCapCuu["NhapKhoCapCuuStatus"]);

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
            if (cboKhoCapCuu.SelectedValue == null || cboKhoCapCuu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn thông tin cấp cứu.", IconType.Information);
                cboKhoCapCuu.Focus();
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
                Result rs = NhapKhoCapCuuBus.GetNhapKhoCapCuu(_nhapKhoCapCuu.NhapKhoCapCuuGUID.ToString());
                if (!rs.IsOK)
                {
                    MsgBox.Show(this.Text, rs.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuu"), IconType.Error);
                    return false;
                }

                int soLuongXuat = (rs.QueryResult as NhapKhoCapCuuView).SoLuongXuat;

                if (soLuongNhap < soLuongXuat)
                {
                    MsgBox.Show(this.Text, "Số lượng nhập phải lớn hơn hoặc bằng số lượng xuất.", IconType.Information);
                    numSoLuongNhap.Focus();
                    return false;
                }
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
                    _nhapKhoCapCuu.NgayNhap = dtpkNgayNhap.Value;
                    _nhapKhoCapCuu.KhoCapCuuGUID = Guid.Parse(cboKhoCapCuu.SelectedValue.ToString());
                    _nhapKhoCapCuu.SoDangKy = txtSoDangKy.Text;
                    _nhapKhoCapCuu.NgaySanXuat = dtpkNgaySanXuat.Value;
                    _nhapKhoCapCuu.NgayHetHan = dtpkNgayHetHan.Value;
                    _nhapKhoCapCuu.HangSanXuat = txtHangSanXuat.Text;
                    _nhapKhoCapCuu.NhaPhanPhoi = cboNhaPhanPhoi.Text;
                    _nhapKhoCapCuu.SoLuongNhap = (int)numSoLuongNhap.Value;
                    _nhapKhoCapCuu.GiaNhap = (double)numGiaNhap.Value;
                    _nhapKhoCapCuu.DonViTinhNhap = cboDonViTinhNhap.Text;
                    _nhapKhoCapCuu.DonViTinhQuiDoi = txtDonViTinhQuiDoi.Text;
                    _nhapKhoCapCuu.SoLuongQuiDoi = (int)numSoLuongQuiDoi.Value;
                    _nhapKhoCapCuu.GiaNhapQuiDoi = (double)numGiaNhapQuiDoi.Value;
                    _nhapKhoCapCuu.SoLuongXuat = 0;
                    _nhapKhoCapCuu.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _nhapKhoCapCuu.CreatedDate = DateTime.Now;
                        _nhapKhoCapCuu.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _nhapKhoCapCuu.UpdatedDate = DateTime.Now;
                        _nhapKhoCapCuu.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = NhapKhoCapCuuBus.InsertNhapKhoCapCuu(_nhapKhoCapCuu);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.InsertNhapKhoCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.InsertNhapKhoCapCuu"));
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
            lbGiaNhapQuiDoi.Text = string.Format("Giá nhập mỗi {0}:", txtDonViTinhQuiDoi.Text.ToLower());
            numGiaNhapQuiDoi.Value = (Decimal)giaQuiDoi;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddLoThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew)
                DisplayInfo(_drNhapKhoCapCuu);
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDonViTinh();
            RefreshGiaNhapQuiDoi();
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
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else if (_allowEdit)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin nhập kho cấp cứu ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void dtpkNgayNhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
        
    }
}
