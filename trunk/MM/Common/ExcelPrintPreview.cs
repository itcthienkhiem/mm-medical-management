using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;

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
            Excel.Workbook workBook = null;
            try
            {
                object objOpt = System.Reflection.Missing.Value;
                excelApp = ExcelInit();
                workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                           objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);

                Excel.Worksheet workSheet = workBook.Sheets[0];
                workSheet.PageSetup.TopMargin = 1;

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
                if (workBook != null)
                {
                    workBook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                    workBook = null;
                }
                
                ExcelTerminal(excelApp);
            }
        }

        public static string ConvertToExcelPrinterFriendlyName(string printerName)
        {
            var key = Registry.CurrentUser;
            var subkey = key.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Devices");

            var value = subkey.GetValue(printerName);
            if (value == null) throw new Exception(string.Format("Device not found: {0}", printerName));

            var portName = value.ToString().Substring(9);  //strip away the winspool, 

            return string.Format("{0} on {1}", printerName, portName);
        }

        public static void Print(string fileName, string printerName)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            try
            {
                object objOpt = System.Reflection.Missing.Value;
                excelApp = ExcelInit();
                workBook = excelApp.Workbooks.Open(fileName, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                                           objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
                excelApp.Visible = false;
                excelApp.ActivePrinter = ConvertToExcelPrinterFriendlyName(printerName);
                workBook.PrintOut(objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                    workBook = null;
                }

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
