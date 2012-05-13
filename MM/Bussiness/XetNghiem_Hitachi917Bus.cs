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
        public static Result GetDanhSachXetNghiem()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT DISTINCT X.* FROM XetNghiem_Hitachi917 X, ChiTietXetNghiem_Hitachi917 C WHERE X.Status = {0} AND X.XetNghiemGUID = C.XetNghiemGUID ORDER BY X.Fullname", 
                    (byte)Status.Actived);
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

        public static Result GetKetQuaXetNghiemList(DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (tenBenhNhan.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_Hitachi917View WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} ORDER BY NgayXN DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_Hitachi917View WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} AND FullName LIKE N'%{3}%' ORDER BY NgayXN DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived, tenBenhNhan);
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

        private static ChiTietXetNghiem_Hitachi917 GetChiTietXetNghiem(List<ChiTietXetNghiem_Hitachi917> ctxns, DoiTuong doiTuong)
        {
            foreach (ChiTietXetNghiem_Hitachi917 ctxn in ctxns)
            {
                if (ctxn.DoiTuong == (int)doiTuong)
                    return ctxn;
            }

            return null;
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
                KetQuaXetNghiem_Hitachi917 kqxn = null;
                kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID.ToString() == ketQuaXetNghiemGUID);
                if (kqxn == null) return result;

                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    int testNum = Convert.ToInt32(row["TestNum"]);
                    double testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());

                    bool isSau2h = false;

                    if (testNum == 17)
                    {
                        if (kqxn.NgayXN.Hour > 14)
                        {
                            row["FullName"] = "Postprandial blood sugar";
                            isSau2h = true;
                        }
                    }

                    if ((row["FromValue"] != null && row["FromValue"] != DBNull.Value) ||
                        (row["ToValue"] != null && row["ToValue"] != DBNull.Value))
                    {
                        #region Đã cập nhật chỉ số xét nghiệm
                        string donVi = string.Empty;
                        if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                            donVi = row["DonVi"].ToString();

                        double fromValue = 0;
                        double toValue = 0;
                        int kq = 0;
                        if (row["FromValue"] == null || row["FromValue"] == DBNull.Value)
                        {
                            kq = 1;
                            toValue = Convert.ToDouble(row["ToValue"]);
                            if (testResult >= toValue)
                                row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                            else
                                row["TinhTrang"] = (byte)TinhTrang.BinhThuong;
                        }
                        else if (row["ToValue"] == null || row["ToValue"] == DBNull.Value)
                        {
                            kq = 2;
                            fromValue = Convert.ToDouble(row["FromValue"]);
                            if (testResult <= fromValue)
                                row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                            else
                                row["TinhTrang"] = (byte)TinhTrang.BinhThuong;
                        }
                        else
                        {
                            fromValue = Convert.ToDouble(row["FromValue"]);
                            toValue = Convert.ToDouble(row["ToValue"]);

                            if (testResult >= fromValue && testResult <= toValue)
                                row["TinhTrang"] = TinhTrang.BinhThuong;
                            else
                                row["TinhTrang"] = TinhTrang.BatThuong;
                        }


                        DoiTuong doiTuong = (DoiTuong)Convert.ToByte(row["DoiTuong"]);
                        switch (doiTuong)
                        {
                            case DoiTuong.Chung:
                            case DoiTuong.Chung_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("({0} - {1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(< {0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(> {0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.Nam:
                            case DoiTuong.Nam_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("(M: {0} - {1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(M < {0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(M > {0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.Nu:
                            case DoiTuong.Nu_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("(F: {0} - {1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(F < {0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(F > {0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.TreEm:
                            case DoiTuong.TreEm_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("Child ({0} - {1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("Child (< {0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("Child (> {0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.NguoiLon:
                            case DoiTuong.NguoiLon_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("Adult ({0} - {1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("Adult (< {0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("Adult (> {0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.NguoiCaoTuoi:
                            case DoiTuong.NguoiCaoTuoi_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("> 60 year ({0} - {1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("> 60 year (< {0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("> 60 year (> {0} {1})", fromValue, donVi);
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Chưa cập nhật chỉ số xét nghiệm
                        XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.TestNum == testNum);
                        if (xn == null) continue;
                        List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.ToList<ChiTietXetNghiem_Hitachi917>();
                        if (ctxns.Count <= 0) continue;
                        ChiTietXetNghiem_Hitachi917 ctxn = null;
                        Gender gender = Gender.None;

                        if (!isSau2h)
                        {
                            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung);
                            if (ctxn == null)
                            {
                                if (kqxn.PatientGUID.HasValue)
                                {
                                    PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.PatientGUID == kqxn.PatientGUID.Value);
                                    if (patient == null) continue;
                                    if (patient.Gender.HasValue) gender = (Gender)patient.Gender.Value;
                                }
                                else
                                {
                                    if (kqxn.Sex.HasValue) gender = (Gender)kqxn.Sex.Value;
                                }

                                if (gender == Gender.None) continue;

                                if (gender == Gender.Male)
                                    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nam);
                                else
                                    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nu);
                            }

                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiLon);
                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiCaoTuoi);
                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.TreEm);
                        }
                        else
                        {
                            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung_Sau2h);
                            if (ctxn == null)
                            {
                                if (kqxn == null)
                                    kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID.ToString() == ketQuaXetNghiemGUID);
                                if (kqxn == null) continue;

                                if (kqxn.PatientGUID.HasValue)
                                {
                                    PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.PatientGUID == kqxn.PatientGUID.Value);
                                    if (patient == null) continue;
                                    if (patient.Gender.HasValue) gender = (Gender)patient.Gender.Value;
                                }
                                else
                                {
                                    if (kqxn.Sex.HasValue) gender = (Gender)kqxn.Sex.Value;
                                }

                                if (gender == Gender.None) continue;

                                if (gender == Gender.Male)
                                    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nam_Sau2h);
                                else
                                    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nu_Sau2h);
                            }

                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiLon_Sau2h);
                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiCaoTuoi_Sau2h);
                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.TreEm_Sau2h);
                        }

                        if (ctxn == null) continue;

                        ChiTietKetQuaXetNghiem_Hitachi917 ctkqxn = db.ChiTietKetQuaXetNghiem_Hitachi917s.SingleOrDefault<ChiTietKetQuaXetNghiem_Hitachi917>(c => c.ChiTietKQXN_Hitachi917GUID.ToString() == row["ChiTietKQXN_Hitachi917GUID"].ToString());
                        if (ctkqxn != null)
                        {
                            ctkqxn.FromValue = ctxn.FromValue;
                            ctkqxn.ToValue = ctxn.ToValue;
                            ctkqxn.DoiTuong = ctxn.DoiTuong;
                            ctkqxn.DonVi = ctxn.DonVi;

                            if (ctxn.FromValue.HasValue)
                                row["FromValue"] = ctxn.FromValue.Value;

                            if (ctxn.ToValue.HasValue)
                                row["ToValue"] = ctxn.ToValue.Value;

                            row["DoiTuong"] = ctxn.DoiTuong;
                            row["DonVi"] = ctxn.DonVi;

                            db.SubmitChanges();
                        }

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
                                case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M: {0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F: {0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child ({0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult ({0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                case DoiTuong.NguoiCaoTuoi_Sau2h:
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
                                case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M > {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F > {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child (> {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult (> {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                case DoiTuong.NguoiCaoTuoi_Sau2h:
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
                                case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M < {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F < {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    row["BinhThuong"] = string.Format("< 60 year (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                            }
                        }
                        #endregion
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

                        //
                        var ctkqs = kqxn.ChiTietKetQuaXetNghiem_Hitachi917s;
                        foreach (ChiTietKetQuaXetNghiem_Hitachi917 ctkq in ctkqs)
                        {
                            int testNum = ctkq.TestNum;
                            double testResult = Convert.ToDouble(ctkq.TestResult.Trim());
                            bool isSau2h = false;

                            if (testNum == 17)
                            {
                                if (kqxn.NgayXN.Hour > 14) isSau2h = true;
                            }

                            #region Chưa cập nhật chỉ số xét nghiệm
                            XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.TestNum == testNum);
                            if (xn == null) continue;
                            List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.ToList<ChiTietXetNghiem_Hitachi917>();
                            if (ctxns.Count <= 0) continue;
                            ChiTietXetNghiem_Hitachi917 ctxn = null;
                            Gender gender = Gender.None;

                            if (!isSau2h)
                            {
                                ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung);
                                if (ctxn == null)
                                {
                                    if (kqxn.PatientGUID != null && kqxn.PatientGUID.HasValue)
                                    {
                                        PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.PatientGUID == kqxn.PatientGUID.Value);
                                        if (patient == null) continue;
                                        if (patient.Gender.HasValue) gender = (Gender)patient.Gender.Value;
                                    }
                                    else
                                    {
                                        if (kqxn.Sex.HasValue) gender = (Gender)kqxn.Sex.Value;
                                    }

                                    if (gender == Gender.None) continue;

                                    if (gender == Gender.Male)
                                        ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nam);
                                    else
                                        ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nu);
                                }

                                if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiLon);
                                if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiCaoTuoi);
                                if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.TreEm);
                            }
                            else
                            {
                                ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung_Sau2h);
                                if (ctxn == null)
                                {
                                    if (kqxn.PatientGUID != null && kqxn.PatientGUID.HasValue)
                                    {
                                        PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.PatientGUID == kqxn.PatientGUID.Value);
                                        if (patient == null) continue;
                                        if (patient.Gender.HasValue) gender = (Gender)patient.Gender.Value;
                                    }
                                    else
                                    {
                                        if (kqxn.Sex.HasValue) gender = (Gender)kqxn.Sex.Value;
                                    }

                                    if (gender == Gender.None) continue;

                                    if (gender == Gender.Male)
                                        ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nam_Sau2h);
                                    else
                                        ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nu_Sau2h);
                                }

                                if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiLon_Sau2h);
                                if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiCaoTuoi_Sau2h);
                                if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.TreEm_Sau2h);
                            }

                            if (ctxn == null) continue;

                            ctkq.FromValue = ctxn.FromValue;
                            ctkq.ToValue = ctxn.ToValue;
                            ctkq.DoiTuong = ctxn.DoiTuong;
                            ctkq.DonVi = ctxn.DonVi;
                            #endregion
                        }
                        //

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
                        ctkqxn.FromValue = chiTietKQXN.FromValue;
                        ctkqxn.ToValue = chiTietKQXN.ToValue;
                        ctkqxn.DoiTuong = chiTietKQXN.DoiTuong;
                        ctkqxn.DonVi = chiTietKQXN.DonVi;

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

                        if ((chiTietKQXN.FromValue != null && chiTietKQXN.FromValue.HasValue) ||
                        (chiTietKQXN.ToValue != null && chiTietKQXN.ToValue.HasValue))
                        {
                            double testResult = Convert.ToDouble(chiTietKQXN.TestResult);
                            string donVi = string.Empty;
                            if (chiTietKQXN.DonVi != null && chiTietKQXN.DonVi.Trim() != string.Empty)
                                donVi = chiTietKQXN.DonVi;

                            double fromValue = 0;
                            double toValue = 0;
                            int kq = 0;
                            if (chiTietKQXN.FromValue == null || !chiTietKQXN.FromValue.HasValue)
                            {
                                kq = 1;
                                toValue = chiTietKQXN.ToValue.Value;
                                if (testResult >= toValue)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                                else
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BinhThuong;
                            }
                            else if (chiTietKQXN.ToValue == null || !chiTietKQXN.ToValue.HasValue)
                            {
                                kq = 2;
                                fromValue = chiTietKQXN.FromValue.Value;
                                if (testResult <= fromValue)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                                else
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BinhThuong;
                            }
                            else
                            {
                                fromValue = chiTietKQXN.FromValue.Value;
                                toValue = chiTietKQXN.ToValue.Value;

                                if (testResult >= fromValue && testResult <= toValue)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BinhThuong;
                                else
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                            }

                            DoiTuong doiTuong = (DoiTuong)chiTietKQXN.DoiTuong;
                            switch (doiTuong)
                            {
                                case DoiTuong.Chung:
                                case DoiTuong.Chung_Sau2h:
                                    if (kq == 0)
                                        result.QueryResult = string.Format("({0} - {1} {2})", fromValue, toValue, donVi);
                                    else if (kq == 1)
                                        result.QueryResult = string.Format("(< {0} {1})", toValue, donVi);
                                    else
                                        result.QueryResult = string.Format("(> {0} {1})", fromValue, donVi);
                                    break;
                                case DoiTuong.Nam:
                                case DoiTuong.Nam_Sau2h:
                                    if (kq == 0)
                                        result.QueryResult = string.Format("(M: {0} - {1} {2})", fromValue, toValue, donVi);
                                    else if (kq == 1)
                                        result.QueryResult = string.Format("(M < {0} {1})", toValue, donVi);
                                    else
                                        result.QueryResult = string.Format("(M > {0} {1})", fromValue, donVi);
                                    break;
                                case DoiTuong.Nu:
                                case DoiTuong.Nu_Sau2h:
                                    if (kq == 0)
                                        result.QueryResult = string.Format("(F: {0} - {1} {2})", fromValue, toValue, donVi);
                                    else if (kq == 1)
                                        result.QueryResult = string.Format("(F < {0} {1})", toValue, donVi);
                                    else
                                        result.QueryResult = string.Format("(F > {0} {1})", fromValue, donVi);
                                    break;
                                case DoiTuong.TreEm:
                                case DoiTuong.TreEm_Sau2h:
                                    if (kq == 0)
                                        result.QueryResult = string.Format("Child ({0} - {1} {2})", fromValue, toValue, donVi);
                                    else if (kq == 1)
                                        result.QueryResult = string.Format("Child (< {0} {1})", toValue, donVi);
                                    else
                                        result.QueryResult = string.Format("Child (> {0} {1})", fromValue, donVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                case DoiTuong.NguoiLon_Sau2h:
                                    if (kq == 0)
                                        result.QueryResult = string.Format("Adult ({0} - {1} {2})", fromValue, toValue, donVi);
                                    else if (kq == 1)
                                        result.QueryResult = string.Format("Adult (< {0} {1})", toValue, donVi);
                                    else
                                        result.QueryResult = string.Format("Adult (> {0} {1})", fromValue, donVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    if (kq == 0)
                                        result.QueryResult = string.Format("> 60 year ({0} - {1} {2})", fromValue, toValue, donVi);
                                    else if (kq == 1)
                                        result.QueryResult = string.Format("> 60 year (< {0} {1})", toValue, donVi);
                                    else
                                        result.QueryResult = string.Format("> 60 year (> {0} {1})", fromValue, donVi);
                                    break;
                            }
                        }
                        else
                        {
                            chiTietKQXN.TinhTrang = (byte)TinhTrang.BinhThuong;
                            result.QueryResult = string.Empty;
                        }

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

        public static Result GetChiTietXetNghiemList(string xetNghiemGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM ChiTietXetNghiem_Hitachi917 WHERE Status={0} AND XetNghiemGUID='{1}'",
                    (byte)Status.Actived, xetNghiemGUID);
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

        public static Result UpdateXetNghiem(XetNghiem_Hitachi917 xetNghiem, List<ChiTietXetNghiem_Hitachi917> ctxns)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.XetNghiemGUID == xetNghiem.XetNghiemGUID);
                    if (xn != null)
                    {
                        xn.UpdatedDate = DateTime.Now;
                        xn.UpdatedBy = Guid.Parse(Global.UserGUID);

                        //Update chi tiết xét nghiệm
                        var chiTietXetNghiemList = xn.ChiTietXetNghiem_Hitachi917s;
                        foreach (ChiTietXetNghiem_Hitachi917 ctxn in chiTietXetNghiemList)
                        {
                            ctxn.UpdatedDate = DateTime.Now;
                            ctxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                            ctxn.Status = (byte)Status.Deactived;
                        }

                        db.SubmitChanges();

                        foreach (ChiTietXetNghiem_Hitachi917 ctxn in ctxns)
                        {
                            ChiTietXetNghiem_Hitachi917 ct = db.ChiTietXetNghiem_Hitachi917s.SingleOrDefault<ChiTietXetNghiem_Hitachi917>(c => c.DoiTuong == ctxn.DoiTuong &&
                                c.XetNghiemGUID == xn.XetNghiemGUID);
                            if (ct == null)
                            {
                                ctxn.ChiTietXetNghiemGUID = Guid.NewGuid();
                                ctxn.XetNghiemGUID = xn.XetNghiemGUID;
                                ctxn.CreatedBy = Guid.Parse(Global.UserGUID);
                                ctxn.CreatedDate = DateTime.Now;
                                db.ChiTietXetNghiem_Hitachi917s.InsertOnSubmit(ctxn);
                            }
                            else
                            {
                                ct.FromValue = ctxn.FromValue;
                                ct.ToValue = ctxn.ToValue;
                                ct.DonVi = ctxn.DonVi;
                                ct.DoiTuong = ctxn.DoiTuong;
                                ct.UpdatedBy = Guid.Parse(Global.UserGUID);
                                ct.UpdatedDate = DateTime.Now;
                                ct.Status = (byte)Status.Actived;
                            }
                        }

                        //Tracking
                        desc += string.Format("GUID: '{0}', Tên xét nghiệm: '{1}'", xn.XetNghiemGUID.ToString(), xn.Fullname);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Sửa thông tin xét nghiệm Hitachi917";
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

        public static Result GetDonViList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT DonVi FROM ChiTietXetNghiem_Hitachi917");
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

        public static Result UpdateDaIn(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        ChiTietKetQuaXetNghiem_Hitachi917 ctkqxn = db.ChiTietKetQuaXetNghiem_Hitachi917s.SingleOrDefault<ChiTietKetQuaXetNghiem_Hitachi917>(c => c.ChiTietKQXN_Hitachi917GUID.ToString() == key);
                        if (ctkqxn != null)
                        {
                            ctkqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                            ctkqxn.UpdatedDate = DateTime.Now;
                            ctkqxn.DaIn = true;
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
    }
}
