using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgLamMoiSoHoaDon : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgLamMoiSoHoaDon()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgLamMoiSoHoaDon_Load(object sender, EventArgs e)
        {
            Result result = QuanLySoHoaDonBus.GetNgayThayDoiSoHoaSonSauCung();
            if (result.IsOK)
            {
                DateTime ngayThayDoiSauCung = Convert.ToDateTime(result.QueryResult);
                dtpkNgayThayDoiSauCung.Value = ngayThayDoiSauCung;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.GetNgayThayDoiSoHoaSonSauCung"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetNgayThayDoiSoHoaSonSauCung"));
            }

            dtpkNgayThayDoiMoi.Value = DateTime.Now;
        }

        private void dlgLamMoiSoHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (MsgBox.Question(this.Text, "Bạn có thật sự muốn thay đổi số hóa đơn ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (dtpkNgayThayDoiSauCung.Value.ToString("dd/MM/yyyy") != dtpkNgayThayDoiMoi.Value.ToString("dd/MM/yyyy"))
                    {
                        Result result = QuanLySoHoaDonBus.SetNgayThayDoiSoHoaSon(dtpkNgayThayDoiMoi.Value);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.SetNgayThayDoiSoHoaSon"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.SetNgayThayDoiSoHoaSon"));
                            e.Cancel = true;
                        }
                        else
                        {
                            Global.NgayThayDoiSoHoaDonSauCung = dtpkNgayThayDoiMoi.Value;
                        }
                    }
                }
                else
                    e.Cancel = true;
            }
        }
        #endregion
    }
}
