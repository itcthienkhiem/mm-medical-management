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
    public class UserBus : BusBase
    {
        public static Result GetAccountList(DateTime fromDate, DateTime toDate, string tenBenhNhan, bool isMaBenhNhan)
        {
            Result result = new Result();
            DataTable dt = null;

            try
            {
                string query = string.Empty;

                //Hitachi917
                if (!isMaBenhNhan)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM AccountView WHERE Archived = 'False' AND FullName LIKE N'%{0}%' AND CreatedDate BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM AccountView WHERE Archived = 'False' AND CustomerId LIKE N'%{0}%' AND CreatedDate BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }

                result = ExcuteQuery(query);
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

        public static Result AddUser(string customerId, string password)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    User user = db.Users.FirstOrDefault(u => u.CustomerId == customerId);
                    if (user == null)
                    {
                        user = new User();
                        user.CustomerId = customerId;
                        user.Password = password;
                        user.CreatedDate = DateTime.Now;
                        db.Users.InsertOnSubmit(user);
                        db.SubmitChanges();
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
