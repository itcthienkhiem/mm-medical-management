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
    public partial class uKetQuaXetNghiemTongHop : uBase
    {
        #region Members
        private string _gioiTinh = string.Empty;
        private string _ngaySinh = string.Empty;
        private Font _normalFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Font _boldFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        #endregion

        #region Constructor
        public uKetQuaXetNghiemTongHop()
        {
            InitializeComponent();
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        private void ViewData()
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            if (txtTenBenhNhan.Text == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân.", IconType.Information);
                btnChonBenhNhan.Focus();
                return;
            }

            string patientGUID = txtTenBenhNhan.Tag.ToString();
            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;

            Result result = KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList(tuNgay, denNgay, patientGUID, _ngaySinh, _gioiTinh);
            if (result.IsOK)
            {
                dgXetNghiem.DataSource = result.QueryResult as DataTable;
                RefreshHighlight();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList"));
            }
        }

        private void RefreshHighlight()
        {
            foreach (DataGridViewRow row in dgXetNghiem.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                TinhTrang tinhTrang = (TinhTrang)Convert.ToByte(dr["TinhTrang"]);
                if (tinhTrang == MM.Common.TinhTrang.BatThuong)
                {
                    row.DefaultCellStyle.Font = _boldFont;
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.Font = _normalFont;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    txtTenBenhNhan.Tag = patientRow["PatientGUID"].ToString();
                    txtTenBenhNhan.Text = patientRow["FullName"].ToString();
                    if (patientRow["DobStr"] != null && patientRow["DobStr"].ToString() != string.Empty)
                        _ngaySinh = patientRow["DobStr"].ToString();
                    else
                        _ngaySinh = string.Empty;

                    if (patientRow["GenderAsStr"] != null && patientRow["GenderAsStr"].ToString() != string.Empty)
                        _gioiTinh = patientRow["GenderAsStr"].ToString();
                    else
                        _gioiTinh = string.Empty;
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        private void dtpkTuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            ViewData();
        }
        #endregion
    }
}
