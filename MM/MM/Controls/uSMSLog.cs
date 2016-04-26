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
using System.Threading;
using MM.Databasae;
using MM.Bussiness;
using MM.Common;

namespace MM.Controls
{
    public partial class uSMSLog : uBase
    {
        #region Members
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 0; //0: Tất cả; 1: Thành công; 2: Không thành công
        #endregion

        #region Constructor
        public uSMSLog()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = dgTracking.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgTracking.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                if (raTatCa.Checked) _type = 0;
                else if (raThanhCong.Checked) _type = 1;
                else if (raKhongThanhCong.Checked) _type = 2;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplaySMSLogListProc));
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

        private void OnDisplaySMSLogList()
        {
            Result result = SMSLogBus.GetSMSLogList(_fromDate, _toDate, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgTracking.DataSource = result.QueryResult;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SMSLogBus.GetSMSLogList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SMSLogBus.GetSMSLogList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void raTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (raTatCa.Checked) DisplayAsThread();
        }

        private void raThanhCong_CheckedChanged(object sender, EventArgs e)
        {
            if (raThanhCong.Checked) DisplayAsThread();
        }

        private void raKhongThanhCong_CheckedChanged(object sender, EventArgs e)
        {
            if (raKhongThanhCong.Checked) DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplaySMSLogListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplaySMSLogList();
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
