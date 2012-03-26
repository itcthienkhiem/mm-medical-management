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
using MM.Exports;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uSymptomList : uBase
    {
        #region Members
        DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uSymptomList()
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
            //btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplaySymptomListProc));
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
            dgSymptom.DataSource = null;
        }

        private void OnDisplaySymptomList()
        {
            Result result = SymptomBus.GetSymptomList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchTrieuChung();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SymptomBus.GetSymptomList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SymptomBus.GetSymptomList"));
            }
        }

        private void OnAddSymptom()
        {
            if (_dataSource == null) return;
            dlgAddSymptom dlg = new dlgAddSymptom();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;//dgSymptom.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["SymptomGUID"] = dlg.Symptom.SymptomGUID.ToString();
                newRow["Code"] = dlg.Symptom.Code;
                newRow["SymptomName"] = dlg.Symptom.SymptomName;
                newRow["Advice"] = dlg.Symptom.Advice;

                if (dlg.Symptom.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Symptom.CreatedDate;

                if (dlg.Symptom.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Symptom.CreatedBy.ToString();

                if (dlg.Symptom.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Symptom.UpdatedDate;

                if (dlg.Symptom.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Symptom.UpdatedBy.ToString();

                if (dlg.Symptom.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Symptom.DeletedDate;

                if (dlg.Symptom.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Symptom.DeletedBy.ToString();

                newRow["Status"] = dlg.Symptom.Status;
                dt.Rows.Add(newRow);
                //SelectLastedRow();
                OnSearchTrieuChung();
            }
        }

        private void SelectLastedRow()
        {
            dgSymptom.CurrentCell = dgSymptom[1, dgSymptom.RowCount - 1];
            dgSymptom.Rows[dgSymptom.RowCount - 1].Selected = true;
        }

        private DataRow GetDataRow(string symptomGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("SymptomGUID = '{0}'", symptomGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditSymptom()
        {
            if (_dataSource == null) return;
            if (dgSymptom.SelectedRows == null || dgSymptom.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 triệu chứng.", IconType.Information);
                return;
            }

            string symptomGUID = (dgSymptom.SelectedRows[0].DataBoundItem as DataRowView).Row["SymptomGUID"].ToString();
            DataRow drSymp = GetDataRow(symptomGUID);
            dlgAddSymptom dlg = new dlgAddSymptom(drSymp, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drSymp["Code"] = dlg.Symptom.Code;
                drSymp["SymptomName"] = dlg.Symptom.SymptomName;
                drSymp["Advice"] = dlg.Symptom.Advice;

                if (dlg.Symptom.CreatedDate.HasValue)
                    drSymp["CreatedDate"] = dlg.Symptom.CreatedDate;

                if (dlg.Symptom.CreatedBy.HasValue)
                    drSymp["CreatedBy"] = dlg.Symptom.CreatedBy.ToString();

                if (dlg.Symptom.UpdatedDate.HasValue)
                    drSymp["UpdatedDate"] = dlg.Symptom.UpdatedDate;

                if (dlg.Symptom.UpdatedBy.HasValue)
                    drSymp["UpdatedBy"] = dlg.Symptom.UpdatedBy.ToString();

                if (dlg.Symptom.DeletedDate.HasValue)
                    drSymp["DeletedDate"] = dlg.Symptom.DeletedDate;

                if (dlg.Symptom.DeletedBy.HasValue)
                    drSymp["DeletedBy"] = dlg.Symptom.DeletedBy.ToString();

                drSymp["Status"] = dlg.Symptom.Status;

                OnSearchTrieuChung();
            }
        }

        private void OnDeleteSymptom()
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
                    deletedSympList.Add(row["SymptomGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSympList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những triệu chứng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = SymptomBus.DeleteSymptom(deletedSympList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchTrieuChung();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("SymptomBus.DeleteSymptom"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("SymptomBus.DeleteSymptom"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những triệu chứng cần xóa.", IconType.Information);
        }

        private void UpdateChecked()
        {
            DataTable dt = dgSymptom.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string serviceGUID1 = row1["SymptomGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("SymptomGUID='{0}'", serviceGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchTrieuChung()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtTrieuChung.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("SymptomName")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgSymptom.DataSource = newDataSource;
                if (dgSymptom.RowCount > 0) dgSymptom.Rows[0].Selected = true;
                return;
            }

            string str = txtTrieuChung.Text.ToLower();

            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("SymptomName") != null &&
                           p.Field<string>("SymptomName").Trim() != string.Empty &&
                           p.Field<string>("SymptomName").ToLower().IndexOf(str) >= 0
                       //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                       //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                       orderby p.Field<string>("SymptomName")
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgSymptom.DataSource = newDataSource;
                return;
            }

            dgSymptom.DataSource = newDataSource;
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgSymptom.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\Symptom.xls", Application.StartupPath);
                if (ExportExcel.ExportSymptomToExcel(exportFileName, checkedRows))
                    try
                    {
                        if (isPreview)
                            ExcelPrintPreview.PrintPreview(exportFileName);
                        else
                        {
                            if (_printDialog.ShowDialog() == DialogResult.OK)
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                    }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những triệu chứng cần in.", IconType.Information);
        }
        #endregion
       
        #region Window Event Handlers
        private void dgSymptom_DoubleClick(object sender, EventArgs e)
        {
            //if (!AllowEdit) return;
            OnEditSymptom();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddSymptom();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditSymptom();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteSymptom();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgSymptom.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void txtTrieuChung_TextChanged(object sender, EventArgs e)
        {
            OnSearchTrieuChung();
        }

        private void txtTrieuChung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgSymptom.Focus();

                if (dgSymptom.SelectedRows != null && dgSymptom.SelectedRows.Count > 0)
                {
                    int index = dgSymptom.SelectedRows[0].Index;
                    if (index < dgSymptom.RowCount - 1)
                    {
                        index++;
                        dgSymptom.CurrentCell = dgSymptom[1, index];
                        dgSymptom.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgSymptom.Focus();

                if (dgSymptom.SelectedRows != null && dgSymptom.SelectedRows.Count > 0)
                {
                    int index = dgSymptom.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgSymptom.CurrentCell = dgSymptom[1, index];
                        dgSymptom.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplaySymptomListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplaySymptomList();
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
