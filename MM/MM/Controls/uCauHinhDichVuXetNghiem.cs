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
using MM.Dialogs;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Exports;

namespace MM.Controls
{
    public partial class uCauHinhDichVuXetNghiem : uBase
    {
        #region Members
        private string _name = string.Empty;
        #endregion

        #region Constructor
        public uCauHinhDichVuXetNghiem()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            dgService.ReadOnly = !AllowEdit;
        }

        public void ClearData()
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgService.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                _name = txtTenDichVu.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServicesListProc));
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

        public override void SearchAsThread()
        {
            try
            {
                _name = txtTenDichVu.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayServicesList()
        {
            lock (ThisLock)
            {
                Result result = ServicesBus.GetCauHinhDichVuXetNghiem(_name);
                if (result.IsOK)
                {
                    dgService.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        dgService.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetCauHinhDichVuXetNghiem"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetCauHinhDichVuXetNghiem"));
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void dgService_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 2) return;

            DataRow row = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string serviceGUID = row["ServiceGUID"].ToString();

            DataGridViewCheckBoxCell cell1 = (DataGridViewCheckBoxCell)dgService.Rows[e.RowIndex].Cells[2];
            bool isChecked1 = Convert.ToBoolean(cell1.EditingCellFormattedValue);

            DataGridViewCheckBoxCell cell2 = (DataGridViewCheckBoxCell)dgService.Rows[e.RowIndex].Cells[3];
            bool isChecked2 = Convert.ToBoolean(cell2.EditingCellFormattedValue);

            if (e.ColumnIndex == 2 && isChecked1)
            {
                cell2.EditingCellFormattedValue = false;
                cell2.Value = false;
            }

            if (e.ColumnIndex == 3 && isChecked2)
            {
                cell1.EditingCellFormattedValue = false;
                cell1.Value = false;
            }

            isChecked1 = Convert.ToBoolean(cell1.EditingCellFormattedValue);
            isChecked2 = Convert.ToBoolean(cell2.EditingCellFormattedValue);

            Result result = ServicesBus.UpdateCauHinhDichVuXetNghiem(serviceGUID, isChecked1, isChecked2);
            if (!result.IsOK)
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.UpdateCauHinhDichVuXetNghiem"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.UpdateCauHinhDichVuXetNghiem"));
            }
        }

        private void txtTenDichVu_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtTenDichVu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index < dgService.RowCount - 1)
                    {
                        index++;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayServicesListProc(object state)
        {
            try
            {
                OnDisplayServicesList();
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayServicesList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        
        #endregion

        
    }
}
