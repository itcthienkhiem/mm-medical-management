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
    public partial class uThuocList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uThuocList()
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
            dgThuoc.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
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

        private void OnDisplayThuocList()
        {
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgThuoc.DataSource = result.QueryResult;
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

        private void OnAddThuoc()
        {
            dlgAddThuoc dlg = new dlgAddThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgThuoc.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ThuocGUID"] = dlg.Thuoc.ThuocGUID.ToString();
                newRow["MaThuoc"] = dlg.Thuoc.MaThuoc;
                newRow["TenThuoc"] = dlg.Thuoc.TenThuoc;
                newRow["BietDuoc"] = dlg.Thuoc.BietDuoc;
                newRow["HamLuong"] = dlg.Thuoc.HamLuong;
                newRow["HoatChat"] = dlg.Thuoc.HoatChat;
                newRow["DonViTinh"] = dlg.Thuoc.DonViTinh;
                newRow["Note"] = dlg.Thuoc.Note;

                if (dlg.Thuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Thuoc.CreatedDate;

                if (dlg.Thuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Thuoc.CreatedBy.ToString();

                if (dlg.Thuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Thuoc.UpdatedDate;

                if (dlg.Thuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Thuoc.UpdatedBy.ToString();

                if (dlg.Thuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Thuoc.DeletedDate;

                if (dlg.Thuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Thuoc.DeletedBy.ToString();

                newRow["Status"] = dlg.Thuoc.Status;
                dt.Rows.Add(newRow);
                SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgThuoc.CurrentCell = dgThuoc[1, dgThuoc.RowCount - 1];
            dgThuoc.Rows[dgThuoc.RowCount - 1].Selected = true;
        }

        private void OnEditThuoc()
        {
            if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thuốc.", IconType.Information);
                return;
            }

            DataRow drThuoc = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddThuoc dlg = new dlgAddThuoc(drThuoc);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drThuoc["MaThuoc"] = dlg.Thuoc.MaThuoc;
                drThuoc["TenThuoc"] = dlg.Thuoc.TenThuoc;
                drThuoc["BietDuoc"] = dlg.Thuoc.BietDuoc;
                drThuoc["HamLuong"] = dlg.Thuoc.HamLuong;
                drThuoc["HoatChat"] = dlg.Thuoc.HoatChat;
                drThuoc["DonViTinh"] = dlg.Thuoc.DonViTinh;
                drThuoc["Note"] = dlg.Thuoc.Note;

                if (dlg.Thuoc.CreatedDate.HasValue)
                    drThuoc["CreatedDate"] = dlg.Thuoc.CreatedDate;

                if (dlg.Thuoc.CreatedBy.HasValue)
                    drThuoc["CreatedBy"] = dlg.Thuoc.CreatedBy.ToString();

                if (dlg.Thuoc.UpdatedDate.HasValue)
                    drThuoc["UpdatedDate"] = dlg.Thuoc.UpdatedDate;

                if (dlg.Thuoc.UpdatedBy.HasValue)
                    drThuoc["UpdatedBy"] = dlg.Thuoc.UpdatedBy.ToString();

                if (dlg.Thuoc.DeletedDate.HasValue)
                    drThuoc["DeletedDate"] = dlg.Thuoc.DeletedDate;

                if (dlg.Thuoc.DeletedBy.HasValue)
                    drThuoc["DeletedBy"] = dlg.Thuoc.DeletedBy.ToString();

                drThuoc["Status"] = dlg.Thuoc.Status;
            }
        }

        private void OnDeleteThuoc()
        {
            List<string> deletedThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgThuoc.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedThuocList.Add(row["ThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ThuocBus.DeleteThuoc(deletedThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThuocBus.DeleteThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.DeleteThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thuốc cần xóa.", IconType.Information);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteThuoc();
        }

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditThuoc();
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
