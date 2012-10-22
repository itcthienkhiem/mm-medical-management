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
    public partial class dlgCauHinhKhoCapCuu : dlgBase
    {
        #region Constructor
        public dlgCauHinhKhoCapCuu()
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
            numSoNgay.Value = Global.AlertSoNgayHetHanCapCuu;
            numSoLuongHetTonKho.Value = Global.AlertSoLuongHetTonKhoCapCuu;
        }

        private void dlgCauHinhSoNgayBaoTiemNgua_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Global.AlertSoNgayHetHanCapCuu = (int)numSoNgay.Value;
                Global.AlertSoLuongHetTonKhoCapCuu = (int)numSoLuongHetTonKho.Value;

                Configuration.SetValues(Const.AlertSoNgayHetHanCapCuuKey, Global.AlertSoNgayHetHanCapCuu);
                Configuration.SetValues(Const.AlertSoLuongHetTonKhoCapCuuKey, Global.AlertSoLuongHetTonKhoCapCuu);
                Configuration.SaveData(Global.AppConfig);
            }
        }
        #endregion
    }
}
