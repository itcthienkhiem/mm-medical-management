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
using MM.Exports;

namespace MM.Controls
{
    public partial class uToaCapCuuList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isAll = true;
        #endregion

        #region Constructor
        public uToaCapCuuList()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
            dtpkToDate.Value = DateTime.Now;
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
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
        }

        public void ClearData()
        {
            DataTable dt = dgToaCapCuu.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgToaCapCuu.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;

                _isAll = raAll.Checked;
                _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayToaCapCuuListProc));
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

        private void OnDisplayToaCapCuuList()
        {
            Result result = KeToaCapCuuBus.GetToaCapCuuList(_isAll, _fromDate, _toDate);

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgToaCapCuu.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaCapCuuBus.GetToaCapCuuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaCapCuuBus.GetToaCapCuuList"));
            }
        }

        private void SelectLastedRow()
        {
            dgToaCapCuu.CurrentCell = dgToaCapCuu[1, dgToaCapCuu.RowCount - 1];
            dgToaCapCuu.Rows[dgToaCapCuu.RowCount - 1].Selected = true;
        }

        private void OnAdd()
        {
            dlgAddKeToaCapCuu dlg = new dlgAddKeToaCapCuu();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgToaCapCuu.SelectedRows == null || dgToaCapCuu.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 toa cấp cứu.", IconType.Information);
                return;
            }

            DataRow drToaCapCuu = (dgToaCapCuu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddKeToaCapCuu dlg = new dlgAddKeToaCapCuu(drToaCapCuu, AllowEdit);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgToaCapCuu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedList.Add(row["ToaCapCuuGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những toa cấp cứu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KeToaCapCuuBus.DeleteToaCapCuu(deletedList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaCapCuuBus.DeleteToaCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KeToaCapCuuBus.DeleteToaCapCuu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những toa cấp cứu cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
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

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgToaCapCuu.DataSource as DataTable;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
        }
        #endregion

        #region Working Thread
        private void OnDisplayToaCapCuuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayToaCapCuuList();
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
