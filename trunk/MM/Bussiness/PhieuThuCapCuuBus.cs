using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using System.Data.Linq;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class PhieuThuCapCuuBus : BusBase
    {
        public static Result GetPhieuThuCapCuuList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type, int type2)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                string subQuery = string.Empty;
                if (type2 == 1 || type2 == 2) subQuery = type2 == 1 ? " AND ChuaThuTien = 0 " : " AND ChuaThuTien = 1 ";

                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuCapCuuView WITH(NOLOCK) WHERE NgayThu BETWEEN '{0}' AND '{1}'{2} ORDER BY NgayThu DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), subQuery);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuCapCuuView WITH(NOLOCK) WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}'{3} ORDER BY NgayThu DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), subQuery);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuCapCuuView WITH(NOLOCK) WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}'{3} ORDER BY NgayThu DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), subQuery);
                    }
                    
                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuCapCuuView WITH(NOLOCK) WHERE TenBenhNhan LIKE N'%{0}%'{1} ORDER BY NgayThu DESC", tenBenhNhan, subQuery);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuCapCuuView WITH(NOLOCK) WHERE Status={0} AND TenBenhNhan LIKE N'%{1}%'{2} ORDER BY NgayThu DESC", 
                        (byte)Status.Actived, tenBenhNhan, subQuery);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuCapCuuView WITH(NOLOCK) WHERE Status={0} AND TenBenhNhan LIKE N'%{1}%'{2} ORDER BY NgayThu DESC", 
                        (byte)Status.Deactived, tenBenhNhan, subQuery);
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

        public static Result GetChiTietPhieuThuCapCuu(string phieuThuCapCuuGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM ChiTietPhieuThuCapCuuView WITH(NOLOCK) WHERE CTPTCCStatus={0} AND KhoCapCuuStatus={0} AND PhieuThuCapCuuGUID='{1}' ORDER BY TenCapCuu",
                    (byte)Status.Actived, phieuThuCapCuuGUID);
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

        public static Result GetPhieuThuCapCuuCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM PhieuThuCapCuu WITH(NOLOCK)";
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                        result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                    else result.QueryResult = 0;
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

            return result;
        }

        public static Result GetPhieuThuCapCuu(string phieuThuCapCuuGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuCapCuu ptcc = db.PhieuThuCapCuus.SingleOrDefault<PhieuThuCapCuu>(p => p.PhieuThuCapCuuGUID.ToString() == phieuThuCapCuuGUID);
                result.QueryResult = ptcc;
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

        public static Result DeletePhieuThuCapCuu(List<string> keys, List<string> noteList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DateTime dt = DateTime.Now;
                    string desc = string.Empty;
                    int index = 0;
                    foreach (string key in keys)
                    {
                        PhieuThuCapCuu ptcc = db.PhieuThuCapCuus.SingleOrDefault<PhieuThuCapCuu>(p => p.PhieuThuCapCuuGUID.ToString() == key);
                        if (ptcc != null)
                        {
                            Status status = (Status)ptcc.Status;
                            ptcc.DeletedDate = DateTime.Now;
                            ptcc.DeletedBy = Guid.Parse(Global.UserGUID);
                            ptcc.Status = (byte)Status.Deactived;
                            ptcc.Notes = noteList[index];

                            if (status == (byte)Status.Actived)
                            {
                                //Update So luong Kho Cap Cuu
                                var ctptts = ptcc.ChiTietPhieuThuCapCuus;
                                foreach (var ct in ctptts)
                                {
                                    int soLuong = Convert.ToInt32(ct.SoLuong);

                                    var nhapKhoCapCuuList = from l in db.NhapKhoCapCuus
                                                            where l.Status == (byte)Status.Actived &&
                                                            l.KhoCapCuuGUID == ct.KhoCapCuuGUID &&
                                                            new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                                            l.SoLuongXuat > 0
                                                            orderby new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) ascending, 
                                                            l.NgayNhap ascending
                                                            select l;

                                    if (nhapKhoCapCuuList != null)
                                    {
                                        foreach (var lt in nhapKhoCapCuuList)
                                        {
                                            if (soLuong > 0)
                                            {
                                                if (lt.SoLuongXuat >= soLuong)
                                                {
                                                    lt.SoLuongXuat -= soLuong;
                                                    soLuong = 0;
                                                    db.SubmitChanges();
                                                    break;
                                                }
                                                else
                                                {
                                                    soLuong -= lt.SoLuongXuat;
                                                    lt.SoLuongXuat = 0;
                                                    db.SubmitChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            desc += string.Format("- GUID: '{0}', Mã phiếu thu: '{1}', Ngày thu: '{2}', Mã bệnh nhân: '{3}', Tên bệnh nhân: '{4}', Địa chỉ: '{5}', Ghi chú: '{6}', Đã thu tiền: '{7}', Lý do giảm: '{8}', Toa thuốc GUID: '{9}', Hình thức thanh toán: '{10}'\n",
                                ptcc.PhieuThuCapCuuGUID.ToString(), ptcc.MaPhieuThuCapCuu, ptcc.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                                ptcc.MaBenhNhan, ptcc.TenBenhNhan, ptcc.DiaChi, noteList[index], !ptcc.ChuaThuTien, ptcc.LyDoGiam, ptcc.ToaCapCuuGUID.ToString(),
                                ptcc.HinhThucThanhToan);
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
                    tk.Action = "Xóa thông tin phiếu thu cấp cứu";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
                    tk.ComputerName = Utility.GetDNSHostName();
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

        public static Result CheckPhieuThuCapCuuExistCode(string phieuThuCapCuuGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuCapCuu ptcc = null;
                if (phieuThuCapCuuGUID == null || phieuThuCapCuuGUID == string.Empty)
                    ptcc = db.PhieuThuCapCuus.SingleOrDefault<PhieuThuCapCuu>(p => p.MaPhieuThuCapCuu.ToLower() == code.ToLower());
                else
                    ptcc = db.PhieuThuCapCuus.SingleOrDefault<PhieuThuCapCuu>(p => p.MaPhieuThuCapCuu.ToLower() == code.ToLower() &&
                                                                p.PhieuThuCapCuuGUID.ToString() != phieuThuCapCuuGUID);

                if (ptcc == null)
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

        public static Result InsertPhieuThuCapCuu(PhieuThuCapCuu ptcc, List<ChiTietPhieuThuCapCuu> addedList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (ptcc.PhieuThuCapCuuGUID == null || ptcc.PhieuThuCapCuuGUID == Guid.Empty)
                    {
                        ptcc.PhieuThuCapCuuGUID = Guid.NewGuid();
                        db.PhieuThuCapCuus.InsertOnSubmit(ptcc);
                        db.SubmitChanges();

                        desc += string.Format("- Phiếu thu cấp cứu: GUID: '{0}', Mã phiếu thu: '{1}', Ngày thu: '{2}', Mã bệnh nhân: '{3}', Tên bệnh nhân: '{4}', Địa chỉ: '{5}', Ghi chú: '{6}', Đã thu tiền: '{7}', Lý do giảm: '{8}', Toa thuốc GUID: '{9}', Hình thức thanh toán: '{10}'\n",
                            ptcc.PhieuThuCapCuuGUID.ToString(), ptcc.MaPhieuThuCapCuu, ptcc.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                            ptcc.MaBenhNhan, ptcc.TenBenhNhan, ptcc.DiaChi, ptcc.Notes, !ptcc.ChuaThuTien, ptcc.LyDoGiam, ptcc.ToaCapCuuGUID.ToString(),
                            ptcc.HinhThucThanhToan);

                        desc += "- Chi tiết phiếu thu cấp cứu được thêm:\n";

                        //Chi tiet phieu thu
                        DateTime dt = DateTime.Now;
                        foreach (ChiTietPhieuThuCapCuu ctptcc in addedList)
                        {
                            ctptcc.PhieuThuCapCuuGUID = ptcc.PhieuThuCapCuuGUID;
                            ctptcc.ChiTietPhieuThuCapCuuGUID = Guid.NewGuid();

                            int soLuong = Convert.ToInt32(ctptcc.SoLuong);
                            
                            var nhapKhoCapCuuList = from l in db.NhapKhoCapCuus
                                                    where l.Status == (byte)Status.Actived &&
                                                    l.KhoCapCuuGUID == ctptcc.KhoCapCuuGUID &&
                                                    new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                                    l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                                    orderby new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) ascending, 
                                                    l.NgayNhap ascending
                                                    select l; 

                            double giaNhapTB = 0;
                            if (nhapKhoCapCuuList != null)
                            {
                                double tongGiaNhap = 0;
                                int count = 0;
                                foreach (var lt in nhapKhoCapCuuList)
                                {
                                    if (soLuong > 0)
                                    {
                                        int soLuongTon = lt.SoLuongNhap * lt.SoLuongQuiDoi - lt.SoLuongXuat;
                                        if (soLuongTon >= soLuong)
                                        {
                                            lt.SoLuongXuat += soLuong;
                                            tongGiaNhap += (soLuong * lt.GiaNhapQuiDoi.Value);
                                            count += soLuong;
                                            soLuong = 0;
                                            db.SubmitChanges();
                                            break;
                                        }
                                        else
                                        {
                                            lt.SoLuongXuat += soLuongTon;
                                            soLuong -= soLuongTon;
                                            tongGiaNhap += (soLuongTon * lt.GiaNhapQuiDoi.Value);
                                            count += soLuongTon;
                                            db.SubmitChanges();
                                        }
                                    }
                                }

                                giaNhapTB = Math.Round(tongGiaNhap / count, 0);
                            }
                           
                            ctptcc.DonGiaNhap = giaNhapTB;
                            db.ChiTietPhieuThuCapCuus.InsertOnSubmit(ctptcc);
                            db.SubmitChanges();

                            desc += string.Format("  + GUID: '{0}', Tên cấp cứu: '{1}', Đơn giá: '{2}', Số lượng: '{3}', Giảm: '{4}', Thành tiền: '{5}', Đơn giá nhập: '{6}'\n",
                                ctptcc.ChiTietPhieuThuCapCuuGUID.ToString(), ctptcc.KhoCapCuu.TenCapCuu, ctptcc.DonGia, ctptcc.SoLuong, ctptcc.Giam, ctptcc.ThanhTien, ctptcc.DonGiaNhap);
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin phiếu thu cấp cứu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    
                    tnx.Complete();
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

        public static Result CapNhatTrangThaiPhieuThu(string phieuThuCapCuuGUID, bool daXuatHD, bool daThuTien, byte hinhThucThanhToan, string ghiChu)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    PhieuThuCapCuu ptcc = db.PhieuThuCapCuus.SingleOrDefault<PhieuThuCapCuu>(p => p.PhieuThuCapCuuGUID.ToString() == phieuThuCapCuuGUID);
                    if (ptcc != null)
                    {
                        ptcc.UpdatedDate = DateTime.Now;
                        ptcc.UpdatedBy = Guid.Parse(Global.UserGUID);
                        ptcc.IsExported = daXuatHD;
                        ptcc.ChuaThuTien = !daThuTien;
                        ptcc.HinhThucThanhToan = hinhThucThanhToan;
                        ptcc.Notes = ghiChu;

                        string desc = string.Format("Phiếu thu cấp cứu: GUID: '{0}', Mã phiếu thu: '{1}', Ngày thu: '{2}', Mã bệnh nhân: '{3}', Tên bệnh nhân: '{4}', Địa chỉ: '{5}', Ghi chú: '{6}', Đã thu tiền: '{7}', Đã xuất HĐ: '{8}', Hình thức thanh toán: '{9}'",
                            ptcc.PhieuThuCapCuuGUID.ToString(), ptcc.MaPhieuThuCapCuu, ptcc.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                            ptcc.MaBenhNhan, ptcc.TenBenhNhan, ptcc.DiaChi, ptcc.Notes, !ptcc.ChuaThuTien, ptcc.IsExported, ptcc.HinhThucThanhToan);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Sửa trạng thái phiếu thu cấp cứu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }

                    tnx.Complete();
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
