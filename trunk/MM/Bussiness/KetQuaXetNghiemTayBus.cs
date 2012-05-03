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
    public class KetQuaXetNghiemTayBus : BusBase
    {
        public static Result GetKetQuaXetNghiemList(DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (tenBenhNhan.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_ManualView WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} ORDER BY NgayXN DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_ManualView WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} AND FullName LIKE N'%{3}%' ORDER BY NgayXN DESC",
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

        public static Result SetTinhTrangXetNghiem(DataRow drXetNghiem, DataRow row)
        {
            Result result = new Result();

            try
            {
                MMOverride db = new MMOverride();
                string xetNghiem_ManualGUID = row["XetNghiem_ManualGUID"].ToString();
                XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID.ToString() == xetNghiem_ManualGUID);
                if (xn == null) return result;
                List<ChiTietXetNghiem_Manual> ctxns = xn.ChiTietXetNghiem_Manuals.ToList<ChiTietXetNghiem_Manual>();
                if (ctxns.Count <= 0) return result;

                double testResult = 0;
                ChiTietXetNghiem_Manual ctxn = null;

                row["TinhTrang"] = (byte)TinhTrang.BinhThuong;
                row["BinhThuong"] = string.Empty;

                try
                {
                    testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                    if (ctxns[0].DoiTuong == (byte)DoiTuong.Chung) ctxn = ctxns[0];
                    else if (ctxns[0].DoiTuong == (byte)DoiTuong.Nam || ctxns[0].DoiTuong == (byte)DoiTuong.Nu)
                    {
                        if (drXetNghiem["GenderAsStr"] == null || drXetNghiem["GenderAsStr"] == DBNull.Value) return result;
                        if (drXetNghiem["GenderAsStr"].ToString().ToLower() != "nam" && drXetNghiem["GenderAsStr"].ToString().ToLower() != "nữ") return result;

                        if (drXetNghiem["GenderAsStr"].ToString().ToLower() == "nam")
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
                        //KetQuaXetNghiem_ManualView kqxn = db.KetQuaXetNghiem_ManualViews.SingleOrDefault<KetQuaXetNghiem_ManualView>(k => k.KetQuaXetNghiemManualGUID.ToString() == ketQuaXetNghiemGUID);
                        //if (kqxn == null) continue;
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
                catch
                {
                        
                }
                
                if (ctxn == null) return result;

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
                        case DoiTuong.TreEm:
                            row["BinhThuong"] = string.Format("Child ({0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
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
                        case DoiTuong.TreEm:
                            row["BinhThuong"] = string.Format("Child (> {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
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
                            row["BinhThuong"] = string.Format("< 60 year (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                            break;
                        case DoiTuong.TreEm:
                            row["BinhThuong"] = string.Format("Child (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                            break;
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

        public static Result GetChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID)
        {
            Result result = new Result();

            try
            {
                string query = query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST('' AS nvarchar(50)) AS BinhThuong FROM ChiTietKetQuaXetNghiem_ManualView WHERE KetQuaXetNghiem_ManualGUID = '{0}' AND Status = {1} ORDER BY Fullname",
                           ketQuaXetNghiemGUID, (byte)Status.Actived);

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                MMOverride db = new MMOverride();
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    string xetNghiem_ManualGUID = row["XetNghiem_ManualGUID"].ToString();

                    XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID.ToString() == xetNghiem_ManualGUID);
                    if (xn == null) continue;
                    List<ChiTietXetNghiem_Manual> ctxns = xn.ChiTietXetNghiem_Manuals.ToList<ChiTietXetNghiem_Manual>();
                    if (ctxns.Count <= 0) continue;

                    try
                    {
                        double testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                        ChiTietXetNghiem_Manual ctxn = null;
                        if (ctxns[0].DoiTuong == (byte)DoiTuong.Chung) ctxn = ctxns[0];
                        else if (ctxns[0].DoiTuong == (byte)DoiTuong.Nam || ctxns[0].DoiTuong == (byte)DoiTuong.Nu)
                        {
                            KetQuaXetNghiem_ManualView kqxn = db.KetQuaXetNghiem_ManualViews.SingleOrDefault<KetQuaXetNghiem_ManualView>(k => k.KetQuaXetNghiemManualGUID.ToString() == ketQuaXetNghiemGUID);
                            if (kqxn == null) continue;
                            if (kqxn.GenderAsStr.ToLower() != "nam" && kqxn.GenderAsStr.ToLower() != "nữ") continue;

                            if (kqxn.GenderAsStr.ToLower() == "nam")
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
                            //KetQuaXetNghiem_ManualView kqxn = db.KetQuaXetNghiem_ManualViews.SingleOrDefault<KetQuaXetNghiem_ManualView>(k => k.KetQuaXetNghiemManualGUID.ToString() == ketQuaXetNghiemGUID);
                            //if (kqxn == null) continue;
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
                                case DoiTuong.TreEm:
                                    row["BinhThuong"] = string.Format("Child ({0} - {1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
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
                                case DoiTuong.TreEm:
                                    row["BinhThuong"] = string.Format("Child (> {0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
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
                                    row["BinhThuong"] = string.Format("< 60 year (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                    row["BinhThuong"] = string.Format("Child (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                            }
                        }
                    }
                    catch {}
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
                        KetQuaXetNghiem_Manual kqxn = db.KetQuaXetNghiem_Manuals.SingleOrDefault<KetQuaXetNghiem_Manual>(k => k.KetQuaXetNghiemManualGUID.ToString() == key);

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
                                    kqxn.KetQuaXetNghiemManualGUID.ToString(), fileNum, tenBenhNhan, kqxn.NgayXN.ToString("dd/MM/yyyy HH:mm:ss"));
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa xét nghiệm tay";
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

        public static Result InsertKQXN(KetQuaXetNghiem_Manual kqxn, List<ChiTietKetQuaXetNghiem_Manual> ctkqxns)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (kqxn.KetQuaXetNghiemManualGUID == null || kqxn.KetQuaXetNghiemManualGUID == Guid.Empty)
                    {
                        kqxn.KetQuaXetNghiemManualGUID = Guid.NewGuid();
                        db.KetQuaXetNghiem_Manuals.InsertOnSubmit(kqxn);
                        db.SubmitChanges();

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

                        desc += string.Format("- Kết quả xét nghiệm tay: GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}', Ngày xét nghiệm: '{3}'\n",
                                    kqxn.KetQuaXetNghiemManualGUID.ToString(), fileNum, tenBenhNhan, kqxn.NgayXN.ToString("dd/MM/yyyy HH:mm:ss"));

                        desc += "- Chi tiết kết quả xét nghiệm tay:\n";
                        //Chi tiết
                        foreach (ChiTietKetQuaXetNghiem_Manual ct in ctkqxns)
                        {
                            ct.ChiTietKetQuaXetNghiem_ManualGUID = Guid.NewGuid();
                            ct.KetQuaXetNghiem_ManualGUID = kqxn.KetQuaXetNghiemManualGUID;
                            ct.CreatedDate = DateTime.Now;
                            ct.CreatedBy = Guid.Parse(Global.UserGUID);
                            ct.Status = (byte)Status.Actived;
                            db.ChiTietKetQuaXetNghiem_Manuals.InsertOnSubmit(ct);
                            db.SubmitChanges();

                            desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}\n", ct.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                                ct.XetNghiem_Manual.Fullname, ct.TestResult);
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm kết quả xét nghiệm tay";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else
                    {
                        KetQuaXetNghiem_Manual ketQuaXN = db.KetQuaXetNghiem_Manuals.SingleOrDefault<KetQuaXetNghiem_Manual>(k => k.KetQuaXetNghiemManualGUID == kqxn.KetQuaXetNghiemManualGUID);
                        if (ketQuaXN != null)
                        {
                            ketQuaXN.NgayXN = kqxn.NgayXN;
                            ketQuaXN.PatientGUID = kqxn.PatientGUID;
                            ketQuaXN.UpdatedDate = DateTime.Now;
                            ketQuaXN.UpdatedBy = Guid.Parse(Global.UserGUID);
                            ketQuaXN.Status = (byte)Status.Actived;

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

                            desc += string.Format("- Kết quả xét nghiệm tay: GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}', Ngày xét nghiệm: '{3}'\n",
                                    ketQuaXN.KetQuaXetNghiemManualGUID.ToString(), fileNum, tenBenhNhan, ketQuaXN.NgayXN.ToString("dd/MM/yyyy HH:mm:ss"));

                            desc += "- Chi tiết kết quả xét nghiệm tay:\n";
                            //Chi tiết
                            var chiTietKQXNs = ketQuaXN.ChiTietKetQuaXetNghiem_Manuals;
                            foreach (ChiTietKetQuaXetNghiem_Manual ct in chiTietKQXNs)
                            {
                                ct.UpdatedDate = DateTime.Now;
                                ct.UpdatedBy = Guid.Parse(Global.UserGUID);
                                ct.Status = (byte)Status.Deactived;
                            }

                            db.SubmitChanges();

                            foreach (ChiTietKetQuaXetNghiem_Manual ct in ctkqxns)
                            {
                                ChiTietKetQuaXetNghiem_Manual chiTietKQXN = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(c => c.KetQuaXetNghiem_ManualGUID == ketQuaXN.KetQuaXetNghiemManualGUID &&
                                    c.XetNghiem_ManualGUID == ct.XetNghiem_ManualGUID);
                                if (chiTietKQXN == null)
                                {
                                    ct.ChiTietKetQuaXetNghiem_ManualGUID = Guid.NewGuid();
                                    ct.KetQuaXetNghiem_ManualGUID = ketQuaXN.KetQuaXetNghiemManualGUID;
                                    ct.CreatedDate = DateTime.Now;
                                    ct.CreatedBy = Guid.Parse(Global.UserGUID);
                                    ct.Status = (byte)Status.Actived;
                                    db.ChiTietKetQuaXetNghiem_Manuals.InsertOnSubmit(ct);
                                    db.SubmitChanges();

                                    desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}\n", ct.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                                        ct.XetNghiem_Manual.Fullname, ct.TestResult);
                                }
                                else
                                {
                                    chiTietKQXN.TestResult = ct.TestResult;
                                    chiTietKQXN.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    chiTietKQXN.UpdatedDate = DateTime.Now;
                                    chiTietKQXN.Status = (byte)Status.Actived;

                                    desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}\n", chiTietKQXN.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                                        chiTietKQXN.XetNghiem_Manual.Fullname, chiTietKQXN.TestResult);
                                }
                            }

                            //Tracking
                            desc = desc.Substring(0, desc.Length - 1);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa kết quả xét nghiệm tay";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
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
