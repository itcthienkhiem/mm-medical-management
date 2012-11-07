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
    public class GiaCapCuuBus : BusBase
    {
        public static Result GetGiaCapCuuList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaCapCuuView WITH(NOLOCK) WHERE GiaCapCuuStatus={0} AND KhoCapCuuStatus={0} ORDER BY TenCapCuu ASC, NgayApDung DESC",
                    (byte)Status.Actived);
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

        public static Result GetGiaCapCuuMoiNhat(string khoCapCuuGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TOP 1 * FROM GiaCapCuu WITH(NOLOCK) WHERE KhoCapCuuGUID = '{0}' AND Status = {1} AND NgayApDung <= '{2}' ORDER BY NgayApDung DESC",
                    khoCapCuuGUID, (byte)Status.Actived, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DateTime ngayApDung =  Convert.ToDateTime(dt.Rows[0]["NgayApDung"]);

                        query = string.Format("SELECT * FROM GiaCapCuu WITH(NOLOCK) WHERE KhoCapCuuGUID = '{0}' AND Status = {1} AND NgayApDung BETWEEN '{2}' AND '{3}' ORDER BY NgayApDung DESC",
                                khoCapCuuGUID, (byte)Status.Actived, ngayApDung.ToString("yyyy-MM-dd 00:00:00"), ngayApDung.ToString("yyyy-MM-dd 23:59:59"));
                        return ExcuteQuery(query);
                    }
                    else return result;
                }
                else return result;
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

        public static Result DeleteGiaCapCuu(List<string> keys)
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
                        GiaCapCuu giaCapCuu = db.GiaCapCuus.SingleOrDefault<GiaCapCuu>(g => g.GiaCapCuuGUID.ToString() == key);
                        if (giaCapCuu != null)
                        {
                            giaCapCuu.DeletedDate = DateTime.Now;
                            giaCapCuu.DeletedBy = Guid.Parse(Global.UserGUID);
                            giaCapCuu.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Cấp cứu: '{1}', Giá bán: '{2}', Ngày áp dụng: '{3}'\n",
                                giaCapCuu.GiaCapCuuGUID.ToString(), giaCapCuu.KhoCapCuu.TenCapCuu, giaCapCuu.GiaBan, giaCapCuu.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin giá cấp cứu";
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

        public static Result InsertGiaCapCuu(GiaCapCuu giaCapCuu)
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
                    if (giaCapCuu.GiaCapCuuGUID == null || giaCapCuu.GiaCapCuuGUID == Guid.Empty)
                    {
                        giaCapCuu.GiaCapCuuGUID = Guid.NewGuid();
                        db.GiaCapCuus.InsertOnSubmit(giaCapCuu);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Cấp cứu: '{1}', Giá bán: '{2}', Ngày áp dụng: '{3}'",
                                giaCapCuu.GiaCapCuuGUID.ToString(), giaCapCuu.KhoCapCuu.TenCapCuu, giaCapCuu.GiaBan, giaCapCuu.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin giá cấp cứu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        GiaCapCuu gt = db.GiaCapCuus.SingleOrDefault<GiaCapCuu>(g => g.GiaCapCuuGUID == giaCapCuu.GiaCapCuuGUID);
                        if (gt != null)
                        {
                            double giaCu = gt.GiaBan;

                            gt.KhoCapCuuGUID = giaCapCuu.KhoCapCuuGUID;
                            gt.GiaBan = giaCapCuu.GiaBan;
                            gt.NgayApDung = giaCapCuu.NgayApDung;
                            gt.Note = giaCapCuu.Note;
                            gt.CreatedDate = giaCapCuu.CreatedDate;
                            gt.CreatedBy = giaCapCuu.CreatedBy;
                            gt.UpdatedDate = giaCapCuu.UpdatedDate;
                            gt.UpdatedBy = giaCapCuu.UpdatedBy;
                            gt.DeletedDate = giaCapCuu.DeletedDate;
                            gt.DeletedBy = giaCapCuu.DeletedBy;
                            gt.Status = giaCapCuu.Status;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Cấp cứu: '{1}', Giá bán: cũ: '{2}' - mới: '{3}', Ngày áp dụng: '{4}'",
                                    gt.GiaCapCuuGUID.ToString(), gt.KhoCapCuu.TenCapCuu, giaCu, gt.GiaBan, gt.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin giá cấp cứu";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
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
