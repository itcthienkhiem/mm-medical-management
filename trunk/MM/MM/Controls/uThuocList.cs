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
    public partial class uThuocList : uBase
    {
        #region Members
        private bool _isReport = false;
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uThuocList()
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
                string thuocGUID1 = row1["ThuocGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("ThuocGUID='{0}'", thuocGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            //btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnExportExcel.Enabled = AllowExport;
        }

        public void ClearData()
        {
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
            }

            ClearDataSource();
        }

        private void ClearDataSource()
        {
            DataTable dt = dgThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

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
                    ClearData();
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchThuoc();
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
            if (_dataSource == null) return;
            dlgAddThuoc dlg = new dlgAddThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;
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
                //SelectLastedRow();
                OnSearchThuoc();
            }
        }

        private DataRow GetDataRow(string thuocGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("ThuocGUID = '{0}'", thuocGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void SelectLastedRow()
        {
            dgThuoc.CurrentCell = dgThuoc[1, dgThuoc.RowCount - 1];
            dgThuoc.Rows[dgThuoc.RowCount - 1].Selected = true;
        }

        private void OnEditThuoc()
        {
            if (_dataSource == null) return;
            if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thuốc.", IconType.Information);
                return;
            }

            string thuocGUID = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row["ThuocGUID"].ToString();
            DataRow drThuoc = GetDataRow(thuocGUID);
            if (drThuoc == null) return;
            dlgAddThuoc dlg = new dlgAddThuoc(drThuoc, AllowEdit);
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

                OnSearchThuoc();
            }
        }

        private void OnDeleteThuoc()
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
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchThuoc();
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

        private void OnSearchThuoc()
        {
            UpdateChecked();
            ClearDataSource();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtTenThuoc.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("TenThuoc")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgThuoc.DataSource = newDataSource;
                if (dgThuoc.RowCount > 0) dgThuoc.Rows[0].Selected = true;
                return;
            }

            string str = txtTenThuoc.Text.ToLower();

            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("TenThuoc") != null &&
                           p.Field<string>("TenThuoc").Trim() != string.Empty &&
                           p.Field<string>("TenThuoc").ToLower().IndexOf(str) >= 0
                       //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                       //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                       orderby p.Field<string>("TenThuoc")
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
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thuốc cần xuất excel.", IconType.Information);
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachThuocToExcel(dlg.FileName, checkedRows);
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
            if (_isReport) return;
            //if (!AllowEdit) return;
            OnEditThuoc();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            OnSearchThuoc();
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
