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
    public class ThongBaoBus : BusBase
    {
        public static Result GetThongBaoList(DateTime tuNgay, DateTime denNgay, string tenNguoiTao, bool isAll)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (isAll)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ThongBaoView WHERE ((CreatedDate IS NOT NULL AND CreatedDate BETWEEN '{0}' AND '{1}') OR (NgayDuyet1 IS NOT NULL AND NgayDuyet1 BETWEEN '{0}' AND '{1}') OR (NgayDuyet2 IS NOT NULL AND NgayDuyet2 BETWEEN '{0}' AND '{1}') OR (NgayDuyet3 IS NOT NULL AND NgayDuyet3 BETWEEN '{0}' AND '{1}')) AND FullName LIKE N'%{2}%' AND Status = {3} ORDER BY CreatedDate DESC",
                        tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), tenNguoiTao, (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ThongBaoView WHERE ((NgayDuyet1 IS NOT NULL AND NgayDuyet1 BETWEEN '{0}' AND '{1}') OR (NgayDuyet2 IS NOT NULL AND NgayDuyet2 BETWEEN '{0}' AND '{1}') OR (NgayDuyet3 IS NOT NULL AND NgayDuyet3 BETWEEN '{0}' AND '{1}')) AND FullName LIKE N'%{2}%' AND Status = {3} ORDER BY CreatedDate DESC",
                        tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), tenNguoiTao, (byte)Status.Actived);
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

        public static Result DeleteThongBao(List<string> keys)
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
                        ThongBao tb = db.ThongBaos.SingleOrDefault<ThongBao>(b => b.ThongBaoGUID.ToString() == key);
                        if (tb != null)
                        {
                            tb.DeletedDate = DateTime.Now;
                            tb.DeletedBy = Guid.Parse(Global.UserGUID);
                            tb.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Tên thông báo: '{1}', Ngày tạo: '{2}', Ngày duyệt 1: '{3}', Ngày duyệt 2: '{4}', Ngày duyệt 3: '{5}'\n",
                                tb.ThongBaoGUID.ToString(), tb.TenThongBao, tb.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                tb.NgayDuyet1.HasValue ? tb.NgayDuyet1.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty, 
                                tb.NgayDuyet2.HasValue ? tb.NgayDuyet2.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                tb.NgayDuyet3.HasValue ? tb.NgayDuyet3.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông báo";
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

        public static Result InsertThongBao(ThongBao thongBao)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (thongBao.ThongBaoGUID == null || thongBao.ThongBaoGUID == Guid.Empty)
                    {
                        thongBao.ThongBaoGUID = Guid.NewGuid();
                        db.ThongBaos.InsertOnSubmit(thongBao);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên thông báo: '{1}', Ngày tạo: '{2}', Ngày duyệt 1: '{3}', Ngày duyệt 2: '{4}', Ngày duyệt 3: '{5}'",
                                thongBao.ThongBaoGUID.ToString(), thongBao.TenThongBao, thongBao.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                thongBao.NgayDuyet1.HasValue ? thongBao.NgayDuyet1.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                thongBao.NgayDuyet2.HasValue ? thongBao.NgayDuyet2.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                thongBao.NgayDuyet3.HasValue ? thongBao.NgayDuyet3.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông báo";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        ThongBao tb = db.ThongBaos.SingleOrDefault<ThongBao>(b => b.ThongBaoGUID == thongBao.ThongBaoGUID);
                        if (tb != null)
                        {
                            tb.TenThongBao = thongBao.TenThongBao;
                            tb.ThongBaoBuff = thongBao.ThongBaoBuff;
                            tb.NgayDuyet1 = thongBao.NgayDuyet1;
                            tb.ThongBaoBuff1 = thongBao.ThongBaoBuff1;
                            tb.NgayDuyet2 = thongBao.NgayDuyet2;
                            tb.ThongBaoBuff2 = thongBao.ThongBaoBuff2;
                            tb.NgayDuyet3 = thongBao.NgayDuyet3;
                            tb.ThongBaoBuff3 = thongBao.ThongBaoBuff3;
                            tb.GhiChu = thongBao.GhiChu;
                            tb.Path = thongBao.Path;
                            tb.CreatedDate = thongBao.CreatedDate;
                            tb.CreatedBy = thongBao.CreatedBy;
                            tb.UpdatedDate = thongBao.UpdatedDate;
                            tb.UpdatedBy = thongBao.UpdatedBy;
                            tb.DeletedDate = thongBao.DeletedDate;
                            tb.DeletedBy = thongBao.DeletedBy;
                            tb.Status = thongBao.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên thông báo: '{1}', Ngày tạo: '{2}', Ngày duyệt 1: '{3}', Ngày duyệt 2: '{4}', Ngày duyệt 3: '{5}'",
                                tb.ThongBaoGUID.ToString(), tb.TenThongBao, tb.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                tb.NgayDuyet1.HasValue ? tb.NgayDuyet1.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                tb.NgayDuyet2.HasValue ? tb.NgayDuyet2.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                tb.NgayDuyet3.HasValue ? tb.NgayDuyet3.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông báo";
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
