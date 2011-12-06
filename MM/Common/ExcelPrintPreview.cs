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
            Excel.Application excelApp = null;
            try
            {
                object objOpt = System.Reflection.Missing.Value;
                excelApp = ExcelInit();
                Excel.Workbook workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                           objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
                excelApp.Visible = true;
                workBook.PrintPreview(objOpt);
                excelApp.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ExcelTerminal(excelApp);
            }
        }

        public static void Print(string fileName)
        {
            Excel.Application excelApp = null;
            try
            {
                object objOpt = System.Reflection.Missing.Value;
                excelApp = ExcelInit();
                Excel.Workbook workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                           objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
                excelApp.Visible = false;
                workBook.PrintOut(objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ExcelTerminal(excelApp);
            }
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
    }
}
