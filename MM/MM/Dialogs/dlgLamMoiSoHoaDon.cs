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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgLamMoiSoHoaDon : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgLamMoiSoHoaDon()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgLamMoiSoHoaDon_Load(object sender, EventArgs e)
        {
            Result result = QuanLySoHoaDonBus.GetThayDoiSoHoaSonSauCung();
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    NgayBatDauLamMoiSoHoaDon thayDoiSauCung = result.QueryResult as NgayBatDauLamMoiSoHoaDon;
                    dtpkNgayThayDoiSauCung.Value = thayDoiSauCung.NgayBatDau;
                    txtMauSoCu.Text = thayDoiSauCung.MauSo;
                    txtKiHieuCu.Text = thayDoiSauCung.KiHieu;
                    txtSoHDBatDauCu.Text = thayDoiSauCung.SoHoaDonBatDau.ToString();
                }
                else
                {
                    dtpkNgayThayDoiSauCung.Value = Global.MinDateTime;
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.GetThayDoiSoHoaSonSauCung"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetThayDoiSoHoaSonSauCung"));
            }

            dtpkNgayThayDoiMoi.Value = DateTime.Now;
        }

        private bool CheckInfo()
        {
            if (txtMauSoMoi.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mẫu số hóa đơn.", IconType.Information);
                txtMauSoMoi.Focus();
                return false;
            }

            if (txtKiHieuMoi.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập kí hiệu hóa đơn.", IconType.Information);
                txtKiHieuMoi.Focus();
                return false;
            }

            return true;
        }

        private void dlgLamMoiSoHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (MsgBox.Question(this.Text, "Bạn có thật sự muốn thay đổi số hóa đơn ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (dtpkNgayThayDoiSauCung.Value.ToString("dd/MM/yyyy") != dtpkNgayThayDoiMoi.Value.ToString("dd/MM/yyyy"))
                    {
                        if (CheckInfo())
                        {
                            Result result = QuanLySoHoaDonBus.SetThayDoiSoHoaSon(dtpkNgayThayDoiMoi.Value, txtMauSoMoi.Text, 
                                txtKiHieuMoi.Text, (int)numSoHDBatDauMoi.Value);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.SetThayDoiSoHoaSon"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.SetThayDoiSoHoaSon"));
                                e.Cancel = true;
                            }
                            else
                            {
                                Global.MauSoSauCung = txtMauSoMoi.Text;
                                Global.KiHieuSauCung = txtKiHieuMoi.Text;
                                Global.NgayThayDoiSoHoaDonSauCung = dtpkNgayThayDoiMoi.Value;
                                Global.SoHoaDonBatDau = (int)numSoHDBatDauMoi.Value;
                            }
                        }
                        else
                            e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
        }
        #endregion
    }
}
