using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Dialogs;

namespace MM.Controls
{
    #region Delegate Events
    public delegate void ColorClickedHandler(Color color);
    public delegate void DrawTypeClickedHandler(DrawType type, int width);
    #endregion

    public partial class uBase : UserControl
    {
        #region Events
        public event ColorClickedHandler OnColorClicked;
        public event DrawTypeClickedHandler OnDrawTypeClicked;
        #endregion

        #region Members
        private dlgWaiting _dlgWaiting = null;
        #endregion

        #region Constructor
        public uBase()
        {
            InitializeComponent();
        }
        #endregion

        #region Raise Events
        public void RaiseColorClicked(Color color)
        {
            if (OnColorClicked != null)
                OnColorClicked(color);
        }

        public void RaiseDrawTypeClicked(DrawType type, int width)
        {
            if (OnDrawTypeClicked != null)
                OnDrawTypeClicked(type, width);
        }
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
                    _dlgWaiting.Close();
                    _dlgWaiting = null;
                }
            };

            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }
        #endregion
    }
}
