using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uChiDinhList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private DataRow _patientRow2 = null;
        private Hashtable _htDichVuChiDinh = null;
        private bool _flag = true;
        private bool _isChuyenBenhAn = false;
        #endregion

        #region Constructor
        public uChiDinhList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsChuyenBenhAn
        {
            get { return _isChuyenBenhAn; }
            set 
            { 
                _isChuyenBenhAn = value;
                btnChuyen.Visible = _isChuyenBenhAn;
                btnAdd.Visible = !_isChuyenBenhAn;
                btnEdit.Visible = !_isChuyenBenhAn;
                btnDelete.Visible = !_isChuyenBenhAn;
                btnConfirm.Visible = !_isChuyenBenhAn;

                if (_isChuyenBenhAn)
                    this.ContextMenuStrip = ctmAction2;
            }
        }

        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }

        public DataRow PatientRow2
        {
            get { return _patientRow2; }
            set { _patientRow2 = value; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = Global.AllowAddChiDinh;
            btnEdit.Enabled = Global.AllowEditChiDinh;
            btnDelete.Enabled = Global.AllowDeleteChiDinh;
            btnConfirm.Enabled = Global.AllowConfirmChiDinh;

            addToolStripMenuItem.Enabled = Global.AllowAddChiDinh;
            editToolStripMenuItem.Enabled = Global.AllowEditChiDinh;
            deleteToolStripMenuItem.Enabled = Global.AllowDeleteChiDinh;
            xacNhanDVChiDinhToolStripMenuItem.Enabled = Global.AllowConfirmChiDinh;

            btnChuyen.Enabled = AllowChuyenKetQuaKham;
            chuyenToolStripMenuItem.Enabled = AllowChuyenKetQuaKham;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayChiDinhListProc));
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

        public void ClearChiDinhData()
        {
            DataTable dt = dgChiDinh.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgChiDinh.DataSource = null;
            }
        }

        private void OnDisplayChiDinhList()
        {
            if (_patientRow == null) return;
            string patientGUID = _patientRow["PatientGUID"].ToString();
            Result result = ChiDinhBus.GetChiDinhList(patientGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _flag = false;
                    ClearChiDinhData();
                    dgChiDinh.DataSource = result.QueryResult;
                    _flag = true;
                    OnGetDichVuChiDinh();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetChiDinhList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiDinhList"));
            }
        }

        public void OnGetDichVuChiDinh()
        {
            if (_htDichVuChiDinh == null) _htDichVuChiDinh = new Hashtable();
            else _htDichVuChiDinh.Clear();

            foreach (DataGridViewRow row in dgChiDinh.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                string chiDinhGUID = r["ChiDinhGUID"].ToString();
                Result result = ChiDinhBus.GetDichVuChiDinhList(chiDinhGUID);
                if (result.IsOK)
                {
                    List<DichVuChiDinhView> dichVuChiDinhList = (List<DichVuChiDinhView>)result.QueryResult;
                    _htDichVuChiDinh.Add(chiDinhGUID, dichVuChiDinhList);

                    if (dichVuChiDinhList != null && dichVuChiDinhList.Count > 0)
                    {
                        (row.Cells["Checked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                        row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    }
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetDichVuChiDinhList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetDichVuChiDinhList"));
                    return;
                }
            }

            OnDisplayChiTietChiDinh();
        }

        public void ClearChiTietChiDinh()
        {
            DataTable dt = dgChiTiet.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgChiTiet.DataSource = null;
            }
        }

        private void OnDisplayChiTietChiDinh()
        {
            string chiDinhGUID = string.Empty;
            if (dgChiDinh.SelectedRows == null || dgChiDinh.SelectedRows.Count <= 0)
                chiDinhGUID = Guid.Empty.ToString();
            else
            {
                DataRow row = (dgChiDinh.SelectedRows[0].DataBoundItem as DataRowView).Row;
                chiDinhGUID = row["ChiDinhGUID"].ToString();
            }

            Result result = ChiDinhBus.GetChiTietChiDinhList(chiDinhGUID);
            if (result.IsOK)
            {
                ClearChiTietChiDinh();
                dgChiTiet.DataSource = result.QueryResult;

                if (_htDichVuChiDinh.ContainsKey(chiDinhGUID))
                {
                    List<DichVuChiDinhView> dichVuChiDinhList = (List<DichVuChiDinhView>)_htDichVuChiDinh[chiDinhGUID];
                    foreach (DataGridViewRow row in dgChiTiet.Rows)
                    {
                        DataRow r = (row.DataBoundItem as DataRowView).Row;
                        string serviceGUID = r["ServiceGUID"].ToString();
                        foreach (var dvcd in dichVuChiDinhList)
                        {
                            if (serviceGUID == dvcd.ServiceGUID.ToString())
                            {
                                (row.Cells["ChiTietChiDinhChecked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                                row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList"));
            }
        }

        private void UpdateDichVuChiDinh()
        {
            if (dgChiDinh.SelectedRows == null || dgChiDinh.SelectedRows.Count <= 0)
                return;
            DataRow dr = (dgChiDinh.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string chiDinhGUID = dr["ChiDinhGUID"].ToString();

            if (_htDichVuChiDinh.ContainsKey(chiDinhGUID))
            {
                List<DichVuChiDinhView> dichVuChiDinhList = (List<DichVuChiDinhView>)_htDichVuChiDinh[chiDinhGUID];
                foreach (DataGridViewRow row in dgChiTiet.Rows)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    string serviceGUID = r["ServiceGUID"].ToString();
                    foreach (var dvcd in dichVuChiDinhList)
                    {
                        if (serviceGUID == dvcd.ServiceGUID.ToString())
                        {
                            (row.Cells["ChiTietChiDinhChecked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                            row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                            break;
                        }
                    }
                }
            }
        }

        private void OnAddChiDinh()
        {
            dlgAddChiDinh dlg = new dlgAddChiDinh(_patientRow);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgChiDinh.DataSource as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ChiDinhGUID"] = dlg.ChiDinh.ChiDinhGUID.ToString();
                newRow["MaChiDinh"] = dlg.ChiDinh.MaChiDinh;
                newRow["NgayChiDinh"] = dlg.ChiDinh.NgayChiDinh;
                newRow["BacSiChiDinhGUID"] = dlg.ChiDinh.BacSiChiDinhGUID.ToString();
                newRow["FullName"] = dlg.TenBacSiChiDinh;
                newRow["BenhNhanGUID"] = dlg.ChiDinh.BenhNhanGUID.ToString();

                if (dlg.ChiDinh.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.ChiDinh.CreatedDate;

                if (dlg.ChiDinh.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.ChiDinh.CreatedBy.ToString();

                if (dlg.ChiDinh.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.ChiDinh.UpdatedDate;

                if (dlg.ChiDinh.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.ChiDinh.UpdatedBy.ToString();

                if (dlg.ChiDinh.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.ChiDinh.DeletedDate;

                if (dlg.ChiDinh.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.ChiDinh.DeletedBy.ToString();

                newRow["Status"] = dlg.ChiDinh.Status;

                dt.Rows.Add(newRow);

                _htDichVuChiDinh.Add(dlg.ChiDinh.ChiDinhGUID.ToString(), new List<DichVuChiDinhView>());

                //SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgChiDinh.CurrentCell = dgChiDinh[1, dgChiDinh.RowCount - 1];
            dgChiDinh.Rows[dgChiDinh.RowCount - 1].Selected = true;
        }

        private void OnEditChiDinh()
        {
            if (_isChuyenBenhAn) return;
            if (dgChiDinh.SelectedRows == null || dgChiDinh.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 chỉ định.", IconType.Information);
                return;
            }

            DataRow drChiDinh = (dgChiDinh.SelectedRows[0].DataBoundItem as DataRowView).Row;
            List<DichVuChiDinhView> dichVuChiDinhList = (List<DichVuChiDinhView>)_htDichVuChiDinh[drChiDinh["ChiDinhGUID"].ToString()];
            DataTable dtChiTiet = dgChiTiet.DataSource as DataTable;
            dlgAddChiDinh dlg = new dlgAddChiDinh(_patientRow, drChiDinh, dtChiTiet, dichVuChiDinhList);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                drChiDinh["MaChiDinh"] = dlg.ChiDinh.MaChiDinh;
                drChiDinh["NgayChiDinh"] = dlg.ChiDinh.NgayChiDinh;
                drChiDinh["BacSiChiDinhGUID"] = dlg.ChiDinh.BacSiChiDinhGUID.ToString();
                drChiDinh["FullName"] = dlg.TenBacSiChiDinh;
                drChiDinh["BenhNhanGUID"] = dlg.ChiDinh.BenhNhanGUID.ToString();

                if (dlg.ChiDinh.CreatedDate.HasValue)
                    drChiDinh["CreatedDate"] = dlg.ChiDinh.CreatedDate;

                if (dlg.ChiDinh.CreatedBy.HasValue)
                    drChiDinh["CreatedBy"] = dlg.ChiDinh.CreatedBy.ToString();

                if (dlg.ChiDinh.UpdatedDate.HasValue)
                    drChiDinh["UpdatedDate"] = dlg.ChiDinh.UpdatedDate;

                if (dlg.ChiDinh.UpdatedBy.HasValue)
                    drChiDinh["UpdatedBy"] = dlg.ChiDinh.UpdatedBy.ToString();

                if (dlg.ChiDinh.DeletedDate.HasValue)
                    drChiDinh["DeletedDate"] = dlg.ChiDinh.DeletedDate;

                if (dlg.ChiDinh.DeletedBy.HasValue)
                    drChiDinh["DeletedBy"] = dlg.ChiDinh.DeletedBy.ToString();

                drChiDinh["Status"] = dlg.ChiDinh.Status;
                OnDisplayChiTietChiDinh();
            }
        }

        private void OnDeleteChiDinh()
        {
            List<string> deletedChiDinhList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgChiDinh.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedChiDinhList.Add(row["ChiDinhGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedChiDinhList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những chỉ định mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ChiDinhBus.DeleteChiDinhs(deletedChiDinhList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.DeleteChiDinhs"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.DeleteChiDinhs"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những chỉ định cần xóa.", IconType.Information);
        }

        private void OnConfirmDichVuChiDinh()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgChiTiet.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xác nhận những dịch vụ chỉ định mà bạn đánh dấu ?") == DialogResult.Yes)
                {
                    DataRow drChiDinh = (dgChiDinh.SelectedRows[0].DataBoundItem as DataRowView).Row;

                    foreach (DataRow row in checkedRows)
                    {
                        dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientRow["PatientGUID"].ToString());
                        dlg.ServiceGUID = row["ServiceGUID"].ToString();
                        dlg.BacSiChiDinhGUID = drChiDinh["BacSiChiDinhGUID"].ToString();
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            if (dlg.ServiceHistory.ServiceHistoryGUID == Guid.Empty) return;

                            DichVuChiDinh dichVuChiDinh = new DichVuChiDinh();
                            //(dgChiDinh.SelectedRows[0].DataBoundItem as DataRowView).Row["ChiDinhGUID"].ToString();
                            dichVuChiDinh.ServiceHistoryGUID = dlg.ServiceHistory.ServiceHistoryGUID;
                            dichVuChiDinh.ChiTietChiDinhGUID = Guid.Parse(row["ChiTietChiDinhGUID"].ToString());
                            dichVuChiDinh.CreatedDate = DateTime.Now;
                            dichVuChiDinh.CraetedBy = Guid.Parse(Global.UserGUID);
                            dichVuChiDinh.Status = (byte)Status.Actived;

                            Result result = ChiDinhBus.InsertDichVuChiDinh(dichVuChiDinh);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.InsertDichVuChiDinh"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.InsertDichVuChiDinh"));
                                return;
                            }
                        }
                    }

                    OnGetDichVuChiDinh();
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ chỉ định cần xác nhận.", IconType.Information);
        }

        private void OnChuyenKetQuaKham()
        {
            if (!_isChuyenBenhAn) return;

            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgChiDinh.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (dgChiDinh.RowCount <= 0 || deletedRows == null || deletedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 chỉ định cần chuyển.", IconType.Information);
                return;
            }

            if (_patientRow2 == null)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân nhận chỉ định chuyển đến.", IconType.Information);
                return;
            }

            string fileNum = _patientRow2["FileNum"].ToString();
            if (MsgBox.Question(Application.ProductName, string.Format("Bạn có muốn chuyển những chỉ định đã chọn đến bệnh nhân: '{0}'?", fileNum)) == DialogResult.No) return;

            Result result = ChiDinhBus.ChuyenBenhAn(_patientRow2["PatientGUID"].ToString(), deletedRows);
            if (result.IsOK)
                DisplayAsThread();
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.ChuyenBenhAn"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.ChuyenBenhAn"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dgChiDinh_SelectionChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            OnDisplayChiTietChiDinh();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (dgChiDinh.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgChiDinh.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["Checked"] as DataGridViewDisableCheckBoxCell;
                if (cell.Enabled)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    r["Checked"] = chkChecked.Checked;
                }
            }
        }

        private void chkChiTietChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (dgChiTiet.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["ChiTietChiDinhChecked"] as DataGridViewDisableCheckBoxCell;
                if (cell.Enabled)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    r["Checked"] = chkChiTietChecked.Checked;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddChiDinh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditChiDinh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteChiDinh();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            OnConfirmDichVuChiDinh();
        }

        private void dgChiDinh_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditChiDinh();
        }

        private void dgChiDinh_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OnGetDichVuChiDinh();
        }

        private void dgChiTiet_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateDichVuChiDinh();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddChiDinh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditChiDinh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteChiDinh();
        }

        private void xacNhanDVChiDinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnConfirmDichVuChiDinh();
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            OnChuyenKetQuaKham();
        }

        private void chuyenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnChuyenKetQuaKham();
        }
        #endregion

        #region Working Thread
        private void OnDisplayChiDinhListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayChiDinhList();
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
