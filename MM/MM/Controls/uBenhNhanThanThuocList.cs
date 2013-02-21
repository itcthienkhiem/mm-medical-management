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

namespace MM.Controls
{
    public partial class uBenhNhanThanThuocList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private string _fileName = string.Empty;
        private bool _isAscending = true;
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private int _type = 0;
        #endregion

        #region Constructor
        public uBenhNhanThanThuocList()
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
            btnVaoPhongCho.Enabled = Global.AllowAddPhongCho;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            moBenhNhanToolStripMenuItem.Enabled = AllowOpenPatient;
            vaoPhongChoToolStripMenuItem.Enabled = Global.AllowAddPhongCho;
        }

        public void ClearData()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;

                dgPatient.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
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
                Result result = PatientBus.GetBenhNhanThanThuocList(_name, _type);
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
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetBenhNhanThanThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetBenhNhanThanThuocList"));
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

        private void OnAddBenhNhanThanThuoc()
        {
            dlgSelectPatient dlg = new dlgSelectPatient(PatientSearchType.BenhNhanKhongThanThuoc);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                List<DataRow> patientRows = dlg.CheckedPatientRows;
                if (patientRows == null || patientRows.Count <= 0) return;

                Result result = PatientBus.InsertBenhNhanThanThuoc(patientRows);
                if (result.IsOK)
                {
                    SearchAsThread();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.DeletePatient"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.DeletePatient"));
                }
            }
        }

        private void OnDeleteBenhNhanThanThuoc()
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
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bệnh nhân thân thuộc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PatientBus.DeleteBenhNhanThanThuoc(deletedPatientList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgPatient.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;
                        foreach (string key in deletedPatientList)
                        {
                            DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", key));
                            if (rows != null && rows.Length > 0)
                                dt.Rows.Remove(rows[0]);
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
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân thân thuộc cần xóa.", IconType.Information);
        }

        private void OnVaoPhongCho()
        {
            List<string> addedPatientList = new List<string>();
            foreach (DataRow row in _dictPatient.Values.ToList())
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
        #endregion

        #region Window Event Handlers
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddBenhNhanThanThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteBenhNhanThanThuoc();
        }

        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            OnOpentPatient();
        }

        private void btnVaoPhongCho_Click(object sender, EventArgs e)
        {
            OnVaoPhongCho();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditPatient();
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

        private void dgPatient_DoubleClick(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddBenhNhanThanThuoc();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteBenhNhanThanThuoc();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void moBenhNhanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnOpentPatient();
        }

        private void vaoPhongChoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnVaoPhongCho();
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
        #endregion
    }
}
