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
    public partial class uXuatKhoCapCuuList : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private int _currentRowIndex = 0;
        #endregion

        #region Constructor
        public uXuatKhoCapCuuList()
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
            dgNhapKhoCapCuu.DataSource = null;
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayXuatKhoCapCuuListProc));
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

        private void OnDisplayXuatKhoCapCuuList()
        {
            Result result = XuatKhoCapCuuBus.GetXuatKhoCapCuuList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchXuatKhoCapCuu();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XuatKhoCapCuuBus.GetXuatKhoCapCuuList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XuatKhoCapCuuBus.GetXuatKhoCapCuuList"));
            }
        }

        private void UpdateChecked()
        {
            DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string xuatKhoCapCuuGUID1 = row1["XuatKhoCapCuuGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("XuatKhoCapCuuGUID='{0}'", xuatKhoCapCuuGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchXuatKhoCapCuu()
        {
            if (_dataSource == null) return;
            if (dgNhapKhoCapCuu.CurrentRow != null)
                _currentRowIndex = dgNhapKhoCapCuu.CurrentRow.Index;

            UpdateChecked();
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (raTenCapCuu.Checked)
            {
                if (txtTenCapCuu.Text.Trim() == string.Empty)
                {
                    DataTable dtSource = _dataSource as DataTable;
                    results = (from p in dtSource.AsEnumerable()
                               orderby p.Field<DateTime>("NgayXuat") descending
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
                           orderby p.Field<DateTime>("NgayXuat") descending
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
                           where p.Field<DateTime>("NgayXuat") != null &&
                           p.Field<DateTime>("NgayXuat") >= tuNgay &&
                           p.Field<DateTime>("NgayXuat") <= denNgay
                           orderby p.Field<DateTime>("NgayXuat") descending
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
            dlgAddXuatKhoCapCuu dlg = new dlgAddXuatKhoCapCuu();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["XuatKhoCapCuuGUID"] = dlg.XuatKhoCapCuu.XuatKhoCapCuuGUID.ToString();
                newRow["NgayXuat"] = dlg.XuatKhoCapCuu.NgayXuat;
                newRow["KhoCapCuuGUID"] = dlg.XuatKhoCapCuu.KhoCapCuuGUID;
                newRow["TenCapCuu"] = dlg.TenCapCuu;
                newRow["SoLuong"] = dlg.XuatKhoCapCuu.SoLuong;
                newRow["GiaXuat"] = dlg.XuatKhoCapCuu.GiaXuat;
                newRow["Note"] = dlg.XuatKhoCapCuu.Note;

                if (dlg.XuatKhoCapCuu.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.XuatKhoCapCuu.CreatedDate;

                if (dlg.XuatKhoCapCuu.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.XuatKhoCapCuu.CreatedBy.ToString();

                if (dlg.XuatKhoCapCuu.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.XuatKhoCapCuu.UpdatedDate;

                if (dlg.XuatKhoCapCuu.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.XuatKhoCapCuu.UpdatedBy.ToString();

                if (dlg.XuatKhoCapCuu.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.XuatKhoCapCuu.DeletedDate;

                if (dlg.XuatKhoCapCuu.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.XuatKhoCapCuu.DeletedBy.ToString();

                newRow["XuatKhoCapCuuStatus"] = dlg.XuatKhoCapCuu.Status;
                dt.Rows.Add(newRow);
                OnSearchXuatKhoCapCuu();
            }
        }

        private void OnDelete()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        deletedList.Add(row["XuatKhoCapCuuGUID"].ToString());
                        deletedRows.Add(row);
                    }
                }
            }

            if (deletedList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thông tin xuất kho cấp cứu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = XuatKhoCapCuuBus.DeleteXuatKhoCappCuu(deletedList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchXuatKhoCapCuu();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XuatKhoCapCuuBus.DeleteXuatKhoCappCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XuatKhoCapCuuBus.DeleteXuatKhoCappCuu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông tin xuất kho cấp cứu cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void raTenThuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtTenCapCuu.ReadOnly = !raTenCapCuu.Checked;
            dtpkTuNgay.Enabled = !raTenCapCuu.Checked;
            dtpkDenNgay.Enabled = !raTenCapCuu.Checked;

            OnSearchXuatKhoCapCuu();
        }

        private void raTuNgayDenNgay_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchXuatKhoCapCuu();
        }

        private void dtpkTuNgay_ValueChanged(object sender, EventArgs e)
        {
            OnSearchXuatKhoCapCuu();
        }

        private void dtpkDenNgay_ValueChanged(object sender, EventArgs e)
        {
            OnSearchXuatKhoCapCuu();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            OnSearchXuatKhoCapCuu();
        }
        #endregion

        #region Working Thread
        private void OnDisplayXuatKhoCapCuuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayXuatKhoCapCuuList();
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
