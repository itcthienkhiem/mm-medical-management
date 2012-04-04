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
    public partial class uGiaVonDichVuList : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private bool _isFromDateToDate = true;
        private string _tenDichVu = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uGiaVonDichVuList()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            dtpkDenNgay.Value = DateTime.Now;
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
            btnExportExcel.Enabled = AllowExport;
        }

        public void ClearData()
        {
            dgGiaVonDichVu.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenDichVu = txtDichVu.Text;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayGiaVonDichVuListProc));
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

        private void OnDisplayGiaVonDichVuList()
        {
            Result result = GiaVonDichVuBus.GetGiaVonDichVuList(_isFromDateToDate, _fromDate, _toDate, _tenDichVu);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgGiaVonDichVu.DataSource = result.QueryResult as DataTable;
                    //OnSearchGiaVonDichVu();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaVonDichVuBus.GetGiaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GiaVonDichVuBus.GetGiaThuocList"));
            }
        }

        private void UpdateChecked()
        {
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["GiaVonDichVuGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("GiaVonDichVuGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }

            //DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            //if (dt == null) return;

            //foreach (DataRow row1 in dt.Rows)
            //{
            //    string giaVonGUID1 = row1["GiaVonDichVuGUID"].ToString();
            //    bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
            //    foreach (DataRow row2 in _dataSource.Rows)
            //    {
            //        string giaVonGUID2 = row2["GiaVonDichVuGUID"].ToString();
            //        bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

            //        if (giaVonGUID1 == giaVonGUID2)
            //        {
            //            row2["Checked"] = row1["Checked"];
            //            break;
            //        }
            //    }
            //}
        }

        private void OnSearchGiaVonDichVu()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            if (txtDichVu.Text.Trim() == string.Empty)
            {
                dgGiaVonDichVu.DataSource = _dataSource;
                if (dgGiaVonDichVu.RowCount > 0)
                {
                    dgGiaVonDichVu.CurrentCell = dgGiaVonDichVu[1, 0];
                    dgGiaVonDichVu.Rows[0].Selected = true;
                }
                return;
            }

            string str = txtDichVu.Text.ToLower();

            //Name
            List<DataRow> results = (from p in _dataSource.AsEnumerable()
                                     where //(p.Field<string>("Name").ToLower().IndexOf(str) >= 0 ||
                                     //str.IndexOf(p.Field<string>("Name").ToLower()) >= 0) &&
                                     p.Field<string>("Name").ToLower().IndexOf(str) >= 0 &&
                                     p.Field<string>("Name") != null &&
                                     p.Field<string>("Name").Trim() != string.Empty
                                     orderby p.Field<string>("Name") ascending, p.Field<DateTime>("NgayApDung") descending 
                                     select p).ToList<DataRow>();

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgGiaVonDichVu.DataSource = newDataSource;
                return;
            }

            dgGiaVonDichVu.DataSource = newDataSource;
        }

        private void OnAddGiaVonDichVu()
        {
            //if (_dataSource == null) return;
            dlgAddGiaVonDichVu dlg = new dlgAddGiaVonDichVu();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
                //DataTable dt = _dataSource;
                //if (dt == null) return;
                //DataRow newRow = dt.NewRow();
                //newRow["Checked"] = false;
                //newRow["GiaVonDichVuGUID"] = dlg.GiaVonDichVu.GiaVonDichVuGUID.ToString();
                //newRow["ServiceGUID"] = dlg.GiaVonDichVu.ServiceGUID.ToString();
                //newRow["Name"] = dlg.TenDichVu;
                //newRow["GiaVon"] = dlg.GiaVonDichVu.GiaVon;
                //newRow["NgayApDung"] = dlg.GiaVonDichVu.NgayApDung;
                //newRow["GiaVonDichVuStatus"] = dlg.GiaVonDichVu.Status;

                //if (dlg.GiaVonDichVu.CreatedDate.HasValue)
                //    newRow["CreatedDate"] = dlg.GiaVonDichVu.CreatedDate;

                //if (dlg.GiaVonDichVu.CreatedBy.HasValue)
                //    newRow["CreatedBy"] = dlg.GiaVonDichVu.CreatedBy.ToString();

                //if (dlg.GiaVonDichVu.UpdatedDate.HasValue)
                //    newRow["UpdatedDate"] = dlg.GiaVonDichVu.UpdatedDate;

                //if (dlg.GiaVonDichVu.UpdatedBy.HasValue)
                //    newRow["UpdatedBy"] = dlg.GiaVonDichVu.UpdatedBy.ToString();

                //if (dlg.GiaVonDichVu.DeletedDate.HasValue)
                //    newRow["DeletedDate"] = dlg.GiaVonDichVu.DeletedDate;

                //if (dlg.GiaVonDichVu.DeletedBy.HasValue)
                //    newRow["DeletedBy"] = dlg.GiaVonDichVu.DeletedBy.ToString();

                //dt.Rows.Add(newRow);
                //OnSearchGiaVonDichVu();
            }
        }

        private DataRow GetDataRow(string giaVonDichVuGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("GiaVonDichVuGUID = '{0}'", giaVonDichVuGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditGiaVonDichVu()
        {
            //if (_dataSource == null) return;

            if (dgGiaVonDichVu.SelectedRows == null || dgGiaVonDichVu.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 giá vốn dịch vụ.", IconType.Information);
                return;
            }

            //string giaVonDichGUID = (dgGiaVonDichVu.SelectedRows[0].DataBoundItem as DataRowView).Row["GiaVonDichVuGUID"].ToString();
            DataRow drGiaVonDichVu = (dgGiaVonDichVu.SelectedRows[0].DataBoundItem as DataRowView).Row;//GetDataRow(giaVonDichGUID);
            //if (drGiaVonDichVu == null) return;
            dlgAddGiaVonDichVu dlg = new dlgAddGiaVonDichVu(drGiaVonDichVu);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //drGiaVonDichVu["ServiceGUID"] = dlg.GiaVonDichVu.ServiceGUID.ToString();
                //drGiaVonDichVu["Name"] = dlg.TenDichVu;
                //drGiaVonDichVu["GiaVon"] = dlg.GiaVonDichVu.GiaVon;
                //drGiaVonDichVu["NgayApDung"] = dlg.GiaVonDichVu.NgayApDung;
                //drGiaVonDichVu["GiaVonDichVuStatus"] = dlg.GiaVonDichVu.Status;

                //if (dlg.GiaVonDichVu.CreatedDate.HasValue)
                //    drGiaVonDichVu["CreatedDate"] = dlg.GiaVonDichVu.CreatedDate;

                //if (dlg.GiaVonDichVu.CreatedBy.HasValue)
                //    drGiaVonDichVu["CreatedBy"] = dlg.GiaVonDichVu.CreatedBy.ToString();

                //if (dlg.GiaVonDichVu.UpdatedDate.HasValue)
                //    drGiaVonDichVu["UpdatedDate"] = dlg.GiaVonDichVu.UpdatedDate;

                //if (dlg.GiaVonDichVu.UpdatedBy.HasValue)
                //    drGiaVonDichVu["UpdatedBy"] = dlg.GiaVonDichVu.UpdatedBy.ToString();

                //if (dlg.GiaVonDichVu.DeletedDate.HasValue)
                //    drGiaVonDichVu["DeletedDate"] = dlg.GiaVonDichVu.DeletedDate;

                //if (dlg.GiaVonDichVu.DeletedBy.HasValue)
                //    drGiaVonDichVu["DeletedBy"] = dlg.GiaVonDichVu.DeletedBy.ToString();

                //OnSearchGiaVonDichVu();
                DisplayAsThread();
            }
        }

        private void OnDeleteGiaVonDichVu()
        {
            //if (_dataSource == null) return;
            //UpdateChecked();
            List<string> deletedGiaVonList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string giaThuocGUID = row["GiaVonDichVuGUID"].ToString();
                    deletedGiaVonList.Add(giaThuocGUID);
                    deletedRows.Add(row);
                }
            }

            if (deletedGiaVonList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những giá vốn dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = GiaVonDichVuBus.DeleteGiaVonDichVu(deletedGiaVonList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }

                        //OnSearchGiaVonDichVu();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaVonDichVuBus.DeleteGiaVonDichVu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaVonDichVuBus.DeleteGiaVonDichVu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá vốn dịch vụ cần xóa.", IconType.Information);
        }

        private void OnExportExcel()
        {
            //if (_dataSource == null) return;
            //UpdateChecked();
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ExportExcel.ExportGiaVonDichVuToExcel(dlg.FileName, checkedRows))
                        return;
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá vốn dịch vụ cần xuất Excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddGiaVonDichVu();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditGiaVonDichVu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteGiaVonDichVu();
        }

        private void dgGiaThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditGiaVonDichVu();
        }


        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            //OnSearchGiaVonDichVu();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void txtTenThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Down)
            //{
            //    dgGiaVonDichVu.Focus();

            //    if (dgGiaVonDichVu.SelectedRows != null && dgGiaVonDichVu.SelectedRows.Count > 0)
            //    {
            //        int index = dgGiaVonDichVu.SelectedRows[0].Index;
            //        if (index < dgGiaVonDichVu.RowCount - 1)
            //        {
            //            index++;
            //            dgGiaVonDichVu.CurrentCell = dgGiaVonDichVu[1, index];
            //            dgGiaVonDichVu.Rows[index].Selected = true;
            //        }
            //    }
            //}

            //if (e.KeyCode == Keys.Up)
            //{
            //    dgGiaVonDichVu.Focus();

            //    if (dgGiaVonDichVu.SelectedRows != null && dgGiaVonDichVu.SelectedRows.Count > 0)
            //    {
            //        int index = dgGiaVonDichVu.SelectedRows[0].Index;
            //        if (index > 0)
            //        {
            //            index--;
            //            dgGiaVonDichVu.CurrentCell = dgGiaVonDichVu[1, index];
            //            dgGiaVonDichVu.Rows[index].Selected = true;
            //        }
            //    }
            //}
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtDichVu.Enabled = !raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayGiaVonDichVuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayGiaVonDichVuList();
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
