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
    public partial class dlgAddKeToaCapCuu : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private bool _flag = true;
        private ToaCapCuu _toaCapCuu = new ToaCapCuu();
        private DataRow _drToaCapCuu = null;
        private string _tenCongTy = string.Empty;
        private DataTable _dataSourceBenhNhan = null;
        private ComboBox _cboBox = null;
        private List<string> _deletedKeys = new List<string>();
        #endregion

        #region Constructor
        public dlgAddKeToaCapCuu()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddKeToaCapCuu(DataRow drToaCapCuu, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _drToaCapCuu = drToaCapCuu;

            this.Text = "Sua toa cap cuu";

            if (!allowEdit)
            {
                btnOK.Enabled = false;
                dgChiTiet.AllowUserToAddRows = false;
                dgChiTiet.AllowUserToDeleteRows = false;
                dgChiTiet.ReadOnly = true;
                txtMaToaCapCuu.ReadOnly = true;
                dtpkNgayKeToa.Enabled = false;
                txtMaBenhNhan.ReadOnly = true;
                txtTenBenhNhan.ReadOnly = true;
                txtDiaChi.ReadOnly = true;
                txtGhiChu.ReadOnly = true;
                btnChonBenhNhan.Enabled = false;
                cboDocStaff.Enabled = false;
            }
        }
        #endregion

        #region Properties
        public ToaCapCuu ToaCapCuu
        {
            get { return _toaCapCuu; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = KeToaCapCuuBus.GetToaCapCuuCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaToaCapCuu.Text = Utility.GetCode("TCC", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaCapCuuBus.GetToaCapCuuCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaCapCuuBus.GetToaCapCuuCount"));
            }
        }

        private void InitData()
        {
            dtpkNgayKeToa.Value = DateTime.Now;
            DisplayDocStaffList();
            OnDisplayKhoCapCuu();
            OnGetSanhSachBenhNhan();
        }

        private void OnGetSanhSachBenhNhan()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
                _dataSourceBenhNhan = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void DisplayDocStaffList()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                newRow["FullName"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboDocStaff.DataSource = dt;
            }
        }

        private void DisplayInfo(DataRow drToaCapCuu)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaToaCapCuu.Text = drToaCapCuu["MaToaCapCuu"] as string;
                dtpkNgayKeToa.Value = Convert.ToDateTime(drToaCapCuu["NgayKeToa"]);
                txtMaBenhNhan.Text = drToaCapCuu["MaBenhNhan"] as string;
                txtTenBenhNhan.Text = drToaCapCuu["TenBenhNhan"] as string;
                txtDiaChi.Text = drToaCapCuu["DiaChi"] as string;
                txtGhiChu.Text = drToaCapCuu["Note"] as string;

                _toaCapCuu.ToaCapCuuGUID = Guid.Parse(drToaCapCuu["ToaCapCuuGUID"].ToString());

                if (drToaCapCuu["CreatedDate"] != null && drToaCapCuu["CreatedDate"] != DBNull.Value)
                    _toaCapCuu.CreatedDate = Convert.ToDateTime(drToaCapCuu["CreatedDate"]);

                if (drToaCapCuu["CreatedBy"] != null && drToaCapCuu["CreatedBy"] != DBNull.Value)
                    _toaCapCuu.CreatedBy = Guid.Parse(drToaCapCuu["CreatedBy"].ToString());

                if (drToaCapCuu["UpdatedDate"] != null && drToaCapCuu["UpdatedDate"] != DBNull.Value)
                    _toaCapCuu.UpdatedDate = Convert.ToDateTime(drToaCapCuu["UpdatedDate"]);

                if (drToaCapCuu["UpdatedBy"] != null && drToaCapCuu["UpdatedBy"] != DBNull.Value)
                    _toaCapCuu.UpdatedBy = Guid.Parse(drToaCapCuu["UpdatedBy"].ToString());

                if (drToaCapCuu["DeletedDate"] != null && drToaCapCuu["DeletedDate"] != DBNull.Value)
                    _toaCapCuu.DeletedDate = Convert.ToDateTime(drToaCapCuu["DeletedDate"]);

                if (drToaCapCuu["DeletedBy"] != null && drToaCapCuu["DeletedBy"] != DBNull.Value)
                    _toaCapCuu.DeletedBy = Guid.Parse(drToaCapCuu["DeletedBy"].ToString());

                _toaCapCuu.Status = Convert.ToByte(drToaCapCuu["Status"]);

                OnGetChiTietToaCapCuu(_toaCapCuu.ToaCapCuuGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayKhoCapCuu()
        {
            Result result = KhoCapCuuBus.GetDanhSachCapCuu();
            if (result.IsOK)
            {
                KhoCapCuuGUID.DataSource = result.QueryResult as DataTable;
                KhoCapCuuGUID.DisplayMember = "TenCapCuu";
                KhoCapCuuGUID.ValueMember = "KhoCapCuuGUID";
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"));
            }
        }

        private void RefreshNo()
        {
            for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
            {
                dgChiTiet[0, i].Value = i + 1;
            }
        }

        private void OnGetChiTietToaCapCuu(string toaCapCuuGUID)
        {
            Result result = KeToaCapCuuBus.GetChiTietToaCapCuu(toaCapCuuGUID);
            if (result.IsOK)
            {
                dgChiTiet.DataSource = result.QueryResult;
                UpdateNgayHetHanVaSoLuongTon();
                RefreshNo();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaCapCuuBus.GetChiTietToaCapCuu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaCapCuuBus.GetChiTietToaCapCuu"));
            }
        }

        private void UpdateNgayHetHanVaSoLuongTon()
        {
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                if (row.Cells["KhoCapCuuGUID"].Value == null || row.Cells["KhoCapCuuGUID"].Value == DBNull.Value ||
                    row.Cells["KhoCapCuuGUID"].Value.ToString() == Guid.Empty.ToString())
                    continue;

                string khoCapCuuGUID = row.Cells["KhoCapCuuGUID"].Value.ToString();
                Result result = NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu(khoCapCuuGUID);
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        DateTime ngayHetHan = Convert.ToDateTime(result.QueryResult);
                        row.Cells[3].Value = ngayHetHan;
                    }
                    else
                        row.Cells[3].Value = DBNull.Value;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"));
                }

                result = NhapKhoCapCuuBus.GetKhoCapCuuTonKho(khoCapCuuGUID);
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        int soLuongTon = Convert.ToInt32(result.QueryResult);
                        row.Cells[4].Value = soLuongTon;
                    }
                    else
                        row.Cells[4].Value = DBNull.Value;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"));
                }
            }
        }

        private bool CheckInfo()
        {
            if (txtMaToaCapCuu.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã toa cấp cứu.", IconType.Information);
                txtMaToaCapCuu.Focus();
                return false;
            }

            if (txtTenBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn tên bệnh nhân.", IconType.Information);
                txtTenBenhNhan.Focus();
                return false;
            }

            string toaCapCuuGUID = _isNew ? string.Empty : _toaCapCuu.ToaCapCuuGUID.ToString();
            Result result = KeToaCapCuuBus.CheckToaCapCuuExistCode(toaCapCuuGUID, txtMaToaCapCuu.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã toa cấp cứu này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaToaCapCuu.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaCapCuuBus.CheckToaCapCuuExistCode"), IconType.Error);
                return false;
            }

            if (dgChiTiet.RowCount > 1)
            {
                for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
                {
                    DataGridViewRow row = dgChiTiet.Rows[i];

                    if (row.Cells[1].Value == null || row.Cells[1].Value == DBNull.Value || row.Cells[1].Value.ToString() == Guid.Empty.ToString())
                    {
                        MsgBox.Show(this.Text, "Vui lòng nhập cấp cứu.", IconType.Information);
                        return false;
                    }

                    string khoCapCuuGUID = row.Cells[1].Value.ToString();
                    string tenCapCuu = GetTenCapCuu(khoCapCuuGUID);

                    int soLuong = 1;
                    if (row.Cells[2].Value != null && row.Cells[2].Value != DBNull.Value)
                        soLuong = Convert.ToInt32(row.Cells[2].Value);

                    Result r = NhapKhoCapCuuBus.CheckKhoCapCuuTonKho(khoCapCuuGUID, soLuong);
                    if (r.IsOK)
                    {
                        if (!Convert.ToBoolean(r.QueryResult))
                        {
                            MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã hết hoặc không đủ số lượng để bán. Vui lòng chọn cấp cứu khác.", tenCapCuu), IconType.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuTonKho"), IconType.Error);
                        Utility.WriteToTraceLog(r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuTonKho"));
                        return false;
                    }

                    r = NhapKhoCapCuuBus.CheckKhoCapCuuHetHan(khoCapCuuGUID);
                    if (r.IsOK)
                    {
                        if (Convert.ToBoolean(r.QueryResult))
                        {
                            MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã hết hạn sử dụng. Vui lòng chọn cấp cứu khác.", tenCapCuu), IconType.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MsgBox.Show(this.Text, r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuHetHan"), IconType.Error);
                        Utility.WriteToTraceLog(r.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuHetHan"));
                        return false;
                    }
                }
            }
            else
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 cấp cứu.", IconType.Information);
                return false;
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
                            string tenCapCuu = GetTenCapCuu(row1.Cells[1].Value.ToString());
                            MsgBox.Show(this.Text, string.Format("Cấp cứu '{0}' đã tồn tại rồi. Vui lòng chọn cấp cứu khác", tenCapCuu), IconType.Information);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private string GetTenCapCuu(string khoCapCuuGUID)
        {
            DataTable dt = KhoCapCuuGUID.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return string.Empty;

            DataRow[] rows = dt.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID));
            if (rows != null && rows.Length > 0)
                return rows[0]["TenCapCuu"].ToString();

            return string.Empty;
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
                    _toaCapCuu.MaToaCapCuu = txtMaToaCapCuu.Text;
                    _toaCapCuu.NgayKeToa = dtpkNgayKeToa.Value;

                    if (cboDocStaff.SelectedValue != null && cboDocStaff.Text.Trim() != string.Empty)
                        _toaCapCuu.BacSiKeToaGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    else
                        _toaCapCuu.BacSiKeToaGUID = null;

                    _toaCapCuu.MaBenhNhan = txtMaBenhNhan.Text;
                    _toaCapCuu.TenBenhNhan = txtTenBenhNhan.Text;
                    _toaCapCuu.DiaChi = txtDiaChi.Text;
                    _toaCapCuu.TenCongTy = _tenCongTy;
                    _toaCapCuu.Status = (byte)Status.Actived;
                    _toaCapCuu.Note = txtGhiChu.Text;

                    if (_isNew)
                    {
                        _toaCapCuu.CreatedDate = DateTime.Now;
                        _toaCapCuu.CreatedBy = Guid.Parse(Global.UserGUID);
                    }

                    List<ChiTietToaCapCuu> addedList = new List<ChiTietToaCapCuu>();
                    for (int i = 0; i < dgChiTiet.RowCount - 1; i++)
                    {
                        DataGridViewRow row = dgChiTiet.Rows[i];
                        ChiTietToaCapCuu cttcc = new ChiTietToaCapCuu();
                        if (row.Cells["ChiTietToaCapCuuGUID"].Value != null && row.Cells["ChiTietToaCapCuuGUID"].Value != DBNull.Value)
                            cttcc.ChiTietToaCapCuuGUID = Guid.Parse(row.Cells["ChiTietToaCapCuuGUID"].Value.ToString());

                        cttcc.CreatedDate = DateTime.Now;
                        cttcc.CreatedBy = Guid.Parse(Global.UserGUID);

                        cttcc.KhoCapCuuGUID = Guid.Parse(row.Cells["KhoCapCuuGUID"].Value.ToString());

                        if (row.Cells["SoLuong"].Value != null && row.Cells["SoLuong"].Value != DBNull.Value)
                            cttcc.SoLuong = Convert.ToDouble(row.Cells["SoLuong"].Value);
                        else
                            cttcc.SoLuong = 1;

                        cttcc.Status = (byte)Status.Actived;
                        addedList.Add(cttcc);
                    }

                    Result result = KeToaCapCuuBus.InsertToaCapCuu(_toaCapCuu, addedList, _deletedKeys);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KeToaCapCuuBus.InsertToaCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KeToaCapCuuBus.InsertToaCapCuu"));
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
        private void dlgAddPhieuThuThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (_isNew)
                OnGetChiTietToaCapCuu(Guid.Empty.ToString());
            else
                DisplayInfo(_drToaCapCuu);
        }

        private void dlgAddPhieuThuThuoc_FormClosing(object sender, FormClosingEventArgs e)
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
            if (e.Row.Cells["ChiTietToaCapCuuGUID"].Value != null && e.Row.Cells["ChiTietToaCapCuuGUID"].Value != DBNull.Value &&
                e.Row.Cells["ChiTietToaCapCuuGUID"].Value.ToString() != string.Empty)
            {
                string chiTietToaCapCuuGUID = e.Row.Cells["ChiTietToaCapCuuGUID"].Value.ToString();
                if (!_deletedKeys.Contains(chiTietToaCapCuuGUID))
                    _deletedKeys.Add(chiTietToaCapCuuGUID);
            }
        }

        private void dgChiTiet_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            _flag = false;    
            try
            {
                if (e.RowIndex < 0) return;
                dgChiTiet.CurrentCell = dgChiTiet[e.ColumnIndex, e.RowIndex];
                dgChiTiet.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex)
            {
                
            }

            _flag = true;
        }

        private void dgChiTiet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgChiTiet.CurrentCell.ColumnIndex == 1)
            {
                _cboBox = e.Control as ComboBox;
                _cboBox.DropDownStyle = ComboBoxStyle.DropDown;
                _cboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                _cboBox.SelectedValueChanged -= new EventHandler(cmbox_SelectedValueChanged);
                _cboBox.SelectedValueChanged += new EventHandler(cmbox_SelectedValueChanged);
            }
            else if (dgChiTiet.CurrentCell.ColumnIndex == 2)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress -= new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged -= new EventHandler(textBox_TextChanged);
                    textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged += new EventHandler(textBox_TextChanged);
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            TextBox textBox = (TextBox)sender;
            
            int colIndex = dgChiTiet.CurrentCell.ColumnIndex;
            if (colIndex != 2) return;

            if (textBox.Text == string.Empty)
                textBox.Text = "1";

            string strValue = textBox.Text.Replace(",", "").Replace(".", "");

            try
            {
                int value = int.Parse(strValue);
            }
            catch
            {
                textBox.Text = int.MaxValue.ToString();
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int colIndex = dgChiTiet.CurrentCell.ColumnIndex;
            if (colIndex != 2) return;

            DataGridViewTextBoxEditingControl textBox = (DataGridViewTextBoxEditingControl)sender;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
            }
        }

        private void cmbox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_flag) return;

            if (dgChiTiet.CurrentCell.ColumnIndex == 1)
            {
                _flag = false;
                DataGridViewComboBoxEditingControl cbo = (DataGridViewComboBoxEditingControl)sender;
                if (cbo.SelectedValue == null || cbo.SelectedValue.ToString() == "System.Data.DataRowView") return;
                string khoCapCuuGUID = cbo.SelectedValue.ToString();
                
                Result result = NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu(khoCapCuuGUID);
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        DateTime ngayHetHan = Convert.ToDateTime(result.QueryResult);
                        dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[3].Value = ngayHetHan;
                    }
                    else
                        dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[3].Value = DBNull.Value;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetNgayHetHanCuaKhoCapCuu"));
                }

                result = NhapKhoCapCuuBus.GetKhoCapCuuTonKho(khoCapCuuGUID);
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        int soLuongTon = Convert.ToInt32(result.QueryResult);
                        dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[4].Value = soLuongTon;
                    }
                    else
                        dgChiTiet.Rows[dgChiTiet.CurrentRow.Index].Cells[4].Value = DBNull.Value;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetKhoCapCuuTonKho"));
                }
                _flag = true;
            }
        }

        private void dgChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != 2) return;

            if (e.Value == null || e.Value.ToString() == string.Empty || e.Value == DBNull.Value)
                e.Value = "1";
        }

        private void dgChiTiet_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshNo();
        }

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(_dataSourceBenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    txtMaBenhNhan.Text = patientRow["FileNum"].ToString();
                    txtTenBenhNhan.Text = patientRow["FullName"].ToString();
                    txtDiaChi.Text = patientRow["Address"].ToString();
                }
            }
        }

        private void dgChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgChiTiet_Leave(object sender, EventArgs e)
        {
            if (_isNew)
            {
                int rowIndex = dgChiTiet.CurrentRow.Index;
                if (rowIndex < 0) return;
                dgChiTiet.CurrentCell = dgChiTiet[0, rowIndex];
                dgChiTiet.Rows[rowIndex].Selected = true;

            }
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
