using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddServices : Form
    {
        #region Members
        private bool _isNew = true;
        #endregion

        #region Constructor
        public dlgAddServices(bool isNew)
        {
            InitializeComponent();
            _isNew = isNew;
            this.Text = isNew ? "Thêm dịch vụ" : "Sửa dịch vụ";
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgAddServices_Load(object sender, EventArgs e)
        {

        }

        private void dlgAddServices_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion
    }
}
