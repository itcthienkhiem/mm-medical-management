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
    public class NhanXetKhamLamSangBus : BusBase
    {
        public static Result GetNhanXetKhamLamSangist(string nhanXet)
        {
            Result result = null;

            try
            {
                string subQuery = nhanXet.Trim() == string.Empty ? string.Empty : string.Format(" AND NhanXet LIKE N'{0}%'", nhanXet);
                string query = string.Format(@"SELECT CAST(0 AS Bit) AS Checked, * FROM NhanXetKhamLamSangView WITH(NOLOCK) 
                                                WHERE Status={0} {1} ORDER BY Loai, NhanXet", 
                                                (byte)Status.Actived, subQuery);
                
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

        public static Result GetNhanXetKhamLamSangist(int loai)
        {
            Result result = null;

            try
            {
                string query = string.Format(@"SELECT * FROM NhanXetKhamLamSangView WITH(NOLOCK) 
                                                WHERE Status={0} AND Loai = {1} ORDER BY NhanXet",
                                                (byte)Status.Actived, loai);

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

        public static Result CheckNhanXetExist(string nhanXetKhamLamSangGUID, int loai, string nhanXet)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                NhanXetKhamLamSang nxkls = null;
                if (nhanXetKhamLamSangGUID == null || nhanXetKhamLamSangGUID == string.Empty)
                    nxkls = db.NhanXetKhamLamSangs.SingleOrDefault<NhanXetKhamLamSang>(s => s.NhanXet.Trim().ToLower() == nhanXet.Trim().ToLower() &&
                                                                                            s.Loai == loai);
                else
                    nxkls = db.NhanXetKhamLamSangs.SingleOrDefault<NhanXetKhamLamSang>(s => s.NhanXet.Trim().ToLower() == nhanXet.Trim().ToLower() &&
                                                                                            s.Loai == loai &&
                                                                                            s.NhanXetKhamLamSangGUID.ToString() != nhanXetKhamLamSangGUID);

                if (nxkls == null)
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

        public static Result DeleteNhanXetKhamLamSang(List<string> keys)
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
                        NhanXetKhamLamSang nxkls = db.NhanXetKhamLamSangs.SingleOrDefault<NhanXetKhamLamSang>(ss => ss.NhanXetKhamLamSangGUID.ToString() == key);
                        if (nxkls != null)
                        {
                            nxkls.DeletedDate = DateTime.Now;
                            nxkls.DeletedBy = Guid.Parse(Global.UserGUID);
                            nxkls.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Nhận xét: '{1}', Loại: '{2}'\n", 
                                nxkls.NhanXetKhamLamSangGUID.ToString(), nxkls.NhanXet, nxkls.Loai);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin nhận xét khám lâm sàng";
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

        public static Result InsertNhanXetKhamLamSang(NhanXetKhamLamSang nhanXetKhamLamSang)
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
                    if (nhanXetKhamLamSang.NhanXetKhamLamSangGUID == null || nhanXetKhamLamSang.NhanXetKhamLamSangGUID == Guid.Empty)
                    {
                        nhanXetKhamLamSang.NhanXetKhamLamSangGUID = Guid.NewGuid();
                        db.NhanXetKhamLamSangs.InsertOnSubmit(nhanXetKhamLamSang);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Nhận xét: '{1}', Loại: '{2}'",
                               nhanXetKhamLamSang.NhanXetKhamLamSangGUID.ToString(), nhanXetKhamLamSang.NhanXet, nhanXetKhamLamSang.Loai);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin nhận xét khám lâm sàng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        NhanXetKhamLamSang nxkls = db.NhanXetKhamLamSangs.SingleOrDefault<NhanXetKhamLamSang>(s => s.NhanXetKhamLamSangGUID == nhanXetKhamLamSang.NhanXetKhamLamSangGUID);
                        if (nxkls != null)
                        {
                            nxkls.NhanXet = nhanXetKhamLamSang.NhanXet;
                            nxkls.Loai = nhanXetKhamLamSang.Loai;
                            nxkls.GhiChu = nhanXetKhamLamSang.GhiChu;
                            nxkls.CreatedDate = nhanXetKhamLamSang.CreatedDate;
                            nxkls.CreatedBy = nhanXetKhamLamSang.CreatedBy;
                            nxkls.UpdatedDate = nhanXetKhamLamSang.UpdatedDate;
                            nxkls.UpdatedBy = nhanXetKhamLamSang.UpdatedBy;
                            nxkls.DeletedDate = nhanXetKhamLamSang.DeletedDate;
                            nxkls.DeletedBy = nhanXetKhamLamSang.DeletedBy;
                            nxkls.Status = nhanXetKhamLamSang.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Nhận xét: '{1}', Loại: '{2}'", nxkls.NhanXetKhamLamSangGUID.ToString(), nxkls.NhanXet, nxkls.Loai);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin nhận xét khám lâm sàng";
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
    }
}
