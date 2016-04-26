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
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uMergeDocStaff : UserControl
    {
        private DataTable _dataSource = null;
        private bool _isAscending = true;

        public uMergeDocStaff()
        {
            InitializeComponent();
        }

        public DataTable DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        public void BindData()
        {
            dgMergePatient.DataSource = DataSource;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (dgMergePatient.SelectedRows == null || dgMergePatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn nhân viên cần giữ lại.", IconType.Information);
                return;
            }

            string keepPatientGUID = (dgMergePatient.SelectedRows[0].DataBoundItem as DataRowView).Row["DocStaffGUID"].ToString();
            foreach(DataGridViewRow row in dgMergePatient.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                if (dr["DocStaffGUID"].ToString() != keepPatientGUID)
                {
                    string mergePatientGUID = dr["DocStaffGUID"].ToString();
                    DocStaffBus.Merge2DocStaffs(keepPatientGUID, mergePatientGUID);
                }
            }

            MsgBox.Show("Merge nhan vien", "Merge kết thúc", IconType.Information);
            if(Form.ActiveForm!=null)
                Form.ActiveForm.Close();
        }

        private void dgMergePatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgMergePatient.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return;
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

                dgMergePatient.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }
    }
}
