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
    public class SMSLogBus : BusBase
    {
        public static Result GetSMSLogList(DateTime tuNgay, DateTime denNgay, int type)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (type == 0) //All
                    query = string.Format("SELECT * FROM SMSLogView WITH(NOLOCK) WHERE Ngay BETWEEN '{0}' AND '{1}' ORDER BY Ngay",
                        tuNgay.ToString("yyyy-MM-dd HH:mm:ss"), denNgay.ToString("yyyy-MM-dd HH:mm:ss"));
                else if (type == 1) //OK
                    query = string.Format("SELECT * FROM SMSLogView WITH(NOLOCK) WHERE Ngay BETWEEN '{0}' AND '{1}' AND Status = 0 ORDER BY Ngay",
                        tuNgay.ToString("yyyy-MM-dd HH:mm:ss"), denNgay.ToString("yyyy-MM-dd HH:mm:ss"));
                else if (type == 2) //Fail
                    query = string.Format("SELECT * FROM SMSLogView WITH(NOLOCK) WHERE Ngay BETWEEN '{0}' AND '{1}' AND Status <> 0 ORDER BY Ngay",
                        tuNgay.ToString("yyyy-MM-dd HH:mm:ss"), denNgay.ToString("yyyy-MM-dd HH:mm:ss"));

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

        public static Result InsertSMSLog(SMSLog smsLog)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    smsLog.SMSLogGUID = Guid.NewGuid();
                    db.SMSLogs.InsertOnSubmit(smsLog);
                    db.SubmitChanges();
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
