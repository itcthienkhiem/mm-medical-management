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
    public partial class uKetLuanList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uKetLuanList()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now;
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
            btnAdd.Enabled = Global.AllowAddKetLuan;
            btnDelete.Enabled = Global.AllowDeleteKetLuan;

            addToolStripMenuItem.Enabled = Global.AllowAddKetLuan;
            deleteToolStripMenuItem.Enabled = Global.AllowDeleteKetLuan;
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
                

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetLuanProc));
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

        private void ClearData()
        {
            DataTable dt = dgKetLuan.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgKetLuan.DataSource = null;
            }
        }

        private void OnDisplayKetLuan()
        {
            Result result = KetLuanBus.GetKetLuanList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgKetLuan.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetLuanBus.GetKetLuanList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetLuanBus.GetKetLuanList"));
            }
        }

        private void OnAdd()
        {
            dlgAddKetLuan dlg = new dlgAddKetLuan(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgKetLuan.SelectedRows == null || dgKetLuan.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết luận.", IconType.Information);
                return;
            }

            DataRow drKetLuan = (dgKetLuan.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddKetLuan dlg = new dlgAddKetLuan(_patientGUID, drKetLuan, Global.AllowEditKetLuan);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKetLuanList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKetLuan.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKetLuanList.Add(row["KetLuanGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKetLuanList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết luận mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetLuanBus.DeleteKetLuan(deletedKetLuanList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetLuanBus.DeleteKetLuan"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetLuanBus.DeleteKetLuan"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết luận cần xóa.", IconType.Information);
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

        private void dgKetLuan_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgKetLuan.DataSource as DataTable;
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
        private void OnDisplayKetLuanProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetLuan();
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
