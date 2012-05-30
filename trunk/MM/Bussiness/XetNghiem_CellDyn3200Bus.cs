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
    public class XetNghiem_CellDyn3200Bus : BusBase
    {
        public static Result GetDanhSachXetNghiem()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT *, '' AS Normal, '' AS NormalPercent FROM XetNghiem_CellDyn3200 WHERE Status = {0} ORDER BY GroupID, [Order]", (byte)Status.Actived);
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {

                    if (row["FromValue"] != null && row["FromValue"] != DBNull.Value &&
                        row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                        row["Normal"] = string.Format("{0:F2} - {1:F3}", Convert.ToDouble(row["FromValue"]), Convert.ToDouble(row["ToValue"]));
                    else if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                        row["Normal"] = string.Format("> {0:F2}", Convert.ToDouble(row["FromValue"]));
                    else
                        row["Normal"] = string.Format("< {0:F2}", Convert.ToDouble(row["ToValue"]));

                    string donVi = string.Empty;
                    if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                        donVi = row["DonVi"].ToString();

                    if (row["FromPercent"] != null && row["FromPercent"] != DBNull.Value &&
                        row["ToPercent"] != null && row["ToPercent"] != DBNull.Value)
                    {
                        row["NormalPercent"] = string.Format("{0:F2} - {1:F3} {2}", 
                            Convert.ToDouble(row["FromPercent"]), Convert.ToDouble(row["ToPercent"]), donVi);
                    }
                    else if (row["FromPercent"] != null && row["FromPercent"] != DBNull.Value)
                        row["NormalPercent"] = string.Format("> {0:F2} {1}", Convert.ToDouble(row["FromPercent"]), donVi);
                    else if (row["ToPercent"] != null && row["ToPercent"] != DBNull.Value)
                        row["NormalPercent"] = string.Format("< {0:F2} {1}", Convert.ToDouble(row["ToPercent"]), donVi);
                    else
                        row["Normal"] = string.Format("{0} {1}", row["Normal"], donVi);
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

            return result;
        }

        public static Result GetKetQuaXetNghiemList(DateTime fromDate, DateTime toDate, string tenBenhNhan, bool isMaBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (tenBenhNhan.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_CellDyn3200View WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} ORDER BY NgayXN DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived);
                }
                else
                {
                    if (!isMaBenhNhan)
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_CellDyn3200View WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} AND FullName LIKE N'%{3}%' ORDER BY NgayXN DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived, tenBenhNhan);
                    }
                    else
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_CellDyn3200View WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} AND FileNum LIKE N'%{3}%' ORDER BY NgayXN DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived, tenBenhNhan);
                    }
                    
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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST('' AS nvarchar(50)) AS BinhThuong, CAST('' AS nvarchar(50)) AS [Percent] FROM ChiTietKetQuaXetNghiem_CellDyn3200View WHERE KQXN_CellDyn3200GUID = '{0}' AND Status = {1} ORDER BY GroupID, [Order]",
                           ketQuaXetNghiemGUID, (byte)Status.Actived);

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                MMOverride db = new MMOverride();
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    double? fromValue = null;
                    double? toValue = null;
                    double? fromPercent = null;
                    double? toPercent = null;
                    string donVi = string.Empty;

                    if ((row["FromValue2"] != null && row["FromValue2"] != DBNull.Value) ||
                        (row["ToValue2"] != null && row["ToValue2"] != DBNull.Value))
                    {
                        if (row["FromValue2"] != null && row["FromValue2"] != DBNull.Value)
                            fromValue = Convert.ToDouble(row["FromValue2"]);

                        if (row["ToValue2"] != null && row["ToValue2"] != DBNull.Value)
                            toValue = Convert.ToDouble(row["ToValue2"]);

                        if (row["FromPercent2"] != null && row["FromPercent2"] != DBNull.Value)
                            fromPercent = Convert.ToDouble(row["FromPercent2"]);

                        if (row["ToPercent2"] != null && row["ToPercent2"] != DBNull.Value)
                            toPercent = Convert.ToDouble(row["ToPercent2"]);

                        if (row["DonVi2"] != null && row["DonVi2"] != DBNull.Value)
                            donVi = row["DonVi2"].ToString().Trim();
                    }
                    else
                    {
                        string tenXetNghiem = row["Fullname"].ToString();
                        XetNghiem_CellDyn3200 xn = db.XetNghiem_CellDyn3200s.SingleOrDefault<XetNghiem_CellDyn3200>(x => x.TenXetNghiem == tenXetNghiem);
                        if (xn == null) continue;

                        if (xn.FromValue.HasValue)
                            fromValue = xn.FromValue.Value;

                        if (xn.ToValue.HasValue)
                            toValue = xn.ToValue.Value;

                        if (xn.FromPercent.HasValue)
                            fromPercent = xn.FromPercent.Value;

                        if (xn.ToPercent.HasValue)
                            toPercent = xn.ToPercent.Value;

                        if (xn.DonVi != null && xn.DonVi != string.Empty) 
                            donVi = xn.DonVi;
                    }
                    
                    double testResult = Convert.ToDouble(row["TestResult"]);
                    TinhTrang tinhTrang = TinhTrang.BinhThuong;

                    if (fromValue != null && toValue != null)
                    {
                        if (fromPercent != null || toPercent != null)
                            row["BinhThuong"] = string.Format("({0:F2} - {1:F2})", fromValue.Value, toValue.Value);
                        else
                            row["BinhThuong"] = string.Format("({0:F2} - {1:F2} {2})", fromValue.Value, toValue.Value, donVi);

                        if (testResult < fromValue.Value || testResult > toValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }
                    else if (fromValue != null)
                    {
                        if (fromPercent != null || toPercent != null)
                            row["BinhThuong"] = string.Format("(> {0:F2})", fromValue.Value);
                        else
                            row["BinhThuong"] = string.Format("(> {0:F2} {1})", fromValue.Value, donVi);

                        if (testResult <= fromValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }
                    else
                    {
                        if (fromPercent != null || toPercent != null)
                            row["BinhThuong"] = string.Format("(< {0:F2})", toValue.Value);
                        else
                            row["BinhThuong"] = string.Format("(< {0:F2} {1})", toValue.Value, donVi);

                        if (testResult >= toValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }

                    if (fromPercent != null && toPercent != null)
                    {
                        double testPercent = Convert.ToDouble(row["TestPercent"]);
                        row["Percent"] = string.Format("{0:F2}% ({1:F2} - {2:F2} {3})", testPercent, fromPercent.Value, toPercent.Value, donVi);

                        if (tinhTrang == TinhTrang.BinhThuong)
                        {
                            if (testPercent < fromPercent.Value || testPercent > toPercent.Value)
                                tinhTrang = TinhTrang.BatThuong;
                        }
                    }
                    else if (fromPercent != null)
                    {
                        double testPercent = Convert.ToDouble(row["TestPercent"]);
                        row["Percent"] = string.Format("{0:F2}% (> {1:F2} {2})", testPercent, fromPercent.Value, donVi);

                        if (tinhTrang == TinhTrang.BinhThuong)
                        {
                            if (testPercent <= fromPercent.Value)
                                tinhTrang = TinhTrang.BatThuong;
                        }
                    }
                    else if (toPercent != null)
                    {
                        double testPercent = Convert.ToDouble(row["TestPercent"]);
                        row["Percent"] = string.Format("{0:F2}% (< {1:F2} {2})", testPercent, toPercent.Value, donVi);

                        if (tinhTrang == TinhTrang.BinhThuong)
                        {
                            if (testPercent >= toPercent.Value)
                                tinhTrang = TinhTrang.BatThuong;
                        }
                    }

                    row["TinhTrang"] = (byte)tinhTrang;

                    ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.ChiTietKQXN_CellDyn3200GUID.ToString() == row["ChiTietKQXN_CellDyn3200GUID"].ToString());
                    if (ctkqxn != null)
                    {
                        ctkqxn.FromValue = fromValue;
                        ctkqxn.ToValue = toValue;
                        ctkqxn.FromPercent = fromPercent;
                        ctkqxn.ToPercent = toPercent;
                        ctkqxn.DonVi = donVi;

                        if (ctkqxn.FromValue != null && ctkqxn.FromValue.HasValue)
                            row["FromValue"] = ctkqxn.FromValue.Value;

                        if (ctkqxn.ToValue != null && ctkqxn.ToValue.HasValue)
                            row["ToValue"] = ctkqxn.ToValue.Value;

                        if (ctkqxn.FromPercent != null && ctkqxn.FromValue.HasValue)
                            row["FromPercent"] = ctkqxn.FromPercent.Value;

                        if (ctkqxn.ToPercent != null && ctkqxn.ToValue.HasValue)
                            row["ToPercent"] = ctkqxn.ToPercent.Value;

                        //row["DoiTuong"] = ctkqxn.DoiTuong;
                        row["DonVi"] = ctkqxn.DonVi;
                    }
                }

                db.SubmitChanges();
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

        public static Result InsertKQXN(List<TestResult_CellDyn3200> testResults)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (TestResult_CellDyn3200 testResult in testResults)
                    {
                        PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.FileNum.ToLower().Trim() == testResult.KetQuaXetNghiem.SpecimenID.Trim().ToLower());

                        KetQuaXetNghiem_CellDyn3200 kqxn = db.KetQuaXetNghiem_CellDyn3200s.SingleOrDefault<KetQuaXetNghiem_CellDyn3200>(k => k.SpecimenID.Trim().ToLower() == testResult.KetQuaXetNghiem.SpecimenID.Trim().ToLower() &&
                                k.OperatorID.Trim().ToLower() == testResult.KetQuaXetNghiem.OperatorID.Trim().ToLower() && k.NgayXN == testResult.KetQuaXetNghiem.NgayXN);

                        string ketQuaXetNghiemGUID = string.Empty;
                        if (kqxn == null) //Add New
                        {
                            testResult.KetQuaXetNghiem.KQXN_CellDyn3200GUID = Guid.NewGuid();
                            testResult.KetQuaXetNghiem.CreatedDate = DateTime.Now;
                            if (Global.UserGUID != string.Empty) testResult.KetQuaXetNghiem.CreatedBy = Guid.Parse(Global.UserGUID);
                            if (patient != null) testResult.KetQuaXetNghiem.PatientGUID = patient.PatientGUID;
                            db.KetQuaXetNghiem_CellDyn3200s.InsertOnSubmit(testResult.KetQuaXetNghiem);
                            db.SubmitChanges();

                            ketQuaXetNghiemGUID = testResult.KetQuaXetNghiem.KQXN_CellDyn3200GUID.ToString();
                        }
                        else //Update
                        {
                            kqxn.UpdatedDate = DateTime.Now;
                            if (Global.UserGUID != string.Empty) kqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                            if (patient != null) kqxn.PatientGUID = patient.PatientGUID;
                            kqxn.Status = (byte)Status.Actived;
                            kqxn.MessageType = testResult.KetQuaXetNghiem.MessageType;
                            kqxn.InstrumentType = testResult.KetQuaXetNghiem.InstrumentType;
                            kqxn.SerialNum = testResult.KetQuaXetNghiem.SerialNum;
                            kqxn.SequenceNum = testResult.KetQuaXetNghiem.SequenceNum;
                            kqxn.SpareField = testResult.KetQuaXetNghiem.SpareField;
                            kqxn.SpecimenType = testResult.KetQuaXetNghiem.SpecimenType;
                            kqxn.SpecimenID = testResult.KetQuaXetNghiem.SpecimenID;
                            kqxn.SpecimenName = testResult.KetQuaXetNghiem.SpecimenName;
                            kqxn.PatientID = testResult.KetQuaXetNghiem.PatientID;
                            kqxn.SpecimenSex = testResult.KetQuaXetNghiem.SpecimenSex;
                            kqxn.SpecimenDOB = testResult.KetQuaXetNghiem.SpecimenDOB;
                            kqxn.DrName = testResult.KetQuaXetNghiem.DrName;
                            kqxn.OperatorID = testResult.KetQuaXetNghiem.OperatorID;
                            kqxn.NgayXN = testResult.KetQuaXetNghiem.NgayXN;
                            kqxn.CollectionDate = testResult.KetQuaXetNghiem.CollectionDate;
                            kqxn.CollectionTime = testResult.KetQuaXetNghiem.CollectionTime;
                            kqxn.Comment = testResult.KetQuaXetNghiem.Comment;
                            db.SubmitChanges();
                            ketQuaXetNghiemGUID = kqxn.KQXN_CellDyn3200GUID.ToString();
                        }

                        foreach (ChiTietKetQuaXetNghiem_CellDyn3200 r in testResult.ChiTietKetQuaXetNghiem)
                        {
                            ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.KQXN_CellDyn3200GUID.ToString() == ketQuaXetNghiemGUID &&
                                c.TenXetNghiem.Trim().ToLower() == r.TenXetNghiem.Trim().ToLower());

                            if (ctkqxn == null) //Add New
                            {
                                r.ChiTietKQXN_CellDyn3200GUID = Guid.NewGuid();
                                r.KQXN_CellDyn3200GUID = Guid.Parse(ketQuaXetNghiemGUID);
                                r.CreatedDate = DateTime.Now;
                                if (Global.UserGUID != string.Empty) r.CreatedBy = Guid.Parse(Global.UserGUID);
                                db.ChiTietKetQuaXetNghiem_CellDyn3200s.InsertOnSubmit(r);
                            }
                            else //Update
                            {
                                ctkqxn.UpdatedDate = DateTime.Now;
                                if (Global.UserGUID != string.Empty) ctkqxn.UpdatedBy = Guid.Parse(Global.UserGUID);

                                ctkqxn.TenXetNghiem = r.TenXetNghiem;
                                ctkqxn.TestResult = r.TestResult;
                                ctkqxn.TestPercent = r.TestPercent;
                                ctkqxn.TinhTrang = (byte)TinhTrang.BinhThuong;
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

        public static Result UpdatePatient(KetQuaXetNghiem_CellDyn3200 ketQuaXetNghiem)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    KetQuaXetNghiem_CellDyn3200 kqxn = db.KetQuaXetNghiem_CellDyn3200s.SingleOrDefault<KetQuaXetNghiem_CellDyn3200>(k => k.KQXN_CellDyn3200GUID == ketQuaXetNghiem.KQXN_CellDyn3200GUID &&
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

                        desc += string.Format("GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}', Ngày xét nghiệm: '{3}'",
                                kqxn.KQXN_CellDyn3200GUID.ToString(), fileNum, tenBenhNhan, kqxn.NgayXN.Value.ToString("dd/MM/yyyy HH:mm:ss"));

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Cập nhật bệnh nhân xét nghiệm celldyn 3200";
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
                        KetQuaXetNghiem_CellDyn3200 kqxn = db.KetQuaXetNghiem_CellDyn3200s.SingleOrDefault<KetQuaXetNghiem_CellDyn3200>(k => k.KQXN_CellDyn3200GUID.ToString() == key);

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

                            desc += string.Format("- GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}', Ngày xét nghiệm: '{3}'\n",
                                    kqxn.KQXN_CellDyn3200GUID.ToString(), fileNum, tenBenhNhan, kqxn.NgayXN.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa xét nghiệm celldyn 3200";
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
                        ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.ChiTietKQXN_CellDyn3200GUID.ToString() == key);
                        if (ctkqxn != null)
                        {
                            ctkqxn.DeletedDate = DateTime.Now;
                            ctkqxn.DeletedBy = Guid.Parse(Global.UserGUID);
                            ctkqxn.Status = (byte)Status.Deactived;

                            KetQuaXetNghiem_CellDyn3200 kqxn = ctkqxn.KetQuaXetNghiem_CellDyn3200;
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

                            desc += string.Format("- GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}', Ngày xét nghiệm: '{3}', Tên xét nghiệm: '{4}', Kết quả: '{5}', % kết quả: '{6}'\n",
                                    kqxn.KQXN_CellDyn3200GUID.ToString(), fileNum, tenBenhNhan, kqxn.NgayXN.Value.ToString("dd/MM/yyyy HH:mm:ss"), ctkqxn.TenXetNghiem, ctkqxn.TestResult, ctkqxn.TestPercent);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa chi tiết kết quả xét nghiệm celldyn 3200";
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

        public static Result UpdateChiSoKetQuaXetNghiem(ChiTietKetQuaXetNghiem_CellDyn3200 chiTietKQXN, ref string binhThuong, ref string percent)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.ChiTietKQXN_CellDyn3200GUID == chiTietKQXN.ChiTietKQXN_CellDyn3200GUID);
                    if (ctkqxn != null)
                    {
                        ctkqxn.UpdatedDate = DateTime.Now;
                        ctkqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                        ctkqxn.Status = (byte)Status.Actived;
                        ctkqxn.TestResult = chiTietKQXN.TestResult;
                        ctkqxn.TestPercent = chiTietKQXN.TestPercent;
                        ctkqxn.FromValue = chiTietKQXN.FromValue;
                        ctkqxn.ToValue = chiTietKQXN.ToValue;
                        ctkqxn.FromPercent = chiTietKQXN.FromPercent;
                        ctkqxn.ToPercent = chiTietKQXN.ToPercent;
                        ctkqxn.DonVi = chiTietKQXN.DonVi;

                        KetQuaXetNghiem_CellDyn3200 kqxn = ctkqxn.KetQuaXetNghiem_CellDyn3200;
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
                        double? fromValue = null;
                        double? toValue = null;
                        double? fromPercent = null;
                        double? toPercent = null;
                        string donVi = string.Empty;

                        if (ctkqxn.FromValue.HasValue)
                            fromValue = ctkqxn.FromValue.Value;

                        if (ctkqxn.ToValue.HasValue)
                            toValue = ctkqxn.ToValue.Value;

                        if (ctkqxn.FromPercent.HasValue)
                            fromPercent = ctkqxn.FromPercent.Value;

                        if (ctkqxn.ToPercent.HasValue)
                            toPercent = ctkqxn.ToPercent.Value;

                        if (ctkqxn.DonVi != null && ctkqxn.DonVi != string.Empty)
                            donVi = ctkqxn.DonVi;

                        double testResult = ctkqxn.TestResult.Value;
                        TinhTrang tinhTrang = TinhTrang.BinhThuong;

                        if (fromValue != null && toValue != null)
                        {
                            if (fromPercent != null || toPercent != null)
                                binhThuong = string.Format("({0:F2} - {1:F2})", fromValue.Value, toValue.Value);
                            else
                                binhThuong = string.Format("({0:F2} - {1:F2} {2})", fromValue.Value, toValue.Value, donVi);

                            if (testResult < fromValue.Value || testResult > toValue.Value)
                                tinhTrang = TinhTrang.BatThuong;
                        }
                        else if (fromValue != null)
                        {
                            if (fromPercent != null || toPercent != null)
                                binhThuong = string.Format("(> {0:F2})", fromValue.Value);
                            else
                                binhThuong = string.Format("(> {0:F2} {1})", fromValue.Value, donVi);

                            if (testResult <= fromValue.Value)
                                tinhTrang = TinhTrang.BatThuong;
                        }
                        else
                        {
                            if (fromPercent != null || toPercent != null)
                                binhThuong = string.Format("(< {0:F2})", toValue.Value);
                            else
                                binhThuong = string.Format("(< {0:F2} {1})", toValue.Value, donVi);

                            if (testResult >= toValue.Value)
                                tinhTrang = TinhTrang.BatThuong;
                        }

                        if (fromPercent != null && toPercent != null)
                        {
                            double testPercent = ctkqxn.TestPercent.Value;
                            percent = string.Format("{0:F2}% ({1:F2} - {2:F2} {3})", testPercent, fromPercent.Value, toPercent.Value, donVi);

                            if (tinhTrang == TinhTrang.BinhThuong)
                            {
                                if (testPercent < fromPercent.Value || testPercent > toPercent.Value)
                                    tinhTrang = TinhTrang.BatThuong;
                            }
                        }
                        else if (fromPercent != null)
                        {
                            double testPercent = ctkqxn.TestPercent.Value;
                            percent = string.Format("{0:F2}% (> {1:F2} {2})", testPercent, fromPercent.Value, donVi);

                            if (tinhTrang == TinhTrang.BinhThuong)
                            {
                                if (testPercent <= fromPercent.Value)
                                    tinhTrang = TinhTrang.BatThuong;
                            }
                        }
                        else if (toPercent != null)
                        {
                            double testPercent = ctkqxn.TestPercent.Value;
                            percent = string.Format("{0:F2}% (< {1:F2} {2})", testPercent, toPercent.Value, donVi);

                            if (tinhTrang == TinhTrang.BinhThuong)
                            {
                                if (testPercent >= toPercent.Value)
                                    tinhTrang = TinhTrang.BatThuong;
                            }
                        }

                        chiTietKQXN.TinhTrang = (byte)tinhTrang;

                        //

                        desc += string.Format("- GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}', Ngày xét nghiệm: '{3}', Tên xét nghiệm: '{4}', Kết quả: '{5}', % kết quả: '{6}'",
                                    kqxn.KQXN_CellDyn3200GUID.ToString(), fileNum, tenBenhNhan, kqxn.NgayXN.Value.ToString("dd/MM/yyyy HH:mm:ss"), ctkqxn.TenXetNghiem, ctkqxn.TestResult, ctkqxn.TestPercent);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Cập nhật chỉ số kết quả xét nghiệm celldyn 3200";
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

        public static Result UpdateXetNghiem(XetNghiem_CellDyn3200 xetNghiem)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    XetNghiem_CellDyn3200 xn = db.XetNghiem_CellDyn3200s.SingleOrDefault<XetNghiem_CellDyn3200>(x => x.XetNghiemGUID == xetNghiem.XetNghiemGUID);
                    if (xn != null)
                    {
                        xn.UpdatedDate = xetNghiem.UpdatedDate;
                        xn.UpdatedBy = xetNghiem.UpdatedBy;
                        xn.FromValue = xetNghiem.FromValue;
                        xn.ToValue = xetNghiem.ToValue;
                        xn.FromPercent = xetNghiem.FromPercent;
                        xn.ToPercent = xetNghiem.ToPercent;
                        xn.Status = (byte)Status.Actived;

                        desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}'", xn.XetNghiemGUID.ToString(), xn.FullName);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Cập nhật xét nghiệm CellDyn3200";
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
                        ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.ChiTietKQXN_CellDyn3200GUID.ToString() == key);
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

        public static Result UpdateDaUpload(List<string> keys)
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
                        ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.ChiTietKQXN_CellDyn3200GUID.ToString() == key);
                        if (ctkqxn != null)
                        {
                            ctkqxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                            ctkqxn.UpdatedDate = DateTime.Now;
                            ctkqxn.DaUpload = true;
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
