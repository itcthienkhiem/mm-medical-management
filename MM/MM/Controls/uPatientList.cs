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
using MM.Dialogs;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using SpreadsheetGear;
using SpreadsheetGear.Advanced.Cells;
using MM.Exports;

namespace MM.Controls
{
    public partial class uPatientList : uBase
    {
        #region Members
        private string _fileName = string.Empty;
        private bool _isAscending = true;
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private int _type = 0;
        private DataTable _dtTemp = null;
        #endregion

        #region Constructor
        public uPatientList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
            btnOpenPatient.Enabled = AllowOpenPatient;
            btnImportExcel.Enabled = AllowImport;
            btnVaoPhongCho.Enabled = Global.AllowAddPhongCho;
            btnTaoHoSo.Enabled = Global.AllowTaoHoSo;
            btnUploadHoSo.Enabled = Global.AllowUploadHoSo;
            btnTaoMatKhau.Enabled = Global.AllowAddMatKhauHoSo;
            btnExportExcel.Enabled = AllowExport;
            btnPrint.Enabled = AllowPrint;
            btnXuatMaVach.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            moBenhNhanToolStripMenuItem.Enabled = AllowOpenPatient;
            importExcelToolStripMenuItem.Enabled = AllowImport;
            vaoPhongChoToolStripMenuItem.Enabled = Global.AllowAddPhongCho;
            taoHoSoToolStripMenuItem.Enabled = Global.AllowTaoHoSo;
            uploadHoSoToolStripMenuItem.Enabled = Global.AllowUploadHoSo;
            taoMatKhauToolStripMenuItem.Enabled = Global.AllowAddMatKhauHoSo;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
            printToolStripMenuItem.Enabled = AllowPrint;
            xuatMaVachToolStripMenuItem.Enabled = AllowExport;
        }

        public void ClearData()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgPatient.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
                //if (_name.Trim() == string.Empty) _name = "*";
                //else if (_name.Trim() == "*") _name = string.Empty;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
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

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
                //if (_name.Trim() == string.Empty) _name = "*";
                //else if (_name.Trim() == "*") _name = string.Empty;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayPatientList()
        {
            lock (ThisLock)
            {
                Result result = PatientBus.GetPatientList(_name, _type);
                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgPatient.DataSource = dt;

                        lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);

                        txtSearchPatient.Focus();
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["PatientGUID"].ToString();
                if (_dictPatient.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddPatient()
        {
            dlgAddPatient dlg = new dlgAddPatient();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEditPatient()
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.", IconType.Information);
                return;
            }

            DataRow drPatient = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drPatient == null) return;

            dlgAddPatient dlg = new dlgAddPatient(drPatient, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();

                DataRow[] rows = Global.dtOpenPatient.Select(string.Format("PatientGUID='{0}'", drPatient["PatientGUID"].ToString()));
                if (rows != null && rows.Length > 0)
                {
                    rows[0]["FileNum"] = dlg.Patient.FileNum;
                    rows[0]["FullName"] = dlg.Contact.FullName;
                    rows[0]["SurName"] = dlg.Contact.SurName;
                    rows[0]["MiddleName"] = dlg.Contact.MiddleName;
                    rows[0]["FirstName"] = dlg.Contact.FirstName;
                    rows[0]["KnownAs"] = dlg.Contact.KnownAs;
                    rows[0]["PreferredName"] = dlg.Contact.PreferredName;
                    rows[0]["Gender"] = dlg.Contact.Gender;

                    if (dlg.Contact.Gender == 0) rows[0]["GenderAsStr"] = "Nam";//dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                    else if (dlg.Contact.Gender == 1) rows[0]["GenderAsStr"] = "Nữ";
                    else rows[0]["GenderAsStr"] = "Không xác định";

                    rows[0]["DobStr"] = dlg.Contact.DobStr;
                    rows[0]["IdentityCard"] = dlg.Contact.IdentityCard;
                    rows[0]["HomePhone"] = dlg.Contact.HomePhone;
                    rows[0]["WorkPhone"] = dlg.Contact.WorkPhone;
                    rows[0]["Mobile"] = dlg.Contact.Mobile;
                    rows[0]["Email"] = dlg.Contact.Email;
                    rows[0]["FAX"] = dlg.Contact.FAX;
                    rows[0]["Address"] = dlg.Contact.Address;
                    rows[0]["Ward"] = dlg.Contact.Ward;
                    rows[0]["District"] = dlg.Contact.District;
                    rows[0]["City"] = dlg.Contact.City;
                    rows[0]["Occupation"] = dlg.Contact.Occupation;
                    rows[0]["CompanyName"] = dlg.Contact.CompanyName;

                    if (dlg.Patient.NgayKham != null && dlg.Patient.NgayKham.HasValue)
                        rows[0]["NgayKham"] = dlg.Patient.NgayKham.Value;
                    else
                        rows[0]["NgayKham"] = DBNull.Value;

                    if (dlg.Contact.CreatedDate.HasValue)
                        rows[0]["CreatedDate"] = dlg.Contact.CreatedDate;

                    if (dlg.Contact.CreatedBy.HasValue)
                        rows[0]["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                    if (dlg.Contact.UpdatedDate.HasValue)
                        rows[0]["UpdatedDate"] = dlg.Contact.UpdatedDate;

                    if (dlg.Contact.UpdatedBy.HasValue)
                        rows[0]["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                    if (dlg.Contact.DeletedDate.HasValue)
                        rows[0]["DeletedDate"] = dlg.Contact.DeletedDate;

                    if (dlg.Contact.DeletedBy.HasValue)
                        rows[0]["DeletedBy"] = dlg.Contact.DeletedBy.ToString();

                    //Patient History
                    rows[0]["Di_Ung_Thuoc"] = dlg.PatientHistory.Di_Ung_Thuoc.Value;
                    if (dlg.PatientHistory.Di_Ung_Thuoc.Value)
                        rows[0]["Thuoc_Di_Ung"] = dlg.PatientHistory.Thuoc_Di_Ung;
                    else
                        rows[0]["Thuoc_Di_Ung"] = string.Empty;

                    rows[0]["Dot_Quy"] = dlg.PatientHistory.Dot_Quy.Value;
                    rows[0]["Benh_Tim_Mach"] = dlg.PatientHistory.Benh_Tim_Mach.Value;
                    rows[0]["Benh_Lao"] = dlg.PatientHistory.Benh_Lao.Value;
                    rows[0]["Dai_Thao_Duong"] = dlg.PatientHistory.Dai_Thao_Duong.Value;
                    rows[0]["Dai_Duong_Dang_Dieu_Tri"] = dlg.PatientHistory.Dai_Duong_Dang_Dieu_Tri.Value;
                    rows[0]["Viem_Gan_B"] = dlg.PatientHistory.Viem_Gan_B.Value;
                    rows[0]["Viem_Gan_C"] = dlg.PatientHistory.Viem_Gan_C.Value;
                    rows[0]["Viem_Gan_Dang_Dieu_Tri"] = dlg.PatientHistory.Viem_Gan_Dang_Dieu_Tri.Value;
                    rows[0]["Ung_Thu"] = dlg.PatientHistory.Ung_Thu.Value;
                    if (dlg.PatientHistory.Ung_Thu.Value)
                        rows[0]["Co_Quan_Ung_Thu"] = dlg.PatientHistory.Co_Quan_Ung_Thu;
                    else
                        rows[0]["Co_Quan_Ung_Thu"] = string.Empty;

                    rows[0]["Dong_Kinh"] = dlg.PatientHistory.Dong_Kinh.Value;
                    rows[0]["Hen_Suyen"] = dlg.PatientHistory.Hen_Suyen.Value;
                    rows[0]["Benh_Khac"] = dlg.PatientHistory.Benh_Khac.Value;
                    if (dlg.PatientHistory.Benh_Khac.Value)
                    {
                        rows[0]["Benh_Gi"] = dlg.PatientHistory.Benh_Gi;
                        rows[0]["Thuoc_Dang_Dung"] = dlg.PatientHistory.Thuoc_Dang_Dung;
                    }
                    else
                    {
                        rows[0]["Benh_Gi"] = string.Empty;
                        rows[0]["Thuoc_Dang_Dung"] = string.Empty;
                    }

                    rows[0]["Hut_Thuoc"] = dlg.PatientHistory.Hut_Thuoc.Value;
                    rows[0]["Uong_Ruou"] = dlg.PatientHistory.Uong_Ruou.Value;
                    rows[0]["Tinh_Trang_Gia_Dinh"] = dlg.PatientHistory.Tinh_Trang_Gia_Dinh;
                    rows[0]["Chich_Ngua_Viem_Gan_B"] = dlg.PatientHistory.Chich_Ngua_Viem_Gan_B.Value;
                    rows[0]["Chich_Ngua_Uon_Van"] = dlg.PatientHistory.Chich_Ngua_Uon_Van;
                    rows[0]["Chich_Ngua_Cum"] = dlg.PatientHistory.Chich_Ngua_Cum;
                    rows[0]["Dang_Co_Thai"] = dlg.PatientHistory.Dang_Co_Thai.Value;
                }
            }
        }

        private void OnDeletePatient()
        {
            List<string> deletedPatientList = new List<string>();
            List<DataRow> deletedRows = _dictPatient.Values.ToList();
            foreach (DataRow row in deletedRows)
            {
                string patientGUID = row["PatientGUID"].ToString();
                deletedPatientList.Add(patientGUID);
            }

            if (deletedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PatientBus.DeletePatient(deletedPatientList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgPatient.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;
                        foreach (string key in deletedPatientList)
                        {
                            DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", key));
                            if (rows != null && rows.Length > 0)
                                dt.Rows.Remove(rows[0]);

                            rows = Global.dtOpenPatient.Select(string.Format("PatientGUID='{0}'", key));
                            if (rows != null && rows.Length > 0)
                                Global.dtOpenPatient.Rows.Remove(rows[0]);
                        }

                        _dictPatient.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.DeletePatient"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.DeletePatient"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần xóa.", IconType.Information);
        }

        private void OnOpentPatient()
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.", IconType.Information);
                return;
            }

            DataRow patientRow = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (patientRow != null)
            {
                string patientGUID = patientRow["PatientGUID"].ToString();
                DataRow[] rows = Global.dtOpenPatient.Select(string.Format("PatientGUID='{0}'", patientGUID));
                if (rows == null || rows.Length <= 0)
                {
                    DataRow newRow = Global.dtOpenPatient.NewRow();
                    newRow["PatientGUID"] = patientRow["PatientGUID"];
                    newRow["FileNum"] = patientRow["FileNum"];
                    newRow["FullName"] = patientRow["FullName"];
                    newRow["GenderAsStr"] = patientRow["GenderAsStr"];
                    newRow["DobStr"] = patientRow["DobStr"];
                    newRow["IdentityCard"] = patientRow["IdentityCard"];
                    newRow["WorkPhone"] = patientRow["WorkPhone"];
                    newRow["Mobile"] = patientRow["Mobile"];
                    newRow["Email"] = patientRow["Email"];
                    newRow["Address"] = patientRow["Address"];
                    newRow["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                    Global.dtOpenPatient.Rows.Add(newRow);
                    base.RaiseOpentPatient(newRow);
                }
                else
                {
                    rows[0]["PatientGUID"] = patientRow["PatientGUID"];
                    rows[0]["FileNum"] = patientRow["FileNum"];
                    rows[0]["FullName"] = patientRow["FullName"];
                    rows[0]["GenderAsStr"] = patientRow["GenderAsStr"];
                    rows[0]["DobStr"] = patientRow["DobStr"];
                    rows[0]["IdentityCard"] = patientRow["IdentityCard"];
                    rows[0]["WorkPhone"] = patientRow["WorkPhone"];
                    rows[0]["Mobile"] = patientRow["Mobile"];
                    rows[0]["Email"] = patientRow["Email"];
                    rows[0]["Address"] = patientRow["Address"];
                    rows[0]["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                    base.RaiseOpentPatient(rows[0]);
                }
            }
        }

        private void OnImportExcel()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _fileName = dlg.FileName;
                ImportPatientFromExcel();
            }
        }

        private int GetPatientQuantity()
        {
            Result result = PatientBus.GetPatientCount();
            if (result.IsOK)
                return Convert.ToInt32(result.QueryResult);
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientCount"));
                return 0;
            }
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
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.CheckPatientExist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.CheckPatientExist"));
                return false;
            }
        }

        private bool IsExistFileNum(string code)
        {
            Result result = PatientBus.CheckPatientExistFileNum(string.Empty, code);
            if (result.Error.Code == MM.Common.ErrorCode.EXIST || result.Error.Code == MM.Common.ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == MM.Common.ErrorCode.EXIST)
                    return true;
                else
                    return false;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.CheckPatientExist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.CheckPatientExist"));
                return false;
            }
        }

        private bool CheckTemplate(IWorksheet ws, ref string message)
        {
            string s = string.Format("Sheet {0} không đúng định dạng nên không được nhập", ws.Name) + System.Environment.NewLine;
            try
            {
                if (ws.Cells[0, 0].Text.ToLower().Trim() != "code" ||
                        ws.Cells[0, 1].Text.ToLower().Trim() != "fullname" ||
                        ws.Cells[0, 2].Text.ToLower().Trim() != "dob" ||
                        ws.Cells[0, 3].Text.ToLower().Trim() != "sex" ||
                        ws.Cells[0, 4].Text.ToLower().Trim() != "mobile")
                {
                    message += s;
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                message += s;
                Utility.WriteToTraceLog(ex.Message);
                return false;
            }
        }

        private string ReFormatDate(string type, string value)
        {
            string sRet = value;
            if (type.ToLower().StartsWith("m"))
            {
                char[] split = new char[]{'/'};
                string[] sTemp = value.Split(split, StringSplitOptions.None);
                if (sTemp.Count() == 3)
                {
                    sRet = sTemp[1] + "/" + sTemp[0] + "/" + sTemp[2];
                }
                else
                {
                    sRet = value;
                }
            }
            return sRet;
        }

        private void ImportPatientFromExcel()
        {
            bool generateCode = true;
            string message="Nhập dữ liệu từ Excel hoàn tất." + System.Environment.NewLine;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (File.Exists(_fileName))
                {
                    //how to import CODE
                    string sChoice = "Chọn 'Đồng ý' để tạo mã (code) tự động" + System.Environment.NewLine + System.Environment.NewLine + "Chọn 'Không' nếu muốn giữ nguyên mã (code) trong file Excel";
                    if(MsgBox.Question("Nhap Benh Nhan", sChoice)==DialogResult.No)
                    {
                        generateCode = false;
                    }
                    //end code
                    IWorkbook book = SpreadsheetGear.Factory.GetWorkbook(_fileName);
                    
                    foreach (IWorksheet sheet in book.Worksheets)
                    {
                        if(CheckTemplate(sheet, ref message))
                        {
                            int RowCount = sheet.UsedRange.RowCount + 1;
                            int ColumnCount = sheet.UsedRange.ColumnCount + 1;
                            for (int i = 1; i < RowCount; i++)
                            {
                                Contact ct = new Contact();
                                Patient p = new Patient();
                                PatientHistory ph = new PatientHistory();
                                string sCode = "VGH";
                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    string curCellValue = string.Empty;
                                    if (sheet.Cells[i, j] != null && sheet.Cells[i, j].Value != null && sheet.Cells[i, j].Text!=null)
                                    {
                                        //get text, no need to get value
                                        //curCellValue = sheet.Cells[i, j].Value.ToString().Trim();
                                        curCellValue = sheet.Cells[i, j].Text.Trim();
                                    }
                                    //process NULL text in excel 
                                    if (curCellValue.ToUpper() == "NULL")
                                        curCellValue = "";

                                    string sType = sheet.Cells[i, j].NumberFormat.Trim();
                                    if (sType.Contains("yy"))
                                    {
                                        curCellValue = ReFormatDate(sType, curCellValue);
                                    }
                                    //process "'" character
                                    curCellValue = curCellValue.Replace("'", "''");
                                    if(sheet.Cells[i,j].Font.Name.ToLower().IndexOf("vni")==0)
                                        curCellValue = Utility.ConvertVNI2Unicode(curCellValue);
                                    if (sheet.Cells[0, j].Value != null && sheet.Cells[0, j].Value.ToString().Trim() != null)
                                    {
                                        switch (sheet.Cells[0, j].Value.ToString().Trim().ToLower())
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
                                                    ct.Dob = dt;
                                                break;

                                            case "gender":
                                            case "sex":
                                                string s = curCellValue.ToLower();
                                                if (s != string.Empty)
                                                {
                                                    if (s == "nam" || s == "male" || s == "m")
                                                        ct.Gender = (byte)Gender.Male;
                                                    else
                                                    {
                                                        ct.Gender = (byte)Gender.Female;
                                                        if (s == "mf")
                                                        {
                                                            ph.Tinh_Trang_Gia_Dinh = "Có gia đình";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ct.Gender =(byte) Gender.None;
                                                }
                                                break;
                                                //
                                            case "code":
                                            case "companycode":
                                                if (curCellValue != string.Empty)
                                                    sCode = curCellValue;
                                                break;
                                            case "mobile":
                                                if (curCellValue != string.Empty)
                                                    ct.Mobile = curCellValue;
                                                break;
                                            default:
                                                break;
                                        }
                                    }

                                }
                                //add patient to database only if they have surname and firstname
                                if (ct.FirstName != null && ct.FirstName != string.Empty && ct.SurName != null && ct.SurName != string.Empty & ct.Gender.HasValue)
                                {
                                    if (ct.FullName == null || ct.FullName == string.Empty)
                                        ct.FullName = ct.SurName + " " + ct.FirstName;
                                    ct.Source = Path.GetFileName(_fileName);

                                    if (!generateCode && IsExistFileNum(sCode)) continue;

                                    if (!IsPatientExist(ct.FullName, ct.DobStr, ct.Gender.Value, ct.Source))
                                    {
                                        ct.CreatedBy = Guid.Parse(Global.UserGUID);
                                        ct.CreatedDate = DateTime.Now;
                                        int iCount = GetPatientQuantity();
                                        iCount++;
                                        if (generateCode)
                                            p.FileNum = Utility.GetCode("VHG", iCount, 5);
                                        else
                                            p.FileNum = sCode;

                                        Result result = PatientBus.InsertPatient(ct, p, ph);
                                        if (!result.IsOK)
                                        {
                                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.InsertPatient"), IconType.Error);
                                            Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.InsertPatient"));
                                            SearchAsThread();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                MsgBox.Show(Application.ProductName, message, IconType.Information);
                SearchAsThread();
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void OnTaoHoSo()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn tạo hồ sơ cho những bệnh nhân mà bạn đánh dấu ?") == DialogResult.Yes)
                {
                    if (OnClearHoSo(checkedRows))
                    {
                        TaoHoSoAsThread(checkedRows);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần tạo hồ sơ.", IconType.Information);
        }

        private bool OnClearHoSo(List<DataRow> checkedRows)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                foreach (DataRow row in checkedRows)
                {
                    string maBenhNhan = row["FileNum"].ToString();
                    string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
                    string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
                    if (Directory.Exists(path))
                    {
                        string[] files = Directory.GetFiles(path);
                        foreach (string file in files)
                        {
                            File.Delete(file);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
                return false;
            }
        }

        private void TaoHoSoAsThread(List<DataRow> checkedRows)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnTaoHoSoProc), checkedRows);
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

        private bool OnTaoHoSo(List<DataRow> checkedRows)
        {
            
            foreach (DataRow row in checkedRows)
            {
                string patientGUID = row["PatientGUID"].ToString();

                Result result = ReportBus.GetNgayKhamCuoiCung(patientGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetNgayKhamCuoiCung"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetNgayKhamCuoiCung"));
                    return false;
                }

                List<DateTime> ngayKhamCuoiCungList = (List<DateTime>)result.QueryResult;

                string maBenhNhan = row["FileNum"].ToString();
                string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
                string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string ketQuaKhamTongQuatFileName = string.Format("{0}\\Temp\\KetQuaKhamSucKhoeTongQuat.xls", Application.StartupPath);
                if (!Exports.ExportExcel.ExportKetQuaKhamTongQuatToExcel(ketQuaKhamTongQuatFileName, row, ngayKhamCuoiCungList))
                    return false;

                string pdfFileName = string.Format("{0}\\KetQuaKhamSucKhoeTongQuat_{1}.pdf", path, DateTime.Now.ToString("ddMMyyyyHHmmssms"));
                if (!Exports.ConvertExcelToPDF.Convert(ketQuaKhamTongQuatFileName, pdfFileName, Global.PageSetupConfig.GetPageSetup(Const.KhamSucKhoeTongQuatTemplate)))
                    return false;

                //Kết quả nội soi
                if (ngayKhamCuoiCungList[5] != Global.MinDateTime)
                {
                    DateTime fromDate = new DateTime(ngayKhamCuoiCungList[5].Year, ngayKhamCuoiCungList[5].Month, ngayKhamCuoiCungList[5].Day, 0, 0, 0);
                    DateTime toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);

                    result = KetQuaNoiSoiBus.GetKetQuaNoiSoiList2(patientGUID, fromDate, toDate);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList2"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList2"));
                        return false;
                    }

                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            LoaiNoiSoi loaiNoiSoi = (LoaiNoiSoi)Convert.ToInt32(dr["LoaiNoiSoi"]);
                            string ketQuaNoiSoiFileName = string.Format("{0}\\Temp\\KetQuaNoiSoi.xls", Application.StartupPath);

                            PageSetup p = null;

                            switch (loaiNoiSoi)
                            {
                                case LoaiNoiSoi.Tai:
                                    if (!Exports.ExportExcel.ExportKetQuaNoiSoiTaiToExcel(ketQuaNoiSoiFileName, row, dr))
                                        return false;
                                    p = Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiTemplate);
                                    break;
                                case LoaiNoiSoi.Mui:
                                    if (!Exports.ExportExcel.ExportKetQuaNoiSoiMuiToExcel(ketQuaNoiSoiFileName, row, dr))
                                        return false;
                                    p = Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiMuiTemplate);
                                    break;
                                case LoaiNoiSoi.Hong_ThanhQuan:
                                    if (!Exports.ExportExcel.ExportKetQuaNoiSoiHongThanhQuanToExcel(ketQuaNoiSoiFileName, row, dr))
                                        return false;
                                    p = Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiHongThanhQuanTemplate);
                                    break;
                                case LoaiNoiSoi.TaiMuiHong:
                                    if (!Exports.ExportExcel.ExportKetQuaNoiSoiTaiMuiHongToExcel(ketQuaNoiSoiFileName, row, dr))
                                        return false;
                                    p = Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiMuiHongTemplate);
                                    break;
                                case LoaiNoiSoi.TongQuat:
                                    if (!Exports.ExportExcel.ExportKetQuaNoiSoiTongQuatToExcel(ketQuaNoiSoiFileName, row, dr))
                                        return false;
                                    p = Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTongQuatTemplate);
                                    break;
                            }

                            pdfFileName = string.Format("{0}\\KetQuaNoiSoi_{1}_{2}.xls", path, loaiNoiSoi.ToString(), DateTime.Now.ToString("ddMMyyyyHHmmssms"));
                            if (!Exports.ConvertExcelToPDF.Convert(ketQuaNoiSoiFileName, pdfFileName, p))
                                return false;
                        }
                    }
                }

                //Kết quả soi CTC
                if (ngayKhamCuoiCungList[6] != Global.MinDateTime)
                {
                    DateTime fromDate = new DateTime(ngayKhamCuoiCungList[6].Year, ngayKhamCuoiCungList[6].Month, ngayKhamCuoiCungList[6].Day, 0, 0, 0);
                    DateTime toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);

                    result = KetQuaSoiCTCBus.GetKetQuaSoiCTCList2(patientGUID, fromDate, toDate);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList2"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList2"));
                        return false;
                    }

                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string ketQuaSoiCTCFileName = string.Format("{0}\\Temp\\KetQuaSoiCTC.xls", Application.StartupPath);
                            if (!Exports.ExportExcel.ExportKetQuaSoiCTCToExcel(ketQuaSoiCTCFileName, row, dr))
                                return false;

                            pdfFileName = string.Format("{0}\\KetQuaSoiCTC_{1}.xls", path, DateTime.Now.ToString("ddMMyyyyHHmmssms"));
                            if (!Exports.ConvertExcelToPDF.Convert(ketQuaSoiCTCFileName, pdfFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaSoiCTCTemplate)))
                                return false;
                        }
                    }
                }

                //Kết quả siêu âm
                if (ngayKhamCuoiCungList[7] != Global.MinDateTime)
                {
                    DateTime fromDate = new DateTime(ngayKhamCuoiCungList[7].Year, ngayKhamCuoiCungList[7].Month, ngayKhamCuoiCungList[7].Day, 0, 0, 0);
                    DateTime toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);

                    result = SieuAmBus.GetKetQuaSieuAmList2(patientGUID, fromDate, toDate);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList2"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList2"));
                        return false;
                    }

                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        MethodInvoker method = delegate
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string tenSieuAm = Utility.ConvertToUnSign(dr["TenSieuAm"].ToString());
                                string ketQuaSieuAmFileName = string.Format("{0}\\KetQuaSieuAm_{1}_{2}.pdf", path, tenSieuAm,
                                    DateTime.Now.ToString("ddMMyyyyHHmmssms"));

                                _uPrintKetQuaSieuAm.PatientRow = row;
                                _uPrintKetQuaSieuAm.ExportToPDF(dr, ketQuaSieuAmFileName);

                            }
                        };
                        if (InvokeRequired) BeginInvoke(method);
                        else method.Invoke();
                    }
                }
            }

            return true;
        }

        private void OnXemHoSo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!Directory.Exists(Global.HoSoPath))
                    Directory.CreateDirectory(Global.HoSoPath);

                System.Diagnostics.Process.Start(Global.HoSoPath);
            }
            catch (Exception ex)
            {
                MM.MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private bool CheckHoSo(List<DataRow> checkedRows)
        {
            foreach (DataRow row in checkedRows)
            {
                string maBenhNhan = row["FileNum"].ToString();
                string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
                string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
                if (!Directory.Exists(path))
                {
                    MsgBox.Show(Application.ProductName, string.Format("Bệnh nhân: '{0}' chưa có hồ sơ để upload. Vui lòng kiểm tra lại.", row["FullName"].ToString()), 
                        IconType.Information);
                    return false;
                }

                string[] files = Directory.GetFiles(path);
                if (files == null || files.Length <= 0)
                {
                    MsgBox.Show(Application.ProductName, string.Format("Bệnh nhân: '{0}' chưa có hồ sơ để upload. Vui lòng kiểm tra lại.", row["FullName"].ToString()),
                        IconType.Information);
                    return false;
                }
            }

            return true;
        }

        private void OnUploadHoSoAsThread(List<DataRow> checkedRows)
        {
            try
            {
                if (!CheckHoSo(checkedRows)) return;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnUploadHoSoProc), checkedRows);
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

        private void OnUploadHoSo(List<DataRow> checkedRows)
        {
            foreach (DataRow row in checkedRows)
            {
                string maBenhNhan = row["FileNum"].ToString();
                string tenBenhNhan = Utility.ConvertToUnSign(row["FullName"].ToString());
                string tenBenhNhan2 = Utility.ConvertToUnSign2(row["FullName"].ToString());
                string password = Utility.GeneratePassword(5);
                string path = string.Format("{0}\\{1}@{2}", Global.HoSoPath, maBenhNhan, tenBenhNhan);
                if (!Directory.Exists(path)) continue;
                string[] files = Directory.GetFiles(path);
                if (files == null || files.Length <= 0) continue;

                Result result = UserBus.GetUser(maBenhNhan);
                if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.GetUser"), IconType.Information);
                    Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.GetUser"));
                    return;
                }

                if (result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    result = UserBus.AddUser(maBenhNhan, password);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.AddUser"), IconType.Information);
                        Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.AddUser"));
                        return;
                    }   
                }
                else
                {
                    password = (result.QueryResult as User).Password;
                }

                result = MySQLHelper.InsertUser(maBenhNhan, password, tenBenhNhan2);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("MySQLHelper.InsertUser"), IconType.Information);
                    Utility.WriteToTraceLog(result.GetErrorAsString("MySQLHelper.InsertUser"));
                    return;
                }

                foreach (string file in files)
                {
                    string remoteFileName = string.Format("{0}/{1}/{2}", Global.FTPFolder, maBenhNhan, Path.GetFileName(file));
                    result = FTP.UploadFile(Global.FTPConnectionInfo, file, remoteFileName);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("FTP.UploadFile"), IconType.Information);
                        Utility.WriteToTraceLog(result.GetErrorAsString("FTP.UploadFile"));
                    }
                }
            }
        }

        private void OnTaoMatKhau(List<DataRow> checkedRows)
        {
            foreach (DataRow row in checkedRows)
            {
                string fileNum = row["FileNum"].ToString();
                Result result = UserBus.GetUser(fileNum);
                if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.GetUser"), IconType.Information);
                    Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.GetUser"));
                    return;
                }

                if (result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    string password = Utility.GeneratePassword(5);
                    result = UserBus.AddUser(fileNum, password);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.AddUser"), IconType.Information);
                        Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.AddUser"));
                        return;
                    }
                }
            }
        }

        private void OnTaoMatKhauAsThread(List<DataRow> checkedRows)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnTaoMatKhauProc), checkedRows);
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

        private void OnPrint()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            
            if (checkedRows.Count > 0)
            {
                bool onlyEmail = false;
                if (MsgBox.Question(Application.ProductName, "Bạn chỉ muốn xuất những bệnh nhân có địa chỉ email hay xuất hết ?\nNhấn 'Đồng ý' để xuất những bệnh nhân có email.\nNhấn 'Không' để xuất tất cả bệnh nhân.") == DialogResult.Yes)
                    onlyEmail = true;

                string exportFileName = string.Format("{0}\\Temp\\DanhSachBenhNhan2.xls", Application.StartupPath);
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    if (ExportExcel.ExportDanhSachBenhNhan2ToExcel(exportFileName, checkedRows, onlyEmail))
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachBenhNhan2Template));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }

        private void OnExportToExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                bool onlyEmail = false;
                if (MsgBox.Question(Application.ProductName, "Bạn chỉ muốn xuất những bệnh nhân có địa chỉ email hay xuất hết ?\nNhấn 'Đồng ý' để xuất những bệnh nhân có email.\nNhấn 'Không' để xuất tất cả bệnh nhân.") == DialogResult.Yes)
                    onlyEmail = true;

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ExportExcel.ExportDanhSachBenhNhan2ToExcel(dlg.FileName, checkedRows, onlyEmail))
                        return;
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần xuất Excel.", IconType.Information);
        }

        private void OnVaoPhongCho()
        {
            List<string> addedPatientList = new List<string>();
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            foreach (DataRow row in checkedRows)
            {
                string patientGUID = row["PatientGUID"].ToString();
                addedPatientList.Add(patientGUID);
            }

            if (addedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn thêm những bệnh nhân đã đánh dấu vào phòng chờ ?") == DialogResult.Yes)
                {
                    Result result = PhongChoBus.AddPhongCho(addedPatientList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhongChoBus.AddPhongCho"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhongChoBus.AddPhongCho"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần đưa vào phòng chờ.", IconType.Information);
        }

        private void UploadHoSo()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn upload hồ sơ ?") == DialogResult.Yes)
                {
                    OnUploadHoSoAsThread(checkedRows);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần upload hồ sơ.", IconType.Information);
        }

        private void TaoMatKhauHoSo()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn tạo mật khẩu hồ sơ cho những bệnh nhân bạn đánh dấu ?") == DialogResult.Yes)
                    OnTaoMatKhauAsThread(checkedRows);
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần tạo mật khẩu hồ sơ.", IconType.Information);
        }

        private void OnXuatMaVach()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        string fileNum = row["FileNum"].ToString();
                        string fullName = row["FullName"].ToString();
                        fullName = Utility.ConvertToUnSign(row["FullName"].ToString());
                        string fileName = string.Format("{0}\\{1}-{2}.png", dlg.SelectedPath, fileNum, fullName);
                        barCode.BarCode = fileNum;
                        barCode.SaveImage(fileName);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần xuất mã vạch.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnUploadHoSo_Click(object sender, EventArgs e)
        {
            UploadHoSo();
        }

        private void btnXemHoSo_Click(object sender, EventArgs e)
        {
            OnXemHoSo();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddPatient();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeletePatient();
        }

        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            OnOpentPatient();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string patientGUID = row["PatientGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictPatient.ContainsKey(patientGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictPatient.ContainsKey(patientGUID))
                    {
                        _dictPatient.Remove(patientGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void dgDocStaff_DoubleClick(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OnImportExcel();
        }

        private void dgPatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgPatient.DataSource as DataTable;
                DataTable newDataSource = null;

                if (_isAscending)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }
                
                dgPatient.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnVaoPhongCho();
        }

        private void btnTaoHoSo_Click(object sender, EventArgs e)
        {
            OnTaoHoSo();
        }

        private void dgPatient_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgPatient.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictPatient.ContainsKey(patientGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictPatient.ContainsKey(patientGUID))
                {
                    _dictPatient.Remove(patientGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void btnTaoMatKhau_Click(object sender, EventArgs e)
        {
            TaoMatKhauHoSo();
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportToExcel();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddPatient();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeletePatient();
        }

        private void importExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnImportExcel();
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportToExcel();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void moBenhNhanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnOpentPatient();
        }

        private void vaoPhongChoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnVaoPhongCho();
        }

        private void taoHoSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnTaoHoSo();
        }

        private void xemHoSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnXemHoSo();
        }

        private void uploadHoSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadHoSo();   
        }

        private void taoMatKhauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaoMatKhauHoSo();
        }

        private void btnXuatMaVach_Click(object sender, EventArgs e)
        {
            OnXuatMaVach();
        }

        private void xuatMaVachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnXuatMaVach();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                OnDisplayPatientList();
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
                OnDisplayPatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnTaoHoSoProc(object state)
        {
            try
            {
                List<DataRow> checkedRows = (List<DataRow>)state;
                if (OnTaoHoSo(checkedRows))
                {
                    if (MsgBox.Question(Application.ProductName, "Bạn có muốn upload hồ sơ ?") == DialogResult.Yes)
                        OnUploadHoSo(checkedRows);
                }
            }
            catch (Exception e)
            {
                base.HideWaiting();
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnUploadHoSoProc(object state)
        {
            try
            {
                List<DataRow> checkedRows = (List<DataRow>)state;
                OnUploadHoSo(checkedRows);
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                this.HideWaiting();
            }
        }

        private void OnTaoMatKhauProc(object state)
        {
            try
            {
                List<DataRow> checkedRows = (List<DataRow>)state;
                OnTaoMatKhau(checkedRows);
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                this.HideWaiting();
            }
        }
        #endregion

        

        
    }
}
