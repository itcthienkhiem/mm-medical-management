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
    public class YKienKhachHangBus : BusBase
    {
        public static Result GetYKienKhachHangList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WITH(NOLOCK) WHERE Status={0} ORDER BY ContactDate DESC", (byte)Status.Actived);
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

        public static Result GetYKienKhachHangList(int type, DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (type == 0)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WITH(NOLOCK) WHERE Status={0} AND ContactDate BETWEEN '{1}' AND '{2}' ORDER BY ContactDate DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else if (type == 1)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WITH(NOLOCK) WHERE Status={0} AND TenKhachHang LIKE N'%{1}%' ORDER BY ContactDate DESC",
                        (byte)Status.Actived, tenBenhNhan);
                }
                else if (type == 2)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WITH(NOLOCK) WHERE Status={0} AND NguoiTao LIKE N'%{1}%' ORDER BY ContactDate DESC",
                        (byte)Status.Actived, tenBenhNhan);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WITH(NOLOCK) WHERE Status={0} AND BacSiPhuTrach LIKE N'%{1}%' ORDER BY ContactDate DESC",
                        (byte)Status.Actived, tenBenhNhan);
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

        public static Result DeleteYKienKhachHang(List<string> keys)
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
                        YKienKhachHang s = db.YKienKhachHangs.SingleOrDefault<YKienKhachHang>(ss => ss.YKienKhachHangGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;
                            desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}', Bác sĩ phụ trách: '{9}', Đã xong: '{10}'\n",
                                s.YKienKhachHangGUID.ToString(), s.TenKhachHang, s.SoDienThoai, s.DiaChi, s.YeuCau, s.Nguon, s.Note, s.NguoiKetLuan, s.KetLuan, s.BacSiPhuTrachGUID, s.DaXong);
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
                    tk.Action = "Xóa ý kiến khách hàng";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
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

        public static Result InsertYKienKhachHang(YKienKhachHang yKienKhachHang)
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
                    if (yKienKhachHang.YKienKhachHangGUID == null || yKienKhachHang.YKienKhachHangGUID == Guid.Empty)
                    {
                        yKienKhachHang.YKienKhachHangGUID = Guid.NewGuid();
                        db.YKienKhachHangs.InsertOnSubmit(yKienKhachHang);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}', Bác sĩ phụ trách: '{9}', Đã xong: '{10}'",
                               yKienKhachHang.YKienKhachHangGUID.ToString(), yKienKhachHang.TenKhachHang, yKienKhachHang.SoDienThoai, yKienKhachHang.DiaChi,
                               yKienKhachHang.YeuCau, yKienKhachHang.Nguon, yKienKhachHang.Note, yKienKhachHang.NguoiKetLuan, yKienKhachHang.KetLuan, yKienKhachHang.BacSiPhuTrachGUID, yKienKhachHang.DaXong);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm ý kiến khách hàng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        YKienKhachHang ykkh = db.YKienKhachHangs.SingleOrDefault<YKienKhachHang>(s => s.YKienKhachHangGUID == yKienKhachHang.YKienKhachHangGUID);
                        if (ykkh != null)
                        {
                            ykkh.PatientGUID = yKienKhachHang.PatientGUID;
                            ykkh.TenKhachHang = yKienKhachHang.TenKhachHang;
                            ykkh.SoDienThoai = yKienKhachHang.SoDienThoai;
                            ykkh.DiaChi = yKienKhachHang.DiaChi;
                            ykkh.YeuCau = yKienKhachHang.YeuCau;
                            ykkh.Nguon = yKienKhachHang.Nguon;
                            ykkh.Note = yKienKhachHang.Note;
                            ykkh.ContactDate = yKienKhachHang.ContactDate;
                            ykkh.ContactBy = yKienKhachHang.ContactBy;
                            ykkh.UpdatedDate = yKienKhachHang.UpdatedDate;
                            ykkh.UpdatedBy = yKienKhachHang.UpdatedBy;
                            ykkh.DeletedDate = yKienKhachHang.DeletedDate;
                            ykkh.DeletedBy = yKienKhachHang.DeletedBy;
                            ykkh.Status = yKienKhachHang.Status;
                            ykkh.NguoiKetLuan = yKienKhachHang.NguoiKetLuan;
                            ykkh.KetLuan = yKienKhachHang.KetLuan;
                            ykkh.BacSiPhuTrachGUID = yKienKhachHang.BacSiPhuTrachGUID;
                            ykkh.DaXong = yKienKhachHang.DaXong;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}', Bác sĩ phụ trách: '{9}', Đã xong: '{10}'",
                               ykkh.YKienKhachHangGUID.ToString(), ykkh.TenKhachHang, ykkh.SoDienThoai, ykkh.DiaChi, ykkh.YeuCau, ykkh.Nguon, ykkh.Note, ykkh.NguoiKetLuan, ykkh.KetLuan, ykkh.BacSiPhuTrachGUID, ykkh.DaXong);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa ý kiến khách hàng";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
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
                var dt = (from y in db.YKienKhachHangs
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

        public static Result CheckKhachHangExist(string tenKhachHang, string yKienKhachHangGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                YKienKhachHang ykkh = null;
                if (yKienKhachHangGUID == null || yKienKhachHangGUID == string.Empty)
                    ykkh = db.YKienKhachHangs.FirstOrDefault<YKienKhachHang>(n => n.TenKhachHang.ToLower() == tenKhachHang.ToLower() &&
                        n.ContactBy.Value.ToString() == Global.UserGUID && n.Status == (byte)Status.Actived);
                else
                    ykkh = db.YKienKhachHangs.FirstOrDefault<YKienKhachHang>(n => n.TenKhachHang.ToLower() == tenKhachHang.ToLower() &&
                        n.ContactBy.Value.ToString() == Global.UserGUID && n.YKienKhachHangGUID.ToString() != yKienKhachHangGUID &&
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
                string query = string.Format("SELECT DISTINCT Nguon FROM YKienKhachHang WITH(NOLOCK) ORDER BY Nguon", (byte)Status.Actived);
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

        public static Result UpdateKetLuan(string yKienKhachHangGUID, string ketLuan)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    
                    YKienKhachHang ykkh = db.YKienKhachHangs.SingleOrDefault<YKienKhachHang>(s => s.YKienKhachHangGUID.ToString() == yKienKhachHangGUID);
                    if (ykkh != null)
                    {
                        ykkh.NguoiKetLuan = Guid.Parse(Global.UserGUID);
                        ykkh.KetLuan = ketLuan;

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}', Người kết luận: '{7}', Kết luận: '{8}'",
                            ykkh.YKienKhachHangGUID.ToString(), ykkh.TenKhachHang, ykkh.SoDienThoai, ykkh.DiaChi, ykkh.YeuCau, ykkh.Nguon, ykkh.Note, ykkh.NguoiKetLuan, ykkh.KetLuan);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Cập nhật kết luận ý kiến khách hàng";
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
