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
    public class ThuocBus : BusBase
    {
        public static Result GetThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Thuoc WHERE Status={0} ORDER BY TenThuoc", (byte)Status.Actived);
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

        public static Result GetThuocListNotInNhomThuoc(string nhomThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Thuoc WHERE Status={0} AND ThuocGUID NOT IN (SELECT ThuocGUID FROM NhomThuoc_Thuoc WHERE NhomThuocGUID = '{1}' AND Status={0}) ORDER BY TenThuoc", 
                    (byte)Status.Actived, nhomThuocGUID);
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

        public static Result GetThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Thuoc";
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

        public static Result DeleteThuoc(List<string> thuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in thuocKeys)
                    {
                        Thuoc thuoc = db.Thuocs.SingleOrDefault<Thuoc>(th => th.ThuocGUID.ToString() == key);
                        if (thuoc != null)
                        {
                            thuoc.DeletedDate = DateTime.Now;
                            thuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            thuoc.Status = (byte)Status.Deactived;
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

        public static Result CheckThuocExistCode(string thuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Thuoc th = null;
                if (thuocGUID == null || thuocGUID == string.Empty)
                    th = db.Thuocs.SingleOrDefault<Thuoc>(t => t.MaThuoc.ToLower() == code.ToLower());
                else
                    th = db.Thuocs.SingleOrDefault<Thuoc>(t => t.MaThuoc.ToLower() == code.ToLower() &&
                                                                t.ThuocGUID.ToString() != thuocGUID);

                if (th == null)
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

        public static Result InsertThuoc(Thuoc thuoc)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                //Insert
                if (thuoc.ThuocGUID == null || thuoc.ThuocGUID == Guid.Empty)
                {
                    thuoc.ThuocGUID = Guid.NewGuid();
                    db.Thuocs.InsertOnSubmit(thuoc);
                }
                else //Update
                {
                    Thuoc th = db.Thuocs.SingleOrDefault<Thuoc>(t => t.ThuocGUID.ToString() == thuoc.ThuocGUID.ToString());
                    if (th != null)
                    {
                        th.MaThuoc = thuoc.MaThuoc;
                        th.TenThuoc = thuoc.TenThuoc;
                        th.BietDuoc = thuoc.BietDuoc;
                        th.HamLuong = thuoc.HamLuong;
                        th.HoatChat = thuoc.HoatChat;
                        th.Note = thuoc.Note;
                        th.CreatedDate = thuoc.CreatedDate;
                        th.CreatedBy = thuoc.CreatedBy;
                        th.UpdatedDate = thuoc.UpdatedDate;
                        th.UpdatedBy = thuoc.UpdatedBy;
                        th.DeletedDate = thuoc.DeletedDate;
                        th.DeletedBy = thuoc.DeletedBy;
                        th.Status = thuoc.Status;
                    }
                }

                db.SubmitChanges();
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
