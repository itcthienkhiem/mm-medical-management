using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel2007 = Microsoft.Office.Interop.Excel;
using MM.Common;

namespace MM.Exports
{
    public class ConvertExcelToPDF
    {
        public static bool Convert(string excelFileName, string pdfFileName, PageSetup p)
        {
            Excel2007.Application excelApp = null;
            Excel2007.Workbook workBook = null;
            
            try
            {
                excelApp = new Excel2007.Application();
                workBook = excelApp.Workbooks.Open(excelFileName);

                if (p != null)
                {
                    Excel2007.Worksheet workSheet = null;
                    int sheetCount = workBook.Sheets.Count;
                    int i = 0;
                    while (i <= sheetCount)
                    {
                        try
                        {
                            workSheet = workBook.Sheets[i];
                            break;
                        }
                        catch
                        {
                            i++;
                        }
                    }

                    workSheet.PageSetup.LeftMargin = p.GetLeftMargin();
                    workSheet.PageSetup.RightMargin = p.GetRightMargin();
                    workSheet.PageSetup.TopMargin = p.GetTopMargin();
                    workSheet.PageSetup.BottomMargin = p.GetBottomMargin();
                }

                if (workBook != null)
                    workBook.ExportAsFixedFormat(Excel2007.XlFixedFormatType.xlTypePDF, pdfFileName);
                return true;
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, "Không thể chuyển qua PDF. Yêu cầu phải cài Office Excel 2007.", Common.IconType.Error);
                return false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close(false, Type.Missing, Type.Missing);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                    workBook = null;
                }

                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    excelApp = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
