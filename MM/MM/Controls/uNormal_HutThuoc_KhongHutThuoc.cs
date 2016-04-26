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
using MM.Databasae;

namespace MM.Controls
{
    public partial class uNormal_HutThuoc_KhongHutThuoc : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_HutThuoc_KhongHutThuoc()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool HutThuocChecked
        {
            get { return chkHutThuoc.Checked; }
            set { chkHutThuoc.Checked = value; }
        }

        public bool KhongHutThuocChecked
        {
            get { return chkKhongHutThuoc.Checked; }
            set { chkKhongHutThuoc.Checked = value; }
        }

        public uNormal_Chung Normal_HutThuoc
        {
            get { return uNormal_HutThuoc; }
        }

        public uNormal_Chung Normal_KhongHutThuoc
        {
            get { return uNormal_KhongHutThuoc; }
        }

        public DataTable DonViList
        {
            set
            {
                this.Normal_HutThuoc.DonViList = value;
                this.Normal_KhongHutThuoc.DonViList = value;
            }
        }
        #endregion

        #region UI Command
        public List<ChiTietXetNghiem_Manual> GetChiTietXetNghiem_ManualList()
        {
            List<ChiTietXetNghiem_Manual> ctxns = new List<ChiTietXetNghiem_Manual>();
            if (this.HutThuocChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_HutThuoc.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.HutThuoc;
                ctxns.Add(ct);
            }

            if (this.KhongHutThuocChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_KhongHutThuoc.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.KhongHutThuoc;
                ctxns.Add(ct);
            }

            return ctxns;
        }

        public void SetChiTietXetNghiem_ManualList(List<ChiTietXetNghiem_Manual> ctxns)
        {
            if (ctxns == null || ctxns.Count <= 0) return;

            foreach (var ct in ctxns)
            {
                switch ((DoiTuong)ct.DoiTuong)
                {
                    case DoiTuong.HutThuoc:
                        this.HutThuocChecked = true;
                        this.Normal_HutThuoc.SetChiTietXetNghiem_Manual(ct);
                        break;

                    case DoiTuong.KhongHutThuoc:
                        this.KhongHutThuocChecked = true;
                        this.Normal_KhongHutThuoc.SetChiTietXetNghiem_Manual(ct);
                        break;
                }
            }
        }

        public bool CheckInfo()
        {
            if (!chkHutThuoc.Checked && !chkKhongHutThuoc.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho hút thuốc hoặc không ít thuốc.", Common.IconType.Information);
                chkHutThuoc.Focus();
                return false;
            }

            if (chkHutThuoc.Checked && !Normal_HutThuoc.CheckInfo())
            {
                uNormal_HutThuoc.Focus();
                return false;
            }

            if (chkKhongHutThuoc.Checked && !Normal_KhongHutThuoc.CheckInfo())
            {
                uNormal_KhongHutThuoc.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkHutThuoc_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_HutThuoc.Enabled = chkHutThuoc.Checked;
        }

        private void chkKhongHutThuoc_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_KhongHutThuoc.Enabled = chkKhongHutThuoc.Checked;
        }
        #endregion
    }
}
