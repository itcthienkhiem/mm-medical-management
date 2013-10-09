using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uCompanyList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictCongTys = new Dictionary<string, DataRow>();
        private string _name = string.Empty;
        #endregion

        #region Constructor
        public uCompanyList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtTenCongTy.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayCompanyListProc));
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
                _name = txtTenCongTy.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        public void ClearData()
        {
            DataTable dt = dgCompany.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgCompany.DataSource = null;
            }
        }

        private void OnDisplayCompanyList()
        {
            lock (ThisLock)
            {
                Result result = CompanyBus.GetCompanyList(_name);
                if (result.IsOK)
                {
                    dgCompany.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();
                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgCompany.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetCompanyList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["CompanyGUID"].ToString();
                if (_dictCongTys.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddCompany()
        {
            dlgAddCompany dlg = new dlgAddCompany();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
                //DataTable dt = dgCompany.DataSource as DataTable;
                //if (dt == null) return;
                //DataRow newRow = dt.NewRow();
                //newRow["Checked"] = false;
                //newRow["CompanyGUID"] = dlg.Company.CompanyGUID.ToString();
                //newRow["MaCty"] = dlg.Company.MaCty;
                //newRow["TenCty"] = dlg.Company.TenCty;
                //newRow["MaSoThue"] = dlg.Company.MaSoThue;
                //newRow["DiaChi"] = dlg.Company.DiaChi;
                //newRow["Dienthoai"] = dlg.Company.Dienthoai;
                //newRow["Fax"] = dlg.Company.Fax;
                //newRow["Website"] = dlg.Company.Website;

                //if (dlg.Company.CreatedDate.HasValue)
                //    newRow["CreatedDate"] = dlg.Company.CreatedDate;

                //if (dlg.Company.CreatedBy.HasValue)
                //    newRow["CreatedBy"] = dlg.Company.CreatedBy.ToString();

                //if (dlg.Company.UpdatedDate.HasValue)
                //    newRow["UpdatedDate"] = dlg.Company.UpdatedDate;

                //if (dlg.Company.UpdatedBy.HasValue)
                //    newRow["UpdatedBy"] = dlg.Company.UpdatedBy.ToString();

                //if (dlg.Company.DeletedDate.HasValue)
                //    newRow["DeletedDate"] = dlg.Company.DeletedDate;

                //if (dlg.Company.DeletedBy.HasValue)
                //    newRow["DeletedBy"] = dlg.Company.DeletedBy.ToString();

                //newRow["Status"] = dlg.Company.Status;
                //dt.Rows.Add(newRow);
            }
        }

        private void OnEditCompany()
        {
            if (dgCompany.SelectedRows == null || dgCompany.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 công ty.", IconType.Information);
                return;
            }

            DataRow drCom = (dgCompany.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddCompany dlg = new dlgAddCompany(drCom, AllowEdit);
            dlg.OnOpenPatientEvent += new OpenPatientHandler(dlg_OnOpenPatient);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
                //drCom["MaCty"] = dlg.Company.MaCty;
                //drCom["TenCty"] = dlg.Company.TenCty;
                //drCom["MaSoThue"] = dlg.Company.MaSoThue;
                //drCom["DiaChi"] = dlg.Company.DiaChi;
                //drCom["Dienthoai"] = dlg.Company.Dienthoai;
                //drCom["Fax"] = dlg.Company.Fax;
                //drCom["Website"] = dlg.Company.Website;

                //if (dlg.Company.CreatedDate.HasValue)
                //    drCom["CreatedDate"] = dlg.Company.CreatedDate;

                //if (dlg.Company.CreatedBy.HasValue)
                //    drCom["CreatedBy"] = dlg.Company.CreatedBy.ToString();

                //if (dlg.Company.UpdatedDate.HasValue)
                //    drCom["UpdatedDate"] = dlg.Company.UpdatedDate;

                //if (dlg.Company.UpdatedBy.HasValue)
                //    drCom["UpdatedBy"] = dlg.Company.UpdatedBy.ToString();

                //if (dlg.Company.DeletedDate.HasValue)
                //    drCom["DeletedDate"] = dlg.Company.DeletedDate;

                //if (dlg.Company.DeletedBy.HasValue)
                //    drCom["DeletedBy"] = dlg.Company.DeletedBy.ToString();

                //drCom["Status"] = dlg.Company.Status;
            }
        }

        private void OnDeleteCompany()
        {
            
            
            if (_dictCongTys == null) return;
            List<DataRow> deletedRows = _dictCongTys.Values.ToList<DataRow>();
            List<string> deletedComList = new List<string>();
            foreach (DataRow row in deletedRows)
            {
                deletedComList.Add(row["CompanyGUID"].ToString());
            }

            if (deletedComList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những công ty mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyBus.DeleteCompany(deletedComList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgCompany.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedComList)
                        {
                            DataRow[] rows = dt.Select(string.Format("CompanyGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);
                        }

                        _dictCongTys.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.DeleteCompany"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.DeleteCompany"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những công ty cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgCompany.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string companyGUID = row["CompanyGUID"].ToString();

                if (chkChecked.Checked)
                {
                    if (!_dictCongTys.ContainsKey(companyGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictCongTys.Add(companyGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictCongTys.ContainsKey(companyGUID))
                    {
                        _dictCongTys.Remove(companyGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("CompanyGUID='{0}'", companyGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddCompany();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditCompany();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteCompany();
        }

        private void dgCompany_DoubleClick(object sender, EventArgs e)
        {
            OnEditCompany();
        }

        private void dlg_OnOpenPatient(DataRow patientRow)
        {
            base.RaiseOpentPatient(patientRow);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddCompany();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditCompany();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteCompany();
        }

        private void txtTenCongTy_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void dgCompany_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgCompany.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgCompany.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string companyGUID = row["CompanyGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictCongTys.ContainsKey(companyGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictCongTys.Add(companyGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictCongTys.ContainsKey(companyGUID))
                {
                    _dictCongTys.Remove(companyGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("CompanyGUID='{0}'", companyGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void txtTenCongTy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgCompany.Focus();

                if (dgCompany.SelectedRows != null && dgCompany.SelectedRows.Count > 0)
                {
                    int index = dgCompany.SelectedRows[0].Index;
                    if (index < dgCompany.RowCount - 1)
                    {
                        index++;
                        dgCompany.CurrentCell = dgCompany[1, index];
                        dgCompany.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgCompany.Focus();

                if (dgCompany.SelectedRows != null && dgCompany.SelectedRows.Count > 0)
                {
                    int index = dgCompany.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgCompany.CurrentCell = dgCompany[1, index];
                        dgCompany.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayCompanyListProc(object state)
        {
            try
            {
                OnDisplayCompanyList();
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
                OnDisplayCompanyList();
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
