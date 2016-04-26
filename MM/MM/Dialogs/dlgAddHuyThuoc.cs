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
    public partial class dlgAddHuyThuoc : dlgBase
    {
        #region Members
        private HuyThuoc _huyThuoc = new HuyThuoc();
        #endregion

        #region Constructor
        public dlgAddHuyThuoc()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayHuy.Value = DateTime.Now;

            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["ThuocGUID"] = Guid.Empty;
                newRow["TenThuoc"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboThuoc.DataSource = dt;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
            }
        }

        private bool CheckInfo()
        {
            if (cboThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 thuốc.", IconType.Information);
                cboThuoc.Focus();
                return false;
            }

            string thuocGUID = cboThuoc.SelectedValue.ToString();
            Result result = HuyThuocBus.GetThuocTonKho(thuocGUID);
            if (result.IsOK)
            {
                int soLuongTon = 0;
                if (result.QueryResult != null)
                    soLuongTon = Convert.ToInt32(result.QueryResult);

                if ((int)numSoLuong.Value > soLuongTon)
                {
                    MsgBox.Show(this.Text, "Số lượng hủy lớn hơn số lượng thuốc còn trong kho. Vui lòng kiểm tra lại.", IconType.Information);
                    numSoLuong.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("HuyThuocBus.GetNgayHetHanCuaThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HuyThuocBus.GetNgayHetHanCuaThuoc"));
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
                    _huyThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    _huyThuoc.CreatedDate = DateTime.Now;
                    _huyThuoc.ThuocGUID = Guid.Parse(cboThuoc.SelectedValue.ToString());
                    _huyThuoc.SoLuong = (int)numSoLuong.Value;
                    _huyThuoc.NgayHuy = dtpkNgayHuy.Value;
                    _huyThuoc.Note = txtGhiChu.Text;

                    Result result = HuyThuocBus.InsertHuyThuoc(_huyThuoc);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("HuyThuocBus.InsertHuyThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("HuyThuocBus.InsertHuyThuoc"));
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

        private void DisplayThuocTonKho(string thuocGUID)
        {
            Result result = HuyThuocBus.GetThuocTonKho(thuocGUID);
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    int soLuongTon = Convert.ToInt32(result.QueryResult);
                    numSoLuongTon.Value = soLuongTon;
                }
                else
                    numSoLuongTon.Value = 0;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("HuyThuocBus.GetNgayHetHanCuaThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HuyThuocBus.GetNgayHetHanCuaThuoc"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddHuyThuoc_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void btnChonThuoc_Click(object sender, EventArgs e)
        {
            DataTable dtThuoc = cboThuoc.DataSource as DataTable;
            dlgSelectSingleThuoc dlg = new dlgSelectSingleThuoc(dtThuoc);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                cboThuoc.SelectedValue = dlg.MaThuocGUID;
            }
        }

        private void dlgAddHuyThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void cboThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThuoc.SelectedValue == null || cboThuoc.Text.Trim() == string.Empty)
            {
                numSoLuongTon.Value = 0;
                return;
            }

            string thuocGUID = cboThuoc.SelectedValue.ToString();
            DisplayThuocTonKho(thuocGUID);
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
