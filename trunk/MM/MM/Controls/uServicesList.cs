﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Dialogs;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uServicesList : uBase
    {
        #region Members
        DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uServicesList()
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
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            priceDataGridViewTextBoxColumn.Visible = Global.AllowShowServiePrice;
        }

        public void ClearData()
        {
            dgService.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServicesListProc));
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

        private void OnDisplayServicesList()
        {
            Result result = ServicesBus.GetServicesList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    //dgService.DataSource = result.QueryResult; 
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchDichVu();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
            }
        }

        private void OnAddService()
        {
            if (_dataSource == null) return;
            dlgAddServices dlg = new dlgAddServices();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;//dgService.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ServiceGUID"] = dlg.Service.ServiceGUID.ToString();
                newRow["Code"] = dlg.Service.Code;
                newRow["Name"] = dlg.Service.Name;
                newRow["Price"] = dlg.Service.Price;
                newRow["Description"] = dlg.Service.Description;
                newRow["EnglishName"] = dlg.Service.EnglishName;
                newRow["Type"] = dlg.Service.Type;
                newRow["TypeStr"] = dlg.Service.Type == (byte)ServiceType.LamSang ? "Lâm sàng" : "Cận lâm sàng";
                newRow["StaffType"] = dlg.Service.StaffType.Value;
                newRow["StaffTypeStr"] = Utility.ParseStaffTypeEnumToName((StaffType)dlg.Service.StaffType.Value);

                if (dlg.Service.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Service.CreatedDate;

                if (dlg.Service.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Service.CreatedBy.ToString();

                if (dlg.Service.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Service.UpdatedDate;

                if (dlg.Service.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Service.UpdatedBy.ToString();

                if (dlg.Service.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Service.DeletedDate;

                if (dlg.Service.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Service.DeletedBy.ToString();

                newRow["Status"] = dlg.Service.Status;
                dt.Rows.Add(newRow);
                //SelectLastedRow();
                OnSearchDichVu();
            }
        }

        private void SelectLastedRow()
        {
            dgService.CurrentCell = dgService[1, dgService.RowCount - 1];
            dgService.Rows[dgService.RowCount - 1].Selected = true;
        }

        private DataRow GetDataRow(string serviceGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("ServiceGUID = '{0}'", serviceGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditService()
        {
            if (_dataSource == null) return;

            if (dgService.SelectedRows == null || dgService.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ.", IconType.Information);
                return;
            }

            string serviceGUID = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row["ServiceGUID"].ToString();
            DataRow drService = GetDataRow(serviceGUID);
            if (drService == null) return;
            //DataRow drService = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddServices dlg = new dlgAddServices(drService);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drService["Code"] = dlg.Service.Code;
                drService["Name"] = dlg.Service.Name;
                drService["Price"] = dlg.Service.Price;
                drService["Description"] = dlg.Service.Description;
                drService["EnglishName"] = dlg.Service.EnglishName;
                drService["Type"] = dlg.Service.Type;
                drService["TypeStr"] = dlg.Service.Type == (byte)ServiceType.LamSang ? "Lâm sàng" : "Cận lâm sàng";
                drService["StaffType"] = dlg.Service.StaffType.Value;
                drService["StaffTypeStr"] = Utility.ParseStaffTypeEnumToName((StaffType)dlg.Service.StaffType.Value);

                if (dlg.Service.CreatedDate.HasValue)
                    drService["CreatedDate"] = dlg.Service.CreatedDate;

                if (dlg.Service.CreatedBy.HasValue)
                    drService["CreatedBy"] = dlg.Service.CreatedBy.ToString();

                if (dlg.Service.UpdatedDate.HasValue)
                    drService["UpdatedDate"] = dlg.Service.UpdatedDate;

                if (dlg.Service.UpdatedBy.HasValue)
                    drService["UpdatedBy"] = dlg.Service.UpdatedBy.ToString();

                if (dlg.Service.DeletedDate.HasValue)
                    drService["DeletedDate"] = dlg.Service.DeletedDate;

                if (dlg.Service.DeletedBy.HasValue)
                    drService["DeletedBy"] = dlg.Service.DeletedBy.ToString();

                drService["Status"] = dlg.Service.Status;

                OnSearchDichVu();
            }
        }

        private void UpdateChecked()
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string serviceGUID1 = row1["ServiceGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("ServiceGUID='{0}'", serviceGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnDeleteService()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedServiceList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = _dataSource;//dgService.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    //deletedServiceList.Add(row["ServiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    List<string> noteList = new List<string>();

                    foreach (DataRow row in deletedRows)
                    {
                        string serviceCode = row["Code"].ToString();
                        string serviceGUID = row["ServiceGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(serviceCode, 2);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedServiceList.Add(serviceGUID);
                        }
                    }

                    if (deletedServiceList.Count > 0)
                    {
                        Result result = ServicesBus.DeleteServices(deletedServiceList, noteList);
                        if (result.IsOK)
                        {
                            foreach (DataRow row in deletedRows)
                            {
                                _dataSource.Rows.Remove(row);
                                //dt.Rows.Remove(row);
                            }

                            OnSearchDichVu();
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.DeleteServices"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.DeleteServices"));
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }

        private void OnSearchDichVu()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtTenDichVu.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("Name")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgService.DataSource = newDataSource;
                if (dgService.RowCount > 0) dgService.Rows[0].Selected = true;
                return;
            }

            string str = txtTenDichVu.Text.ToLower();

            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                        where p.Field<string>("Name") != null &&
                            p.Field<string>("Name").Trim() != string.Empty &&
                            p.Field<string>("Name").ToLower().IndexOf(str) >= 0
                        //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                        //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                        orderby p.Field<string>("Name")
                        select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgService.DataSource = newDataSource;
                return;
            }

            dgService.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditService();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void dgService_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditService();
        }

        private void txtTenDichVu_TextChanged(object sender, EventArgs e)
        {
            OnSearchDichVu();
        }

        private void txtTenDichVu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index < dgService.RowCount - 1)
                    {
                        index++;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayServicesListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayServicesList();
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