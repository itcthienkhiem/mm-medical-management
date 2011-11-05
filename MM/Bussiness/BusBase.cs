using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;
using System.Data.SqlClient;
using MM.Common;
using MM.Databasae;


namespace MM.Bussiness
{
    public class BusBase
    {
        protected static Result ExcuteQuery(string query) 
        {
            Result result = new Result();
            MMOverride db = null;
            SqlDataAdapter adapter = null;

            try
            {
                db = new MMOverride();
                adapter = new SqlDataAdapter(query, (SqlConnection)db.Connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                result.QueryResult = dt;
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
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }

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
