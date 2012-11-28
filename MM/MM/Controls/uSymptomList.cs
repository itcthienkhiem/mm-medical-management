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
        private Dictionary<string, DataRow> _dictSymptom = new Dictionary<string, DataRow>();
        private string _name = string.Empty;
        private DataTable _dtTemp = null;
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
                _name = txtTrieuChung.Text;
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
            DataTable dt = dgSymptom.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgSymptom.DataSource = null;
        }

        private void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtTrieuChung.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplaySymptomList()
        {
            Result result = SymptomBus.GetSymptomList(_name);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    if (_dtTemp == null) _dtTemp = dt.Clone();
                    UpdateChecked(dt);
                    dgSymptom.DataSource = dt;
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

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["SymptomGUID"].ToString();
                if (_dictSymptom.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddSymptom()
        {
            dlgAddSymptom dlg = new dlgAddSymptom();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEditSymptom()
        {
            if (dgSymptom.SelectedRows == null || dgSymptom.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 triệu chứng.", IconType.Information);
                return;
            }

            DataRow drSymp = (dgSymptom.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drSymp == null) return;
            dlgAddSymptom dlg = new dlgAddSymptom(drSymp, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDeleteSymptom()
        {
            if (_dictSymptom == null) return;

            List<string> deletedSympList = new List<string>();
            List<DataRow> deletedRows = _dictSymptom.Values.ToList<DataRow>();

            foreach (DataRow row in deletedRows)
            {
                deletedSympList.Add(row["SymptomGUID"].ToString());
            }

            if (deletedSympList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những triệu chứng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = SymptomBus.DeleteSymptom(deletedSympList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgSymptom.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedSympList)
                        {
                            DataRow[] rows = dt.Select(string.Format("SymptomGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);        
                        }

                        _dictSymptom.Clear();
                        _dtTemp.Rows.Clear();
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

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = _dictSymptom.Values.ToList();
            
            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\Symptom.xls", Application.StartupPath);
                if (ExportExcel.ExportSymptomToExcel(exportFileName, checkedRows))
                    try
                    {
                        if (isPreview)
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.TrieuChungTemplate));
                        else
                        {
                            if (_printDialog.ShowDialog() == DialogResult.OK)
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.TrieuChungTemplate));
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
        private void dgSymptom_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgSymptom.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgSymptom.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string symptomGUID = row["SymptomGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictSymptom.ContainsKey(symptomGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictSymptom.Add(symptomGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictSymptom.ContainsKey(symptomGUID))
                {
                    _dictSymptom.Remove(symptomGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("SymptomGUID='{0}'", symptomGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void dgSymptom_DoubleClick(object sender, EventArgs e)
        {
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
                string symptomGUID = row["SymptomGUID"].ToString();

                if (chkChecked.Checked)
                {
                    if (!_dictSymptom.ContainsKey(symptomGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictSymptom.Add(symptomGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictSymptom.ContainsKey(symptomGUID))
                    {
                        _dictSymptom.Remove(symptomGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("SymptomGUID='{0}'", symptomGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtTrieuChung_TextChanged(object sender, EventArgs e)
        {
            SearchAsThread();
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplaySymptomList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        
    }
}
