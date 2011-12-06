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
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uSymptomList : uBase
    {
        #region Members

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
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
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
                    dgSymptom.DataSource = result.QueryResult;
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
            dlgAddSymptom dlg = new dlgAddSymptom();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgSymptom.DataSource as DataTable;
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
                SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgSymptom.CurrentCell = dgSymptom[1, dgSymptom.RowCount - 1];
            dgSymptom.Rows[dgSymptom.RowCount - 1].Selected = true;
        }

        private void OnEditSymptom()
        {
            if (dgSymptom.SelectedRows == null || dgSymptom.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 triệu chứng.", IconType.Information);
                return;
            }

            DataRow drSymp = (dgSymptom.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddSymptom dlg = new dlgAddSymptom(drSymp);
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
            }
        }

        private void OnDeleteSymptom()
        {
            List<string> deletedSympList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgSymptom.DataSource as DataTable;
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
                            dt.Rows.Remove(row);
                        }
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
                if (ExportToExcel(exportFileName, checkedRows))
                    if (isPreview) 
                        ExcelPrintPreview.PrintPreview(exportFileName);
                    else
                        ExcelPrintPreview.Print(exportFileName);
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những triệu chứng cần in.", IconType.Information);
        }

        private bool ExportToExcel(string exportFileName, List<DataRow> checkedRows)
        {
            string excelTemplateName = string.Format("{0}\\Templates\\SymptomTemplate.xls", Application.StartupPath);
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                ExcelPrintPreview.SetCulturalWithEN_US();
                IWorksheet workSheet = workBook.Worksheets[0];
                int rowIndex = 1;

                foreach (DataRow row in checkedRows)
                {
                    string symptom = row["SymptomName"].ToString();
                    string advice = row["Advice"].ToString();
                    workSheet.Cells[rowIndex, 0].Value = rowIndex;
                    workSheet.Cells[rowIndex, 1].Value = symptom.Replace("\r", "").Replace("\t", "");
                    workSheet.Cells[rowIndex, 2].Value = advice.Replace("\r", "").Replace("\t", "");
                    rowIndex++;
                }

                IRange range = workSheet.Cells[string.Format("A2:C{0}", checkedRows.Count + 1)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;
                range.Borders.LineStyle = LineStyle.Continuous;
                range.Borders.Weight = BorderWeight.Thin;

                range = workSheet.Cells[string.Format("A2:A{0}", checkedRows.Count + 1)];
                range.HorizontalAlignment = HAlign.Center;
                range.VerticalAlignment = VAlign.Top;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.XLS97);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                ExcelPrintPreview.SetCulturalWithCurrent();
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }
        #endregion
       
        #region Window Event Handlers
        private void dgSymptom_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
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
