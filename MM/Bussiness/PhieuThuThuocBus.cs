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
    public class PhieuThuThuocBus : BusBase
    {
        public static Result GetPhieuThuThuocList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuThuocView WHERE NgayThu BETWEEN '{0}' AND '{1}' ORDER BY NgayThu DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuThuocView WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}' ORDER BY NgayThu DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuThuocView WHERE Status={0} AND NgayThu BETWEEN '{1}' AND '{2}' ORDER BY NgayThu DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    
                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuThuocView WHERE TenBenhNhan LIKE N'%{0}%' ORDER BY NgayThu DESC", tenBenhNhan);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuThuocView WHERE Status={0} AND TenBenhNhan LIKE N'%{1}%' ORDER BY NgayThu DESC", 
                        (byte)Status.Actived, tenBenhNhan);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM PhieuThuThuocView WHERE Status={0} AND TenBenhNhan LIKE N'%{1}%' ORDER BY NgayThu DESC", 
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

        public static Result GetChiTietPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM ChiTietPhieuThuThuocView WHERE CTPTTStatus={0} AND ThuocStatus={0} AND PhieuThuThuocGUID='{1}' ORDER BY TenThuoc", 
                    (byte)Status.Actived, phieuThuThuocGUID);
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

        public static Result GetPhieuThuThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM PhieuThuThuoc";
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

        public static Result GetPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuThuoc ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.PhieuThuThuocGUID.ToString() == phieuThuThuocGUID);
                result.QueryResult = ptthuoc;
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

        public static Result DeletePhieuThuThuoc(List<string> phieuThuThuocKeys, List<string> noteList)
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
                    foreach (string key in phieuThuThuocKeys)
                    {
                        PhieuThuThuoc ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.PhieuThuThuocGUID.ToString() == key);
                        if (ptthuoc != null)
                        {
                            Status status = (Status)ptthuoc.Status;
                            ptthuoc.DeletedDate = DateTime.Now;
                            ptthuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            ptthuoc.Status = (byte)Status.Deactived;
                            ptthuoc.Notes = noteList[index];

                            if (status == (byte)Status.Actived)
                            {
                                //Update So luong Lo thuoc
                                var ctptts = ptthuoc.ChiTietPhieuThuThuocs;
                                foreach (var ctptt in ctptts)
                                {
                                    int soLuong = Convert.ToInt32(ctptt.SoLuong);

                                    var loThuocList = from l in db.LoThuocs
                                                      where l.Status == (byte)Status.Actived &&
                                                      l.ThuocGUID == ctptt.ThuocGUID &&
                                                      new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) > dt &&
                                                      l.SoLuongXuat > 0
                                                      orderby new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) ascending, l.CreatedDate ascending
                                                      select l;

                                    if (loThuocList != null)
                                    {
                                        foreach (var lt in loThuocList)
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
                                    else
                                        Utility.WriteToTraceLog(string.Format("Không tồn tại lô thuốc: '{0}', Mã phiếu thu: '{1}'",
                                        ctptt.ThuocGUID.ToString(), ptthuoc.MaPhieuThuThuoc));
                                }
                            }

                            string maToaThuoc = string.Empty;
                            if (ptthuoc.ToaThuocGUID.Value != Guid.Empty)
                                maToaThuoc = db.ToaThuocs.SingleOrDefault<ToaThuoc>(tt => tt.ToaThuocGUID == ptthuoc.ToaThuocGUID.Value).MaToaThuoc;

                            desc += string.Format("- GUID: '{0}', Mã toa thuốc: '{1}', Mã phiếu thu: '{2}', Ngày thu: '{3}', Mã bệnh nhân: '{4}', Tên bệnh nhân: '{5}', Địa chỉ: '{6}', Ghi chú: '{7}', Đã thu tiền: '{8}'\n",
                                ptthuoc.PhieuThuThuocGUID.ToString(), maToaThuoc, ptthuoc.MaPhieuThuThuoc, ptthuoc.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"), 
                                ptthuoc.MaBenhNhan, ptthuoc.TenBenhNhan, ptthuoc.DiaChi, noteList[index], !ptthuoc.ChuaThuTien);
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
                    tk.Action = "Xóa thông tin phiếu thu thuốc";
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

        public static Result CheckPhieuThuThuocExistCode(string phieuThuThuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuThuoc ptthuoc = null;
                if (phieuThuThuocGUID == null || phieuThuThuocGUID == string.Empty)
                    ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.MaPhieuThuThuoc.ToLower() == code.ToLower());
                else
                    ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.MaPhieuThuThuoc.ToLower() == code.ToLower() &&
                                                                p.PhieuThuThuocGUID.ToString() != phieuThuThuocGUID);

                if (ptthuoc == null)
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

        public static Result InsertPhieuThuThuoc(PhieuThuThuoc ptthuoc, List<ChiTietPhieuThuThuoc> addedList)
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
                    if (ptthuoc.PhieuThuThuocGUID == null || ptthuoc.PhieuThuThuocGUID == Guid.Empty)
                    {
                        ptthuoc.PhieuThuThuocGUID = Guid.NewGuid();
                        db.PhieuThuThuocs.InsertOnSubmit(ptthuoc);
                        db.SubmitChanges();

                        string maToaThuoc = string.Empty;
                        if (ptthuoc.ToaThuocGUID.Value != Guid.Empty)
                            maToaThuoc = db.ToaThuocs.SingleOrDefault<ToaThuoc>(tt => tt.ToaThuocGUID == ptthuoc.ToaThuocGUID.Value).MaToaThuoc;

                        desc += string.Format("- Phiếu thu thuốc: GUID: '{0}', Mã toa thuốc: '{1}', Mã phiếu thu: '{2}', Ngày thu: '{3}', Mã bệnh nhân: '{4}', Tên bệnh nhân: '{5}', Địa chỉ: '{6}', Ghi chú: '{7}', Đã thu tiền: '{8}'\n",
                            ptthuoc.PhieuThuThuocGUID.ToString(), maToaThuoc, ptthuoc.MaPhieuThuThuoc, ptthuoc.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                            ptthuoc.MaBenhNhan, ptthuoc.TenBenhNhan, ptthuoc.DiaChi, ptthuoc.Notes, !ptthuoc.ChuaThuTien);

                        desc += "- Chi tiết phiếu thu thuốc được thêm:\n";

                        //Chi tiet phieu thu
                        DateTime dt = DateTime.Now;
                        foreach (ChiTietPhieuThuThuoc ctptt in addedList)
                        {
                            ctptt.PhieuThuThuocGUID = ptthuoc.PhieuThuThuocGUID;
                            ctptt.ChiTietPhieuThuThuocGUID = Guid.NewGuid();

                            int soLuong = Convert.ToInt32(ctptt.SoLuong);
                            if (soLuong <= 0)
                            {
                                Utility.WriteToTraceLog(string.Format("Số lượng: '{0}', Mã thuốc: '{1}', Mã phiếu thu: '{2}'",
                                    soLuong, ctptt.ThuocGUID.ToString(), ptthuoc.MaPhieuThuThuoc));
                            }

                            var loThuocList = from l in db.LoThuocs
                                              where l.Status == (byte)Status.Actived &&
                                              l.ThuocGUID == ctptt.ThuocGUID &&
                                              new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) > dt &&
                                              l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                              orderby new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) ascending, l.CreatedDate ascending
                                              select l; 

                            double giaNhapTB = 0;
                            if (loThuocList != null)
                            {
                                double tongGiaNhap = 0;
                                int count = 0;
                                foreach (var lt in loThuocList)
                                {
                                    if (soLuong > 0)
                                    {
                                        int soLuongTon = lt.SoLuongNhap * lt.SoLuongQuiDoi - lt.SoLuongXuat;
                                        if (soLuongTon >= soLuong)
                                        {
                                            lt.SoLuongXuat += soLuong;
                                            tongGiaNhap += (soLuong * lt.GiaNhapQuiDoi);
                                            count += soLuong;
                                            soLuong = 0;
                                            db.SubmitChanges();
                                            break;
                                        }
                                        else
                                        {
                                            lt.SoLuongXuat += soLuongTon;
                                            soLuong -= soLuongTon;
                                            tongGiaNhap += (soLuongTon * lt.GiaNhapQuiDoi);
                                            count += soLuongTon;
                                            db.SubmitChanges();
                                        }
                                    }
                                }

                                giaNhapTB = Math.Round(tongGiaNhap / count, 0);
                            }
                            else
                                Utility.WriteToTraceLog(string.Format("Không tồn tại lô thuốc: '{0}', Mã phiếu thu: '{1}'", 
                                    ctptt.ThuocGUID.ToString(), ptthuoc.MaPhieuThuThuoc));

                            ctptt.DonGiaNhap = giaNhapTB;
                            db.ChiTietPhieuThuThuocs.InsertOnSubmit(ctptt);
                            db.SubmitChanges();

                            desc += string.Format("  + GUID: '{0}', Thuốc: '{1}', Đơn giá: '{2}', Số lượng: '{3}', Giảm: '{4}', Thành tiền: '{5}', Đơn giá nhập: '{6}'\n",
                                ctptt.ChiTietPhieuThuThuocGUID.ToString(), ctptt.Thuoc.TenThuoc, ctptt.DonGia, ctptt.SoLuong, ctptt.Giam, ctptt.ThanhTien, ctptt.DonGiaNhap);
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin phiếu thu thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
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

        public static Result CapNhatTrangThaiPhieuThu(string phieuThuThuocGUID, bool daXuatHD, bool daThuTien)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    PhieuThuThuoc ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.PhieuThuThuocGUID.ToString() == phieuThuThuocGUID);
                    if (ptthuoc != null)
                    {
                        ptthuoc.UpdatedDate = DateTime.Now;
                        ptthuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                        ptthuoc.IsExported = daXuatHD;
                        ptthuoc.ChuaThuTien = !daThuTien;

                        string maToaThuoc = string.Empty;
                        if (ptthuoc.ToaThuocGUID.Value != Guid.Empty)
                            maToaThuoc = db.ToaThuocs.SingleOrDefault<ToaThuoc>(tt => tt.ToaThuocGUID == ptthuoc.ToaThuocGUID.Value).MaToaThuoc;

                        string desc = string.Format("Phiếu thu thuốc: GUID: '{0}', Mã toa thuốc: '{1}', Mã phiếu thu: '{2}', Ngày thu: '{3}', Mã bệnh nhân: '{4}', Tên bệnh nhân: '{5}', Địa chỉ: '{6}', Ghi chú: '{7}', Đã thu tiền: '{8}', Đã xuất HĐ: '{9}'",
                            ptthuoc.PhieuThuThuocGUID.ToString(), maToaThuoc, ptthuoc.MaPhieuThuThuoc, ptthuoc.NgayThu.ToString("dd/MM/yyyy HH:mm:ss"),
                            ptthuoc.MaBenhNhan, ptthuoc.TenBenhNhan, ptthuoc.DiaChi, ptthuoc.Notes, !ptthuoc.ChuaThuTien, ptthuoc.IsExported);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Sửa trạng thái phiếu thu thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
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
