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
    public partial class dlgBase : Form
    {
        #region Events

        #endregion

        #region Members
        private dlgWaiting _dlgWaiting = null;
        #endregion

        #region Constructor
        public dlgBase()
        {
            InitializeComponent();
        }
        #endregion

        #region Raise Events

        #endregion

        #region Methods
        protected void ShowWaiting()
        {
            if (_dlgWaiting == null) _dlgWaiting = new dlgWaiting();
            _dlgWaiting.ShowDialog();
        }

        protected void HideWaiting()
        {
            MethodInvoker method = delegate
            {
                if (_dlgWaiting != null)
                {
                    _dlgWaiting.Hide();
                    _dlgWaiting.Close();
                    _dlgWaiting = null;
                }
            };

            if (InvokeRequired) BeginInvoke(method);
            else method.Invoke();
        }
        #endregion
    }
}
