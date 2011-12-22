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
    public class GiaThuocBus : BusBase
    {
        public static Result GetGiaThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaThuocView WHERE GiaThuocStatus={0} AND ThuocStatus={0} ORDER BY TenThuoc ASC, NgayApDung DESC",
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

        public static Result GetGiaThuocMoiNhat(string thuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TOP 1 * FROM GiaThuoc WHERE ThuocGUID = '{0}' AND Status = {1} ORDER BY NgayApDung DESC",
                    thuocGUID, (byte)Status.Actived);
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

        public static Result DeleteGiaThuoc(List<string> giaThuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in giaThuocKeys)
                    {
                        GiaThuoc giaThuoc = db.GiaThuocs.SingleOrDefault<GiaThuoc>(g => g.GiaThuocGUID.ToString() == key);
                        if (giaThuoc != null)
                        {
                            giaThuoc.DeletedDate = DateTime.Now;
                            giaThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            giaThuoc.Status = (byte)Status.Deactived;
                        }
                    }

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

        public static Result InsertGiaThuoc(GiaThuoc giaThuoc)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (giaThuoc.GiaThuocGUID == null || giaThuoc.GiaThuocGUID == Guid.Empty)
                    {
                        giaThuoc.GiaThuocGUID = Guid.NewGuid();
                        db.GiaThuocs.InsertOnSubmit(giaThuoc);
                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        GiaThuoc gt = db.GiaThuocs.SingleOrDefault<GiaThuoc>(g => g.GiaThuocGUID.ToString() == giaThuoc.GiaThuocGUID.ToString());
                        if (gt != null)
                        {
                            gt.ThuocGUID = giaThuoc.ThuocGUID;
                            gt.GiaBan = giaThuoc.GiaBan;
                            gt.NgayApDung = giaThuoc.NgayApDung;
                            gt.Note = giaThuoc.Note;
                            gt.CreatedDate = giaThuoc.CreatedDate;
                            gt.CreatedBy = giaThuoc.CreatedBy;
                            gt.UpdatedDate = giaThuoc.UpdatedDate;
                            gt.UpdatedBy = giaThuoc.UpdatedBy;
                            gt.DeletedDate = giaThuoc.DeletedDate;
                            gt.DeletedBy = giaThuoc.DeletedBy;
                            gt.Status = giaThuoc.Status;
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
