﻿using System;
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

        public static Result GetMinMaxNgayXuatHoaDon(int soHoaDon, ref DateTime minDate, ref DateTime maxDate)
        {
            Result result = new Result();
            minDate = Global.MinDateTime;
            maxDate = Global.MaxDateTime;

            try
            {
                string query = string.Format("SELECT MAX(InvoiceDate) AS MinDate FROM Invoice WHERE Status = 0 AND CAST(InvoiceCode as int) < {0} SELECT MAX(NgayXuatHoaDon) AS MinDate FROM HoaDonThuoc WHERE Status = 0 AND CAST(SoHoaDon as int) < {0} SELECT MAX(NgayXuatHoaDon) AS MinDate FROM HoaDonXuatTruoc WHERE Status = 0 AND CAST(SoHoaDon as int) < {0} SELECT MIN(InvoiceDate) AS MaxDate FROM Invoice WHERE Status = 0 AND CAST(InvoiceCode as int) > {0} SELECT MIN(NgayXuatHoaDon) AS MaxDate FROM HoaDonThuoc WHERE Status = 0 AND CAST(SoHoaDon as int) > {0} SELECT MIN(NgayXuatHoaDon) AS MaxDate FROM HoaDonXuatTruoc WHERE Status = 0 AND CAST(SoHoaDon as int) > {0}", soHoaDon);
                result = ExcuteQueryDataSet(query);
                if (!result.IsOK) return result;

                DataSet ds = result.QueryResult as DataSet;
                object obj = ds.Tables[0].Rows[0]["MinDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) > minDate)
                    minDate = Convert.ToDateTime(obj);

                obj = ds.Tables[1].Rows[0]["MinDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) > minDate)
                    minDate = Convert.ToDateTime(obj);

                obj = ds.Tables[2].Rows[0]["MinDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) > minDate)
                    minDate = Convert.ToDateTime(obj);

                obj = ds.Tables[3].Rows[0]["MaxDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) < maxDate)
                    maxDate = Convert.ToDateTime(obj);

                obj = ds.Tables[4].Rows[0]["MaxDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) < maxDate)
                    maxDate = Convert.ToDateTime(obj);

                obj = ds.Tables[5].Rows[0]["MaxDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) < maxDate)
                    maxDate = Convert.ToDateTime(obj);
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