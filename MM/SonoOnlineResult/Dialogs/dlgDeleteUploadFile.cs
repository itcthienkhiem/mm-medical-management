using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgDeleteUploadFile : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgDeleteUploadFile()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now.AddMonths(-1);
            dtpkFrom.Value = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
            dtpkTo.Value = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59);
        }
        #endregion

        #region Properties
        public DateTime From
        {
            get { return dtpkFrom.Value; }
        }

        public DateTime To
        {
            get { return dtpkTo.Value; }
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (dtpkFrom.Value > dtpkTo.Value)
            {
                MessageBox.Show("Please input From Date is less than or equal To Date.", 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpkFrom.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgDeleteUploadFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
