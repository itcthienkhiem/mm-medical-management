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
    public partial class uLoiKhuyenList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uLoiKhuyenList()
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
            btnAdd.Enabled = Global.AllowAddLoiKhuyen;
            btnDelete.Enabled = Global.AllowDeleteLoiKhuyen;
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

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayLoiKhuyenListProc));
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

        private void OnDisplayLoiKhuyenList()
        {
            Result result = LoiKhuyenBus.GetLoiKhuyenList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgLoiKhuyen.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoiKhuyenBus.GetLoiKhuyenList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoiKhuyenBus.GetLoiKhuyenList"));
            }
        }

        private void OnAdd()
        {
            dlgAddMultiLoiKhuyen dlg = new dlgAddMultiLoiKhuyen(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgLoiKhuyen.SelectedRows == null || dgLoiKhuyen.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 lời khuyên.", IconType.Information);
                return;
            }

            DataRow drLoiKhuyen = (dgLoiKhuyen.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddLoiKhuyen dlg = new dlgAddLoiKhuyen(_patientGUID, drLoiKhuyen, Global.AllowEditLoiKhuyen);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedLoiKhuyenList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgLoiKhuyen.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedLoiKhuyenList.Add(row["LoiKhuyenGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedLoiKhuyenList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những lời khuyên mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = LoiKhuyenBus.DeleteLoiKhuyen(deletedLoiKhuyenList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoiKhuyenBus.DeleteLoiKhuyen"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LoiKhuyenBus.DeleteLoiKhuyen"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những lời khuyên.", IconType.Information);

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

        private void dgLoiKhuyen_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgLoiKhuyen.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

            DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayLoiKhuyenListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayLoiKhuyenList();
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
