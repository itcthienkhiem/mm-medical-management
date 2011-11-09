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
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uServiceHistory : uBase
    {
        #region Members
        private object _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isAll = true;
        #endregion

        #region Constructor
        public uServiceHistory()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
            dtpkToDate.Value = DateTime.Now;
        }
        #endregion

        #region Properties
        public object PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        private void OnAdd()
        {
            dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgServiceHistory.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ServiceHistoryGUID"] = dlg.ServiceHistory.ServiceHistoryGUID.ToString();
                newRow["PatientGUID"] = dlg.ServiceHistory.PatientGUID.ToString();
                newRow["DocStaffGUID"] = dlg.ServiceHistory.DocStaffGUID.ToString();
                newRow["ServiceGUID"] = dlg.ServiceHistory.ServiceGUID.ToString();
                newRow["FixedPrice"] = dlg.ServiceHistory.Price;
                newRow["Note"] = dlg.ServiceHistory.Note;
                newRow["Fullname"] = dlg.FullName;
                newRow["Name"] = dlg.ServiceName;
                newRow["Code"] = dlg.ServiceCode;
                newRow["CreatedDate"] = dlg.ServiceHistory.CreatedDate;
                dt.Rows.Add(newRow);
                CalculateTotalPrice();
            }
        }

        private void OnEdit()
        {
            if (dgServiceHistory.SelectedRows == null || dgServiceHistory.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ.");
                return;
            }

            DataRow drServiceHistory = (dgServiceHistory.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientGUID, drServiceHistory);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgServiceHistory.DataSource as DataTable;
                if (dt == null) return;
                drServiceHistory["Checked"] = false;
                drServiceHistory["DocStaffGUID"] = dlg.ServiceHistory.DocStaffGUID.ToString();
                drServiceHistory["ServiceGUID"] = dlg.ServiceHistory.ServiceGUID.ToString();
                drServiceHistory["FixedPrice"] = dlg.ServiceHistory.Price;
                drServiceHistory["Note"] = dlg.ServiceHistory.Note;
                drServiceHistory["Fullname"] = dlg.FullName;
                drServiceHistory["Name"] = dlg.ServiceName;
                drServiceHistory["Code"] = dlg.ServiceCode;
                drServiceHistory["CreatedDate"] = dlg.ServiceHistory.CreatedDate;
                CalculateTotalPrice();
            }
        }

        private void OnDelete()
        {
            List<string> deletedServiceHistoryList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgServiceHistory.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedServiceHistoryList.Add(row["ServiceHistoryGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedServiceHistoryList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ServiceHistoryBus.DeleteServiceHistory(deletedServiceHistoryList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }

                        CalculateTotalPrice();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.DeleteServiceHistory"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.DeleteServiceHistory"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.");
        }

        public void DisplayAsThread()
        {
            if (_patientRow == null) return;

                //if (!raAll.Checked && _fromDate.ToString("dd/MM/yyyy") == dtpkFromDate.Value.ToString("dd/MM/yyyy") &&
                //_toDate.ToString("dd/MM/yyyy") == dtpkToDate.Value.ToString("dd/MM/yyyy")) return;

            try
            {
                DataRow row = _patientRow as DataRow;
                _patientGUID = row["PatientGUID"].ToString();
                _isAll = raAll.Checked;
                _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServiceHistoryProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayServicesHistory()
        {
            Result result = ServiceHistoryBus.GetServiceHistory(_patientGUID, _isAll, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgServiceHistory.DataSource = result.QueryResult;
                    CalculateTotalPrice();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"));
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"));
            }
        }

        private void CalculateTotalPrice()
        {
            double totalPrice = 0;
            DataTable dt = dgServiceHistory.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0)
                lbTotalPrice.Text = string.Format("Tổng tiền: {0} (VNĐ)", totalPrice);
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    double price = Convert.ToDouble(row["FixedPrice"]);
                    totalPrice += price;
                }

                lbTotalPrice.Text = string.Format("Tổng tiền: {0:#,###} (VNĐ)", totalPrice);
            }
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

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgServiceHistory.DataSource as DataTable;
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

        private void dtpk_ValueChanged(object sender, EventArgs e)
        {
            //if (_fromDate.ToString("dd/MM/yyyy") == dtpkFromDate.Value.ToString("dd/MM/yyyy") &&
            //    _toDate.ToString("dd/MM/yyyy") == dtpkToDate.Value.ToString("dd/MM/yyyy")) return;

            //DisplayAsThread();
        }

        private void dgServiceHistory_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void dtpk_Leave(object sender, EventArgs e)
        {
            //if (_fromDate.ToString("dd/MM/yyyy") == dtpkFromDate.Value.ToString("dd/MM/yyyy") &&
            //    _toDate.ToString("dd/MM/yyyy") == dtpkToDate.Value.ToString("dd/MM/yyyy")) return;

            //DisplayAsThread();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayServiceHistoryProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayServicesHistory();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
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
