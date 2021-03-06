/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
        public static Result GetNhomXetNghiemList()
        {
            Result result = new Result();

            try
            {
                string query = "SELECT DISTINCT CAST(1 AS Bit) AS Checked, GroupName, [Type], GroupID FROM XetNghiem_Manual ORDER BY GroupID";
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = true;
                newRow["GroupName"] = "CÔNG THỨC MÁU (MÁY CELLDYN 3200)";
                newRow["Type"] = "Haematology";
                newRow["GroupID"] = 0;
                dt.Rows.InsertAt(newRow, 0);

                int index = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Type"].ToString() == "Biochemistry")
                    {
                        newRow = dt.NewRow();
                        newRow["Checked"] = true;
                        newRow["GroupName"] = "SINH HÓA (MÁY HITACHI 917)";
                        newRow["Type"] = "Biochemistry";
                        newRow["GroupID"] = 0;
                        dt.Rows.InsertAt(newRow, index);
                        break;
                    }

                    index++;
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

        public static Result GetDanhSachBenhNhanXetNghiemList(DateTime fromDate, DateTime toDate, string tenBenhNhan, bool isMaBenhNhan)
        {
            Result result = new Result();
            DataTable dt = null;

            try
            {
                string query = string.Empty;

                //Hitachi917
                if (!isMaBenhNhan)
                {
                    query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_Hitachi917View WITH(NOLOCK) WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FullName LIKE N'%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }
                else
                {
                    query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_Hitachi917View WITH(NOLOCK) WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FileNum LIKE N'%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;
                dt = result.QueryResult as DataTable;

                //CellDyn3200
                if (!isMaBenhNhan)
                {
                    query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_CellDyn3200View WITH(NOLOCK) WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FullName LIKE N'%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }
                else
                {
                    query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_CellDyn3200View WITH(NOLOCK) WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FileNum LIKE N'%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }

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
                if (!isMaBenhNhan)
                {
                    //query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_ManualView WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FullName LIKE N'%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    //tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                    query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, V.PatientGUID, V.FileNum, V.FullName, V.DobStr, V.GenderAsStr, V.Address FROM KetQuaXetNghiem_ManualView V WITH(NOLOCK), ChiTietKetQuaXetNghiem_Manual C WITH(NOLOCK) WHERE V.KetQuaXetNghiemManualGUID = C.KetQuaXetNghiem_ManualGUID AND V.Archived = 'False' AND C.NgayXetNghiem BETWEEN '{0}' AND '{1}' AND V.Status = {2} AND C.Status = {2} AND V.FullName LIKE N'%{3}%'",
                           fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"), (byte)Status.Actived, tenBenhNhan);
                }
                else
                {
                    //query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_ManualView WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND FileNum LIKE N'%{0}%' AND NgayXN BETWEEN '{1}' AND '{2}'",
                    //tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                    query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, V.PatientGUID, V.FileNum, V.FullName, V.DobStr, V.GenderAsStr, V.Address FROM KetQuaXetNghiem_ManualView V WITH(NOLOCK), ChiTietKetQuaXetNghiem_Manual C WITH(NOLOCK) WHERE V.KetQuaXetNghiemManualGUID = C.KetQuaXetNghiem_ManualGUID AND V.Archived = 'False' AND C.NgayXetNghiem BETWEEN '{0}' AND '{1}' AND V.Status = {2} AND C.Status = {2} AND V.FileNum LIKE N'%{3}%'",
                           fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"), (byte)Status.Actived, tenBenhNhan);
                }

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtXetNghiemTay = result.QueryResult as DataTable;
                foreach (DataRow row in dtXetNghiemTay.Rows)
                {
                    DataRow[] rows = dt.Select(string.Format("PatientGUID = '{0}'", row["PatientGUID"].ToString()));
                    if (rows != null && rows.Length > 0) continue;

                    dt.ImportRow(row);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string maBenhNhan = row["FileNum"].ToString();
                        string maCongTy = Utility.GetMaCongTy(maBenhNhan);
                        result = DiaChiCongTyBus.GetDiaChiCongTy(maCongTy);
                        if (!result.IsOK) return result;

                        row["Address"] = result.QueryResult.ToString();
                    }
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

        public static Result GetDanhSachBenhNhanXetNghiemCellDyn3200List(DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            DataTable dt = null;

            try
            {
                string query = string.Empty;

                //CellDyn3200
                query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_CellDyn3200View WITH(NOLOCK) WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND NgayXN BETWEEN '{0}' AND '{1}'",
                fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;
                               
                dt = result.QueryResult as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string maBenhNhan = row["FileNum"].ToString();
                        string maCongTy = Utility.GetMaCongTy(maBenhNhan);
                        result = DiaChiCongTyBus.GetDiaChiCongTy(maCongTy);
                        if (!result.IsOK) return result;

                        row["Address"] = result.QueryResult.ToString();
                    }
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

        public static Result GetDanhSachBenhNhanXetNghiemSinhHoaList(DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            DataTable dt = null;

            try
            {
                string query = string.Empty;

                //Hitachi917
                query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_Hitachi917View WITH(NOLOCK) WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND NgayXN BETWEEN '{0}' AND '{1}'",
                fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;
                dt = result.QueryResult as DataTable;

                //Xet nghiem tay
                query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, PatientGUID, FileNum, FullName, DobStr, GenderAsStr, Address FROM KetQuaXetNghiem_ManualView WITH(NOLOCK) WHERE Status = 0 AND Archived = 'False' AND PatientGUID IS NOT NULL AND NgayXN BETWEEN '{0}' AND '{1}'",
                fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtXetNghiemTay = result.QueryResult as DataTable;
                foreach (DataRow row in dtXetNghiemTay.Rows)
                {
                    DataRow[] rows = dt.Select(string.Format("PatientGUID = '{0}'", row["PatientGUID"].ToString()));
                    if (rows != null && rows.Length > 0) continue;

                    dt.ImportRow(row);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string maBenhNhan = row["FileNum"].ToString();
                        string maCongTy = Utility.GetMaCongTy(maBenhNhan);
                        result = DiaChiCongTyBus.GetDiaChiCongTy(maCongTy);
                        if (!result.IsOK) return result;

                        row["Address"] = result.QueryResult.ToString();
                    }
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

        private static ChiTietXetNghiem_Hitachi917 GetChiTietXetNghiem(List<ChiTietXetNghiem_Hitachi917> ctxns, DoiTuong doiTuong)
        {
            foreach (ChiTietXetNghiem_Hitachi917 ctxn in ctxns)
            {
                if (ctxn.DoiTuong == (int)doiTuong)
                    return ctxn;
            }

            return null;
        }

        public static Result GetKetQuaXetNghiemTongHopList(DateTime fromDate, DateTime toDate, string patientGUID, string ngaySinh, string gioiTinh)
        {
            Result result = new Result();
            MMOverride db = new MMOverride();

            try
            {
                string query = string.Empty;
                DataTable dt = null;

                //Celldyn3200
                string emptyGUID = Guid.Empty.ToString();
                query = string.Format("SELECT CAST(0 AS Bit) AS Checked, ChiTietKQXN_CellDyn3200GUID AS ChiTietKQXNGUID, '{4}' AS XetNghiemGUID, KQXN_CellDyn3200GUID AS KetQuaXetNghiemGUID, NgayXN, '' AS NgayXN2, Fullname, N'CÔNG THỨC MÁU (MÁY CELLDYN 3200)' AS GroupName, CAST(TestResult AS nvarchar(50)) AS TestResult, TestPercent, TinhTrang, '' AS BinhThuong, [Type], DaIn, FromValue2 AS FromValue, ToValue2 AS ToValue, '' AS FromOperator, '' AS ToOperator, 0 AS FromAge, 0 AS ToAge, 0 AS FromTime, 0 AS ToTime, '' AS FromTimeOperator, '' AS ToTimeOperator, 0 AS XValue, CAST(0 AS Bit) AS HasHutThuoc, DoiTuong2 AS DoiTuong, DonVi2 AS DonVi, FromPercent2 AS FromPercent, ToPercent2 AS ToPercent, GroupID, [Order], 'CellDyn3200' AS LoaiXN, 0 AS TestNum, DaUpload, LamThem, 1 AS ThuTu FROM dbo.ChiTietKetQuaXetNghiem_CellDyn3200View WITH(NOLOCK) WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY GroupID, [Order], NgayXN",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"), emptyGUID);

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtCellDyn3200 = result.QueryResult as DataTable;
                foreach (DataRow row in dtCellDyn3200.Rows)
                {
                    double? fromValue = null;
                    double? toValue = null;
                    //double? fromPercent = null;
                    //double? toPercent = null;
                    string donVi = string.Empty;

                    if ((row["FromValue"] != null && row["FromValue"] != DBNull.Value) ||
                        (row["ToValue"] != null && row["ToValue"] != DBNull.Value))
                    {
                        if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                            fromValue = Convert.ToDouble(row["FromValue"]);

                        if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                            toValue = Convert.ToDouble(row["ToValue"]);

                        //if (row["FromPercent"] != null && row["FromPercent"] != DBNull.Value)
                        //    fromPercent = Convert.ToDouble(row["FromPercent"]);

                        //if (row["ToPercent"] != null && row["ToPercent"] != DBNull.Value)
                        //    toPercent = Convert.ToDouble(row["ToPercent"]);

                        if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                            donVi = row["DonVi"].ToString().Trim();
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

                        //if (xn.FromPercent.HasValue)
                        //    fromPercent = xn.FromPercent.Value;

                        //if (xn.ToPercent.HasValue)
                        //    toPercent = xn.ToPercent.Value;

                        if (xn.DonVi != null && xn.DonVi != string.Empty)
                            donVi = xn.DonVi;
                    }

                    double testResult = Convert.ToDouble(row["TestResult"]);
                    TinhTrang tinhTrang = TinhTrang.BinhThuong;

                    if (fromValue != null && toValue != null)
                    {
                        //if (fromPercent != null || toPercent != null)
                        //    row["BinhThuong"] = string.Format("({0:F2} - {1:F2})", fromValue.Value, toValue.Value);
                        //else
                        if (donVi !=string.Empty)
                            row["BinhThuong"] = string.Format("({0:F2}-{1:F2} {2})", fromValue.Value, toValue.Value, donVi);
                        else
                            row["BinhThuong"] = string.Format("({0:F2}-{1:F2})", fromValue.Value, toValue.Value);

                        if (testResult < fromValue.Value || testResult > toValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }
                    else if (fromValue != null)
                    {
                        //if (fromPercent != null || toPercent != null)
                        //    row["BinhThuong"] = string.Format("(> {0:F2})", fromValue.Value);
                        //else
                        if (donVi != string.Empty)
                            row["BinhThuong"] = string.Format("(>{0:F2} {1})", fromValue.Value, donVi);
                        else
                            row["BinhThuong"] = string.Format("(>{0:F2})", fromValue.Value);

                        if (testResult <= fromValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }
                    else
                    {
                        //if (fromPercent != null || toPercent != null)
                        //    row["BinhThuong"] = string.Format("(< {0:F2})", toValue.Value);
                        //else
                        if (donVi != string.Empty)
                            row["BinhThuong"] = string.Format("(<{0:F2} {1})", toValue.Value, donVi);
                        else
                            row["BinhThuong"] = string.Format("(<{0:F2})", toValue.Value);

                        if (testResult >= toValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }

                    //if (fromPercent != null && toPercent != null)
                    //{
                    //    double testPercent = Convert.ToDouble(row["TestPercent"]);
                    //    row["Percent"] = string.Format("{0:F2}% ({1:F2} - {2:F2} {3})", testPercent, fromPercent.Value, toPercent.Value, donVi);

                    //    if (tinhTrang == TinhTrang.BinhThuong)
                    //    {
                    //        if (testPercent < fromPercent.Value || testPercent > toPercent.Value)
                    //            tinhTrang = TinhTrang.BatThuong;
                    //    }
                    //}
                    //else if (fromPercent != null)
                    //{
                    //    double testPercent = Convert.ToDouble(row["TestPercent"]);
                    //    row["Percent"] = string.Format("{0:F2}% (> {1:F2} {2})", testPercent, fromPercent.Value, donVi);

                    //    if (tinhTrang == TinhTrang.BinhThuong)
                    //    {
                    //        if (testPercent <= fromPercent.Value)
                    //            tinhTrang = TinhTrang.BatThuong;
                    //    }
                    //}
                    //else if (toPercent != null)
                    //{
                    //    double testPercent = Convert.ToDouble(row["TestPercent"]);
                    //    row["Percent"] = string.Format("{0:F2}% (< {1:F2} {2})", testPercent, toPercent.Value, donVi);

                    //    if (tinhTrang == TinhTrang.BinhThuong)
                    //    {
                    //        if (testPercent >= toPercent.Value)
                    //            tinhTrang = TinhTrang.BatThuong;
                    //    }
                    //}

                    row["TinhTrang"] = (byte)tinhTrang;

                    ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.ChiTietKQXN_CellDyn3200GUID.ToString() == row["ChiTietKQXNGUID"].ToString());
                    if (ctkqxn != null)
                    {
                        ctkqxn.FromValue = fromValue;
                        ctkqxn.ToValue = toValue;
                        //ctkqxn.FromPercent = fromPercent;
                        //ctkqxn.ToPercent = toPercent;
                        ctkqxn.DonVi = donVi;

                        if (fromValue != null)
                            row["FromValue"] = fromValue.Value;

                        if (toValue != null)
                            row["ToValue"] = toValue.Value;

                        //if (fromPercent != null)
                        //    row["FromPercent2"] = fromPercent.Value;

                        //if (toPercent != null)
                        //    row["ToPercent2"] = toPercent.Value;

                        row["DonVi"] = donVi;

                        db.SubmitChanges();
                    }

                    
                }

                dt = dtCellDyn3200;

                //Hitachi917
                query = string.Format("SELECT CAST(0 AS Bit) AS Checked, ChiTietKQXN_Hitachi917GUID AS ChiTietKQXNGUID, XetNghiemGUID, KQXN_Hitachi917GUID AS KetQuaXetNghiemGUID, NgayXN, '' AS NgayXN2, Fullname, N'SINH HÓA (MÁY HITACHI 917)' AS GroupName, TestResult, 0 AS TestPercent, TinhTrang, '' AS BinhThuong, [Type], DaIn, FromValue, ToValue, '' AS FromOperator, '' AS ToOperator, 0 AS FromAge, 0 AS ToAge, 0 AS FromTime, 0 AS ToTime, '' AS FromTimeOperator, '' AS ToTimeOperator, 0 AS XValue, CAST(0 AS Bit) AS HasHutThuoc, DoiTuong, DonVi, CAST(NULL AS float) AS FromPercent, CAST(NULL AS float) AS ToPercent, GroupID, [Order], 'Hitachi917' AS LoaiXN, TestNum, DaUpload, LamThem, 2 AS ThuTu  FROM dbo.ChiTietKetQuaXetNghiem_Hitachi917View WITH(NOLOCK) WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY GroupID, [Order], NgayXN",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtHitachi917 = result.QueryResult as DataTable;
                foreach (DataRow row in dtHitachi917.Rows)
                {
                    string tenXetNghiem = row["Fullname"].ToString();
                    double testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                    DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);

                    //bool isSau2h = false;

                    //if (tenXetNghiem == "Glucose")
                    //{
                    //    if (ngayXN.Hour > 14)
                    //    {
                    //        row["FullName"] = "Postprandial blood sugar";
                    //        isSau2h = true;
                    //    }
                    //}

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
                            //case DoiTuong.Chung_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(>{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.Nam:
                            //case DoiTuong.Nam_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("(M: {0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(M <{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(M >{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.Nu:
                            //case DoiTuong.Nu_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("(F: {0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(F <{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(F >{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.TreEm:
                            //case DoiTuong.TreEm_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("Child ({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("Child (<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("Child (>{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.NguoiLon:
                            //case DoiTuong.NguoiLon_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("Adult ({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("Adult (<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("Adult (>{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.NguoiCaoTuoi:
                            //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format(">60 year ({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format(">60 year (<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format(">60 year (>{0} {1})", fromValue, donVi);
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Chưa cập nhật chỉ số xét nghiệm
                        XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.XetNghiemGUID.ToString() == row["XetNghiemGUID"].ToString());
                        if (xn == null)
                        {
                            dt.ImportRow(row);
                            continue;
                        }

                        List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.Where(c => c.Status == (byte)Status.Actived).ToList<ChiTietXetNghiem_Hitachi917>();
                        if (ctxns.Count <= 0)
                        {
                            dt.ImportRow(row);
                            continue;
                        }

                        ChiTietXetNghiem_Hitachi917 ctxn = null;
                        Gender gender = Gender.None;

                        //if (!isSau2h)
                        {
                            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung);
                            if (ctxn == null)
                            {
                                if (gioiTinh.Trim().ToLower() == "nam")
                                    gender = Gender.Male;
                                else if (gioiTinh.Trim().ToLower() == "nữ")
                                    gender = Gender.Female;

                                if (gender == Gender.None)
                                {
                                    dt.ImportRow(row);
                                    continue;
                                }

                                if (gender == Gender.Male)
                                    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nam);
                                else
                                    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nu);
                            }

                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiLon);
                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiCaoTuoi);
                            if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.TreEm);
                        }
                        //else
                        //{
                        //    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung_Sau2h);
                        //    if (ctxn == null)
                        //    {
                        //        if (gioiTinh.Trim().ToLower() == "nam")
                        //            gender = Gender.Male;
                        //        else if (gioiTinh.Trim().ToLower() == "nữ")
                        //            gender = Gender.Female;

                        //        if (gender == Gender.None) continue;

                        //        if (gender == Gender.Male)
                        //            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nam_Sau2h);
                        //        else
                        //            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nu_Sau2h);
                        //    }

                        //    if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiLon_Sau2h);
                        //    if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiCaoTuoi_Sau2h);
                        //    if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.TreEm_Sau2h);
                        //}

                        if (ctxn == null)
                        {
                            dt.ImportRow(row);
                            continue;
                        }

                        ChiTietKetQuaXetNghiem_Hitachi917 ctkqxn = db.ChiTietKetQuaXetNghiem_Hitachi917s.SingleOrDefault<ChiTietKetQuaXetNghiem_Hitachi917>(c => c.ChiTietKQXN_Hitachi917GUID.ToString() == row["ChiTietKQXNGUID"].ToString());
                        if (ctkqxn != null)
                        {
                            ctkqxn.FromValue = ctxn.FromValue;
                            ctkqxn.ToValue = ctxn.ToValue;
                            ctkqxn.DoiTuong = ctxn.DoiTuong;
                            ctkqxn.DonVi = ctxn.DonVi;

                            if (ctkqxn.FromValue != null && ctxn.FromValue.HasValue)
                                row["FromValue"] = ctxn.FromValue.Value;

                            if (ctkqxn.ToValue != null && ctxn.ToValue.HasValue)
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
                                //case DoiTuong.Chung_Sau2h:
                                    row["BinhThuong"] = string.Format("({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nam:
                                //case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                //case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                //case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                //case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    row["BinhThuong"] = string.Format(">60 year ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
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
                                //case DoiTuong.Chung_Sau2h:
                                    row["BinhThuong"] = string.Format("(>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nam:
                                //case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M >{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                //case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F >{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                //case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child (>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                //case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult (>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    row["BinhThuong"] = string.Format(">60 year (>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
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
                                //case DoiTuong.Chung_Sau2h:
                                    row["BinhThuong"] = string.Format("(<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nam:
                                //case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M <{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                //case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F <{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                //case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child (<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                //case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult (<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    row["BinhThuong"] = string.Format("<60 year (<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                            }
                        }
                        #endregion
                    }

                    dt.ImportRow(row);
                }

                //Xet nghiem tay
                query = string.Format("SELECT CAST(0 AS Bit) AS Checked, ChiTietKetQuaXetNghiem_ManualGUID AS ChiTietKQXNGUID, XetNghiem_ManualGUID AS XetNghiemGUID, KetQuaXetNghiem_ManualGUID AS KetQuaXetNghiemGUID, NgayXetNghiem AS NgayXN, '' AS NgayXN2, Fullname, GroupName, TestResult, 0 AS TestPercent, TinhTrang, '' AS BinhThuong, [Type], DaIn, FromValue, ToValue, FromOperator, ToOperator, FromAge, ToAge, FromTime, ToTime, FromTimeOperator, ToTimeOperator, XValue, HasHutThuoc, DoiTuong, DonVi, CAST(NULL AS float) AS FromPercent, CAST(NULL AS float) AS ToPercent, ISNULL(GroupID, 0) AS GroupID, ISNULL([Order], 0) AS [Order], 'Manual' AS LoaiXN, 0 AS TestNum, DaUpload, LamThem, 3 AS ThuTu FROM dbo.ChiTietKetQuaXetNghiem_ManualView WITH(NOLOCK) WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXetNghiem BETWEEN '{2}' AND '{3}' ORDER BY GroupID, [Order], NgayXetNghiem",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtManual = result.QueryResult as DataTable;
                foreach (DataRow row in dtManual.Rows)
                {
                    bool isUpdate = false;
                    double testResult = 0;
                    try
                    {
                        string resultStr = row["TestResult"].ToString().Trim().ToLower();
                        resultStr = resultStr.Replace("negative", "").Replace("positive", "");
                        testResult = Convert.ToDouble(resultStr.Trim());
                    }
                    catch
                    {
                        string resultStr = row["TestResult"].ToString().Trim().ToLower();
                        if (resultStr.IndexOf("positive") >= 0)
                            row["TinhTrang"] = (byte)TinhTrang.BatThuong;

                        dt.ImportRow(row);
                        continue;
                    }

                    ChiTietXetNghiem_Manual ctxn = KetQuaXetNghiemTayBus.GetChiTietXetNghiem_Manual(db, row, ref isUpdate);
                    if (ctxn == null)
                    {
                        dt.ImportRow(row);
                        continue;
                    }

                    TinhTrang tinhTrang = TinhTrang.BinhThuong;
                    string normalStr = KetQuaXetNghiemTayBus.GetNormalString(testResult, ctxn, ref tinhTrang);
                    row["TinhTrang"] = (byte)tinhTrang;
                    row["BinhThuong"] = normalStr;
                    if (ctxn.DoiTuong == (byte)DoiTuong.AmTinhDuongTinh)
                    {
                        if (tinhTrang == TinhTrang.BinhThuong)
                            row["TestResult"] = string.Format("Negative {0}", row["TestResult"].ToString());
                        else
                            row["TestResult"] = string.Format("Positive {0}", row["TestResult"].ToString());
                    }

                    if (isUpdate)
                    {
                        ChiTietKetQuaXetNghiem_Manual ctkqxn = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(c => c.ChiTietKetQuaXetNghiem_ManualGUID.ToString() == row["ChiTietKQXNGUID"].ToString());
                        if (ctkqxn != null)
                        {
                            ctkqxn.FromValue = ctxn.FromValue;
                            ctkqxn.ToValue = ctxn.ToValue;
                            ctkqxn.FromOperator = ctxn.FromOperator;
                            ctkqxn.ToOperator = ctxn.ToOperator;
                            ctkqxn.FromAge = ctxn.FromAge;
                            ctkqxn.ToAge = ctxn.ToAge;
                            ctkqxn.FromTime = ctxn.FromTime;
                            ctkqxn.ToTime = ctxn.ToTime;
                            ctkqxn.FromTimeOperator = ctxn.FromTimeOperator;
                            ctkqxn.ToTimeOperator = ctxn.ToTimeOperator;
                            ctkqxn.XValue = ctxn.XValue;
                            ctkqxn.DoiTuong = ctxn.DoiTuong;
                            ctkqxn.DonVi = ctxn.DonVi;

                            if (ctxn.FromValue != null && ctxn.FromValue.HasValue)
                                row["FromValue"] = ctxn.FromValue.Value;

                            if (ctxn.ToValue != null && ctxn.ToValue.HasValue)
                                row["ToValue"] = ctxn.ToValue.Value;

                            if (ctxn.FromOperator != null)
                                row["FromOperator"] = ctxn.FromOperator;

                            if (ctxn.ToOperator != null)
                                row["ToOperator"] = ctxn.ToOperator;

                            if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                row["FromAge"] = ctxn.FromAge.Value;

                            if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                row["ToAge"] = ctxn.ToAge.Value;

                            if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                                row["FromTime"] = ctxn.FromTime.Value;

                            if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                                row["ToTime"] = ctxn.ToTime.Value;

                            if (ctxn.FromTimeOperator != null)
                                row["FromTimeOperator"] = ctxn.FromTimeOperator;

                            if (ctxn.ToTimeOperator != null)
                                row["ToTimeOperator"] = ctxn.ToTimeOperator;

                            if (ctxn.XValue != null && ctxn.XValue.HasValue)
                                row["XValue"] = ctxn.XValue.Value;

                            row["DoiTuong"] = ctxn.DoiTuong;
                            row["DonVi"] = ctxn.DonVi;

                            db.SubmitChanges();
                        }
                    }

                    dt.ImportRow(row);
                }

                //List<DataRow> results = (from p in dt.AsEnumerable()
                //           orderby p.Field<DateTime>("NgayXN"), p.Field<int>("GroupID"), p.Field<int>("Order")
                //           select p).ToList<DataRow>();

                //DataTable newDataSource = dt.Clone();

                //string ngay = string.Empty;
                //foreach (DataRow row in results)
                //{
                //    string ngayXN = Convert.ToDateTime(row["NgayXN2"]).ToString("dd/MM/yyyy");
                //    if (ngay == ngayXN)
                //        row["NgayXN2"] = DBNull.Value;

                //    ngay = ngayXN;

                //    newDataSource.ImportRow(row);
                //}

                //dt.Rows.Clear();
                //dt = null;
                //result.QueryResult = newDataSource;
                result.QueryResult = dt;

                db.SubmitChanges();
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

        public static Result GetKetQuaXetNghiemCellDyn3200List(DateTime fromDate, DateTime toDate, string patientGUID, string ngaySinh, string gioiTinh)
        {
            Result result = new Result();
            MMOverride db = new MMOverride();

            try
            {
                string query = string.Empty;
                DataTable dt = null;

                string emptyGUID = Guid.Empty.ToString();
                query = string.Format("SELECT CAST(0 AS Bit) AS Checked, ChiTietKQXN_CellDyn3200GUID AS ChiTietKQXNGUID, '{4}' AS XetNghiemGUID, NgayXN, NgayXN AS NgayXN2, Fullname, TestResult, TestPercent, TinhTrang, '' AS BinhThuong, [Type], DaIn, FromValue2, ToValue2, DoiTuong2, DonVi2, FromPercent2, ToPercent2, GroupID, [Order], 'CellDyn3200' AS LoaiXN, DaUpload, LamThem FROM dbo.ChiTietKetQuaXetNghiem_CellDyn3200View WITH(NOLOCK) WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY NgayXN, GroupID, [Order]",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"), emptyGUID);

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    double? fromValue = null;
                    double? toValue = null;
                    //double? fromPercent = null;
                    //double? toPercent = null;
                    string donVi = string.Empty;

                    if ((row["FromValue2"] != null && row["FromValue2"] != DBNull.Value) ||
                        (row["ToValue2"] != null && row["ToValue2"] != DBNull.Value))
                    {
                        if (row["FromValue2"] != null && row["FromValue2"] != DBNull.Value)
                            fromValue = Convert.ToDouble(row["FromValue2"]);

                        if (row["ToValue2"] != null && row["ToValue2"] != DBNull.Value)
                            toValue = Convert.ToDouble(row["ToValue2"]);

                        //if (row["FromPercent2"] != null && row["FromPercent2"] != DBNull.Value)
                        //    fromPercent = Convert.ToDouble(row["FromPercent2"]);

                        //if (row["ToPercent2"] != null && row["ToPercent2"] != DBNull.Value)
                        //    toPercent = Convert.ToDouble(row["ToPercent2"]);

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

                        //if (xn.FromPercent.HasValue)
                        //    fromPercent = xn.FromPercent.Value;

                        //if (xn.ToPercent.HasValue)
                        //    toPercent = xn.ToPercent.Value;

                        if (xn.DonVi != null && xn.DonVi != string.Empty)
                            donVi = xn.DonVi;
                    }

                    double testResult = Convert.ToDouble(row["TestResult"]);
                    TinhTrang tinhTrang = TinhTrang.BinhThuong;

                    if (fromValue != null && toValue != null)
                    {
                        //if (fromPercent != null || toPercent != null)
                        //    row["BinhThuong"] = string.Format("({0:F2} - {1:F2})", fromValue.Value, toValue.Value);
                        //else
                        if (donVi != string.Empty)
                            row["BinhThuong"] = string.Format("({0:F2}-{1:F2} {2})", fromValue.Value, toValue.Value, donVi);
                        else
                            row["BinhThuong"] = string.Format("({0:F2}-{1:F2})", fromValue.Value, toValue.Value);

                        if (testResult < fromValue.Value || testResult > toValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }
                    else if (fromValue != null)
                    {
                        //if (fromPercent != null || toPercent != null)
                        //    row["BinhThuong"] = string.Format("(> {0:F2})", fromValue.Value);
                        //else
                        if (donVi != string.Empty)
                            row["BinhThuong"] = string.Format("(>{0:F2} {1})", fromValue.Value, donVi);
                        else
                            row["BinhThuong"] = string.Format("(>{0:F2})", fromValue.Value);

                        if (testResult <= fromValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }
                    else
                    {
                        //if (fromPercent != null || toPercent != null)
                        //    row["BinhThuong"] = string.Format("(< {0:F2})", toValue.Value);
                        //else
                        if (donVi != string.Empty)
                            row["BinhThuong"] = string.Format("(<{0:F2} {1})", toValue.Value, donVi);
                        else
                            row["BinhThuong"] = string.Format("(<{0:F2})", toValue.Value);

                        if (testResult >= toValue.Value)
                            tinhTrang = TinhTrang.BatThuong;
                    }

                    //if (fromPercent != null && toPercent != null)
                    //{
                    //    double testPercent = Convert.ToDouble(row["TestPercent"]);
                    //    row["Percent"] = string.Format("{0:F2}% ({1:F2} - {2:F2} {3})", testPercent, fromPercent.Value, toPercent.Value, donVi);

                    //    if (tinhTrang == TinhTrang.BinhThuong)
                    //    {
                    //        if (testPercent < fromPercent.Value || testPercent > toPercent.Value)
                    //            tinhTrang = TinhTrang.BatThuong;
                    //    }
                    //}
                    //else if (fromPercent != null)
                    //{
                    //    double testPercent = Convert.ToDouble(row["TestPercent"]);
                    //    row["Percent"] = string.Format("{0:F2}% (> {1:F2} {2})", testPercent, fromPercent.Value, donVi);

                    //    if (tinhTrang == TinhTrang.BinhThuong)
                    //    {
                    //        if (testPercent <= fromPercent.Value)
                    //            tinhTrang = TinhTrang.BatThuong;
                    //    }
                    //}
                    //else if (toPercent != null)
                    //{
                    //    double testPercent = Convert.ToDouble(row["TestPercent"]);
                    //    row["Percent"] = string.Format("{0:F2}% (< {1:F2} {2})", testPercent, toPercent.Value, donVi);

                    //    if (tinhTrang == TinhTrang.BinhThuong)
                    //    {
                    //        if (testPercent >= toPercent.Value)
                    //            tinhTrang = TinhTrang.BatThuong;
                    //    }
                    //}

                    row["TinhTrang"] = (byte)tinhTrang;

                    ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = db.ChiTietKetQuaXetNghiem_CellDyn3200s.SingleOrDefault<ChiTietKetQuaXetNghiem_CellDyn3200>(c => c.ChiTietKQXN_CellDyn3200GUID.ToString() == row["ChiTietKQXNGUID"].ToString());
                    if (ctkqxn != null)
                    {
                        ctkqxn.FromValue = fromValue;
                        ctkqxn.ToValue = toValue;
                        //ctkqxn.FromPercent = fromPercent;
                        //ctkqxn.ToPercent = toPercent;
                        ctkqxn.DonVi = donVi;

                        if (fromValue != null)
                            row["FromValue2"] = fromValue.Value;

                        if (toValue != null)
                            row["ToValue2"] = toValue.Value;

                        //if (fromPercent != null)
                        //    row["FromPercent2"] = fromPercent.Value;

                        //if (toPercent != null)
                        //    row["ToPercent2"] = toPercent.Value;

                        row["DonVi2"] = donVi;

                        db.SubmitChanges();
                    }
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

        public static Result GetKetQuaXetNghiemSinhHoaList(DateTime fromDate, DateTime toDate, string patientGUID, string ngaySinh, string gioiTinh)
        {
            Result result = new Result();
            MMOverride db = new MMOverride();

            try
            {
                string query = string.Empty;
                DataTable dt = null;

                //Hitachi917
                query = string.Format("SELECT CAST(0 AS Bit) AS Checked, ChiTietKQXN_Hitachi917GUID AS ChiTietKQXNGUID, XetNghiemGUID, NgayXN, NgayXN AS NgayXN2, Fullname, TestResult, 0 AS TestPercent, TinhTrang, '' AS BinhThuong, [Type], DaIn, FromValue AS FromValue2, ToValue AS ToValue2, DoiTuong AS DoiTuong2, DonVi AS DonVi2, CAST(NULL AS float) AS FromPercent2, CAST(NULL AS float) AS ToPercent2, GroupID, [Order], 'Hitachi917' AS LoaiXN, DaUpload, LamThem  FROM dbo.ChiTietKetQuaXetNghiem_Hitachi917View WITH(NOLOCK) WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY GroupID, [Order], NgayXN",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtHitachi917 = result.QueryResult as DataTable;
                foreach (DataRow row in dtHitachi917.Rows)
                {
                    string tenXetNghiem = row["Fullname"].ToString();
                    double testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                    DateTime ngayXN = Convert.ToDateTime(row["NgayXN"]);

                    //bool isSau2h = false;

                    //if (tenXetNghiem == "Glucose")
                    //{
                    //    if (ngayXN.Hour > 14)
                    //    {
                    //        row["FullName"] = "Postprandial blood sugar";
                    //        isSau2h = true;
                    //    }
                    //}

                    if ((row["FromValue2"] != null && row["FromValue2"] != DBNull.Value) ||
                        (row["ToValue2"] != null && row["ToValue2"] != DBNull.Value))
                    {
                        #region Đã cập nhật chỉ số xét nghiệm
                        string donVi = string.Empty;
                        if (row["DonVi2"] != null && row["DonVi2"] != DBNull.Value)
                            donVi = row["DonVi2"].ToString();

                        double fromValue = 0;
                        double toValue = 0;
                        int kq = 0;
                        if (row["FromValue2"] == null || row["FromValue2"] == DBNull.Value)
                        {
                            kq = 1;
                            toValue = Convert.ToDouble(row["ToValue2"]);
                            if (testResult >= toValue)
                                row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                            else
                                row["TinhTrang"] = (byte)TinhTrang.BinhThuong;
                        }
                        else if (row["ToValue2"] == null || row["ToValue2"] == DBNull.Value)
                        {
                            kq = 2;
                            fromValue = Convert.ToDouble(row["FromValue2"]);
                            if (testResult <= fromValue)
                                row["TinhTrang"] = (byte)TinhTrang.BatThuong;
                            else
                                row["TinhTrang"] = (byte)TinhTrang.BinhThuong;
                        }
                        else
                        {
                            fromValue = Convert.ToDouble(row["FromValue2"]);
                            toValue = Convert.ToDouble(row["ToValue2"]);

                            if (testResult >= fromValue && testResult <= toValue)
                                row["TinhTrang"] = TinhTrang.BinhThuong;
                            else
                                row["TinhTrang"] = TinhTrang.BatThuong;
                        }


                        DoiTuong doiTuong = (DoiTuong)Convert.ToByte(row["DoiTuong2"]);
                        switch (doiTuong)
                        {
                            case DoiTuong.Chung:
                            //case DoiTuong.Chung_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(>{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.Nam:
                            //case DoiTuong.Nam_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("(M: {0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(M <{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(M >{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.Nu:
                            //case DoiTuong.Nu_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("(F: {0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("(F <{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("(F >{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.TreEm:
                            //case DoiTuong.TreEm_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("Child ({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("Child (<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("Child (>{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.NguoiLon:
                            //case DoiTuong.NguoiLon_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format("Adult ({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format("Adult (<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format("Adult (>{0} {1})", fromValue, donVi);
                                break;
                            case DoiTuong.NguoiCaoTuoi:
                            //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                if (kq == 0)
                                    row["BinhThuong"] = string.Format(">60 year ({0}-{1} {2})", fromValue, toValue, donVi);
                                else if (kq == 1)
                                    row["BinhThuong"] = string.Format(">60 year (<{0} {1})", toValue, donVi);
                                else
                                    row["BinhThuong"] = string.Format(">60 year (>{0} {1})", fromValue, donVi);
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Chưa cập nhật chỉ số xét nghiệm
                        XetNghiem_Hitachi917 xn = db.XetNghiem_Hitachi917s.SingleOrDefault<XetNghiem_Hitachi917>(x => x.XetNghiemGUID.ToString() == row["XetNghiemGUID"].ToString());
                        if (xn == null) continue;
                        List<ChiTietXetNghiem_Hitachi917> ctxns = xn.ChiTietXetNghiem_Hitachi917s.Where(c => c.Status == (byte)Status.Actived).ToList<ChiTietXetNghiem_Hitachi917>();
                        if (ctxns.Count <= 0) continue;
                        ChiTietXetNghiem_Hitachi917 ctxn = null;
                        Gender gender = Gender.None;

                        //if (!isSau2h)
                        {
                            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung);
                            if (ctxn == null)
                            {
                                if (gioiTinh.Trim().ToLower() == "nam")
                                    gender = Gender.Male;
                                else if (gioiTinh.Trim().ToLower() == "nữ")
                                    gender = Gender.Female;

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
                        //else
                        //{
                        //    ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Chung_Sau2h);
                        //    if (ctxn == null)
                        //    {
                        //        if (gioiTinh.Trim().ToLower() == "nam")
                        //            gender = Gender.Male;
                        //        else if (gioiTinh.Trim().ToLower() == "nữ")
                        //            gender = Gender.Female;

                        //        if (gender == Gender.None) continue;

                        //        if (gender == Gender.Male)
                        //            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nam_Sau2h);
                        //        else
                        //            ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.Nu_Sau2h);
                        //    }

                        //    if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiLon_Sau2h);
                        //    if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.NguoiCaoTuoi_Sau2h);
                        //    if (ctxn == null) ctxn = GetChiTietXetNghiem(ctxns, DoiTuong.TreEm_Sau2h);
                        //}

                        if (ctxn == null) continue;

                        ChiTietKetQuaXetNghiem_Hitachi917 ctkqxn = db.ChiTietKetQuaXetNghiem_Hitachi917s.SingleOrDefault<ChiTietKetQuaXetNghiem_Hitachi917>(c => c.ChiTietKQXN_Hitachi917GUID.ToString() == row["ChiTietKQXNGUID"].ToString());
                        if (ctkqxn != null)
                        {
                            ctkqxn.FromValue = ctxn.FromValue;
                            ctkqxn.ToValue = ctxn.ToValue;
                            ctkqxn.DoiTuong = ctxn.DoiTuong;
                            ctkqxn.DonVi = ctxn.DonVi;

                            if (ctxn.FromValue != null && ctxn.FromValue.HasValue)
                                row["FromValue2"] = ctxn.FromValue.Value;

                            if (ctxn.ToValue != null && ctxn.ToValue.HasValue)
                                row["ToValue2"] = ctxn.ToValue.Value;

                            row["DoiTuong2"] = ctxn.DoiTuong;
                            row["DonVi2"] = ctxn.DonVi;

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
                                //case DoiTuong.Chung_Sau2h:
                                    row["BinhThuong"] = string.Format("({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nam:
                                //case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                //case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                //case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                //case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    row["BinhThuong"] = string.Format(">60 year ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
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
                                //case DoiTuong.Chung_Sau2h:
                                    row["BinhThuong"] = string.Format("(>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nam:
                                //case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M >{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                //case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F >{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                //case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child (>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                //case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult (>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    row["BinhThuong"] = string.Format(">60 year (>{0} {1})", ctxn.FromValue.Value, ctxn.DonVi);
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
                                //case DoiTuong.Chung_Sau2h:
                                    row["BinhThuong"] = string.Format("(<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nam:
                                //case DoiTuong.Nam_Sau2h:
                                    row["BinhThuong"] = string.Format("(M <{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.Nu:
                                //case DoiTuong.Nu_Sau2h:
                                    row["BinhThuong"] = string.Format("(F <{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.TreEm:
                                //case DoiTuong.TreEm_Sau2h:
                                    row["BinhThuong"] = string.Format("Child (<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiLon:
                                //case DoiTuong.NguoiLon_Sau2h:
                                    row["BinhThuong"] = string.Format("Adult (<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                                case DoiTuong.NguoiCaoTuoi:
                                //case DoiTuong.NguoiCaoTuoi_Sau2h:
                                    row["BinhThuong"] = string.Format("<60 year (<{0} {1})", ctxn.ToValue.Value, ctxn.DonVi);
                                    break;
                            }
                        }
                        #endregion
                    }
                }

                dt = dtHitachi917;

                //Xet nghiem tay
                query = string.Format("SELECT CAST(0 AS Bit) AS Checked, ChiTietKetQuaXetNghiem_ManualGUID AS ChiTietKQXNGUID, XetNghiem_ManualGUID AS XetNghiemGUID, NgayXN, NgayXN AS NgayXN2, Fullname, TestResult, 0 AS TestPercent, TinhTrang, '' AS BinhThuong, [Type], DaIn, FromValue AS FromValue2, ToValue AS ToValue2, DoiTuong AS DoiTuong2, DonVi AS DonVi2, CAST(NULL AS float) AS FromPercent2, CAST(NULL AS float) AS ToPercent2, ISNULL(GroupID, 0) AS GroupID, ISNULL([Order], 0) AS [Order], 'Manual' AS LoaiXN, DaUpload, LamThem FROM dbo.ChiTietKetQuaXetNghiem_ManualView WITH(NOLOCK) WHERE Status={0} AND KQXNStatus={0} AND PatientGUID='{1}' AND NgayXN BETWEEN '{2}' AND '{3}' ORDER BY NgayXN, Fullname",
                    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dtManual = result.QueryResult as DataTable;
                foreach (DataRow row in dtManual.Rows)
                {
                    bool isUpdate = false;
                    double testResult = 0;
                    try
                    {
                        testResult = Convert.ToDouble(row["TestResult"].ToString().Trim());
                    }
                    catch
                    {
                        continue;
                    }

                    string xetNghiem_ManualGUID = row["XetNghiemGUID"].ToString();
                    ChiTietXetNghiem_Manual ctxn = null;

                    if ((row["FromValue2"] != null && row["FromValue2"] != DBNull.Value) ||
                        (row["ToValue2"] != null && row["ToValue2"] != DBNull.Value))
                    {
                        ctxn = new ChiTietXetNghiem_Manual();

                        if (row["FromValue2"] != null && row["FromValue2"] != DBNull.Value)
                            ctxn.FromValue = Convert.ToDouble(row["FromValue2"]);

                        if (row["ToValue2"] != null && row["ToValue2"] != DBNull.Value)
                            ctxn.ToValue = Convert.ToDouble(row["ToValue2"]);

                        if (row["DonVi2"] != null && row["DonVi2"] != DBNull.Value)
                            ctxn.DonVi = row["DonVi2"].ToString().Trim();

                        ctxn.DoiTuong = Convert.ToByte(row["DoiTuong2"]);
                    }
                    else
                    {
                        XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID.ToString() == xetNghiem_ManualGUID);
                        if (xn == null) continue;
                        List<ChiTietXetNghiem_Manual> ctxns = xn.ChiTietXetNghiem_Manuals.ToList<ChiTietXetNghiem_Manual>();
                        if (ctxns.Count <= 0) continue;

                        if (ctxns[0].DoiTuong == (byte)DoiTuong.Chung) ctxn = ctxns[0];
                        else if (ctxns[0].DoiTuong == (byte)DoiTuong.Nam || ctxns[0].DoiTuong == (byte)DoiTuong.Nu)
                        {
                            if (gioiTinh.Trim().ToLower() != "nam" && gioiTinh.Trim().ToLower() != "nữ") continue;

                            if (gioiTinh.Trim().ToLower() == "nam")
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

                        isUpdate = true;
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

                    if (isUpdate)
                    {
                        ChiTietKetQuaXetNghiem_Manual ctkqxn = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(c => c.ChiTietKetQuaXetNghiem_ManualGUID.ToString() == row["ChiTietKQXNGUID"].ToString());
                        if (ctkqxn != null)
                        {
                            ctkqxn.FromValue = ctxn.FromValue;
                            ctkqxn.ToValue = ctxn.ToValue;
                            ctkqxn.DoiTuong = ctxn.DoiTuong;
                            ctkqxn.DonVi = ctxn.DonVi;

                            if (ctxn.FromValue != null && ctxn.FromValue.HasValue)
                                row["FromValue2"] = ctxn.FromValue.Value;

                            if (ctxn.ToValue != null && ctxn.ToValue.HasValue)
                                row["ToValue2"] = ctxn.ToValue.Value;

                            row["DoiTuong2"] = ctxn.DoiTuong;
                            row["DonVi2"] = ctxn.DonVi;

                            db.SubmitChanges();
                        }
                    }

                    dt.ImportRow(row);
                }

                List<DataRow> results = (from p in dt.AsEnumerable()
                                         orderby p.Field<DateTime>("NgayXN"), p.Field<int>("GroupID"), p.Field<int>("Order")
                                         select p).ToList<DataRow>();

                DataTable newDataSource = dt.Clone();

                foreach (DataRow row in results)
                {
                    newDataSource.ImportRow(row);
                }

                dt.Rows.Clear();
                dt = null;
                result.QueryResult = newDataSource;
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
