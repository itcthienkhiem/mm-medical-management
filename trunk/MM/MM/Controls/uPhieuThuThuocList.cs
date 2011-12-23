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
    public partial class uPhieuThuThuocList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPhieuThuThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
        }

        public void ClearData()
        {

        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPhieuThuThuocListProc));
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

        private void OnDisplayPhieuThuThuocList()
        {
            Result result = PhieuThuThuocBus.GetPhieuThuThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgPhieuThu.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocList"));
            }
        }

        private void SelectLastedRow()
        {
            dgPhieuThu.CurrentCell = dgPhieuThu[1, dgPhieuThu.RowCount - 1];
            dgPhieuThu.Rows[dgPhieuThu.RowCount - 1].Selected = true;
        }

        private void OnAddPhieuThu()
        {

        }

        private void OnEditPhieuThu()
        {

        }

        private void OnDeletePhieuThu()
        {
            List<string> deletedPTThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedPTThuocList.Add(row["PhieuThuThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedPTThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những phiếu thu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PhieuThuThuocBus.DeletePhieuThuThuoc(deletedPTThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.DeletePhieuThuThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.DeletePhieuThuThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần xóa.", IconType.Information);
        }

        private void OnPrint(bool isPreview)
        {

        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddPhieuThu();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditPhieuThu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeletePhieuThu();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void dgPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditPhieuThu();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

       
        #endregion

        #region Working Thread
        private void OnDisplayPhieuThuThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayPhieuThuThuocList();
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
