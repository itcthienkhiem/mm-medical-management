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
                string query = "SELECT * FROM ThongTinKhachHang WITH(NOLOCK) ORDER BY TenKhachHang";
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
                                                           where tt.TenKhachHang.Trim().ToLower() == ttkh.TenKhachHang.Trim().ToLower()
                                                           select tt).FirstOrDefault();

                    if (thongTinKhachHang == null)
                    {
                        ttkh.ThongTinKhachHangGUID = Guid.NewGuid();
                        db.ThongTinKhachHangs.InsertOnSubmit(ttkh);
                    }
                    else
                    {
                        thongTinKhachHang.TenDonVi = ttkh.TenDonVi;
                        thongTinKhachHang.MaSoThue = ttkh.MaSoThue;
                        thongTinKhachHang.DiaChi = ttkh.DiaChi;
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
    }
}
