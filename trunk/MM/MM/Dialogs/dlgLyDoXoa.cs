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
    public partial class dlgLyDoXoa : dlgBase
    {
        #region Members
        private int _type = 0; //0: phiếu thu; 1: hóa đơn
        #endregion

        #region Constructor
        public dlgLyDoXoa(string code, int type)
        {
            InitializeComponent();
            this.Text = code;
            _type = type;
        }
        #endregion

        #region Properties
        public string Notes
        {
            get { return txtLyDo.Text; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgLyDoXoa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (txtLyDo.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập lý do xóa.", Common.IconType.Information);
                    txtLyDo.Focus();
                    e.Cancel = true;
                }
            }
            else
            {
                string msg = string.Empty;
                if (_type == 0)
                    msg = string.Format("Bạn không nhập lý do xóa nên phiếu thu: '{0}' không được xóa.", this.Text);
                else
                    msg = string.Format("Bạn không nhập lý do xóa nên hóa đơn: '{0}' không được xóa.", this.Text);
                        
                MsgBox.Show(this.Text, msg, Common.IconType.Information);
            }
        }
        #endregion
        
    }
}
