using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class DocStaffBus : BusBase
    {
        public static Result GetUserList()
        {
            Result result = null;
            
            try
            {
                string query = "SELECT ContactGUID, Username FROM DocStaff WHERE AvailableToWork = 'True' ORDER BY Username";
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
    }
}
