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
    public class XetNghiem_Hitachi917Bus : BusBase
    {
        public static Result InsertKQXN(List<TestResult_Hitachi917> testResults)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                //db = new MMOverride();
                //string desc = string.Empty;
                //using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                //    foreach (TestResult_Hitachi917 testResult in testResults)
                //    {
                //        foreach (Result_Hitachi917 r in testResult.Results)
                //        {
                //            string idNum = testResult.IDNum;
                //            int testNum = r.TestNum;
                //            string operationID = testResult.OperatorID;
                //            string sex = testResult.Sex.Trim();
                //            DateTime ngayXN = DateTime.ParseExact(string.Format("{0} {1}", testResult.CollectionDate, testResult.CollectionTime),
                //                "MMddyy HHmm", null);

                //            PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.FileNum.ToLower().Trim() == idNum.ToLower().Trim());
                            
                //            string trangThai = string.Empty;

                //            //XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.TestNum == testNum);
                //            //if (xn != null)
                //            //{
                //            //    List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.ToList<ChiTietXetNghiem_Hitachi917>();
                                
                //            //}

                //            //if (sex != string.Empty)
                //            //{

                //            //}

                //            KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.IDNum == idNum &&
                //                k.TestNum == testNum && k.OperationID == operationID && k.NgayXN == ngayXN);

                //            if (kqxn == null) //Add New
                //            {
                //                kqxn = new KetQuaXetNghiem_Hitachi917();
                //                kqxn.KQXN_Hitachi917GUID = Guid.NewGuid();
                //                kqxn.CreatedDate = DateTime.Now;
                //                if (Global.UserGUID != string.Empty) kqxn.CreatedBy = Guid.Parse(Global.UserGUID);
                //                kqxn.IDNum = idNum;
                //                if (patient != null) kqxn.PatientGUID = patient.PatientGUID;
                //                kqxn.NgayXN = ngayXN;
                //                kqxn.OperationID = operationID;
                //                kqxn.TestNum = testNum;
                //                kqxn.TestResult = r.Result;
                //                kqxn.AlarmCode = r.AlarmCode;
                //                kqxn.TrangThai = trangThai;
                //                kqxn.Status = (byte)Status.Actived;
                //                db.KetQuaXetNghiem_Hitachi917s.InsertOnSubmit(kqxn);
                //            }
                //            else //Update
                //            {
                //                kqxn.UpdatedDate = DateTime.Now;
                //                if (Global.UserGUID != string.Empty) kqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                //                kqxn.IDNum = idNum;
                //                if (patient != null) kqxn.PatientGUID = patient.PatientGUID;
                //                kqxn.NgayXN = ngayXN;
                //                kqxn.OperationID = operationID;
                //                kqxn.TestNum = testNum;
                //                kqxn.TestResult = r.Result;
                //                kqxn.AlarmCode = r.AlarmCode;
                //                kqxn.TrangThai = trangThai;
                //                kqxn.Status = (byte)Status.Actived;
                //            }
                //        }
                //    }

                //    db.SubmitChanges();
                //    t.Complete();
                //}

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
