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
    public partial class uNhomThuocList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNhomThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            //btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
        }

        public void ClearData()
        {
            dgNhomThuoc.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayNhomThuocListProc));
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

        private void OnDisplayNhomThuocList()
        {
            Result result = NhomThuocBus.GetNhomThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgNhomThuoc.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhomThuocBus.GetNhomThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhomThuocBus.GetNhomThuocList"));
            }
        }

        private void SelectLastedRow()
        {
            dgNhomThuoc.CurrentCell = dgNhomThuoc[1, dgNhomThuoc.RowCount - 1];
            dgNhomThuoc.Rows[dgNhomThuoc.RowCount - 1].Selected = true;
        }

        private void OnAddNhomThuoc()
        {
            dlgAddNhomThuoc dlg = new dlgAddNhomThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgNhomThuoc.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["NhomThuocGUID"] = dlg.NhomThuoc.NhomThuocGUID.ToString();
                newRow["MaNhomThuoc"] = dlg.NhomThuoc.MaNhomThuoc;
                newRow["TenNhomThuoc"] = dlg.NhomThuoc.TenNhomThuoc;
                newRow["Note"] = dlg.NhomThuoc.Note;

                if (dlg.NhomThuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.NhomThuoc.CreatedDate;

                if (dlg.NhomThuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.NhomThuoc.CreatedBy.ToString();

                if (dlg.NhomThuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.NhomThuoc.UpdatedDate;

                if (dlg.NhomThuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.NhomThuoc.UpdatedBy.ToString();

                if (dlg.NhomThuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.NhomThuoc.DeletedDate;

                if (dlg.NhomThuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.NhomThuoc.DeletedBy.ToString();

                newRow["Status"] = dlg.NhomThuoc.Status;
                dt.Rows.Add(newRow);
                //SelectLastedRow();
            }
        }

        private void OnEditNhomThuoc()
        {
            if (dgNhomThuoc.SelectedRows == null || dgNhomThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 nhóm thuốc.", IconType.Information);
                return;
            }

            DataRow drNhomThuoc = (dgNhomThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddNhomThuoc dlg = new dlgAddNhomThuoc(drNhomThuoc, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drNhomThuoc["MaNhomThuoc"] = dlg.NhomThuoc.MaNhomThuoc;
                drNhomThuoc["TenNhomThuoc"] = dlg.NhomThuoc.TenNhomThuoc;
                drNhomThuoc["Note"] = dlg.NhomThuoc.Note;

                if (dlg.NhomThuoc.CreatedDate.HasValue)
                    drNhomThuoc["CreatedDate"] = dlg.NhomThuoc.CreatedDate;

                if (dlg.NhomThuoc.CreatedBy.HasValue)
                    drNhomThuoc["CreatedBy"] = dlg.NhomThuoc.CreatedBy.ToString();

                if (dlg.NhomThuoc.UpdatedDate.HasValue)
                    drNhomThuoc["UpdatedDate"] = dlg.NhomThuoc.UpdatedDate;

                if (dlg.NhomThuoc.UpdatedBy.HasValue)
                    drNhomThuoc["UpdatedBy"] = dlg.NhomThuoc.UpdatedBy.ToString();

                if (dlg.NhomThuoc.DeletedDate.HasValue)
                    drNhomThuoc["DeletedDate"] = dlg.NhomThuoc.DeletedDate;

                if (dlg.NhomThuoc.DeletedBy.HasValue)
                    drNhomThuoc["DeletedBy"] = dlg.NhomThuoc.DeletedBy.ToString();

                drNhomThuoc["Status"] = dlg.NhomThuoc.Status;
            }
        }

        private void OnDeleteNhomThuoc()
        {
            List<string> deletedNhomThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgNhomThuoc.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedNhomThuocList.Add(row["NhomThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedNhomThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhóm thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = NhomThuocBus.DeleteNhomThuoc(deletedNhomThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhomThuocBus.DeleteNhomThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhomThuocBus.DeleteNhomThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhóm thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddNhomThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditNhomThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteNhomThuoc();
        }

        private void dgNhomThuoc_DoubleClick(object sender, EventArgs e)
        {
            //if (!AllowEdit) return;
            OnEditNhomThuoc();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgNhomThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayNhomThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayNhomThuocList();
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
