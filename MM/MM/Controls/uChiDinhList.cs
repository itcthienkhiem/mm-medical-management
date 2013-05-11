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
using MM.Exports;

namespace MM.Controls
{
    public partial class uChiDinhList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private DataRow _patientRow2 = null;
        private bool _isChuyenBenhAn = false;
        private DataTable _dtDichVuChiDinh = null;
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

        private void OnDisplayChiDinhList()
        {
            if (_patientRow == null) return;
            string patientGUID = _patientRow["PatientGUID"].ToString();
            Result result = ChiDinhBus.GetChiTietChiDinhList2(patientGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearChiTietChiDinh();
                    dgChiTiet.DataSource = result.QueryResult;
                    OnGetDichVuChiDinh(patientGUID);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList2"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList2"));
            }
        }

        public void OnGetDichVuChiDinh(string patientGUID)
        {
            Result result = ChiDinhBus.GetDichVuChiDinhList2(patientGUID);
            if (result.IsOK)
            {
                if (_dtDichVuChiDinh != null)
                {
                    _dtDichVuChiDinh.Rows.Clear();
                    _dtDichVuChiDinh.Clear();
                    _dtDichVuChiDinh = null;
                }

                _dtDichVuChiDinh = result.QueryResult as DataTable;
                if (_dtDichVuChiDinh == null && _dtDichVuChiDinh.Rows.Count <= 0) return;

                foreach (DataGridViewRow row in dgChiTiet.Rows)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    string chiTietChiDinhGUID = r["ChiTietChiDinhGUID"].ToString();

                    DataRow[] rows = _dtDichVuChiDinh.Select(string.Format("ChiTietChiDinhGUID='{0}'", chiTietChiDinhGUID));
                    if (rows != null && rows.Length > 0)
                    {
                        (row.Cells["Checked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                        row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    }

                    r["Checked"] = false;
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetDichVuChiDinhList2"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetDichVuChiDinhList2"));
            }
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

        private void UpdateDichVuChiDinh()
        {
            if (_dtDichVuChiDinh == null && _dtDichVuChiDinh.Rows.Count <= 0) return;

            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                string chiTietChiDinhGUID = r["ChiTietChiDinhGUID"].ToString();

                DataRow[] rows = _dtDichVuChiDinh.Select(string.Format("ChiTietChiDinhGUID='{0}'", chiTietChiDinhGUID));
                if (rows != null && rows.Length > 0)
                {
                    (row.Cells["Checked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                }

                r["Checked"] = false;
            }
        }

        private void OnAddChiDinh()
        {
            dlgAddChiDinh dlg = new dlgAddChiDinh(_patientRow);
            dlg.OnAddChiDinhEvent += new AddChiDinhHandler(dlg_OnAddChiDinhEvent);
            dlg.ShowDialog(this);
        }

        private void OnEditChiDinh()
        {
            if (_isChuyenBenhAn) return;
            if (dgChiTiet.SelectedRows == null || dgChiTiet.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 chỉ định.", IconType.Information);
                return;
            }

            DataRow drChiDinh = (dgChiTiet.SelectedRows[0].DataBoundItem as DataRowView).Row;
            Result result = ChiDinhBus.GetChiTietChiDinhList(drChiDinh["ChiDinhGUID"].ToString());
            if (result.IsOK)
            {
                DataTable dtChiTiet = result.QueryResult as DataTable;
                dlgAddChiDinh dlg = new dlgAddChiDinh(_patientRow, drChiDinh, dtChiTiet, _dtDichVuChiDinh);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    DisplayAsThread();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList"));
                return;
            }
        }

        private void OnDeleteChiDinh()
        {
            List<string> deletedChiDinhList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgChiTiet.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedChiDinhList.Add(row["ChiTietChiDinhGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedChiDinhList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những chỉ định mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ChiDinhBus.DeleteChiTietChiDinhs(deletedChiDinhList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.DeleteChiTietChiDinhs"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.DeleteChiTietChiDinhs"));
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
                    foreach (DataRow row in checkedRows)
                    {
                        dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientRow["PatientGUID"].ToString());
                        dlg.ServiceGUID = row["ServiceGUID"].ToString();
                        dlg.BacSiChiDinhGUID = row["BacSiChiDinhGUID"].ToString();
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            if (dlg.ServiceHistory.ServiceHistoryGUID == Guid.Empty) return;

                            DichVuChiDinh dichVuChiDinh = new DichVuChiDinh();
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

                    OnGetDichVuChiDinh(_patientRow["PatientGUID"].ToString());
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ chỉ định cần xác nhận.", IconType.Information);
        }

        private void OnChuyenKetQuaKham()
        {
            if (!_isChuyenBenhAn) return;

            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgChiTiet.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (dgChiTiet.RowCount <= 0 || deletedRows == null || deletedRows.Count <= 0)
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

        private List<DataRow> GetChiDinhDuocXacNhan()
        {
            List<DataRow> chiDinhs = new List<DataRow>();

            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                if (!(row.Cells["Checked"] as DataGridViewDisableCheckBoxCell).Enabled)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    chiDinhs.Add(r);
                }
            }

            return chiDinhs;
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetChiDinhDuocXacNhan();
            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\ChiDinh.xls", Application.StartupPath);
                if (isPreview)
                {
                    if (!ExportExcel.ExportChiDinhToExcel(exportFileName, _patientRow, checkedRows))
                        return;
                    else
                    {
                        try
                        {
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.ChiDinhTemplate));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                        }
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!ExportExcel.ExportChiDinhToExcel(exportFileName, _patientRow, checkedRows))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.ChiDinhTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            }
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Không tồn tại chỉ định nào được xác nhận.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dlg_OnAddChiDinhEvent(ChiDinh chiDinh, string tenBacSiChiDinh)
        {
            DisplayAsThread();
        }

        private void chkChiTietChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (dgChiTiet.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["Checked"] as DataGridViewDisableCheckBoxCell;
                if (cell.Enabled)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    r["Checked"] = chkChecked.Checked;
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

        private void dgChiTiet_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditChiDinh();
        }

        private void xemBanInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
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
