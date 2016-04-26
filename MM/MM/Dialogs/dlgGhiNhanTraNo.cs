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
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgGhiNhanTraNo : Form
    {
        #region Members
        private LoaiPT _loaiPT = LoaiPT.DichVu;
        private string _phieuThuGUID = string.Empty;
        private bool _daThuTien = true;
        #endregion

        #region Constructor
        public dlgGhiNhanTraNo(LoaiPT loaiPT, string phieuThuGUID, bool daThuTien)
        {
            InitializeComponent();
            _loaiPT = loaiPT;
            _phieuThuGUID = phieuThuGUID;
            _daThuTien = daThuTien;
            _uGhiNhanTraNoList.LoaiPT = _loaiPT;
            _uGhiNhanTraNoList.PhieuThuGUID = _phieuThuGUID;
            _uGhiNhanTraNoList.DaThuTien = _daThuTien;
            _uGhiNhanTraNoList.OnCloseEvent += new MM.Controls.CloseClickEventHandler(_uGhiNhanTraNoList_OnCloseEvent);
        }
        #endregion

        #region Properties
        public bool IsDataChange
        {
            get { return _uGhiNhanTraNoList.IsDataChange; }
            set { _uGhiNhanTraNoList.IsDataChange = value; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgGhiNhanTraNo_Load(object sender, EventArgs e)
        {
            _uGhiNhanTraNoList.DisplayAsThread();
        }

        private void _uGhiNhanTraNoList_OnCloseEvent()
        {
            this.Close();
        }
        #endregion

        
    }
}
