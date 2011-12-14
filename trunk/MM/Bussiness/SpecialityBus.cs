using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class SpecialityBus : BusBase
    {
        public static Result GetSpecialityList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Speciality WHERE Status={0} ORDER BY Name", (byte)Status.Actived);
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

        public static Result GetSpecialityCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Speciality";
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

        public static Result DeleteSpeciality(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        Speciality s = db.Specialities.SingleOrDefault<Speciality>(ss => ss.SpecialityGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;
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

        public static Result CheckSpecialityExistCode(string specGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Speciality spec = null;
                if (specGUID == null || specGUID == string.Empty)
                    spec = db.Specialities.SingleOrDefault<Speciality>(s => s.Code.ToLower() == code.ToLower());
                else
                    spec = db.Specialities.SingleOrDefault<Speciality>(s => s.Code.ToLower() == code.ToLower() &&
                                                                s.SpecialityGUID.ToString() != specGUID);

                if (spec == null)
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

        public static Result InsertSpeciality(Speciality spec)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                //Insert
                if (spec.SpecialityGUID == null || spec.SpecialityGUID == Guid.Empty)
                {
                    spec.SpecialityGUID = Guid.NewGuid();
                    db.Specialities.InsertOnSubmit(spec);
                }
                else //Update
                {
                    Speciality speciality = db.Specialities.SingleOrDefault<Speciality>(s => s.SpecialityGUID.ToString() == spec.SpecialityGUID.ToString());
                    if (speciality != null)
                    {
                        speciality.Code = spec.Code;
                        speciality.Name = spec.Name;
                        speciality.Description = spec.Description;
                        speciality.CreatedDate = spec.CreatedDate;
                        speciality.CreatedBy = spec.CreatedBy;
                        speciality.UpdatedDate = spec.UpdatedDate;
                        speciality.UpdatedBy = spec.UpdatedBy;
                        speciality.DeletedDate = spec.DeletedDate;
                        speciality.DeletedBy = spec.DeletedBy;
                        speciality.Status = spec.Status;
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
