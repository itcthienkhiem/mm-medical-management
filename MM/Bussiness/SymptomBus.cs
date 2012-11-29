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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Symptom WITH(NOLOCK) WHERE Status={0} ORDER BY SymptomName", (byte)Status.Actived);
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

        public static Result GetSymptomList(string symtomName, int type) //0: Name; 1: Code
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (symtomName.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Symptom WITH(NOLOCK) WHERE Status={0} ORDER BY SymptomName",
                        (byte)Status.Actived);
                }
                else if (type == 0)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Symptom WITH(NOLOCK) WHERE SymptomName LIKE N'{0}%' AND Status={1} ORDER BY SymptomName",
                        symtomName, (byte)Status.Actived);
                }
                else if (type == 1)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Symptom WITH(NOLOCK) WHERE Code LIKE N'{0}%' AND Status={1} ORDER BY SymptomName",
                        symtomName, (byte)Status.Actived);
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

        public static Result GetSymptomCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Symptom WITH(NOLOCK)";
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

        public static Result DeleteSymptom(List<string> keys)
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
                        Symptom s = db.Symptoms.SingleOrDefault<Symptom>(ss => ss.SymptomGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã triệu chứng: '{1}', Tên triệu chứng: '{2}', Lời khuyên: '{3}'\n",
                                s.SymptomGUID.ToString(), s.Code, s.SymptomName, s.Advice);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin triệu chứng";
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
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (symp.SymptomGUID == null || symp.SymptomGUID == Guid.Empty)
                    {
                        symp.SymptomGUID = Guid.NewGuid();
                        db.Symptoms.InsertOnSubmit(symp);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Mã triệu chứng: '{1}', Tên triệu chứng: '{2}', Lời khuyên: '{3}'",
                               symp.SymptomGUID.ToString(), symp.Code, symp.SymptomName, symp.Advice);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin triệu chứng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
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

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Mã triệu chứng: '{1}', Tên triệu chứng: '{2}', Lời khuyên: '{3}'",
                                   symptom.SymptomGUID.ToString(), symptom.Code, symptom.SymptomName, symptom.Advice);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin triệu chứng";
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
