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
    public class ThucHienXetNghiemBus : BusBase
    {
        public static Result GetDanhSachCongTy()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT TenCongTy FROM ThucHienXetNghiem WITH(NOLOCK) WHERE Status={0} ORDER BY TenCongTy", (byte)Status.Actived);
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

        public static Result GetDanhSachKhachHang()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT TenKhachHang FROM ThucHienXetNghiem WITH(NOLOCK) WHERE Status={0} ORDER BY TenKhachHang", (byte)Status.Actived);
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

        public static Result GetDanhSachDichVu()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT TenDichVu FROM ThucHienXetNghiem WITH(NOLOCK) WHERE Status={0} ORDER BY TenDichVu", (byte)Status.Actived);
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

        public static Result GetDichVuXetNghiemList(DateTime tuNgay, DateTime denNgay, string tenCongTy, string tenKhachHang, string tenDichVu)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ThucHienXetNghiem WITH(NOLOCK) WHERE Status={0} AND NgayThucHien BETWEEN '{1}' AND '{2}' AND TenCongTy LIKE N'%{3}%' AND TenKhachHang LIKE N'%{4}%' AND TenDichVu LIKE N'%{5}%' ORDER BY NgayThucHien, TenCongTy, TenKhachHang, TenDichVu",
                    (byte)Status.Actived, tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), tenCongTy, tenKhachHang, tenDichVu);
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

        public static Result DeleteDichVuXetNghiem(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 30, 0)))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        ThucHienXetNghiem thxn = db.ThucHienXetNghiems.SingleOrDefault<ThucHienXetNghiem>(x => x.ThucHienXetNghiemGUID.ToString() == key);
                        if (thxn != null)
                        {
                            thxn.DeletedDate = DateTime.Now;
                            thxn.DeletedBy = Guid.Parse(Global.UserGUID);
                            thxn.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Ngày thực hiện: '{1}', Tên công ty: '{2}', Tên khách hàng: '{3}', Tên dịch vụ: '{4}', Số tiền: '{5}', Source: '{6}'\n", 
                                thxn.ThucHienXetNghiemGUID.ToString(), thxn.NgayThucHien.ToString("dd/MM/yyyy"), thxn.TenCongTy, thxn.TenKhachHang, thxn.TenDichVu, thxn.SoTien, thxn.SourcePath);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa dịch vụ xét nghiệm";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
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

        public static Result UpdateGiaDichVuXetNghiem(DataTable dtSource)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 30, 0)))
                {
                    string desc = string.Empty;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        if (row.RowState != DataRowState.Modified) continue;
                        ThucHienXetNghiem thxn = db.ThucHienXetNghiems.SingleOrDefault<ThucHienXetNghiem>(x => x.ThucHienXetNghiemGUID.ToString() == row["ThucHienXetNghiemGUID"].ToString());
                        if (thxn != null)
                        {
                            double soTien = Convert.ToDouble(row["SoTien"]);
                            thxn.SoTien = soTien;
                            thxn.UpdatedDate = DateTime.Now;
                            thxn.UpdatedBy = Guid.Parse(Global.UserGUID);

                            desc = string.Format("- GUID: '{0}', Ngày thực hiện: '{1}', Tên công ty: '{2}', Tên khách hàng: '{3}', Tên dịch vụ: '{4}', Số tiền: '{5}', Source: '{6}'",
                                thxn.ThucHienXetNghiemGUID.ToString(), thxn.NgayThucHien.ToString("dd/MM/yyyy"), thxn.TenCongTy, thxn.TenKhachHang, thxn.TenDichVu, soTien, thxn.SourcePath);

                            //Tracking
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Delete;
                            tk.Action = "Sửa dịch vụ xét nghiệm";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
                            db.Trackings.InsertOnSubmit(tk);
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

        public static Result InsertDichVuXetNghiem(ThucHienXetNghiem xn)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 30, 0)))
                {
                    string desc = string.Empty;
                    ThucHienXetNghiem thxn = db.ThucHienXetNghiems.FirstOrDefault<ThucHienXetNghiem>(x => x.NgayThucHien == xn.NgayThucHien &&
                        x.TenCongTy.Trim().ToUpper() == xn.TenCongTy.Trim().ToUpper() &&
                        x.TenKhachHang.Trim().ToUpper() == xn.TenKhachHang.Trim().ToUpper() &&
                        x.TenDichVu.Trim().ToUpper() == xn.TenDichVu.Trim().ToUpper());

                    if (thxn == null)
                    {
                        xn.ThucHienXetNghiemGUID = Guid.NewGuid();
                        xn.CreatedDate = DateTime.Now;
                        xn.CreatedBy = Guid.Parse(Global.UserGUID);
                        xn.Status = (byte)Status.Actived;
                        db.ThucHienXetNghiems.InsertOnSubmit(xn);

                        desc = string.Format("- GUID: '{0}', Ngày thực hiện: '{1}', Tên công ty: '{2}', Tên khách hàng: '{3}', Tên dịch vụ: '{4}', Số tiền: '{5}', Source: '{6}'",
                            xn.ThucHienXetNghiemGUID.ToString(), xn.NgayThucHien.ToString("dd/MM/yyyy"), xn.TenCongTy, xn.TenKhachHang, xn.TenDichVu, xn.SoTien, xn.SourcePath);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Delete;
                        tk.Action = "Thêm dịch vụ xét nghiệm";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        db.Trackings.InsertOnSubmit(tk);
                    }
                    else
                    {
                        thxn.SoTien = xn.SoTien;
                        thxn.SourcePath = xn.SourcePath;
                        thxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                        thxn.UpdatedDate = DateTime.Now;
                        thxn.Status = (byte)Status.Actived;

                        desc = string.Format("- GUID: '{0}', Ngày thực hiện: '{1}', Tên công ty: '{2}', Tên khách hàng: '{3}', Tên dịch vụ: '{4}', Số tiền: '{5}', Source: '{6}'",
                            thxn.ThucHienXetNghiemGUID.ToString(), thxn.NgayThucHien.ToString("dd/MM/yyyy"), thxn.TenCongTy, thxn.TenKhachHang, thxn.TenDichVu, thxn.SoTien, thxn.SourcePath);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Delete;
                        tk.Action = "Sửa dịch vụ xét nghiệm";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
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
    }
}
