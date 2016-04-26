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
using DevComponents.DotNetBar.Controls;

namespace MM.Dialogs
{
    public partial class dlgConfirmThuTien : Form
    {
        #region Members
        private List<DataRow> _serviceList = null;
        #endregion

        #region Constructor
        public dlgConfirmThuTien(List<DataRow> serviceList)
        {
            InitializeComponent();
            dtpkNgayXuat.Value = DateTime.Now;
            cboHinhThucThanhToan.SelectedIndex = 0;
            _serviceList = serviceList;
        }
        #endregion

        #region Properties
        public bool DaThuTien
        {
            get { return raDaThuTien.Checked; }
        }

        public DateTime NgayXuat
        {
            get { return dtpkNgayXuat.Value; }
        }

        public string GhiChu
        {
            get { return txtGhiChu.Text; }
        }

        public string LyDoGiam
        {
            get { return txtLyDoGiam.Text; }
        }

        public int HinhThucThanhToan
        {
            get { return cboHinhThucThanhToan.SelectedIndex; }
        }

        public DataGridView DataGridViewDetail
        {
            get { return dgReceiptDetail; }
        }

        public bool TrongGoiKham
        {
            get { return chkTrongGoiKham.Checked; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            if (_serviceList == null || _serviceList.Count <= 0) return;
            foreach (DataRow row in _serviceList)
            {
                string maDichVu = row["Code"].ToString();
                string tenDichVu = row["Name"].ToString();
                int soLuong = Convert.ToInt32(row["SoLuong"]);
                double gia = Convert.ToDouble(row["FixedPrice"]);
                double giam = Convert.ToDouble(row["Discount"]);
                double thanhTien = (gia - ((gia * giam) / 100)) * soLuong;
                string serviceHistoryGUID = row["ServiceHistoryGUID"].ToString();

                object[] objs = new object[6];
                objs[0] = maDichVu;
                objs[1] = tenDichVu;
                objs[2] = soLuong;
                objs[3] = gia;
                objs[4] = giam;
                objs[5] = thanhTien;

                int rowIndex = dgReceiptDetail.Rows.Add(objs);
                dgReceiptDetail.Rows[rowIndex].Tag = serviceHistoryGUID;
            }

            CalculateTongTien();
        }

        private void CalculateTongTien()
        {
            double totalPrice = 0;
            foreach (DataGridViewRow row in dgReceiptDetail.Rows)
            {
                double tt = Convert.ToDouble(row.Cells[5].Value);
                totalPrice += tt;
            }

            if (totalPrice == 0)
                lbTotalPrice.Text = totalPrice.ToString();
            else
                lbTotalPrice.Text = string.Format("{0}", totalPrice.ToString("#,###"));
        }
        #endregion

        #region Window Event Handlers
        private void dlgConfirmThuTien_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            //    e.Cancel = true;
        }

        private void dlgConfirmThuTien_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dgReceiptDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType() == typeof(DataGridViewIntegerInputEditingControl))
            {
                DataGridViewIntegerInputEditingControl ctrl = e.Control as DataGridViewIntegerInputEditingControl;
                ctrl.ValueChanged += new EventHandler(ctrl_ValueChanged);
            }
        }

        private void ctrl_ValueChanged(object sender, EventArgs e)
        {
            if (dgReceiptDetail.SelectedRows == null || dgReceiptDetail.SelectedRows.Count <= 0) return;
            int rowIndex = dgReceiptDetail.SelectedRows[0].Cells[0].RowIndex;
            DataGridViewIntegerInputEditingControl ctrl = sender as DataGridViewIntegerInputEditingControl;
            int soLuong = ctrl.Value;
            double gia = Convert.ToDouble(dgReceiptDetail[3, rowIndex].Value);
            double giam = Convert.ToDouble(dgReceiptDetail[4, rowIndex].Value);
            double thanhTien = (gia - ((gia * giam) / 100)) * soLuong;
            dgReceiptDetail[5, rowIndex].Value = thanhTien;

            CalculateTongTien();
        }
        #endregion
    }
}
