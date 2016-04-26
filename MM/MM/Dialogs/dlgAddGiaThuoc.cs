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
    public partial class dlgAddGiaThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private GiaThuoc _giaThuoc = new GiaThuoc();
        private DataRow _drGiaThuoc = null;
        #endregion

        #region Constructor
        public dlgAddGiaThuoc()
        {
            InitializeComponent();
        }

        public dlgAddGiaThuoc(DataRow drGiaThuoc)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua gia thuoc";
            _drGiaThuoc = drGiaThuoc;
        }
        #endregion

        #region Properties
        public GiaThuoc GiaThuoc
        {
            get { return _giaThuoc; }
        }

        public string TenThuoc
        {
            get { return cboThuoc.Text; }
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
            OnDisplayThuocList();
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

        private void RefreshDonViTinh()
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = cboThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string thuocGUID = cboThuoc.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows == null || rows.Length <= 0) return;

            string donViTinh = rows[0]["DonViTinh"].ToString();
            txtDonViTinh.Text = donViTinh;
        }

        private void DisplayInfo(DataRow drGiaThuoc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cboThuoc.SelectedValue = drGiaThuoc["ThuocGUID"].ToString();
                dtpkNgayApDung.Value = Convert.ToDateTime(drGiaThuoc["NgayApDung"]);
                numGiaBan.Value = (Decimal)Convert.ToDouble(drGiaThuoc["GiaBan"]);

                _giaThuoc.GiaThuocGUID = Guid.Parse(drGiaThuoc["GiaThuocGUID"].ToString());

                if (drGiaThuoc["CreatedDate"] != null && drGiaThuoc["CreatedDate"] != DBNull.Value)
                    _giaThuoc.CreatedDate = Convert.ToDateTime(drGiaThuoc["CreatedDate"]);

                if (drGiaThuoc["CreatedBy"] != null && drGiaThuoc["CreatedBy"] != DBNull.Value)
                    _giaThuoc.CreatedBy = Guid.Parse(drGiaThuoc["CreatedBy"].ToString());

                if (drGiaThuoc["UpdatedDate"] != null && drGiaThuoc["UpdatedDate"] != DBNull.Value)
                    _giaThuoc.UpdatedDate = Convert.ToDateTime(drGiaThuoc["UpdatedDate"]);

                if (drGiaThuoc["UpdatedBy"] != null && drGiaThuoc["UpdatedBy"] != DBNull.Value)
                    _giaThuoc.UpdatedBy = Guid.Parse(drGiaThuoc["UpdatedBy"].ToString());

                if (drGiaThuoc["DeletedDate"] != null && drGiaThuoc["DeletedDate"] != DBNull.Value)
                    _giaThuoc.DeletedDate = Convert.ToDateTime(drGiaThuoc["DeletedDate"]);

                if (drGiaThuoc["DeletedBy"] != null && drGiaThuoc["DeletedBy"] != DBNull.Value)
                    _giaThuoc.DeletedBy = Guid.Parse(drGiaThuoc["DeletedBy"].ToString());

                _giaThuoc.Status = Convert.ToByte(drGiaThuoc["GiaThuocStatus"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboThuoc.SelectedValue == null || cboThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn thuốc.", IconType.Information);
                cboThuoc.Focus();
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
                    _giaThuoc.ThuocGUID = Guid.Parse(cboThuoc.SelectedValue.ToString());
                    _giaThuoc.GiaBan = (double)numGiaBan.Value;
                    _giaThuoc.NgayApDung = new DateTime(dtpkNgayApDung.Value.Year, dtpkNgayApDung.Value.Month,
                        dtpkNgayApDung.Value.Day, 0, 0, 0, 0);
                    _giaThuoc.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _giaThuoc.CreatedDate = DateTime.Now;
                        _giaThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _giaThuoc.UpdatedDate = DateTime.Now;
                        _giaThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = GiaThuocBus.InsertGiaThuoc(_giaThuoc);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("GiaThuocBus.InsertGiaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaThuocBus.InsertGiaThuoc"));
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

        private double GetGiaThuocNhap()
        {
            double giaThuocNhap = 0;
            string maThuocGUID = cboThuoc.SelectedValue.ToString();
            Result result = LoThuocBus.GetGiaThuocNhap(maThuocGUID);

            if (result.IsOK)
                giaThuocNhap = Convert.ToDouble(result.QueryResult);
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LoThuocBus.GetGiaThuocNhap"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetGiaThuocNhap"));
            }

            return giaThuocNhap;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddGiaThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew)
                DisplayInfo(_drGiaThuoc);
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin giá thuốc ?") == System.Windows.Forms.DialogResult.Yes)
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
            double giaThuocNhap = GetGiaThuocNhap();

            if (giaThuocNhap == 0)
            {
                numGiaBan.Value = (Decimal)giaThuocNhap;
                return;
            }

            double delta = giaThuocNhap * 35 / 100;
            if (delta < 1500) delta = 1500;

            giaThuocNhap += delta;
            giaThuocNhap = Utility.FixedPrice(Convert.ToInt32(giaThuocNhap));
            numGiaBan.Value = (Decimal)giaThuocNhap;
        }

        private void btnQuiTacTinhVacxinDichTruyen_Click(object sender, EventArgs e)
        {
            if (!CheckInfo()) return;
            double giaThuocNhap = GetGiaThuocNhap();

            if (giaThuocNhap == 0)
            {
                numGiaBan.Value = (Decimal)giaThuocNhap;
                return;
            }

            giaThuocNhap += 100000;
            giaThuocNhap = Utility.FixedPrice(Convert.ToInt32(giaThuocNhap));
            numGiaBan.Value = (Decimal)giaThuocNhap;
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
