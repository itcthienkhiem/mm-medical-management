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
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uKetQuaLamSangList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uKetQuaLamSangList()
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

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaLamSangListProc));
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

        private void OnDisplayKetQuaLamSangList()
        {
            Result result = KetQuaLamSangBus.GetKetQuaLamSangList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgKhamLamSang.DataSource = result.QueryResult;
                    UpdateKetQua();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaLamSangBus.GetKetQuaLamSangList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaLamSangBus.GetKetQuaLamSangList"));
            }
        }

        private void UpdateKetQua()
        {
            DataTable dt = dgKhamLamSang.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                bool normal = Convert.ToBoolean(row["Normal"]);
                bool abnormal = Convert.ToBoolean(row["Abnormal"]);

                string kq = string.Empty;
                if (normal) kq += "Bình thường, ";
                if (abnormal) kq += "Bất thường, ";

                if (kq != string.Empty) kq = kq.Substring(0, kq.Length - 2);

                row["KetQua"] = kq;
            }
        }

        private void OnAdd()
        {
            dlgAddKhamLamSang dlg = new dlgAddKhamLamSang(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgKhamLamSang.SelectedRows == null || dgKhamLamSang.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết quả lâm sàng.", IconType.Information);
                return;
            }

            DataRow drKetQuaLamSang = (dgKhamLamSang.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddKhamLamSang dlg = new dlgAddKhamLamSang(_patientGUID, drKetQuaLamSang);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKQLSList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKhamLamSang.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQLSList.Add(row["KetQuaLamSangGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKQLSList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết quả lâm sàng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetQuaLamSangBus.DeleteKetQuaLamSang(deletedKQLSList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaLamSangBus.DeleteKetQuaLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaLamSangBus.DeleteKetQuaLamSang"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả lâm sàng.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgKhamLamSang.DataSource as DataTable;
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

        private void dgKhamLamSang_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaLamSangListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaLamSangList();
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
