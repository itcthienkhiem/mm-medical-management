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
    public class ServicesBus : BusBase
    {
        public static Result GetServicesList()
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                result.QueryResult = db.Services.ToList<Service>();
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

        public static Result DeleteServices(List<Service> services)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Service d;
                    db.Services.DeleteAllOnSubmit(services);
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

        public static Result CheckServicesExistCode(string srvGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Service srv = null;
                if (srvGUID == null || srvGUID == string.Empty)
                    srv = db.Services.SingleOrDefault<Service>(s => s.Code.ToLower() == code.ToLower());
                else
                    srv = db.Services.SingleOrDefault<Service>(s => s.Code.ToLower() == code.ToLower() && 
                                                                s.ServiceGUID.ToString() == srvGUID);

                if (srv == null)
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

        public static Result InsertService(Service service)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                //Insert
                if (service.ServiceGUID == null || service.ServiceGUID.ToString() == string.Empty)
                {
                    db.Services.InsertOnSubmit(service);
                }
                else //Update
                {
                    Service srv = db.Services.SingleOrDefault<Service>(s => s.ServiceGUID.ToString() == service.ServiceGUID.ToString());
                    if (srv != null)
                    {
                        srv.Code = service.Code;
                        srv.Name = service.Name;
                        srv.Price = service.Price;
                        srv.Description = service.Description;
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
