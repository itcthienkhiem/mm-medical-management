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
    public class ThuocBus : BusBase
    {
        public static Result GetThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Thuoc WITH(NOLOCK) WHERE Status={0} ORDER BY TenThuoc", (byte)Status.Actived);
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

        public static Result GetThuocList(string tenThuoc)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (tenThuoc.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Thuoc WITH(NOLOCK) WHERE Status={0} ORDER BY TenThuoc",
                        (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Thuoc WITH(NOLOCK) WHERE (TenThuoc LIKE N'%{0}%' OR BietDuoc LIKE N'%{0}%') AND Status={1} ORDER BY TenThuoc",
                    tenThuoc, (byte)Status.Actived);
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
        
        public static Result GetThuocListNotInNhomThuoc(string nhomThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Thuoc WITH(NOLOCK) WHERE Status={0} AND ThuocGUID NOT IN (SELECT ThuocGUID FROM NhomThuoc_Thuoc WHERE NhomThuocGUID = '{1}' AND Status={0}) ORDER BY TenThuoc", 
                    (byte)Status.Actived, nhomThuocGUID);
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

        public static Result GetThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Thuoc WITH(NOLOCK)";
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

        public static Result DeleteThuoc(List<string> thuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in thuocKeys)
                    {
                        Thuoc thuoc = db.Thuocs.SingleOrDefault<Thuoc>(th => th.ThuocGUID.ToString() == key);
                        if (thuoc != null)
                        {
                            thuoc.DeletedDate = DateTime.Now;
                            thuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            thuoc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã thuốc: '{1}', Tên thuốc: '{2}', ĐVT: '{3}', Biệt dược: '{4}', Hàm lượng: '{5}', Hoạt chất: '{6}'\n",
                                thuoc.ThuocGUID.ToString(), thuoc.MaThuoc, thuoc.TenThuoc, thuoc.DonViTinh, thuoc.BietDuoc, thuoc.HamLuong, thuoc.HoatChat);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin thuốc";
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

        public static Result CheckThuocExistCode(string thuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Thuoc th = null;
                if (thuocGUID == null || thuocGUID == string.Empty)
                    th = db.Thuocs.SingleOrDefault<Thuoc>(t => t.MaThuoc.ToLower() == code.ToLower());
                else
                    th = db.Thuocs.SingleOrDefault<Thuoc>(t => t.MaThuoc.ToLower() == code.ToLower() &&
                                                                t.ThuocGUID.ToString() != thuocGUID);

                if (th == null)
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

        public static Result InsertThuoc(Thuoc thuoc)
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
                    if (thuoc.ThuocGUID == null || thuoc.ThuocGUID == Guid.Empty)
                    {
                        thuoc.ThuocGUID = Guid.NewGuid();
                        db.Thuocs.InsertOnSubmit(thuoc);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Mã thuốc: '{1}', Tên thuốc: '{2}', ĐVT: '{3}', Biệt dược: '{4}', Hàm lượng: '{5}', Hoạt chất: '{6}'",
                                thuoc.ThuocGUID.ToString(), thuoc.MaThuoc, thuoc.TenThuoc, thuoc.DonViTinh, thuoc.BietDuoc, thuoc.HamLuong, thuoc.HoatChat);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        Thuoc th = db.Thuocs.SingleOrDefault<Thuoc>(t => t.ThuocGUID.ToString() == thuoc.ThuocGUID.ToString());
                        if (th != null)
                        {
                            th.MaThuoc = thuoc.MaThuoc;
                            th.TenThuoc = thuoc.TenThuoc;
                            th.BietDuoc = thuoc.BietDuoc;
                            th.HamLuong = thuoc.HamLuong;
                            th.HoatChat = thuoc.HoatChat;
                            th.DonViTinh = thuoc.DonViTinh;
                            th.Note = thuoc.Note;
                            th.CreatedDate = thuoc.CreatedDate;
                            th.CreatedBy = thuoc.CreatedBy;
                            th.UpdatedDate = thuoc.UpdatedDate;
                            th.UpdatedBy = thuoc.UpdatedBy;
                            th.DeletedDate = thuoc.DeletedDate;
                            th.DeletedBy = thuoc.DeletedBy;
                            th.Status = thuoc.Status;

                            //Update DVT to LoThuoc
                            var loThuocs = th.LoThuocs;
                            foreach (var lo in loThuocs)
                            {
                                if (th.DonViTinh == lo.DonViTinhQuiDoi) continue;
                                lo.DonViTinhQuiDoi = th.DonViTinh;
                                if (lo.DonViTinhNhap != "Hộp" && lo.DonViTinhNhap != "Vỉ")
                                    lo.DonViTinhNhap = th.DonViTinh;
                                else if (lo.DonViTinhNhap == "Vỉ" && th.DonViTinh != "Viên")
                                    lo.DonViTinhNhap = "Hộp";
                            }

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Mã thuốc: '{1}', Tên thuốc: '{2}', ĐVT: '{3}', Biệt dược: '{4}', Hàm lượng: '{5}', Hoạt chất: '{6}'",
                                    th.ThuocGUID.ToString(), th.MaThuoc, th.TenThuoc, th.DonViTinh, th.BietDuoc, th.HamLuong, th.HoatChat);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin thuốc";
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
