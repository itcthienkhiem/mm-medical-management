using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgSelectNhanVienHopDong : dlgBase
    {
        #region Members
        private DateTime _activedDate = DateTime.Now;
        private string _hopDongGUID = string.Empty;
        private string _serviceGUID = string.Empty;
        private string _patientGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgSelectNhanVienHopDong(DateTime activedDate, string serviceGUID, string patientGUID)
        {
            InitializeComponent();
            _activedDate = activedDate;
            _serviceGUID = serviceGUID;
            _patientGUID = patientGUID;
            _uSearchPatient.OnOpenPatientEvent += new MM.Controls.OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return (DataRow)_uSearchPatient.PatientRow; }
        }

        public string HopDongGUID
        {
            get { return cboMaHopDong.SelectedValue.ToString(); }
        }
        #endregion

        #region UI Command
        private void DisplayHopDongByThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DisplayHopDongProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayHopDong()
        {
            Result result = CompanyContractBus.GetHopDongByDate(_activedDate, _serviceGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    cboMaHopDong.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetHopDongByDate"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetHopDongByDate"));
            }
        }

        private string GetTenHopDong()
        {
            List<CompanyContractView> hopDongList = cboMaHopDong.DataSource as List<CompanyContractView>;
            if (hopDongList != null && hopDongList.Count > 0)
            {
                foreach (var hd in hopDongList)
                {
                    if (hd.CompanyContractGUID.ToString() == _hopDongGUID)
                        return hd.ContractName;
                }
            }

            return string.Empty;
        }

        private void GetDanhSachNhanVienTheoHopDong()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = CompanyContractBus.GetContractMemberList(_hopDongGUID, _serviceGUID);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow[] rows = dt.Select(string.Format("PatientGUID = '{0}'", _patientGUID));
                if (rows != null && rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                        dt.Rows.Remove(row);
                }

                _uSearchPatient.DataSource = dt;
                _uSearchPatient.OnSearch();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
            }
        }
        #endregion

        #region Window Event Handles
        private void dlgSelectNhanVienHopDong_Load(object sender, EventArgs e)
        {
            DisplayHopDongByThread();
        }

        private void _uSearchPatient_OnOpenPatient(object patientRow)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;
            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();

            txtTenHopDong.Text = GetTenHopDong();

            GetDanhSachNhanVienTheoHopDong();
        }
        #endregion

        #region Working Thread
        private void DisplayHopDongProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayHopDong();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
