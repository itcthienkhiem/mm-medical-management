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

namespace MM.Controls
{
    public partial class uKetQuaSieuAmList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isPrint = false;
        private KetQuaSieuAm _ketQuaSieuAm = null;
        #endregion

        #region Constructor
        public uKetQuaSieuAmList()
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
            btnAdd.Enabled = Global.AllowAddSieuAm;
            btnDelete.Enabled = Global.AllowDeleteSieuAm;
            btnPrint.Enabled = Global.AllowPrintSieuAm;
            btnPrintPreview.Enabled = Global.AllowPrintSieuAm;
            btnExportExcel.Enabled = Global.AllowExportSieuAm;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            if (_patientRow == null) return;

            try
            {
                DataRow row = _patientRow as DataRow;
                _patientGUID = row["PatientGUID"].ToString();
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


                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaSieuAmListProc));
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

        private void OnDisplayKetQuaSieuAmList()
        {
            Result result = SieuAmBus.GetKetQuaSieuAmList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    dgSieuAm.DataSource = dt;

                    if (_isPrint)
                    {
                        DataRow[] rows = dt.Select(string.Format("KetQuaSieuAmGUID='{0}'", _ketQuaSieuAm.KetQuaSieuAmGUID.ToString()));
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList"));
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

        private void OnPrint(DataRow drKetQuaSieuAm)
        {

        }

        private void OnPrint(bool isPreview)
        {

        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgSieuAm.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            return checkedRows;
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
            DataTable dt = dgSieuAm.DataSource as DataTable;
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
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaSieuAmListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaSieuAmList();
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
