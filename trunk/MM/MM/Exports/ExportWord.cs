using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Exports
{
    public class ExportWord
    {
        private static List<string> alphab = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
                                                                "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
                                                                "u", "v", "w", "x", "y", "z"};
        public static void PrintMauHoSoChung(object reportFileName, DataRow patientRow, string printerName)
        {
            string fileNum = patientRow["FileNum"].ToString();
            string fullName = patientRow["FullName"].ToString();
            string dob = patientRow["DobStr"].ToString();
            string gender = patientRow["GenderAsStr"].ToString();
            string address = patientRow["Address"] as string;
            string mobile = patientRow["Mobile"] as string;
            string email = patientRow["Email"] as string;

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object missing = System.Type.Missing;

            try
            {
                doc = word.Documents.Open(ref reportFileName,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                doc.Activate();

                foreach (Microsoft.Office.Interop.Word.Range tmpRange in doc.StoryRanges)
                {
                    tmpRange.Find.Text = "#N";
                    tmpRange.Find.Replacement.Text = fullName;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#S";
                    tmpRange.Find.Replacement.Text = gender;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#D";
                    tmpRange.Find.Replacement.Text = dob;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#A";
                    tmpRange.Find.Replacement.Text = address;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#T";
                    tmpRange.Find.Replacement.Text = mobile;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#E";
                    tmpRange.Find.Replacement.Text = email;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);
                }

                if (doc.Sections != null && doc.Sections.Count > 0)
                    doc.Sections[1].Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = string.Format("CODE: {0}", fileNum);

                word.Visible = false;
                word.ActivePrinter = printerName;

                doc.PrintOut(ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (doc != null) doc.Close(ref missing, ref missing, ref missing);
                if (word != null) word.Application.Quit(ref missing, ref missing, ref missing);
            }
        }

        private static string GetSubNo(int index)
        {
            index++;
            int div = index / 26;
            int mod = index % 26;

            string chr = mod == 0 ? alphab[25] : alphab[mod - 1];
            for (int i = 0; i < div - 1; i++)
            {
                chr += chr;
            }

            return chr;
        }

        public static void PrintXetNghiem(object reportFileName, DataRow patientRow, DataRow[] serviceRows, string printerName)
        {
            string fileNum = patientRow["FileNum"].ToString();
            string fullName = patientRow["FullName"].ToString();
            string dob = patientRow["DobStr"].ToString();
            string gender = patientRow["GenderAsStr"].ToString();
            string address = patientRow["Address"] as string;
            string mobile = patientRow["Mobile"] as string;
            string email = patientRow["Email"] as string;

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object missing = System.Type.Missing;

            try
            {
                doc = word.Documents.Open(ref reportFileName,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                doc.Activate();

                foreach (Microsoft.Office.Interop.Word.Range tmpRange in doc.StoryRanges)
                {
                    tmpRange.Find.Text = "#N";
                    tmpRange.Find.Replacement.Text = fullName;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#S";
                    tmpRange.Find.Replacement.Text = gender;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#D";
                    tmpRange.Find.Replacement.Text = dob;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#A";
                    tmpRange.Find.Replacement.Text = address;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#T";
                    tmpRange.Find.Replacement.Text = mobile;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#E";
                    tmpRange.Find.Replacement.Text = email;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);
                }

                if (doc.Sections != null && doc.Sections.Count > 0)
                    doc.Sections[1].Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = string.Format("CODE: {0}", fileNum);

                //Fill Services
                Microsoft.Office.Interop.Word.Row dr = null;
                if (serviceRows != null && serviceRows.Length > 0)
                {
                    foreach (DataRow row in serviceRows)
                    {
                        string serviceName = row["Name"].ToString();
                        dr = doc.Tables[1].Rows.Add(ref missing);
                        dr.Cells[1].Range.Text = serviceName;
                        dr.Cells[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        SetNormalText(dr.Cells[1].Range);
                    }
                }

                dr = doc.Tables[1].Rows.Add(ref missing);
                dr.Cells[1].Range.Text = string.Format("Notes (Ghi chú)\n\n\n\n");
                dr.Cells[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                SetNormalText(dr.Cells[1].Range);

                word.Visible = false;
                word.ActivePrinter = printerName;

                doc.PrintOut(ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (doc != null) doc.Close(ref missing, ref missing, ref missing);
                if (word != null) word.Application.Quit(ref missing, ref missing, ref missing);
            }
        }

        public static void PrintChecklist(object reportFileName, DataRow patientRow, DataRow[] serviceRows, string printerName)
        {
            string fileNum = patientRow["FileNum"].ToString();
            string fullName = patientRow["FullName"].ToString();
            string dob = patientRow["DobStr"].ToString();
            string gender = patientRow["GenderAsStr"].ToString();
            string address = patientRow["Address"] as string;
            string mobile = patientRow["Mobile"] as string;
            string email = patientRow["Email"] as string;

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object missing = System.Type.Missing;

            try
            {
                doc = word.Documents.Open(ref reportFileName,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                doc.Activate();

                foreach (Microsoft.Office.Interop.Word.Range tmpRange in doc.StoryRanges)
                {
                    tmpRange.Find.Text = "#N";
                    tmpRange.Find.Replacement.Text = fullName;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#S";
                    tmpRange.Find.Replacement.Text = gender;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#D";
                    tmpRange.Find.Replacement.Text = dob;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#A";
                    tmpRange.Find.Replacement.Text = address;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#T";
                    tmpRange.Find.Replacement.Text = mobile;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);

                    tmpRange.Find.Text = "#E";
                    tmpRange.Find.Replacement.Text = email;
                    tmpRange.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    tmpRange.Find.Execute(ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref replaceAll,
                        ref missing, ref missing, ref missing, ref missing);
                }

                if (doc.Sections != null && doc.Sections.Count > 0)
                    doc.Sections[1].Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = string.Format("CODE: {0}", fileNum);

                //Fill Services
                int no = 2;
                Microsoft.Office.Interop.Word.Row dr = null;
                if (serviceRows != null && serviceRows.Length > 0)
                {
                    List<string> servicesNameList = new List<string>();
                    Hashtable htServiceGroup = new Hashtable();
                    foreach (DataRow row in serviceRows)
                    {
                        string serviceGUID = row["ServiceGUID"].ToString();
                        string serviceName = row["Name"].ToString();

                        Result result = ServiceGroupBus.GetServiceGroup(serviceGUID);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.GetServiceGroup"));
                            return;
                        }
                        else
                        {
                            if (result.QueryResult == null)
                                servicesNameList.Add(serviceName);
                            else
                            {
                                ServiceGroup serviceGroup = result.QueryResult as ServiceGroup;
                                string groupName = serviceGroup.Name;
                                if (htServiceGroup.ContainsKey(groupName))
                                {
                                    List<string> serviceList = (List<string>)htServiceGroup[groupName];
                                    serviceList.Add(serviceName);
                                }
                                else
                                {
                                    List<string> serviceList = new List<string>();
                                    serviceList.Add(serviceName);
                                    htServiceGroup.Add(groupName, serviceList);
                                }
                            }
                        }
                    }

                    servicesNameList.Sort();
                    foreach (string groupName in htServiceGroup.Keys)
                    {
                        string text = string.Format("{0}.  {1}", no++, groupName);
                        List<string> serviceList = (List<string>)htServiceGroup[groupName];
                        int subNo = 0;
                        foreach (string serviceName in serviceList)
                        {
                            text += string.Format("\n     {0}. {1}", GetSubNo(subNo++), serviceName);
                        }

                        dr = doc.Tables[1].Rows.Add(ref missing);
                        dr.Cells[1].Range.Text = text;
                        SetNormalText(dr.Cells[1].Range);
                    }

                    foreach (string serviceName in servicesNameList)
                    {
                        dr = doc.Tables[1].Rows.Add(ref missing);
                        dr.Cells[1].Range.Text = string.Format("{0}.  {1}", no++, serviceName);
                        SetNormalText(dr.Cells[1].Range);
                    }
                }

                dr = doc.Tables[1].Rows.Add(ref missing);
                dr.Cells[1].Range.Text = string.Format("Prescription (Toa thuốc) 	 Nội khoa	 Tai Mũi Họng	 Răng Hàm Mặt	 Sản phụ khoa");

                Microsoft.Office.Interop.Word.Row dr2 = doc.Tables[1].Rows.Add(ref missing);
                dr2.Cells[1].Range.Text = string.Format("{0}.  Others (Khác)", no);
                SetNormalText(dr2.Cells[1].Range);

                dr.Cells[1].Merge(dr.Cells[2]);
                dr.Height = dr.Height / 2.5f;
                dr.Cells[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                SetNormalText(dr.Cells[1].Range);

                word.Visible = false;
                word.ActivePrinter = printerName;

                doc.PrintOut(ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (doc != null) doc.Close(ref missing, ref missing, ref missing);
                if (word != null) word.Application.Quit(ref missing, ref missing, ref missing);
            }
        }

        public static void PrintKetQuaCanLamSang(object reportFileName, DataRow patientRow, DataRow[] serviceRows, string printerName)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object missing = System.Type.Missing;

            try
            {
                doc = word.Documents.Open(ref reportFileName,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

                doc.Activate();

                //Fill Services
                Microsoft.Office.Interop.Word.Row dr = null;
                if (serviceRows != null && serviceRows.Length > 0)
                {
                    bool isFirst = true;
                    foreach (DataRow row in serviceRows)
                    {
                        if (isFirst)
                        {
                            dr = doc.Tables[1].Rows[2];
                            isFirst = false;
                        }
                        else
                            dr = doc.Tables[1].Rows.Add(ref missing);

                        string serviceName = row["Name"].ToString();
                        bool isNormal_Abnormal = Convert.ToBoolean(row["Normal_Abnormal"]);
                        bool isNegative_Positive = Convert.ToBoolean(row["Negative_Positive"]);

                        if (isNormal_Abnormal) serviceName += "\n Normal (Bình thường)	 Abnormal (Bất thường)";
                        if (isNegative_Positive) serviceName += "\n Negative (Âm tính)                   	 Positive (Dương tính)";

                        dr.Cells[1].Range.Text = serviceName;
                        dr.Cells[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        SetNormalText(dr.Cells[1].Range);
                    }

                    dr = doc.Tables[1].Rows.Add(ref missing);
                    dr.Cells[1].Range.Text = string.Format("Others (Cận lâm sàng khác)");
                    dr.Cells[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    SetNormalText(dr.Cells[1].Range);
                }
                else
                {
                    dr = doc.Tables[1].Rows[2];
                    dr.Cells[1].Range.Text = string.Format("Others (Cận lâm sàng khác)");
                    dr.Cells[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    SetNormalText(dr.Cells[1].Range);
                }

                word.Visible = false;
                word.ActivePrinter = printerName;

                doc.PrintOut(ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (doc != null) doc.Close(ref missing, ref missing, ref missing);
                if (word != null) word.Application.Quit(ref missing, ref missing, ref missing);
            }
        }

        private static void SetNormalText(Microsoft.Office.Interop.Word.Range range)
        {
            bool isNormal = false;
            foreach (Microsoft.Office.Interop.Word.Range w in range.Words)
            {
                if (w.Text.Trim() == "(")
                {
                    w.Bold = 0;
                    isNormal = true;
                }

                if (isNormal) w.Bold = 0;

                if (w.Text.Trim() == ")")
                {
                    w.Bold = 0;
                    isNormal = false;
                }

                if (w.Text.Trim() == "Normal" || w.Text.Trim() == "Abnormal" ||
                    w.Text.Trim() == "Negative" || w.Text.Trim() == "Positive")
                    w.Bold = 0;
            }
        }
    }
}
