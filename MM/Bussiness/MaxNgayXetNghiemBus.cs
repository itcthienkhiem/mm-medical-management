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
    public class MaxNgayXetNghiemBus : BusBase 
    {
        public static Result CheckMaxNgayXNExist(string patientGUID, string loaiXN, DateTime maxNgayXN)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                MaxNgayXetNghiem m = db.MaxNgayXetNghiems.FirstOrDefault(p => p.PatientGUID.ToString() == patientGUID &&
                    p.MaxNgayXetNghiem1 == maxNgayXN && p.LoaiXN == loaiXN);

                if (m == null) result.Error.Code = ErrorCode.NOT_EXIST;
                else result.Error.Code = ErrorCode.EXIST;
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

        public static Result InsertMaxNgayXN(string patientGUID, string loaiXN, DateTime maxNgayXN)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    MaxNgayXetNghiem m = new MaxNgayXetNghiem();
                    m.MaxNgayXetNghiemGUID = Guid.NewGuid();
                    m.PatientGUID = Guid.Parse(patientGUID);
                    m.LoaiXN = loaiXN;
                    m.MaxNgayXetNghiem1 = maxNgayXN;
                    db.MaxNgayXetNghiems.InsertOnSubmit(m);
                    db.SubmitChanges();
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
