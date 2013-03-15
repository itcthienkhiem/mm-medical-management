using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;
using MM.Common;

namespace MM.Controls
{
    public partial class uTinNhanMauList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uTinNhanMauList()
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
            btnDuyet.Enabled = AllowConfirm;
            btnBoDuyet.Enabled = AllowConfirm;

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            duyetToolStripMenuItem.Enabled = AllowConfirm;
            boDuyetToolStripMenuItem.Enabled = AllowConfirm;
        }

        public void ClearData()
        {
            DataTable dt = dgTinNhanMau.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgTinNhanMau.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayTinNhanMauListProc));
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

        private void OnDisplayTinNhanMauList()
        {
            Result result = TinNhanMauBus.GetTinNhanMauList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgTinNhanMau.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("TinNhanMauBus.GetTinNhanMauList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("TinNhanMauBus.GetTinNhanMauList"));
            }
        }

        private void OnAdd()
        {
            dlgAddTinNhanMau dlg = new dlgAddTinNhanMau();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgTinNhanMau.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["TinNhanMauGUID"] = dlg.TinNhanMau.TinNhanMauGUID.ToString();
                newRow["TieuDe"] = dlg.TinNhanMau.TieuDe;
                newRow["NoiDung"] = dlg.TinNhanMau.NoiDung;

                if (dlg.TinNhanMau.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.TinNhanMau.CreatedDate;

                if (dlg.TinNhanMau.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.TinNhanMau.CreatedBy.ToString();

                if (dlg.TinNhanMau.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.TinNhanMau.UpdatedDate;

                if (dlg.TinNhanMau.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.TinNhanMau.UpdatedBy.ToString();

                if (dlg.TinNhanMau.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.TinNhanMau.DeletedDate;

                if (dlg.TinNhanMau.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.TinNhanMau.DeletedBy.ToString();

                newRow["Status"] = dlg.TinNhanMau.Status;
                dt.Rows.Add(newRow);
            }
        }

        private void OnEdit()
        {
            if (dgTinNhanMau.SelectedRows == null || dgTinNhanMau.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 tin nhắn mẫu cần sửa.", IconType.Information);
                return;
            }

            DataRow drTinNhanMau = (dgTinNhanMau.SelectedRows[0].DataBoundItem as DataRowView).Row;
            bool isDuyet = Convert.ToBoolean(drTinNhanMau["IsDuyet"]);
            if (isDuyet)
            {
                MsgBox.Show(Application.ProductName, "Tin nhắn mẫu này đã được duyệt. Bạn không thể sửa.", IconType.Information);
                return;
            }

            dlgAddTinNhanMau dlg = new dlgAddTinNhanMau(drTinNhanMau);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drTinNhanMau["TieuDe"] = dlg.TinNhanMau.TieuDe;
                drTinNhanMau["NoiDung"] = dlg.TinNhanMau.NoiDung;

                if (dlg.TinNhanMau.CreatedDate.HasValue)
                    drTinNhanMau["CreatedDate"] = dlg.TinNhanMau.CreatedDate;

                if (dlg.TinNhanMau.CreatedBy.HasValue)
                    drTinNhanMau["CreatedBy"] = dlg.TinNhanMau.CreatedBy.ToString();

                if (dlg.TinNhanMau.UpdatedDate.HasValue)
                    drTinNhanMau["UpdatedDate"] = dlg.TinNhanMau.UpdatedDate;

                if (dlg.TinNhanMau.UpdatedBy.HasValue)
                    drTinNhanMau["UpdatedBy"] = dlg.TinNhanMau.UpdatedBy.ToString();

                if (dlg.TinNhanMau.DeletedDate.HasValue)
                    drTinNhanMau["DeletedDate"] = dlg.TinNhanMau.DeletedDate;

                if (dlg.TinNhanMau.DeletedBy.HasValue)
                    drTinNhanMau["DeletedBy"] = dlg.TinNhanMau.DeletedBy.ToString();

                drTinNhanMau["Status"] = dlg.TinNhanMau.Status;
            }
        }

        private void OnDelete()
        {
            List<string> deletedKeys = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgTinNhanMau.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKeys.Add(row["TinNhanMauGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKeys.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những tin nhắn mẫu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = TinNhanMauBus.DeleteTinNhanMau(deletedKeys);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("TinNhanMauBus.DeleteTinNhanMau"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("TinNhanMauBus.DeleteTinNhanMau"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những tin nhắn mẫu cần xóa.", IconType.Information);
        }

        private void OnDuyetTinNhan(bool isDuyet)
        {
            List<string> deletedKeys = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgTinNhanMau.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKeys.Add(row["TinNhanMauGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            string msg = isDuyet ? "Bạn có muốn duyệt những tin nhắn mẫu mà bạn đã đánh dấu ?" : 
                "Bạn có muốn bỏ duyệt những tin nhắn mẫu mà bạn đã đánh dấu ?";

            string msg2 = isDuyet ? "Vui lòng đánh dấu những tin nhắn mẫu cần duyệt." :
                "Vui lòng đánh dấu những tin nhắn mẫu cần bỏ duyệt.";

            if (deletedKeys.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, msg) == DialogResult.Yes)
                {
                    Result result = TinNhanMauBus.DuyetTinNhanMau(deletedKeys, isDuyet);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("TinNhanMauBus.DuyetTinNhanMau"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("TinNhanMauBus.DuyetTinNhanMau"));
                    }
                    else
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            row["IsDuyet"] = isDuyet;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, msg2, IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgTinNhanMau.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgTinNhanMau_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEdit();
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

        private void duyetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDuyetTinNhan(true);
        }

        private void boDuyetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDuyetTinNhan(false);
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            OnDuyetTinNhan(true);
        }

        private void btnBoDuyet_Click(object sender, EventArgs e)
        {
            OnDuyetTinNhan(false);
        }
        #endregion

        #region Working Thread
        private void OnDisplayTinNhanMauListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayTinNhanMauList();
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
