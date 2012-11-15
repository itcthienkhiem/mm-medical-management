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
    public partial class uNhapKhoCapCuuList : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private int _currentRowIndex = 0;
        private Dictionary<string, DataRow> _dictNhapKhoCapCuu = null;
        #endregion

        #region Constructor
        public uNhapKhoCapCuuList()
        {
            InitializeComponent();

            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
            }

            if (_dictNhapKhoCapCuu != null)
            {
                _dictNhapKhoCapCuu.Clear();
                _dictNhapKhoCapCuu = null;
            }

            ClearDataSource();
        }

        private void ClearDataSource()
        {
            DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgNhapKhoCapCuu.DataSource = null;
            }
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            //btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayNhapKhoCapCuuListProc));
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

        private void OnDisplayNhapKhoCapCuuList()
        {
            Result result = NhapKhoCapCuuBus.GetNhapKhoCapCuuList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    _dataSource = result.QueryResult as DataTable;

                    if (_dictNhapKhoCapCuu == null) _dictNhapKhoCapCuu = new Dictionary<string, DataRow>();
                    foreach (DataRow row in _dataSource.Rows)
                    {
                        string nhapKhoCapCuuGUID = row["NhapKhoCapCuuGUID"].ToString();
                        _dictNhapKhoCapCuu.Add(nhapKhoCapCuuGUID, row);
                    }

                    OnSearchNhapKhoCapCuu();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuuList"));
            }
        }

        //private void UpdateChecked()
        //{
        //    DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
        //    if (dt == null) return;

        //    DataRow[] rows1 = dt.Select("Checked='True'");
        //    if (rows1 == null || rows1.Length <= 0) return;

        //    foreach (DataRow row1 in rows1)
        //    {
        //        string nhapKhoCapCuuGUID1 = row1["NhapKhoCapCuuGUID"].ToString();
        //        DataRow[] rows2 = _dataSource.Select(string.Format("NhapKhoCapCuuGUID='{0}'", nhapKhoCapCuuGUID1));
        //        if (rows2 == null || rows2.Length <= 0) continue;

        //        rows2[0]["Checked"] = row1["Checked"];
        //    }
        //}

        private void OnSearchNhapKhoCapCuu()
        {
            if (_dataSource == null) return;
            if (dgNhapKhoCapCuu.CurrentRow != null)
                _currentRowIndex = dgNhapKhoCapCuu.CurrentRow.Index;

            //UpdateChecked();
            ClearDataSource();
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (raTenCapCuu.Checked)
            {
                if (txtTenCapCuu.Text.Trim() == string.Empty)
                {
                    DataTable dtSource = _dataSource as DataTable;
                    results = (from p in dtSource.AsEnumerable()
                               orderby p.Field<DateTime>("NgayNhap") descending
                               select p).ToList<DataRow>();

                    newDataSource = dtSource.Clone();

                    foreach (DataRow row in results)
                        newDataSource.ImportRow(row);

                    dgNhapKhoCapCuu.DataSource = newDataSource;

                    if (_currentRowIndex < newDataSource.Rows.Count)
                    {
                        dgNhapKhoCapCuu.CurrentCell = dgNhapKhoCapCuu[0, _currentRowIndex];
                        dgNhapKhoCapCuu.Rows[_currentRowIndex].Selected = true;
                    }

                    return;
                }

                string str = txtTenCapCuu.Text.ToLower();
                DataTable dt = _dataSource as DataTable;
                newDataSource = dt.Clone();

                //Ten Thuoc
                results = (from p in dt.AsEnumerable()
                           where p.Field<string>("TenCapCuu") != null &&
                           p.Field<string>("TenCapCuu").Trim() != string.Empty &&
                           (p.Field<string>("TenCapCuu").ToLower().IndexOf(str) == 0 ||
                           str.IndexOf(p.Field<string>("TenCapCuu").ToLower()) == 0)
                           orderby p.Field<DateTime>("NgayNhap") descending
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgNhapKhoCapCuu.DataSource = newDataSource;

                    if (_currentRowIndex < newDataSource.Rows.Count)
                    {
                        dgNhapKhoCapCuu.CurrentCell = dgNhapKhoCapCuu[0, _currentRowIndex];
                        dgNhapKhoCapCuu.Rows[_currentRowIndex].Selected = true;
                    }

                    return;
                }
            }
            else
            {
                DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

                DataTable dt = _dataSource as DataTable;
                newDataSource = dt.Clone();

                results = (from p in dt.AsEnumerable()
                           where p.Field<DateTime>("NgayNhap") != null &&
                           p.Field<DateTime>("NgayNhap") >= tuNgay &&
                           p.Field<DateTime>("NgayNhap") <= denNgay
                           orderby p.Field<DateTime>("NgayNhap") descending
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgNhapKhoCapCuu.DataSource = newDataSource;

                    if (_currentRowIndex < newDataSource.Rows.Count)
                    {
                        dgNhapKhoCapCuu.CurrentCell = dgNhapKhoCapCuu[0, _currentRowIndex];
                        dgNhapKhoCapCuu.Rows[_currentRowIndex].Selected = true;
                    }

                    return;
                }
            }

            dgNhapKhoCapCuu.DataSource = newDataSource;

            if (_currentRowIndex < newDataSource.Rows.Count)
            {
                dgNhapKhoCapCuu.CurrentCell = dgNhapKhoCapCuu[0, _currentRowIndex];
                dgNhapKhoCapCuu.Rows[_currentRowIndex].Selected = true;
            }
        }

        private void SelectLastedRow()
        {
            dgNhapKhoCapCuu.CurrentCell = dgNhapKhoCapCuu[1, dgNhapKhoCapCuu.RowCount - 1];
            dgNhapKhoCapCuu.Rows[dgNhapKhoCapCuu.RowCount - 1].Selected = true;
        }

        private void OnAdd()
        {
            dlgAddNhapKhoCapCuu dlg = new dlgAddNhapKhoCapCuu();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["NhapKhoCapCuuGUID"] = dlg.NhapKhoCapCuu.NhapKhoCapCuuGUID.ToString();
                newRow["NgayNhap"] = dlg.NhapKhoCapCuu.NgayNhap;
                newRow["KhoCapCuuGUID"] = dlg.NhapKhoCapCuu.KhoCapCuuGUID;
                newRow["TenCapCuu"] = dlg.TenCapCuu;
                newRow["SoDangKy"] = dlg.NhapKhoCapCuu.SoDangKy;
                newRow["HangSanXuat"] = dlg.NhapKhoCapCuu.HangSanXuat;
                newRow["NgaySanXuat"] = dlg.NhapKhoCapCuu.NgaySanXuat;
                newRow["NgayHetHan"] = dlg.NhapKhoCapCuu.NgayHetHan;
                newRow["NhaPhanPhoi"] = dlg.NhapKhoCapCuu.NhaPhanPhoi;
                newRow["SoLuongNhap"] = dlg.NhapKhoCapCuu.SoLuongNhap;
                newRow["DonViTinhNhap"] = dlg.NhapKhoCapCuu.DonViTinhNhap;
                newRow["GiaNhap"] = dlg.NhapKhoCapCuu.GiaNhap;
                newRow["SoLuongQuiDoi"] = dlg.NhapKhoCapCuu.SoLuongQuiDoi;
                newRow["DonViTinhQuiDoi"] = dlg.NhapKhoCapCuu.DonViTinhQuiDoi;
                newRow["GiaNhapQuiDoi"] = dlg.NhapKhoCapCuu.GiaNhapQuiDoi;
                newRow["SoLuongXuat"] = 0;

                if (dlg.NhapKhoCapCuu.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.NhapKhoCapCuu.CreatedDate;

                if (dlg.NhapKhoCapCuu.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.NhapKhoCapCuu.CreatedBy.ToString();

                if (dlg.NhapKhoCapCuu.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.NhapKhoCapCuu.UpdatedDate;

                if (dlg.NhapKhoCapCuu.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.NhapKhoCapCuu.UpdatedBy.ToString();

                if (dlg.NhapKhoCapCuu.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.NhapKhoCapCuu.DeletedDate;

                if (dlg.NhapKhoCapCuu.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.NhapKhoCapCuu.DeletedBy.ToString();

                newRow["NhapKhoCapCuuStatus"] = dlg.NhapKhoCapCuu.Status;
                dt.Rows.Add(newRow);
                _dictNhapKhoCapCuu.Add(dlg.NhapKhoCapCuu.NhapKhoCapCuuGUID.ToString(), newRow);
                OnSearchNhapKhoCapCuu();
            }
        }

        private DataRow GetDataRow(string loThuocGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            if (_dictNhapKhoCapCuu == null) return null;
            return _dictNhapKhoCapCuu[loThuocGUID];
            //DataRow[] rows = _dataSource.Select(string.Format("NhapKhoCapCuuGUID = '{0}'", loThuocGUID));
            //if (rows == null || rows.Length <= 0) return null;

            //return rows[0];
        }

        private void OnEdit()
        {
            if (dgNhapKhoCapCuu.SelectedRows == null || dgNhapKhoCapCuu.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thông tin cấp cứu.", IconType.Information);
                return;
            }

            string nhapKhoCapCuuGUID = (dgNhapKhoCapCuu.SelectedRows[0].DataBoundItem as DataRowView).Row["NhapKhoCapCuuGUID"].ToString();
            DataRow drNhapKhoCapCuu = GetDataRow(nhapKhoCapCuuGUID);
            if (drNhapKhoCapCuu == null) return;
            dlgAddNhapKhoCapCuu dlg = new dlgAddNhapKhoCapCuu(drNhapKhoCapCuu, AllowEdit);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                drNhapKhoCapCuu["NgayNhap"] = dlg.NhapKhoCapCuu.NgayNhap;
                drNhapKhoCapCuu["KhoCapCuuGUID"] = dlg.NhapKhoCapCuu.KhoCapCuuGUID;
                drNhapKhoCapCuu["TenCapCuu"] = dlg.TenCapCuu;
                drNhapKhoCapCuu["SoDangKy"] = dlg.NhapKhoCapCuu.SoDangKy;
                drNhapKhoCapCuu["HangSanXuat"] = dlg.NhapKhoCapCuu.HangSanXuat;
                drNhapKhoCapCuu["NgaySanXuat"] = dlg.NhapKhoCapCuu.NgaySanXuat;
                drNhapKhoCapCuu["NgayHetHan"] = dlg.NhapKhoCapCuu.NgayHetHan;
                drNhapKhoCapCuu["NhaPhanPhoi"] = dlg.NhapKhoCapCuu.NhaPhanPhoi;
                drNhapKhoCapCuu["SoLuongNhap"] = dlg.NhapKhoCapCuu.SoLuongNhap;
                drNhapKhoCapCuu["DonViTinhNhap"] = dlg.NhapKhoCapCuu.DonViTinhNhap;
                drNhapKhoCapCuu["GiaNhap"] = dlg.NhapKhoCapCuu.GiaNhap;
                drNhapKhoCapCuu["SoLuongQuiDoi"] = dlg.NhapKhoCapCuu.SoLuongQuiDoi;
                drNhapKhoCapCuu["DonViTinhQuiDoi"] = dlg.NhapKhoCapCuu.DonViTinhQuiDoi;
                drNhapKhoCapCuu["GiaNhapQuiDoi"] = dlg.NhapKhoCapCuu.GiaNhapQuiDoi;

                if (dlg.NhapKhoCapCuu.CreatedDate.HasValue)
                    drNhapKhoCapCuu["CreatedDate"] = dlg.NhapKhoCapCuu.CreatedDate;

                if (dlg.NhapKhoCapCuu.CreatedBy.HasValue)
                    drNhapKhoCapCuu["CreatedBy"] = dlg.NhapKhoCapCuu.CreatedBy.ToString();

                if (dlg.NhapKhoCapCuu.UpdatedDate.HasValue)
                    drNhapKhoCapCuu["UpdatedDate"] = dlg.NhapKhoCapCuu.UpdatedDate;

                if (dlg.NhapKhoCapCuu.UpdatedBy.HasValue)
                    drNhapKhoCapCuu["UpdatedBy"] = dlg.NhapKhoCapCuu.UpdatedBy.ToString();

                if (dlg.NhapKhoCapCuu.DeletedDate.HasValue)
                    drNhapKhoCapCuu["DeletedDate"] = dlg.NhapKhoCapCuu.DeletedDate;

                if (dlg.NhapKhoCapCuu.DeletedBy.HasValue)
                    drNhapKhoCapCuu["DeletedBy"] = dlg.NhapKhoCapCuu.DeletedBy.ToString();

                drNhapKhoCapCuu["NhapKhoCapCuuStatus"] = dlg.NhapKhoCapCuu.Status;
                OnSearchNhapKhoCapCuu();
            }
        }

        private void OnDelete()
        {
            if (_dataSource == null) return;
            //UpdateChecked();
            List<string> deletedList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        deletedList.Add(row["NhapKhoCapCuuGUID"].ToString());
                        deletedRows.Add(row);
                    }
                }
            }


            if (deletedList.Count > 0)
            {
                foreach (string key in deletedList)
                {
                    Result rs = NhapKhoCapCuuBus.GetNhapKhoCapCuu(key);
                    if (!rs.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, rs.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(rs.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuu"));
                        return;
                    }

                    NhapKhoCapCuuView nkcc = rs.QueryResult as NhapKhoCapCuuView;
                    if (nkcc.SoLuongXuat > 0)
                    {
                        MsgBox.Show(Application.ProductName, string.Format("Cấp cứu: '{0}' này đã xuất rồi không thể xóa.", nkcc.TenCapCuu),
                            IconType.Information);
                        return;
                    }
                }

                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thông tin nhập kho cấp cứu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = NhapKhoCapCuuBus.DeleteNhapKhoCappCuu(deletedList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dictNhapKhoCapCuu.Remove(row["NhapKhoCapCuuGUID"].ToString());
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchNhapKhoCapCuu();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhapKhoCapCuuBus.DeleteNhapKhoCappCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.DeleteNhapKhoCappCuu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông tin nhập kho cấp cứu cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgNhapKhoCapCuu_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            if (_dataSource == null) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgNhapKhoCapCuu.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgNhapKhoCapCuu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string nhapKhoCapCuuGUID = row["NhapKhoCapCuuGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            _dictNhapKhoCapCuu[nhapKhoCapCuuGUID]["Checked"] = isChecked;
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string nhapKhoCapCuuGUID = row["NhapKhoCapCuuGUID"].ToString();
                _dictNhapKhoCapCuu[nhapKhoCapCuuGUID]["Checked"] = chkChecked.Checked;
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

        private void dgLoThuoc_DoubleClick(object sender, EventArgs e)
        {
            //if (!AllowEdit) return;
            OnEdit();
        }

        private void raTenThuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtTenCapCuu.ReadOnly = !raTenCapCuu.Checked;
            dtpkTuNgay.Enabled = !raTenCapCuu.Checked;
            dtpkDenNgay.Enabled = !raTenCapCuu.Checked;

            OnSearchNhapKhoCapCuu();
        }

        private void raTuNgayDenNgay_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchNhapKhoCapCuu();
        }

        private void dtpkTuNgay_ValueChanged(object sender, EventArgs e)
        {
            OnSearchNhapKhoCapCuu();
        }

        private void dtpkDenNgay_ValueChanged(object sender, EventArgs e)
        {
            OnSearchNhapKhoCapCuu();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            OnSearchNhapKhoCapCuu();
        }
        #endregion

        #region Working Thread
        private void OnDisplayNhapKhoCapCuuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayNhapKhoCapCuuList();
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
