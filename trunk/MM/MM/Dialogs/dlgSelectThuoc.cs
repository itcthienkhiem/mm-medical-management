using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgSelectThuoc : dlgBase
    {
        #region Members
        private List<string> _addedThuocs = null;
        private List<DataRow> _deletedThuocRows = null;
        private string _nhomThuocGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgSelectThuoc(List<string> addedThuocs, List<DataRow> deletedThuocRows, string nhomThuocGUID)
        {
            InitializeComponent();
            _nhomThuocGUID = nhomThuocGUID;
            _addedThuocs = addedThuocs;
            _deletedThuocRows = deletedThuocRows;

        }
        #endregion

        #region Properties
        public List<DataRow> CheckedThuocs
        {
            get
            {
                if (dgThuoc.RowCount <= 0) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgThuoc.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        checkedRows.Add(row);
                    }
                }

                return checkedRows;
            }
        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayThuocListProc));
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

        private DataTable GetDataSource(DataTable dt)
        {
            //Delete
            List<DataRow> deletedRows = new List<DataRow>();
            foreach (string key in _addedThuocs)
            {
                DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", key));
                if (rows == null || rows.Length <= 0) continue;

                deletedRows.AddRange(rows);
            }

            foreach (DataRow row in deletedRows)
            {
                dt.Rows.Remove(row);
            }

            //Add
            foreach (DataRow row in _deletedThuocRows)
            {
                string key = row["ThuocGUID"].ToString();
                DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", key));
                if (rows != null && rows.Length > 0) continue;

                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ThuocGUID"] = key;
                newRow["MaThuoc"] = row["MaThuoc"];
                newRow["TenThuoc"] = row["TenThuoc"];
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        private void OnDisplayThuocList()
        {
            Result result = ThuocBus.GetThuocListNotInNhomThuoc(_nhomThuocGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dataSource = GetDataSource((DataTable)result.QueryResult);//result.QueryResult as DataTable;
                    dgThuoc.DataSource = dataSource;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dlgSelectThuoc_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dlgSelectThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedThuocs = this.CheckedThuocs;
                if (checkedThuocs == null || checkedThuocs.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 thuốc.", IconType.Information);
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayThuocList();
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
