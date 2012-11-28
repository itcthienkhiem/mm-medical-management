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

namespace MM.Controls
{
    public partial class uPatientList : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private string _fileName = string.Empty;
        private bool _isAscending = true;
        private Dictionary<string, DataRow> _dictPatient = null;
        #endregion

        #region Constructor
        public uPatientList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public object DataSource
        {
            get
            {
                DisplayAsThread();
                return _dataSource;
            }

            set { _dataSource = (DataTable)value; }
        }

        public Dictionary<string, DataRow> DictPatient
        {
            get { return _dictPatient; }
            set { _dictPatient = value; }
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
        }

        public void ClearData()
        {
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
            }

            if (_dictPatient != null)
            {
                _dictPatient.Clear();
                _dictPatient = null;
            }

            ClearDataSource();
        }

        private void ClearDataSource()
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

        private void OnDisplayPatientList()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    _dataSource = result.QueryResult as DataTable;

                    if (_dictPatient == null) _dictPatient = new Dictionary<string, DataRow>();
                    foreach (DataRow row in _dataSource.Rows)
                    {
                        string patientGUID = row["PatientGUID"].ToString();
                        if (!_dictPatient.ContainsKey(patientGUID))
                            _dictPatient.Add(patientGUID, row);
                        else
                            _dictPatient[patientGUID] = row;
                    }

                    OnSearchPatient();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void OnAddPatient()
        {
            if (_dataSource == null) return;
            dlgAddPatient dlg = new dlgAddPatient();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = _dataSource;

                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["PatientGUID"] = dlg.Patient.PatientGUID.ToString();
                newRow["ContactGUID"] = dlg.Contact.ContactGUID.ToString();
                newRow["FileNum"] = dlg.Patient.FileNum;
                newRow["FullName"] = dlg.Contact.FullName;
                newRow["SurName"] = dlg.Contact.SurName;
                newRow["MiddleName"] = dlg.Contact.MiddleName;
                newRow["FirstName"] = dlg.Contact.FirstName;
                newRow["KnownAs"] = dlg.Contact.KnownAs;
                newRow["PreferredName"] = dlg.Contact.PreferredName;
                newRow["Gender"] = dlg.Contact.Gender;

                if (dlg.Contact.Gender == 0) newRow["GenderAsStr"] = "Nam";//dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                else if (dlg.Contact.Gender == 1) newRow["GenderAsStr"] = "Nữ";
                else newRow["GenderAsStr"] = "Không xác định";

                newRow["DobStr"] = dlg.Contact.DobStr;
                newRow["IdentityCard"] = dlg.Contact.IdentityCard;
                newRow["HomePhone"] = dlg.Contact.HomePhone;
                newRow["WorkPhone"] = dlg.Contact.WorkPhone;
                newRow["Mobile"] = dlg.Contact.Mobile;
                newRow["Email"] = dlg.Contact.Email;
                newRow["FAX"] = dlg.Contact.FAX;
                newRow["Address"] = dlg.Contact.Address;
                newRow["Ward"] = dlg.Contact.Ward;
                newRow["District"] = dlg.Contact.District;
                newRow["City"] = dlg.Contact.City;
                newRow["Occupation"] = dlg.Contact.Occupation;
                newRow["CompanyName"] = dlg.Contact.CompanyName;

                if (dlg.Contact.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Contact.CreatedDate;

                if (dlg.Contact.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                if (dlg.Contact.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Contact.UpdatedDate;

                if (dlg.Contact.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                if (dlg.Contact.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Contact.DeletedDate;

                if (dlg.Contact.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Contact.DeletedBy.ToString();

                if (dlg.Patient.NgayKham != null && dlg.Patient.NgayKham.HasValue)
                    newRow["NgayKham"] = dlg.Patient.NgayKham.Value;

                //Patient History
                newRow["PatientHistoryGUID"] = dlg.PatientHistory.PatientHistoryGUID.ToString();
                newRow["Di_Ung_Thuoc"] = dlg.PatientHistory.Di_Ung_Thuoc.Value;
                if (dlg.PatientHistory.Di_Ung_Thuoc.Value)
                    newRow["Thuoc_Di_Ung"] = dlg.PatientHistory.Thuoc_Di_Ung;
                else
                    newRow["Thuoc_Di_Ung"] = string.Empty;

                newRow["Dot_Quy"] = dlg.PatientHistory.Dot_Quy.Value;
                newRow["Benh_Tim_Mach"] = dlg.PatientHistory.Benh_Tim_Mach.Value;
                newRow["Benh_Lao"] = dlg.PatientHistory.Benh_Lao.Value;
                newRow["Dai_Thao_Duong"] = dlg.PatientHistory.Dai_Thao_Duong.Value;
                newRow["Dai_Duong_Dang_Dieu_Tri"] = dlg.PatientHistory.Dai_Duong_Dang_Dieu_Tri.Value;
                newRow["Viem_Gan_B"] = dlg.PatientHistory.Viem_Gan_B.Value;
                newRow["Viem_Gan_C"] = dlg.PatientHistory.Viem_Gan_C.Value;
                newRow["Viem_Gan_Dang_Dieu_Tri"] = dlg.PatientHistory.Viem_Gan_Dang_Dieu_Tri.Value;
                newRow["Ung_Thu"] = dlg.PatientHistory.Ung_Thu.Value;
                if (dlg.PatientHistory.Ung_Thu.Value)
                    newRow["Co_Quan_Ung_Thu"] = dlg.PatientHistory.Co_Quan_Ung_Thu;
                else
                    newRow["Co_Quan_Ung_Thu"] = string.Empty;

                newRow["Dong_Kinh"] = dlg.PatientHistory.Dong_Kinh.Value;
                newRow["Hen_Suyen"] = dlg.PatientHistory.Hen_Suyen.Value;
                newRow["Benh_Khac"] = dlg.PatientHistory.Benh_Khac.Value;
                if (dlg.PatientHistory.Benh_Khac.Value)
                {
                    newRow["Benh_Gi"] = dlg.PatientHistory.Benh_Gi;
                    newRow["Thuoc_Dang_Dung"] = dlg.PatientHistory.Thuoc_Dang_Dung;
                }
                else
                {
                    newRow["Benh_Gi"] = string.Empty;
                    newRow["Thuoc_Dang_Dung"] = string.Empty;
                }

                newRow["Hut_Thuoc"] = dlg.PatientHistory.Hut_Thuoc.Value;
                newRow["Uong_Ruou"] = dlg.PatientHistory.Uong_Ruou.Value;
                newRow["Tinh_Trang_Gia_Dinh"] = dlg.PatientHistory.Tinh_Trang_Gia_Dinh;
                newRow["Chich_Ngua_Viem_Gan_B"] = dlg.PatientHistory.Chich_Ngua_Viem_Gan_B.Value;
                newRow["Chich_Ngua_Uon_Van"] = dlg.PatientHistory.Chich_Ngua_Uon_Van;
                newRow["Chich_Ngua_Cum"] = dlg.PatientHistory.Chich_Ngua_Cum;
                newRow["Dang_Co_Thai"] = dlg.PatientHistory.Dang_Co_Thai.Value;

                dt.Rows.Add(newRow);
                _dictPatient.Add(dlg.Patient.PatientGUID.ToString(), newRow);
                OnSearchPatient();
            }
        }

        private DataRow GetDataRow(string patientGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            if (_dictPatient == null) return null;
            if (!_dictPatient.ContainsKey(patientGUID)) return null;

            return _dictPatient[patientGUID];
            
            //DataRow[] rows = _dataSource.Select(string.Format("PatientGUID = '{0}'", patientGUID));
            //if (rows == null || rows.Length <= 0) return null;
            //return rows[0];
        }

        private void OnEditPatient()
        {
            if (_dataSource == null) return;

            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.", IconType.Information);
                return;
            }

            string patientGUID = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            DataRow drPatient = GetDataRow(patientGUID);
            if (drPatient == null) return;

            dlgAddPatient dlg = new dlgAddPatient(drPatient, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drPatient["FileNum"] = dlg.Patient.FileNum;
                drPatient["FullName"] = dlg.Contact.FullName;
                drPatient["SurName"] = dlg.Contact.SurName;
                drPatient["MiddleName"] = dlg.Contact.MiddleName;
                drPatient["FirstName"] = dlg.Contact.FirstName;
                drPatient["KnownAs"] = dlg.Contact.KnownAs;
                drPatient["PreferredName"] = dlg.Contact.PreferredName;
                drPatient["Gender"] = dlg.Contact.Gender;

                if (dlg.Contact.Gender == 0) drPatient["GenderAsStr"] = "Nam";//dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                else if (dlg.Contact.Gender == 1) drPatient["GenderAsStr"] = "Nữ";
                else drPatient["GenderAsStr"] = "Không xác định";

                drPatient["DobStr"] = dlg.Contact.DobStr;
                drPatient["IdentityCard"] = dlg.Contact.IdentityCard;
                drPatient["HomePhone"] = dlg.Contact.HomePhone;
                drPatient["WorkPhone"] = dlg.Contact.WorkPhone;
                drPatient["Mobile"] = dlg.Contact.Mobile;
                drPatient["Email"] = dlg.Contact.Email;
                drPatient["FAX"] = dlg.Contact.FAX;
                drPatient["Address"] = dlg.Contact.Address;
                drPatient["Ward"] = dlg.Contact.Ward;
                drPatient["District"] = dlg.Contact.District;
                drPatient["City"] = dlg.Contact.City;
                drPatient["Occupation"] = dlg.Contact.Occupation;
                drPatient["CompanyName"] = dlg.Contact.CompanyName;

                if (dlg.Patient.NgayKham != null && dlg.Patient.NgayKham.HasValue)
                    drPatient["NgayKham"] = dlg.Patient.NgayKham.Value;
                else
                    drPatient["NgayKham"] = DBNull.Value;

                if (dlg.Contact.CreatedDate.HasValue)
                    drPatient["CreatedDate"] = dlg.Contact.CreatedDate;

                if (dlg.Contact.CreatedBy.HasValue)
                    drPatient["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                if (dlg.Contact.UpdatedDate.HasValue)
                    drPatient["UpdatedDate"] = dlg.Contact.UpdatedDate;

                if (dlg.Contact.UpdatedBy.HasValue)
                    drPatient["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                if (dlg.Contact.DeletedDate.HasValue)
                    drPatient["DeletedDate"] = dlg.Contact.DeletedDate;

                if (dlg.Contact.DeletedBy.HasValue)
                    drPatient["DeletedBy"] = dlg.Contact.DeletedBy.ToString();

                //Patient History
                drPatient["Di_Ung_Thuoc"] = dlg.PatientHistory.Di_Ung_Thuoc.Value;
                if (dlg.PatientHistory.Di_Ung_Thuoc.Value)
                    drPatient["Thuoc_Di_Ung"] = dlg.PatientHistory.Thuoc_Di_Ung;
                else
                    drPatient["Thuoc_Di_Ung"] = string.Empty;

                drPatient["Dot_Quy"] = dlg.PatientHistory.Dot_Quy.Value;
                drPatient["Benh_Tim_Mach"] = dlg.PatientHistory.Benh_Tim_Mach.Value;
                drPatient["Benh_Lao"] = dlg.PatientHistory.Benh_Lao.Value;
                drPatient["Dai_Thao_Duong"] = dlg.PatientHistory.Dai_Thao_Duong.Value;
                drPatient["Dai_Duong_Dang_Dieu_Tri"] = dlg.PatientHistory.Dai_Duong_Dang_Dieu_Tri.Value;
                drPatient["Viem_Gan_B"] = dlg.PatientHistory.Viem_Gan_B.Value;
                drPatient["Viem_Gan_C"] = dlg.PatientHistory.Viem_Gan_C.Value;
                drPatient["Viem_Gan_Dang_Dieu_Tri"] = dlg.PatientHistory.Viem_Gan_Dang_Dieu_Tri.Value;
                drPatient["Ung_Thu"] = dlg.PatientHistory.Ung_Thu.Value;
                if (dlg.PatientHistory.Ung_Thu.Value)
                    drPatient["Co_Quan_Ung_Thu"] = dlg.PatientHistory.Co_Quan_Ung_Thu;
                else
                    drPatient["Co_Quan_Ung_Thu"] = string.Empty;

                drPatient["Dong_Kinh"] = dlg.PatientHistory.Dong_Kinh.Value;
                drPatient["Hen_Suyen"] = dlg.PatientHistory.Hen_Suyen.Value;
                drPatient["Benh_Khac"] = dlg.PatientHistory.Benh_Khac.Value;
                if (dlg.PatientHistory.Benh_Khac.Value)
                {
                    drPatient["Benh_Gi"] = dlg.PatientHistory.Benh_Gi;
                    drPatient["Thuoc_Dang_Dung"] = dlg.PatientHistory.Thuoc_Dang_Dung;
                }
                else
                {
                    drPatient["Benh_Gi"] = string.Empty;
                    drPatient["Thuoc_Dang_Dung"] = string.Empty;
                }

                drPatient["Hut_Thuoc"] = dlg.PatientHistory.Hut_Thuoc.Value;
                drPatient["Uong_Ruou"] = dlg.PatientHistory.Uong_Ruou.Value;
                drPatient["Tinh_Trang_Gia_Dinh"] = dlg.PatientHistory.Tinh_Trang_Gia_Dinh;
                drPatient["Chich_Ngua_Viem_Gan_B"] = dlg.PatientHistory.Chich_Ngua_Viem_Gan_B.Value;
                drPatient["Chich_Ngua_Uon_Van"] = dlg.PatientHistory.Chich_Ngua_Uon_Van;
                drPatient["Chich_Ngua_Cum"] = dlg.PatientHistory.Chich_Ngua_Cum;
                drPatient["Dang_Co_Thai"] = dlg.PatientHistory.Dang_Co_Thai.Value;

                OnSearchPatient();
                RaiseEditPatient(drPatient);
            }
        }

        private void OnDeletePatient()
        {
            if (_dataSource == null) return;
            List<string> deletedPatientList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    deletedPatientList.Add(patientGUID);
                    deletedRows.Add(row);
                }
            }

            if (deletedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PatientBus.DeletePatient(deletedPatientList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dictPatient.Remove(row["PatientGUID"].ToString());
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchPatient();
                        RaiseDeletePatient(deletedPatientList);
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

            string patientGUID = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            DataRow drPatient = GetDataRow(patientGUID);
            base.RaiseOpentPatient(drPatient);
        }

        private void OnSearchPatient()
        {
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            ClearDataSource();

            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName") 
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgPatient.DataSource = newDataSource;
                if (dgPatient.RowCount > 0) dgPatient.Rows[0].Selected = true;
                _isAscending = true;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }

            string str = txtSearchPatient.Text.ToLower();

            newDataSource = _dataSource.Clone();

            if (chkMaBenhNhan.Checked)
            {
                //FileNum
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("FileNum") != null &&
                             p.Field<string>("FileNum").Trim() != string.Empty &&
                             p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                    return;
                }
            }

            if (chkTheoSoDienThoai.Checked)
            {
                //FileNum
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("Mobile") != null &&
                             p.Field<string>("Mobile").Trim() != string.Empty &&
                             p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                    return;
                }
            }

            //FullName
            results = (from p in _dataSource.AsEnumerable()
                        where p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 &&
                        p.Field<string>("FullName") != null &&
                        p.Field<string>("FullName").Trim() != string.Empty
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();

                
            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }

            dgPatient.DataSource = newDataSource;
            lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
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
                                    if (!IsPatientExist(ct.FullName, ct.DobStr, ct.Gender.Value, ct.Source))
                                    {
                                        ct.CreatedBy = Guid.Parse(Global.UserGUID);
                                        ct.CreatedDate = DateTime.Now;
                                        int iCount = GetPatientQuantity();
                                        iCount++;
                                        if (generateCode)
                                        {
                                            p.FileNum = Utility.GetCode(sCode, iCount, 5);
                                        }
                                        else
                                        {
                                            p.FileNum = sCode;
                                        }

                                        Result result = PatientBus.InsertPatient(ct, p, ph);
                                        if (!result.IsOK)
                                        {
                                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.InsertPatient"), IconType.Error);
                                            Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.InsertPatient"));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                MsgBox.Show(Application.ProductName, message, IconType.Information);
                OnDisplayPatientList();
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        public void UpdatePatients(DataTable dtPatient)
        {
            if (_dataSource == null) return;
            if (_dictPatient == null) return;

            List<string> deletedKeys = new List<string>();
            foreach (DataRow row in dtPatient.Rows)
            {
                string patientGUID = row["PatientGUID"].ToString();
                bool isDelete = Convert.ToBoolean(row["Archived"]);
                
                if (isDelete)
                {
                    string deletedGUID = row["DeletedBy"].ToString();
                    if (deletedGUID != Global.UserGUID.ToString())
                    {
                        if (_dictPatient.ContainsKey(patientGUID))
                        {
                            DataRow dr = _dictPatient[patientGUID];
                            deletedKeys.Add(patientGUID);
                            _dictPatient.Remove(patientGUID);
                            _dataSource.Rows.Remove(dr);
                        }
                    }
                }
                else
                {
                    string userGUID = string.Empty;
                    if (row["UpdatedBy"] != null && row["UpdatedBy"] != DBNull.Value)
                        userGUID = row["UpdatedBy"].ToString();
                    else
                        userGUID = row["CreatedBy"].ToString();

                    if (userGUID != Global.UserGUID.ToString())
                    {
                        if (!_dictPatient.ContainsKey(patientGUID))
                        {
                            _dataSource.ImportRow(row);
                            if (!_dictPatient.ContainsKey(patientGUID))
                                _dictPatient.Add(patientGUID, _dataSource.Rows[_dataSource.Rows.Count - 1]);
                            else
                                _dictPatient[patientGUID] = _dataSource.Rows[_dataSource.Rows.Count - 1];
                        }
                        else
                        {
                            DataRow dr = _dictPatient[patientGUID];
                            for (int i = 0; i < _dataSource.Columns.Count; i++)
                            {
                                dr[i] = row[i];
                            }

                            RaiseEditPatient(row);
                        }
                    }
                }
            }

            OnSearchPatient();

            if (deletedKeys.Count > 0)
                RaiseDeletePatient(deletedKeys);
        }

        private void OnTaoHoSo()
        {
            if (_dataSource == null) return;
            List<DataRow> checkedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    checkedRows.Add(row);
                }
            }

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
                string password = Utility.GeneratePassword();
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
                    string password = Utility.GeneratePassword();
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
        #endregion

        #region Window Event Handlers
        private void btnUploadHoSo_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;
            List<DataRow> checkedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    checkedRows.Add(row);
                }
            }

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
            if (_dataSource == null) return;
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;

                string patientGUID = row["PatientGUID"].ToString();
                _dictPatient[patientGUID]["Checked"] = chkChecked.Checked;
            }
        }

        private void dgDocStaff_DoubleClick(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
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
                List<DataRow> results = null;

                if (_isAscending)
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                               select p).ToList<DataRow>();
                }
                else
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                               select p).ToList<DataRow>();
                }


                DataTable newDataSource = dt.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgPatient.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;
            List<string> addedPatientList = new List<string>();
            foreach (DataRow row in _dataSource.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    addedPatientList.Add(patientGUID);
                }
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void btnTaoHoSo_Click(object sender, EventArgs e)
        {
            OnTaoHoSo();
        }

        private void dgPatient_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            if (_dataSource == null) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgPatient.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            _dictPatient[patientGUID]["Checked"] = isChecked;
        }

        private void btnTaoMatKhau_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;
            List<DataRow> checkedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn tạo mật khẩu hồ sơ cho những bệnh nhân bạn đánh dấu ?") == DialogResult.Yes)
                    OnTaoMatKhauAsThread(checkedRows);
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần tạo mật khẩu hồ sơ.", IconType.Information);
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
