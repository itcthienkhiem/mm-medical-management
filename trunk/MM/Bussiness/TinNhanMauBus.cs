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
    public class TinNhanMauBus : BusBase
    {
        public static Result GetTinNhanMauList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TinNhanMau WITH(NOLOCK) WHERE Status={0} ORDER BY CreatedDate", (byte)Status.Actived);
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

        public static Result DeleteTinNhanMau(List<string> keys)
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
                        TinNhanMau tnm = db.TinNhanMaus.SingleOrDefault<TinNhanMau>(ss => ss.TinNhanMauGUID.ToString() == key);
                        if (tnm != null)
                        {
                            tnm.DeletedDate = DateTime.Now;
                            tnm.DeletedBy = Guid.Parse(Global.UserGUID);
                            tnm.Status = (byte)Status.Deactived;
                            
                            desc += string.Format("- GUID: '{0}', Nội dung: '{1}'\n", tnm.TinNhanMauGUID.ToString(), tnm.NoiDung);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa tin nhắn mẫu";
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

        public static Result InsertTinNhanMau(TinNhanMau tinNhanMau)
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
                    if (tinNhanMau.TinNhanMauGUID == null || tinNhanMau.TinNhanMauGUID == Guid.Empty)
                    {
                        tinNhanMau.TinNhanMauGUID = Guid.NewGuid();
                        db.TinNhanMaus.InsertOnSubmit(tinNhanMau);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Nội dung: '{1}'", tinNhanMau.TinNhanMauGUID.ToString(), tinNhanMau.NoiDung);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm tin nhắn mẫu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        TinNhanMau tnm = db.TinNhanMaus.SingleOrDefault<TinNhanMau>(s => s.TinNhanMauGUID == tinNhanMau.TinNhanMauGUID);
                        if (tnm != null)
                        {
                            tnm.NoiDung = tinNhanMau.NoiDung;
                            tnm.CreatedDate = tinNhanMau.CreatedDate;
                            tnm.CreatedBy = tinNhanMau.CreatedBy;
                            tnm.UpdatedDate = tinNhanMau.UpdatedDate;
                            tnm.UpdatedBy = tinNhanMau.UpdatedBy;
                            tnm.DeletedDate = tinNhanMau.DeletedDate;
                            tnm.DeletedBy = tinNhanMau.DeletedBy;
                            tnm.Status = tinNhanMau.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Nội dung: '{1}'", tnm.TinNhanMauGUID.ToString(), tnm.NoiDung);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa tin nhắn mẫu";
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
