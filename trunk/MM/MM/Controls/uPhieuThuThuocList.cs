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
using MM.Exports;

namespace MM.Controls
{
    public partial class uPhieuThuThuocList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPhieuThuThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
        }

        public void ClearData()
        {
            dgPhieuThu.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPhieuThuThuocListProc));
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

        private void OnDisplayPhieuThuThuocList()
        {
            Result result = PhieuThuThuocBus.GetPhieuThuThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgPhieuThu.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocList"));
            }
        }

        private void SelectLastedRow()
        {
            dgPhieuThu.CurrentCell = dgPhieuThu[1, dgPhieuThu.RowCount - 1];
            dgPhieuThu.Rows[dgPhieuThu.RowCount - 1].Selected = true;
        }

        private void OnAddPhieuThu()
        {
            dlgAddPhieuThuThuoc dlg = new dlgAddPhieuThuThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgPhieuThu.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["PhieuThuThuocGUID"] = dlg.PhieuThuThuoc.PhieuThuThuocGUID.ToString();
                newRow["MaPhieuThuThuoc"] = dlg.PhieuThuThuoc.MaPhieuThuThuoc;
                newRow["NgayThu"] = dlg.PhieuThuThuoc.NgayThu;
                newRow["MaBenhNhan"] = dlg.PhieuThuThuoc.MaBenhNhan;
                newRow["TenBenhNhan"] = dlg.PhieuThuThuoc.TenBenhNhan;
                newRow["DiaChi"] = dlg.PhieuThuThuoc.DiaChi;

                if (dlg.PhieuThuThuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.PhieuThuThuoc.CreatedDate;

                if (dlg.PhieuThuThuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.PhieuThuThuoc.CreatedBy.ToString();

                if (dlg.PhieuThuThuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.PhieuThuThuoc.UpdatedDate;

                if (dlg.PhieuThuThuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.PhieuThuThuoc.UpdatedBy.ToString();

                if (dlg.PhieuThuThuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.PhieuThuThuoc.DeletedDate;

                if (dlg.PhieuThuThuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.PhieuThuThuoc.DeletedBy.ToString();

                newRow["Status"] = dlg.PhieuThuThuoc.Status;
                dt.Rows.Add(newRow);
                SelectLastedRow();
            }
        }

        private void OnDeletePhieuThu()
        {
            List<string> deletedPTThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedPTThuocList.Add(row["PhieuThuThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedPTThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những phiếu thu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PhieuThuThuocBus.DeletePhieuThuThuoc(deletedPTThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.DeletePhieuThuThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.DeletePhieuThuThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần xóa.", IconType.Information);
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\PhieuThuThuoc.xls", Application.StartupPath);
                if (isPreview)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        string phieuThuThuocGUID = row["PhieuThuThuocGUID"].ToString();
                        if (ExportExcel.ExportPhieuThuThuocToExcel(exportFileName, phieuThuThuocGUID))
                        {
                            try
                            {
                                ExcelPrintPreview.PrintPreview(exportFileName);
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }
                        }
                        else
                            return;
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (DataRow row in checkedRows)
                        {
                            string phieuThuThuocGUID = row["PhieuThuThuocGUID"].ToString();
                            if (ExportExcel.ExportPhieuThuThuocToExcel(exportFileName, phieuThuThuocGUID))
                            {
                                try
                                {
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                                }
                                catch (Exception ex)
                                {
                                    MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                    return;
                                }
                            }
                            else
                                return;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần in.", IconType.Information);
        }

        private void OnViewPhieuThuThuoc()
        {
            if (dgPhieuThu.SelectedRows == null || dgPhieuThu.SelectedRows.Count <= 0)
                return;

            DataRow drPhieuThu = (dgPhieuThu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddPhieuThuThuoc dlg = new dlgAddPhieuThuThuoc(drPhieuThu);
            dlg.ShowDialog(this);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddPhieuThu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeletePhieuThu();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }
        
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPhieuThu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            OnViewPhieuThuThuoc();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPhieuThuThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayPhieuThuThuocList();
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
