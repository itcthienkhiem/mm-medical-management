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
    public partial class dlgLamMoiSoHoaDonXetNghiem : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgLamMoiSoHoaDonXetNghiem()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgLamMoiSoHoaDon_Load(object sender, EventArgs e)
        {
            Result result = QuanLySoHoaDonXetNghiemBus.GetThayDoiSoHoaSonSauCung();
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    NgayBatDauLamMoiSoHoaDonXetNghiemYKhoa thayDoiSauCung = result.QueryResult as NgayBatDauLamMoiSoHoaDonXetNghiemYKhoa;
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonXetNghiemBus.GetThayDoiSoHoaSonSauCung"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonXetNghiemBus.GetThayDoiSoHoaSonSauCung"));
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
                            Result result = QuanLySoHoaDonXetNghiemBus.SetThayDoiSoHoaSon(dtpkNgayThayDoiMoi.Value, txtMauSoMoi.Text, 
                                txtKiHieuMoi.Text, (int)numSoHDBatDauMoi.Value);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonXetNghiemBus.SetThayDoiSoHoaSon"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonXetNghiemBus.SetThayDoiSoHoaSon"));
                                e.Cancel = true;
                            }
                            else
                            {
                                Global.MauSoXetNghiemSauCung = txtMauSoMoi.Text;
                                Global.KiHieuXetNghiemSauCung = txtKiHieuMoi.Text;
                                Global.NgayThayDoiSoHoaDonXetNghiemSauCung = dtpkNgayThayDoiMoi.Value;
                                Global.SoHoaDonXetNghiemBatDau = (int)numSoHDBatDauMoi.Value;
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
