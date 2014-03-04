using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using System.Data.Linq;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class ThongTinKhachHangBus : BusBase
    {
        public static Result GetThongTinKhachHangList()
        {
            Result result = null;

            try
            {
                string query = "SELECT ThongTinKhachHangGUID, TenKhachHang FROM ThongTinKhachHang WITH(NOLOCK) ORDER BY TenKhachHang";
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

        public static Result GetTenDonViList()
        {
            Result result = null;

            try
            {
                string query = "SELECT DISTINCT TenDonVi FROM ThongTinKhachHang WITH(NOLOCK) UNION SELECT TenCty As TenDonVi FROM Company WITH(NOLOCK) WHERE [Status] = 0 ORDER BY TenDonVi";
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

        public static Result GetMaDonViList()
        {
            Result result = null;

            try
            {
                string query = "SELECT DISTINCT MaDonVi FROM ThongTinKhachHang WITH(NOLOCK) UNION SELECT MaCty As MaDonVi FROM Company WITH(NOLOCK) WHERE [Status] = 0 ORDER BY MaDonVi";
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

        public static Result GetThongTinKhachHang(string thongTinKhachHangGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ThongTinKhachHang ttkh = db.ThongTinKhachHangs.FirstOrDefault(t => t.ThongTinKhachHangGUID.ToString().ToLower() == thongTinKhachHangGUID.ToLower());
                result.QueryResult = ttkh;
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

        public static Result GetThongTinMaDonVi(string maDonVi)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                if (maDonVi == null) maDonVi = string.Empty;
                db = new MMOverride();
                ThongTinKhachHang thongTinKhachHang = null;
                Company company = db.Companies.FirstOrDefault(t => t.MaCty.Trim().ToLower() == maDonVi.Trim().ToLower() &&
                    t.Status == (byte)Status.Actived);
                if (company != null)
                {
                    thongTinKhachHang = new ThongTinKhachHang();
                    thongTinKhachHang.MaDonVi = company.MaCty;
                    thongTinKhachHang.TenDonVi = company.TenCty;
                    thongTinKhachHang.DiaChi = company.DiaChi;
                    thongTinKhachHang.MaSoThue = company.MaSoThue;
                    thongTinKhachHang.HinhThucThanhToan = 0;

                    ThongTinKhachHang ttkh = db.ThongTinKhachHangs.FirstOrDefault(t => t.MaDonVi.Trim().ToLower() == maDonVi.Trim().ToLower());
                    if (ttkh != null)
                    {
                        thongTinKhachHang.HinhThucThanhToan = ttkh.HinhThucThanhToan;
                        thongTinKhachHang.SoTaiKhoan = ttkh.SoTaiKhoan;
                    }    
                }

                result.QueryResult = thongTinKhachHang;
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

        public static Result GetThongTinDonVi(string tenDonVi)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                if (tenDonVi == null) tenDonVi = string.Empty;
                db = new MMOverride();
                ThongTinKhachHang thongTinKhachHang = null;
                Company company = db.Companies.FirstOrDefault(t => t.TenCty.Trim().ToLower() == tenDonVi.Trim().ToLower() &&
                    t.Status == (byte)Status.Actived);
                if (company != null)
                {
                    thongTinKhachHang = new ThongTinKhachHang();
                    thongTinKhachHang.MaDonVi = company.MaCty;
                    thongTinKhachHang.TenDonVi = company.TenCty;
                    thongTinKhachHang.DiaChi = company.DiaChi;
                    thongTinKhachHang.MaSoThue = company.MaSoThue;
                    thongTinKhachHang.HinhThucThanhToan = 0;

                    ThongTinKhachHang ttkh = db.ThongTinKhachHangs.FirstOrDefault(t => t.TenDonVi.Trim().ToLower() == tenDonVi.Trim().ToLower());
                    if (ttkh != null)
                    {
                        thongTinKhachHang.HinhThucThanhToan = ttkh.HinhThucThanhToan;
                        thongTinKhachHang.SoTaiKhoan = ttkh.SoTaiKhoan;
                    }
                }

                result.QueryResult = thongTinKhachHang;
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

        public static Result InsertThongTinKhachHang(ThongTinKhachHang ttkh)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    ThongTinKhachHang thongTinKhachHang = (from tt in db.ThongTinKhachHangs
                                                           where tt.TenKhachHang.Trim().ToLower() == ttkh.TenKhachHang.Trim().ToLower() &&
                                                           tt.MaDonVi.Trim().ToLower() == ttkh.MaDonVi.Trim().ToLower()
                                                           //tt.TenDonVi.Trim().ToLower() == ttkh.TenDonVi.Trim().ToLower() &&
                                                           //tt.DiaChi.Trim().ToLower() == ttkh.DiaChi.Trim().ToLower()
                                                           select tt).FirstOrDefault();

                    if (thongTinKhachHang == null)
                    {
                        ttkh.ThongTinKhachHangGUID = Guid.NewGuid();
                        db.ThongTinKhachHangs.InsertOnSubmit(ttkh);
                    }
                    else
                    {
                        //thongTinKhachHang.MaDonVi = ttkh.MaDonVi;
                        //thongTinKhachHang.TenDonVi = ttkh.TenDonVi;
                        //thongTinKhachHang.MaSoThue = ttkh.MaSoThue;
                        //thongTinKhachHang.DiaChi = ttkh.DiaChi;
                        thongTinKhachHang.SoTaiKhoan = ttkh.SoTaiKhoan;
                        thongTinKhachHang.HinhThucThanhToan = ttkh.HinhThucThanhToan;
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

        public static Result DeleteTenKhachHang(string thongTinKhachHangGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ThongTinKhachHang ttkh = db.ThongTinKhachHangs.FirstOrDefault(t => t.ThongTinKhachHangGUID.ToString().ToLower() == thongTinKhachHangGUID.ToLower());
                if (ttkh != null)
                    ttkh.TenKhachHang = string.Empty;

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

        public static Result DeleteMaDonVi(string maDonVi)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ThongTinKhachHang> ttkhs = db.ThongTinKhachHangs.Where(t => t.MaDonVi.Trim().ToLower() == maDonVi.Trim().ToLower()).ToList();
                if (ttkhs != null)
                {
                    foreach (var ttkh in ttkhs)
                    {
                        ttkh.MaDonVi = string.Empty;
                        //ttkh.TenDonVi = string.Empty;
                        //ttkh.DiaChi = string.Empty;
                        //ttkh.MaSoThue = string.Empty;
                    }

                    db.SubmitChanges();
                }

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

        public static Result DeleteTenDonVi(string tenDonVi)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ThongTinKhachHang> ttkhs = db.ThongTinKhachHangs.Where(t => t.TenDonVi.Trim().ToLower() == tenDonVi.Trim().ToLower()).ToList();
                if (ttkhs != null)
                {
                    foreach (var ttkh in ttkhs)
                    {
                        //ttkh.MaDonVi = string.Empty;
                        ttkh.TenDonVi = string.Empty;
                        //ttkh.DiaChi = string.Empty;
                        //ttkh.MaSoThue = string.Empty;
                    }

                    db.SubmitChanges();
                }

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
    }
}
