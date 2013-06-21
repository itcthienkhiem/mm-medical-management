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
                adapter.SelectCommand.CommandTimeout = 0;
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

        protected static Result ExcuteQueryDataSet(string query)
        {
            Result result = new Result();
            MMOverride db = null;
            SqlDataAdapter adapter = null;

            try
            {
                db = new MMOverride();
                adapter = new SqlDataAdapter(query, (SqlConnection)db.Connection);
                adapter.SelectCommand.CommandTimeout = 0;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                result.QueryResult = ds;
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

        protected static Result ExcuteQueryDataSet(string spName, List<SqlParameter> sqlParams)
        {
            Result result = new Result();
            MMOverride db = null;
            SqlDataAdapter adapter = null;
            SqlCommand cmd = null;

            try
            {
                db = new MMOverride();
                cmd = new SqlCommand();
                cmd.Connection = (SqlConnection)db.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.CommandText = spName;

                foreach (SqlParameter param in sqlParams)
                {
                    cmd.Parameters.Add(param);
                }

                adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                result.QueryResult = ds;
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
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

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

        protected static Result ExcuteQuery(string spName, List<SqlParameter> sqlParams)
        {
            Result result = new Result();
            MMOverride db = null;
            SqlDataAdapter adapter = null;
            SqlCommand cmd = null;

            try
            {
                db = new MMOverride();
                cmd = new SqlCommand();
                cmd.Connection = (SqlConnection)db.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.CommandText = spName;

                foreach (SqlParameter param in sqlParams)
                {
                    cmd.Parameters.Add(param);
                }

                adapter = new SqlDataAdapter(cmd);
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
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

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

        protected static Result ExcuteNonQuery(string spName, List<SqlParameter> sqlParams)
        {
            Result result = new Result();
            MMOverride db = null;
            SqlCommand cmd = null;

            try
            {
                db = new MMOverride();
                cmd = new SqlCommand();
                cmd.Connection = (SqlConnection)db.Connection;
                if (cmd.Connection.State == ConnectionState.Closed ||
                    cmd.Connection.State == ConnectionState.Broken)
                    cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.CommandText = spName;

                foreach (SqlParameter param in sqlParams)
                {
                    cmd.Parameters.Add(param);
                }

                cmd.ExecuteNonQuery();
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
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        protected static void OverrideTransactionScopeMaximumTimeout(TimeSpan timeOut)
        {
            // 1. create a object of the type specified by the fully qualified name
            Type oSystemType = typeof(global::System.Transactions.TransactionManager);
            System.Reflection.FieldInfo oCachedMaxTimeout = oSystemType.GetField("_cachedMaxTimeout", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Reflection.FieldInfo oMaximumTimeout = oSystemType.GetField("_maximumTimeout", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            oCachedMaxTimeout.SetValue(null, true);
            oMaximumTimeout.SetValue(null, timeOut);
        }
    }
}
