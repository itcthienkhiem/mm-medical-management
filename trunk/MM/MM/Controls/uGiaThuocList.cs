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
    public partial class uGiaThuocList : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uGiaThuocList()
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
            dgGiaThuoc.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayGiaThuocListProc));
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

        private void OnDisplayGiaThuocList()
        {
            Result result = GiaThuocBus.GetGiaThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchGiaThuoc();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaThuocBus.GetGiaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GiaThuocBus.GetGiaThuocList"));
            }
        }

        private void UpdateChecked()
        {
            DataTable dt = dgGiaThuoc.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row1 in dt.Rows)
            {
                string giaThuocGUID1 = row1["GiaThuocGUID"].ToString();
                bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
                foreach (DataRow row2 in _dataSource.Rows)
                {
                    string giaThuocGUID2 = row2["GiaThuocGUID"].ToString();
                    bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

                    if (giaThuocGUID1 == giaThuocGUID2)
                    {
                        row2["Checked"] = row1["Checked"];
                        break;
                    }
                }
            }
        }

        private void OnSearchGiaThuoc()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            if (txtTenThuoc.Text.Trim() == string.Empty)
            {
                dgGiaThuoc.DataSource = _dataSource;
                if (dgGiaThuoc.RowCount > 0)
                {
                    dgGiaThuoc.CurrentCell = dgGiaThuoc[1, 0];
                    dgGiaThuoc.Rows[0].Selected = true;
                }
                return;
            }

            string str = txtTenThuoc.Text.ToLower();

            //FullName
            List<DataRow> results = (from p in _dataSource.AsEnumerable()
                                     where (p.Field<string>("TenThuoc").ToLower().IndexOf(str) >= 0 ||
                                     str.IndexOf(p.Field<string>("TenThuoc").ToLower()) >= 0) &&
                                     p.Field<string>("TenThuoc") != null &&
                                     p.Field<string>("TenThuoc").Trim() != string.Empty
                                     orderby p.Field<string>("TenThuoc") ascending, p.Field<DateTime>("NgayApDung") descending 
                                     select p).ToList<DataRow>();

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgGiaThuoc.DataSource = newDataSource;
                return;
            }

            dgGiaThuoc.DataSource = newDataSource;
        }

        private void OnAddGiaThuoc()
        {
            if (_dataSource == null) return;
            dlgAddGiaThuoc dlg = new dlgAddGiaThuoc();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = _dataSource;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["GiaThuocGUID"] = dlg.GiaThuoc.GiaThuocGUID.ToString();
                newRow["ThuocGUID"] = dlg.GiaThuoc.ThuocGUID.ToString();
                newRow["TenThuoc"] = dlg.TenThuoc;
                newRow["GiaBan"] = dlg.GiaThuoc.GiaBan;
                newRow["NgayApDung"] = dlg.GiaThuoc.NgayApDung;
                newRow["DonViTinh"] = dlg.DonViTinh;
                newRow["GiaThuocStatus"] = dlg.GiaThuoc.Status;
                
                if (dlg.GiaThuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.GiaThuoc.CreatedDate;

                if (dlg.GiaThuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.GiaThuoc.CreatedBy.ToString();

                if (dlg.GiaThuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.GiaThuoc.UpdatedDate;

                if (dlg.GiaThuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.GiaThuoc.UpdatedBy.ToString();

                if (dlg.GiaThuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.GiaThuoc.DeletedDate;

                if (dlg.GiaThuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.GiaThuoc.DeletedBy.ToString();

                dt.Rows.Add(newRow);
                OnSearchGiaThuoc();
                SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgGiaThuoc.CurrentCell = dgGiaThuoc[1, dgGiaThuoc.RowCount - 1];
            dgGiaThuoc.Rows[dgGiaThuoc.RowCount - 1].Selected = true;
        }

        private DataRow GetDataRow(string giaThuocGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("GiaThuocGUID = '{0}'", giaThuocGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditGiaThuoc()
        {
            if (_dataSource == null) return;

            if (dgGiaThuoc.SelectedRows == null || dgGiaThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 giá thuốc.", IconType.Information);
                return;
            }

            string giaThuocGUID = (dgGiaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row["GiaThuocGUID"].ToString();
            DataRow drGiaThuoc = GetDataRow(giaThuocGUID);
            if (drGiaThuoc == null) return;
            dlgAddGiaThuoc dlg = new dlgAddGiaThuoc(drGiaThuoc);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drGiaThuoc["ThuocGUID"] = dlg.GiaThuoc.ThuocGUID.ToString();
                drGiaThuoc["TenThuoc"] = dlg.TenThuoc;
                drGiaThuoc["GiaBan"] = dlg.GiaThuoc.GiaBan;
                drGiaThuoc["NgayApDung"] = dlg.GiaThuoc.NgayApDung;
                drGiaThuoc["DonViTinh"] = dlg.DonViTinh;
                drGiaThuoc["GiaThuocStatus"] = dlg.GiaThuoc.Status;

                if (dlg.GiaThuoc.CreatedDate.HasValue)
                    drGiaThuoc["CreatedDate"] = dlg.GiaThuoc.CreatedDate;

                if (dlg.GiaThuoc.CreatedBy.HasValue)
                    drGiaThuoc["CreatedBy"] = dlg.GiaThuoc.CreatedBy.ToString();

                if (dlg.GiaThuoc.UpdatedDate.HasValue)
                    drGiaThuoc["UpdatedDate"] = dlg.GiaThuoc.UpdatedDate;

                if (dlg.GiaThuoc.UpdatedBy.HasValue)
                    drGiaThuoc["UpdatedBy"] = dlg.GiaThuoc.UpdatedBy.ToString();

                if (dlg.GiaThuoc.DeletedDate.HasValue)
                    drGiaThuoc["DeletedDate"] = dlg.GiaThuoc.DeletedDate;

                if (dlg.GiaThuoc.DeletedBy.HasValue)
                    drGiaThuoc["DeletedBy"] = dlg.GiaThuoc.DeletedBy.ToString();

                OnSearchGiaThuoc();
            }
        }

        private void OnDeleteGiaThuoc()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedGiaThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            List<DataRow> deletedRows2 = new List<DataRow>();
            foreach (DataRow row in _dataSource.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string giaThuocGUID = row["GiaThuocGUID"].ToString();
                    deletedGiaThuocList.Add(giaThuocGUID);
                    deletedRows.Add(row);
                    DataRow r = GetDataRow(giaThuocGUID);
                    if (r != null) deletedRows2.Add(r);
                }
            }

            if (deletedGiaThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những giá thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = GiaThuocBus.DeleteGiaThuoc(deletedGiaThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        try
                        {
                            DataTable dt = dgGiaThuoc.DataSource as DataTable;
                            foreach (DataRow row in deletedRows2)
                            {
                                if (row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted)
                                    dt.Rows.Remove(row);
                            }
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                            Utility.WriteToTraceLog(ex.Message);
                        }

                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaThuocBus.DeleteGiaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaThuocBus.DeleteGiaThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddGiaThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditGiaThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteGiaThuoc();
        }

        private void dgGiaThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditGiaThuoc();
        }


        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            OnSearchGiaThuoc();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgGiaThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void txtTenThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgGiaThuoc.Focus();

                if (dgGiaThuoc.SelectedRows != null && dgGiaThuoc.SelectedRows.Count > 0)
                {
                    int index = dgGiaThuoc.SelectedRows[0].Index;
                    if (index < dgGiaThuoc.RowCount - 1)
                    {
                        index++;
                        dgGiaThuoc.CurrentCell = dgGiaThuoc[1, index];
                        dgGiaThuoc.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgGiaThuoc.Focus();

                if (dgGiaThuoc.SelectedRows != null && dgGiaThuoc.SelectedRows.Count > 0)
                {
                    int index = dgGiaThuoc.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgGiaThuoc.CurrentCell = dgGiaThuoc[1, index];
                        dgGiaThuoc.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayGiaThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayGiaThuocList();
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
