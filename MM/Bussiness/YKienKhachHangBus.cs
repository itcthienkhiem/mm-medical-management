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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WHERE Status={0} ORDER BY ContactDate DESC", (byte)Status.Actived);
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

        public static Result GetYKienKhachHangList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WHERE Status={0} AND ContactDate BETWEEN '{1}' AND '{2}' ORDER BY ContactDate DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM YKienKhachHangView WHERE Status={0} AND TenKhachHang LIKE N'%{1}%' ORDER BY ContactDate DESC", 
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

                            desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}'\n",
                                s.YKienKhachHangGUID.ToString(), s.TenKhachHang, s.SoDienThoai, s.DiaChi, s.YeuCau, s.Nguon, s.Note);
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
                        desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}'",
                               yKienKhachHang.YKienKhachHangGUID.ToString(), yKienKhachHang.TenKhachHang, yKienKhachHang.SoDienThoai, yKienKhachHang.DiaChi,
                               yKienKhachHang.YeuCau, yKienKhachHang.Nguon, yKienKhachHang.Note);

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

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên khách hàng: '{1}', Số điện thoại: '{2}', Địa chỉ: '{3}', Yêu cầu: '{4}', Nguồn: '{5}', Ghi chú: '{6}'",
                               ykkh.YKienKhachHangGUID.ToString(), ykkh.TenKhachHang, ykkh.SoDienThoai, ykkh.DiaChi, ykkh.YeuCau, ykkh.Nguon, ykkh.Note);

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
    }
}
