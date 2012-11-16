using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel2007 = Microsoft.Office.Interop.Excel;

namespace MM.Exports
{
    public class ConvertExcelToPDF
    {
        public static bool Convert(string excelFileName, string pdfFileName)
        {
            Excel2007.Application excelApp = null;
            try
            {
                excelApp = new Excel2007.Application();
                excelApp.Workbooks.Open(excelFileName);
                excelApp.ActiveWorkbook.ExportAsFixedFormat(Excel2007.XlFixedFormatType.xlTypePDF, pdfFileName);
                return true;
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, "Không thể chuyển qua PDF. Yêu cầu phải cài Office Excel 2007.", Common.IconType.Error);
                return false;
            }
            finally
            {
                if (excelApp != null)
                {
                    excelApp.Quit();
                    excelApp = null;
                }
            }
        }
    }
}
