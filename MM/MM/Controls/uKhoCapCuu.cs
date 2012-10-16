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
    public partial class uKhoCapCuu : uBase
    {
        #region Members
        private bool _isReport = false;
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uKhoCapCuu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsReport
        {
            get { return _isReport; }
            set
            {
                _isReport = value;
                panel1.Visible = !_isReport;
            }
        }

        public List<DataRow> CheckedRows
        {
            get
            {
                UpdateChecked();
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = _dataSource;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToBoolean(row["Checked"]))
                            checkedRows.Add(row);
                    }
                }

                return checkedRows;
            }
        }
        #endregion

        #region UI Command
        private void UpdateChecked()
        {
            DataTable dt = dgThuoc.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string khoCapCuuGUID1 = row1["KhoCapCuuGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnExportExcel.Enabled = AllowExport;
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
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKhoCapCuuProc));
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

        private void OnDisplayKhoCapCuu()
        {
            Result result = KhoCapCuuBus.GetDanhSachCapCuu();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchKhoCapCuu();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"));
            }
        }

        private void OnAdd()
        {
            if (_dataSource == null) return;
            dlgAddCapCuu dlg = new dlgAddCapCuu();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["KhoCapCuuGUID"] = dlg.KhoCapCuu.KhoCapCuuGUID.ToString();
                newRow["TenCapCuu"] = dlg.KhoCapCuu.TenCapCuu;
                newRow["DonViTinh"] = dlg.KhoCapCuu.DonViTinh;
                newRow["Note"] = dlg.KhoCapCuu.Note;

                if (dlg.KhoCapCuu.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.KhoCapCuu.CreatedDate;

                if (dlg.KhoCapCuu.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.KhoCapCuu.CreatedBy.ToString();

                if (dlg.KhoCapCuu.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.KhoCapCuu.UpdatedDate;

                if (dlg.KhoCapCuu.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.KhoCapCuu.UpdatedBy.ToString();

                if (dlg.KhoCapCuu.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.KhoCapCuu.DeletedDate;

                if (dlg.KhoCapCuu.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.KhoCapCuu.DeletedBy.ToString();

                newRow["Status"] = dlg.KhoCapCuu.Status;
                dt.Rows.Add(newRow);
                OnSearchKhoCapCuu();
            }
        }

        private DataRow GetDataRow(string thuocGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("KhoCapCuuGUID = '{0}'", thuocGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void SelectLastedRow()
        {
            dgThuoc.CurrentCell = dgThuoc[1, dgThuoc.RowCount - 1];
            dgThuoc.Rows[dgThuoc.RowCount - 1].Selected = true;
        }

        private void OnEdit()
        {
            if (_dataSource == null) return;
            if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thông tin cấp cứu.", IconType.Information);
                return;
            }

            string khoCapCuuGUID = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row["KhoCapCuuGUID"].ToString();
            DataRow drCapCuu = GetDataRow(khoCapCuuGUID);
            if (drCapCuu == null) return;
            dlgAddCapCuu dlg = new dlgAddCapCuu(drCapCuu);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drCapCuu["TenCapCuu"] = dlg.KhoCapCuu.TenCapCuu;
                drCapCuu["DonViTinh"] = dlg.KhoCapCuu.DonViTinh;
                drCapCuu["Note"] = dlg.KhoCapCuu.Note;

                if (dlg.KhoCapCuu.CreatedDate.HasValue)
                    drCapCuu["CreatedDate"] = dlg.KhoCapCuu.CreatedDate;

                if (dlg.KhoCapCuu.CreatedBy.HasValue)
                    drCapCuu["CreatedBy"] = dlg.KhoCapCuu.CreatedBy.ToString();

                if (dlg.KhoCapCuu.UpdatedDate.HasValue)
                    drCapCuu["UpdatedDate"] = dlg.KhoCapCuu.UpdatedDate;

                if (dlg.KhoCapCuu.UpdatedBy.HasValue)
                    drCapCuu["UpdatedBy"] = dlg.KhoCapCuu.UpdatedBy.ToString();

                if (dlg.KhoCapCuu.DeletedDate.HasValue)
                    drCapCuu["DeletedDate"] = dlg.KhoCapCuu.DeletedDate;

                if (dlg.KhoCapCuu.DeletedBy.HasValue)
                    drCapCuu["DeletedBy"] = dlg.KhoCapCuu.DeletedBy.ToString();

                drCapCuu["Status"] = dlg.KhoCapCuu.Status;

                OnSearchKhoCapCuu();
            }
        }

        private void OnDelete()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = _dataSource;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedThuocList.Add(row["KhoCapCuuGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thông tin cấp cứu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KhoCapCuuBus.DeleteThongTinCapCuu(deletedThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchKhoCapCuu();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KhoCapCuuBus.DeleteThongTinCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.DeleteThongTinCapCuu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông tin cấp cứu cần xóa.", IconType.Information);
        }

        private void OnSearchKhoCapCuu()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtTenCapCuu.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("TenCapCuu")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgThuoc.DataSource = newDataSource;
                if (dgThuoc.RowCount > 0) dgThuoc.Rows[0].Selected = true;
                return;
            }

            string str = txtTenCapCuu.Text.ToLower();

            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("TenCapCuu") != null &&
                           p.Field<string>("TenCapCuu").Trim() != string.Empty &&
                           p.Field<string>("TenCapCuu").ToLower().IndexOf(str) >= 0
                       orderby p.Field<string>("TenCapCuu")
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgThuoc.DataSource = newDataSource;
                return;
            }

            dgThuoc.DataSource = newDataSource;
        }

        private void OnExportExcel()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = _dataSource;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    checkedRows.Add(row);
            }

            if (checkedRows.Count <= 0)
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông tin cấp cứu cần xuất excel.", IconType.Information);
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachKhoCapCuuToExcel(dlg.FileName, checkedRows);
                }
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

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (_isReport) return;
            if (!AllowEdit) return;
            OnEdit();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            OnSearchKhoCapCuu();
        }

        private void txtTenThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgThuoc.Focus();

                if (dgThuoc.SelectedRows != null && dgThuoc.SelectedRows.Count > 0)
                {
                    int index = dgThuoc.SelectedRows[0].Index;
                    if (index < dgThuoc.RowCount - 1)
                    {
                        index++;
                        dgThuoc.CurrentCell = dgThuoc[1, index];
                        dgThuoc.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgThuoc.Focus();

                if (dgThuoc.SelectedRows != null && dgThuoc.SelectedRows.Count > 0)
                {
                    int index = dgThuoc.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgThuoc.CurrentCell = dgThuoc[1, index];
                        dgThuoc.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayKhoCapCuuProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKhoCapCuu();
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
