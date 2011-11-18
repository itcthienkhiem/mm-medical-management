using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MM.Common
{
    public class ExcelPrintPreview
    {
        #region Members
        private static CultureInfo _currentCultural = System.Threading.Thread.CurrentThread.CurrentCulture;
        #endregion

        public static Excel.Application ExcelInit()
        {
            Excel.Application excelApp = null;
            try
            {
                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;
                excelApp.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return excelApp;
        }

        public static void ExcelTerminal(Excel.Application excelApp)
        {
            try
            {
                if (excelApp == null) return;

                excelApp.Workbooks.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp.Workbooks);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                excelApp = null;
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PrintPreview(string fileName)
        {
            object objOpt = System.Reflection.Missing.Value;
            Excel.Application excelApp = ExcelInit();
            Excel.Workbook workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                       objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
            excelApp.Visible = true;
            workBook.PrintPreview(objOpt);
            excelApp.Visible = false;
            ExcelTerminal(excelApp);
        }

        public static void SetCulturalWithEN_US()
        {
            if (_currentCultural.Name != "en-US")
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        public static void SetCulturalWithCurrent()
        {
            if (_currentCultural.Name != "en-US")
                System.Threading.Thread.CurrentThread.CurrentCulture = _currentCultural;
        }

        /*public static Error ExportLichHenKhacHang(string templateFileName, string outputFileName, SourceGrid2.Grid dataGrid, int thang, string tenLoaiDo)
        {
            Error error = new Error();
            IWorkbook workBook = null;

            try
            {
                workBook = SpreadsheetGear.Factory.GetWorkbook(templateFileName);
                SetCulturalWithEN_US();
                IWorksheet workSheet = workBook.Worksheets[0];
                int row = 0;

                //Tháng & Tên Loại đồ
                workSheet.Cells[row++, 0].Value = string.Format("Tháng: {0}", thang);
                workSheet.Cells[row++, 0].Value = string.Format("Loại đồ: {0}", tenLoaiDo);

                for (int rowIndex = 0; rowIndex < dataGrid.RowsCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < dataGrid.ColumnsCount; colIndex++)
                    {
                        SourceGrid2.Cells.Real.Cell cell = dataGrid[rowIndex, colIndex] as SourceGrid2.Cells.Real.Cell;
                        Color backColor = cell.BackColor;
                        Color foreColor = cell.ForeColor;
                        IRange range = workSheet.Cells[row, colIndex];
                        if (cell.Value != null && cell.Value.ToString().Trim() != string.Empty)
                            range.Value = cell.Value.ToString();

                        range.Interior.Color = backColor;
                        range.Font.Color = foreColor;

                        if (rowIndex == 0 || colIndex == 0)
                            range.Borders.Color = Color.White;
                        else
                            range.Borders.Color = Color.Black;

                        range.Borders.Weight = SpreadsheetGear.BorderWeight.Thin;
                        range.Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;

                        if (cell.ToolTipText != null && cell.ToolTipText.Trim() != string.Empty)
                        {
                            string comment = cell.ToolTipText.Replace("(Đã Khóa)\n", "").Replace("(Đã Khóa)", "");
                            if (comment.Trim() != string.Empty)
                                range.AddComment(comment);
                        }
                    }

                    row++;
                }

                workBook.SaveAs(outputFileName, SpreadsheetGear.FileFormat.XLS97);
            }
            catch (Exception e)
            {
                error.Code = ErrorCode.UNKNOWN_ERROR;
                error.Description = e.Message;
            }
            finally
            {
                SetCulturalWithCurrent();
                workBook.Close();
                workBook = null;
            }

            return error;
        }*/
    }
}
