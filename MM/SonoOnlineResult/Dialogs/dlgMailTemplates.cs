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
    public partial class dlgMailTemplates : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgMailTemplates()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void OnAdd()
        {

        }

        private void OnEdit()
        {

        }

        private void OnDelete()
        {

        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void chkCheck_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
