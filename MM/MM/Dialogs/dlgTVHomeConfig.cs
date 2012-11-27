using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgTVHomeConfig : dlgBase
    {
        #region Members
        
        #endregion

        #region Constructor
        public dlgTVHomeConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtPath.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn đường dẫn TVHome.", IconType.Information);
                btnBrowser.Focus();
                return false;
            }

            if (!File.Exists(txtPath.Text))
            {
                MsgBox.Show(this.Text, "Đường dẫn TVHome không tồn tại. Vui lòng kiểm tra lại.", IconType.Information);
                btnBrowser.Focus();
                return false;
            }

            return true;
        }

        public void SetAppConfig()
        {
            Global.TVHomeConfig.Path = txtPath.Text;
            Global.TVHomeConfig.SuDungSoiCTC = raSoiCTC_Co.Checked;
            Global.TVHomeConfig.SuDungSieuAm = raSieuAm_Co.Checked;
        }
        #endregion

        #region Window Event Handlers
        private void dlgTVHomeConfig_Load(object sender, EventArgs e)
        {
            txtPath.Text = Global.TVHomeConfig.Path;
            raSoiCTC_Co.Checked = Global.TVHomeConfig.SuDungSoiCTC;
            raSoiCTC_Khong.Checked = !Global.TVHomeConfig.SuDungSoiCTC;
            raSieuAm_Co.Checked = Global.TVHomeConfig.SuDungSieuAm;
            raSieuAm_Khong.Checked = !Global.TVHomeConfig.SuDungSieuAm;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.exe)|*.exe";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.FileName;
            }
        }

        private void dlgTVHomeConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
