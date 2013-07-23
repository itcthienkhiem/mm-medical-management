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
        public static Result GetKetQuaXetNghiemList(DateTime fromDate, DateTime toDate, string tenBenhNhan, bool isMaBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (tenBenhNhan.Trim() == string.Empty)
                {
                    query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, V.KetQuaXetNghiemManualGUID, V.PatientGUID, V.Status, V.FullName, V.DobStr, V.GenderAsStr, V.Archived, V.FileNum, V.Address FROM KetQuaXetNghiem_ManualView V WITH(NOLOCK), ChiTietKetQuaXetNghiem_Manual C WITH(NOLOCK) WHERE V.KetQuaXetNghiemManualGUID = C.KetQuaXetNghiem_ManualGUID AND V.Archived = 'False' AND C.NgayXetNghiem BETWEEN '{0}' AND '{1}' AND V.Status = {2} AND C.Status = {2} ORDER BY V.FullName",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived);
                }
                else
                {
                    if (!isMaBenhNhan)
                    {
                        //query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_ManualView WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} AND FullName LIKE N'%{3}%' ORDER BY NgayXN DESC",
                        //   fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived, tenBenhNhan);
                        query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, V.KetQuaXetNghiemManualGUID, V.PatientGUID, V.Status, V.FullName, V.DobStr, V.GenderAsStr, V.Archived, V.FileNum, V.Address FROM KetQuaXetNghiem_ManualView V WITH(NOLOCK), ChiTietKetQuaXetNghiem_Manual C WITH(NOLOCK) WHERE V.KetQuaXetNghiemManualGUID = C.KetQuaXetNghiem_ManualGUID AND V.Archived = 'False' AND C.NgayXetNghiem BETWEEN '{0}' AND '{1}' AND V.Status = {2} AND C.Status = {2} AND V.FullName LIKE N'%{3}%' ORDER BY V.FullName",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived, tenBenhNhan);
                    }
                    else
                    {
                        //query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KetQuaXetNghiem_ManualView WHERE NgayXN BETWEEN '{0}' AND '{1}' AND Status = {2} AND FileNum LIKE N'%{3}%' ORDER BY NgayXN DESC",
                        //   fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived, tenBenhNhan);
                        query = string.Format("SELECT DISTINCT CAST(0 AS Bit) AS Checked, V.KetQuaXetNghiemManualGUID, V.PatientGUID, V.Status, V.FullName, V.DobStr, V.GenderAsStr, V.Archived, V.FileNum, V.Address FROM KetQuaXetNghiem_ManualView V WITH(NOLOCK), ChiTietKetQuaXetNghiem_Manual C WITH(NOLOCK) WHERE V.KetQuaXetNghiemManualGUID = C.KetQuaXetNghiem_ManualGUID AND V.Archived = 'False' AND C.NgayXetNghiem BETWEEN '{0}' AND '{1}' AND V.Status = {2} AND C.Status = {2} AND V.FileNum LIKE N'%{3}%' ORDER BY V.FullName",
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

        public static ChiTietXetNghiem_Manual GetChiTietXetNghiem_Manual(List<ChiTietXetNghiem_Manual> ctxns, DoiTuong doiTuong)
        {
            foreach (ChiTietXetNghiem_Manual ctxn in ctxns)
            {
                if (ctxn.DoiTuong == (int)doiTuong)
                    return ctxn;
            }

            return null;
        }

        public static ChiTietXetNghiem_Manual GetChiTietXetNghiem_Manual(MMOverride db, DataRow row, ref bool isUpdate)
        {
            ChiTietXetNghiem_Manual ctxn = null;

            if ((row["DoiTuong"] != null && row["DoiTuong"] != DBNull.Value))
            {
                ctxn = new ChiTietXetNghiem_Manual();

                if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                    ctxn.FromValue = Convert.ToDouble(row["FromValue"]);

                if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                    ctxn.ToValue = Convert.ToDouble(row["ToValue"]);

                if (row["FromOperator"] != null && row["FromOperator"] != DBNull.Value)
                    ctxn.FromOperator = row["FromOperator"].ToString();

                if (row["ToOperator"] != null && row["ToOperator"] != DBNull.Value)
                    ctxn.ToOperator = row["ToOperator"].ToString();

                if (row["FromAge"] != null && row["FromAge"] != DBNull.Value)
                    ctxn.FromAge = Convert.ToInt32(row["FromAge"]);

                if (row["ToAge"] != null && row["ToAge"] != DBNull.Value)
                    ctxn.ToAge = Convert.ToInt32(row["ToAge"]);

                if (row["FromTime"] != null && row["FromTime"] != DBNull.Value)
                    ctxn.FromTime = Convert.ToInt32(row["FromTime"]);

                if (row["ToTime"] != null && row["ToTime"] != DBNull.Value)
                    ctxn.ToTime = Convert.ToInt32(row["ToTime"]);

                if (row["FromTimeOperator"] != null && row["FromTimeOperator"] != DBNull.Value)
                    ctxn.FromTimeOperator = row["FromTimeOperator"].ToString();

                if (row["ToTimeOperator"] != null && row["ToTimeOperator"] != DBNull.Value)
                    ctxn.ToTimeOperator = row["ToTimeOperator"].ToString();

                if (row["XValue"] != null && row["XValue"] != DBNull.Value)
                    ctxn.XValue = Convert.ToDouble(row["XValue"]);

                if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                    ctxn.DonVi = row["DonVi"].ToString().Trim();

                ctxn.DoiTuong = Convert.ToByte(row["DoiTuong"]);
            }
            else
            {
                isUpdate = true;
                string xetNghiem_ManualGUID = string.Empty;
                if (row.Table.Columns.Contains("XetNghiemGUID"))
                    xetNghiem_ManualGUID = row["XetNghiemGUID"].ToString();
                else
                    xetNghiem_ManualGUID = row["XetNghiem_ManualGUID"].ToString();

                XetNghiem_Manual xn = db.XetNghiem_Manuals.FirstOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID.ToString() == xetNghiem_ManualGUID);
                if (xn == null || xn.Fullname.ToUpper() == "ESTRADIOL") return null;
                List<ChiTietXetNghiem_Manual> ctxns = xn.ChiTietXetNghiem_Manuals.Where(c => c.Status == (byte)Status.Actived).ToList<ChiTietXetNghiem_Manual>();
                if (ctxns == null || ctxns.Count <= 0) return null;
                int age = 0;

                //Chung
                ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Chung);
                if (ctxn != null) return ctxn;

                //Âm tính - Dương Tính
                ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.AmTinhDuongTinh);
                if (ctxn != null) return ctxn;
                
                //Nam - Nữ
                string ketQuaXetNghiem_ManualGUID = string.Empty;
                if (row.Table.Columns.Contains("KetQuaXetNghiemGUID"))
                    ketQuaXetNghiem_ManualGUID = row["KetQuaXetNghiemGUID"].ToString();
                else
                    ketQuaXetNghiem_ManualGUID = row["KetQuaXetNghiem_ManualGUID"].ToString();

                KetQuaXetNghiem_ManualView kqxn = db.KetQuaXetNghiem_ManualViews.SingleOrDefault<KetQuaXetNghiem_ManualView>(k => k.KetQuaXetNghiemManualGUID.ToString() == ketQuaXetNghiem_ManualGUID);
                if (kqxn == null) return null;
                if (kqxn.GenderAsStr.ToLower() == "nam")
                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Nam);
                else
                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Nu);

                if (ctxn != null)
                {
                    if (ctxn != null && ((ctxn.FromAge != null && ctxn.FromAge.HasValue) || (ctxn.ToAge != null && ctxn.ToAge.HasValue)))
                    {
                        if (kqxn.DobStr == null || kqxn.DobStr.Trim() == string.Empty) return null;
                        age = Utility.GetAge(kqxn.DobStr);

                        if (ctxn.FromAge != null && ctxn.FromAge.HasValue && age < ctxn.FromAge.Value)
                            return null;

                        if (ctxn.ToAge != null && ctxn.ToAge.HasValue && age > ctxn.ToAge.Value)
                            return null;
                    }

                    return ctxn;
                }

                //Trẻ em - Người lớn - Người cao tuổi
                if (kqxn.DobStr != null && kqxn.DobStr.Trim() != string.Empty)
                {
                    age = Utility.GetAge(kqxn.DobStr);
                    if (age < 18)
                        ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.TreEm);
                    else if (age < 60)
                        ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.NguoiLon);
                    else
                        ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.NguoiCaoTuoi);

                    if (ctxn != null) return ctxn;
                }

                //Khác
                ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Khac);
                if (ctxn != null) return ctxn;

                //Hút thuốc - Không hút thuốc
                bool hasHutThuoc = Convert.ToBoolean(row["HasHutThuoc"]);
                if (hasHutThuoc)
                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.HutThuoc);
                else
                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.KhongHutThuoc);

                if (ctxn != null) return ctxn;

                //Sáng - Chiều
                DateTime ngayXetNghiem = DateTime.Now;
                if (row.Table.Columns.Contains("NgayXN"))
                    ngayXetNghiem = Convert.ToDateTime(row["NgayXN"]);
                else
                    ngayXetNghiem = Convert.ToDateTime(row["NgayXetNghiem"]);

                int second = ngayXetNghiem.Hour * 60 * 60 + ngayXetNghiem.Minute * 60 + ngayXetNghiem.Second;

                ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Sang_Chung);
                if (ctxn != null)
                {
                    if (ctxn.FromTime != null && ctxn.FromTime.HasValue &&
                        ctxn.ToTime != null && ctxn.ToTime.HasValue)
                    {
                        int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                        int toTimeSecond = ctxn.ToTime.Value * 60 * 60;

                        if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                            (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond) ||
                            (ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                            (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                    }
                    else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                    {
                        int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                        if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                            (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond)) ctxn = null;
                    }
                    else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                    {
                        int toTimeSecond = ctxn.ToTime.Value * 60 * 60;
                        if ((ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                            (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                    }
                    else
                        return ctxn;
                }

                ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Chieu_Chung);
                if (ctxn != null)
                {
                    if (ctxn.FromTime != null && ctxn.FromTime.HasValue &&
                        ctxn.ToTime != null && ctxn.ToTime.HasValue)
                    {
                        int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                        int toTimeSecond = ctxn.ToTime.Value * 60 * 60;

                        if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                            (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond) ||
                            (ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                            (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                    }
                    else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                    {
                        int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                        if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                            (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond)) ctxn = null;
                    }
                    else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                    {
                        int toTimeSecond = ctxn.ToTime.Value * 60 * 60;
                        if ((ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                            (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                    }
                    else
                        return ctxn;
                }

                if (kqxn.GenderAsStr.ToLower() == "nam")
                {
                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Sang_Nam);
                    if (ctxn != null)
                    {
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue &&
                            ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;

                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond) ||
                                (ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;
                            if ((ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else
                            return ctxn;
                    }

                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Chieu_Nam);
                    if (ctxn != null)
                    {
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue &&
                            ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;

                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond) ||
                                (ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;
                            if ((ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else
                            return ctxn;
                    }
                }
                else
                {
                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Sang_Nu);
                    if (ctxn != null)
                    {
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue &&
                            ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;

                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond) ||
                                (ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;
                            if ((ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else
                            return ctxn;
                    }

                    ctxn = GetChiTietXetNghiem_Manual(ctxns, DoiTuong.Chieu_Nu);
                    if (ctxn != null)
                    {
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue &&
                            ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;

                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond) ||
                                (ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            int fromTimeSecond = ctxn.FromTime.Value * 60 * 60;
                            if ((ctxn.FromTimeOperator == "<" && second <= fromTimeSecond) ||
                                (ctxn.FromTimeOperator == "<=" && second < fromTimeSecond)) ctxn = null;
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            int toTimeSecond = ctxn.ToTime.Value * 60 * 60;
                            if ((ctxn.ToTimeOperator == "<" && second >= toTimeSecond) ||
                                (ctxn.ToTimeOperator == "<=" && second > toTimeSecond)) ctxn = null;
                        }
                        else
                            return ctxn;
                    }
                }
            }

            return ctxn;
        }

        public static string GetNormalString(double testResult, ChiTietXetNghiem_Manual ctxn, ref TinhTrang tinhTrang)
        {
            string normalString = string.Empty;
            DoiTuong doiTuong = (DoiTuong)ctxn.DoiTuong;

            if (ctxn.FromValue.HasValue && ctxn.ToValue.HasValue)
            {
                tinhTrang = TinhTrang.BinhThuong;

                if (doiTuong == DoiTuong.Khac)
                {
                    if (testResult < ctxn.FromValue.Value || testResult > ctxn.ToValue.Value)
                        tinhTrang = TinhTrang.BatThuong;
                }
                else
                {
                    if (ctxn.FromOperator == "<" && testResult <= ctxn.FromValue.Value)
                        tinhTrang = TinhTrang.BatThuong;
                    else if (ctxn.FromOperator == "<=" && testResult < ctxn.FromValue.Value)
                        tinhTrang = TinhTrang.BatThuong;

                    if (ctxn.ToOperator == "<" && testResult >= ctxn.ToValue.Value)
                        tinhTrang = TinhTrang.BatThuong;
                    else if (ctxn.ToOperator == "<=" && testResult > ctxn.ToValue.Value)
                        tinhTrang = TinhTrang.BatThuong;
                }

                switch (doiTuong)
                {
                    case DoiTuong.Chung:
                        normalString = string.Format("({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;
                    case DoiTuong.Nam:
                        if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                            normalString = string.Format("(M {3}-{4}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value, ctxn.ToAge.Value);
                        else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                            normalString = string.Format("(M >{3}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value);
                        else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                            normalString = string.Format("(M <{3}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.ToAge.Value);
                        else
                            normalString = string.Format("(M: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;
                    case DoiTuong.Nu:
                        if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                            normalString = string.Format("(F {3}-{4}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value, ctxn.ToAge.Value);
                        else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                            normalString = string.Format("(F >{3}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value);
                        else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                            normalString = string.Format("(F <{3}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.ToAge.Value);
                        else
                            normalString = string.Format("(F: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;
                    case DoiTuong.TreEm:
                        normalString = string.Format("Child ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;
                    case DoiTuong.NguoiLon:
                        normalString = string.Format("Adult ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;
                    case DoiTuong.NguoiCaoTuoi:
                        normalString = string.Format(">60 year ({0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;
                    case DoiTuong.AmTinhDuongTinh:
                        normalString = string.Format("(Negative {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;

                    case DoiTuong.HutThuoc:
                        normalString = string.Format("(Smoke {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;

                    case DoiTuong.KhongHutThuoc:
                        normalString = string.Format("(Nosmoke {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi);
                        break;

                    case DoiTuong.Khac:
                        normalString = string.Format("{0}-{1} (+)/ {2}X", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.XValue);
                        break;

                    case DoiTuong.Sang_Chung:
                    case DoiTuong.Chieu_Chung:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                            normalString = string.Format("({3}-{4}h: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromTime.Value, ctxn.ToTime.Value);
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");
                            normalString = string.Format("({3}{4}h: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, op, ctxn.FromTime.Value);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                            normalString = string.Format("({3}{4}h: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, ctxn.ToTimeOperator, ctxn.FromTime.Value);
                        break;

                    case DoiTuong.Sang_Nam:
                    case DoiTuong.Chieu_Nam:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({3}-{4}h) (M {5}-{6}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({3}-{4}h) (M >{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({3}-{4}h) (M <{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.ToAge.Value);
                            else
                                normalString = string.Format("({3}-{4}h) (M: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    ctxn.FromTime.Value, ctxn.ToTime.Value);
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");

                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({3}{4}h) (M {5}-{6}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({3}{4}h) (M >{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({3}{4}h) (M <{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    op, ctxn.FromTime.Value, ctxn.ToAge.Value);
                            else
                                normalString = string.Format("({3}{4}h) (M: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    op, ctxn.FromTime.Value);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({3}{4}h) (M {5}-{6}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({3}{4}h) (M >{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({3}{4}h) (M <{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.ToAge.Value);
                            else
                                normalString = string.Format("({3}{4}h) (M: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi, 
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value);
                        }
                        break;

                    case DoiTuong.Sang_Nu:
                    case DoiTuong.Chieu_Nu:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({3}-{4}h) (F {5}-{6}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({3}-{4}h) (F >{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({3}-{4}h) (F <{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.ToAge.Value);
                            else
                                normalString = string.Format("({3}-{4}h) (F: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value);
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");

                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({3}{4}h) (F {5}-{6}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({3}{4}h) (F >{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({3}{4}h) (F <{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.ToAge.Value);
                            else
                                normalString = string.Format("({3}{4}h) (F: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({3}{4}h) (F {5}-{6}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({3}{4}h) (F >{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({3}{4}h) (F <{5}: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.ToAge.Value);
                            else
                                normalString = string.Format("({3}{4}h) (F: {0}-{1} {2})", ctxn.FromValue.Value, ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value);
                        }
                        break;
                }
            }
            else if (ctxn.FromValue.HasValue)
            {
                tinhTrang = TinhTrang.BinhThuong;

                if (ctxn.FromOperator == "<" && testResult <= ctxn.FromValue.Value)
                    tinhTrang = TinhTrang.BatThuong;
                else if (ctxn.FromOperator == "<=" && testResult < ctxn.FromValue.Value)
                    tinhTrang = TinhTrang.BatThuong;

                string fOp = ctxn.FromOperator.Replace("<=", ">=").Replace("<", ">");

                switch (doiTuong)
                {
                    case DoiTuong.Chung:
                        normalString = string.Format("({2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;
                    case DoiTuong.Nam:
                        if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                            normalString = string.Format("(M {2}-{3}: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                        else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                            normalString = string.Format("(M >{2}: {3}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.FromAge.Value, fOp);
                        else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                            normalString = string.Format("(M <{2}: {3}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.ToAge.Value, fOp);
                        else
                            normalString = string.Format("(M {2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;
                    case DoiTuong.Nu:
                        if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                            normalString = string.Format("(F {2}-{3}: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                        else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                            normalString = string.Format("(F >{2}: {3}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.FromAge.Value, fOp);
                        else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                            normalString = string.Format("(F <{2}: {3}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.ToAge.Value, fOp);
                        else
                            normalString = string.Format("(F {2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;
                    case DoiTuong.NguoiLon:
                        normalString = string.Format("Adult ({2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;
                    case DoiTuong.NguoiCaoTuoi:
                        normalString = string.Format(">60 year ({2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;
                    case DoiTuong.TreEm:
                        normalString = string.Format("Child ({2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;

                    case DoiTuong.AmTinhDuongTinh:
                        normalString = string.Format("(Negative {2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;

                    case DoiTuong.HutThuoc:
                        normalString = string.Format("(Smoke {2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;

                    case DoiTuong.KhongHutThuoc:
                        normalString = string.Format("(Nosmoke {2}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, fOp);
                        break;

                    case DoiTuong.Sang_Chung:
                    case DoiTuong.Chieu_Chung:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                            normalString = string.Format("({2}-{3}h: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.FromTime.Value, ctxn.ToTime.Value, fOp);
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");
                            normalString = string.Format("({2}{3}h: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, op, ctxn.FromTime.Value, fOp);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                            normalString = string.Format("({2}{3}h: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi, ctxn.ToTimeOperator, ctxn.FromTime.Value, fOp);
                        break;

                    case DoiTuong.Sang_Nam:
                    case DoiTuong.Chieu_Nam:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}-{3}h) (M {4}-{5}: {6}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (M >{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, fOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (M <{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.ToAge.Value, fOp);
                            else
                                normalString = string.Format("({2}-{3}h) (M: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, fOp);
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");

                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (M {4}-{5}: {6}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M >{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, fOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M <{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.ToAge.Value, fOp);
                            else
                                normalString = string.Format("({2}{3}h) (M: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, fOp);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (M {4}-{5}: {6}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M >{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, fOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M <{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.ToAge.Value, fOp);
                            else
                                normalString = string.Format("({2}{3}h) (M: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, fOp);
                        }
                        break;

                    case DoiTuong.Sang_Nu:
                    case DoiTuong.Chieu_Nu:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}-{3}h) (F {4}-{5}: {6}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (F >{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, fOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (F <{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.ToAge.Value, fOp);
                            else
                                normalString = string.Format("({2}-{3}h) (F: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, fOp);
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");

                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (F {4}-{5}: {6}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F >{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, fOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F <{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.ToAge.Value, fOp);
                            else
                                normalString = string.Format("({2}{3}h) (F: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, fOp);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (F {4}-{5}: {6}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, fOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F >{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, fOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F <{4}: {5}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.ToAge.Value, fOp);
                            else
                                normalString = string.Format("({2}{3}h) (F: {4}{0} {1})", ctxn.FromValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, fOp);
                        }
                        break;
                }
            }
            else if (ctxn.ToValue.HasValue)
            {
                tinhTrang = TinhTrang.BinhThuong;

                if (ctxn.ToOperator == "<" && testResult >= ctxn.ToValue.Value)
                    tinhTrang = TinhTrang.BatThuong;
                else if (ctxn.ToOperator == "<=" && testResult > ctxn.ToValue.Value)
                    tinhTrang = TinhTrang.BatThuong;

                string tOp = ctxn.ToOperator;

                switch (doiTuong)
                {
                    case DoiTuong.Chung:
                        normalString = string.Format("({2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;
                    case DoiTuong.Nam:
                        if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                            normalString = string.Format("(M {2}-{3}: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                        else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                            normalString = string.Format("(M >{2}: {3}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value, tOp);
                        else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                            normalString = string.Format("(M <{2}: {3}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.ToAge.Value, tOp);
                        else
                            normalString = string.Format("(M {2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;
                    case DoiTuong.Nu:
                        if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                            normalString = string.Format("(F {2}-{3}: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                        else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                            normalString = string.Format("(F >{2}: {3}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromAge.Value, tOp);
                        else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                            normalString = string.Format("(F <{2}: {3}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.ToAge.Value, tOp);
                        else
                            normalString = string.Format("(F {2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;
                    case DoiTuong.NguoiLon:
                        normalString = string.Format("Adult ({2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;
                    case DoiTuong.NguoiCaoTuoi:
                        normalString = string.Format(">60 year ({2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;
                    case DoiTuong.TreEm:
                        normalString = string.Format("Child ({2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;

                    case DoiTuong.AmTinhDuongTinh:
                        normalString = string.Format("(Negative {2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;

                    case DoiTuong.HutThuoc:
                        normalString = string.Format("(Smoke {2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;

                    case DoiTuong.KhongHutThuoc:
                        normalString = string.Format("(Nosmoke {2}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, tOp);
                        break;

                    case DoiTuong.Sang_Chung:
                    case DoiTuong.Chieu_Chung:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                            normalString = string.Format("({2}-{3}h: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.FromTime.Value, ctxn.ToTime.Value, tOp);
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");
                            normalString = string.Format("({2}{3}h: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, op, ctxn.FromTime.Value, tOp);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                            normalString = string.Format("({2}{3}h: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi, ctxn.ToTimeOperator, ctxn.FromTime.Value, tOp);
                        break;

                    case DoiTuong.Sang_Nam:
                    case DoiTuong.Chieu_Nam:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}-{3}h) (M {4}-{5}: {6}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (M >{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, tOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (M <{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.ToAge.Value, tOp);
                            else
                                normalString = string.Format("({2}-{3}h) (M: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, tOp);
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");

                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (M {4}-{5}: {6}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M >{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, tOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M <{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.ToAge.Value, tOp);
                            else
                                normalString = string.Format("({2}{3}h) (M: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, tOp);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (M {4}-{5}: {6}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M >{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, tOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (M <{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.ToAge.Value, tOp);
                            else
                                normalString = string.Format("({2}{3}h) (M: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, tOp);
                        }
                        break;

                    case DoiTuong.Sang_Nu:
                    case DoiTuong.Chieu_Nu:
                        if (ctxn.FromTime != null && ctxn.FromTime.HasValue && ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}-{3}h) (F {4}-{5}: {6}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (F >{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.FromAge.Value, tOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}-{3}h) (F <{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, ctxn.ToAge.Value, tOp);
                            else
                                normalString = string.Format("({2}-{3}h) (F: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.FromTime.Value, ctxn.ToTime.Value, tOp);
                        }
                        else if (ctxn.FromTime != null && ctxn.FromTime.HasValue)
                        {
                            string op = ctxn.FromTimeOperator.Replace("<=", ">=").Replace("<", ">");

                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (F {4}-{5}: {6}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F >{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.FromAge.Value, tOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F <{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, ctxn.ToAge.Value, tOp);
                            else
                                normalString = string.Format("({2}{3}h) (F: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    op, ctxn.FromTime.Value, tOp);
                        }
                        else if (ctxn.ToTime != null && ctxn.ToTime.HasValue)
                        {
                            if ((ctxn.FromAge != null && ctxn.FromAge.HasValue) && (ctxn.ToAge != null && ctxn.ToAge.HasValue))
                                normalString = string.Format("({2}{3}h) (F {4}-{5}: {6}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, ctxn.ToAge.Value, tOp);
                            else if (ctxn.FromAge != null && ctxn.FromAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F >{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.FromAge.Value, tOp);
                            else if (ctxn.ToAge != null && ctxn.ToAge.HasValue)
                                normalString = string.Format("({2}{3}h) (F <{4}: {5}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, ctxn.ToAge.Value, tOp);
                            else
                                normalString = string.Format("({2}{3}h) (F: {4}{0} {1})", ctxn.ToValue.Value, ctxn.DonVi,
                                    ctxn.ToTimeOperator, ctxn.ToTime.Value, tOp);
                        }
                        break;
                }
            }
            else
            {
                tinhTrang = TinhTrang.BinhThuong;

                if (ctxn.XValue != null && ctxn.XValue.HasValue)
                    normalString = string.Format("/ {0}X", ctxn.XValue.Value);
                else if (ctxn.DonVi != null && ctxn.DonVi.Trim() != string.Empty)
                    normalString = ctxn.DonVi;
            }

            return normalString;
        }

        public static Result GetChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID)
        {
            return GetChiTietKetQuaXetNghiem(ketQuaXetNghiemGUID, Global.MinDateTime, Global.MaxDateTime);
        }

        public static Result GetChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();

            try
            {
                string query = query = string.Format("SELECT CAST(0 AS Bit) AS Checked, convert(varchar(10), GetDate(), 103) + ' ' + convert(varchar(10), GetDate(), 108) as NgayXNStr, *, CAST('' AS nvarchar(50)) AS BinhThuong FROM ChiTietKetQuaXetNghiem_ManualView WITH(NOLOCK) WHERE KetQuaXetNghiem_ManualGUID = '{0}' AND Status = {1} AND NgayXetNghiem BETWEEN '{2}' AND '{3}' ORDER BY NgayXetNghiem DESC, GroupID, [Order]",
                           ketQuaXetNghiemGUID, (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));

                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                MMOverride db = new MMOverride();
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
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

                        continue;
                    }

                    ChiTietXetNghiem_Manual ctxn = GetChiTietXetNghiem_Manual(db, row, ref isUpdate);
                    if (ctxn == null) continue;
                    
                    TinhTrang tinhTrang = TinhTrang.BinhThuong;
                    string normalStr = GetNormalString(testResult, ctxn, ref tinhTrang);
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
                        ChiTietKetQuaXetNghiem_Manual ctkqxn = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(c => c.ChiTietKetQuaXetNghiem_ManualGUID.ToString() == row["ChiTietKetQuaXetNghiem_ManualGUID"].ToString());
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

                            desc += string.Format("- GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}'\n",
                                    kqxn.KetQuaXetNghiemManualGUID.ToString(), fileNum, tenBenhNhan);
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
                    tk.ComputerName = Utility.GetDNSHostName();
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

        public static Result DeleteChiTietXetNghiem(List<string> keys)
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
                        ChiTietKetQuaXetNghiem_Manual kqxn = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(k => k.ChiTietKetQuaXetNghiem_ManualGUID.ToString() == key);

                        if (kqxn != null)
                        {
                            kqxn.DeletedDate = DateTime.Now;
                            kqxn.DeletedBy = Guid.Parse(Global.UserGUID);
                            kqxn.Status = (byte)Status.Deactived;

                            string tenBenhNhan = string.Empty;
                            string fileNum = string.Empty;
                            if (kqxn.KetQuaXetNghiem_Manual.PatientGUID != null)
                            {
                                PatientView patient = db.PatientViews.SingleOrDefault<PatientView>(p => p.PatientGUID == kqxn.KetQuaXetNghiem_Manual.PatientGUID);
                                if (patient != null)
                                {
                                    fileNum = patient.FileNum;
                                    tenBenhNhan = patient.FullName;
                                }
                            }

                           
                            desc += string.Format("- GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}', Ngày xét nghiệm: '{3}', Tên xét nghiệm: '{4}'\n",
                                    kqxn.ChiTietKetQuaXetNghiem_ManualGUID.ToString(), fileNum, tenBenhNhan, 
                                    kqxn.NgayXetNghiem.ToString("dd/MM/yyyy HH:mm:ss"), kqxn.XetNghiem_Manual.Fullname);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa chi tiết xét nghiệm tay";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
                    tk.ComputerName = Utility.GetDNSHostName();
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

        public static Result InsertKQXN(KetQuaXetNghiem_Manual kqxn, List<ChiTietKetQuaXetNghiem_Manual> ctkqxns, List<string> deletedKeys)
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

                        desc += string.Format("- Kết quả xét nghiệm tay: GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}''\n",
                                    kqxn.KetQuaXetNghiemManualGUID.ToString(), fileNum, tenBenhNhan);

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

                            desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}, Ngày xét nghiệm: '{3}'\n", ct.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                                ct.XetNghiem_Manual.Fullname, ct.TestResult, ct.NgayXetNghiem.ToString("dd/MM/yyyy HH:mm:ss"));
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
                        tk.ComputerName = Utility.GetDNSHostName();
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

                            desc += string.Format("- Kết quả xét nghiệm tay: GUID: '{0}', Mã bệnh nhân: '{1}', Tên bệnh nhân: '{2}'\n",
                                    ketQuaXN.KetQuaXetNghiemManualGUID.ToString(), fileNum, tenBenhNhan);

                            desc += "- Chi tiết kết quả xét nghiệm tay:\n";
                            //Chi tiết
                            //var chiTietKQXNs = ketQuaXN.ChiTietKetQuaXetNghiem_Manuals;
                            //foreach (ChiTietKetQuaXetNghiem_Manual ct in chiTietKQXNs)
                            //{
                            //    ct.UpdatedDate = DateTime.Now;
                            //    ct.UpdatedBy = Guid.Parse(Global.UserGUID);
                            //    ct.Status = (byte)Status.Deactived;
                            //}

                            foreach (string key in deletedKeys)
                            {
                                ChiTietKetQuaXetNghiem_Manual ct = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault(c => c.ChiTietKetQuaXetNghiem_ManualGUID.ToString() == key);
                                if (ct == null) continue;

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

                                    desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}, Ngày xét nghiệm: '{3}'\n", ct.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                                        ct.XetNghiem_Manual.Fullname, ct.TestResult, ct.NgayXetNghiem.ToString("dd/MM/yyyy HH:mm:ss"));
                                }
                                else
                                {
                                    chiTietKQXN.TestResult = ct.TestResult;
                                    chiTietKQXN.FromValue = null;
                                    chiTietKQXN.ToValue = null;
                                    chiTietKQXN.DoiTuong = null;
                                    chiTietKQXN.DonVi = null;
                                    chiTietKQXN.FromOperator = null;
                                    chiTietKQXN.ToOperator = null;
                                    chiTietKQXN.FromAge = null;
                                    chiTietKQXN.ToAge = null;
                                    chiTietKQXN.FromTime = null;
                                    chiTietKQXN.FromTime = null;
                                    chiTietKQXN.FromTimeOperator = null;
                                    chiTietKQXN.ToTimeOperator = null;
                                    chiTietKQXN.XValue = null;
                                    chiTietKQXN.LamThem = ct.LamThem;
                                    chiTietKQXN.HasHutThuoc = ct.HasHutThuoc;
                                    chiTietKQXN.NgayXetNghiem = ct.NgayXetNghiem;
                                    chiTietKQXN.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    chiTietKQXN.UpdatedDate = DateTime.Now;
                                    chiTietKQXN.Status = (byte)Status.Actived;

                                    desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}, Ngày xét nghiệm: '{3}'\n", chiTietKQXN.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                                        chiTietKQXN.XetNghiem_Manual.Fullname, chiTietKQXN.TestResult, chiTietKQXN.NgayXetNghiem.ToString("dd/MM/yyyy HH:mm:ss"));
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
                            tk.ComputerName = Utility.GetDNSHostName();
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

        public static Result InsertChiTietKQXN(ChiTietKetQuaXetNghiem_Manual ctkqxn)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    desc += "Chi tiết kết quả xét nghiệm tay:\n";
                    //Chi tiết
                    ctkqxn.CreatedDate = DateTime.Now;
                    ctkqxn.CreatedBy = Guid.Parse(Global.UserGUID);
                    ctkqxn.Status = (byte)Status.Actived;
                    db.ChiTietKetQuaXetNghiem_Manuals.InsertOnSubmit(ctkqxn);
                    db.SubmitChanges();

                    desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}, Ngày xét nghiệm: '{3}'", ctkqxn.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                        ctkqxn.XetNghiem_Manual.Fullname, ctkqxn.TestResult, ctkqxn.NgayXetNghiem.ToString("dd/MM/yyyy HH:mm:ss"));

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
                    tk.ComputerName = Utility.GetDNSHostName();
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

        public static Result UpdateChiTietKQXN(ChiTietKetQuaXetNghiem_Manual ctkqxn)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    ChiTietKetQuaXetNghiem_Manual ct = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(c => c.ChiTietKetQuaXetNghiem_ManualGUID == ctkqxn.ChiTietKetQuaXetNghiem_ManualGUID);
                    if (ct != null)
                    {
                        desc += "Chi tiết kết quả xét nghiệm tay:\n";
                        //Chi tiết
                        ct.TestResult = ctkqxn.TestResult;
                        ct.FromValue = ctkqxn.FromValue;
                        ct.ToValue = ctkqxn.ToValue;
                        ct.DonVi = ctkqxn.DonVi;
                        ct.FromOperator = ctkqxn.FromOperator;
                        ct.ToOperator = ctkqxn.ToOperator;
                        //ct.FromAge = ctkqxn.FromAge;
                        //ct.ToAge = ctkqxn.ToAge;
                        //ct.FromTime = ctkqxn.FromTime;
                        //ct.FromTime = ctkqxn.FromTime;
                        //ct.FromTimeOperator = ctkqxn.FromTimeOperator;
                        //ct.ToTimeOperator = ctkqxn.ToTimeOperator;
                        ct.XValue = ctkqxn.XValue;
                        ct.LamThem = ctkqxn.LamThem;
                        ct.HasHutThuoc = ctkqxn.HasHutThuoc;
                        ct.NgayXetNghiem = ctkqxn.NgayXetNghiem;
                        ct.UpdatedDate = DateTime.Now;
                        ct.UpdatedBy = Guid.Parse(Global.UserGUID);
                        ct.Status = (byte)Status.Actived;

                        desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Kết quả: {2}, Ngày xét nghiệm: '{3}'", ct.ChiTietKetQuaXetNghiem_ManualGUID.ToString(),
                            ct.XetNghiem_Manual.Fullname, ct.TestResult, ct.NgayXetNghiem.ToString("dd/MM/yyyy HH:mm:ss"));

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
                        tk.ComputerName = Utility.GetDNSHostName();
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
                        ChiTietKetQuaXetNghiem_Manual ctkqxn = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(c => c.ChiTietKetQuaXetNghiem_ManualGUID.ToString() == key);
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
                        ChiTietKetQuaXetNghiem_Manual ctkqxn = db.ChiTietKetQuaXetNghiem_Manuals.SingleOrDefault<ChiTietKetQuaXetNghiem_Manual>(c => c.ChiTietKetQuaXetNghiem_ManualGUID.ToString() == key);
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
