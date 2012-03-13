using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
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

        public static Result GetChiTietPhieuThuDichVu(DateTime tuNgay, DateTime denNgay)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ReceiptDetailView> chiTietPhieuThuList = (from pt in db.Receipts
                                                  join ctpt in db.ReceiptDetailViews on pt.ReceiptGUID equals ctpt.ReceiptGUID
                                                  where pt.Status == (byte)Status.Actived && 
                                                  pt.ReceiptDate >= tuNgay && pt.ReceiptDate <= denNgay
                                                  select ctpt).ToList<ReceiptDetailView>();

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

        public static Result GetChiTietPhieuThuThuoc(DateTime tuNgay, DateTime denNgay)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ChiTietPhieuThuThuocView> chiTietPhieuThuList = (from pt in db.PhieuThuThuocs
                                                               join ctpt in db.ChiTietPhieuThuThuocViews on pt.PhieuThuThuocGUID equals ctpt.PhieuThuThuocGUID
                                                               where pt.Status == (byte)Status.Actived &&
                                                               pt.NgayThu >= tuNgay && pt.NgayThu <= denNgay
                                                                      select ctpt).ToList<ChiTietPhieuThuThuocView>();

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
                    newRow["LoaiHoaDon"] = "Hóa đơn hợp đồng";
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
                    newRow["LoaiHoaDon"] = "Hóa đơn dịch vụ";
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
    }
}
