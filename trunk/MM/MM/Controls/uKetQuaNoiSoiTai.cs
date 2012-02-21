using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiTai : uBase
    {
        #region Members
        #endregion

        #region Constructor
        public uKetQuaNoiSoiTai()
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
            dgKetQuaNoiSoi.Rows.Add("", "ỐNG TAI", "");
            dgKetQuaNoiSoi.Rows.Add("", "MÀNG NHĨ", "");
            dgKetQuaNoiSoi.Rows.Add("", "CÁN BÚA", "");
            dgKetQuaNoiSoi.Rows.Add("", "HÒM NHĨ", "");
            dgKetQuaNoiSoi.Rows.Add("", "VALSAVA", "");
        }
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
