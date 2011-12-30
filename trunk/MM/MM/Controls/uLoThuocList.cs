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
    public partial class uLoThuocList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uLoThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            dgLoThuoc.DataSource = null;
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayLoThuocListProc));
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

        private void OnDisplayLoThuocList()
        {
            Result result = LoThuocBus.GetLoThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgLoThuoc.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoThuocBus.GetLoThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetLoThuocList"));
            }
        }

        private void SelectLastedRow()
        {
            dgLoThuoc.CurrentCell = dgLoThuoc[1, dgLoThuoc.RowCount - 1];
            dgLoThuoc.Rows[dgLoThuoc.RowCount - 1].Selected = true;
        }

        private void OnAddLoThuoc()
        {
            dlgAddLoThuoc dlg = new dlgAddLoThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgLoThuoc.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["LoThuocGUID"] = dlg.LoThuoc.LoThuocGUID.ToString();
                newRow["MaLoThuoc"] = dlg.LoThuoc.MaLoThuoc;
                newRow["TenLoThuoc"] = dlg.LoThuoc.TenLoThuoc;
                newRow["ThuocGUID"] = dlg.LoThuoc.ThuocGUID;
                newRow["TenThuoc"] = dlg.TenThuoc;
                newRow["SoDangKy"] = dlg.LoThuoc.SoDangKy;
                newRow["HangSanXuat"] = dlg.LoThuoc.HangSanXuat;
                newRow["NgaySanXuat"] = dlg.LoThuoc.NgaySanXuat;
                newRow["NgayHetHan"] = dlg.LoThuoc.NgayHetHan;
                newRow["NhaPhanPhoi"] = dlg.LoThuoc.NhaPhanPhoi;
                newRow["SoLuongNhap"] = dlg.LoThuoc.SoLuongNhap;
                newRow["DonViTinhNhap"] = dlg.LoThuoc.DonViTinhNhap;
                newRow["GiaNhap"] = dlg.LoThuoc.GiaNhap;
                newRow["SoLuongQuiDoi"] = dlg.LoThuoc.SoLuongQuiDoi;
                newRow["DonViTinhQuiDoi"] = dlg.LoThuoc.DonViTinhQuiDoi;
                newRow["GiaNhapQuiDoi"] = dlg.LoThuoc.GiaNhapQuiDoi;
                newRow["TongTien"] = dlg.LoThuoc.SoLuongNhap * dlg.LoThuoc.GiaNhap;

                if (dlg.LoThuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.LoThuoc.CreatedDate;

                if (dlg.LoThuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.LoThuoc.CreatedBy.ToString();

                if (dlg.LoThuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.LoThuoc.UpdatedDate;

                if (dlg.LoThuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.LoThuoc.UpdatedBy.ToString();

                if (dlg.LoThuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.LoThuoc.DeletedDate;

                if (dlg.LoThuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.LoThuoc.DeletedBy.ToString();

                newRow["LoThuocStatus"] = dlg.LoThuoc.Status;
                dt.Rows.Add(newRow);
                //SelectLastedRow();
            }
        }

        private void OnEditLoThuoc()
        {
            if (dgLoThuoc.SelectedRows == null || dgLoThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 lô thuốc.", IconType.Information);
                return;
            }

            DataRow drLoThuoc = (dgLoThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddLoThuoc dlg = new dlgAddLoThuoc(drLoThuoc);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                drLoThuoc["MaLoThuoc"] = dlg.LoThuoc.MaLoThuoc;
                drLoThuoc["TenLoThuoc"] = dlg.LoThuoc.TenLoThuoc;
                drLoThuoc["ThuocGUID"] = dlg.LoThuoc.ThuocGUID;
                drLoThuoc["TenThuoc"] = dlg.TenThuoc;
                drLoThuoc["SoDangKy"] = dlg.LoThuoc.SoDangKy;
                drLoThuoc["HangSanXuat"] = dlg.LoThuoc.HangSanXuat;
                drLoThuoc["NgaySanXuat"] = dlg.LoThuoc.NgaySanXuat;
                drLoThuoc["NgayHetHan"] = dlg.LoThuoc.NgayHetHan;
                drLoThuoc["NhaPhanPhoi"] = dlg.LoThuoc.NhaPhanPhoi;
                drLoThuoc["SoLuongNhap"] = dlg.LoThuoc.SoLuongNhap;
                drLoThuoc["DonViTinhNhap"] = dlg.LoThuoc.DonViTinhNhap;
                drLoThuoc["GiaNhap"] = dlg.LoThuoc.GiaNhap;
                drLoThuoc["SoLuongQuiDoi"] = dlg.LoThuoc.SoLuongQuiDoi;
                drLoThuoc["DonViTinhQuiDoi"] = dlg.LoThuoc.DonViTinhQuiDoi;
                drLoThuoc["GiaNhapQuiDoi"] = dlg.LoThuoc.GiaNhapQuiDoi;
                drLoThuoc["TongTien"] = dlg.LoThuoc.SoLuongNhap * dlg.LoThuoc.GiaNhap;

                if (dlg.LoThuoc.CreatedDate.HasValue)
                    drLoThuoc["CreatedDate"] = dlg.LoThuoc.CreatedDate;

                if (dlg.LoThuoc.CreatedBy.HasValue)
                    drLoThuoc["CreatedBy"] = dlg.LoThuoc.CreatedBy.ToString();

                if (dlg.LoThuoc.UpdatedDate.HasValue)
                    drLoThuoc["UpdatedDate"] = dlg.LoThuoc.UpdatedDate;

                if (dlg.LoThuoc.UpdatedBy.HasValue)
                    drLoThuoc["UpdatedBy"] = dlg.LoThuoc.UpdatedBy.ToString();

                if (dlg.LoThuoc.DeletedDate.HasValue)
                    drLoThuoc["DeletedDate"] = dlg.LoThuoc.DeletedDate;

                if (dlg.LoThuoc.DeletedBy.HasValue)
                    drLoThuoc["DeletedBy"] = dlg.LoThuoc.DeletedBy.ToString();

                drLoThuoc["LoThuocStatus"] = dlg.LoThuoc.Status;
            }
        }

        private void OnDeleteLoThuoc()
        {
            List<string> deletedLoThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgLoThuoc.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedLoThuocList.Add(row["LoThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedLoThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những lô thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = LoThuocBus.DeleteLoThuoc(deletedLoThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoThuocBus.DeleteLoThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.DeleteLoThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những lô thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgLoThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddLoThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditLoThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteLoThuoc();
        }

        private void dgLoThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditLoThuoc();
        }
        #endregion

        #region Working Thread
        private void OnDisplayLoThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayLoThuocList();
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
