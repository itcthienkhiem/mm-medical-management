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
        }

        public void ClearData()
        {
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
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
                //SelectLastedRow();
                OnSearchPatient();
            }
        }

        private void SelectLastedRow()
        {
            dgPatient.CurrentCell = dgPatient[1, dgPatient.RowCount - 1];
            dgPatient.Rows[dgPatient.RowCount - 1].Selected = true;
        }

        private DataRow GetDataRow(string patientGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("PatientGUID = '{0}'", patientGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
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
            }
        }

        private void OnDeletePatient()
        {
            if (_dataSource == null) return;
            UpdateChecked();
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
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchPatient();
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

        private void UpdateChecked()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("PatientGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchPatient()
        {
            UpdateChecked();
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
                             //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                           //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
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
                           //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                           //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
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
                        where //(p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                        //str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) &&
                        p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 &&
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

            //HomePhone
            /*results = (from p in _dataSource.AsEnumerable()
                        where p.Field<string>("HomePhone") != null &&
                        p.Field<string>("HomePhone").Trim() != string.Empty &&
                        (p.Field<string>("HomePhone").ToLower().IndexOf(str) >= 0 ||
                        str.IndexOf(p.Field<string>("HomePhone").ToLower()) >= 0)
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //WorkPhone
            results = (from p in _dataSource.AsEnumerable()
                        where p.Field<string>("WorkPhone") != null &&
                            p.Field<string>("WorkPhone").Trim() != string.Empty &&
                            (p.Field<string>("WorkPhone").ToLower().IndexOf(str) >= 0 ||
                        str.IndexOf(p.Field<string>("WorkPhone").ToLower()) >= 0)
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //Mobile
            results = (from p in _dataSource.AsEnumerable()
                        where p.Field<string>("Mobile") != null &&
                            p.Field<string>("Mobile").Trim() != string.Empty &&
                            (p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0 ||
                        str.IndexOf(p.Field<string>("Mobile").ToLower()) >= 0)
                        orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                        select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }*/
                

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
        #endregion

        #region Window Event Handlers
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
            }
        }

        private void dgDocStaff_DoubleClick(object sender, EventArgs e)
        {
            //if (!AllowEdit) return;
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
            UpdateChecked();
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
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
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
        #endregion

        
    }
}
