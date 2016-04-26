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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uTraCuuThongTinKhachHang : uBase
    {
        #region Constructor
        public uTraCuuThongTinKhachHang()
        {
            InitializeComponent();
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = dgBenhNhan.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgBenhNhan.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            Cursor.Current = Cursors.WaitCursor;
            lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            string tenBenhNhan = txtTenBenhNhan.Text;
            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;

            Result result = UserBus.GetAccountList(tuNgay, denNgay, tenBenhNhan, chkMaBenhNhan.Checked);
            if (result.IsOK)
            {
                ClearData();
                DataTable dt = result.QueryResult as DataTable;
                dgBenhNhan.DataSource = result.QueryResult as DataTable;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.GetAccountList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.GetAccountList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }
        #endregion
    }
}
