using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddNhomThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private NhomThuoc _nhomThuoc = new NhomThuoc();
        private List<string> _addedThuocs = new List<string>();
        private List<string> _deletedThuocs = new List<string>();
        private List<DataRow> _deletedThuocRows = new List<DataRow>();
        private DataRow _drNhomThuoc = null;
        #endregion

        #region Constructor
        public dlgAddNhomThuoc()
        {
            InitializeComponent();
            //DisplayThuocListAsThread(Guid.Empty.ToString());
            GenerateCode();
        }

        public dlgAddNhomThuoc(DataRow drNhomThuoc)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua nhom thuoc";
            _drNhomThuoc = drNhomThuoc;
            //DisplayInfo(drNhomThuoc);
        }
        #endregion

        #region Properties
        public NhomThuoc NhomThuoc
        {
            get { return _nhomThuoc; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = NhomThuocBus.GetNhomThuocCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaNhomThuoc.Text = Utility.GetCode("NH", count + 1, 5);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhomThuocBus.GetNhomThuocCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhomThuocBus.GetNhomThuocCount"));
            }
        }

        private void DisplayInfo(DataRow drNhomThuoc)
        {
            try
            {
                txtMaNhomThuoc.Text = drNhomThuoc["MaNhomThuoc"] as string;
                txtTenNhomThuoc.Text = drNhomThuoc["TenNhomThuoc"] as string;
                txtGhiChu.Text = drNhomThuoc["Note"] as string;

                _nhomThuoc.NhomThuocGUID = Guid.Parse(drNhomThuoc["NhomThuocGUID"].ToString());

                if (drNhomThuoc["CreatedDate"] != null && drNhomThuoc["CreatedDate"] != DBNull.Value)
                    _nhomThuoc.CreatedDate = Convert.ToDateTime(drNhomThuoc["CreatedDate"]);

                if (drNhomThuoc["CreatedBy"] != null && drNhomThuoc["CreatedBy"] != DBNull.Value)
                    _nhomThuoc.CreatedBy = Guid.Parse(drNhomThuoc["CreatedBy"].ToString());

                if (drNhomThuoc["UpdatedDate"] != null && drNhomThuoc["UpdatedDate"] != DBNull.Value)
                    _nhomThuoc.UpdatedDate = Convert.ToDateTime(drNhomThuoc["UpdatedDate"]);

                if (drNhomThuoc["UpdatedBy"] != null && drNhomThuoc["UpdatedBy"] != DBNull.Value)
                    _nhomThuoc.UpdatedBy = Guid.Parse(drNhomThuoc["UpdatedBy"].ToString());

                if (drNhomThuoc["DeletedDate"] != null && drNhomThuoc["DeletedDate"] != DBNull.Value)
                    _nhomThuoc.DeletedDate = Convert.ToDateTime(drNhomThuoc["DeletedDate"]);

                if (drNhomThuoc["DeletedBy"] != null && drNhomThuoc["DeletedBy"] != DBNull.Value)
                    _nhomThuoc.DeletedBy = Guid.Parse(drNhomThuoc["DeletedBy"].ToString());

                _nhomThuoc.Status = Convert.ToByte(drNhomThuoc["Status"]);

                DisplayThuocListAsThread(_nhomThuoc.NhomThuocGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void DisplayThuocListAsThread(string nhomThuocGUID)
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayThuocListProc), nhomThuocGUID);
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

        private void OnDisplayThuocList(string nhomThuocGUID)
        {
            Result result = NhomThuocBus.GetThuocListByNhom(nhomThuocGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgThuoc.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhomThuocBus.GetThuocListByNhom"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhomThuocBus.GetThuocListByNhom"));
            }
        }

        private bool CheckInfo()
        {
            if (txtMaNhomThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã nhóm thuốc.", IconType.Information);
                txtMaNhomThuoc.Focus();
                return false;
            }

            if (txtTenNhomThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên nhóm thuốc.", IconType.Information);
                txtTenNhomThuoc.Focus();
                return false;
            }

            string nhomThuocGUID = _isNew ? string.Empty : _nhomThuoc.NhomThuocGUID.ToString();
            Result result = NhomThuocBus.CheckNhomThuocExistCode(nhomThuocGUID, txtMaNhomThuoc.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã nhóm thuốc này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaNhomThuoc.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("NhomThuocBus.CheckNhomThuocExistCode"), IconType.Error);
                return false;
            }

            if (dgThuoc.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập danh sách thuốc.", IconType.Information);
                tabNhomThuoc.SelectedTabIndex = 1;
                return false;
            }

            return true;
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
                _nhomThuoc.MaNhomThuoc = txtMaNhomThuoc.Text;
                _nhomThuoc.TenNhomThuoc = txtTenNhomThuoc.Text;
                _nhomThuoc.Note = txtGhiChu.Text;
                _nhomThuoc.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _nhomThuoc.CreatedDate = DateTime.Now;
                    _nhomThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _nhomThuoc.UpdatedDate = DateTime.Now;
                    _nhomThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                Result result = NhomThuocBus.InsertNhomThuoc(_nhomThuoc, _addedThuocs, _deletedThuocs);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("NhomThuocBus.InsertNhomThuoc"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhomThuocBus.InsertNhomThuoc"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnAddThuoc()
        {
            dlgSelectThuoc dlg = new dlgSelectThuoc(_addedThuocs, _deletedThuocRows, _nhomThuoc.NhomThuocGUID.ToString());
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.CheckedThuocs;
                DataTable dataSource = dgThuoc.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string thuocGUID = row["ThuocGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["ThuocGUID"] = thuocGUID;
                        newRow["MaThuoc"] = row["MaThuoc"];
                        newRow["TenThuoc"] = row["TenThuoc"];
                        dataSource.Rows.Add(newRow);

                        if (!_addedThuocs.Contains(thuocGUID))
                            _addedThuocs.Add(thuocGUID);

                        _deletedThuocs.Remove(thuocGUID);
                        foreach (DataRow r in _deletedThuocRows)
                        {
                            if (r["ThuocGUID"].ToString() == thuocGUID)
                            {
                                _deletedThuocRows.Remove(r);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void OnDeleteThuoc()
        {
            List<string> deletedThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgThuoc.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedThuocList.Add(row["ThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string thuocGUID = row["ThuocGUID"].ToString();
                        if (!_deletedThuocs.Contains(thuocGUID))
                        {
                            _deletedThuocs.Add(thuocGUID);

                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            _deletedThuocRows.Add(r);
                        }

                        _addedThuocs.Remove(thuocGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddNhomThuoc_Load(object sender, EventArgs e)
        {
            if (_isNew)
                DisplayThuocListAsThread(Guid.Empty.ToString());
            else
                DisplayInfo(_drNhomThuoc);
        }

        private void dlgAddNhomThuoc_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin nhóm thuốc ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                        SaveInfoAsThread();
                    else
                        e.Cancel = true;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteThuoc();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
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

        private void OnDisplayThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayThuocList(state.ToString());
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
