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
    public class GiaThuocBus : BusBase
    {
        public static Result GetGiaThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaThuocView WITH(NOLOCK) WHERE GiaThuocStatus={0} AND ThuocStatus={0} ORDER BY TenThuoc ASC, NgayApDung DESC",
                    (byte)Status.Actived);
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

        public static Result GetGiaThuocList(string tenThuoc, int type)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (tenThuoc.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaThuocView WITH(NOLOCK) WHERE GiaThuocStatus={0} AND ThuocStatus={0} ORDER BY TenThuoc ASC, NgayApDung DESC",
                        (byte)Status.Actived);
                }
                else
                {
                    if (type == 0)
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaThuocView WITH(NOLOCK) WHERE TenThuoc LIKE N'{0}%' AND GiaThuocStatus={1} AND ThuocStatus={1} ORDER BY TenThuoc ASC, NgayApDung DESC",
                            tenThuoc, (byte)Status.Actived);
                    }
                    else if (type == 1)
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaThuocView WITH(NOLOCK) WHERE BietDuoc LIKE N'{0}%' AND GiaThuocStatus={1} AND ThuocStatus={1} ORDER BY TenThuoc ASC, NgayApDung DESC",
                            tenThuoc, (byte)Status.Actived);
                    }
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

        public static Result GetGiaThuocMoiNhat(string thuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT TOP 1 * FROM GiaThuoc WITH(NOLOCK) WHERE ThuocGUID = '{0}' AND Status = {1} AND NgayApDung <= '{2}' ORDER BY NgayApDung DESC",
                    thuocGUID, (byte)Status.Actived, DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DateTime ngayApDung =  Convert.ToDateTime(dt.Rows[0]["NgayApDung"]);

                        query = string.Format("SELECT * FROM GiaThuoc WITH(NOLOCK) WHERE ThuocGUID = '{0}' AND Status = {1} AND NgayApDung BETWEEN '{2}' AND '{3}' ORDER BY NgayApDung DESC",
                                thuocGUID, (byte)Status.Actived, ngayApDung.ToString("yyyy-MM-dd 00:00:00"), ngayApDung.ToString("yyyy-MM-dd 23:59:59"));
                        return ExcuteQuery(query);
                    }
                    else return result;
                }
                else return result;
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

        public static Result DeleteGiaThuoc(List<string> giaThuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in giaThuocKeys)
                    {
                        GiaThuoc giaThuoc = db.GiaThuocs.SingleOrDefault<GiaThuoc>(g => g.GiaThuocGUID.ToString() == key);
                        if (giaThuoc != null)
                        {
                            giaThuoc.DeletedDate = DateTime.Now;
                            giaThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            giaThuoc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Thuốc: '{1}', Giá bán: '{2}', Ngày áp dụng: '{3}'\n",
                                giaThuoc.GiaThuocGUID.ToString(), giaThuoc.Thuoc.TenThuoc, giaThuoc.GiaBan, giaThuoc.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin giá thuốc";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
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

        public static Result InsertGiaThuoc(GiaThuoc giaThuoc)
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
                    if (giaThuoc.GiaThuocGUID == null || giaThuoc.GiaThuocGUID == Guid.Empty)
                    {
                        giaThuoc.GiaThuocGUID = Guid.NewGuid();
                        db.GiaThuocs.InsertOnSubmit(giaThuoc);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Thuốc: '{1}', Giá bán: '{2}', Ngày áp dụng: '{3}'",
                                giaThuoc.GiaThuocGUID.ToString(), giaThuoc.Thuoc.TenThuoc, giaThuoc.GiaBan, giaThuoc.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin giá thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        GiaThuoc gt = db.GiaThuocs.SingleOrDefault<GiaThuoc>(g => g.GiaThuocGUID.ToString() == giaThuoc.GiaThuocGUID.ToString());
                        if (gt != null)
                        {
                            double giaCu = gt.GiaBan;

                            gt.ThuocGUID = giaThuoc.ThuocGUID;
                            gt.GiaBan = giaThuoc.GiaBan;
                            gt.NgayApDung = giaThuoc.NgayApDung;
                            gt.Note = giaThuoc.Note;
                            gt.CreatedDate = giaThuoc.CreatedDate;
                            gt.CreatedBy = giaThuoc.CreatedBy;
                            gt.UpdatedDate = giaThuoc.UpdatedDate;
                            gt.UpdatedBy = giaThuoc.UpdatedBy;
                            gt.DeletedDate = giaThuoc.DeletedDate;
                            gt.DeletedBy = giaThuoc.DeletedBy;
                            gt.Status = giaThuoc.Status;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Thuốc: '{1}', Giá bán: cũ: '{2}' - mới: '{3}', Ngày áp dụng: '{4}'",
                                    gt.GiaThuocGUID.ToString(), gt.Thuoc.TenThuoc, giaCu, gt.GiaBan, gt.NgayApDung.ToString("dd/MM/yyyy HH:mm:ss"));

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin giá thuốc";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
                            tk.ComputerName = Utility.GetDNSHostName();
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
