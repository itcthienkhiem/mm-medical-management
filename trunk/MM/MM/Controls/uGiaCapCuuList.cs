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
    public partial class uGiaCapCuuList : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private Dictionary<string, DataRow> _dictGiaCapCuu = null;
        #endregion

        #region Constructor
        public uGiaCapCuuList()
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
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
            }

            if (_dictGiaCapCuu != null)
            {
                _dictGiaCapCuu.Clear();
                _dictGiaCapCuu = null;
            }

            ClearDataSource();
        }

        public void ClearDataSource()
        {
            DataTable dt = dgGiaThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgGiaThuoc.DataSource = null;
            }
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
            Result result = GiaCapCuuBus.GetGiaCapCuuList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    _dataSource = result.QueryResult as DataTable;

                    if (_dictGiaCapCuu == null) _dictGiaCapCuu = new Dictionary<string, DataRow>();
                    foreach (DataRow row in _dataSource.Rows)
                    {
                        string giaCapCuuGUID = row["GiaCapCuuGUID"].ToString();
                        _dictGiaCapCuu.Add(giaCapCuuGUID, row);
                    }
                    OnSearchGiaThuoc();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaCapCuuBus.GetGiaCapCuuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GiaCapCuuBus.GetGiaCapCuuList"));
            }
        }

        //private void UpdateChecked()
        //{
        //    DataTable dt = dgGiaThuoc.DataSource as DataTable;
        //    if (dt == null) return;

        //    DataRow[] rows1 = dt.Select("Checked='True'");
        //    if (rows1 == null || rows1.Length <= 0) return;

        //    foreach (DataRow row1 in rows1)
        //    {
        //        string patientGUID1 = row1["GiaCapCuuGUID"].ToString();
        //        DataRow[] rows2 = _dataSource.Select(string.Format("GiaCapCuuGUID='{0}'", patientGUID1));
        //        if (rows2 == null || rows2.Length <= 0) continue;

        //        rows2[0]["Checked"] = row1["Checked"];
        //    }
        //}

        private void OnSearchGiaThuoc()
        {
            //UpdateChecked();
            ClearDataSource();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;
            if (txtTenThuoc.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("TenCapCuu") ascending, p.Field<DateTime>("NgayApDung") descending 
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgGiaThuoc.DataSource = newDataSource;
                if (dgGiaThuoc.RowCount > 0) dgGiaThuoc.Rows[0].Selected = true;
                return;
            }

            string str = txtTenThuoc.Text.ToLower();

            //FullName
            results = (from p in _dataSource.AsEnumerable()
                        where //(p.Field<string>("TenThuoc").ToLower().IndexOf(str) == 0 ||
                        //str.IndexOf(p.Field<string>("TenThuoc").ToLower()) == 0) &&
                        p.Field<string>("TenCapCuu").ToLower().IndexOf(str) == 0 &&
                        p.Field<string>("TenCapCuu") != null &&
                        p.Field<string>("TenCapCuu").Trim() != string.Empty
                        orderby p.Field<string>("TenCapCuu") ascending, p.Field<DateTime>("NgayApDung") descending 
                        select p).ToList<DataRow>();

            newDataSource = _dataSource.Clone();
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
            dlgAddGiaCapCuu dlg = new dlgAddGiaCapCuu();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = _dataSource;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["GiaCapCuuGUID"] = dlg.GiaCapCuu.GiaCapCuuGUID.ToString();
                newRow["KhoCapCuuGUID"] = dlg.GiaCapCuu.KhoCapCuuGUID.ToString();
                newRow["TenCapCuu"] = dlg.TenCapCuu;
                newRow["GiaBan"] = dlg.GiaCapCuu.GiaBan;
                newRow["NgayApDung"] = dlg.GiaCapCuu.NgayApDung;
                newRow["DonViTinh"] = dlg.DonViTinh;
                newRow["GiaCapCuuStatus"] = dlg.GiaCapCuu.Status;

                if (dlg.GiaCapCuu.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.GiaCapCuu.CreatedDate;

                if (dlg.GiaCapCuu.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.GiaCapCuu.CreatedBy.ToString();

                if (dlg.GiaCapCuu.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.GiaCapCuu.UpdatedDate;

                if (dlg.GiaCapCuu.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.GiaCapCuu.UpdatedBy.ToString();

                if (dlg.GiaCapCuu.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.GiaCapCuu.DeletedDate;

                if (dlg.GiaCapCuu.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.GiaCapCuu.DeletedBy.ToString();

                dt.Rows.Add(newRow);
                _dictGiaCapCuu.Add(dlg.GiaCapCuu.GiaCapCuuGUID.ToString(), newRow);
                OnSearchGiaThuoc();
                //SelectLastedRow();
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
            if (_dictGiaCapCuu == null) return null;
            return _dictGiaCapCuu[giaThuocGUID];
            //DataRow[] rows = _dataSource.Select(string.Format("GiaCapCuuGUID = '{0}'", giaThuocGUID));
            //if (rows == null || rows.Length <= 0) return null;

            //return rows[0];
        }

        private void OnEditGiaThuoc()
        {
            if (_dataSource == null) return;

            if (dgGiaThuoc.SelectedRows == null || dgGiaThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 giá cấp cứu.", IconType.Information);
                return;
            }

            string giaThuocGUID = (dgGiaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row["GiaCapCuuGUID"].ToString();
            DataRow drGiaThuoc = GetDataRow(giaThuocGUID);
            if (drGiaThuoc == null) return;
            dlgAddGiaCapCuu dlg = new dlgAddGiaCapCuu(drGiaThuoc);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drGiaThuoc["KhoCapCuuGUID"] = dlg.GiaCapCuu.KhoCapCuuGUID.ToString();
                drGiaThuoc["TenCapCuu"] = dlg.TenCapCuu;
                drGiaThuoc["GiaBan"] = dlg.GiaCapCuu.GiaBan;
                drGiaThuoc["NgayApDung"] = dlg.GiaCapCuu.NgayApDung;
                drGiaThuoc["DonViTinh"] = dlg.DonViTinh;
                drGiaThuoc["GiaCapCuuStatus"] = dlg.GiaCapCuu.Status;

                if (dlg.GiaCapCuu.CreatedDate.HasValue)
                    drGiaThuoc["CreatedDate"] = dlg.GiaCapCuu.CreatedDate;

                if (dlg.GiaCapCuu.CreatedBy.HasValue)
                    drGiaThuoc["CreatedBy"] = dlg.GiaCapCuu.CreatedBy.ToString();

                if (dlg.GiaCapCuu.UpdatedDate.HasValue)
                    drGiaThuoc["UpdatedDate"] = dlg.GiaCapCuu.UpdatedDate;

                if (dlg.GiaCapCuu.UpdatedBy.HasValue)
                    drGiaThuoc["UpdatedBy"] = dlg.GiaCapCuu.UpdatedBy.ToString();

                if (dlg.GiaCapCuu.DeletedDate.HasValue)
                    drGiaThuoc["DeletedDate"] = dlg.GiaCapCuu.DeletedDate;

                if (dlg.GiaCapCuu.DeletedBy.HasValue)
                    drGiaThuoc["DeletedBy"] = dlg.GiaCapCuu.DeletedBy.ToString();

                OnSearchGiaThuoc();
            }
        }

        private void OnDeleteGiaThuoc()
        {
            if (_dataSource == null) return;
            //UpdateChecked();
            List<string> deletedGiaThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            foreach (DataRow row in _dataSource.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string giaThuocGUID = row["GiaCapCuuGUID"].ToString();
                    deletedGiaThuocList.Add(giaThuocGUID);
                    deletedRows.Add(row);
                }
            }

            if (deletedGiaThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những giá cấp cứu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = GiaCapCuuBus.DeleteGiaCapCuu(deletedGiaThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dictGiaCapCuu.Remove(row["GiaCapCuuGUID"].ToString());
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchGiaThuoc();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaCapCuuBus.DeleteGiaCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaCapCuuBus.DeleteGiaCapCuu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá cấp cứu cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgGiaThuoc_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            if (_dataSource == null) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgGiaThuoc.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgGiaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string giaCapCuuGUID = row["GiaCapCuuGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            _dictGiaCapCuu[giaCapCuuGUID]["Checked"] = isChecked;
        }

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
                string giaCapCuuGUID = row["GiaCapCuuGUID"].ToString();
                _dictGiaCapCuu[giaCapCuuGUID]["Checked"] = chkChecked.Checked;
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
