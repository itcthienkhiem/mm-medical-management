using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;


namespace MM.Bussiness
{
    public class SieuAmBus : BusBase
    {
        public static Result GetLoaiSieuAmList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM LoaiSieuAm WHERE Status={0} ORDER BY ThuTu", (byte)Status.Actived);
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

        public Result GetMauBaoCaoList(string loaiSieuAmGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<MauBaoCao> mauBaoCaoList = db.MauBaoCaos.Where(m => m.LoaiSieuAmGUID.ToString() == loaiSieuAmGUID &&
                    m.Status == (byte)Status.Actived).ToList();
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

        public static Result CheckTenSieuAmExist(string loaiSieuAmGUID, string tenSieuAm)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                LoaiSieuAm loaiSieuAm = null;
                if (loaiSieuAmGUID == null || loaiSieuAmGUID == string.Empty)
                    loaiSieuAm = db.LoaiSieuAms.FirstOrDefault<LoaiSieuAm>(s => s.TenSieuAm.ToLower() == tenSieuAm.ToLower());
                else
                    loaiSieuAm = db.LoaiSieuAms.FirstOrDefault<LoaiSieuAm>(s => s.TenSieuAm.ToLower() == tenSieuAm.ToLower() &&
                                                                s.LoaiSieuAmGUID.ToString() != loaiSieuAmGUID);

                if (loaiSieuAm == null)
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

        public static Result DeleteLoaiSieuAm(List<string> keys)
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
                        LoaiSieuAm l = db.LoaiSieuAms.SingleOrDefault<LoaiSieuAm>(ss => ss.LoaiSieuAmGUID.ToString() == key);
                        if (l != null)
                        {
                            l.DeletedDate = DateTime.Now;
                            l.DeletedBy = Guid.Parse(Global.UserGUID);
                            l.Status = (byte)Status.Deactived;
                            
                            desc += string.Format("- GUID: '{0}', Tên siêu âm: '{1}', Thứ tự: '{2}'\n", l.LoaiSieuAmGUID.ToString(), l.TenSieuAm, l.ThuTu);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin loại siêu âm";
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

        public static Result InsertLoaiSieuAm(LoaiSieuAm loaiSieuAm, List<MauBaoCao> mauBaoCaoList)
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
                    if (loaiSieuAm.LoaiSieuAmGUID == null || loaiSieuAm.LoaiSieuAmGUID == Guid.Empty)
                    {
                        loaiSieuAm.LoaiSieuAmGUID = Guid.NewGuid();
                        db.LoaiSieuAms.InsertOnSubmit(loaiSieuAm);
                        db.SubmitChanges();

                        foreach (var mauBaoCao in mauBaoCaoList)
                        {
                            mauBaoCao.MauBaoCaoGUID = Guid.NewGuid();
                            mauBaoCao.LoaiSieuAmGUID = loaiSieuAm.LoaiSieuAmGUID;
                            mauBaoCao.CreatedDate = DateTime.Now;
                            mauBaoCao.CreatedBy = Guid.Parse(Global.UserGUID);
                            db.MauBaoCaos.InsertOnSubmit(mauBaoCao);
                        }

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên siêu âm: '{1}', Thứ tự: '{2}'\n", 
                            loaiSieuAm.LoaiSieuAmGUID.ToString(), loaiSieuAm.TenSieuAm, loaiSieuAm.ThuTu);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin loại siêu âm";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        LoaiSieuAm lsa = db.LoaiSieuAms.SingleOrDefault<LoaiSieuAm>(s => s.LoaiSieuAmGUID.ToString() == loaiSieuAm.LoaiSieuAmGUID.ToString());
                        if (lsa != null)
                        {
                            lsa.TenSieuAm = loaiSieuAm.TenSieuAm;
                            lsa.ThuTu = loaiSieuAm.ThuTu;
                            lsa.Path = loaiSieuAm.Path;
                            lsa.CreatedDate = loaiSieuAm.CreatedDate;
                            lsa.CreatedBy = loaiSieuAm.CreatedBy;
                            lsa.UpdatedDate = loaiSieuAm.UpdatedDate;
                            lsa.UpdatedBy = loaiSieuAm.UpdatedBy;
                            lsa.DeletedDate = loaiSieuAm.DeletedDate;
                            lsa.DeletedBy = loaiSieuAm.DeletedBy;
                            lsa.Status = loaiSieuAm.Status;

                            //Delete mẫu báo cáo
                            var mbcs = lsa.MauBaoCaos;
                            foreach (var mbc in mbcs)
                            {
                                mbc.UpdatedBy = Guid.Parse(Global.UserGUID);
                                mbc.UpdatedDate = DateTime.Now;
                                mbc.Status = (byte)Status.Deactived;
                            }

                            //Update mẫu báo cáo
                            foreach (var mbc in mauBaoCaoList)
                            {
                                MauBaoCao mauBaoCao = lsa.MauBaoCaos.Where(m => m.DoiTuong == mbc.DoiTuong).FirstOrDefault();
                                if (mauBaoCao == null)
                                {
                                    mauBaoCao.MauBaoCaoGUID = Guid.NewGuid();
                                    mauBaoCao.LoaiSieuAmGUID = loaiSieuAm.LoaiSieuAmGUID;
                                    mauBaoCao.CreatedDate = DateTime.Now;
                                    mauBaoCao.CreatedBy = Guid.Parse(Global.UserGUID);
                                    db.MauBaoCaos.InsertOnSubmit(mauBaoCao);
                                }
                                else
                                {
                                    mauBaoCao.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    mauBaoCao.UpdatedDate = DateTime.Now;
                                    mauBaoCao.Template = mbc.Template;
                                    mauBaoCao.Status = (byte)Status.Actived;
                                }
                            }

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên siêu âm: '{1}', Thứ tự: '{2}'\n",
                            lsa.LoaiSieuAmGUID.ToString(), lsa.TenSieuAm, lsa.ThuTu);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin loại siêu âm";
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
