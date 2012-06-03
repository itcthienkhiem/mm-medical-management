using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uDiaChiCongTyList : uBase
    {
        #region Members
        DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uDiaChiCongTyList()
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

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDiaChiCongTyListProc));
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

        public void ClearData()
        {
            dgDiaChi.DataSource = null;
        }

        private void OnDisplayDiaChiCongTyList()
        {
            Result result = DiaChiCongTyBus.GetDanhSachDiaChiCongTy();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchDiaChiCongTy();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("DiaChiCongTyBus.GetDanhSachDiaChiCongTy"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DiaChiCongTyBus.GetDanhSachDiaChiCongTy"));
            }
        }

        private void OnAdd()
        {
            if (_dataSource == null) return;
            dlgAddDiaChiCongTy dlg = new dlgAddDiaChiCongTy();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;//dgSymptom.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["DiaChiCongTyGUID"] = dlg.DiaChiCongTy.DiaChiCongTyGUID.ToString();
                newRow["MaCongTy"] = dlg.DiaChiCongTy.MaCongTy;
                newRow["DiaChi"] = dlg.DiaChiCongTy.DiaChi;

                if (dlg.DiaChiCongTy.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.DiaChiCongTy.CreatedDate;

                if (dlg.DiaChiCongTy.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.DiaChiCongTy.CreatedBy.ToString();

                if (dlg.DiaChiCongTy.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.DiaChiCongTy.UpdatedDate;

                if (dlg.DiaChiCongTy.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.DiaChiCongTy.UpdatedBy.ToString();

                if (dlg.DiaChiCongTy.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.DiaChiCongTy.DeletedDate;

                if (dlg.DiaChiCongTy.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.DiaChiCongTy.DeletedBy.ToString();

                newRow["Status"] = dlg.DiaChiCongTy.Status;
                dt.Rows.Add(newRow);
                OnSearchDiaChiCongTy();
            }
        }

        private DataRow GetDataRow(string diaChiCongTyGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("DiaChiCongTyGUID = '{0}'", diaChiCongTyGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEdit()
        {
            if (_dataSource == null) return;
            if (dgDiaChi.SelectedRows == null || dgDiaChi.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 địa chỉ công ty.", IconType.Information);
                return;
            }

            string diaChiCongTyGUID = (dgDiaChi.SelectedRows[0].DataBoundItem as DataRowView).Row["DiaChiCongTyGUID"].ToString();
            DataRow drDiaChiCongTy = GetDataRow(diaChiCongTyGUID);
            dlgAddDiaChiCongTy dlg = new dlgAddDiaChiCongTy(drDiaChiCongTy);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drDiaChiCongTy["MaCongTy"] = dlg.DiaChiCongTy.MaCongTy;
                drDiaChiCongTy["DiaChi"] = dlg.DiaChiCongTy.DiaChi;

                if (dlg.DiaChiCongTy.CreatedDate.HasValue)
                    drDiaChiCongTy["CreatedDate"] = dlg.DiaChiCongTy.CreatedDate;

                if (dlg.DiaChiCongTy.CreatedBy.HasValue)
                    drDiaChiCongTy["CreatedBy"] = dlg.DiaChiCongTy.CreatedBy.ToString();

                if (dlg.DiaChiCongTy.UpdatedDate.HasValue)
                    drDiaChiCongTy["UpdatedDate"] = dlg.DiaChiCongTy.UpdatedDate;

                if (dlg.DiaChiCongTy.UpdatedBy.HasValue)
                    drDiaChiCongTy["UpdatedBy"] = dlg.DiaChiCongTy.UpdatedBy.ToString();

                if (dlg.DiaChiCongTy.DeletedDate.HasValue)
                    drDiaChiCongTy["DeletedDate"] = dlg.DiaChiCongTy.DeletedDate;

                if (dlg.DiaChiCongTy.DeletedBy.HasValue)
                    drDiaChiCongTy["DeletedBy"] = dlg.DiaChiCongTy.DeletedBy.ToString();

                drDiaChiCongTy["Status"] = dlg.DiaChiCongTy.Status;

                OnSearchDiaChiCongTy();
            }
        }

        private void OnDelete()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedSympList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = _dataSource;//dgSymptom.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedSympList.Add(row["DiaChiCongTyGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSympList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những địa chỉ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = DiaChiCongTyBus.DeleteDiaChiCongTy(deletedSympList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchDiaChiCongTy();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("DiaChiCongTyBus.DeleteDiaChiCongTy"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("DiaChiCongTyBus.DeleteDiaChiCongTy"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những địa chỉ cần xóa.", IconType.Information);
        }

        private void UpdateChecked()
        {
            DataTable dt = dgDiaChi.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string serviceGUID1 = row1["DiaChiCongTyGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("DiaChiCongTyGUID='{0}'", serviceGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchDiaChiCongTy()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtMaCongTy.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("MaCongTy")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgDiaChi.DataSource = newDataSource;
                if (dgDiaChi.RowCount > 0) dgDiaChi.Rows[0].Selected = true;
                return;
            }

            string str = txtMaCongTy.Text.ToLower();

            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("MaCongTy") != null &&
                           p.Field<string>("MaCongTy").Trim() != string.Empty &&
                           p.Field<string>("MaCongTy").ToLower().IndexOf(str) >= 0
                       //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                       //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                       orderby p.Field<string>("MaCongTy")
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgDiaChi.DataSource = newDataSource;
                return;
            }

            dgDiaChi.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void txtMaCongTy_TextChanged(object sender, EventArgs e)
        {
            OnSearchDiaChiCongTy();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgDiaChi.DataSource as DataTable;
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

        private void dgDiaChi_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEdit();
        }
        #endregion

        #region Working Thread
        private void OnDisplayDiaChiCongTyListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayDiaChiCongTyList();
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
