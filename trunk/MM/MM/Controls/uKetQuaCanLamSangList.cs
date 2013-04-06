using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SpreadsheetGear;
using System.IO;
using MM.Common;
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;
using MM.Exports;

namespace MM.Controls
{
    public partial class uKetQuaCanLamSangList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private DataRow _patientRow2 = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isAll = true;
        private bool _isChuyenBenhAn = false;
        #endregion

        #region Constructor
        public uKetQuaCanLamSangList()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
            dtpkToDate.Value = DateTime.Now;
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

                if (_isChuyenBenhAn)
                    dgKetQuaCanLamSang.ContextMenuStrip = ctmAction2;
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

        public List<DataRow> CheckedServiceRows
        {
            get
            {
                if (dgKetQuaCanLamSang.RowCount <= 0) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgKetQuaCanLamSang.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = dgKetQuaCanLamSang.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgKetQuaCanLamSang.DataSource = null;
            }
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = Global.AllowAddCanLamSang;
            btnEdit.Enabled = Global.AllowEditCanLamSang;
            btnDelete.Enabled = Global.AllowDeleteCanLamSang;

            addToolStripMenuItem.Enabled = Global.AllowAddCanLamSang;
            editToolStripMenuItem.Enabled = Global.AllowEditCanLamSang;
            deleteToolStripMenuItem.Enabled = Global.AllowDeleteCanLamSang;

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

                if (raAll.Checked) _isAll = true;
                else
                {
                    _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                    _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaCanLamSangProc));
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

        private void OnDisplayKetQuaCanLamSang()
        {
            Result result = KetQuaCanLamSangBus.GetKetQuaCanLamSangList(_patientGUID, _isAll, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgKetQuaCanLamSang.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaCanLamSangBus.GetKetQuaCanLamSangList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaCanLamSangBus.GetKetQuaCanLamSangList"));
            }
        }

        private void OnAdd()
        {
            dlgAddMultiKetQuaCanLamSang dlg = new dlgAddMultiKetQuaCanLamSang(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (_isChuyenBenhAn) return;
            if (dgKetQuaCanLamSang.SelectedRows == null || dgKetQuaCanLamSang.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ.", IconType.Information);
                return;
            }

            DataRow drCanLamSang = (dgKetQuaCanLamSang.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddKetQuaCanLamSang dlg = new dlgAddKetQuaCanLamSang(_patientGUID, drCanLamSang);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedCanLamSangList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKetQuaCanLamSang.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedCanLamSangList.Add(row["KetQuaCanLamSangGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedCanLamSangList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết quả cận lâm sàng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetQuaCanLamSangBus.DeleteKetQuaCanLamSang(deletedCanLamSangList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaCanLamSangBus.DeleteKetQuaCanLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaCanLamSangBus.DeleteKetQuaCanLamSang"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả cận lâm sàng cần xóa.", IconType.Information);
        }

        private void OnChuyenKetQuaKham()
        {
            if (!_isChuyenBenhAn) return;

            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKetQuaCanLamSang.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (dgKetQuaCanLamSang.RowCount <= 0 || deletedRows == null || deletedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 kết quả cận lâm sàng cần chuyển.", IconType.Information);
                return;
            }

            if (_patientRow2 == null)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân nhận kết quả cận lâm sàng chuyển đến.", IconType.Information);
                return;
            }

            string fileNum = _patientRow2["FileNum"].ToString();
            if (MsgBox.Question(Application.ProductName, string.Format("Bạn có muốn chuyển những kết quả cận lâm sàng đã chọn đến bệnh nhân: '{0}'?", fileNum)) == DialogResult.No) return;


        }
        #endregion

        #region Window Event Handlers
        private void btnChuyen_Click(object sender, EventArgs e)
        {
            OnChuyenKetQuaKham();
        }

        private void chuyenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnChuyenKetQuaKham();
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgKetQuaCanLamSang.DataSource as DataTable;
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

        private void dgServiceHistory_DoubleClick(object sender, EventArgs e)
        {
            
            if (!Global.AllowEditCanLamSang) return;
            OnEdit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
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
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaCanLamSangProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaCanLamSang();
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
