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
    public class NhapKhoCapCuuBus : BusBase
    {
        public static Result GetNhapKhoCapCuuList()
        {
            Result result = null;

            try
            {

                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhapKhoCapCuuView WITH(NOLOCK) WHERE NhapKhoCapCuuStatus={0} AND KhoCapCuuStatus={0} ORDER BY NgayNhap DESC", 
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

        public static Result GetNhapKhoCapCuuList(string tenCapCuu, DateTime tuNgay, DateTime denNgay, bool isTenCapCuu)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (isTenCapCuu)
                {
                    if (tenCapCuu.Trim() == string.Empty)
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhapKhoCapCuuView WITH(NOLOCK) WHERE NhapKhoCapCuuStatus={0} AND KhoCapCuuStatus={0} ORDER BY NgayNhap DESC",
                        (byte)Status.Actived);
                    }
                    else
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhapKhoCapCuuView WITH(NOLOCK) WHERE TenCapCuu LIKE N'{0}%' AND NhapKhoCapCuuStatus={1} AND KhoCapCuuStatus={1} ORDER BY NgayNhap DESC",
                        tenCapCuu, (byte)Status.Actived);
                    }
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhapKhoCapCuuView WITH(NOLOCK) WHERE NgayNhap BETWEEN '{0}' AND '{1}' AND NhapKhoCapCuuStatus={2} AND KhoCapCuuStatus={2} ORDER BY NgayNhap DESC",
                        tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), (byte)Status.Actived);
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

        public static Result CheckKhoCapCuuHetHan()
        {
            Result result = new Result();

            try
            {
                DateTime dt = DateTime.Now;

                string query = string.Format("SELECT TOP 1 N.* FROM KhoCapCuu K WITH(NOLOCK), NhapKhoCapCuu N WITH(NOLOCK) WHERE K.KhoCapCuuGUID = N.KhoCapCuuGUID AND K.Status = 0 AND N.Status = 0 AND N.SoLuongNhap * N.SoLuongQuiDoi - N.SoLuongXuat > 0 AND  DATEDIFF(day, '{0}', NgayHetHan) <= {1}",
                    dt.ToString("yyyy-MM-dd"), Global.AlertSoNgayHetHanCapCuu);

                result = ExcuteQuery(query);
                if (!result.IsOK)
                {
                    result.QueryResult = false;
                    return result;
                }

                DataTable dtResult = result.QueryResult as DataTable;
                if (dtResult != null && dtResult.Rows.Count > 0)
                    result.QueryResult = true;
                else
                    result.QueryResult = false;

                return result;
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

        public static Result CheckKhoCapCuuHetHan(string khoCapCuuGUID)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                DateTime dt = DateTime.Now;
                db = new MMOverride();
                List<KhoCapCuu> results = (from t in db.KhoCapCuus
                                           join l in db.NhapKhoCapCuus on t.KhoCapCuuGUID equals l.KhoCapCuuGUID
                                           where t.Status == (byte)Status.Actived && l.Status == (byte)Status.Actived &&
                                           l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0 &&
                                           new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                           t.KhoCapCuuGUID.ToString() == khoCapCuuGUID
                                           select t).ToList<KhoCapCuu>();

                if (results != null && results.Count > 0)
                    result.QueryResult = false;
                else
                    result.QueryResult = true;
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

        public static Result GetNgayHetHanCuaKhoCapCuu(string khoCapCuuGUID)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                DateTime dt = DateTime.Now;
                db = new MMOverride();
                NhapKhoCapCuu loThuoc = (from t in db.KhoCapCuus
                                        join l in db.NhapKhoCapCuus on t.KhoCapCuuGUID equals l.KhoCapCuuGUID
                                        where t.Status == (byte)Status.Actived && l.Status == (byte)Status.Actived &&
                                        t.KhoCapCuuGUID.ToString() == khoCapCuuGUID
                                        orderby new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) descending, 
                                        l.NgayNhap descending
                                        select l).FirstOrDefault<NhapKhoCapCuu>();

                if (loThuoc != null)
                    result.QueryResult = loThuoc.NgayHetHan;
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

        public static Result GetKhoCapCuuTonKho(string khoCapCuuGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime dt = DateTime.Now;
                List<NhapKhoCapCuu> loThuocList = (from l in db.NhapKhoCapCuus
                                             where l.Status == (byte)Status.Actived &&
                                             l.KhoCapCuuGUID.ToString() == khoCapCuuGUID &&
                                             new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                             l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                             select l).ToList<NhapKhoCapCuu>();

                if (loThuocList != null && loThuocList.Count > 0)
                {
                    int tongSLNhap = 0;
                    int tongSLXuat = 0;

                    foreach (NhapKhoCapCuu lt in loThuocList)
                    {
                        tongSLNhap += lt.SoLuongNhap * lt.SoLuongQuiDoi;
                        tongSLXuat += lt.SoLuongXuat;
                    }

                    result.QueryResult = tongSLNhap - tongSLXuat;
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

        public static Result CheckKhoCapCuuTonKho()
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime dt = DateTime.Now;

                NhapKhoCapCuu nkcc = (from l in db.NhapKhoCapCuus
                                      where l.Status == (byte)Status.Actived &&
                                      new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                      l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat <= Global.AlertSoLuongHetTonKhoCapCuu
                                      select l).FirstOrDefault();

                result.QueryResult = nkcc != null ? true: false;
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

        public static Result CheckKhoCapCuuTonKho(string khoCapCuuGUID, int soLuong)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime dt = DateTime.Now;

                List<NhapKhoCapCuu> loThuocList = (from l in db.NhapKhoCapCuus
                                            where l.Status == (byte)Status.Actived && 
                                            l.KhoCapCuuGUID.ToString() == khoCapCuuGUID &&
                                            new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                            l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                            select l).ToList<NhapKhoCapCuu>();

                if (loThuocList == null || loThuocList.Count <= 0)
                    result.QueryResult = false;
                else
                {
                    int tongSLNhap = 0;
                    int tongSLXuat = 0;

                    foreach (NhapKhoCapCuu lt in loThuocList)
                    {
                        tongSLNhap += lt.SoLuongNhap * lt.SoLuongQuiDoi;
                        tongSLXuat += lt.SoLuongXuat;
                    }

                    if (tongSLNhap >= tongSLXuat + soLuong)
                        result.QueryResult = true;
                    else
                        result.QueryResult = false;
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

        public static Result GetNhaPhanPhoiList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT NhaPhanPhoi FROM NhapKhoCapCuu WITH(NOLOCK) ORDER BY NhaPhanPhoi",
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

        public static Result DeleteNhapKhoCappCuu(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        NhapKhoCapCuu loThuoc = db.NhapKhoCapCuus.SingleOrDefault<NhapKhoCapCuu>(l => l.NhapKhoCapCuuGUID.ToString() == key);
                        if (loThuoc != null)
                        {
                            loThuoc.DeletedDate = DateTime.Now;
                            loThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            loThuoc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Ngày nhập: '{1}', Tên cấp cứu: '{2}', Số đăng ký: '{3}', Hãng SX: '{4}', Ngày SX: '{5}', Ngày hết hạn: '{6}', Nhà phân phối: '{7}', SL nhập: '{8}', ĐVT nhập: '{9}', Giá nhập: '{10}', SL qui đổi: '{11}', ĐVT qui đổi: '{12}', Giá nhập qui đổi: '{13}', SL xuất: '{14}'\n",
                                loThuoc.NhapKhoCapCuuGUID.ToString(), loThuoc.NgayNhap.ToString("dd/MM/yyyy HH:mm:ss"), loThuoc.KhoCapCuu.TenCapCuu, loThuoc.SoDangKy, loThuoc.HangSanXuat,
                                loThuoc.NgaySanXuat.Value.ToString("dd/MM/yyyy"), loThuoc.NgayHetHan.Value.ToString("dd/MM/yyyy"), loThuoc.NhaPhanPhoi, loThuoc.SoLuongNhap, 
                                loThuoc.DonViTinhNhap, loThuoc.GiaNhap, loThuoc.SoLuongQuiDoi, loThuoc.DonViTinhQuiDoi, loThuoc.GiaNhapQuiDoi, loThuoc.SoLuongXuat);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin nhập kho cấp cứu";
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

        public static Result InsertNhapKhoCapCuu(NhapKhoCapCuu loThuoc)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;

                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (loThuoc.NhapKhoCapCuuGUID == null || loThuoc.NhapKhoCapCuuGUID == Guid.Empty)
                    {
                        loThuoc.NhapKhoCapCuuGUID = Guid.NewGuid();
                        db.NhapKhoCapCuus.InsertOnSubmit(loThuoc);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày nhập: '{1}', Tên cấp cứu: '{2}', Số đăng ký: '{3}', Hãng SX: '{4}', Ngày SX: '{5}', Ngày hết hạn: '{6}', Nhà phân phối: '{7}', SL nhập: '{8}', ĐVT nhập: '{9}', Giá nhập: '{10}', SL qui đổi: '{11}', ĐVT qui đổi: '{12}', Giá nhập qui đổi: '{13}', SL xuất: '{14}'",
                                loThuoc.NhapKhoCapCuuGUID.ToString(), loThuoc.NgayNhap.ToString("dd/MM/yyyy HH:mm:ss"), loThuoc.KhoCapCuu.TenCapCuu, loThuoc.SoDangKy, loThuoc.HangSanXuat,
                                loThuoc.NgaySanXuat.Value.ToString("dd/MM/yyyy"), loThuoc.NgayHetHan.Value.ToString("dd/MM/yyyy"), loThuoc.NhaPhanPhoi, loThuoc.SoLuongNhap,
                                loThuoc.DonViTinhNhap, loThuoc.GiaNhap, loThuoc.SoLuongQuiDoi, loThuoc.DonViTinhQuiDoi, loThuoc.GiaNhapQuiDoi, loThuoc.SoLuongXuat);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin nhập kho cấp cứu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        NhapKhoCapCuu lt = db.NhapKhoCapCuus.SingleOrDefault<NhapKhoCapCuu>(l => l.NhapKhoCapCuuGUID == loThuoc.NhapKhoCapCuuGUID);
                        if (lt != null)
                        {
                            int soLuongNhapCu = lt.SoLuongNhap;
                            int soLuongQuiDoiCu = lt.SoLuongQuiDoi;

                            lt.KhoCapCuuGUID = loThuoc.KhoCapCuuGUID;
                            lt.NgayNhap = loThuoc.NgayNhap;
                            lt.SoDangKy = loThuoc.SoDangKy;
                            lt.HangSanXuat = loThuoc.HangSanXuat;
                            lt.NgaySanXuat = loThuoc.NgaySanXuat;
                            lt.NgayHetHan = loThuoc.NgayHetHan;
                            lt.NhaPhanPhoi = loThuoc.NhaPhanPhoi;
                            lt.SoLuongNhap = loThuoc.SoLuongNhap;
                            lt.DonViTinhNhap = loThuoc.DonViTinhNhap;
                            lt.GiaNhap = loThuoc.GiaNhap;
                            lt.SoLuongQuiDoi = loThuoc.SoLuongQuiDoi;
                            lt.DonViTinhQuiDoi = loThuoc.DonViTinhQuiDoi;
                            lt.GiaNhapQuiDoi = loThuoc.GiaNhapQuiDoi;
                            lt.Note = loThuoc.Note;
                            lt.CreatedDate = loThuoc.CreatedDate;
                            lt.CreatedBy = loThuoc.CreatedBy;
                            lt.UpdatedDate = loThuoc.UpdatedDate;
                            lt.UpdatedBy = loThuoc.UpdatedBy;
                            lt.DeletedDate = loThuoc.DeletedDate;
                            lt.DeletedBy = loThuoc.DeletedBy;
                            lt.Status = loThuoc.Status;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Ngày nhập: '{1}', Tên cấp cứu: '{2}', Số đăng ký: '{3}', Hãng SX: '{4}', Ngày SX: '{5}', Ngày hết hạn: '{6}', Nhà phân phối: '{7}', SL nhập: cũ: '{8}' - mới: '{9}', ĐVT nhập: '{10}', SL qui đổi: cũ: '{11}' - mới: '{12}', ĐVT qui đổi: '{13}', SL xuất: '{14}'",
                                    lt.NhapKhoCapCuuGUID.ToString(), lt.NgayNhap.ToString("dd/MM/yyyy HH:mm:ss"), lt.KhoCapCuu.TenCapCuu, lt.SoDangKy, lt.HangSanXuat,
                                    lt.NgaySanXuat.Value.ToString("dd/MM/yyyy"), lt.NgayHetHan.Value.ToString("dd/MM/yyyy"), lt.NhaPhanPhoi, soLuongNhapCu,
                                    lt.SoLuongNhap, lt.DonViTinhNhap, soLuongQuiDoiCu, lt.SoLuongQuiDoi, lt.DonViTinhQuiDoi, lt.SoLuongXuat);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin nhập kho cấp cứu";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

                    tnx.Complete();
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

        public static Result GetNhapKhoCapCuu(string nhapKhoCapCuuGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                NhapKhoCapCuuView nkcc = db.NhapKhoCapCuuViews.SingleOrDefault(n => n.NhapKhoCapCuuGUID.ToString() == nhapKhoCapCuuGUID);
                result.QueryResult = nkcc;
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

        public static Result GetGiaCapCuuNhap(string khoCapCuuGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                NhapKhoCapCuu loThuoc = (from l in db.NhapKhoCapCuus
                                         where l.KhoCapCuuGUID.ToString() == khoCapCuuGUID &&
                                         l.Status == (byte)Status.Actived
                                         orderby l.NgayNhap descending
                                         select l).FirstOrDefault();

                if (loThuoc != null)
                    result.QueryResult = loThuoc.GiaNhapQuiDoi;
                else
                    result.QueryResult = 0;
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
