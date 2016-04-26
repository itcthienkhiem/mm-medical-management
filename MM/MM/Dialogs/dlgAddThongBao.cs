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
using System.IO;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddThongBao : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private ThongBao _thongBao = new ThongBao();
        private DataRow _drThongBao = null;
        private bool _isDuyet = false;        
        #endregion

        #region Constructor
        public dlgAddThongBao(bool isDuyet)
        {
            InitializeComponent();
            _isDuyet = isDuyet;
        }

        public dlgAddThongBao(DataRow drThongBao, bool isDuyet)
        {
            InitializeComponent();
            _isDuyet = isDuyet;
            _drThongBao = drThongBao;
            _isNew = false;
            this.Text = "Sua thong bao";
        }
        #endregion

        #region Properties
        public ThongBao ThongBao
        {
            get { return _thongBao; }
        }

        public bool IsDuyet
        {
            get
            {
                if (chkDuyetLan1.Enabled && chkDuyetLan1.Checked) return true;
                if (chkDuyetLan2.Enabled && chkDuyetLan2.Checked) return true;
                if (chkDuyetLan3.Enabled && chkDuyetLan3.Checked) return true;
                return false;
            }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {
                _thongBao.ThongBaoGUID = Guid.Parse(_drThongBao["ThongBaoGUID"].ToString());
                txtTenThongBao.Text = _drThongBao["TenThongBao"] as string;

                if (_drThongBao["ThongBaoBuff"] != null && _drThongBao["ThongBaoBuff"] != DBNull.Value)
                {
                    byte[] buff = (byte[])_drThongBao["ThongBaoBuff"];
                    _thongBao.ThongBaoBuff = new System.Data.Linq.Binary(buff);
                }

                if (_drThongBao["NgayDuyet1"] != null && _drThongBao["NgayDuyet1"] != DBNull.Value)
                {
                    txtNgayDuyet1.Text = Convert.ToDateTime(_drThongBao["NgayDuyet1"]).ToString("dd/MM/yyyy HH:mm:ss");
                    chkDuyetLan1.Checked = true;
                    _thongBao.NgayDuyet1 = Convert.ToDateTime(_drThongBao["NgayDuyet1"]);

                    if (_drThongBao["NguoiDuyet1GUID"] != null && _drThongBao["NguoiDuyet1GUID"] != DBNull.Value)
                        _thongBao.NguoiDuyet1GUID = Guid.Parse(_drThongBao["NguoiDuyet1GUID"].ToString());

                    byte[] buff = (byte[])_drThongBao["ThongBaoBuff1"];
                    _thongBao.ThongBaoBuff1 = new System.Data.Linq.Binary(buff);
                    chkDuyetLan1.Enabled = false;
                    chkDuyetLan2.Enabled = true;
                }

                if (_drThongBao["NgayDuyet2"] != null && _drThongBao["NgayDuyet2"] != DBNull.Value)
                {
                    txtNgayDuyet2.Text = Convert.ToDateTime(_drThongBao["NgayDuyet2"]).ToString("dd/MM/yyyy HH:mm:ss");
                    chkDuyetLan2.Checked = true;
                    _thongBao.NgayDuyet2 = Convert.ToDateTime(_drThongBao["NgayDuyet2"]);

                    if (_drThongBao["NguoiDuyet2GUID"] != null && _drThongBao["NguoiDuyet2GUID"] != DBNull.Value)
                        _thongBao.NguoiDuyet2GUID = Guid.Parse(_drThongBao["NguoiDuyet2GUID"].ToString());

                    byte[] buff = (byte[])_drThongBao["ThongBaoBuff2"];
                    _thongBao.ThongBaoBuff2 = new System.Data.Linq.Binary(buff);
                    chkDuyetLan2.Enabled = false;
                    chkDuyetLan3.Enabled = true;
                }

                if (_drThongBao["NgayDuyet3"] != null && _drThongBao["NgayDuyet3"] != DBNull.Value)
                {
                    txtNgayDuyet3.Text = Convert.ToDateTime(_drThongBao["NgayDuyet3"]).ToString("dd/MM/yyyy HH:mm:ss");
                    chkDuyetLan3.Checked = true;
                    _thongBao.NgayDuyet3 = Convert.ToDateTime(_drThongBao["NgayDuyet3"]);

                    if (_drThongBao["NguoiDuyet3GUID"] != null && _drThongBao["NguoiDuyet3GUID"] != DBNull.Value)
                        _thongBao.NguoiDuyet3GUID = Guid.Parse(_drThongBao["NguoiDuyet3GUID"].ToString());

                    byte[] buff = (byte[])_drThongBao["ThongBaoBuff3"];
                    _thongBao.ThongBaoBuff3 = new System.Data.Linq.Binary(buff);
                    chkDuyetLan3.Enabled = false;
                }

                if (_drThongBao["CreatedDate"] != null && _drThongBao["CreatedDate"] != DBNull.Value)
                    _thongBao.CreatedDate = Convert.ToDateTime(_drThongBao["CreatedDate"]);

                if (_drThongBao["CreatedBy"] != null && _drThongBao["CreatedBy"] != DBNull.Value)
                    _thongBao.CreatedBy = Guid.Parse(_drThongBao["CreatedBy"].ToString());

                if (_drThongBao["UpdatedDate"] != null && _drThongBao["UpdatedDate"] != DBNull.Value)
                    _thongBao.UpdatedDate = Convert.ToDateTime(_drThongBao["UpdatedDate"]);

                if (_drThongBao["UpdatedBy"] != null && _drThongBao["UpdatedBy"] != DBNull.Value)
                    _thongBao.UpdatedBy = Guid.Parse(_drThongBao["UpdatedBy"].ToString());

                if (_drThongBao["DeletedDate"] != null && _drThongBao["DeletedDate"] != DBNull.Value)
                    _thongBao.DeletedDate = Convert.ToDateTime(_drThongBao["DeletedDate"]);

                if (_drThongBao["DeletedBy"] != null && _drThongBao["DeletedBy"] != DBNull.Value)
                    _thongBao.DeletedBy = Guid.Parse(_drThongBao["DeletedBy"].ToString());

                _thongBao.Status = Convert.ToByte(_drThongBao["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtTenThongBao.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên thông báo.", IconType.Information);
                txtTenThongBao.Focus();
                return false;
            }

            if (_isNew)
            {
                if (txtTapTinThongBao.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn tập tin thông báo.", IconType.Information);
                    btnBrowser.Focus();
                    return false;
                }

                if (!File.Exists(txtTapTinThongBao.Text))
                {
                    MsgBox.Show(this.Text, "Tập tin thông báo không hợp lệ. Vui lòng kiểm tra lại.", IconType.Information);
                    btnBrowser.Focus();
                    return false;
                }
            }
            else
            {
                if (txtTapTinThongBao.Text.Trim() != string.Empty && !File.Exists(txtTapTinThongBao.Text))
                {
                    MsgBox.Show(this.Text, "Tập tin thông báo không hợp lệ. Vui lòng kiểm tra lại.", IconType.Information);
                    btnBrowser.Focus();
                    return false;
                }
            }

            return true;
        }

        private void OnSaveInfo()
        {
            try
            {
                MethodInvoker method = delegate
                {
                    if (_isNew)
                    {
                        _thongBao.CreatedDate = DateTime.Now;
                        _thongBao.CreatedBy = Guid.Parse(Global.UserGUID);
                        byte[] buff = Utility.GetBytesFromFile(txtTapTinThongBao.Text);
                        _thongBao.ThongBaoBuff = new System.Data.Linq.Binary(buff);

                        if (chkDuyetLan1.Checked)
                        {
                            _thongBao.NgayDuyet1 = DateTime.Now;
                            _thongBao.ThongBaoBuff1 = new System.Data.Linq.Binary(buff);
                            _thongBao.NguoiDuyet1GUID = Guid.Parse(Global.UserGUID);
                        }
                    }
                    else
                    {
                        _thongBao.UpdatedDate = DateTime.Now;
                        _thongBao.UpdatedBy = Guid.Parse(Global.UserGUID);

                        if (txtTapTinThongBao.Text.Trim() != string.Empty)
                        {
                            byte[] buff = Utility.GetBytesFromFile(txtTapTinThongBao.Text);
                            _thongBao.ThongBaoBuff = new System.Data.Linq.Binary(buff);
                        }

                        if (chkDuyetLan1.Enabled && chkDuyetLan1.Checked)
                        {
                            byte[] buff = _thongBao.ThongBaoBuff.ToArray();
                            _thongBao.ThongBaoBuff1 = new System.Data.Linq.Binary(buff);
                            _thongBao.NgayDuyet1 = DateTime.Now;
                            _thongBao.NguoiDuyet1GUID = Guid.Parse(Global.UserGUID);
                        }

                        if (chkDuyetLan2.Enabled && chkDuyetLan2.Checked)
                        {
                            byte[] buff = _thongBao.ThongBaoBuff.ToArray();
                            _thongBao.ThongBaoBuff2 = new System.Data.Linq.Binary(buff);
                            _thongBao.NgayDuyet2 = DateTime.Now;
                            _thongBao.NguoiDuyet2GUID = Guid.Parse(Global.UserGUID);
                        }

                        if (chkDuyetLan3.Enabled && chkDuyetLan3.Checked)
                        {
                            byte[] buff = _thongBao.ThongBaoBuff.ToArray();
                            _thongBao.ThongBaoBuff3 = new System.Data.Linq.Binary(buff);
                            _thongBao.NgayDuyet3 = DateTime.Now;
                            _thongBao.NguoiDuyet3GUID = Guid.Parse(Global.UserGUID);
                        }
                    }

                    _thongBao.TenThongBao = txtTenThongBao.Text;
                    _thongBao.Path = txtTapTinThongBao.Text;

                    Result result = ThongBaoBus.InsertThongBao(_thongBao);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("ThongBaoBus.InsertThongBao"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThongBaoBus.InsertThongBao"));
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
        #endregion

        #region Window Event Handlers
        private void dlgAddThongBao_Load(object sender, EventArgs e)
        {
            if (!_isNew) DisplayInfo();

            if (chkDuyetLan1.Enabled) chkDuyetLan1.Enabled = _isDuyet;
            if (chkDuyetLan2.Enabled) chkDuyetLan2.Enabled = _isDuyet;
            if (chkDuyetLan3.Enabled) chkDuyetLan3.Enabled = _isDuyet;
        }

        private void dlgAddThongBao_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (IsDuyet)
                {
                    if (MsgBox.Question(this.Text, "Bạn đã sẵn sàng duyệt thông báo này ?") == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                if (CheckInfo()) 
                    SaveInfoAsThread();
                else 
                    e.Cancel = true;
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtTapTinThongBao.Text = dlg.FileName;
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
