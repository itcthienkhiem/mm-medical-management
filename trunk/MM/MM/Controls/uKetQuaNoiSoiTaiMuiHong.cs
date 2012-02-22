using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiTaiMuiHong : UserControl
    {
       #region Members
        #endregion

        #region Constructor
        public uKetQuaNoiSoiTaiMuiHong()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public string OngTaiNgoai
        {
            get { return GetValue(0, 1); }
            set { SetValue(0, 1, value); }
        }

        public string MangNhi
        {
            get { return GetValue(1, 1); }
            set { SetValue(1, 1, value); }
        }

        public string NiemMacMui
        {
            get { return GetValue(2, 1); }
            set { SetValue(2, 1, value); }
        }

        public string KheTren
        {
            get { return GetValue(4, 1); }
            set { SetValue(4, 1, value); }
        }

        public string KheGiua
        {
            get { return GetValue(5, 1); }
            set { SetValue(5, 1, value); }
        }

        public string MomMocBongSang
        {
            get { return GetValue(6, 1); }
            set { SetValue(6, 1, value); }
        }

        public string Vom
        {
            get { return GetValue(7, 1); }
            set { SetValue(7, 1, value); }
        }

        public string Amydale
        {
            get { return GetValue(8, 1); }
            set { SetValue(8, 1, value); }
        }

        public string ThanhQuan
        {
            get { return GetValue(9, 1); }
            set { SetValue(9, 1, value); }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dgKetQuaNoiSoi.Rows.Add("ỐNG TAI NGOÀI", "");
            dgKetQuaNoiSoi.Rows.Add("MÀNG NHĨ", "");
            dgKetQuaNoiSoi.Rows.Add("NIÊM MẠC MŨI", "");
            dgKetQuaNoiSoi.Rows.Add("VÁCH NGĂN", "");
            dgKetQuaNoiSoi.Rows.Add("KHE TRÊN", "");
            dgKetQuaNoiSoi.Rows.Add("KHE GIỮA", "");
            dgKetQuaNoiSoi.Rows.Add("MÕM MÓC-BÓNG SÀNG", "");
            dgKetQuaNoiSoi.Rows.Add("VÒM", "");
            dgKetQuaNoiSoi.Rows.Add("AMYDALE", "");
            dgKetQuaNoiSoi.Rows.Add("THANH QUẢN", "");
        }

        private string GetValue(int rowIndex, int colIndex)
        {
            if (dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value != null && dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value != DBNull.Value)
                return dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value.ToString();
            else
                return string.Empty;
        }

        private void SetValue(int rowIndex, int colIndex, string value)
        {
            dgKetQuaNoiSoi.Rows[rowIndex].Cells[colIndex].Value = value;
        }
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
