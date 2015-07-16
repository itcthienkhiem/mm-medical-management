using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Text;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class TuVanKhachHangBus : BusBase
    {
        public static Result GetTuVanKhachHangList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TuVanKhachHangView WITH(NOLOCK) WHERE Status={0} ORDER BY ContactDate DESC", (byte)Status.Actived);
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

        public static Result GetTuVanKhachHangList(DateTime fromDate, DateTime toDate, string tenBenhNhan, string tenNguoiTao, string bacSiPhuTrach, int inOut)
        {
            Result result = null;

            try
            {
                string query = string.Empty;

                if (inOut == 0)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TuVanKhachHangView WITH(NOLOCK) WHERE Status={0} AND TenKhachHang LIKE N'%{1}%' AND ContactDate BETWEEN '{2}' AND '{3}' AND NguoiTao LIKE N'%{4}%' AND BacSiPhuTrach LIKE N'%{5}%' ORDER BY ContactDate DESC",
                            (byte)Status.Actived, tenBenhNhan, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), tenNguoiTao, bacSiPhuTrach);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TuVanKhachHangView WITH(NOLOCK) WHERE Status={0} AND TenKhachHang LIKE N'%{1}%' AND ContactDate BETWEEN '{2}' AND '{3}' AND NguoiTao LIKE N'%{4}%' AND BacSiPhuTrach LIKE N'%{5}%' AND isIN = '{6}' ORDER BY ContactDate DESC",
                        (byte)Status.Actived, tenBenhNhan, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), tenNguoiTao, bacSiPhuTrach, inOut == 1 ? true : false);
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

        public static Result DeleteTuVanKhachHang(List<string> keys)
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
                        TuVanKhachHang s = db.TuVanKhachHangs.SingleOrDefault<TuVanKhachHang>(ss => ss.TuVanKhachHangGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;
                            desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}', Bác sĩ phụ trách: '{9}', Đã xong: '{10}', IN: '{11}', Số tổng đài: '{12}', Bán thẻ: '{13}'\n",
                                s.TuVanKhachHangGUID.ToString(), s.TenKhachHang, s.SoDienThoai, s.DiaChi, s.YeuCau, s.Nguon, s.Note, s.NguoiKetLuan, s.KetLuan, s.BacSiPhuTrachGUID, s.DaXong, s.IsIN, s.SoTongDai, s.BanThe);
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
                    tk.Action = "Xóa tư vấn khách hàng";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
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

        public static Result InsertTuVanKhachHang(TuVanKhachHang tuVanKhachHang)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (tuVanKhachHang.TuVanKhachHangGUID == null || tuVanKhachHang.TuVanKhachHangGUID == Guid.Empty)
                    {
                        tuVanKhachHang.TuVanKhachHangGUID = Guid.NewGuid();
                        db.TuVanKhachHangs.InsertOnSubmit(tuVanKhachHang);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}', Bác sĩ phụ trách: '{9}', Đã xong: '{10}', IN: '{11}', Số tổng đài: '{12}', Bán thẻ: '{13}'",
                               tuVanKhachHang.TuVanKhachHangGUID.ToString(), tuVanKhachHang.TenKhachHang, tuVanKhachHang.SoDienThoai, tuVanKhachHang.DiaChi,
                               tuVanKhachHang.YeuCau, tuVanKhachHang.Nguon, tuVanKhachHang.Note, tuVanKhachHang.NguoiKetLuan, tuVanKhachHang.KetLuan, 
                               tuVanKhachHang.BacSiPhuTrachGUID, tuVanKhachHang.DaXong, tuVanKhachHang.IsIN, tuVanKhachHang.SoTongDai, tuVanKhachHang.BanThe);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm tư vấn khách hàng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        TuVanKhachHang ykkh = db.TuVanKhachHangs.SingleOrDefault<TuVanKhachHang>(s => s.TuVanKhachHangGUID == tuVanKhachHang.TuVanKhachHangGUID);
                        if (ykkh != null)
                        {
                            ykkh.PatientGUID = tuVanKhachHang.PatientGUID;
                            ykkh.TenCongTy = tuVanKhachHang.TenCongTy;
                            ykkh.MaKhachHang = tuVanKhachHang.MaKhachHang;
                            ykkh.MucDich = tuVanKhachHang.MucDich;
                            ykkh.TenKhachHang = tuVanKhachHang.TenKhachHang;
                            ykkh.SoDienThoai = tuVanKhachHang.SoDienThoai;
                            ykkh.DiaChi = tuVanKhachHang.DiaChi;
                            ykkh.YeuCau = tuVanKhachHang.YeuCau;
                            ykkh.Nguon = tuVanKhachHang.Nguon;
                            ykkh.Note = tuVanKhachHang.Note;
                            ykkh.ContactDate = tuVanKhachHang.ContactDate;
                            ykkh.ContactBy = tuVanKhachHang.ContactBy;
                            ykkh.UpdatedDate = tuVanKhachHang.UpdatedDate;
                            ykkh.UpdatedBy = tuVanKhachHang.UpdatedBy;
                            ykkh.DeletedDate = tuVanKhachHang.DeletedDate;
                            ykkh.DeletedBy = tuVanKhachHang.DeletedBy;
                            ykkh.Status = tuVanKhachHang.Status;
                            ykkh.NguoiKetLuan = tuVanKhachHang.NguoiKetLuan;
                            ykkh.KetLuan = tuVanKhachHang.KetLuan;
                            ykkh.BacSiPhuTrachGUID = tuVanKhachHang.BacSiPhuTrachGUID;
                            ykkh.DaXong = tuVanKhachHang.DaXong;
                            ykkh.BanThe = tuVanKhachHang.BanThe;
                            ykkh.IsIN = tuVanKhachHang.IsIN;
                            ykkh.SoTongDai = tuVanKhachHang.SoTongDai;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}', Bác sĩ phụ trách: '{9}', Đã xong: '{10}', IN: '{11}', Số tổng đài: '{12}', Bán thẻ: '{13}'",
                               ykkh.TuVanKhachHangGUID.ToString(), ykkh.TenKhachHang, ykkh.SoDienThoai, ykkh.DiaChi, ykkh.YeuCau, ykkh.Nguon, ykkh.Note, ykkh.NguoiKetLuan, 
                               ykkh.KetLuan, ykkh.BacSiPhuTrachGUID, ykkh.DaXong, ykkh.IsIN, ykkh.SoTongDai, ykkh.BanThe);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa tư vấn khách hàng";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
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

        public static Result GetNgayLienHeBenhNhanGanNhat(string patientGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                var dt = (from y in db.TuVanKhachHangs
                                       where y.PatientGUID.HasValue && y.Status == (byte)Status.Actived &&
                                       y.PatientGUID.Value.ToString() == patientGUID
                                       select y.ContactDate).Max();

                result.QueryResult = dt;
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

        public static Result CheckKhachHangExist(string tenKhachHang, string tuVanKhachHangGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                TuVanKhachHang ykkh = null;
                if (tuVanKhachHangGUID == null || tuVanKhachHangGUID == string.Empty)
                    ykkh = db.TuVanKhachHangs.FirstOrDefault<TuVanKhachHang>(n => n.TenKhachHang.ToLower() == tenKhachHang.ToLower() &&
                        n.ContactBy.Value.ToString() == Global.UserGUID && n.Status == (byte)Status.Actived);
                else
                    ykkh = db.TuVanKhachHangs.FirstOrDefault<TuVanKhachHang>(n => n.TenKhachHang.ToLower() == tenKhachHang.ToLower() &&
                        n.ContactBy.Value.ToString() == Global.UserGUID && n.TuVanKhachHangGUID.ToString() != tuVanKhachHangGUID &&
                        n.Status == (byte)Status.Actived);

                if (ykkh == null)
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

        public static Result GetNguonList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT Nguon FROM TuVanKhachHang WITH(NOLOCK) ORDER BY Nguon", (byte)Status.Actived);
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

        public static Result UpdateKetLuan(string tuVanKhachHangGUID, string ketLuan)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    TuVanKhachHang ykkh = db.TuVanKhachHangs.SingleOrDefault<TuVanKhachHang>(s => s.TuVanKhachHangGUID.ToString() == tuVanKhachHangGUID);
                    if (ykkh != null)
                    {
                        ykkh.NguoiKetLuan = Guid.Parse(Global.UserGUID);
                        ykkh.KetLuan = ketLuan;

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}'",
                            ykkh.TuVanKhachHangGUID.ToString(), ykkh.TenKhachHang, ykkh.SoDienThoai, ykkh.DiaChi, ykkh.YeuCau, ykkh.Nguon, ykkh.Note, ykkh.NguoiKetLuan, ykkh.KetLuan);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Cập nhật kết luận tư vấn khách hàng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
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
