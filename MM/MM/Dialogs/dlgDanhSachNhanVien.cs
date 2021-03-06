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
using System.Threading;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgDanhSachNhanVien : dlgBase
    {
        #region Members
        private string _contractGUID = string.Empty;
        private int _type = 0; //0: Chua kham; 1: Kham chua du; 2: Kham day du
        private bool _isAscending = true;
        #endregion

        #region Constructor
        public dlgDanhSachNhanVien(string contractGUID, int type)
        {
            InitializeComponent();
            _contractGUID = contractGUID;
            _type = type;
        }
        #endregion

        #region UI Command
        private void OnDisplayInfo()
        {
            Result result = CompanyContractBus.GetDanhSachNhanVien(_contractGUID, _type, string.Empty, 0);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgDSNV.DataSource = result.QueryResult;
                    RefreshNo();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetDanhSachNhanVien"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetDanhSachNhanVien"));
            }
        }

        private void RefreshNo()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgDSNV.Rows)
            {
                row.Cells["STT"].Value = index++;
            }
        }

        private void DisplayInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayInfoProc));
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

        private void OnDisplayCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgDSNV.SelectedRows == null || dgDSNV.SelectedRows.Count <= 0) return;
            DataRow drMember = (dgDSNV.SelectedRows[0].DataBoundItem as DataRowView).Row;

            string patientGUID = drMember["PatientGUID"].ToString();
            Result result = CompanyContractBus.GetCheckList(_contractGUID, patientGUID);
            if (result.IsOK)
            {
                dgService.DataSource = result.QueryResult as DataTable;
                RefreshUsingService();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetCheckList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetCheckList"));
            }
        }

        private void RefreshUsingService()
        {
            foreach (DataGridViewRow row in dgService.Rows)
            {
                DataGridViewImageCell cell = row.Cells["Using"] as DataGridViewImageCell;
                DataRow drRow = (row.DataBoundItem as DataRowView).Row;
                if (!drRow.Table.Columns.Contains("Using"))
                    cell.Value = imgList.Images[1];
                else if (drRow["Using"] != null && drRow["Using"] != DBNull.Value && Convert.ToBoolean(drRow["Using"]))
                    cell.Value = imgList.Images[0];
                else
                    cell.Value = imgList.Images[1];
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            DisplayInfoAsThread();
        }

        private void dgDSNV_SelectionChanged(object sender, EventArgs e)
        {
            OnDisplayCheckList();
        }

        private void dgDSNV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgDSNV.DataSource as DataTable;
                List<DataRow> results = null;

                if (_isAscending)
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                               select p).ToList<DataRow>();
                }
                else
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                               select p).ToList<DataRow>();
                }


                DataTable newDataSource = dt.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgDSNV.DataSource = newDataSource;
            }
            else
                _isAscending = false;

            RefreshNo();
        }
        #endregion

        #region Working Thread
        private void OnDisplayInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayInfo();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
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
