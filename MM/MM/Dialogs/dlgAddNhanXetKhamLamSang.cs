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
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using System.Threading;

namespace MM.Dialogs
{
    public partial class dlgAddNhanXetKhamLamSang : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private NhanXetKhamLamSang _nhanXetKhamLamSang = new NhanXetKhamLamSang();
        private DataRow _drNhanXetKhamLamSang = null;
        #endregion

        #region Constructor
        public dlgAddNhanXetKhamLamSang()
        {
            InitializeComponent();
        }

        public dlgAddNhanXetKhamLamSang(DataRow drNhanXetKhamLamSang) : this()
        {
            this.Text = "Sua nhan xet kham lam sang";
            _isNew = false;
            _drNhanXetKhamLamSang = drNhanXetKhamLamSang;
        }
        #endregion

        #region Properties
        public NhanXetKhamLamSang NhanXetKhamLamSang
        {
            get { return _nhanXetKhamLamSang; }
            set { _nhanXetKhamLamSang = value; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {
                cboCoQuan.SelectedIndex = Convert.ToInt32(_drNhanXetKhamLamSang["Loai"]);
                txtNhanXet.Text = _drNhanXetKhamLamSang["NhanXet"] as string;
                _nhanXetKhamLamSang.NhanXetKhamLamSangGUID = Guid.Parse(_drNhanXetKhamLamSang["NhanXetKhamLamSangGUID"].ToString());

                if (_drNhanXetKhamLamSang["CreatedDate"] != null && _drNhanXetKhamLamSang["CreatedDate"] != DBNull.Value)
                    _nhanXetKhamLamSang.CreatedDate = Convert.ToDateTime(_drNhanXetKhamLamSang["CreatedDate"]);

                if (_drNhanXetKhamLamSang["CreatedBy"] != null && _drNhanXetKhamLamSang["CreatedBy"] != DBNull.Value)
                    _nhanXetKhamLamSang.CreatedBy = Guid.Parse(_drNhanXetKhamLamSang["CreatedBy"].ToString());

                if (_drNhanXetKhamLamSang["UpdatedDate"] != null && _drNhanXetKhamLamSang["UpdatedDate"] != DBNull.Value)
                    _nhanXetKhamLamSang.UpdatedDate = Convert.ToDateTime(_drNhanXetKhamLamSang["UpdatedDate"]);

                if (_drNhanXetKhamLamSang["UpdatedBy"] != null && _drNhanXetKhamLamSang["UpdatedBy"] != DBNull.Value)
                    _nhanXetKhamLamSang.UpdatedBy = Guid.Parse(_drNhanXetKhamLamSang["UpdatedBy"].ToString());

                if (_drNhanXetKhamLamSang["DeletedDate"] != null && _drNhanXetKhamLamSang["DeletedDate"] != DBNull.Value)
                    _nhanXetKhamLamSang.DeletedDate = Convert.ToDateTime(_drNhanXetKhamLamSang["DeletedDate"]);

                if (_drNhanXetKhamLamSang["DeletedBy"] != null && _drNhanXetKhamLamSang["DeletedBy"] != DBNull.Value)
                    _nhanXetKhamLamSang.DeletedBy = Guid.Parse(_drNhanXetKhamLamSang["DeletedBy"].ToString());

                _nhanXetKhamLamSang.Status = Convert.ToByte(_drNhanXetKhamLamSang["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboCoQuan.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn cơ quan.", IconType.Information);
                cboCoQuan.Focus();
                return false;
            }

            if (txtNhanXet.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập nhận xét.", IconType.Information);
                txtNhanXet.Focus();
                return false;
            }

            string nhanXetKhamLamSangGUID = _isNew ? string.Empty : _nhanXetKhamLamSang.NhanXetKhamLamSangGUID.ToString();
            Result result = NhanXetKhamLamSangBus.CheckNhanXetExist(nhanXetKhamLamSangGUID, cboCoQuan.SelectedIndex , txtNhanXet.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Nhận xét này đã tồn tại rồi. Vui lòng nhập nhận xét khác.", IconType.Information);
                    txtNhanXet.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhanXetKhamLamSangBus.CheckNhanXetExist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhanXetKhamLamSangBus.CheckNhanXetExist"));
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
                this.Invoke(new MethodInvoker(delegate()
                {
                    _nhanXetKhamLamSang.Loai = cboCoQuan.SelectedIndex;
                    _nhanXetKhamLamSang.NhanXet = txtNhanXet.Text;
                }));
                
                _nhanXetKhamLamSang.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _nhanXetKhamLamSang.CreatedDate = DateTime.Now;
                    _nhanXetKhamLamSang.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _nhanXetKhamLamSang.UpdatedDate = DateTime.Now;
                    _nhanXetKhamLamSang.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                Result result = NhanXetKhamLamSangBus.InsertNhanXetKhamLamSang(_nhanXetKhamLamSang);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhanXetKhamLamSangBus.InsertNhanXetKhamLamSang"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhanXetKhamLamSangBus.InsertNhanXetKhamLamSang"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }

        }
        #endregion

        #region Window Event Handlers
        private void dlgAddNhanXetKhamLamSang_Load(object sender, EventArgs e)
        {
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddNhanXetKhamLamSang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin nhận xét khám lâm sàng ?") == System.Windows.Forms.DialogResult.Yes)
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
