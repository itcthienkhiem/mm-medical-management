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
    public partial class dlgAddPhieuThuThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        #endregion

        #region Constructor
        public dlgAddPhieuThuThuoc()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayThu.Value = DateTime.Now;
            OnDisplayToaThuocList();
        }

        private void OnDisplayToaThuocList()
        {
            Result result = KeToaBus.GetToaThuocList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["ToaThuocGUID"] = Guid.Empty.ToString();
                newRow["MaToaThuoc"] = "----Không có----";
                dt.Rows.InsertAt(newRow, 0);
                cboMaToaThuoc.DataSource = dt;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuocList"));
            }
        }

        private DataRow GetToaThuocRow(string toaThuocGUID)
        {
            DataTable dt = cboMaToaThuoc.DataSource as DataTable;
            DataRow[] rows = dt.Select(string.Format("ToaThuocGUID='{0}'", toaThuocGUID));
            if (rows != null && rows.Length > 0)
                return rows[0];

            return null;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddPhieuThuThuoc_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void dlgAddPhieuThuThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cboMaToaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string toaThuocGUID = cboMaToaThuoc.SelectedValue.ToString();
            if (toaThuocGUID == Guid.Empty.ToString())
            {
                txtMaBenhNhan.ReadOnly = true;
                txtMaBenhNhan.Text = string.Empty;
            }
            else
            {
                txtMaBenhNhan.ReadOnly = false;
                DataRow row = GetToaThuocRow(toaThuocGUID);
                if (row != null)
                {
                    txtMaBenhNhan.Text = row["FileNum"].ToString();
                    txtTenBenhNhan.Text = row["TenBenhNhan"].ToString();
                    txtDiaChi.Text = row["Address"].ToString();
                }
            }
        }
        #endregion
    }
}
