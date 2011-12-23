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

namespace MM.Dialogs
{
    public partial class dlgAddToaThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _drToaThuoc = null;
        private ToaThuoc _toaThuoc = new ToaThuoc();
        private List<string> _deletedKeys = new List<string>();
        private bool _flag = true;
        #endregion

        #region Constructor
        public dlgAddToaThuoc()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddToaThuoc(DataRow drToaThuoc)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua toa thuoc";
            _drToaThuoc = drToaThuoc;
        }
        #endregion

        #region Properties
        public ToaThuoc ToaThuoc
        {
            get { return _toaThuoc; }
        }

        public string TenBacSi
        {
            get { return cboBacSi.Text; }
        }

        public string TenBenhNhan
        {
            get { return cboBenhNhan.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            dtpkNgayKeToa.Value = DateTime.Now;
            OnDisplayBacSi();
            OnDisplayBenhNhan();
            OnDisplayThuoc();
        }

        private void OnDisplayBacSi()
        {
            Result result = DocStaffBus.GetDocStaffList();
            if (result.IsOK)
                cboBacSi.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
            }
        }

        private void OnDisplayBenhNhan()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
                cboBenhNhan.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void OnDisplayThuoc()
        {
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
                thuocGUIDDataGridViewTextBoxColumn.DataSource = result.QueryResult;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
            }
        }

        private void OnGetChiTietToaThuoc(string toaThuocGUID)
        {
            Result result = KeToaBus.GetChiTietToaThuocList(toaThuocGUID);
            if (result.IsOK)
            {
                dgChiTiet.DataSource = result.QueryResult;
                RefreshNo();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"));
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = KeToaBus.GetToaThuocCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaToaThuoc.Text = Utility.GetCode("KT", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.GetToaThuocCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuocCount"));
            }
        }

        private void DisplayInfo(DataRow drToaThuoc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaToaThuoc.Text = drToaThuoc["MaToaThuoc"] as string;
                dtpkNgayKeToa.Value = Convert.ToDateTime(drToaThuoc["NgayKeToa"]);
                cboBacSi.SelectedValue = drToaThuoc["BacSiKeToa"].ToString();
                cboBenhNhan.SelectedValue = drToaThuoc["BenhNhan"].ToString();
                txtGhiChu.Text = drToaThuoc["Note"] as string;

                _toaThuoc.ToaThuocGUID = Guid.Parse(drToaThuoc["ToaThuocGUID"].ToString());

                if (drToaThuoc["CreatedDate"] != null && drToaThuoc["CreatedDate"] != DBNull.Value)
                    _toaThuoc.CreatedDate = Convert.ToDateTime(drToaThuoc["CreatedDate"]);

                if (drToaThuoc["CreatedBy"] != null && drToaThuoc["CreatedBy"] != DBNull.Value)
                    _toaThuoc.CreatedBy = Guid.Parse(drToaThuoc["CreatedBy"].ToString());

                if (drToaThuoc["UpdatedDate"] != null && drToaThuoc["UpdatedDate"] != DBNull.Value)
                    _toaThuoc.UpdatedDate = Convert.ToDateTime(drToaThuoc["UpdatedDate"]);

                if (drToaThuoc["UpdatedBy"] != null && drToaThuoc["UpdatedBy"] != DBNull.Value)
                    _toaThuoc.UpdatedBy = Guid.Parse(drToaThuoc["UpdatedBy"].ToString());

                if (drToaThuoc["DeletedDate"] != null && drToaThuoc["DeletedDate"] != DBNull.Value)
                    _toaThuoc.DeletedDate = Convert.ToDateTime(drToaThuoc["DeletedDate"]);

                if (drToaThuoc["DeletedBy"] != null && drToaThuoc["DeletedBy"] != DBNull.Value)
                    _toaThuoc.DeletedBy = Guid.Parse(drToaThuoc["DeletedBy"].ToString());

                _toaThuoc.Status = Convert.ToByte(drToaThuoc["Status"]);

                OnGetChiTietToaThuoc(_toaThuoc.ToaThuocGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtMaToaThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã toa thuốc.", IconType.Information);
                txtMaToaThuoc.Focus();
                return false;
            }

            if (cboBacSi.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ kê toa.", IconType.Information);
                cboBacSi.Focus();
                return false;
            }

            if (cboBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bệnh nhân.", IconType.Information);
                cboBenhNhan.Focus();
                return false;
            }


            string toaThuocGUID = _isNew ? string.Empty : _toaThuoc.ToaThuocGUID.ToString();
            Result result = KeToaBus.CheckToaThuocExistCode(toaThuocGUID, txtMaToaThuoc.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã toa thuốc này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaToaThuoc.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.CheckToaThuocExistCode"), IconType.Error);
                return false;
            }

            if (dgChiTiet.RowCount > 1)
            {
                for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
                {
                    DataGridViewRow row = dgChiTiet.Rows[i];
                    if (row.Cells[1].Value == null || row.Cells[1].Value == DBNull.Value || row.Cells[1].Value.ToString() == string.Empty)
                    {
                        MsgBox.Show(this.Text, "Vui lòng chọn thuốc để kê toa.", IconType.Information);
                        return false;
                    }
                }
            }

            if (dgChiTiet.RowCount > 2)
            {
                for (int i = 0; i < dgChiTiet.RowCount - 2; i++)
                {
                    DataGridViewRow row1 = dgChiTiet.Rows[i];
                    for (int j = i + 1; j < dgChiTiet.RowCount - 1; j++)
                    {
                        DataGridViewRow row2 = dgChiTiet.Rows[j];
                        if (row1.Cells[1].Value.ToString() == row2.Cells[1].Value.ToString())
                        {
                            string tenThuoc = GetTenThuoc(row1.Cells[1].Value.ToString());
                            MsgBox.Show(this.Text, string.Format("Thuốc '{0}' đã được kê toa rồi. Vui lòng chọn thuốc khác", tenThuoc), IconType.Information);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private string GetTenThuoc(string thuocGUID)
        {
            DataTable dt = thuocGUIDDataGridViewTextBoxColumn.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["TenThuoc"].ToString();

            return string.Empty;
        }

        private string GetDonViTinh(string thuocGUID)
        {
            DataTable dt = thuocGUIDDataGridViewTextBoxColumn.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["DonViTinh"].ToString();

            return string.Empty;
        }

        private void RefreshNo()
        {
            for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
            {
                dgChiTiet[0, i].Value = i + 1;
                if (dgChiTiet[3, i].Value == null || 
                    dgChiTiet[3, i].Value == DBNull.Value ||
                    dgChiTiet[3, i].Value.ToString() == "0")
                    dgChiTiet[3, i].Value = 1;

                if (dgChiTiet[4, i].Value == null ||
                    dgChiTiet[4, i].Value == DBNull.Value ||
                    dgChiTiet[4, i].Value.ToString() == "0")
                    dgChiTiet[4, i].Value = 1;

                if (dgChiTiet[5, i].Value == null ||
                    dgChiTiet[5, i].Value == DBNull.Value ||
                    dgChiTiet[5, i].Value.ToString() == "0")
                    dgChiTiet[5, i].Value = 1;
            }
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
                MethodInvoker method = delegate
                {
                    _toaThuoc.MaToaThuoc = txtMaToaThuoc.Text;
                    _toaThuoc.NgayKeToa = dtpkNgayKeToa.Value;
                    _toaThuoc.BacSiKeToa = Guid.Parse(cboBacSi.SelectedValue.ToString());
                    _toaThuoc.BenhNhan = Guid.Parse(cboBenhNhan.SelectedValue.ToString());
                    _toaThuoc.Note = txtGhiChu.Text;
                    _toaThuoc.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _toaThuoc.CreatedDate = DateTime.Now;
                        _toaThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _toaThuoc.UpdatedDate = DateTime.Now;
                        _toaThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    DataTable dt = dgChiTiet.DataSource as DataTable;
                    List<ChiTietToaThuoc> addedList = new List<ChiTietToaThuoc>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached) continue;
                        ChiTietToaThuoc cttt = new ChiTietToaThuoc();
                        if (row["ChiTietToaThuocGUID"] != null && row["ChiTietToaThuocGUID"] != DBNull.Value)
                        {
                            cttt.ChiTietToaThuocGUID = Guid.Parse(row["ChiTietToaThuocGUID"].ToString());
                            cttt.UpdatedDate = DateTime.Now;
                            cttt.UpdatedBy = Guid.Parse(Global.UserGUID);
                        }
                        else
                        {
                            cttt.CreatedDate = DateTime.Now;
                            cttt.CreatedBy = Guid.Parse(Global.UserGUID);
                        }

                        cttt.ThuocGUID = Guid.Parse(row["ThuocGUID"].ToString());
                        cttt.SoNgayUong = Convert.ToInt32(row["SoNgayUong"]);
                        cttt.SoLanTrongNgay = Convert.ToInt32(row["SoLanTrongNgay"]);
                        cttt.SoLuongTrongLan = Convert.ToInt32(row["SoLuongTrongLan"]);

                        if (row["Note"] != null && row["Note"] != DBNull.Value)
                            cttt.Note = row["Note"].ToString();  
                        else
                            cttt.Note = string.Empty;

                        cttt.Status = (byte)Status.Actived;
                        addedList.Add(cttt);
                    }

                    Result result = KeToaBus.InsertToaThuoc(_toaThuoc, addedList, _deletedKeys);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.InsertToaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.InsertToaThuoc"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddToaThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (_isNew)
                OnGetChiTietToaThuoc(Guid.Empty.ToString());
            else
                DisplayInfo(_drToaThuoc);    
        }

        private void dlgAddToaThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void dgChiTiet_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgChiTiet_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgChiTiet_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataRow row = (e.Row.DataBoundItem as DataRowView).Row;
            if (row["ChiTietToaThuocGUID"] != null && row["ChiTietToaThuocGUID"] != DBNull.Value)
                _deletedKeys.Add(row["ChiTietToaThuocGUID"].ToString());
        }

        private void dgChiTiet_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshNo();
        }
        
        private void dgChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgChiTiet_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            _flag = false;
            if (e.RowIndex < 0) return;
            dgChiTiet.CurrentCell = dgChiTiet[1, e.RowIndex];
            dgChiTiet.Rows[e.RowIndex].Selected = true;
            _flag = true;
        }

        private void thuocThayTheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (dgChiTiet.SelectedRows == null || dgChiTiet.SelectedRows.Count <= 0) return;
            int rowIndex = dgChiTiet.SelectedRows[0].Index;
            if (rowIndex == dgChiTiet.RowCount - 1) return;
            dgChiTiet.EndEdit();
            if (dgChiTiet.SelectedRows[0].Cells[1].Value == null || dgChiTiet.SelectedRows[0].Cells[1].Value == DBNull.Value) return;
            string thuocGUID = dgChiTiet.SelectedRows[0].Cells[1].Value.ToString();
            dlgThuocThayThe dlg = new dlgThuocThayThe(thuocGUID);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                dgChiTiet.SelectedRows[0].Cells[1].Value = dlg.ThuocThayThe;
                dgChiTiet.RefreshEdit();
            }
        }

        private void dgChiTiet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgChiTiet.CurrentCell.ColumnIndex == 1)
            {
                ComboBox cmbox = e.Control as ComboBox;
                cmbox.SelectedValueChanged -= new EventHandler(cmbox_SelectedValueChanged);
                cmbox.SelectedValueChanged += new EventHandler(cmbox_SelectedValueChanged);
            }
        }

        private void cmbox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_flag) return;

            DataGridViewComboBoxEditingControl cbo = (DataGridViewComboBoxEditingControl)sender;
            string thuocGUID = cbo.SelectedValue.ToString();
            string donViTinh = GetDonViTinh(thuocGUID);
            dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[2].Value = donViTinh;
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
        #endregion
    }
}
