using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiMui : UserControl
    {
        #region Members

        #endregion

        #region Constructor
        public uKetQuaNoiSoiMui()
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
            dgKetQuaNoiSoi.Rows.Add("", "NIÊM MẠC", "");
            dgKetQuaNoiSoi.Rows.Add("", "VÁCH NGĂN", "");
            dgKetQuaNoiSoi.Rows.Add("", "KHE TRÊN", "");
            dgKetQuaNoiSoi.Rows.Add("", "KHE GIỮA", "");
            dgKetQuaNoiSoi.Rows.Add("", "CUỐN GIỮA", "");
            dgKetQuaNoiSoi.Rows.Add("", "CUỐN DƯỚI", "");
            dgKetQuaNoiSoi.Rows.Add("", "MÕM MÓC", "");
            dgKetQuaNoiSoi.Rows.Add("", "BÓNG SÀNG", "");
            dgKetQuaNoiSoi.Rows.Add("", "VÒM", "");
        }
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
