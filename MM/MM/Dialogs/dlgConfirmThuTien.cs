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
    public partial class dlgConfirmThuTien : Form
    {
        #region Constructor
        public dlgConfirmThuTien()
        {
            InitializeComponent();
            dtpkNgayXuat.Value = DateTime.Now;
        }
        #endregion

        #region Properties
        public bool DaThuTien
        {
            get { return raDaThuTien.Checked; }
        }

        public DateTime NgayXuat
        {
            get { return dtpkNgayXuat.Value; }
        }

        public string GhiChu
        {
            get { return txtGhiChu.Text; }
        }
        #endregion

        #region Window Event Handlers
        private void dlgConfirmThuTien_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                e.Cancel = true;
        }
        #endregion
    }
}
