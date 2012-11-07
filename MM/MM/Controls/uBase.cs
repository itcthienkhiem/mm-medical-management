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
    public delegate void OpenPatientHandler(object patientRow);
    public delegate void ServiceHistoryChangedHandler();
    public delegate void ExportReceiptChangedHandler();
    public delegate void RefreshCheckListHandler();
    public delegate void RefreshPatientHandler();
    #endregion

    public partial class uBase : UserControl
    {
        #region Events
        public event ColorClickedHandler OnColorClicked;
        public event DrawTypeClickedHandler OnDrawTypeClicked;
        public event OpenPatientHandler OnOpenPatient;
        public event ServiceHistoryChangedHandler OnServiceHistoryChanged;
        public event ExportReceiptChangedHandler OnExportReceiptChanged;
        public event RefreshCheckListHandler OnRefreshCheckList;
        public event RefreshPatientHandler OnRefreshPatient;
        #endregion

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

        public void RaiseOpentPatient(object patientRow)
        {
            if (OnOpenPatient != null)
                OnOpenPatient(patientRow);
        }

        public void RaiseServiceHistoryChanged()
        {
            if (OnServiceHistoryChanged != null)
                OnServiceHistoryChanged();
        }

        public void RaiseExportReceiptChanged()
        {
            if (OnExportReceiptChanged != null)
                OnServiceHistoryChanged();
        }

        public void RaiseRefreshCheckList()
        {
            if (OnRefreshCheckList != null)
                OnRefreshCheckList();
        }

        public void RaiseRefreshPatient()
        {
            if (OnRefreshPatient != null)
                OnRefreshPatient();
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
            /*MethodInvoker method = delegate
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
                method.Invoke();*/

           

            this.Invoke(new MethodInvoker(delegate()
            {
                if (_dlgWaiting != null)
                {
                    _dlgWaiting.Close();
                    _dlgWaiting = null;
                }
            }));
        }
        #endregion
    }
}
