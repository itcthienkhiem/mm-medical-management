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
    public class KhoCapCuuBus : BusBase
    {
        public static Result GetDanhSachCapCuu()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KhoCapCuu WITH(NOLOCK) WHERE Status={0} ORDER BY TenCapCuu", (byte)Status.Actived);
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

        public static Result GetDanhSachCapCuu(string tenCapCuu)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (tenCapCuu.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KhoCapCuu WITH(NOLOCK) WHERE Status={0} ORDER BY TenCapCuu",
                    (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM KhoCapCuu WITH(NOLOCK) WHERE TenCapCuu LIKE N'%{0}%' AND Status={1} ORDER BY TenCapCuu",
                    tenCapCuu, (byte)Status.Actived);
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

        public static Result DeleteThongTinCapCuu(List<string> keys)
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
                        KhoCapCuu kcc = db.KhoCapCuus.SingleOrDefault<KhoCapCuu>(k => k.KhoCapCuuGUID.ToString() == key);
                        if (kcc != null)
                        {
                            kcc.DeletedDate = DateTime.Now;
                            kcc.DeletedBy = Guid.Parse(Global.UserGUID);
                            kcc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Tên cấp cứu: '{1}', ĐVT: '{2}'\n", kcc.KhoCapCuuGUID.ToString(), kcc.TenCapCuu, kcc.DonViTinh);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin cấp cứu";
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

        public static Result CheckTenCapCuuExist(string khoCapCuuGUID, string tenCapCuu)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                KhoCapCuu kcc = null;
                if (khoCapCuuGUID == null || khoCapCuuGUID == string.Empty)
                    kcc = db.KhoCapCuus.SingleOrDefault<KhoCapCuu>(k => k.TenCapCuu.Trim().ToLower() == tenCapCuu.Trim().ToLower());
                else
                    kcc = db.KhoCapCuus.SingleOrDefault<KhoCapCuu>(k => k.TenCapCuu.Trim().ToLower() == tenCapCuu.Trim().ToLower() &&
                                                                k.KhoCapCuuGUID.ToString() != khoCapCuuGUID);

                if (kcc == null)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
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

        public static Result InsertThongTinCapCuu(KhoCapCuu khoCapCuu)
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
                    if (khoCapCuu.KhoCapCuuGUID == null || khoCapCuu.KhoCapCuuGUID == Guid.Empty)
                    {
                        khoCapCuu.KhoCapCuuGUID = Guid.NewGuid();
                        db.KhoCapCuus.InsertOnSubmit(khoCapCuu);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên cấp cứu: '{1}', ĐVT: '{2}'",
                            khoCapCuu.KhoCapCuuGUID.ToString(), khoCapCuu.TenCapCuu, khoCapCuu.DonViTinh);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin cấp cứu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        KhoCapCuu kcc = db.KhoCapCuus.SingleOrDefault<KhoCapCuu>(k => k.KhoCapCuuGUID == khoCapCuu.KhoCapCuuGUID);
                        if (kcc != null)
                        {
                            kcc.TenCapCuu = khoCapCuu.TenCapCuu;
                            kcc.DonViTinh = khoCapCuu.DonViTinh;
                            kcc.Note = khoCapCuu.Note;
                            kcc.CreatedDate = khoCapCuu.CreatedDate;
                            kcc.CreatedBy = khoCapCuu.CreatedBy;
                            kcc.UpdatedDate = khoCapCuu.UpdatedDate;
                            kcc.UpdatedBy = khoCapCuu.UpdatedBy;
                            kcc.DeletedDate = khoCapCuu.DeletedDate;
                            kcc.DeletedBy = khoCapCuu.DeletedBy;
                            kcc.Status = khoCapCuu.Status;

                            //Update DVT to NhapKhoCapCuu
                            var nhapKhoCapCuus = kcc.NhapKhoCapCuus;
                            foreach (var nk in nhapKhoCapCuus)
                            {
                                if (kcc.DonViTinh == nk.DonViTinhQuiDoi) continue;
                                nk.DonViTinhQuiDoi = kcc.DonViTinh;
                                if (nk.DonViTinhNhap != "Hộp" && nk.DonViTinhNhap != "Vỉ")
                                    nk.DonViTinhNhap = kcc.DonViTinh;
                                else if (nk.DonViTinhNhap == "Vỉ" && kcc.DonViTinh != "Viên")
                                    nk.DonViTinhNhap = "Hộp";
                            }

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên cấp cứu: '{1}', ĐVT: '{2}'",
                            kcc.KhoCapCuuGUID.ToString(), kcc.TenCapCuu, kcc.DonViTinh);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin cấp cứu";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
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
    }
}
