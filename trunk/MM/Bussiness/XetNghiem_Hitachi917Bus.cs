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
        public static Result GetKetQuaXetNghiemList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_Hitachi917View WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} ORDER BY NgayXN DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived);
                }
                else
                {
                    if (tenBenhNhan.Trim() == string.Empty)
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_Hitachi917View WHERE Status={0} ORDER BY NgayXN DESC",
                            (byte)Status.Actived, tenBenhNhan);
                    else
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_Hitachi917View WHERE Status={0} AND FullName LIKE N'%{1}%' ORDER BY NgayXN DESC",
                            (byte)Status.Actived, tenBenhNhan);
                }

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

        public static Result GetChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID)
        {
            Result result = new Result();

            try
            {
                string query = query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietKetQuaXetNghiem_Hitachi917View WHERE KQXN_Hitachi917GUID = '{0}' AND Status = {1} ORDER BY TestNum",
                           ketQuaXetNghiemGUID, (byte)Status.Actived);

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

        public static Result InsertKQXN(List<TestResult_Hitachi917> testResults)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (TestResult_Hitachi917 testResult in testResults)
                    {
                        string idNum = testResult.IDNum;
                        string operationID = testResult.OperatorID;
                        string sex = testResult.Sex.Trim();
                        DateTime ngayXN = DateTime.ParseExact(string.Format("{0} {1}", testResult.CollectionDate, testResult.CollectionTime),
                            "MMddyy HHmm", null);

                        PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.FileNum.ToLower().Trim() == idNum.ToLower().Trim());

                        KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.IDNum == idNum &&
                                k.OperationID == operationID && k.NgayXN == ngayXN);

                        if (kqxn == null) //Add New
                        {
                            kqxn = new KetQuaXetNghiem_Hitachi917();
                            kqxn.KQXN_Hitachi917GUID = Guid.NewGuid();
                            kqxn.CreatedDate = DateTime.Now;
                            if (Global.UserGUID != string.Empty) kqxn.CreatedBy = Guid.Parse(Global.UserGUID);
                            kqxn.IDNum = idNum;
                            if (patient != null) kqxn.PatientGUID = patient.PatientGUID;
                            kqxn.NgayXN = ngayXN;
                            kqxn.OperationID = operationID;
                            kqxn.Status = (byte)Status.Actived;
                            db.KetQuaXetNghiem_Hitachi917s.InsertOnSubmit(kqxn);
                        }
                        else //Update
                        {
                            kqxn.UpdatedDate = DateTime.Now;
                            if (Global.UserGUID != string.Empty) kqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                            kqxn.IDNum = idNum;
                            if (patient != null) kqxn.PatientGUID = patient.PatientGUID;
                            kqxn.NgayXN = ngayXN;
                            kqxn.OperationID = operationID;
                            kqxn.Status = (byte)Status.Actived;
                        }

                        db.SubmitChanges();

                        foreach (Result_Hitachi917 r in testResult.Results)
                        {
                            int testNum = r.TestNum;
                            byte tinhTrang = 0;

                            //XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.TestNum == testNum);
                            //if (xn != null)
                            //{
                            //    List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.ToList<ChiTietXetNghiem_Hitachi917>();

                            //}

                            //if (sex != string.Empty)
                            //{

                            //}

                            ChiTietKetQuaXetNghiem_Hitachi917 ctkqxn = db.ChiTietKetQuaXetNghiem_Hitachi917s.SingleOrDefault<ChiTietKetQuaXetNghiem_Hitachi917>(c => c.KQXN_Hitachi917GUID == kqxn.KQXN_Hitachi917GUID &&
                                c.TestNum == testNum);

                            if (ctkqxn == null) //Add New
                            {
                                ctkqxn = new ChiTietKetQuaXetNghiem_Hitachi917();
                                ctkqxn.ChiTietKQXN_Hitachi917GUID = Guid.NewGuid();
                                ctkqxn.KQXN_Hitachi917GUID = kqxn.KQXN_Hitachi917GUID;
                                ctkqxn.CreatedDate = DateTime.Now;
                                if (Global.UserGUID != string.Empty) ctkqxn.CreatedBy = Guid.Parse(Global.UserGUID);
                                ctkqxn.TestNum = testNum;
                                ctkqxn.TestResult = r.Result;
                                ctkqxn.AlarmCode = r.AlarmCode;
                                ctkqxn.TinhTrang = tinhTrang;
                                ctkqxn.Status = (byte)Status.Actived;
                                db.ChiTietKetQuaXetNghiem_Hitachi917s.InsertOnSubmit(ctkqxn);
                            }
                            else //Update
                            {
                                ctkqxn.UpdatedDate = DateTime.Now;
                                if (Global.UserGUID != string.Empty) ctkqxn.UpdatedBy = Guid.Parse(Global.UserGUID);

                                ctkqxn.TestNum = testNum;
                                ctkqxn.TestResult = r.Result;
                                ctkqxn.AlarmCode = r.AlarmCode;
                                ctkqxn.TinhTrang = tinhTrang;
                                ctkqxn.Status = (byte)Status.Actived;
                            }
                        }
                    }

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

        public static Result UpdatePatient(KetQuaXetNghiem_Hitachi917 ketQuaXetNghiem)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID == ketQuaXetNghiem.KQXN_Hitachi917GUID &&
                        k.Status == (byte)Status.Actived);

                    if (kqxn != null)
                    {
                        kqxn.UpdatedDate = DateTime.Now;
                        kqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                        kqxn.PatientGUID = ketQuaXetNghiem.PatientGUID;

                        string tenBenhNhan = string.Empty;
                        string fileNum = string.Empty;
                        if (ketQuaXetNghiem.PatientGUID != null)
                        {
                            PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.PatientGUID == ketQuaXetNghiem.PatientGUID);
                            if (patient != null)
                            {
                                fileNum = patient.FileNum;
                                tenBenhNhan = patient.FullName;
                            }
                        }

                        desc += string.Format("GUID: '{0}', IDNum: '{1}', Mã bệnh nhân: '{2}', Tên bệnh nhân: '{3}', Ngày xét nghiệm: '{4}', OperationID: '{5}'",
                                kqxn.KQXN_Hitachi917GUID.ToString(), kqxn.IDNum, fileNum, tenBenhNhan, kqxn.NgayXN.ToString("dd/MM/yyyy HH:mm:ss"), kqxn.OperationID);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Delete;
                        tk.Action = "Cập nhật bệnh nhân xét nghiệm hitachi 917";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);
                    }

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

        public static Result DeleteXetNghiem(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID.ToString() == key);

                        if (kqxn != null)
                        {
                            kqxn.DeletedDate = DateTime.Now;
                            kqxn.DeletedBy = Guid.Parse(Global.UserGUID);
                            kqxn.Status = (byte)Status.Deactived;
                            
                            string tenBenhNhan = string.Empty;
                            string fileNum = string.Empty;
                            if (kqxn.PatientGUID != null)
                            {
                                PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.PatientGUID == kqxn.PatientGUID);
                                if (patient != null)
                                {
                                    fileNum = patient.FileNum;
                                    tenBenhNhan = patient.FullName;
                                }
                            }

                            desc += string.Format("- GUID: '{0}', IDNum: '{1}', Mã bệnh nhân: '{2}', Tên bệnh nhân: '{3}', Ngày xét nghiệm: '{4}', OperationID: '{5}'\n",
                                    kqxn.KQXN_Hitachi917GUID.ToString(), kqxn.IDNum, fileNum, tenBenhNhan, kqxn.NgayXN.ToString("dd/MM/yyyy HH:mm:ss"), kqxn.OperationID);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa xét nghiệm hitachi 917";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
                    db.Trackings.InsertOnSubmit(tk);

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
