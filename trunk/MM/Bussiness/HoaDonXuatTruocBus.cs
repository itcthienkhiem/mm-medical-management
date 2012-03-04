using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;


namespace MM.Bussiness
{
    public class HoaDonXuatTruocBus : BusBase
    {
        public static Result GetHoaDonXuatTruocList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HoaDonXuatTruocView WHERE Status={0} ORDER BY NgayXuatHoaDon DESC", (byte)Status.Actived);
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

        public static Result GetHoaDonXuatTruocList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HoaDonXuatTruocView WHERE NgayXuatHoaDon BETWEEN '{0}' AND '{1}' ORDER BY NgayXuatHoaDon DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HoaDonXuatTruocView WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HoaDonXuatTruocView WHERE Status={0} AND NgayXuatHoaDon BETWEEN '{1}' AND '{2}' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HoaDonXuatTruocView WHERE TenNguoiMuaHang LIKE N'%{0}%' ORDER BY NgayXuatHoaDon DESC", tenBenhNhan);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HoaDonXuatTruocView WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Actived, tenBenhNhan);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HoaDonXuatTruocView WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY NgayXuatHoaDon DESC",
                        (byte)Status.Deactived, tenBenhNhan);
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

        public static Result GetChiTietHoaDonXuatTruoc(string hoaDonXuatTruocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TenThuoc, SoLuong, DonViTinh, DonGia, ThanhTien FROM ChiTietHoaDonXuatTruoc WHERE HoaDonXuatTruocGUID='{0}' AND Status={1} ORDER BY TenMatHang",
                    hoaDonXuatTruocGUID, (byte)Status.Actived);
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

        public static Result GetHoaDonXuatTruoc(string hoaDonXuatTruocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                HoaDonXuatTruocView hdt = db.HoaDonXuatTruocViews.SingleOrDefault<HoaDonXuatTruocView>(h => h.HoaDonXuatTruocGUID.ToString() == hoaDonXuatTruocGUID &&
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
                HoaDonXuatTruoc hdt = (from i in db.HoaDonXuatTruocs
                                   where i.SoHoaDon == soHoaDon &&
                                   i.Status == (byte)Status.Deactived
                                   orderby i.NgayXuatHoaDon descending
                                   select i).FirstOrDefault();

                if (hdt != null)
                    result.QueryResult = hdt.NgayXuatHoaDon;
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

        public static Result CheckHoaDonXuatTruocExistCode(int soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon && q.DaXuat == true);

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

        public static Result DeleteHoaDonXuatTruoc(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        HoaDonXuatTruoc hdt = db.HoaDonXuatTruocs.SingleOrDefault<HoaDonXuatTruoc>(i => i.HoaDonXuatTruocGUID.ToString() == key);
                        if (hdt != null)
                        {
                            hdt.DeletedDate = DateTime.Now;
                            hdt.DeletedBy = Guid.Parse(Global.UserGUID);
                            hdt.Status = (byte)Status.Deactived;

                            int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                            QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon);
                            if (qlshd != null) qlshd.DaXuat = false;
                            else
                            {
                                qlshd = new QuanLySoHoaDon();
                                qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                                qlshd.SoHoaDon = soHoaDon;
                                qlshd.DaXuat = false;
                                qlshd.XuatTruoc = true;
                                db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                            }

                            desc += string.Format("- GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}'\n",
                                hdt.HoaDonXuatTruocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan,
                                hdt.HinhThucThanhToan == 0 ? "Tiền mặt" : "Chuyển khoản");
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin hóa đơn xuất trước";
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

        public static Result InsertHoaDonXuatTruoc(HoaDonXuatTruoc hdt, List<ChiTietHoaDonXuatTruoc> addedDetails)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    hdt.HoaDonXuatTruocGUID = Guid.NewGuid();
                    db.HoaDonXuatTruocs.InsertOnSubmit(hdt);
                    db.SubmitChanges();

                    desc += string.Format("- Hóa đơn xuất trước: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}'\n",
                                hdt.HoaDonXuatTruocGUID.ToString(), hdt.SoHoaDon, hdt.NgayXuatHoaDon.ToString("dd/MM/yyyy HH:mm:ss"),
                                hdt.TenNguoiMuaHang, hdt.TenDonVi, hdt.DiaChi, hdt.SoTaiKhoan, hdt.HinhThucThanhToan == 0 ? "Tiền mặt" : "Chuyển khoản");

                    if (addedDetails != null && addedDetails.Count > 0)
                    {
                        desc += "- Chi tiết hóa đơn:\n";

                        foreach (ChiTietHoaDonXuatTruoc detail in addedDetails)
                        {
                            detail.ChiTietHoaDonXuatTruocGUID = Guid.NewGuid();
                            detail.HoaDonXuatTruocGUID = hdt.HoaDonXuatTruocGUID;
                            db.ChiTietHoaDonXuatTruocs.InsertOnSubmit(detail);

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', ĐVT: '{2}', Số lượng: '{3}', Đơn giá: '{4}', Thành tiền: '{5}'\n",
                                detail.ChiTietHoaDonXuatTruocGUID.ToString(), detail.TenMatHang, detail.DonViTinh, detail.SoLuong, detail.DonGia, detail.ThanhTien);
                        }

                        db.SubmitChanges();
                    }

                    int soHoaDon = Convert.ToInt32(hdt.SoHoaDon);
                    QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon);
                    if (qlshd != null) qlshd.DaXuat = true;
                    else
                    {
                        qlshd = new QuanLySoHoaDon();
                        qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                        qlshd.SoHoaDon = soHoaDon;
                        qlshd.DaXuat = true;
                        qlshd.XuatTruoc = true;
                        db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin hóa đơn xuất trước";
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
    }
}
