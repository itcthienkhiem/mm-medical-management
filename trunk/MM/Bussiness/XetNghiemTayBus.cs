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
    public class XetNghiemTayBus : BusBase
    {
        public static Result GetXetNghiemList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE [Type] WHEN 'Biochemistry' THEN N'Sinh hóa' WHEN 'Urine' THEN N'Nước tiểu' WHEN 'Electrolytes' THEN N'Ido đồ' WHEN 'Haematology' THEN N'Huyết học' END LoaiXN FROM XetNghiem_Manual WHERE Status={0} ORDER BY Fullname", (byte)Status.Actived);
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

        public static Result GetChiTietXetNghiemList(string xetNghiem_ManualGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietXetNghiem_Manual WHERE Status={0} AND XetNghiem_ManualGUID='{1}'",
                    (byte)Status.Actived, xetNghiem_ManualGUID);
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

        public static Result GetDonViList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT DonVi FROM ChiTietXetNghiem_Manual");
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

        public static Result CheckTenXetNghiemExist(string xetNghiem_ManualGUID, string tenXetNghiem)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                XetNghiem_Manual xn = null;
                if (xetNghiem_ManualGUID == null || xetNghiem_ManualGUID == string.Empty)
                    xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.Fullname.ToLower() == tenXetNghiem.ToLower());
                else
                    xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.Fullname.ToLower() == tenXetNghiem.ToLower() &&
                                                                x.XetNghiem_ManualGUID.ToString() != xetNghiem_ManualGUID);

                if (xn == null)
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

        public static Result DeleteXetNghiem(List<string> keys)
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
                        XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID.ToString() == key);
                        if (xn != null)
                        {
                            xn.DeletedDate = DateTime.Now;
                            xn.DeletedBy = Guid.Parse(Global.UserGUID);
                            xn.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Loại xét nghiệm: '{2}'\n", 
                                xn.XetNghiem_ManualGUID.ToString(), xn.Fullname, xn.Type);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin xét nghiệm tay";
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

        public static Result InsertXetNghiem(XetNghiem_Manual xetNghiem, List<ChiTietXetNghiem_Manual> ctxns)   
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
                    if (xetNghiem.XetNghiem_ManualGUID == null || xetNghiem.XetNghiem_ManualGUID == Guid.Empty)
                    {
                        xetNghiem.XetNghiem_ManualGUID = Guid.NewGuid();
                        db.XetNghiem_Manuals.InsertOnSubmit(xetNghiem);
                        db.SubmitChanges();

                        //Chi tiết xét nghiệm
                        foreach (ChiTietXetNghiem_Manual ctxn in ctxns)
                        {
                            ctxn.ChiTietXetNghiem_ManualGUID = Guid.NewGuid();
                            ctxn.XetNghiem_ManualGUID = xetNghiem.XetNghiem_ManualGUID;
                            ctxn.CreatedBy = Guid.Parse(Global.UserGUID);
                            ctxn.CreatedDate = DateTime.Now;
                            db.ChiTietXetNghiem_Manuals.InsertOnSubmit(ctxn);
                        }

                        //Tracking
                        desc += string.Format("GUID: '{0}', Tên xét nghiệm: '{1}', Loại xét nghiệm: '{2}'", 
                            xetNghiem.XetNghiem_ManualGUID.ToString(), xetNghiem.Fullname, xetNghiem.Type);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin xét nghiệm tay";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID == xetNghiem.XetNghiem_ManualGUID);
                        if (xn != null)
                        {
                            xn.TenXetNghiem = xetNghiem.TenXetNghiem;
                            xn.Fullname = xetNghiem.Fullname;
                            xn.Type = xetNghiem.Type;
                            xn.UpdatedDate = DateTime.Now;
                            xn.UpdatedBy = Guid.Parse(Global.UserGUID);
                            xn.Status = xetNghiem.Status;

                            //Update chi tiết xét nghiệm
                            var chiTietXetNghiemList = xn.ChiTietXetNghiem_Manuals;
                            foreach (ChiTietXetNghiem_Manual ctxn in chiTietXetNghiemList)
                            {
                                ctxn.UpdatedDate = DateTime.Now;
                                ctxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                                ctxn.Status = (byte)Status.Deactived;
                            }

                            db.SubmitChanges();

                            foreach (ChiTietXetNghiem_Manual ctxn in ctxns)
                            {
                                ChiTietXetNghiem_Manual ct = db.ChiTietXetNghiem_Manuals.SingleOrDefault<ChiTietXetNghiem_Manual>(c => c.DoiTuong == ctxn.DoiTuong &&
                                    c.XetNghiem_ManualGUID == xn.XetNghiem_ManualGUID);
                                if (ct == null)
                                {
                                    ctxn.ChiTietXetNghiem_ManualGUID = Guid.NewGuid();
                                    ctxn.XetNghiem_ManualGUID = xn.XetNghiem_ManualGUID;
                                    ctxn.CreatedBy = Guid.Parse(Global.UserGUID);
                                    ctxn.CreatedDate = DateTime.Now;
                                    db.ChiTietXetNghiem_Manuals.InsertOnSubmit(ctxn);
                                }
                                else
                                {
                                    ct.FromValue = ctxn.FromValue;
                                    ct.ToValue = ctxn.ToValue;
                                    ct.DonVi = ctxn.DonVi;
                                    ct.DoiTuong = ctxn.DoiTuong;
                                    ct.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    ct.UpdatedDate = DateTime.Now;
                                    ct.Status = (byte)Status.Actived;
                                }
                            }

                            //Tracking
                            desc += string.Format("GUID: '{0}', Tên xét nghiệm: '{1}', Loại xét nghiệm: '{2}'",
                                xn.XetNghiem_ManualGUID.ToString(), xn.Fullname, xn.Type);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin xét nghiệm tay";
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
