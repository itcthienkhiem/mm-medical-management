/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
                MsgBox.Show(Application.ProductName, "Không thể chuyển qua PDF. Yêu cầu phải cài Office Excel 2007 và Microsoft Save as PDF 2007 Add-in.", Common.IconType.Error);
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
