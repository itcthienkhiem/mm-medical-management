using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class DocStaffBus
    {
        public static Result GetUserList()
        {
            Result result = new Result();
            MMOverride db = null;
            
            try
            {
                
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
