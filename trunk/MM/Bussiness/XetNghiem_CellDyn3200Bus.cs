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
        public static Result GetKetQuaXetNghiemList(DateTime fromDate, DateTime toDate, string tenBenhNhan)
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
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_CellDyn3200View WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} AND FullName LIKE N'%{3}%' ORDER BY NgayXN DESC",
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

        public static Result GetChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID)
        {
            Result result = new Result();

            try
            {
                string query = query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST('' AS nvarchar(50)) AS BinhThuong FROM ChiTietKetQuaXetNghiem_CellDyn3200View WHERE KQXN_CellDyn3200GUID = '{0}' AND Status = {1} ORDER BY GroupID, [Order]",
                           ketQuaXetNghiemGUID, (byte)Status.Actived);

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                MMOverride db = new MMOverride();
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    string tenXetNghiem = row["Fullname"].ToString();

                    XetNghiem_CellDyn3200 xn = db.XetNghiem_CellDyn3200s.SingleOrDefault<XetNghiem_CellDyn3200>(x => x.TenXetNghiem == tenXetNghiem);
                    if (xn == null) continue;

                    string donVi = string.Empty;
                    if (xn.DonVi != null && xn.DonVi != string.Empty) donVi = xn.DonVi;
                    double testResult = Convert.ToDouble(row["TestResult"]);

                    if (xn.FromPercent.HasValue)
                    {
                        row["BinhThuong"] = string.Format("({0:F2} - {1:F2})  ({2:F2} - {3:F2} {4})", xn.FromValue.Value, xn.ToValue.Value, xn.FromPercent.Value, xn.ToPercent.Value, donVi);
                        double testPercent = Convert.ToDouble(row["TestPercent"]);

                        if (testResult < xn.FromValue.Value || testResult > xn.ToValue.Value)
                            row["TinhTrang"] = (byte)TinhTrang.BatThuong;

                        if (testPercent < xn.FromPercent.Value || testPercent > xn.ToPercent.Value)
                            row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                    }
                    else
                    {
                        row["BinhThuong"] = string.Format("({0:F2} - {1:F2} {2})", xn.FromValue.Value, xn.ToValue.Value, donVi);

                        if (testResult < xn.FromValue.Value || testResult > xn.ToValue.Value)
                            row["TinhTrang"] = (byte)TinhTrang.BatThuong;
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

        public static Result UpdateChiSoKetQuaXetNghiem(ChiTietKetQuaXetNghiem_CellDyn3200 chiTietKQXN)
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
                        for (int i = 0; i < 1; i++)
                        {
                            XetNghiem_CellDyn3200 xn = db.XetNghiem_CellDyn3200s.SingleOrDefault<XetNghiem_CellDyn3200>(x => x.TenXetNghiem.Trim().ToLower() == ctkqxn.TenXetNghiem.Trim().ToLower());
                            if (xn == null) continue;

                            double testResult = ctkqxn.TestResult.Value;

                            if (xn.FromPercent.HasValue)
                            {
                                double testPercent = ctkqxn.TestPercent.Value;

                                if (testResult < xn.FromValue.Value || testResult > xn.ToValue.Value)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;

                                if (testPercent < xn.FromPercent.Value || testPercent > xn.ToPercent.Value)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                            }
                            else
                            {
                                if (testResult < xn.FromValue.Value || testResult > xn.ToValue.Value)
                                    chiTietKQXN.TinhTrang = (byte)TinhTrang.BatThuong;
                            }
                        }
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
    }
}
