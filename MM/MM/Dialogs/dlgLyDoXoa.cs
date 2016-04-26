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

namespace MM.Dialogs
{
    public partial class dlgLyDoXoa : dlgBase
    {
        #region Members
        private int _type = 0; //0: phiếu thu; 1: hóa đơn; 2: dịch vụ; 3: lô thuốc
        private bool _isDeleted = true;
        #endregion

        #region Constructor
        public dlgLyDoXoa(string code, int type)
        {
            InitializeComponent();
            this.Text = code;
            _type = type;
        }

        public dlgLyDoXoa(string code, int type, bool isDeleted)
        {
            InitializeComponent();
            this.Text = code;
            _type = type;
            _isDeleted = isDeleted;

            if (!_isDeleted) lbTitle.Text = "Lý do sửa:";
        }
        #endregion

        #region Properties
        public string Notes
        {
            get { return txtLyDo.Text; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgLyDoXoa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (txtLyDo.Text.Trim() == string.Empty)
                {
                    if (_isDeleted)
                        MsgBox.Show(this.Text, "Vui lòng nhập lý do xóa.", Common.IconType.Information);
                    else
                        MsgBox.Show(this.Text, "Vui lòng nhập lý do sửa.", Common.IconType.Information);

                    txtLyDo.Focus();
                    e.Cancel = true;
                }
            }
            else
            {
                string msg = string.Empty;
                if (_isDeleted)
                {
                    if (_type == 0)
                        msg = string.Format("Bạn không nhập lý do xóa nên phiếu thu: '{0}' không được xóa.", this.Text);
                    else if (_type == 1)
                        msg = string.Format("Bạn không nhập lý do xóa nên hóa đơn: '{0}' không được xóa.", this.Text);
                    else if (_type == 2)
                        msg = string.Format("Bạn không nhập lý do xóa nên dịch vụ: '{0}' không được xóa.", this.Text);
                    else if (_type == 3)
                        msg = string.Format("Bạn không nhập lý do xóa nên lô thuốc: '{0}' không được xóa.", this.Text);
                }
                else
                {
                    if (_type == 3)
                        msg = string.Format("Bạn không nhập lý do sửa nên lô thuốc: '{0}' không được cập nhật.", this.Text);
                }
                        
                MsgBox.Show(this.Text, msg, Common.IconType.Information);
            }
        }
        #endregion
        
    }
}
