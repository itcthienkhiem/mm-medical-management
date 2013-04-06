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
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uKetQuaSieuAmList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isPrint = false;
        private KetQuaSieuAm _ketQuaSieuAm = null;
        private DataRow _patientRow2 = null;
        private bool _isChuyenBenhAn = false;
        #endregion

        #region Constructor
        public uKetQuaSieuAmList()
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
                btnExportExcel.Visible = !_isChuyenBenhAn;
                btnPrint.Visible = !_isChuyenBenhAn;
                btnPrintPreview.Visible = !_isChuyenBenhAn;

                if (_isChuyenBenhAn)
                    dgSieuAm.ContextMenuStrip = ctmAction2;
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
            btnAdd.Enabled = Global.AllowAddSieuAm;
            btnDelete.Enabled = Global.AllowDeleteSieuAm;
            btnPrint.Enabled = Global.AllowPrintSieuAm;
            btnPrintPreview.Enabled = Global.AllowPrintSieuAm;
            btnExportExcel.Enabled = Global.AllowExportSieuAm;

            addToolStripMenuItem.Enabled = Global.AllowAddSieuAm;
            deleteToolStripMenuItem.Enabled = Global.AllowDeleteSieuAm;
            printPreviewToolStripMenuItem.Enabled = Global.AllowPrintSieuAm;
            printToolStripMenuItem.Enabled = Global.AllowPrintSieuAm;
            exportExcelToolStripMenuItem.Enabled = Global.AllowExportSieuAm;

            btnChuyen.Enabled = AllowChuyenKetQuaKham;
            chuyenToolStripMenuItem.Enabled = AllowChuyenKetQuaKham;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            if (_patientRow == null) return;

            try
            {
                DataRow row = _patientRow as DataRow;
                _patientGUID = row["PatientGUID"].ToString();
                if (raAll.Checked)
                {
                    _fromDate = Global.MinDateTime;
                    _toDate = Global.MaxDateTime;
                }
                else
                {
                    _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                    _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
                }


                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaSieuAmListProc));
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

        public void ClearData()
        {
            DataTable dt = dgSieuAm.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgSieuAm.DataSource = null;
            }
        }

        private void OnDisplayKetQuaSieuAmList()
        {
            Result result = SieuAmBus.GetKetQuaSieuAmList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgSieuAm.DataSource = dt;

                    if (_isPrint)
                    {
                        DataRow[] rows = dt.Select(string.Format("KetQuaSieuAmGUID='{0}'", _ketQuaSieuAm.KetQuaSieuAmGUID.ToString()));
                        if (rows != null && rows.Length > 0)
                            OnPrint(rows[0]);
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetKetQuaSieuAmList"));
            }
        }

        private void OnAdd()
        {
            _isPrint = false;
            _ketQuaSieuAm = null;

            if (Global.TVHomeConfig.SuDungSieuAm && !File.Exists(Global.TVHomeConfig.Path))
            {
                MsgBox.Show(Application.ProductName, "Đường dẫn TVHome không tồn tại, vui lòng kiểm tra lại.", IconType.Information);
                return;
            }

            string gioiTinh = _patientRow["GenderAsStr"].ToString();
            dlgAddKetQuaSieuAm dlg = new dlgAddKetQuaSieuAm(_patientGUID, gioiTinh);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _isPrint = dlg.IsPrint;
                _ketQuaSieuAm = dlg.KetQuaSieuAm;
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            _isPrint = false;
            _ketQuaSieuAm = null;
            if (dgSieuAm.SelectedRows == null || dgSieuAm.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết quả siêu âm.", IconType.Information);
                return;
            }

            if (Global.AllowEditKhamCTC && Global.TVHomeConfig.SuDungSieuAm && !File.Exists(Global.TVHomeConfig.Path))
            {
                MsgBox.Show(Application.ProductName, "Đường dẫn TVHome không tồn tại, vui lòng kiểm tra lại.", IconType.Information);
                return;
            }

            string gioiTinh = _patientRow["GenderAsStr"].ToString();
            DataRow drKetQuaSieuAm = (dgSieuAm.SelectedRows[0].DataBoundItem as DataRowView).Row;
            bool allowEdit = _isChuyenBenhAn ? false : Global.AllowEditSieuAm;
            dlgAddKetQuaSieuAm dlg = new dlgAddKetQuaSieuAm(_patientGUID, gioiTinh, drKetQuaSieuAm, allowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _isPrint = dlg.IsPrint;
                _ketQuaSieuAm = dlg.KetQuaSieuAm;
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKQNSList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgSieuAm.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQNSList.Add(row["KetQuaSieuAmGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKQNSList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết quả siêu âm mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = SieuAmBus.DeleteKetQuaSieuAm(deletedKQNSList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.DeleteKetQuaSieuAm"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.DeleteKetQuaSieuAm"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả siêu âm.", IconType.Information);
        }

        private void OnPrint(DataRow drKetQuaSieuAm)
        {
            List<DataRow> rows = new List<DataRow>();
            rows.Add(drKetQuaSieuAm);
            _uPrintKetQuaSieuAm.PatientRow = _patientRow;
            _uPrintKetQuaSieuAm.Print(rows);
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count > 0)
            {
                if (isPreview)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        dlgPrintKetQuaSieuAm dlg = new dlgPrintKetQuaSieuAm(_patientRow, row);
                        dlg.ShowDialog();
                    }
                }
                else
                {
                    _uPrintKetQuaSieuAm.PatientRow = _patientRow;
                    _uPrintKetQuaSieuAm.Print(checkedRows);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả siêu âm cần in.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgSieuAm.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            return checkedRows;
        }

        private void OnExport()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count > 0)
            {
                foreach (DataRow row in checkedRows)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Title = "Export Excel";
                    dlg.Filter = "Excel Files(*.rtf)|*.rtf";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        _uPrintKetQuaSieuAm.PatientRow = _patientRow;
                        _uPrintKetQuaSieuAm.Export(row, dlg.FileName);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả siêu âm cần xuất.", IconType.Information);
        }

        private void OnChuyenKetQuaKham()
        {
            if (!_isChuyenBenhAn) return;

            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgSieuAm.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (dgSieuAm.RowCount <= 0 || deletedRows == null || deletedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 kết quả siêu âm cần chuyển.", IconType.Information);
                return;
            }

            if (_patientRow2 == null)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân nhận kết quả siêu âm chuyển đến.", IconType.Information);
                return;
            }

            string fileNum = _patientRow2["FileNum"].ToString();
            if (MsgBox.Question(Application.ProductName, string.Format("Bạn có muốn chuyển những kết quả siêu âm đã chọn đến bệnh nhân: '{0}'?", fileNum)) == DialogResult.No) return;

            Result result = SieuAmBus.ChuyenBenhAn(_patientRow2["PatientGUID"].ToString(), deletedRows);
            if (result.IsOK)
                DisplayAsThread();
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.ChuyenBenhAn"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.ChuyenBenhAn"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

            DisplayAsThread();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgSieuAm.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void dgSieuAm_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
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
            OnExport();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
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
            OnExport();
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
        private void OnDisplayKetQuaSieuAmListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaSieuAmList();
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
