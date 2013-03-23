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
    public partial class uToaThuocTrongNgayList : uBase
    {
        #region Constructor
        public uToaThuocTrongNgayList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnTatCanhBao.Enabled = AllowEdit;
            tatCanhBaoToolStripMenuItem.Enabled = AllowEdit;
        }

        public void ClearData()
        {
            DataTable dt = dgToaThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgToaThuoc.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayToaThuocListProc));
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

        private void OnDisplayToaThuocList()
        {
            Result result = KeToaBus.GetToaThuocTrongNgayList();

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgToaThuoc.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetToaThuocTrongNgayList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuocTrongNgayList"));
            }
        }

        private void OnViewToaThuoc()
        {
            if (dgToaThuoc.SelectedRows == null || dgToaThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 toa thuốc.", IconType.Information);
                return;
            }

            DataRow drToaThuoc = (dgToaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddToaThuoc dlg = new dlgAddToaThuoc(drToaThuoc, false);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnTatCanhBao()
        {
            List<string> deletedToaThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgToaThuoc.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedToaThuocList.Add(row["ToaThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedToaThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn tắt cảnh báo những toa thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KeToaBus.TatCanhBaoToaThuoc(deletedToaThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.TatCanhBaoToaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.TatCanhBaoToaThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những toa thuốc cần tắt cảnh báo.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            OnViewToaThuoc();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgToaThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void tatCanhBaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnTatCanhBao();
        }

        private void btnTatCanhBao_Click(object sender, EventArgs e)
        {
            OnTatCanhBao();
        }
        #endregion

        #region Working Thread
        private void OnDisplayToaThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayToaThuocList();
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
