using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgCauHinhSoNgayBaoTiemNgua : dlgBase
    {
        #region Constructor
        public dlgCauHinhSoNgayBaoTiemNgua()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgCauHinhSoNgayBaoTiemNgua_Load(object sender, EventArgs e)
        {
            numSoNgay.Value = Global.AlertDays;
        }

        private void dlgCauHinhSoNgayBaoTiemNgua_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Global.AlertDays = (int)numSoNgay.Value;
                Configuration.SetValues(Const.AlertDayKey, Global.AlertDays);
                Configuration.SaveData(Global.AppConfig);
            }
        }
        #endregion
    }
}
