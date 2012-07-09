using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uXetNghiem : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uXetNghiem()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void InitData()
        {
            btnDanhSachXetNghiemHitachi917.Enabled = AllowView;
            btnDanhSachXNCellDyn3200.Enabled = AllowView;
            btnDanhSachXNTay.Enabled = AllowView;
            btnKetQuaXNHitachi917.Enabled = AllowView;
            btnKetQuaXNCellDyn3200.Enabled = AllowView;
            btnKetQuaXNTay.Enabled = AllowView;
            btnKetQuaXNTongHop.Enabled = AllowView;
            btnDanhSachDiaChiCongTy.Enabled = Global.AllowViewDSDiaChiCongTy;
            btnTraCuuThongTinKhachHang.Enabled = Global.AllowViewTraCuuDanhSachKhachHang;

            _uKetQuaXetNghiem_Hitachi917.AllowAdd = AllowAdd;
            _uKetQuaXetNghiem_Hitachi917.AllowEdit = AllowEdit;
            _uKetQuaXetNghiem_Hitachi917.AllowDelete = AllowDelete;
            _uKetQuaXetNghiem_Hitachi917.AllowPrint = AllowPrint;
            _uKetQuaXetNghiem_Hitachi917.AllowExport = AllowExport;
            _uKetQuaXetNghiem_Hitachi917.AllowImport = AllowImport;
            _uKetQuaXetNghiem_Hitachi917.AllowLock = AllowLock;
            _uKetQuaXetNghiem_Hitachi917.AllowExportAll = AllowExportAll;

            _uKetQuaXetNghiem_CellDyn3200.AllowAdd = AllowAdd;
            _uKetQuaXetNghiem_CellDyn3200.AllowEdit = AllowEdit;
            _uKetQuaXetNghiem_CellDyn3200.AllowDelete = AllowDelete;
            _uKetQuaXetNghiem_CellDyn3200.AllowPrint = AllowPrint;
            _uKetQuaXetNghiem_CellDyn3200.AllowExport = AllowExport;
            _uKetQuaXetNghiem_CellDyn3200.AllowImport = AllowImport;
            _uKetQuaXetNghiem_CellDyn3200.AllowLock = AllowLock;
            _uKetQuaXetNghiem_CellDyn3200.AllowExportAll = AllowExportAll;

            _uXetNghiemTay.AllowAdd = AllowAdd;
            _uXetNghiemTay.AllowEdit = AllowEdit;
            _uXetNghiemTay.AllowDelete = AllowDelete;
            _uXetNghiemTay.AllowPrint = AllowPrint;
            _uXetNghiemTay.AllowExport = AllowExport;
            _uXetNghiemTay.AllowImport = AllowImport;
            _uXetNghiemTay.AllowLock = AllowLock;
            _uXetNghiemTay.AllowExportAll = AllowExportAll;

            _uDanhSachXetNghiemHitachi917List.AllowAdd = AllowAdd;
            _uDanhSachXetNghiemHitachi917List.AllowEdit = AllowEdit;
            _uDanhSachXetNghiemHitachi917List.AllowDelete = AllowDelete;
            _uDanhSachXetNghiemHitachi917List.AllowPrint = AllowPrint;
            _uDanhSachXetNghiemHitachi917List.AllowExport = AllowExport;
            _uDanhSachXetNghiemHitachi917List.AllowImport = AllowImport;
            _uDanhSachXetNghiemHitachi917List.AllowLock = AllowLock;
            _uDanhSachXetNghiemHitachi917List.AllowExportAll = AllowExportAll;

            _uDanhSachXetNghiem_CellDyn3200List.AllowAdd = AllowAdd;
            _uDanhSachXetNghiem_CellDyn3200List.AllowEdit = AllowEdit;
            _uDanhSachXetNghiem_CellDyn3200List.AllowDelete = AllowDelete;
            _uDanhSachXetNghiem_CellDyn3200List.AllowPrint = AllowPrint;
            _uDanhSachXetNghiem_CellDyn3200List.AllowExport = AllowExport;
            _uDanhSachXetNghiem_CellDyn3200List.AllowImport = AllowImport;
            _uDanhSachXetNghiem_CellDyn3200List.AllowLock = AllowLock;
            _uDanhSachXetNghiem_CellDyn3200List.AllowExportAll = AllowExportAll;

            _uKetQuaXetNghiemTay.AllowAdd = AllowAdd;
            _uKetQuaXetNghiemTay.AllowEdit = AllowEdit;
            _uKetQuaXetNghiemTay.AllowDelete = AllowDelete;
            _uKetQuaXetNghiemTay.AllowPrint = AllowPrint;
            _uKetQuaXetNghiemTay.AllowExport = AllowExport;
            _uKetQuaXetNghiemTay.AllowImport = AllowImport;
            _uKetQuaXetNghiemTay.AllowLock = AllowLock;
            _uKetQuaXetNghiemTay.AllowExportAll = AllowExportAll;

            _uKetQuaXetNghiemTongHop.AllowAdd = AllowAdd;
            _uKetQuaXetNghiemTongHop.AllowEdit = AllowEdit;
            _uKetQuaXetNghiemTongHop.AllowDelete = AllowDelete;
            _uKetQuaXetNghiemTongHop.AllowPrint = AllowPrint;
            _uKetQuaXetNghiemTongHop.AllowExport = AllowExport;
            _uKetQuaXetNghiemTongHop.AllowImport = AllowImport;
            _uKetQuaXetNghiemTongHop.AllowLock = AllowLock;
            _uKetQuaXetNghiemTongHop.AllowExportAll = AllowExportAll;

            _uDiaChiCongTyList.AllowAdd = Global.AllowAddDSDiaChiCongTy;
            _uDiaChiCongTyList.AllowEdit = Global.AllowEditDSDiaChiCongTy;
            _uDiaChiCongTyList.AllowDelete = Global.AllowDeleteDSDiaChiCongTy;
        }

        private void ViewControl(Control view)
        {
            view.Visible = true;

            foreach (Control ctrl in this.pDetail.Controls)
            {
                if (ctrl != view)
                    ctrl.Visible = false;
            }
        }

        private void OnCauHinhKetNoi()
        {
            dlgPortConfig dlg = new dlgPortConfig();
            dlg.ShowDialog(this);
        }

        private void OnDanhSachXetNghiemHitachi917()
        {
            this.lbDetail.Text = "Danh sách xét nghiệm Hitachi 917";
            ViewControl(_uDanhSachXetNghiemHitachi917List);
            _uDanhSachXetNghiemHitachi917List.DisplayAsThread();
        }

        private void OnDanhSachXetNghiemCellDyn3200()
        {
            this.lbDetail.Text = "Danh sách xét nghiệm CellDyn 3200";
            ViewControl(_uDanhSachXetNghiem_CellDyn3200List);
            _uDanhSachXetNghiem_CellDyn3200List.DisplayAsThread();
        }

        private void OnDanhSachXetNghiemTay()
        {
            this.lbDetail.Text = "Danh sách xét nghiệm tay";
            ViewControl(_uXetNghiemTay);
            _uXetNghiemTay.DisplayAsThread();
        }

        private void OnKetQuaXetNghiemHitachi917()
        {
            this.lbDetail.Text = "Kết quả xét nghiệm Hitachi 917";
            ViewControl(_uKetQuaXetNghiem_Hitachi917);
            _uKetQuaXetNghiem_Hitachi917.DisplayAsThread();
        }

        private void OnKetQuaXetNghiemCellDyn3200()
        {
            this.lbDetail.Text = "Kết quả xét nghiệm CellDyn 3200";
            ViewControl(_uKetQuaXetNghiem_CellDyn3200);
            _uKetQuaXetNghiem_CellDyn3200.DisplayAsThread();
        }

        private void OnKetQuaXetNghiemTay()
        {
            this.lbDetail.Text = "Kết quả xét nghiệm tay";
            ViewControl(_uKetQuaXetNghiemTay);
            _uKetQuaXetNghiemTay.DisplayAsThread();
        }

        private void OnKetQuaXetNghiemTongHop()
        {
            this.lbDetail.Text = "Kết quả xét nghiệm tổng hợp";
            ViewControl(_uKetQuaXetNghiemTongHop);
            _uKetQuaXetNghiemTongHop.DisplayDanhSachBenhNhan();
        }

        private void OnDanhSachDiaChiCongTy()
        {
            this.lbDetail.Text = "Danh sách địa chỉ công ty";
            ViewControl(_uDiaChiCongTyList);
            _uDiaChiCongTyList.DisplayAsThread();
        }

        private void OnTraCuuThongTinKhachHang()
        {
            this.lbDetail.Text = "Tra cứu thông tin khách hàng";
            ViewControl(_uTraCuuThongTinKhachHang);
            _uTraCuuThongTinKhachHang.DisplayAsThread();
        }
        #endregion

        #region Window Event Handlers
        private void btnCauHinhKetNoi_Click(object sender, EventArgs e)
        {
            OnCauHinhKetNoi();
        }

        private void btnDanhSachXetNghiemHitachi917_Click(object sender, EventArgs e)
        {
            OnDanhSachXetNghiemHitachi917();
        }

        private void btnKetQuaXNHitachi917_Click(object sender, EventArgs e)
        {
            OnKetQuaXetNghiemHitachi917();
        }

        private void btnDanhSachXNCellDyn3200_Click(object sender, EventArgs e)
        {
            OnDanhSachXetNghiemCellDyn3200();
        }

        private void btnKetQuaXNCellDyn3200_Click(object sender, EventArgs e)
        {
            OnKetQuaXetNghiemCellDyn3200();
        }

        private void btnDanhSachXNTay_Click(object sender, EventArgs e)
        {
            OnDanhSachXetNghiemTay();
        }

        private void btnKetQuaXNTay_Click(object sender, EventArgs e)
        {
            OnKetQuaXetNghiemTay();
        }

        private void btnKetQuaXNTongHop_Click(object sender, EventArgs e)
        {
            OnKetQuaXetNghiemTongHop();
        }

        private void btnDanhSachDiaChiCongTy_Click(object sender, EventArgs e)
        {
            OnDanhSachDiaChiCongTy();
        }

        private void btnTraCuuThongTinKhachHang_Click(object sender, EventArgs e)
        {
            OnTraCuuThongTinKhachHang();
        }
        #endregion
    }
}
