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
    public partial class uToaThuocList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uToaThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
        }

        public void ClearData()
        {
            dgToaThuoc.DataSource = null;
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
            Result result = KeToaBus.GetToaThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgToaThuoc.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuocList"));
            }
        }

        private void SelectLastedRow()
        {
            dgToaThuoc.CurrentCell = dgToaThuoc[1, dgToaThuoc.RowCount - 1];
            dgToaThuoc.Rows[dgToaThuoc.RowCount - 1].Selected = true;
        }

        private void OnAddToaThuoc()
        {
            dlgAddToaThuoc dlg = new dlgAddToaThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgToaThuoc.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ToaThuocGUID"] = dlg.ToaThuoc.ToaThuocGUID.ToString();
                newRow["MaToaThuoc"] = dlg.ToaThuoc.MaToaThuoc;
                newRow["NgayKeToa"] = dlg.ToaThuoc.NgayKeToa;
                newRow["BacSiKeToa"] = dlg.ToaThuoc.BacSiKeToa;
                newRow["BenhNhan"] = dlg.ToaThuoc.BenhNhan;
                newRow["TenBacSi"] = dlg.TenBacSi;
                newRow["TenBenhNhan"] = dlg.TenBenhNhan;
                newRow["Note"] = dlg.ToaThuoc.Note;

                if (dlg.ToaThuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.ToaThuoc.CreatedDate;

                if (dlg.ToaThuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.ToaThuoc.CreatedBy.ToString();

                if (dlg.ToaThuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.ToaThuoc.UpdatedDate;

                if (dlg.ToaThuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.ToaThuoc.UpdatedBy.ToString();

                if (dlg.ToaThuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.ToaThuoc.DeletedDate;

                if (dlg.ToaThuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.ToaThuoc.DeletedBy.ToString();

                newRow["Status"] = dlg.ToaThuoc.Status;
                dt.Rows.Add(newRow);
                SelectLastedRow();
            }
        }

        private void OnEditToaThuoc()
        {
            if (dgToaThuoc.SelectedRows == null || dgToaThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 toa thuốc.", IconType.Information);
                return;
            }

            DataRow drToaThuoc = (dgToaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddToaThuoc dlg = new dlgAddToaThuoc(drToaThuoc);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                drToaThuoc["MaToaThuoc"] = dlg.ToaThuoc.MaToaThuoc;
                drToaThuoc["NgayKeToa"] = dlg.ToaThuoc.NgayKeToa;
                drToaThuoc["BacSiKeToa"] = dlg.ToaThuoc.BacSiKeToa;
                drToaThuoc["BenhNhan"] = dlg.ToaThuoc.BenhNhan;
                drToaThuoc["TenBacSi"] = dlg.TenBacSi;
                drToaThuoc["TenBenhNhan"] = dlg.TenBenhNhan;
                drToaThuoc["Note"] = dlg.ToaThuoc.Note;

                if (dlg.ToaThuoc.CreatedDate.HasValue)
                    drToaThuoc["CreatedDate"] = dlg.ToaThuoc.CreatedDate;

                if (dlg.ToaThuoc.CreatedBy.HasValue)
                    drToaThuoc["CreatedBy"] = dlg.ToaThuoc.CreatedBy.ToString();

                if (dlg.ToaThuoc.UpdatedDate.HasValue)
                    drToaThuoc["UpdatedDate"] = dlg.ToaThuoc.UpdatedDate;

                if (dlg.ToaThuoc.UpdatedBy.HasValue)
                    drToaThuoc["UpdatedBy"] = dlg.ToaThuoc.UpdatedBy.ToString();

                if (dlg.ToaThuoc.DeletedDate.HasValue)
                    drToaThuoc["DeletedDate"] = dlg.ToaThuoc.DeletedDate;

                if (dlg.ToaThuoc.DeletedBy.HasValue)
                    drToaThuoc["DeletedBy"] = dlg.ToaThuoc.DeletedBy.ToString();

                drToaThuoc["Status"] = dlg.ToaThuoc.Status;
            }
        }

        private void OnDeleteToaThuoc()
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
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những toa thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KeToaBus.DeleteToaThuoc(deletedToaThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.DeleteToaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.DeleteToaThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những toa thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddToaThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditToaThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteToaThuoc();
        }

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditToaThuoc();
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
