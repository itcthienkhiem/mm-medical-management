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
    public partial class dlgPrintType : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgPrintType()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool Lien1
        {
            get { return chkLien1.Checked; }
        }

        public bool Lien2
        {
            get { return chkLien2.Checked; }
        }

        public bool Lien3
        {
            get { return chkLien3.Checked; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgPrintType_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!chkLien1.Checked && !chkLien2.Checked && !chkLien3.Checked)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 liên để in.", Common.IconType.Information);
                    e.Cancel = true;
                }
            }
        }
        #endregion
    }
}
