using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class GhiNhanTraNoBus : BusBase
    {
        public static Result GetGhiNhanTraNoList(string phieuThuGUID, LoaiPT loaiPT)
        {
            Result result = new Result();

            try
            {
                string query = string.Format(@"SELECT CAST(0 AS Bit) AS Checked, * FROM GhiNhanTraNoView WITH(NOLOCK) 
                                                WHERE MaPhieuThuGUID = '{0}' AND LoaiPT = {1} AND [Status] = 0 
                                                ORDER BY NgayTra DESC", phieuThuGUID, (int)loaiPT);
                

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
    }
}
