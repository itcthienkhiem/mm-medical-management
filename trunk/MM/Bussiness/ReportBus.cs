using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using System.Data;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class ReportBus : BusBase
    {
        public static Result GetDoanhThuNhanVienTongHop(DateTime fromDate, DateTime toDate, string docStaffGUID, byte type)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                result.QueryResult = db.spDoanhThuNhanVienTongHop(fromDate, toDate, docStaffGUID, type).ToList<spDoanhThuNhanVienTongHopResult>().ToList <spDoanhThuNhanVienTongHopResult>();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetDoanhThuNhanVienChiTiet(DateTime fromDate, DateTime toDate, string docStaffGUID, byte type)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                result.QueryResult = db.spDoanhThuNhanVienChiTiet(fromDate, toDate, docStaffGUID, type).ToList<spDoanhThuNhanVienChiTietResult>();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetDichVuHopDong(string contractGUID, DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                result.QueryResult = db.spDichVuHopDong(contractGUID, tuNgay, denNgay, type).ToList<spDichVuHopDongResult>();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetDichVuTuTuc(DateTime tuNgay, DateTime denNgay)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                result.QueryResult = db.spDichVuTuTuc(tuNgay, denNgay).ToList<spDichVuTuTucResult>();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetThuocHetHanList(int soNgayHetHan, List<string> thuocList)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                DateTime dt = DateTime.Now.AddDays(soNgayHetHan);
                db = new MMOverride();
                List<ThuocResult> thuocResults = (from t in db.Thuocs
                                join l in db.LoThuocs on t.ThuocGUID equals l.ThuocGUID
                                where t.Status == (byte)Status.Actived && l.Status == (byte)Status.Actived &&
                                l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0 &&
                                l.NgayHetHan <= dt && thuocList.Contains(t.ThuocGUID.ToString())
                                select new ThuocResult(soNgayHetHan, t.MaThuoc, t.TenThuoc,
                                    l.MaLoThuoc, l.TenLoThuoc, l.NgaySanXuat, l.NgayHetHan,
                                    l.SoLuongNhap * l.SoLuongQuiDoi, l.SoLuongXuat,
                                    l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat, 
                                    t.DonViTinh)).ToList<ThuocResult>();

                result.QueryResult = thuocResults;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetCapCuuHetHanList(int soNgayHetHan, List<string> capCuuKeyList)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                DateTime dt = DateTime.Now.AddDays(soNgayHetHan);
                db = new MMOverride();
                List<CapCuuResult> capCuuResults = (from t in db.KhoCapCuus
                                                    join l in db.NhapKhoCapCuus on t.KhoCapCuuGUID equals l.KhoCapCuuGUID
                                                    where t.Status == (byte)Status.Actived && l.Status == (byte)Status.Actived &&
                                                    l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0 &&
                                                    l.NgayHetHan <= dt && capCuuKeyList.Contains(t.KhoCapCuuGUID.ToString())
                                                    select new CapCuuResult(soNgayHetHan, t.MaCapCuu, t.TenCapCuu,
                                                        l.NgaySanXuat.Value, l.NgayHetHan.Value,
                                                        l.SoLuongNhap * l.SoLuongQuiDoi, l.SoLuongXuat,
                                                        l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat,
                                                        t.DonViTinh)).ToList<CapCuuResult>();

                result.QueryResult = capCuuResults;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetThuocTonKhoList(List<string> thuocList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ThuocResult> thuocResults = (from t in db.Thuocs
                                                  join l in db.LoThuocs on t.ThuocGUID equals l.ThuocGUID
                                                  where t.Status == (byte)Status.Actived && l.Status == (byte)Status.Actived &&
                                                  l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0 &&
                                                  thuocList.Contains(t.ThuocGUID.ToString())
                                                  orderby t.MaThuoc, l.TenLoThuoc
                                                  select new ThuocResult(0, t.MaThuoc, t.TenThuoc,
                                                      l.MaLoThuoc, l.TenLoThuoc, l.NgaySanXuat, l.NgayHetHan,
                                                      l.SoLuongNhap * l.SoLuongQuiDoi, l.SoLuongXuat,
                                                      l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat,
                                                      t.DonViTinh)).ToList<ThuocResult>();

                result.QueryResult = thuocResults;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetChiTietPhieuThuDichVu(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ReceiptDetailView> chiTietPhieuThuList = null;

                if (type == 0)
                {
                    chiTietPhieuThuList = (from pt in db.Receipts
                                           join ctpt in db.ReceiptDetailViews on pt.ReceiptGUID equals ctpt.ReceiptGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.ReceiptDate >= tuNgay && pt.ReceiptDate <= denNgay 
                                           select ctpt).ToList<ReceiptDetailView>();
                }
                else if (type == 1)
                {
                    chiTietPhieuThuList = (from pt in db.Receipts
                                           join ctpt in db.ReceiptDetailViews on pt.ReceiptGUID equals ctpt.ReceiptGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.ReceiptDate >= tuNgay && pt.ReceiptDate <= denNgay &&
                                           pt.ChuaThuTien == false
                                           select ctpt).ToList<ReceiptDetailView>();
                }
                else
                {
                    chiTietPhieuThuList = (from pt in db.Receipts
                                           join ctpt in db.ReceiptDetailViews on pt.ReceiptGUID equals ctpt.ReceiptGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.ReceiptDate >= tuNgay && pt.ReceiptDate <= denNgay &&
                                           pt.ChuaThuTien == true
                                           select ctpt).ToList<ReceiptDetailView>();
                }

                result.QueryResult = chiTietPhieuThuList;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetChiTietPhieuThuThuoc(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ChiTietPhieuThuThuocView> chiTietPhieuThuList = null;

                if (type == 0)
                {
                    chiTietPhieuThuList = (from pt in db.PhieuThuThuocs
                                           join ctpt in db.ChiTietPhieuThuThuocViews on pt.PhieuThuThuocGUID equals ctpt.PhieuThuThuocGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.NgayThu >= tuNgay && pt.NgayThu <= denNgay
                                           select ctpt).ToList<ChiTietPhieuThuThuocView>();
                }
                else if (type == 1)
                {
                    chiTietPhieuThuList = (from pt in db.PhieuThuThuocs
                                           join ctpt in db.ChiTietPhieuThuThuocViews on pt.PhieuThuThuocGUID equals ctpt.PhieuThuThuocGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.NgayThu >= tuNgay && pt.NgayThu <= denNgay &&
                                           pt.ChuaThuTien == false
                                           select ctpt).ToList<ChiTietPhieuThuThuocView>();
                }
                else
                {
                    chiTietPhieuThuList = (from pt in db.PhieuThuThuocs
                                           join ctpt in db.ChiTietPhieuThuThuocViews on pt.PhieuThuThuocGUID equals ctpt.PhieuThuThuocGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.NgayThu >= tuNgay && pt.NgayThu <= denNgay &&
                                           pt.ChuaThuTien == true
                                           select ctpt).ToList<ChiTietPhieuThuThuocView>();
                }

                result.QueryResult = chiTietPhieuThuList;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetChiTietPhieuThuCapCuu(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ChiTietPhieuThuCapCuuView> chiTietPhieuThuList = null;

                if (type == 0)
                {
                    chiTietPhieuThuList = (from pt in db.PhieuThuCapCuus
                                           join ctpt in db.ChiTietPhieuThuCapCuuViews on pt.PhieuThuCapCuuGUID equals ctpt.PhieuThuCapCuuGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.NgayThu >= tuNgay && pt.NgayThu <= denNgay 
                                           select ctpt).ToList<ChiTietPhieuThuCapCuuView>();
                }
                else if (type == 1)
                {
                    chiTietPhieuThuList = (from pt in db.PhieuThuCapCuus
                                           join ctpt in db.ChiTietPhieuThuCapCuuViews on pt.PhieuThuCapCuuGUID equals ctpt.PhieuThuCapCuuGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.NgayThu >= tuNgay && pt.NgayThu <= denNgay &&
                                           pt.ChuaThuTien == false
                                           select ctpt).ToList<ChiTietPhieuThuCapCuuView>();
                }
                else
                {
                    chiTietPhieuThuList = (from pt in db.PhieuThuCapCuus
                                           join ctpt in db.ChiTietPhieuThuCapCuuViews on pt.PhieuThuCapCuuGUID equals ctpt.PhieuThuCapCuuGUID
                                           where pt.Status == (byte)Status.Actived &&
                                           pt.NgayThu >= tuNgay && pt.NgayThu <= denNgay &&
                                           pt.ChuaThuTien == true
                                           select ctpt).ToList<ChiTietPhieuThuCapCuuView>();
                }

                result.QueryResult = chiTietPhieuThuList;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetTatCaHoaDon(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type)
        {
            Result result = new Result();
            
            try
            {
                result = HoaDonThuocBus.GetHoaDonThuocList(isFromDateToDate, fromDate, toDate, tenBenhNhan, type);
                if (!result.IsOK) return result;

                DataTable dtAll = result.QueryResult as DataTable;
                DataColumn col = new DataColumn("LoaiHoaDon", typeof(string));
                dtAll.Columns.Add(col);
                foreach (DataRow row in dtAll.Rows)
                {
                    row["LoaiHoaDon"] = "Hóa đơn thuốc";
                }

                result = HoaDonXuatTruocBus.GetHoaDonXuatTruocList(isFromDateToDate, fromDate, toDate, tenBenhNhan, type);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtAll.NewRow();
                    newRow["Checked"] = false;
                    newRow["HoaDonThuocGUID"] = row["HoaDonXuatTruocGUID"];
                    newRow["SoHoaDon"] = row["SoHoaDon"];
                    newRow["NgayXuatHoaDon"] = row["NgayXuatHoaDon"];
                    newRow["TenNguoiMuaHang"] = row["TenNguoiMuaHang"];
                    newRow["DiaChi"] = row["DiaChi"];
                    newRow["TenDonVi"] = row["TenDonVi"];
                    newRow["MaSoThue"] = row["MaSoThue"];
                    newRow["SoTaiKhoan"] = row["SoTaiKhoan"];
                    newRow["HinhThucThanhToan"] = row["HinhThucThanhToan"];
                    newRow["HinhThucThanhToanStr"] = row["HinhThucThanhToanStr"];
                    newRow["VAT"] = row["VAT"];
                    newRow["CreatedDate"] = row["CreatedDate"];
                    newRow["CreatedBy"] = row["CreatedBy"];
                    newRow["UpdatedDate"] = row["UpdatedDate"];
                    newRow["UpdatedBy"] = row["UpdatedBy"];
                    newRow["DeletedDate"] = row["DeletedDate"];
                    newRow["DeletedBy"] = row["DeletedBy"];
                    newRow["Status"] = row["Status"];
                    newRow["PhieuThuThuocGUIDList"] = string.Empty;
                    newRow["Notes"] = row["Notes"];
                    newRow["LoaiHoaDon"] = "Hóa đơn xuất trước";
                    newRow["DaThuTien"] = row["DaThuTien"];
                    newRow["MauSo"] = row["MauSo"];
                    newRow["KiHieu"] = row["KiHieu"];
                    dtAll.Rows.Add(newRow);
                }

                result = HoaDonHopDongBus.GetHoaDonHopDongList(isFromDateToDate, fromDate, toDate, tenBenhNhan, type);
                if (!result.IsOK) return result;

                dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtAll.NewRow();
                    newRow["Checked"] = false;
                    newRow["HoaDonThuocGUID"] = row["HoaDonHopDongGUID"];
                    newRow["SoHoaDon"] = row["SoHoaDon"];
                    newRow["NgayXuatHoaDon"] = row["NgayXuatHoaDon"];
                    newRow["TenNguoiMuaHang"] = row["TenNguoiMuaHang"];
                    newRow["DiaChi"] = row["DiaChi"];
                    newRow["TenDonVi"] = row["TenDonVi"];
                    newRow["MaSoThue"] = row["MaSoThue"];
                    newRow["SoTaiKhoan"] = row["SoTaiKhoan"];
                    newRow["HinhThucThanhToan"] = row["HinhThucThanhToan"];
                    newRow["HinhThucThanhToanStr"] = row["HinhThucThanhToanStr"];
                    newRow["VAT"] = row["VAT"];
                    newRow["CreatedDate"] = row["CreatedDate"];
                    newRow["CreatedBy"] = row["CreatedBy"];
                    newRow["UpdatedDate"] = row["UpdatedDate"];
                    newRow["UpdatedBy"] = row["UpdatedBy"];
                    newRow["DeletedDate"] = row["DeletedDate"];
                    newRow["DeletedBy"] = row["DeletedBy"];
                    newRow["Status"] = row["Status"];
                    newRow["PhieuThuThuocGUIDList"] = string.Empty;
                    newRow["Notes"] = row["Notes"];
                    newRow["DaThuTien"] = row["DaThuTien"];
                    newRow["LoaiHoaDon"] = "Hóa đơn hợp đồng";
                    newRow["MauSo"] = row["MauSo"];
                    newRow["KiHieu"] = row["KiHieu"];
                    dtAll.Rows.Add(newRow);
                }

                result = InvoiceBus.GetInvoiceList(isFromDateToDate, fromDate, toDate, tenBenhNhan, type);
                if (!result.IsOK) return result;
                dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtAll.NewRow();
                    newRow["Checked"] = false;
                    newRow["HoaDonThuocGUID"] = row["InvoiceGUID"];
                    newRow["SoHoaDon"] = row["InvoiceCode"];
                    newRow["NgayXuatHoaDon"] = row["InvoiceDate"];
                    newRow["TenNguoiMuaHang"] = row["TenNguoiMuaHang"];
                    newRow["DiaChi"] = row["DiaChi"];
                    newRow["TenDonVi"] = row["TenDonVi"];
                    newRow["MaSoThue"] = row["MaSoThue"];
                    newRow["SoTaiKhoan"] = row["SoTaiKhoan"];
                    newRow["HinhThucThanhToan"] = row["HinhThucThanhToan"];
                    newRow["HinhThucThanhToanStr"] = row["HinhThucThanhToanStr"];
                    newRow["VAT"] = row["VAT"];
                    newRow["CreatedDate"] = row["CreatedDate"];
                    newRow["CreatedBy"] = row["CreatedBy"];
                    newRow["UpdatedDate"] = row["UpdatedDate"];
                    newRow["UpdatedBy"] = row["UpdatedBy"];
                    newRow["DeletedDate"] = row["DeletedDate"];
                    newRow["DeletedBy"] = row["DeletedBy"];
                    newRow["Status"] = row["Status"];
                    newRow["PhieuThuThuocGUIDList"] = row["ReceiptGUIDList"];
                    newRow["Notes"] = row["Notes"];
                    newRow["DaThuTien"] = row["DaThuTien"];
                    newRow["LoaiHoaDon"] = "Hóa đơn dịch vụ";
                    newRow["MauSo"] = row["MauSo"];
                    newRow["KiHieu"] = row["KiHieu"];
                    dtAll.Rows.Add(newRow);
                }

                DataTable newDataSource = dtAll.Clone();
                List<DataRow> results = (from p in dtAll.AsEnumerable()
                           orderby p.Field<DateTime>("NgayXuatHoaDon") descending
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                result.QueryResult = newDataSource;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetDanhSachKhachHangMuaThuoc(DateTime fromDate, DateTime toDate, string thuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (thuocGUID == string.Empty)
                {
                    query = string.Format("SELECT PT.PhieuThuThuocGUID, PT.TenBenhNhan, CAST(CAST(DATEPART(yyyy, PT.NgayThu) AS varchar(4)) + '-' + CAST(DATEPART(mm, PT.NgayThu) AS varchar(2)) + '-' + CAST(DATEPART(dd, PT.NgayThu) AS varchar(2)) AS Datetime) AS NgayThu, T.TenThuoc, T.DonViTinh, SUM(CT.SoLuong) AS SoLuong FROM PhieuThuThuoc PT WITH(NOLOCK), ChiTietPhieuThuThuoc CT WITH(NOLOCK), Thuoc T WITH(NOLOCK) WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND CT.ThuocGUID = T.ThuocGUID AND PT.Status = {0} AND CT.Status = {0} AND NgayThu BETWEEN '{1}' AND '{2}' GROUP BY PT.PhieuThuThuocGUID, PT.TenBenhNhan, CAST(CAST(DATEPART(yyyy, PT.NgayThu) AS varchar(4)) + '-' + CAST(DATEPART(mm, PT.NgayThu) AS varchar(2)) + '-' + CAST(DATEPART(dd, PT.NgayThu) AS varchar(2)) AS datetime) , T.TenThuoc, T.DonViTinh",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    query = string.Format("SELECT PT.PhieuThuThuocGUID, PT.TenBenhNhan, CAST(CAST(DATEPART(yyyy, PT.NgayThu) AS varchar(4)) + '-' + CAST(DATEPART(mm, PT.NgayThu) AS varchar(2)) + '-' + CAST(DATEPART(dd, PT.NgayThu) AS varchar(2)) AS Datetime) AS NgayThu, T.TenThuoc, T.DonViTinh, SUM(CT.SoLuong) AS SoLuong FROM PhieuThuThuoc PT WITH(NOLOCK), ChiTietPhieuThuThuoc CT WITH(NOLOCK), Thuoc T WITH(NOLOCK) WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND CT.ThuocGUID = T.ThuocGUID AND PT.Status = {0} AND CT.Status = {0} AND NgayThu BETWEEN '{1}' AND '{2}' AND T.ThuocGUID = '{3}' GROUP BY PT.PhieuThuThuocGUID, PT.TenBenhNhan, CAST(CAST(DATEPART(yyyy, PT.NgayThu) AS varchar(4)) + '-' + CAST(DATEPART(mm, PT.NgayThu) AS varchar(2)) + '-' + CAST(DATEPART(dd, PT.NgayThu) AS varchar(2)) AS datetime) , T.TenThuoc, T.DonViTinh",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), thuocGUID);
                }
                

                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetDanhSachBenhNhanKhamBenh(DateTime fromDate, DateTime toDate, string maBenhNhan, bool isDenKham, int type)
        {
            Result result = new Result();

            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@FromDate", fromDate);
                sqlParams.Add(param);
                param = new SqlParameter("@ToDate", toDate);
                sqlParams.Add(param);
                param = new SqlParameter("@MaBenhNhan", maBenhNhan);
                sqlParams.Add(param);
                param = new SqlParameter("@Type", type);
                sqlParams.Add(param);

                if (isDenKham)
                    return ExcuteQuery("spDanhSachNhanVienDenKham", sqlParams);
                else
                    return ExcuteQuery("spDanhSachNhanVienChuaDenKham", sqlParams);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetThuocTonKhoTheoKhoangThoiGian(DateTime fromDate, DateTime toDate, List<string> maThuocStrList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                List<spThuocTonKhoResult> thuocTonKhos = new List<spThuocTonKhoResult>();
                foreach (string maThuocStr in maThuocStrList)
                {
                    List<spThuocTonKhoResult> results = db.spThuocTonKho(fromDate, toDate, maThuocStr).ToList<spThuocTonKhoResult>();
                    thuocTonKhos.AddRange(results);
                }

                result.QueryResult = thuocTonKhos;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetTonKhoCapCuu(DateTime fromDate, DateTime toDate, string maCapCuuList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                result.QueryResult = db.spCapCuuTonKho(fromDate, toDate, maCapCuuList).ToList<spCapCuuTonKhoResult>();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetNgayKhamCuoiCung(string patientGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT Max(NgayCanDo) FROM CanDo WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} SELECT Max(NgayKham) FROM KetQuaLamSang WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} SELECT Max(NgayKham) FROM KetQuaCanLamSang WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} SELECT Max(NgayKetLuan) FROM KetLuan WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} SELECT Max(Ngay) FROM LoiKhuyen WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} SELECT Max(NgayKham) FROM KetQuaNoiSoi WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} SELECT Max(NgayKham) FROM KetQuaSoiCTC WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} SELECT Max(NgaySieuAm) FROM KetQuaSieuAm WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1}",
                    patientGUID, (byte)Status.Actived);

                result = ExcuteQueryDataSet(query);

                if (!result.IsOK) return result;

                DataSet ds = result.QueryResult as DataSet;
                List<DateTime> ngayCuoiCungList = new List<DateTime>();

                DateTime maxDate = Global.MinDateTime;

                foreach (DataTable dt in ds.Tables)
                {
                    DateTime ngayCuoiCung = Global.MinDateTime;
                    if (dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                        ngayCuoiCung = Convert.ToDateTime(dt.Rows[0][0]);

                    ngayCuoiCungList.Add(ngayCuoiCung);

                    if (maxDate < ngayCuoiCung) maxDate = ngayCuoiCung;
                }

                if (maxDate != Global.MinDateTime)
                {
                    DateTime dtTemp = maxDate.AddDays(-1);
                    DateTime fromDate = new DateTime(dtTemp.Year, dtTemp.Month, dtTemp.Day, 0, 0, 0);

                    for (int i = 0; i < ngayCuoiCungList.Count; i++)
                    {
                        DateTime ngayCuoiCung = ngayCuoiCungList[i];
                        if (ngayCuoiCung == Global.MinDateTime) continue;
                        if (ngayCuoiCung < fromDate) ngayCuoiCungList[i] = Global.MinDateTime;
                    }
                }

                result.QueryResult = ngayCuoiCungList;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetThuocXuatHoaDon(DateTime tuNgay, DateTime denNgay)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT TenThuoc, DonViTinh, SUM(SoLuong) AS SoLuong FROM (SELECT CT.ChiTietHoaDonThuocGUID AS CTGUID, CT.TenThuoc, CT.DonViTinh, CT.SoLuong FROM ChiTietHoaDonThuoc CT WITH(NOLOCK), HoaDonThuoc HD WITH(NOLOCK) WHERE CT.HoaDonThuocGUID = HD.HoaDonThuocGUID AND CT.Loai = 0 AND CT.Status = 0 AND HD.Status = 0 AND HD.NgayXuatHoaDon BETWEEN '{0}' AND '{1}' UNION SELECT CT.InvoiceDetailGUID AS CTGUID, CT.TenDichVu As TenThuoc, CT.DonViTinh, CT.SoLuong FROM InvoiceDetail CT, Invoice HD WHERE CT.InvoiceGUID = HD.InvoiceGUID AND CT.Status = 0 AND HD.Status = 0 AND HD.InvoiceDate BETWEEN '{0}' AND '{1}' AND CT.Loai = 1) T GROUP BY TenThuoc, DonViTinh ORDER BY TenThuoc, DonViTinh",
                    tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"));
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetDoanhThuNhomDichVu(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = new Result();

            try
            {
                string subQuery = "1=1";
                if (type == 1) subQuery = "ChuaThuTien='False'";
                else if (type == 2) subQuery = "ChuaThuTien='True'";

                string query = string.Format("SELECT dbo.ServiceGroup.[Name], SUM(CAST((dbo.ReceiptDetailView.Price - (dbo.ReceiptDetailView.Price * dbo.ReceiptDetailView.Discount)/100) AS float) * dbo.ReceiptDetailView.SoLuong) AS TongTien FROM  dbo.ReceiptDetailView INNER JOIN dbo.Receipt ON dbo.ReceiptDetailView.ReceiptGUID = dbo.Receipt.ReceiptGUID INNER JOIN dbo.Service_ServiceGroup INNER JOIN dbo.ServiceGroup ON dbo.Service_ServiceGroup.ServiceGroupGUID = dbo.ServiceGroup.ServiceGroupGUID ON dbo.ReceiptDetailView.ServiceGUID = dbo.Service_ServiceGroup.ServiceGUID WHERE dbo.Receipt.Status = 0 AND dbo.ReceiptDetailView.ReceiptDetailStatus = 0 AND dbo.ServiceGroup.Status = 0 AND ReceiptDate BETWEEN '{0}' AND '{1}' AND {2} GROUP BY dbo.ServiceGroup.[Name] ORDER BY dbo.ServiceGroup.[Name]",
                    tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), subQuery);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result ThongKeHoaDonDichVuVaThuoc(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = new Result();

            try
            {
                string subQuery = "1 = 1";
                if (type == 1) subQuery = "ChuaThuTien = 0";
                else if (type == 2) subQuery = "ChuaThuTien = 1";

                string query = string.Format("SELECT KiHieu, InvoiceCode AS SoHoaDon, InvoiceDate AS NgayHoaDon, TenNguoiMuaHang, TenDonVi, MaDonVi, MaSoThue, DiaChi, TenDichVu AS TenHangHoa, DonViTinh, SoLuong, DonGia, ThanhTien, CASE HinhThucThanhToan WHEN 0 THEN 'TM' WHEN 1 THEN 'CK' WHEN 2 THEN 'TM/CK' END AS HinhThucThanhToan, HD.ReceiptGUIDList AS PhieuThuGUIDList, HD.VAT FROM Invoice HD, InvoiceDetail CT WHERE HD.InvoiceGUID = CT.InvoiceGUID AND HD.Status = 0 AND CT.Status = 0 AND InvoiceDate BETWEEN '{0}' AND '{1}' AND {2} UNION SELECT KiHieu, SoHoaDon, NgayXuatHoaDon AS NgayHoaDon, TenNguoiMuaHang, TenDonVi, MaDonVi, MaSoThue, DiaChi, TenThuoc AS TenHangHoa, DonViTinh, SoLuong, DonGia, ThanhTien, CASE HinhThucThanhToan WHEN 0 THEN 'TM' WHEN 1 THEN 'CK' WHEN 2 THEN 'TM/CK' END AS HinhThucThanhToan, HD.PhieuThuThuocGUIDList AS PhieuThuGUIDList, HD.VAT FROM HoaDonThuoc HD, ChiTietHoaDonThuoc CT WHERE HD.HoaDonThuocGUID = CT.HoaDonThuocGUID AND HD.Status = 0 AND CT.Status = 0 AND NgayXuatHoaDon BETWEEN '{0}' AND '{1}' AND {2} ORDER BY SoHoaDon",
                    tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), subQuery);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result ThongKePhieuThuDichVuVaThuoc(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = new Result();

            try
            {
                string subQuery = "1 = 1";
                if (type == 1) subQuery = "ChuaThuTien = 0";
                else if (type == 2) subQuery = "ChuaThuTien = 1";

                string query = string.Format("SELECT PT.ReceiptCode AS SoPhieuThu, PT.ReceiptDate AS NgayPhieuThu, PT.FullName AS TenKhachHang, PT.Address AS DiaChi, CT.[Name] AS TenHangHoa, N'Lần' AS DonViTinh, CT.SoLuong, CT.Price AS DonGia, CT.Discount AS Giam, CAST(((CT.Price - (CT.Price * CT.Discount)/100) * CT.SoLuong) AS float) AS ThanhTien, CASE HinhThucThanhToan WHEN 0 THEN 'TM' WHEN 1 THEN 'CK' WHEN 2 THEN 'TM/CK' WHEN 3 THEN 'BH' WHEN 4 THEN 'CT' END AS HinhThucThanhToan, PT.FileNum AS MaKhachHang, CT.Code AS MaHangHoa FROM ReceiptView PT, ReceiptDetailView CT WHERE PT.ReceiptGUID = CT.ReceiptGUID AND PT.Status = 0 AND CT.ReceiptDetailStatus = 0 AND PT.ReceiptDate BETWEEN '{0}' AND '{1}' AND {2} UNION SELECT PT.MaPhieuThuThuoc AS SoPhieuThu, PT.NgayThu AS NgayPhieuThu, PT.TenBenhNhan AS TenKhachHang, PT.DiaChi, CT.TenThuoc AS TenHangHoa, CT.DonViTinh, CT.SoLuong, CT.DonGia, CT.Giam, CAST(((CT.DonGia - (CT.DonGia * CT.Giam)/100) * CT.SoLuong) AS float) AS ThanhTien, CASE HinhThucThanhToan WHEN 0 THEN 'TM' WHEN 1 THEN 'CK' WHEN 2 THEN 'TM/CK' WHEN 3 THEN 'BH' WHEN 4 THEN 'CT' END AS HinhThucThanhToan, PT.MaBenhNhan AS MaKhachHang, CT.MaThuoc AS MaHangHoa FROM PhieuThuThuoc PT, ChiTietPhieuThuThuocView CT WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND PT.Status = 0 AND CT.CTPTTStatus = 0 AND PT.NgayThu BETWEEN '{0}' AND '{1}' AND {2} ORDER BY SoPhieuThu",
                    tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), subQuery);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetSoPhieuThuStr(string phieuThuGUIDList, ref DateTime ngayPhieuThu)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                if (phieuThuGUIDList == null || phieuThuGUIDList.Trim() == string.Empty) return result;
                db = new MMOverride();

                List<string> phieuThuGUIDs = phieuThuGUIDList.Split(",".ToCharArray()).ToList();

                List<Receipt> phieuThuDVList = (from p in db.Receipts
                                                where phieuThuGUIDs.Contains(p.ReceiptGUID.ToString())
                                                orderby p.ReceiptCode ascending
                                                select p).ToList();

                string soPhieuThuStr = string.Empty;
                if (phieuThuDVList != null && phieuThuDVList.Count > 0)
                {
                    ngayPhieuThu = Global.MaxDateTime;
                    foreach (var p in phieuThuDVList)
                    {
                        soPhieuThuStr += string.Format("{0},", p.ReceiptCode);
                        if (ngayPhieuThu > p.ReceiptDate) ngayPhieuThu = p.ReceiptDate;
                    }
                }
                else
                {
                    List<PhieuThuThuoc> phieuThuThuocList = (from p in db.PhieuThuThuocs
                                                            where phieuThuGUIDs.Contains(p.PhieuThuThuocGUID.ToString())
                                                            orderby p.MaPhieuThuThuoc ascending
                                                            select p).ToList();

                    if (phieuThuThuocList != null && phieuThuThuocList.Count > 0)
                    {
                        soPhieuThuStr = string.Empty;
                        ngayPhieuThu = Global.MaxDateTime;
                        foreach (var p in phieuThuThuocList)
                        {
                            soPhieuThuStr += string.Format("{0},", p.MaPhieuThuThuoc);
                            if (ngayPhieuThu > p.NgayThu) ngayPhieuThu = p.NgayThu;
                        }

                        
                    }
                }

                if (soPhieuThuStr != string.Empty) soPhieuThuStr = soPhieuThuStr.Substring(0, soPhieuThuStr.Length - 1);
                result.QueryResult = soPhieuThuStr;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetMaThuocDichVu(string tenHangHoa)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string maHangHoa = (from p in db.Services
                                    where p.Name.Trim().ToLower() == tenHangHoa.Trim().ToLower()
                                    select p.Code).FirstOrDefault();

                if (maHangHoa == null || maHangHoa == string.Empty)
                {
                    maHangHoa = (from p in db.Thuocs
                                 where p.TenThuoc.Trim().ToLower() == tenHangHoa.Trim().ToLower()
                                 select p.MaThuoc).FirstOrDefault();
                }

                result.QueryResult = maHangHoa;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetDoanhThuThuocTheoPhieuThu(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = null;

            try
            {
                string subQuery = "1=1";
                if (type == 1) subQuery = "ChuaThuTien=0";
                else if (type == 2) subQuery = "ChuaThuTien=1";

                string query = string.Format(@"SELECT CT.ThuocGUID, CT.MaThuoc, CT.TenThuoc, SUM(CT.SoLuong) AS SoLuong, SUM(ThanhTien) AS TongTienBan 
                                            FROM PhieuThuThuoc PT, ChiTietPhieuThuThuocView CT 
                                            WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND PT.Status = 0 AND CT.CTPTTStatus = 0 AND 
                                            PT.NgayThu BETWEEN '{0}' AND '{1}' AND {2} 
                                            GROUP BY CT.ThuocGUID, CT.MaThuoc, CT.TenThuoc 
                                            ORDER BY CT.TenThuoc", tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), subQuery);

                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }
    }
}
