using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uPhieuThuThuocList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPhieuThuThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {

        }

        public void DisplayAsThread()
        {

        }

        private void OnAddPhieuThu()
        {

        }

        private void OnEditPhieuThu()
        {

        }

        private void OnDeletePhieuThu()
        {

        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddPhieuThu();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditPhieuThu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeletePhieuThu();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void dgPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            OnEditPhieuThu();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
