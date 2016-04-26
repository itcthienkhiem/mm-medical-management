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
using Microsoft.Reporting.WinForms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uBaoCaoCapCuuHetHan : uBase
    {
        #region Members
        private int _soNgayHetHan = 0;
        private List<string> _capCuuKeyList = new List<string>();
        #endregion

        #region Constructor
        public uBaoCaoCapCuuHetHan()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            _uKhoCapCuu.ClearData();
        }

        private void UpdateGUI()
        {
            _ucReportViewer.ShowPrintButton = AllowPrint;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            _uKhoCapCuu.DisplayAsThread();
        }

        private void OnView()
        {
            Result result = ReportBus.GetCapCuuHetHanList(_soNgayHetHan, _capCuuKeyList);
            if (result.IsOK)
            {
                ReportDataSource reportDataSource = new ReportDataSource("CapCuuResult",
                    (List<CapCuuResult>)result.QueryResult);

                MethodInvoker method = delegate
                {
                    tabReport.SelectedTabIndex = 1;
                    _ucReportViewer.ViewReport("MM.Templates.rptCapCuuHetHan.rdlc", reportDataSource);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetCapCuuHetHanList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetCapCuuHetHanList"));
            }
        }

        private void ViewAsThread()
        {
            try
            {
                if (_uKhoCapCuu.CheckedRows == null || _uKhoCapCuu.CheckedRows.Count <= 0)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 thuốc để xem báo cáo.", IconType.Information);
                    return;
                }

                _capCuuKeyList.Clear();
                _soNgayHetHan = (int)numSoNgayHetHan.Value;

                foreach (DataRow row in _uKhoCapCuu.CheckedRows)
                {
                    _capCuuKeyList.Add(row["KhoCapCuuGUID"].ToString());
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnViewProc));
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
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }

        private void xemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }
        #endregion

        #region Working Thread
        private void OnViewProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnView();
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
