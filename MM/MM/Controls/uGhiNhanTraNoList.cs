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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using System.Threading;
using MM.Bussiness;
using MM.Dialogs;

namespace MM.Controls
{
    #region Delegate Events
    public delegate void CloseClickEventHandler();
    #endregion

    public partial class uGhiNhanTraNoList : uBase
    {
        #region Members
        public LoaiPT LoaiPT = LoaiPT.DichVu;
        public string PhieuThuGUID = string.Empty;
        public bool DaThuTien = true;
        public event CloseClickEventHandler OnCloseEvent = null;
        public bool IsDataChange = false;
        #endregion

        #region Constructor
        public uGhiNhanTraNoList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = Global.AllowAddGhiNhanTraNo;
            btnEdit.Enabled = Global.AllowEditGhiNhanTraNo;
            btnDelete.Enabled = Global.AllowDeleteGhiNhanTraNo;

            addToolStripMenuItem.Enabled = Global.AllowAddGhiNhanTraNo;
            editToolStripMenuItem.Enabled = Global.AllowEditGhiNhanTraNo;
            deleteToolStripMenuItem.Enabled = Global.AllowDeleteGhiNhanTraNo;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayGhiNhanCongNoListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayGhiNhanCongNoList()
        {
            Result result = GhiNhanTraNoBus.GetGhiNhanTraNoList(PhieuThuGUID, LoaiPT);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    dgTraNo.DataSource = result.QueryResult;

                    CalculateCongNo();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("GhiNhanTraNoBus.GetGhiNhanTraNoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GhiNhanTraNoBus.GetGhiNhanTraNoList"));
            }
        }

        private void CalculateCongNo()
        {
            if (!DaThuTien)
            {
                //Get tổng tiền đã trả
                double tongTienTra = 0;
                Result result = GhiNhanTraNoBus.GetTongTienTraNo(string.Empty, PhieuThuGUID, LoaiPT);
                if (result.IsOK)
                    tongTienTra = Convert.ToDouble(result.QueryResult);
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienTraNo"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienTraNo"));
                }

                //Get tổng tiền còn nợ
                double tongTienNo = 0;
                result = GhiNhanTraNoBus.GetTongTienPhieuThu(PhieuThuGUID, LoaiPT);
                if (result.IsOK)
                    tongTienNo = Convert.ToDouble(result.QueryResult);
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienPhieuThu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienPhieuThu"));
                }

                tongTienNo = tongTienNo - tongTienTra;

                lbConNo.Text = string.Format("Còn nợ: {0:N0} VNĐ", tongTienNo);
            }
            else
                lbConNo.Text = "Còn nợ: 0 VNĐ";
        }

        private void OnAdd()
        {
            if (DaThuTien)
            {
                MsgBox.Show(Application.ProductName, "Phiếu thu này đã thanh toán tiền hết rồi.", IconType.Information);
                return;
            }

            dlgAddGhiNhanTraNo dlg = new dlgAddGhiNhanTraNo(PhieuThuGUID, LoaiPT);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                IsDataChange = true;
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgTraNo.SelectedRows == null || dgTraNo.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 ghi nhận trả nợ.", IconType.Information);
                return;
            }

            DataRow drData = (dgTraNo.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddGhiNhanTraNo dlg = new dlgAddGhiNhanTraNo(drData, PhieuThuGUID, LoaiPT);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                IsDataChange = true;
                DaThuTien = dlg.IsDaTraDu;
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> keys = new List<string>();
            DataTable dt = dgTraNo.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    keys.Add(row["GhiNhanTraNoGUID"].ToString());
            }

            if (keys.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những ghi nhận trả nợ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {

                    Result result = GhiNhanTraNoBus.DeleteGhiNhanTraNo(keys, PhieuThuGUID, LoaiPT);
                    if (result.IsOK)
                    {
                        IsDataChange = true;
                        DaThuTien = false;
                        DisplayAsThread();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("GhiNhanTraNoBus.DeleteGhiNhanTraNo"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GhiNhanTraNoBus.DeleteGhiNhanTraNo"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những ghi nhận trả nợ cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgTraNo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Global.AllowEditGhiNhanTraNo) return;
            OnEdit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgTraNo.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (OnCloseEvent != null) OnCloseEvent();
        }
        #endregion

        #region Working Thread
        private void OnDisplayGhiNhanCongNoListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayGhiNhanCongNoList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
