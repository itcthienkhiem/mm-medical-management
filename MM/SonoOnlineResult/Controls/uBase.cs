using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using SonoOnlineResult.Dialogs;

namespace SonoOnlineResult.Controls
{
    public partial class uBase : UserControl
    {
        #region Members
        private dlgWaiting _dlgWaiting = null;
        public bool AllowAdd = true;
        public bool AllowEdit = true;
        public bool AllowDelete = true;
        public bool AllowPrint = true;
        public bool AllowImport = true;
        public bool AllowExport = true;
        public bool AllowConfirm = true;
        public bool AllowShowServicePrice = true;
        public bool AllowOpenPatient = true;
        public bool AllowAddDangKy = true;
        public bool AllowEditDangKy = true;
        public bool AllowDeleteDangKy = true;
        public bool AllowLock = true;
        public bool AllowExportAll = true;
        public bool AllowView = true;
        public bool AllowExportInvoice = true;
        public bool AllowSendSMS = true;
        public bool AllowChuyenKetQuaKham = false;
        public Object ThisLock = new Object();
        #endregion

        #region Constructor
        public uBase()
        {
            InitializeComponent();
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
            try
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    if (_dlgWaiting != null)
                    {
                        _dlgWaiting.Close();
                        _dlgWaiting = null;
                    }
                }));
            }
            catch
            {
                if (_dlgWaiting != null)
                {
                    _dlgWaiting.Close();
                    _dlgWaiting = null;
                }
            }
        }

        public void StartTimer()
        {
            if (timerSearch.Enabled) return;
            timerSearch.Enabled = true;
            timerSearch.Start();
        }

        public void StopTimer()
        {
            timerSearch.Stop();
            timerSearch.Enabled = false;
        }
        #endregion

        #region Virtual Methods
        public virtual void SearchAsThread()
        {

        }
        #endregion

        #region Window Event Handlers
        private void timerSearch_Tick(object sender, EventArgs e)
        {
            SearchAsThread();
            StopTimer();
        }
        #endregion
    }
}
