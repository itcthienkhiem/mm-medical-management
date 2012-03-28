using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Exports;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uKetQuaSoiCTCList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isPrint = false;
        private KetQuaSoiCTC _ketQuaSoiCTC = new KetQuaSoiCTC();
        #endregion

        #region Constructor
        public uKetQuaSoiCTCList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = Global.AllowAddKhamNoiSoi;
            btnDelete.Enabled = Global.AllowDeleteKhamNoiSoi;
            btnPrint.Enabled = Global.AllowPrintKhamNoiSoi;
            btnPrintPreview.Enabled = Global.AllowPrintKhamNoiSoi;
            btnExportExcel.Enabled = Global.AllowExportKhamNoiSoi;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            if (_patientRow == null) return;

            try
            {
                _patientGUID = _patientRow["PatientGUID"].ToString();
                if (raAll.Checked)
                {
                    _fromDate = Global.MinDateTime;
                    _toDate = Global.MaxDateTime;
                }
                else
                {
                    _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                    _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
                }


                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaSoiCTCListProc));
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

        private void OnDisplayKetQuaSoiCTCList()
        {
            Result result = KetQuaSoiCTCBus.GetKetQuaSoiCTCList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    dgSoCTC.DataSource = dt;

                    if (_isPrint)
                    {
                        DataRow[] rows = dt.Select(string.Format("KetQuaSoiCTCGUID='{0}'", _ketQuaSoiCTC.KetQuaSoiCTCGUID.ToString()));
                        if (rows != null && rows.Length > 0)
                        {
                            OnPrint(rows[0]);
                        }
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList"));
            }
        }

        private void OnAdd()
        {

        }

        private void OnEdit()
        {

        }

        private void OnDelete()
        {

        }

        private void OnPrint(bool isPreview)
        {

        }

        private void OnPrint(DataRow drKetQuaSoiCTC)
        {

        }

        private void OnExportExcel()
        {

        }
        #endregion

        #region Window Event Handlers
        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

            DisplayAsThread();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgSoCTC.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

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

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void dgSoCTC_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaSoiCTCListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaSoiCTCList();
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
