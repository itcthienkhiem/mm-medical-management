using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using QiHe.CodeLib;
using QiHe.Office.CompoundDocumentFormat;
using QiHe.Office.Excel;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Controls.Excel
{
    public partial class uImportFromExcel : UserControl
    {
        public uImportFromExcel()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Please select your excel file";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtSelectedFile.Text = openFileDialog1.FileName;
            else
                txtSelectedFile.Text = string.Empty;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            label2.Text = "Importing.Please wait...";
            label2.Visible = true;
            label2.Update();
            ImportPatientFromExcel();
            //InsertToPracticeP2k();

            label2.Text = string.Format("Finishing importing addresses from file '{0}'", txtSelectedFile.Text);
            txtSelectedFile.Text = string.Empty;
            this.Cursor = Cursors.Default;
            MessageBox.Show(label2.Text);
        }

        private int GetPatientQuantity()
        {
            Result result = PatientBus.GetCountPatient();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                    return (int)dt.Rows[0][0];
                else return 0;
            }
            else return 0;
        }

        private bool IsPatientExist(string fullname, string dobStr, byte gender, string source)
        {
            Result result = PatientBus.CheckPatientExist(fullname, dobStr, gender, source);
            if (result.Error.Code == MM.Common.ErrorCode.EXIST || result.Error.Code == MM.Common.ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == MM.Common.ErrorCode.EXIST)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private void ImportPatientFromExcel()
        {
            try
            {
                if (File.Exists(txtSelectedFile.Text))
                {
                    CompoundDocument doc = CompoundDocument.Read(txtSelectedFile.Text);
                    byte[] bookdata = doc.GetStreamData("Workbook");
                    Workbook book = new Workbook();
                    book.Read(new MemoryStream(bookdata));

                    foreach (Worksheet sheet in book.Worksheets)
                    {
                        int RowCount = sheet.MaxRowIndex + 1;
                        int ColumnCount = sheet.MaxColIndex + 1;
                        for (int i = 1; i < RowCount; i++)
                        {
                            Contact ct = new Contact();
                            Patient p = new Patient();
                            PatientHistory ph = new PatientHistory();
                            string sCode = "VGH";
                            for (int j = 0; j < ColumnCount; j++)
                            {
                                string curCellValue = string.Empty;
                                if (sheet.Cells[i, j] != null)
                                {
                                    curCellValue = sheet.Cells[i, j].StringValue.Trim();
                                }
                                //process NULL text in excel 
                                if (curCellValue.ToUpper() == "NULL")
                                {
                                    curCellValue = "";
                                }
                                //process "'" character
                                curCellValue = curCellValue.Replace("'", "''");
                                if (sheet.Cells[0, j] != null && sheet.Cells[0, j].StringValue != null)
                                {
                                    switch (sheet.Cells[0, j].StringValue.Trim().ToLower())
                                    {
                                        case "surname":
                                        case "sirname":
                                            ct.SurName = curCellValue;
                                            break;

                                        case "firstname":
                                        case "1st name":
                                        case "first name":
                                            ct.FirstName = curCellValue;
                                            break;

                                        case "fullname":
                                        case "full name":
                                            string fn = curCellValue;
                                            string surName = string.Empty;
                                            string firstName = string.Empty;
                                            Utility.GetSurNameFirstNameFromFullName(fn, ref surName, ref firstName);
                                            ct.SurName = surName;
                                            ct.FirstName = firstName;
                                            ct.FullName = curCellValue;
                                            break;

                                        case "birthday":
                                        case "date of birth":
                                        case "dob":
                                            string dob = curCellValue;
                                            ct.DobStr = dob;
                                            DateTime dt = new DateTime();
                                            if (DateTime.TryParse(dob, out dt))
                                            {
                                                ct.Dob = dt;
                                            }
                                            break;

                                        case "gender":
                                        case "sex":
                                            string s = curCellValue;
                                            if (s == "nữ" || s == "femail" || s == "f")
                                                ct.Gender = (byte)Gender.Female;
                                            else
                                                ct.Gender = (byte)Gender.Male;
                                            break;

                                        case "marital status":
                                        case "maritalstatus":
                                            ph.Tinh_Trang_Gia_Dinh = curCellValue;
                                            break;
                                        case "code":
                                        case "companycode":
                                            sCode = curCellValue;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                            }
                            //add patient to database only if they have surname and firstname
                            if (ct.FirstName != string.Empty && ct.SurName != string.Empty & ct.Gender.HasValue)
                            {
                                if(ct.FullName == string.Empty)
                                    ct.FullName = ct.SurName + " " + ct.FirstName;
                                ct.Source = Path.GetFileName(txtSelectedFile.Text.Trim());
                                if(!IsPatientExist(ct.FullName, ct.DobStr,ct.Gender.Value,ct.Source))
                                {
                                    ct.CreatedBy = Guid.Parse(Global.UserGUID);
                                    ct.CreatedDate = DateTime.Now;
                                    int iCount = GetPatientQuantity();
                                    iCount++;
                                    if (iCount < 10)
                                    {
                                        sCode += string.Format("0000{0}", iCount);
                                    }
                                    else if (iCount >= 10 && iCount < 100)
                                    {
                                        sCode += string.Format("000{0}", iCount);
                                    }
                                    else if (iCount >= 100 && iCount < 1000)
                                    {
                                        sCode += string.Format("00{0}", iCount);
                                    }
                                    else if (iCount >= 1000 && iCount < 10000)
                                    {
                                        sCode += string.Format("0{0}", iCount);
                                    }
                                    else
                                    {
                                        sCode += string.Format("{0}", iCount);
                                    }
                                    p.FileNum = sCode;
                                    Result result = PatientBus.InsertPatient(ct, p, ph);
                                    if (!result.IsOK)
                                    {
                                        MsgBox.Show("Import error", result.GetErrorAsString("PatientBus.InsertPatient"));
                                        Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.InsertPatient"));

                                    }
                                }
                            }
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select your excel file.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
