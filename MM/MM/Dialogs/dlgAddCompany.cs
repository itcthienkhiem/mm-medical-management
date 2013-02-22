using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Exports;

namespace MM.Dialogs
{
    public partial class dlgAddCompany : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Company _company = new Company();
        private List<string> _addedPatients = new List<string>();
        private List<string> _deletedPatients = new List<string>();
        private List<DataRow> _deletedPatientRows = new List<DataRow>();
        private bool _isAscending = true;
        private bool _flag = true;
        private DataRow _drCompany = null;
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddCompany()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddCompany(DataRow drCompany, bool allowEdit)
        {
            InitializeComponent();
            _isNew  = false;
            _allowEdit = allowEdit;
            this.Text = "Sua cong ty";
            _drCompany = drCompany;
        }
        #endregion

        #region Properties
        public Company Company
        {
            get { return _company; }
            set { _company = value; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = CompanyBus.GetCompanyCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaCongTy.Text = Utility.GetCode("CTY", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyCount"));
            }
        }
       
        private void DisplayInfo(DataRow drCompany)
        {
            try
            {
                txtMaCongTy.Text = drCompany["MaCty"] as string;
                txtTenCongTy.Text = drCompany["TenCty"] as string;
                txtDiaChi.Text = drCompany["DiaChi"] as string;
                txtDienThoai.Text = drCompany["DienThoai"] as string;
                txtFax.Text = drCompany["Fax"] as string;
                txtWebsite.Text = drCompany["Website"] as string;

                _company.CompanyGUID = Guid.Parse(drCompany["CompanyGUID"].ToString());

                if (drCompany["CreatedDate"] != null && drCompany["CreatedDate"] != DBNull.Value)
                    _company.CreatedDate = Convert.ToDateTime(drCompany["CreatedDate"]);

                if (drCompany["CreatedBy"] != null && drCompany["CreatedBy"] != DBNull.Value)
                    _company.CreatedBy = Guid.Parse(drCompany["CreatedBy"].ToString());

                if (drCompany["UpdatedDate"] != null && drCompany["UpdatedDate"] != DBNull.Value)
                    _company.UpdatedDate = Convert.ToDateTime(drCompany["UpdatedDate"]);

                if (drCompany["UpdatedBy"] != null && drCompany["UpdatedBy"] != DBNull.Value)
                    _company.UpdatedBy = Guid.Parse(drCompany["UpdatedBy"].ToString());

                if (drCompany["DeletedDate"] != null && drCompany["DeletedDate"] != DBNull.Value)
                    _company.DeletedDate = Convert.ToDateTime(drCompany["DeletedDate"]);

                if (drCompany["DeletedBy"] != null && drCompany["DeletedBy"] != DBNull.Value)
                    _company.DeletedBy = Guid.Parse(drCompany["DeletedBy"].ToString());

                _company.Status = Convert.ToByte(drCompany["Status"]);

                DisplayMembersAsThread(_company.CompanyGUID.ToString());

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    tabControlPanel1.Enabled = _allowEdit;
                    btnAdd.Enabled = _allowEdit;
                    btnDelete.Enabled = _allowEdit;

                    addToolStripMenuItem.Enabled = _allowEdit;
                    deleteToolStripMenuItem.Enabled = _allowEdit;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void DisplayMembersAsThread(string companyGUID)
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayMembersProc), companyGUID);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayMembers(string companyGUID)
        {
            Result result = CompanyBus.GetCompanyMemberList(companyGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgMembers.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyMemberList"));
            }
        }

        private bool CheckInfo()
        {
            if (txtMaCongTy.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã công ty.", IconType.Information);
                txtMaCongTy.Focus();
                return false;
            }

            if (txtTenCongTy.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên công ty.", IconType.Information);
                txtTenCongTy.Focus();
                return false;
            }

            string comGUID = _isNew ? string.Empty : _company.CompanyGUID.ToString();
            Result result = CompanyBus.CheckCompanyExistCode(comGUID, txtMaCongTy.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã công ty này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaCongTy.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.CheckCompanyExistCode"), IconType.Error);
                return false;
            }

            foreach (string patientGUID in _addedPatients)
            {
                result = CompanyBus.CheckMemberExist(patientGUID);

                if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    if (result.Error.Code == ErrorCode.EXIST)
                    {
                        string fullName = GetFullName(patientGUID);
                        MsgBox.Show(this.Text, string.Format("Bệnh nhân: '{0}' đã thuộc 1 công ty khác.", fullName), IconType.Information);

                        DataTable dt = dgMembers.DataSource as DataTable;
                        if (dt != null)
                        {
                            DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", patientGUID));
                            if (rows != null && rows.Length > 0)
                            {
                                _addedPatients.Remove(rows[0]["PatientGUID"].ToString());
                                dt.Rows.Remove(rows[0]);
                            }
                        }

                        return false;
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.CheckMemberExist"), IconType.Error);
                    return false;
                }
            }

            return true;
        }

        private string GetFullName(string patientGUID)
        {
            string fullName = string.Empty;
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt != null)
            {
                DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", patientGUID));
                if (rows != null && rows.Length > 0)
                    fullName = rows[0]["FullName"].ToString();
            }

            return fullName;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                _company.MaCty = txtMaCongTy.Text;
                _company.TenCty = txtTenCongTy.Text;
                _company.DiaChi = txtDiaChi.Text;
                _company.Dienthoai = txtDienThoai.Text;
                _company.Fax = txtFax.Text;
                _company.Website = txtWebsite.Text;
                _company.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _company.CreatedDate = DateTime.Now;
                    _company.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _company.UpdatedDate = DateTime.Now;
                    _company.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                Result result = CompanyBus.InsertCompany(_company, _addedPatients, _deletedPatients);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.InsertCompany"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.InsertCompany"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnAddMember()
        {
            dlgMembers dlg = new dlgMembers(_addedPatients, _deletedPatientRows);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.CheckedMembers;
                DataTable dataSource = dgMembers.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["PatientGUID"] = patientGUID;
                        newRow["FileNum"] = row["FileNum"];
                        newRow["FullName"] = row["FullName"];
                        newRow["DobStr"] = row["DobStr"];
                        newRow["GenderAsStr"] = row["GenderAsStr"];
                        dataSource.Rows.Add(newRow);

                        if (!_addedPatients.Contains(patientGUID))
                            _addedPatients.Add(patientGUID);

                        _deletedPatients.Remove(patientGUID);
                        foreach (DataRow r in _deletedPatientRows)
                        {
                            if (r["PatientGUID"].ToString() == patientGUID)
                            {
                                _deletedPatientRows.Remove(r);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void OnDeleteMember()
        {
            List<string> deletedMemList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgMembers.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedMemList.Add(row["PatientGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedMemList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhân viên mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string patientGUID = row["PatientGUID"].ToString();
                        if (!_deletedPatients.Contains(patientGUID))
                        {
                            _deletedPatients.Add(patientGUID);

                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            _deletedPatientRows.Add(r);
                        }

                        _addedPatients.Remove(patientGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những nhân viên cần xóa.", IconType.Information);
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgMembers.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    checkedRows.Add(row);
            }

            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\DanhSachBenhNhan.xls", Application.StartupPath);
                if (ExportExcel.ExportDanhSachBenhNhanToExcel(exportFileName, checkedRows))
                    try
                    {
                        if (isPreview)
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachBenhNhanTemplate));
                        else
                        {
                            if (_printDialog.ShowDialog() == DialogResult.OK)
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachBenhNhanTemplate));
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                    }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }

        private void OnExportExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgMembers.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    checkedRows.Add(row);
            }

            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachBenhNhanToExcel(dlg.FileName, checkedRows);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddCompany_Load(object sender, EventArgs e)
        {
            if (_isNew)
                DisplayMembersAsThread(Guid.Empty.ToString());
            else
                DisplayInfo(_drCompany);
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddMember();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteMember();
        }

        private void dlgAddCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else
            {
                if (!_flag)
                {
                    _flag = true;
                    return;
                }

                if (_allowEdit)
                {
                    if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin công ty ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (CheckInfo())
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            SaveInfoAsThread();
                        }
                        else
                            e.Cancel = true;
                    }
                }
            }
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '4' &&
                e.KeyChar != '5' && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8' && e.KeyChar != '9' &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2' && e.KeyChar != '3' && e.KeyChar != '4' &&
                e.KeyChar != '5' && e.KeyChar != '6' && e.KeyChar != '7' && e.KeyChar != '8' && e.KeyChar != '9' &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        private void dgMembers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgMembers.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return;
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

                dgMembers.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void dgMembers_DoubleClick(object sender, EventArgs e)
        {
            if (_isNew) return;
            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0) return;

            DataRow patientRow = (dgMembers.SelectedRows[0].DataBoundItem as DataRowView).Row;
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

                _flag = false;
                this.Close();
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddMember();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteMember();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayMembersProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayMembers(state.ToString());
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
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
