using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Controls;

namespace MM.Dialogs
{
    public delegate void AddMemberHandler(List<DataRow> checkedMembers, List<string> addedServices, DataTable serviceDataSource);
    public delegate void RefreshPatientHandler();

    public partial class dlgBase : Form
    {
        #region Events
        public event AddMemberHandler OnAddMemberEvent = null;
        public event OpenPatientHandler OnOpenPatient;
        public event RefreshPatientHandler OnRefreshPatient;
        #endregion

        #region Members
        private dlgWaiting _dlgWaiting = null;
        public bool AllowShowServicePrice = true;
        public bool AllowLock = true;
        #endregion

        #region Constructor
        public dlgBase()
        {
            InitializeComponent();
        }
        #endregion

        #region Raise Events
        public void RaiseAddMember(List<DataRow> checkedMembers, List<string> addedServices, DataTable serviceDataSource)
        {
            if (OnAddMemberEvent != null)
                OnAddMemberEvent(checkedMembers, addedServices, serviceDataSource);
        }

        public void RaiseOpentPatient(object patientRow)
        {
            if (OnOpenPatient != null)
                OnOpenPatient(patientRow);
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
           /* MethodInvoker method = delegate
            {
                if (_dlgWaiting != null)
                {
                    _dlgWaiting.Hide();
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
