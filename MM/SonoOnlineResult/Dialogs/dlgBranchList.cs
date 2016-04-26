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

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgBranchList : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgBranchList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplay()
        {
            Result result = MySQL.GetBranchList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgBranch.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
                MessageBox.Show(result.GetErrorAsString("MySQL.GetBranchList"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnAdd()
        {
            dlgAddBranch dlg = new dlgAddBranch();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataTable dt = dgBranch.DataSource as DataTable;
                DataRow row = dt.NewRow();
                row["BranchKey"] = dlg.BranchKey;
                row["BranchName"] = dlg.BranchName;
                row["Address"] = dlg.Address;
                row["Telephone"] = dlg.Telephone;
                row["Fax"] = dlg.Fax;
                row["Website"] = dlg.Website;
                row["Note"] = dlg.Notes;
                dt.Rows.Add(row);
                dgBranch.Rows[dgBranch.RowCount - 1].Selected = true;
            }
        }

        private void OnEdit()
        {
            if (dgBranch.SelectedRows == null || dgBranch.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please select one branch.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow dataRow = (dgBranch.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddBranch dlg = new dlgAddBranch(dataRow);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                dataRow["BranchName"] = dlg.BranchName;
                dataRow["Address"] = dlg.Address;
                dataRow["Telephone"] = dlg.Telephone;
                dataRow["Fax"] = dlg.Fax;
                dataRow["Website"] = dlg.Website;
                dataRow["Note"] = dlg.Notes;
            }
        }

        private void OnDelete()
        {
            List<DataRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count > 0)
            {
                if (MessageBox.Show("Do you want to delete selected branches ?", 
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dt = dgBranch.DataSource as DataTable;
                    foreach (DataRow row in checkedRows)
                    {
                        int branchKey = Convert.ToInt32(row["BranchKey"]);
                        Result result = MySQL.DeleteBranch(branchKey);
                        if (result.IsOK)
                            dt.Rows.Remove(row);
                        else
                        {
                            MessageBox.Show(result.GetErrorAsString("MySQL.DeleteBranch"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            else
                MessageBox.Show("Please check at least one branch.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgBranch.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Check"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            return checkedRows;
        }
        #endregion

        #region Window Event Handlers
        private void dlgBranchList_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
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

        private void dgBranch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnEdit();
        }

        private void chkCheck_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgBranch.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Check"] = chkCheck.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplay();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        

        

        
    }
}
