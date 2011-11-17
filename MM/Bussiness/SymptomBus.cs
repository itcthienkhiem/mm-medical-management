using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class SymptomBus : BusBase
    {
        public static Result GetSymptomList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Symptom WHERE Status={0} ORDER BY Code", (byte)Status.Actived);
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

        public static Result DeleteSymptom(List<string> keys)
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
                        Symptom s = db.Symptoms.SingleOrDefault<Symptom>(ss => ss.SymptomGUID.ToString() == key);
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

        public static Result CheckSymptomExistCode(string sympGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Symptom symp = null;
                if (sympGUID == null || sympGUID == string.Empty)
                    symp = db.Symptoms.SingleOrDefault<Symptom>(s => s.Code.ToLower() == code.ToLower());
                else
                    symp = db.Symptoms.SingleOrDefault<Symptom>(s => s.Code.ToLower() == code.ToLower() &&
                                                                s.SymptomGUID.ToString() != sympGUID);

                if (symp == null)
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

        public static Result InsertSymptom(Symptom symp)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                //Insert
                if (symp.SymptomGUID == null || symp.SymptomGUID == Guid.Empty)
                {
                    symp.SymptomGUID = Guid.NewGuid();
                    db.Symptoms.InsertOnSubmit(symp);
                }
                else //Update
                {
                    Symptom symptom = db.Symptoms.SingleOrDefault<Symptom>(s => s.SymptomGUID.ToString() == symp.SymptomGUID.ToString());
                    if (symptom != null)
                    {
                        symptom.Code = symp.Code;
                        symptom.SymptomName = symp.SymptomName;
                        symptom.Advice = symp.Advice;
                        symptom.CreatedDate = symp.CreatedDate;
                        symptom.CreatedBy = symp.CreatedBy;
                        symptom.UpdatedDate = symp.UpdatedDate;
                        symptom.UpdatedBy = symp.UpdatedBy;
                        symptom.DeletedDate = symp.DeletedDate;
                        symptom.DeletedBy = symp.DeletedBy;
                        symptom.Status = symp.Status;
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
