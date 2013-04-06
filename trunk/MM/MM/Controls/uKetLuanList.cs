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
    public partial class uKetLuanList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private DataRow _patientRow2 = null;
        private bool _isChuyenBenhAn = false;
        #endregion

        #region Constructor
        public uKetLuanList()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now;
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
                    dgKetLuan.ContextMenuStrip = ctmAction2;
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
            btnAdd.Enabled = Global.AllowAddKetLuan;
            btnDelete.Enabled = Global.AllowDeleteKetLuan;

            addToolStripMenuItem.Enabled = Global.AllowAddKetLuan;
            deleteToolStripMenuItem.Enabled = Global.AllowDeleteKetLuan;

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
                

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetLuanProc));
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
            DataTable dt = dgKetLuan.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgKetLuan.DataSource = null;
            }
        }

        private void OnDisplayKetLuan()
        {
            Result result = KetLuanBus.GetKetLuanList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgKetLuan.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetLuanBus.GetKetLuanList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetLuanBus.GetKetLuanList"));
            }
        }

        private void OnAdd()
        {
            dlgAddKetLuan dlg = new dlgAddKetLuan(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgKetLuan.SelectedRows == null || dgKetLuan.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết luận.", IconType.Information);
                return;
            }

            DataRow drKetLuan = (dgKetLuan.SelectedRows[0].DataBoundItem as DataRowView).Row;
            bool allowEdit = _isChuyenBenhAn ? false : Global.AllowEditKetLuan;
            dlgAddKetLuan dlg = new dlgAddKetLuan(_patientGUID, drKetLuan, allowEdit);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKetLuanList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKetLuan.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKetLuanList.Add(row["KetLuanGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKetLuanList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết luận mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetLuanBus.DeleteKetLuan(deletedKetLuanList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetLuanBus.DeleteKetLuan"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetLuanBus.DeleteKetLuan"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết luận cần xóa.", IconType.Information);
        }

        private void OnChuyenKetQuaKham()
        {
            if (!_isChuyenBenhAn) return;

            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKetLuan.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (dgKetLuan.RowCount <= 0 || deletedRows == null || deletedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 kết luận cần chuyển.", IconType.Information);
                return;
            }

            if (_patientRow2 == null)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân nhận kết luận chuyển đến.", IconType.Information);
                return;
            }

            string fileNum = _patientRow2["FileNum"].ToString();
            if (MsgBox.Question(Application.ProductName, string.Format("Bạn có muốn chuyển những kết luận đã chọn đến bệnh nhân: '{0}'?", fileNum)) == DialogResult.No) return;


        }
        #endregion

        #region Window Event Handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
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

        private void dgKetLuan_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgKetLuan.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

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
        private void OnDisplayKetLuanProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetLuan();
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
