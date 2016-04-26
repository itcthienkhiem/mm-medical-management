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
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using MM.Exports;

namespace MM.Controls
{
    public partial class uBaoCaoThuocTonKho : uBase
    {
        #region Members
        private List<string> _thuocKeyList = new List<string>();
        #endregion

        #region Constructor
        public uBaoCaoThuocTonKho()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            _uThuocList.ClearData();
        }

        private void UpdateGUI()
        {
            _ucReportViewer.ShowPrintButton = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            _uThuocList.DisplayAsThread();
        }

        private void OnView()
        {
            Result result = ReportBus.GetThuocTonKhoList(_thuocKeyList);
            if (result.IsOK)
            {
                ReportDataSource reportDataSource = new ReportDataSource("ThuocResult",
                    (List<ThuocResult>)result.QueryResult);

                MethodInvoker method = delegate
                {
                    tabReport.SelectedTabIndex = 1;
                    _ucReportViewer.ViewReport("MM.Templates.rptThuocTonKho.rdlc", reportDataSource);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetThuocTonKhoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetThuocTonKhoList"));
            }
        }

        private void ViewAsThread()
        {
            try
            {
                if (_uThuocList.CheckedRows == null || _uThuocList.CheckedRows.Count <= 0)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 thuốc để xem báo cáo.", IconType.Information);
                    return;
                }

                _thuocKeyList.Clear();
                foreach (DataRow row in _uThuocList.CheckedRows)
                {
                    _thuocKeyList.Add(row["ThuocGUID"].ToString());
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

        private void OnExportToExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_uThuocList.CheckedRows == null || _uThuocList.CheckedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 1 thuốc để xuất excel.", IconType.Information);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _thuocKeyList.Clear();
                foreach (DataRow row in _uThuocList.CheckedRows)
                {
                    _thuocKeyList.Add(row["ThuocGUID"].ToString());
                }

                Result result = ReportBus.GetThuocTonKhoList(_thuocKeyList);
                if (result.IsOK)
                {
                    List<ThuocResult> thuocList = (List<ThuocResult>)result.QueryResult;
                    ExportExcel.ExportThuocTonKhoToExcel(dlg.FileName, thuocList);
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetThuocTonKhoList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetThuocTonKhoList"));
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportToExcel();
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
