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
    public class QuanLySoHoaDonBus : BusBase
    {
        public static Result GetSoHoaDon()
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                string query = "SELECT Min(SoHoaDon) as SoHoaDon FROM QuanLySoHoaDon WHERE XuatTruoc = 'False' AND DaXuat = 'False'";
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;
                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                    result.QueryResult = dt.Rows[0][0];
                else
                {
                    query = "SELECT MAX(SoHoaDon) as SoHoaDon FROM QuanLySoHoaDon";
                    result = ExcuteQuery(query);
                    if (!result.IsOK) return result;

                    dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                        result.QueryResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                    else
                        result.QueryResult = 1;
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

        public static Result GetSoHoaDonChuaXuat(int count)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                string query = string.Format("SELECT TOP {0} CAST(0 AS Bit) AS Checked, * FROM QuanLySoHoaDon WHERE XuatTruoc = 'False' AND DaXuat = 'False' ORDER BY SoHoaDon", count);
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

        public static Result GetMaxSoHoaDon()
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                string query = "SELECT MAX(SoHoaDon) as SoHoaDon FROM QuanLySoHoaDon";
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                    result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                else
                    result.QueryResult = 0;
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
