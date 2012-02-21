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
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiList()
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
        public void DisplayAsThread()
        {
            if (_patientRow == null) return;

            try
            {
                DataRow row = _patientRow as DataRow;
                _patientGUID = row["PatientGUID"].ToString();
                _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaNoiSoiListProc));
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

        private void OnDisplayKetQuaNoiSoiList()
        {
            Result result = KetQuaNoiSoiBus.GetKetQuaNoiSoiList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgKhamNoiSoi.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList"));
            }
        }

        private void OnAdd()
        {
            dlgAddKetQuaNoiSoi dlg = new dlgAddKetQuaNoiSoi(_patientGUID);
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void OnEdit()
        {
            if (dgKhamNoiSoi.SelectedRows == null || dgKhamNoiSoi.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết quả nội soi.", IconType.Information);
                return;
            }

            DataRow drKetQuaNoiSoi = (dgKhamNoiSoi.SelectedRows[0].DataBoundItem as DataRowView).Row;

        }

        private void OnDelete()
        {
            List<string> deletedKQNSList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKhamNoiSoi.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQNSList.Add(row["KetQuaNoiSoiGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKQNSList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết quả nội soi mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetQuaNoiSoiBus.DeleteKetQuaNoiSoi(deletedKQNSList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaNoiSoiBus.DeleteKetQuaNoiSoi"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.DeleteKetQuaNoiSoi"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả nội soi.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
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

        private void dgKhamNoiSoi_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgKhamNoiSoi.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaNoiSoiListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaNoiSoiList();
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
