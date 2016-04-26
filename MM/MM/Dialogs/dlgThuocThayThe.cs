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
using MM.Databasae;
using MM.Bussiness;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgThuocThayThe : dlgBase
    {
        #region Members
        private string _thuocGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgThuocThayThe(string thuocGUID)
        {
            InitializeComponent();
            _thuocGUID = thuocGUID;
        }
        #endregion

        #region Properties
        public string ThuocThayThe
        {
            get
            {
                DataRow row = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
                return row["ThuocGUID"].ToString();
            }
        }
        #endregion

        #region UI Command
        private void DisplayThuocList()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = NhomThuocBus.GetThuocThayTheList(_thuocGUID);
            if (result.IsOK)
                dgThuoc.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhomThuocBus.GetThuocThayTheList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhomThuocBus.GetThuocThayTheList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgThuocThayThe_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn 1 thuốc thay thế.", IconType.Information);
                    e.Cancel = true;
                }
            }
        }

        private void dlgThuocThayThe_Load(object sender, EventArgs e)
        {
            DisplayThuocList();
        }

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
