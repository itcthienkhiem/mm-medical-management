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
    public partial class dlgKetLuan : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgKetLuan(string ketLuan)
        {
            InitializeComponent();
            txtKetLuan.Text = ketLuan;
        }
        #endregion

        #region Properties
        public string KetLuan
        {
            get { return txtKetLuan.Text; }
            set { txtKetLuan.Text = value; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgKetLuan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (txtKetLuan.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập hướng giải quyết.", Common.IconType.Information);
                    txtKetLuan.Focus();
                    e.Cancel = true;
                }
            }
        }
        #endregion
    }
}
