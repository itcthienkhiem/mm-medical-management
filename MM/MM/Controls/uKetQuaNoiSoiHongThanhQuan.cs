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
    public partial class uKetQuaNoiSoiHongThanhQuan : UserControl
    {
        #region Members
        #endregion

        #region Constructor
        public uKetQuaNoiSoiHongThanhQuan()
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
            dgKetQuaNoiSoi.Rows.Add("AMYDALE", "");
            dgKetQuaNoiSoi.Rows.Add("XOANG LÊ", "");
            dgKetQuaNoiSoi.Rows.Add("MIỆNG THỰC QUẢN", "");
            dgKetQuaNoiSoi.Rows.Add("SỤN PHỂU", "");
            dgKetQuaNoiSoi.Rows.Add("DÂY THANH", "");
            dgKetQuaNoiSoi.Rows.Add("BĂNG THANH THẤT", "");
        }
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
