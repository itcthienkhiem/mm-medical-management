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
    public class KetQuaXetNghiemTongHopBus : BusBase
    {
        public static Result GetDanhSachBenhNhanXetNghiemList(DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = new Result();
            DataTable dt = null;

            try
            {
                string query = string.Empty;

                //Hitachi917
                query = string.Format("SELECT DISTINCT PatientGUID, FullName, DobStr, GenderAsStr FROM KetQuaXetNghiem_Hitachi917View WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FullName LIKE '%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;
                dt = result.QueryResult as DataTable;

                //CellDyn3200
                query = string.Format("SELECT DISTINCT PatientGUID, FullName, DobStr, GenderAsStr FROM KetQuaXetNghiem_CellDyn3200View WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FullName LIKE '%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtCellDyn3200 = result.QueryResult as DataTable;
                foreach (DataRow row in dtCellDyn3200.Rows)
                {
                    DataRow[] rows = dt.Select(string.Format("PatientGUID = '{0}'", row["PatientGUID"].ToString()));
                    if (rows != null && rows.Length > 0) continue;

                    dt.ImportRow(row);
                }

                //Xet nghiem tay
                query = string.Format("SELECT DISTINCT PatientGUID, FullName, DobStr, GenderAsStr FROM KetQuaXetNghiem_ManualView WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FullName LIKE '%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtXetNghiemTay = result.QueryResult as DataTable;
                foreach (DataRow row in dtXetNghiemTay.Rows)
                {
                    DataRow[] rows = dt.Select(string.Format("PatientGUID = '{0}'", row["PatientGUID"].ToString()));
                    if (rows != null && rows.Length > 0) continue;

                    dt.ImportRow(row);
                }

                result.QueryResult = dt;
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

        public static Result GetKetQuaXetNghiemTongHopList(DateTime fromDate, DateTime toDate, string patientGUID, string ngaySinh, string gioiTinh)
        {
            Result result = new Result();
            MMOverride db = new MMOverride();

            try
            {
                string query = string.Empty;
                DataTable dt = null;

                //Hitachi917
                query = string.Format("SELECT XetNghiemGUID, NgayXN, Fullname, TestResult, '' AS TestPercent, TinhTrang, '' AS BinhThuong, [Type] FROM dbo.ChiTietKetQuaXetNghiem_Hitachi917View WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY NgayXN, Fullname",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtHitachi917 = result.QueryResult as DataTable;
                foreach (DataRow row in dtHitachi917.Rows)
                {
                    string tenXetNghiem = row["Fullname"].ToString();
                    string xetNghiemGUID = row["XetNghiemGUID"].ToString();
                    DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);
                    
                    List<ChiTietXetNghiem_Hitachi917> ctxns = (from ct in db.ChiTietXetNghiem_Hitachi917s
                                                               where ct.XetNghiemGUID.ToString() == xetNghiemGUID
                                                               select ct).ToList();
                    if (ctxns == null || ctxns.Count <= 0) continue;

                    double testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                    ChiTietXetNghiem_Hitachi917 ctxn = null;
                    if (tenXetNghiem == "Glucose")
                    {
                        if (ngayXN.Hour < 14)
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
                            if (gioiTinh.ToLower() != "nam" && gioiTinh.ToLower() != "nữ") continue;

                            if (gioiTinh.ToLower() == "nam")
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
                            //KetQuaXetNghiem_Hitachi917 kqxn = db.KetQuaXetNghiem_Hitachi917s.SingleOrDefault<KetQuaXetNghiem_Hitachi917>(k => k.KQXN_Hitachi917GUID.ToString() == ketQuaXetNghiemGUID);
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
                                row["BinhThuong"] = string.Format("< 60 year (< {0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                break;
                        }
                    }
                }

                dt = dtHitachi917;

                //Celldyn3200
                string emptyGUID = Guid.Empty.ToString();
                query = string.Format("SELECT '{4}' AS XetNghiemGUID, NgayXN, Fullname, TestResult, TestPercent, TinhTrang, '' AS BinhThuong, [Type] FROM dbo.ChiTietKetQuaXetNghiem_CellDyn3200View WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY GroupID, [Order], NgayXN",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"), emptyGUID);

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtCellDyn3200 = result.QueryResult as DataTable;
                foreach (DataRow row in dtCellDyn3200.Rows)
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

                    dt.ImportRow(row);
                }



                //Xet nghiem tay
                query = string.Format("SELECT XetNghiem_ManualGUID AS XetNghiemGUID, NgayXN, Fullname, TestResult, '' AS TestPercent, TinhTrang, '' AS BinhThuong, [Type] FROM dbo.ChiTietKetQuaXetNghiem_ManualView WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY NgayXN, Fullname",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtManual = result.QueryResult as DataTable;
                foreach (DataRow row in dtManual.Rows)
                {
                    string xetNghiem_ManualGUID = row["XetNghiemGUID"].ToString();

                    XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID.ToString() == xetNghiem_ManualGUID);
                    if (xn == null) continue;
                    List<ChiTietXetNghiem_Manual> ctxns = xn.ChiTietXetNghiem_Manuals.ToList<ChiTietXetNghiem_Manual>();
                    if (ctxns.Count <= 0) continue;

                    double testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                    ChiTietXetNghiem_Manual ctxn = null;
                    if (ctxns[0].DoiTuong == (byte)DoiTuong.Chung) ctxn = ctxns[0];
                    else if (ctxns[0].DoiTuong == (byte)DoiTuong.Nam || ctxns[0].DoiTuong == (byte)DoiTuong.Nu)
                    {
                        if (gioiTinh.ToLower() != "nam" && gioiTinh.ToLower() != "nữ") continue;

                        if (gioiTinh.ToLower() == "nam")
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

                    dt.ImportRow(row);
                }

                result.QueryResult = dt;
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
