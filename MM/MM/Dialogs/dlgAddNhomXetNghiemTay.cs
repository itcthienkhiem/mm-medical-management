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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddNhomXetNghiemTay : dlgBase
    {
        #region Constructor
        public dlgAddNhomXetNghiemTay()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<string> NhomXetNghiemList
        {
            get
            {
                List<string> nhomXNList = new List<string>();
                foreach (ListViewItem item in lvNhomXN.Items)
                {
                    if (item.Checked)
                        nhomXNList.Add(item.Text);
                }

                return nhomXNList;
            }
        }

        public DateTime NgayXetNghiem
        {
            get { return dtpkNgayXetNghiem.Value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = XetNghiemTayBus.GetNhomXetNghiemList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    lvNhomXN.Items.Add(row[0].ToString());
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiemTayBus.GetNhomXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetNhomXetNghiemList"));
            }

            dtpkNgayXetNghiem.Value = DateTime.Now;
        }

        private bool CheckInfo()
        {
            bool result = false;
            foreach (ListViewItem item in lvNhomXN.Items)
            {
                if (item.Checked)
                {
                    result = true;
                    break;
                }
            }

            if (!result)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 nhóm xét nghiệm.", IconType.Information);
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddChiTietKetQuaXetNghiemTay_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void dlgAddChiTietKetQuaXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
