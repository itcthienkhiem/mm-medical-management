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
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
