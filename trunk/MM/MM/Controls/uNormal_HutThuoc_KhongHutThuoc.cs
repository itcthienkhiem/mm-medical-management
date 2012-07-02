using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Controls
{
    public partial class uNormal_HutThuoc_KhongHutThuoc : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_HutThuoc_KhongHutThuoc()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool HutThuocChecked
        {
            get { return chkHutThuoc.Checked; }
            set { chkHutThuoc.Checked = value; }
        }

        public bool KhongHutThuocChecked
        {
            get { return chkKhongHutThuoc.Checked; }
            set { chkKhongHutThuoc.Checked = value; }
        }

        public uNormal_Chung Normal_HutThuoc
        {
            get { return uNormal_HutThuoc; }
        }

        public uNormal_Chung Normal_KhongHutThuoc
        {
            get { return uNormal_KhongHutThuoc; }
        }
        #endregion

        #region UI Command
        public bool CheckInfo()
        {
            if (!chkHutThuoc.Checked && !chkKhongHutThuoc.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho hút thuốc hoặc không ít thuốc.", Common.IconType.Information);
                chkHutThuoc.Focus();
                return false;
            }

            if (chkHutThuoc.Checked && !Normal_HutThuoc.CheckInfo())
            {
                uNormal_HutThuoc.Focus();
                return false;
            }

            if (chkKhongHutThuoc.Checked && !Normal_KhongHutThuoc.CheckInfo())
            {
                uNormal_KhongHutThuoc.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkHutThuoc_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_HutThuoc.Enabled = chkHutThuoc.Checked;
        }

        private void chkKhongHutThuoc_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_KhongHutThuoc.Enabled = chkKhongHutThuoc.Checked;
        }
        #endregion
    }
}
