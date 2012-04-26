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
                string query = query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST('' AS nvarchar(50)) AS BinhThuong FROM ChiTietKetQuaXetNghiem_Hitachi917View WHERE KQXN_Hitachi917GUID = '{0}' AND Status = {1} ORDER BY TestNum",
                           ketQuaXetNghiemGUID, (byte)Status.Actived);

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                MMOverride db = new MMOverride();
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    int testNum = Convert.ToInt32(row["TestNum"]);
                    
                    XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.TestNum == testNum);
                    if (xn == null) continue;
                    List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.ToList<ChiTietXetNghiem_Hitachi917>();
                    if (ctxns.Count <= 0) continue;

                    double testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                    ChiTietXetNghiem_Hitachi917 ctxn = null;
                    if (testNum == 17)
                    {
                        KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID.ToString() == ketQuaXetNghiemGUID);
                        if (kqxn == null) continue;        
                        if (kqxn.NgayXN.Hour < 14)
                        {
                            ctxn = ctxns[0];
                            if (ctxns[0].DoiTuong != (byte)DoiTuong.Chung) ctxn = ctxns[1];
                        }
                        else
                        {
                            row["FullName"] = "Postprandial blood sugar";
                            ctxn = ctxns[1];
                            if (ctxns[1].DoiTuong != (byte)DoiTuong.Chung_Sau2h) ctxn = ctxns[0];
                        }
                    }
                    else
                    {
                        if (ctxns[0].DoiTuong == (byte)DoiTuong.Chung) ctxn = ctxns[0];
                        else if (ctxns[0].DoiTuong == (byte)DoiTuong.Nam || ctxns[0].DoiTuong == (byte)DoiTuong.Nu)
                        {
                            KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID.ToString() == ketQuaXetNghiemGUID);
                            if (kqxn == null) continue;
                            if (!kqxn.Sex.HasValue || kqxn.Sex.Value == (int)Gender.None) continue;

                            if (kqxn.Sex.Value == (int)Gender.Male)
                            {
                                ctxn = ctxns[0];
                                if (ctxns[0].DoiTuong != (byte)DoiTuong.Nam) ctxn = ctxns[1];
                            }
                            else
                            {
                                ctxn = ctxns[1];
                                if (ctxns[1].DoiTuong != (byte)DoiTuong.Nu) ctxn = ctxns[0];
                            }
                        }
                        else
                        {
                            KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID.ToString() == ketQuaXetNghiemGUID);
                            if (kqxn == null) continue;
                            //if (!kqxn.Age.HasValue || kqxn.Age.Value <= 0) continue;
                            //if (!kqxn.AgeUnit.HasValue || kqxn.AgeUnit.Value == (int)AgeUnit.Unknown ||
                            //    kqxn.AgeUnit == (int)AgeUnit.Days || kqxn.AgeUnit == (int)AgeUnit.Months) continue;
                            //if (kqxn.Age.Value < 18) continue;

                            for (int i = 0; i < ctxns.Count; i++)
                            {
                                if (ctxns[i].DoiTuong == (byte)DoiTuong.NguoiLon)
                                {
                                    ctxn = ctxns[i];
                                    break;
                                }
                            }

                            if (ctxn == null)
                            {
                                for (int i = 0; i < ctxns.Count; i++)
                                {
                                    if (ctxns[i].DoiTuong == (byte)DoiTuong.NguoiCaoTuoi)
                                    {
                                        ctxn = ctxns[i];
                                        break;
                                    }
                                }
                            }

                            if (ctxn == null)
                            {
                                for (int i = 0; i < ctxns.Count; i++)
                                {
                                    if (ctxns[i].DoiTuong == (byte)DoiTuong.TreEm)
                                    {
                                        ctxn = ctxns[i];
                                        break;
                                    }
                                }
                            }
                            //if (kqxn.Age.Value <= 60) //Người trưởng thành
                            //{
                            //    ctxn = ctxns[0];
                            //    if (ctxns[0].DoiTuong != (byte)DoiTuong.NguoiLon) ctxn = ctxns[1];
                            //}
                            //else //Người cao tuổi
                            //{
                            //    ctxn = ctxns[1];
                            //    if (ctxns[1].DoiTuong != (byte)DoiTuong.NguoiCaoTuoi) ctxn = ctxns[0];
                            //}
                        }
                    }

                    if (ctxn == null) continue;

                    DoiTuong doiTuong = (DoiTuong)ctxn.DoiTuong;

                    if (ctxn.FromValue.HasValue && ctxn.ToValue.HasValue)
                    {
                        if (testResult < ctxn.FromValue.Value || testResult > ctxn.ToValue.Value)
                            row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                        else
                            row["TinhTrang"] = (byte)TinhTrang.BinhThuong;

                        switch (doiTuong)
                        {
                            case DoiTuong.Chung:
                            case DoiTuong.Chung_Sau2h:
                                row["BinhThuong"] = string.Format("({0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.Nam:
                                row["BinhThuong"] = string.Format("(M: {0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.Nu:
                                row["BinhThuong"] = string.Format("(F: {0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.NguoiLon:
                                row["BinhThuong"] = string.Format("Adult ({0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.NguoiCaoTuoi:
                                row["BinhThuong"] = string.Format("> 60 year ({0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                        }
                    }
                    else if (ctxn.FromValue.HasValue)
                    {
                        if (testResult <= ctxn.FromValue.Value)
                            row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                        else
                            row["TinhTrang"] = (byte)TinhTrang.BinhThuong;

                        switch (doiTuong)
                        {
                            case DoiTuong.Chung:
                            case DoiTuong.Chung_Sau2h:
                                row["BinhThuong"] = string.Format("(> {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.Nam:
                                row["BinhThuong"] = string.Format("(M > {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.Nu:
                                row["BinhThuong"] = string.Format("(F > {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.NguoiLon:
                                row["BinhThuong"] = string.Format("Adult (> {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.NguoiCaoTuoi:
                                row["BinhThuong"] = string.Format("> 60 year (> {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                break;
                        }
                    }
                    else
                    {
                        if (testResult >= ctxn.ToValue.Value)
                            row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                        else
                            row["TinhTrang"] = (byte)TinhTrang.BinhThuong;

                        switch (doiTuong)
                        {
                            case DoiTuong.Chung:
                            case DoiTuong.Chung_Sau2h:
                                row["BinhThuong"] = string.Format("(< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.Nam:
                                row["BinhThuong"] = string.Format("(M < {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.Nu:
                                row["BinhThuong"] = string.Format("(F < {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.NguoiLon:
                                row["BinhThuong"] = string.Format("Adult (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                            case DoiTuong.NguoiCaoTuoi:
                                row["BinhThuong"] = string.Format("> 60 year (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                        }
                    }
                }

                db.Dispose();
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
                        string strSex = testResult.Sex.Trim();
                        string strAge = testResult.Age.Trim();
                        string strAgeUnit = testResult.AgeUnit.Trim();
                        Gender gender = Gender.None;
                        AgeUnit ageUnit = AgeUnit.Unknown;
                        int age = 0;

                        if (strSex != string.Empty)
                        {
                            int enumIndex = Convert.ToInt32(strSex);
                            if (enumIndex == 0) gender = Gender.None;
                            else if (enumIndex == 1) gender = Gender.Male;
                            else gender = Gender.Female;
                        }

                        if (strAge != string.Empty) age = Convert.ToInt32(strAge);
                        if (strAgeUnit != string.Empty) ageUnit = (AgeUnit)Convert.ToInt32(strAgeUnit);

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
                            kqxn.Sex = (int)gender;
                            kqxn.Age = age;
                            kqxn.AgeUnit = (int)ageUnit;

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
                            kqxn.Sex = (int)gender;
                            kqxn.Age = age;
                            kqxn.AgeUnit = (int)ageUnit;
                        }

                        db.SubmitChanges();

                        foreach (Result_Hitachi917 r in testResult.Results)
                        {
                            int testNum = r.TestNum;
                            byte tinhTrang = 0;
                                                        
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
                        tk.ActionType = (byte)ActionType.Edit;
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

        public static Result UpdateChiSoKetQuaXetNghiem(ChiTietKetQuaXetNghiem_Hitachi917 chiTietKQXN)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    ChiTietKetQuaXetNghiem_Hitachi917 ctkqxn = db.ChiTietKetQuaXetNghiem_Hitachi917s.SingleOrDefault<ChiTietKetQuaXetNghiem_Hitachi917>(c => c.ChiTietKQXN_Hitachi917GUID == chiTietKQXN.ChiTietKQXN_Hitachi917GUID);
                    if (ctkqxn != null)
                    {
                        ctkqxn.UpdatedDate = DateTime.Now;
                        ctkqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                        ctkqxn.Status = (byte)Status.Actived;
                        ctkqxn.TestResult = chiTietKQXN.TestResult;

                        KetQuaXetNghiem_Hitachi917 kqxn = ctkqxn.KetQuaXetNghiem_Hitachi917;
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

                        //
                        for (int i = 0; i < 1; i++)
                        {
                            XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.TestNum == chiTietKQXN.TestNum);
                            if (xn == null) continue;
                            List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.ToList<ChiTietXetNghiem_Hitachi917>();
                            if (ctxns.Count <= 0) continue;

                            double testResult = Convert.ToDouble(chiTietKQXN.TestResult.Trim());
                            ChiTietXetNghiem_Hitachi917 ctxn = null;
                            if (chiTietKQXN.TestNum == 17)
                            {
                                if (kqxn == null) continue;
                                if (kqxn.NgayXN.Hour < 14)
                                {
                                    ctxn = ctxns[0];
                                    if (ctxns[0].DoiTuong != (byte)DoiTuong.Chung) ctxn = ctxns[1];
                                }
                                else
                                {
                                    ctxn = ctxns[1];
                                    if (ctxns[1].DoiTuong != (byte)DoiTuong.Chung_Sau2h) ctxn = ctxns[0];
                                }
                            }
                            else
                            {
                                if (ctxns[0].DoiTuong == (byte)DoiTuong.Chung) ctxn = ctxns[0];
                                else if (ctxns[0].DoiTuong == (byte)DoiTuong.Nam || ctxns[0].DoiTuong == (byte)DoiTuong.Nu)
                                {
                                    if (kqxn == null) continue;
                                    if (!kqxn.Sex.HasValue || kqxn.Sex.Value == (int)Gender.None) continue;

                                    if (kqxn.Sex.Value == (int)Gender.Male)
                                    {
                                        ctxn = ctxns[0];
                                        if (ctxns[0].DoiTuong != (byte)DoiTuong.Nam) ctxn = ctxns[1];
                                    }
                                    else
                                    {
                                        ctxn = ctxns[1];
                                        if (ctxns[1].DoiTuong != (byte)DoiTuong.Nu) ctxn = ctxns[0];
                                    }
                                }
                                else
                                {
                                    if (kqxn == null) continue;
                                    //if (!kqxn.Age.HasValue || kqxn.Age.Value <= 0) continue;
                                    //if (!kqxn.AgeUnit.HasValue || kqxn.AgeUnit.Value == (int)AgeUnit.Unknown ||
                                    //    kqxn.AgeUnit == (int)AgeUnit.Days || kqxn.AgeUnit == (int)AgeUnit.Months) continue;
                                    //if (kqxn.Age.Value < 18) continue;

                                    //if (kqxn.Age.Value <= 60) //Người trưởng thành
                                    //{
                                    //    ctxn = ctxns[0];
                                    //    if (ctxns[0].DoiTuong != (byte)DoiTuong.NguoiLon) ctxn = ctxns[1];
                                    //}
                                    //else //Người cao tuổi
                                    //{
                                    //    ctxn = ctxns[1];
                                    //    if (ctxns[1].DoiTuong != (byte)DoiTuong.NguoiCaoTuoi) ctxn = ctxns[0];
                                    //}

                                    for (int j = 0; j < ctxns.Count; j++)
                                    {
                                        if (ctxns[j].DoiTuong == (byte)DoiTuong.NguoiLon)
                                        {
                                            ctxn = ctxns[j];
                                            break;
                                        }
                                    }

                                    if (ctxn == null)
                                    {
                                        for (int j = 0; j < ctxns.Count; j++)
                                        {
                                            if (ctxns[j].DoiTuong == (byte)DoiTuong.NguoiCaoTuoi)
                                            {
                                                ctxn = ctxns[j];
                                                break;
                                            }
                                        }
                                    }

                                    if (ctxn == null)
                                    {
                                        for (int j = 0; j < ctxns.Count; j++)
                                        {
                                            if (ctxns[j].DoiTuong == (byte)DoiTuong.TreEm)
                                            {
                                                ctxn = ctxns[j];
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (ctxn == null) continue;

                            DoiTuong doiTuong = (DoiTuong)ctxn.DoiTuong;

                            if (ctxn.FromValue.HasValue && ctxn.ToValue.HasValue)
                            {
                                if (testResult < ctxn.FromValue.Value || testResult > ctxn.ToValue.Value)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                                else
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BinhThuong;
                            }
                            else if (ctxn.FromValue.HasValue)
                            {
                                if (testResult <= ctxn.FromValue.Value)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                                else
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BinhThuong;
                            }
                            else
                            {
                                if (testResult >= ctxn.ToValue.Value)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                                else
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BinhThuong;
                            }
                        }
                        //

                        desc += string.Format("GUID: '{0}', IDNum: '{1}', Mã bệnh nhân: '{2}', Tên bệnh nhân: '{3}', Ngày xét nghiệm: '{4}', OperationID: '{5}', TestNum: '{6}', TestResult: '{7}'",
                                kqxn.KQXN_Hitachi917GUID.ToString(), kqxn.IDNum, fileNum, tenBenhNhan, kqxn.NgayXN.ToString("dd/MM/yyyy HH:mm:ss"), kqxn.OperationID, ctkqxn.TestNum, ctkqxn.TestResult);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Cập nhật chỉ số kết quả xét nghiệm hitachi 917";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    
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

        public static Result DeleteChiTietKetQuaXetNghiem(List<string> keys)
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
                        ChiTietKetQuaXetNghiem_Hitachi917 ctkqxn = db.ChiTietKetQuaXetNghiem_Hitachi917s.SingleOrDefault<ChiTietKetQuaXetNghiem_Hitachi917>(c => c.ChiTietKQXN_Hitachi917GUID.ToString() == key);
                        if (ctkqxn != null)
                        {
                            ctkqxn.DeletedDate = DateTime.Now;
                            ctkqxn.DeletedBy = Guid.Parse(Global.UserGUID);
                            ctkqxn.Status = (byte)Status.Deactived;

                            KetQuaXetNghiem_Hitachi917 kqxn = ctkqxn.KetQuaXetNghiem_Hitachi917;
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

                            desc += string.Format("- GUID: '{0}', IDNum: '{1}', Mã bệnh nhân: '{2}', Tên bệnh nhân: '{3}', Ngày xét nghiệm: '{4}', OperationID: '{5}', TestNum: '{6}', TestResult: '{7}'\n",
                                    kqxn.KQXN_Hitachi917GUID.ToString(), kqxn.IDNum, fileNum, tenBenhNhan, kqxn.NgayXN.ToString("dd/MM/yyyy HH:mm:ss"), kqxn.OperationID, ctkqxn.TestNum, ctkqxn.TestResult);

                        }    
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa chi tiết kết quả xét nghiệm hitachi 917";
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
