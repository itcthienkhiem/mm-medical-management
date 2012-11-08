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
    public class NhomThuocBus : BusBase
    {
        public static Result GetNhomThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhomThuoc WITH(NOLOCK) WHERE Status={0} ORDER BY TenNhomThuoc", (byte)Status.Actived);
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

        public static Result GetThuocThayTheList(string thuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT T.* FROM NhomThuoc_Thuoc N WITH(NOLOCK), THUOC T WITH(NOLOCK) WHERE N.NhomThuocGUID IN (SELECT NhomThuocGUID FROM NhomThuoc_Thuoc WITH(NOLOCK) WHERE ThuocGUID = '{1}' AND Status={0}) AND N.ThuocGUID <> '{1}' AND N.ThuocGUID = T.ThuocGUID AND T.Status={0} AND N.Status={0} ORDER BY TenThuoc", 
                    (byte)Status.Actived, thuocGUID);
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

        public static Result GetThuocListByNhom(string nhomThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhomThuoc_ThuocView WHERE NhomThuocGUID='{0}' AND NhomThuoc_ThuocStatus={1} AND ThuocStatus={1} ORDER BY TenThuoc",
                    nhomThuocGUID, (byte)Status.Actived);
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

        public static Result GetNhomThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM NhomThuoc WITH(NOLOCK)";
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                        result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                    else result.QueryResult = 0;
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

            return result;
        }

        public static Result DeleteNhomThuoc(List<string> nhomThuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in nhomThuocKeys)
                    {
                        NhomThuoc nhomThuoc = db.NhomThuocs.SingleOrDefault<NhomThuoc>(n => n.NhomThuocGUID.ToString() == key);
                        if (nhomThuoc != null)
                        {
                            nhomThuoc.DeletedDate = DateTime.Now;
                            nhomThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            nhomThuoc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã nhóm thuốc: '{1}', Tên nhóm thuốc: '{2}'\n", 
                                nhomThuoc.NhomThuocGUID.ToString(), nhomThuoc.MaNhomThuoc, nhomThuoc.TenNhomThuoc);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin nhóm thuốc";
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

        public static Result CheckNhomThuocExistCode(string nhomThuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                NhomThuoc nt = null;
                if (nhomThuocGUID == null || nhomThuocGUID == string.Empty)
                    nt = db.NhomThuocs.SingleOrDefault<NhomThuoc>(n => n.MaNhomThuoc.ToLower() == code.ToLower());
                else
                    nt = db.NhomThuocs.SingleOrDefault<NhomThuoc>(n => n.MaNhomThuoc.ToLower() == code.ToLower() &&
                                                                n.NhomThuocGUID.ToString() != nhomThuocGUID);

                if (nt == null)
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

        public static Result InsertNhomThuoc(NhomThuoc nhomThuoc, List<string> addedThuocList, List<string> deletedThuocList)
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
                    if (nhomThuoc.NhomThuocGUID == null || nhomThuoc.NhomThuocGUID == Guid.Empty)
                    {
                        nhomThuoc.NhomThuocGUID = Guid.NewGuid();
                        db.NhomThuocs.InsertOnSubmit(nhomThuoc);
                        db.SubmitChanges();

                        desc += string.Format("- Nhóm thuốc: GUID: '{0}', Mã nhóm thuốc: '{1}', Tên nhóm thuốc: '{2}'\n",
                            nhomThuoc.NhomThuocGUID.ToString(), nhomThuoc.MaNhomThuoc, nhomThuoc.TenNhomThuoc);

                        //Thuoc
                        if (addedThuocList != null && addedThuocList.Count > 0)
                        {
                            desc += string.Format("- Danh sách thuốc được thêm:\n");
                            foreach (string key in addedThuocList)
                            {
                                NhomThuoc_Thuoc ntt = db.NhomThuoc_Thuocs.SingleOrDefault<NhomThuoc_Thuoc>(n => n.ThuocGUID.ToString() == key &&
                                    n.NhomThuocGUID.ToString() == nhomThuoc.NhomThuocGUID.ToString());

                                if (ntt == null)
                                {
                                    ntt = new NhomThuoc_Thuoc();
                                    ntt.NhomThuoc_ThuocGUID = Guid.NewGuid();
                                    ntt.NhomThuocGUID = nhomThuoc.NhomThuocGUID;
                                    ntt.ThuocGUID = Guid.Parse(key);
                                    ntt.CreatedDate = DateTime.Now;
                                    ntt.CreatedBy = Guid.Parse(Global.UserGUID);
                                    ntt.Status = (byte)Status.Actived;
                                    db.NhomThuoc_Thuocs.InsertOnSubmit(ntt);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    ntt.Status = (byte)Status.Actived;
                                    ntt.UpdatedDate = DateTime.Now;
                                    ntt.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    db.SubmitChanges();
                                }

                                desc += string.Format("  + GUID: '{0}', Thuốc:'{1}'\n", ntt.NhomThuoc_ThuocGUID.ToString(), ntt.Thuoc.TenThuoc);
                            }
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin nhóm thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        NhomThuoc nt = db.NhomThuocs.SingleOrDefault<NhomThuoc>(n => n.NhomThuocGUID.ToString() == nhomThuoc.NhomThuocGUID.ToString());
                        if (nt != null)
                        {
                            nt.MaNhomThuoc = nhomThuoc.MaNhomThuoc;
                            nt.TenNhomThuoc = nhomThuoc.TenNhomThuoc;
                            nt.Note = nhomThuoc.Note;
                            nt.CreatedDate = nhomThuoc.CreatedDate;
                            nt.CreatedBy = nhomThuoc.CreatedBy;
                            nt.UpdatedDate = nhomThuoc.UpdatedDate;
                            nt.UpdatedBy = nhomThuoc.UpdatedBy;
                            nt.DeletedDate = nhomThuoc.DeletedDate;
                            nt.DeletedBy = nhomThuoc.DeletedBy;
                            nt.Status = nhomThuoc.Status;

                            desc += string.Format("- Nhóm thuốc: GUID: '{0}', Mã nhóm thuốc: '{1}', Tên nhóm thuốc: '{2}'\n",
                           nt.NhomThuocGUID.ToString(), nt.MaNhomThuoc, nt.TenNhomThuoc);
                        }

                        //Delete Thuoc
                        if (deletedThuocList != null && deletedThuocList.Count > 0)
                        {
                            desc += string.Format("- Danh sách thuốc được xóa:\n");
                            foreach (string key in deletedThuocList)
                            {
                                NhomThuoc_Thuoc ntt = db.NhomThuoc_Thuocs.SingleOrDefault<NhomThuoc_Thuoc>(n => n.ThuocGUID.ToString() == key &&
                                    n.NhomThuocGUID.ToString() == nhomThuoc.NhomThuocGUID.ToString());

                                if (ntt != null)
                                {
                                    ntt.Status = (byte)Status.Deactived;
                                    ntt.DeletedDate = DateTime.Now;
                                    ntt.DeletedBy = Guid.Parse(Global.UserGUID);

                                    desc += string.Format("  + GUID: '{0}', Thuốc: '{1}'\n", ntt.NhomThuoc_ThuocGUID.ToString(), ntt.Thuoc.TenThuoc);
                                }
                            }
                            db.SubmitChanges();
                        }

                        //Add Thuoc
                        if (addedThuocList != null && addedThuocList.Count > 0)
                        {
                            desc += string.Format("- Danh sách thuốc được thêm:\n");
                            foreach (string key in addedThuocList)
                            {
                                NhomThuoc_Thuoc ntt = db.NhomThuoc_Thuocs.SingleOrDefault<NhomThuoc_Thuoc>(n => n.ThuocGUID.ToString() == key &&
                                    n.NhomThuocGUID.ToString() == nhomThuoc.NhomThuocGUID.ToString());

                                if (ntt == null)
                                {
                                    ntt = new NhomThuoc_Thuoc();
                                    ntt.NhomThuoc_ThuocGUID = Guid.NewGuid();
                                    ntt.NhomThuocGUID = nhomThuoc.NhomThuocGUID;
                                    ntt.ThuocGUID = Guid.Parse(key);
                                    ntt.CreatedDate = DateTime.Now;
                                    ntt.CreatedBy = Guid.Parse(Global.UserGUID);
                                    ntt.Status = (byte)Status.Actived;
                                    db.NhomThuoc_Thuocs.InsertOnSubmit(ntt);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    ntt.Status = (byte)Status.Actived;
                                    ntt.UpdatedDate = DateTime.Now;
                                    ntt.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    db.SubmitChanges();
                                }

                                desc += string.Format("  + GUID: '{0}', Thuốc:'{1}'\n", ntt.NhomThuoc_ThuocGUID.ToString(), ntt.Thuoc.TenThuoc);
                            }
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Sửa thông tin nhóm thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
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
    }
}
