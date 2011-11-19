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
    public partial class dlgPatientHistory : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgPatientHistory(DataRow drPatient)
        {
            InitializeComponent();
            DisplayInfo(drPatient);
        }
        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drPatient)
        {
            if (drPatient["Di_Ung_Thuoc"] != null && drPatient["Di_Ung_Thuoc"] != DBNull.Value)
                chkDiUngThuoc.Checked = Convert.ToBoolean(drPatient["Di_Ung_Thuoc"]);

            if (drPatient["Thuoc_Di_Ung"] != null && drPatient["Thuoc_Di_Ung"] != DBNull.Value)
                txtThuocDiUng.Text = drPatient["Thuoc_Di_Ung"].ToString();

            if (drPatient["Dot_Quy"] != null && drPatient["Dot_Quy"] != DBNull.Value)
                chkDotQuy.Checked = Convert.ToBoolean(drPatient["Dot_Quy"]);

            if (drPatient["Benh_Tim_Mach"] != null && drPatient["Benh_Tim_Mach"] != DBNull.Value)
                chkBenhTimMach.Checked = Convert.ToBoolean(drPatient["Benh_Tim_Mach"]);

            if (drPatient["Benh_Lao"] != null && drPatient["Benh_Lao"] != DBNull.Value)
                chkBenhLao.Checked = Convert.ToBoolean(drPatient["Benh_Lao"]);

            if (drPatient["Dai_Thao_Duong"] != null && drPatient["Dai_Thao_Duong"] != DBNull.Value)
                chkDaiThaoDuong.Checked = Convert.ToBoolean(drPatient["Dai_Thao_Duong"]);

            if (drPatient["Dai_Duong_Dang_Dieu_Tri"] != null && drPatient["Dai_Duong_Dang_Dieu_Tri"] != DBNull.Value)
                chkDaiDuongDangDieuTri.Checked = Convert.ToBoolean(drPatient["Dai_Duong_Dang_Dieu_Tri"]);

            if (drPatient["Viem_Gan_B"] != null && drPatient["Viem_Gan_B"] != DBNull.Value)
                chkViemGanB.Checked = Convert.ToBoolean(drPatient["Viem_Gan_B"]);

            if (drPatient["Viem_Gan_C"] != null && drPatient["Viem_Gan_C"] != DBNull.Value)
                chkViemGanC.Checked = Convert.ToBoolean(drPatient["Viem_Gan_C"]);

            if (drPatient["Viem_Gan_Dang_Dieu_Tri"] != null && drPatient["Viem_Gan_Dang_Dieu_Tri"] != DBNull.Value)
                chkViemGanDangDieuTri.Checked = Convert.ToBoolean(drPatient["Viem_Gan_Dang_Dieu_Tri"]);

            if (drPatient["Ung_Thu"] != null && drPatient["Ung_Thu"] != DBNull.Value)
                chkUngThu.Checked = Convert.ToBoolean(drPatient["Ung_Thu"]);

            if (drPatient["Co_Quan_Ung_Thu"] != null && drPatient["Co_Quan_Ung_Thu"] != DBNull.Value)
                txtCoQuanUngThu.Text = drPatient["Co_Quan_Ung_Thu"].ToString();

            if (drPatient["Dong_Kinh"] != null && drPatient["Dong_Kinh"] != DBNull.Value)
                chkDongKinh.Checked = Convert.ToBoolean(drPatient["Dong_Kinh"]);

            if (drPatient["Hen_Suyen"] != null && drPatient["Hen_Suyen"] != DBNull.Value)
                chkHenSuyen.Checked = Convert.ToBoolean(drPatient["Hen_Suyen"]);

            if (drPatient["Benh_Khac"] != null && drPatient["Benh_Khac"] != DBNull.Value)
                chkBenhKhac.Checked = Convert.ToBoolean(drPatient["Benh_Khac"]);

            if (drPatient["Benh_Gi"] != null && drPatient["Benh_Gi"] != DBNull.Value)
                txtBenhGi.Text = drPatient["Benh_Gi"].ToString();

            if (drPatient["Thuoc_Dang_Dung"] != null && drPatient["Thuoc_Dang_Dung"] != DBNull.Value)
                txtThuocDangDung.Text = drPatient["Thuoc_Dang_Dung"].ToString();

            if (drPatient["Hut_Thuoc"] != null && drPatient["Hut_Thuoc"] != DBNull.Value)
                chkHutThuoc.Checked = Convert.ToBoolean(drPatient["Hut_Thuoc"]);

            if (drPatient["Uong_Ruou"] != null && drPatient["Uong_Ruou"] != DBNull.Value)
                chkUongRuou.Checked = Convert.ToBoolean(drPatient["Uong_Ruou"]);

            if (drPatient["Tinh_Trang_Gia_Dinh"] != null && drPatient["Tinh_Trang_Gia_Dinh"] != DBNull.Value)
                txtTinhTrangGiaDinh.Text = drPatient["Tinh_Trang_Gia_Dinh"].ToString();

            if (drPatient["Chich_Ngua_Viem_Gan_B"] != null && drPatient["Chich_Ngua_Viem_Gan_B"] != DBNull.Value)
                chkChichNguaViemGanB.Checked = Convert.ToBoolean(drPatient["Chich_Ngua_Viem_Gan_B"]);

            if (drPatient["Chich_Ngua_Uon_Van"] != null && drPatient["Chich_Ngua_Uon_Van"] != DBNull.Value)
                chkChichNguaUonVan.Checked = Convert.ToBoolean(drPatient["Chich_Ngua_Uon_Van"]);

            if (drPatient["Chich_Ngua_Cum"] != null && drPatient["Chich_Ngua_Cum"] != DBNull.Value)
                chkChichNguaCum.Checked = Convert.ToBoolean(drPatient["Chich_Ngua_Cum"]);

            if (drPatient["Dang_Co_Thai"] != null && drPatient["Dang_Co_Thai"] != DBNull.Value)
                chkDangCoThai.Checked = Convert.ToBoolean(drPatient["Dang_Co_Thai"]);
        }
        #endregion
    }
}
