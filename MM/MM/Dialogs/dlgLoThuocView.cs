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
    public partial class dlgLoThuocView : Form
    {
        #region Members
        private string _thuocGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgLoThuocView(string thuocGUID)
        {
            InitializeComponent();
            _thuocGUID = thuocGUID;
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = LoThuocBus.GetLoThuocByThuoc(_thuocGUID);
            if (result.IsOK)
            {
                dgThuocTonKho.DataSource = result.QueryResult;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoThuocBus.GetLoThuocByThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetLoThuocByThuoc"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgLoThuocView_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }
        #endregion
    }
}
