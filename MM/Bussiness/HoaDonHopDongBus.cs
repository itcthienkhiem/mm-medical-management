using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class HoaDonHopDongBus : BusBase
    {
        public static Result GetHoaDonHopDongList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonHopDongView WHERE Status={0} ORDER BY NgayXuatHoaDon DESC", (byte)Status.Actived);
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

        public static Result GetHoaDonHopDongList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenKhachHang, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonHopDongView WHERE NgayXuatHoaDon BETWEEN '{0}' AND '{1}' ORDER BY NgayXuatHoaDon DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonHopDongView WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonHopDongView WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonHopDongView WHERE TenNguoiMuaHang LIKE N'%{0}%' ORDER BY NgayXuatHoaDon DESC", tenKhachHang);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonHopDongView WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, tenKhachHang);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM HoaDonHopDongView WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Deactived, tenKhachHang);
                    }

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

        public static Result GetChiTietHoaDonHopDong(string hoaDonHopDongGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TenMatHang, SoLuong, DonViTinh, DonGia, ThanhTien FROM ChiTietHoaDonHopDong WHERE HoaDonHopDongGUID='{0}' AND Status={1} ORDER BY TenMatHang",
                    hoaDonHopDongGUID, (byte)Status.Actived);
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

        public static Result GetChiTietPhieuThuHopDong(string phieuThuHopDongGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT DichVu, DonViTinh, SoLuong, DonGia, ThanhTien FROM ChiTietPhieuThuHopDong WHERE PhieuThuHopDongGUID='{0}' AND Status={1} ORDER BY DichVu",
                    phieuThuHopDongGUID, (byte)Status.Actived);
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

        public static Result GetChiTietPhieuThuHopDong(List<DataRow> phieuThuHopDongList)
        {
            Result result = new Result();

            try
            {
                if (phieuThuHopDongList == null || phieuThuHopDongList.Count <= 0) return result;
                DataTable dtAll = null;

                foreach (DataRow row in phieuThuHopDongList)
                {
                    string phieuThuHopDongGUID = row["PhieuThuHopDongGUID"].ToString();
                    string query = string.Format("SELECT DichVu, N'Lần' AS DonViTinh , SoLuong, DonGia, ThanhTien FROM ChiTietPhieuThuHopDong WHERE PhieuThuHopDongGUID='{0}' AND Status={1} ORDER BY DichVu",
                    phieuThuHopDongGUID, (byte)Status.Actived);

                    result = ExcuteQuery(query);

                    if (!result.IsOK) return result;

                    DataTable dt = result.QueryResult as DataTable;
                    if (dtAll == null)
                    {
                        dtAll = new DataTable();
                        dtAll = dt;
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dtAll.ImportRow(dr);
                        }
                    }
                }

                result.QueryResult = dtAll;
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

        public static Result GetHoaDonHopDong(string hoaDonHopDongGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                HoaDonHopDongView hdt = db.HoaDonHopDongViews.SingleOrDefault<HoaDonHopDongView>(h => h.HoaDonHopDongGUID.ToString() == hoaDonHopDongGUID &&
                    h.Status == (byte)Status.Actived);
                result.QueryResult = hdt;
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

        public static Result GetNgayXuatHoaDon(string soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                HoaDonHopDong hdt = (from i in db.HoaDonHopDongs
                                   where i.SoHoaDon == soHoaDon &&
                                   i.Status == (byte)Status.Deactived &&
                                   i.NgayXuatHoaDon >= Global.NgayThayDoiSoHoaDonSauCung
                                   orderby i.NgayXuatHoaDon descending
                                   select i).FirstOrDefault();

                if (hdt != null)
                    result.QueryResult = hdt.NgayXuatHoaDon.Value;
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

        public static Result CheckHoaDonHopDongExistCode(int soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                   (q.DaXuat == true || q.XuatTruoc == true) && q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);

                if (qlshd == null)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
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

        public static Result DeleteHoaDonHopDong(List<string> keys, List<string> noteList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    int index = 0;
                    foreach (string key in keys)
                    {
                        HoaDonHopDong hdt = db.HoaDonHopDongs.SingleOrDefault<HoaDonHopDong>(i => i.HoaDonHopDongGUID.ToString() == key);
                        if (hdt != null)
                        {
                            hdt.DeletedDate = DateTime.Now;
                            hdt.DeletedBy = Guid.Parse(Global.UserGUID);
                            hdt.Status = (byte)Status.Deactived;
                            hdt.Notes = noteList[index];

                            //Update Exported Invoice
                            if (hdt.PhieuThuHopDongGUIDList != null && hdt.PhieuThuHopDongGUIDList.Trim() != string.Empty)
                            {
                                string[] pttkeys = hdt.PhieuThuHopDongGUIDList.Split(',');
                                foreach (string pttKey in pttkeys)
                                {
                                    PhieuThuHopDong ptt = db.PhieuThuHopDongs.SingleOrDefault<PhieuThuHopDong>(r => r.PhieuThuHopDongGUID.ToString() == pttKey);
                                    if (ptt != null) ptt.IsExported = false;
                                }

                            }

                            int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                            QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                                q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                            if (qlshd != null) qlshd.DaXuat = false;
                            else
                            {
                                qlshd = new QuanLySoHoaDon();
                                qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                                qlshd.SoHoaDon = soHoaDon;
                                qlshd.DaXuat = false;
                                qlshd.XuatTruoc = false;
                                qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                                db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                            }

                            string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);

                            desc += string.Format("- GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonHopDongGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan, htttStr, noteList[index], !hdt.ChuaThuTien);
                        }

                        index++;
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin hóa đơn hợp đồng";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
                    db.Trackings.InsertOnSubmit(tk);

                    db.SubmitChanges();
                    t.Complete();
                }
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

        public static Result InsertHoaDonHopDong(HoaDonHopDong hdt, List<ChiTietHoaDonHopDong> addedDetails)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    hdt.HoaDonHopDongGUID = Guid.NewGuid();
                    db.HoaDonHopDongs.InsertOnSubmit(hdt);
                    db.SubmitChanges();

                    string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)hdt.HinhThucThanhToan);

                    desc += string.Format("- Hóa đơn hợp đồng: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                hdt.HoaDonHopDongGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan, htttStr, hdt.Notes, !hdt.ChuaThuTien);

                    if (addedDetails != null && addedDetails.Count > 0)
                    {
                        desc += "- Chi tiết hóa đơn:\n";

                        foreach (ChiTietHoaDonHopDong detail in addedDetails)
                        {
                            detail.ChiTietHoaDonHopDongGUID = Guid.NewGuid();
                            detail.HoaDonHopDongGUID = hdt.HoaDonHopDongGUID;
                            db.ChiTietHoaDonHopDongs.InsertOnSubmit(detail);

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', ĐVT: '{2}', Số lượng: '{3}', Đơn giá: '{4}', Thành tiền: '{5}'\n",
                                detail.ChiTietHoaDonHopDongGUID.ToString(), detail.TenMatHang, detail.DonViTinh, detail.SoLuong, detail.DonGia, detail.ThanhTien);
                        }

                        db.SubmitChanges();
                    }

                    //Update Exported Invoice
                    if (hdt.PhieuThuHopDongGUIDList != null && hdt.PhieuThuHopDongGUIDList.Trim() != string.Empty)
                    {
                        string[] pttkeys = hdt.PhieuThuHopDongGUIDList.Split(',');
                        foreach (string pttKey in pttkeys)
                        {
                            PhieuThuHopDong ptt = db.PhieuThuHopDongs.SingleOrDefault<PhieuThuHopDong>(r => r.PhieuThuHopDongGUID.ToString() == pttKey);
                            if (ptt != null) ptt.IsExported = true;
                        }

                    }

                    int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                    QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                        q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                    if (qlshd != null) qlshd.DaXuat = true;
                    else
                    {
                        qlshd = new QuanLySoHoaDon();
                        qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                        qlshd.SoHoaDon = soHoaDon;
                        qlshd.DaXuat = true;
                        qlshd.XuatTruoc = false;
                        qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                        db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                    }
                    
                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin hóa đơn hợp đồng";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
                    db.Trackings.InsertOnSubmit(tk);

                    db.SubmitChanges();
                    t.Complete();
                }

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

        public static Result UpdateDaThuTienInvoice(string hoaDonHopDongGUID, bool daThuTien)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    HoaDonHopDong invoice = db.HoaDonHopDongs.SingleOrDefault<HoaDonHopDong>(i => i.HoaDonHopDongGUID.ToString() == hoaDonHopDongGUID);
                    if (invoice != null)
                    {
                        invoice.UpdatedDate = DateTime.Now;
                        invoice.UpdatedBy = Guid.Parse(Global.UserGUID);
                        invoice.ChuaThuTien = !daThuTien;

                        string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)invoice.HinhThucThanhToan);

                        desc += string.Format("- Hóa đơn dịch vụ: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                    invoice.HoaDonHopDongGUID.ToString(), invoice.SoHoaDon, invoice.NgayXuatHoaDon.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                    invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan, htttStr, invoice.Notes, !invoice.ChuaThuTien);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Cập nhật thông tin hóa đơn hợp đồng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }


                    t.Complete();
                }

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
    }
}
