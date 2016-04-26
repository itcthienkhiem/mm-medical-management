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
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddGiaCapCuu : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private GiaCapCuu _giaCapCuu = new GiaCapCuu();
        private DataRow _drGiaCapCuu = null;
        #endregion

        #region Constructor
        public dlgAddGiaCapCuu()
        {
            InitializeComponent();
        }

        public dlgAddGiaCapCuu(DataRow drGiaCapCuu)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua gia cap cuu";
            _drGiaCapCuu = drGiaCapCuu;
        }
        #endregion

        #region Properties
        public GiaCapCuu GiaCapCuu
        {
            get { return _giaCapCuu; }
        }

        public string TenCapCuu
        {
            get { return cboCapCuu.Text; }
        }

        public string DonViTinh
        {
            get { return txtDonViTinh.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayApDung.Value = DateTime.Now;
            OnDisplayCapCuuList();
        }

        private void OnDisplayCapCuuList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = KhoCapCuuBus.GetDanhSachCapCuu();
            if (result.IsOK)
                cboCapCuu.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"));
            }
        }

        private void RefreshDonViTinh()
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = cboCapCuu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string khoCapCuuGUID = cboCapCuu.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID));
            if (rows == null || rows.Length <= 0) return;

            string donViTinh = rows[0]["DonViTinh"].ToString();
            txtDonViTinh.Text = donViTinh;
        }

        private void DisplayInfo(DataRow drGiaCapCuu)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cboCapCuu.SelectedValue = drGiaCapCuu["KhoCapCuuGUID"].ToString();
                dtpkNgayApDung.Value = Convert.ToDateTime(drGiaCapCuu["NgayApDung"]);
                numGiaBan.Value = (Decimal)Convert.ToDouble(drGiaCapCuu["GiaBan"]);

                _giaCapCuu.GiaCapCuuGUID = Guid.Parse(drGiaCapCuu["GiaCapCuuGUID"].ToString());

                if (drGiaCapCuu["CreatedDate"] != null && drGiaCapCuu["CreatedDate"] != DBNull.Value)
                    _giaCapCuu.CreatedDate = Convert.ToDateTime(drGiaCapCuu["CreatedDate"]);

                if (drGiaCapCuu["CreatedBy"] != null && drGiaCapCuu["CreatedBy"] != DBNull.Value)
                    _giaCapCuu.CreatedBy = Guid.Parse(drGiaCapCuu["CreatedBy"].ToString());

                if (drGiaCapCuu["UpdatedDate"] != null && drGiaCapCuu["UpdatedDate"] != DBNull.Value)
                    _giaCapCuu.UpdatedDate = Convert.ToDateTime(drGiaCapCuu["UpdatedDate"]);

                if (drGiaCapCuu["UpdatedBy"] != null && drGiaCapCuu["UpdatedBy"] != DBNull.Value)
                    _giaCapCuu.UpdatedBy = Guid.Parse(drGiaCapCuu["UpdatedBy"].ToString());

                if (drGiaCapCuu["DeletedDate"] != null && drGiaCapCuu["DeletedDate"] != DBNull.Value)
                    _giaCapCuu.DeletedDate = Convert.ToDateTime(drGiaCapCuu["DeletedDate"]);

                if (drGiaCapCuu["DeletedBy"] != null && drGiaCapCuu["DeletedBy"] != DBNull.Value)
                    _giaCapCuu.DeletedBy = Guid.Parse(drGiaCapCuu["DeletedBy"].ToString());

                _giaCapCuu.Status = Convert.ToByte(drGiaCapCuu["GiaThuocStatus"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboCapCuu.SelectedValue == null || cboCapCuu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn cấp cứu.", IconType.Information);
                cboCapCuu.Focus();
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
                    _giaCapCuu.KhoCapCuuGUID = Guid.Parse(cboCapCuu.SelectedValue.ToString());
                    _giaCapCuu.GiaBan = (double)numGiaBan.Value;
                    _giaCapCuu.NgayApDung = new DateTime(dtpkNgayApDung.Value.Year, dtpkNgayApDung.Value.Month,
                        dtpkNgayApDung.Value.Day, 0, 0, 0, 0);
                    _giaCapCuu.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _giaCapCuu.CreatedDate = DateTime.Now;
                        _giaCapCuu.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _giaCapCuu.UpdatedDate = DateTime.Now;
                        _giaCapCuu.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = GiaCapCuuBus.InsertGiaCapCuu(_giaCapCuu);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("GiaCapCuuBus.InsertGiaCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaCapCuuBus.InsertGiaCapCuu"));
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

        private double GetGiaCapCuuNhap()
        {
            double giaCapCuuNhap = 0;
            string khoCapCuuGUID = cboCapCuu.SelectedValue.ToString();
            Result result = NhapKhoCapCuuBus.GetGiaCapCuuNhap(khoCapCuuGUID);

            if (result.IsOK)
                giaCapCuuNhap = Convert.ToDouble(result.QueryResult);
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetGiaCapCuuNhap"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetGiaCapCuuNhap"));
            }

            return giaCapCuuNhap;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddGiaThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew)
                DisplayInfo(_drGiaCapCuu);
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDonViTinh();
        }

        private void dlgAddGiaThuoc_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin giá cấp cứu ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void btnAppDungQuiTacTinhThuocVien_Click(object sender, EventArgs e)
        {
            if (!CheckInfo()) return;
            double giaCapCuuNhap = GetGiaCapCuuNhap();

            if (giaCapCuuNhap == 0)
            {
                numGiaBan.Value = (Decimal)giaCapCuuNhap;
                return;
            }

            double delta = giaCapCuuNhap * 35 / 100;
            if (delta < 1500) delta = 1500;

            giaCapCuuNhap += delta;
            giaCapCuuNhap = Utility.FixedPrice(Convert.ToInt32(giaCapCuuNhap));
            numGiaBan.Value = (Decimal)giaCapCuuNhap;
        }

        private void btnQuiTacTinhVacxinDichTruyen_Click(object sender, EventArgs e)
        {
            if (!CheckInfo()) return;
            double giaCapCuuNhap = GetGiaCapCuuNhap();

            if (giaCapCuuNhap == 0)
            {
                numGiaBan.Value = (Decimal)giaCapCuuNhap;
                return;
            }

            giaCapCuuNhap += 100000;
            giaCapCuuNhap = Utility.FixedPrice(Convert.ToInt32(giaCapCuuNhap));
            numGiaBan.Value = (Decimal)giaCapCuuNhap;
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
