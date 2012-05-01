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
    public partial class uXetNghiemTay : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uXetNghiemTay()
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
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayXetNghiemListProc));
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

        private void OnDisplayXetNghiemList()
        {
            Result result = XetNghiemTayBus.GetXetNghiemList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchXetNghiem();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiemTayBus.GetXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetXetNghiemList"));
            }
        }

        private void UpdateChecked()
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string serviceGUID1 = row1["XetNghiem_ManualGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("XetNghiem_ManualGUID='{0}'", serviceGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchXetNghiem()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtXetNghiem.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("Fullname")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgXetNghiem.DataSource = newDataSource;
                if (dgXetNghiem.RowCount > 0) dgXetNghiem.Rows[0].Selected = true;
                return;
            }

            string str = txtXetNghiem.Text.ToLower();
            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("Fullname") != null &&
                           p.Field<string>("Fullname").Trim() != string.Empty &&
                           p.Field<string>("Fullname").ToLower().IndexOf(str) >= 0
                       orderby p.Field<string>("Fullname")
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgXetNghiem.DataSource = newDataSource;
                return;
            }

            dgXetNghiem.DataSource = newDataSource;
        }

        private string GetLoaiXetNghiem(string type)
        {
            switch (type)
            {
                case "Biochemistry":
                    return "Sinh hóa";
                case "Urine":
                    return "Nước tiểu";
                case "Electrolytes":
                    return "Ion đồ";
                case "Haematology":
                    return "Huyết học";
            }

            return string.Empty;
        }

        private void OnAdd()
        {
            if (_dataSource == null) return;

            dlgAddXetNghiemTay dlg = new dlgAddXetNghiemTay();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataRow newRow = _dataSource.NewRow();
                newRow["Checked"] = false;
                newRow["XetNghiem_ManualGUID"] = dlg.XetNghiem.XetNghiem_ManualGUID.ToString();
                newRow["Fullname"] = dlg.XetNghiem.Fullname;
                newRow["TenXetNghiem"] = dlg.XetNghiem.TenXetNghiem;
                newRow["Type"] = dlg.XetNghiem.Type;
                newRow["LoaiXN"] = GetLoaiXetNghiem(dlg.XetNghiem.Type);

                if (dlg.XetNghiem.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.XetNghiem.CreatedDate;

                if (dlg.XetNghiem.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.XetNghiem.CreatedBy.ToString();

                if (dlg.XetNghiem.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.XetNghiem.UpdatedDate;

                if (dlg.XetNghiem.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.XetNghiem.UpdatedBy.ToString();

                if (dlg.XetNghiem.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.XetNghiem.DeletedDate;

                if (dlg.XetNghiem.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.XetNghiem.DeletedBy.ToString();

                newRow["Status"] = dlg.XetNghiem.Status;
                _dataSource.Rows.Add(newRow);
                OnSearchXetNghiem();
            }
        }

        private void OnEdit()
        {
            if (_dataSource == null) return;

            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 xét nghiệm.", IconType.Information);
                return;
            }

            string xetNghiem_ManualGUID = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row["XetNghiem_ManualGUID"].ToString();
            DataRow drXetNghiem = GetDataRow(xetNghiem_ManualGUID);
            if (drXetNghiem == null) return;
            dlgAddXetNghiemTay dlg = new dlgAddXetNghiemTay(drXetNghiem, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drXetNghiem["Fullname"] = dlg.XetNghiem.Fullname;
                drXetNghiem["TenXetNghiem"] = dlg.XetNghiem.TenXetNghiem;
                drXetNghiem["Type"] = dlg.XetNghiem.Type;
                drXetNghiem["LoaiXN"] = GetLoaiXetNghiem(dlg.XetNghiem.Type);

                if (dlg.XetNghiem.CreatedDate.HasValue)
                    drXetNghiem["CreatedDate"] = dlg.XetNghiem.CreatedDate;

                if (dlg.XetNghiem.CreatedBy.HasValue)
                    drXetNghiem["CreatedBy"] = dlg.XetNghiem.CreatedBy.ToString();

                if (dlg.XetNghiem.UpdatedDate.HasValue)
                    drXetNghiem["UpdatedDate"] = dlg.XetNghiem.UpdatedDate;

                if (dlg.XetNghiem.UpdatedBy.HasValue)
                    drXetNghiem["UpdatedBy"] = dlg.XetNghiem.UpdatedBy.ToString();

                if (dlg.XetNghiem.DeletedDate.HasValue)
                    drXetNghiem["DeletedDate"] = dlg.XetNghiem.DeletedDate;

                if (dlg.XetNghiem.DeletedBy.HasValue)
                    drXetNghiem["DeletedBy"] = dlg.XetNghiem.DeletedBy.ToString();

                drXetNghiem["Status"] = dlg.XetNghiem.Status;

                OnSearchXetNghiem();
            }
        }

        private void OnDelete()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedServiceList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = _dataSource;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedServiceList.Add(row["ServiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedServiceList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những xét nghiệm mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = XetNghiemTayBus.DeleteXetNghiem(deletedServiceList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchXetNghiem();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiemTayBus.DeleteXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.DeleteXetNghiem"));
                    }
                }
                
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những xét nghiệm cần xóa.", IconType.Information);
        }

        private DataRow GetDataRow(string xetNghiem_ManualGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("XetNghiem_ManualGUID = '{0}'", xetNghiem_ManualGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }
        #endregion

        #region Window Event Handlers
        private void txtXetNghiem_TextChanged(object sender, EventArgs e)
        {
            OnSearchXetNghiem();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
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

        private void dgXetNghiem_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void dgXetNghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgXetNghiem.Focus();

                if (dgXetNghiem.SelectedRows != null && dgXetNghiem.SelectedRows.Count > 0)
                {
                    int index = dgXetNghiem.SelectedRows[0].Index;
                    if (index < dgXetNghiem.RowCount - 1)
                    {
                        index++;
                        dgXetNghiem.CurrentCell = dgXetNghiem[1, index];
                        dgXetNghiem.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgXetNghiem.Focus();

                if (dgXetNghiem.SelectedRows != null && dgXetNghiem.SelectedRows.Count > 0)
                {
                    int index = dgXetNghiem.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgXetNghiem.CurrentCell = dgXetNghiem[1, index];
                        dgXetNghiem.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayXetNghiemListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayXetNghiemList();
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

        private void dgXetNghiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
