using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Controls;
using MM.Databasae;

namespace MM.Dialogs
{
    public delegate void AddMemberHandler(List<DataRow> checkedMembers, List<string> addedServices, DataTable serviceDataSource);
    public delegate void AddChiDinhHandler(ChiDinh chiDinh, string tenBacSiChiDinh);

    public partial class dlgBase : Form
    {
        #region Events
        public event AddMemberHandler OnAddMemberEvent = null;
        public event OpenPatientHandler OnOpenPatientEvent = null;
        public event AddChiDinhHandler OnAddChiDinhEvent = null;
        #endregion

        #region Members
        private dlgWaiting _dlgWaiting = null;
        public bool AllowShowServicePrice = true;
        public bool AllowLock = true;
        public Object ThisLock = new Object();
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

        public void RaiseOpentPatient(DataRow patientRow)
        {
            if (OnOpenPatientEvent != null)
                OnOpenPatientEvent(patientRow);
        }

        public void RaiseAddChiDinh(ChiDinh chiDinh, string tenBacSiChiDinh)
        {
            if (OnAddChiDinhEvent != null)
                OnAddChiDinhEvent(chiDinh, tenBacSiChiDinh);
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
