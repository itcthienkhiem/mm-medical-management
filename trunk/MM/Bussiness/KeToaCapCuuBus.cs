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
    public class KeToaCapCuuBus : BusBase
    {
        public static Result GetToaCapCuuList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaCapCuuView WITH(NOLOCK) WHERE Status={0} ORDER BY NgayKeToa DESC",
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

        public static Result GetToaCapCuuList(bool isAll, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;

                if (isAll)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaCapCuuView WITH(NOLOCK) WHERE Status={0} ORDER BY NgayKeToa DESC",
                        (byte)Status.Actived);
                else
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaCapCuuView WITH(NOLOCK) WHERE Status={0} AND NgayKeToa BETWEEN '{1}' AND '{2}' ORDER BY NgayKeToa DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

        public static Result GetChiTietToaCapCuu(string toaCapCuuGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietToaCapCuuView WITH(NOLOCK) WHERE KhoCapCuuStatus={0} AND Status={0} AND ToaCapCuuGUID='{1}' ORDER BY TenCapCuu",
                    (byte)Status.Actived, toaCapCuuGUID);
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

        public static Result GetToaCapCuu(string toaCapCuuGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ToaCapCuuView toaCapCuu = db.ToaCapCuuViews.SingleOrDefault<ToaCapCuuView>(t => t.ToaCapCuuGUID.ToString() == toaCapCuuGUID);
                result.QueryResult = toaCapCuu;
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

        public static Result GetToaCapCuuCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM ToaCapCuu WITH(NOLOCK)";
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

        public static Result DeleteToaCapCuu(List<string> keys)
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
                        ToaCapCuu toaCapCuu = db.ToaCapCuus.SingleOrDefault<ToaCapCuu>(tt => tt.ToaCapCuuGUID.ToString() == key);
                        if (toaCapCuu != null)
                        {
                            toaCapCuu.DeletedDate = DateTime.Now;
                            toaCapCuu.DeletedBy = Guid.Parse(Global.UserGUID);
                            toaCapCuu.Status = (byte)Status.Deactived;

                            string basSiKeToa = string.Empty;
                            if (toaCapCuu.DocStaff != null)
                                basSiKeToa = toaCapCuu.DocStaff.Contact.FullName;

                            desc += string.Format("- GUID: '{0}', Mã toa cấp cứu: '{1}', Ngày kê toa: '{2}', Bác sĩ kê toa: '{3}', Bệnh nhân: '{4}', Ghi chú: '{5}'\n",
                                toaCapCuu.ToaCapCuuGUID.ToString(), toaCapCuu.MaToaCapCuu, toaCapCuu.NgayKeToa.ToString("dd/MM/yyyy HH:mm:ss"), basSiKeToa, 
                                toaCapCuu.TenBenhNhan, toaCapCuu.Note);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin toa cấp cứu";
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

        public static Result CheckToaCapCuuExistCode(string toaCapCuuGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ToaCapCuu tcc = null;
                if (toaCapCuuGUID == null || toaCapCuuGUID == string.Empty)
                    tcc = db.ToaCapCuus.SingleOrDefault<ToaCapCuu>(o => o.MaToaCapCuu.ToLower() == code.ToLower());
                else
                    tcc = db.ToaCapCuus.SingleOrDefault<ToaCapCuu>(o => o.MaToaCapCuu.ToLower() == code.ToLower() &&
                                                                o.ToaCapCuuGUID.ToString() != toaCapCuuGUID);

                if (tcc == null)
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

        public static Result InsertToaCapCuu(ToaCapCuu toaCapCuu, List<ChiTietToaCapCuu> addedList, List<string> deletedKeys)
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
                    if (toaCapCuu.ToaCapCuuGUID == null || toaCapCuu.ToaCapCuuGUID == Guid.Empty)
                    {
                        toaCapCuu.ToaCapCuuGUID = Guid.NewGuid();
                        db.ToaCapCuus.InsertOnSubmit(toaCapCuu);
                        db.SubmitChanges();

                        string basSiKeToa = string.Empty;
                        if (toaCapCuu.DocStaff != null)
                            basSiKeToa = toaCapCuu.DocStaff.Contact.FullName;

                        desc += string.Format("-Toa cấp cứu GUID: '{0}', Mã toa cấp cứu: '{1}', Ngày kê toa: '{2}', Bác sĩ kê toa: '{3}', Bệnh nhân: '{4}', Ghi chú: '{5}'\n",
                            toaCapCuu.ToaCapCuuGUID.ToString(), toaCapCuu.MaToaCapCuu, toaCapCuu.NgayKeToa.ToString("dd/MM/yyyy HH:mm:ss"), basSiKeToa,
                            toaCapCuu.TenBenhNhan, toaCapCuu.Note);

                        if (addedList != null && addedList.Count > 0)
                        {
                            desc += "- Chi tiết toa cấp cứu được thêm:\n";

                            //Chi tiet toa cấp cứu
                            foreach (ChiTietToaCapCuu cttcc in addedList)
                            {
                                cttcc.ChiTietToaCapCuuGUID = Guid.NewGuid();
                                cttcc.ToaCapCuuGUID = toaCapCuu.ToaCapCuuGUID;
                                db.ChiTietToaCapCuus.InsertOnSubmit(cttcc);
                                db.SubmitChanges();

                                desc += string.Format("  + GUID: '{0}', Tên cấp cứu: '{1}', Số lượng: '{2}'\n", cttcc.ChiTietToaCapCuuGUID.ToString(),
                                    cttcc.KhoCapCuu.TenCapCuu, cttcc.SoLuong);
                            }
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin toa cấp cứu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        ToaCapCuu tcc = db.ToaCapCuus.SingleOrDefault<ToaCapCuu>(o => o.ToaCapCuuGUID.ToString() == toaCapCuu.ToaCapCuuGUID.ToString());
                        if (tcc != null)
                        {
                            tcc.MaToaCapCuu = toaCapCuu.MaToaCapCuu;
                            tcc.NgayKeToa = toaCapCuu.NgayKeToa;
                            tcc.BacSiKeToaGUID = toaCapCuu.BacSiKeToaGUID;
                            tcc.TenBenhNhan = toaCapCuu.TenBenhNhan;
                            tcc.MaBenhNhan = toaCapCuu.MaBenhNhan;
                            tcc.DiaChi = toaCapCuu.DiaChi;
                            tcc.TenCongTy = toaCapCuu.TenCongTy;
                            tcc.Note = toaCapCuu.Note;
                            tcc.CreatedDate = toaCapCuu.CreatedDate;
                            tcc.CreatedBy = toaCapCuu.CreatedBy;
                            tcc.UpdatedDate = toaCapCuu.UpdatedDate;
                            tcc.UpdatedBy = toaCapCuu.UpdatedBy;
                            tcc.DeletedDate = toaCapCuu.DeletedDate;
                            tcc.DeletedBy = toaCapCuu.DeletedBy;
                            tcc.Status = toaCapCuu.Status;
                            db.SubmitChanges();

                            string basSiKeToa = string.Empty;
                            if (tcc.DocStaff != null)
                                basSiKeToa = tcc.DocStaff.Contact.FullName;

                            desc += string.Format("-Toa cấp cứu GUID: '{0}', Mã toa cấp cứu: '{1}', Ngày kê toa: '{2}', Bác sĩ kê toa: '{3}', Bệnh nhân: '{4}', Ghi chú: '{5}'\n",
                                tcc.ToaCapCuuGUID.ToString(), tcc.MaToaCapCuu, tcc.NgayKeToa.ToString("dd/MM/yyyy HH:mm:ss"), basSiKeToa,
                                tcc.TenBenhNhan, tcc.Note);

                            //Delete chi tiet toa cấp cứu
                            if (deletedKeys != null && deletedKeys.Count > 0)
                            {
                                desc += "- Chi tiết toa cấp cứu được xóa:\n";
                                foreach (string key in deletedKeys)
                                {
                                    ChiTietToaCapCuu cttcc = db.ChiTietToaCapCuus.SingleOrDefault<ChiTietToaCapCuu>(c => c.ChiTietToaCapCuuGUID.ToString() == key);
                                    if (cttcc != null)
                                    {
                                        cttcc.DeletedDate = DateTime.Now;
                                        cttcc.DeletedBy = Guid.Parse(Global.UserGUID);
                                        cttcc.Status = (byte)Status.Deactived;

                                        desc += string.Format("  + GUID: '{0}', Tên cấp cứu: '{1}', Số lượng: '{2}'\n", cttcc.ChiTietToaCapCuuGUID.ToString(),
                                        cttcc.KhoCapCuu.TenCapCuu, cttcc.SoLuong);
                                    }
                                }

                                db.SubmitChanges();
                            }
                            

                            //Add chi tiet toa cấp cứu
                            if (addedList != null && addedList.Count > 0)
                            {
                                desc += "- Chi tiết toa cấp cứu được thêm:\n";
                                foreach (ChiTietToaCapCuu cttcc in addedList)
                                {
                                    cttcc.ToaCapCuuGUID = tcc.ToaCapCuuGUID;
                                    if (cttcc.ChiTietToaCapCuuGUID == Guid.Empty)
                                    {
                                        cttcc.ChiTietToaCapCuuGUID = Guid.NewGuid();
                                        db.ChiTietToaCapCuus.InsertOnSubmit(cttcc);
                                        db.SubmitChanges();

                                        desc += string.Format("  + GUID: '{0}', Tên cấp cứu: '{1}', Số lượng: '{2}'\n", cttcc.ChiTietToaCapCuuGUID.ToString(),
                                        cttcc.KhoCapCuu.TenCapCuu, cttcc.SoLuong);
                                    }
                                    else
                                    {
                                        ChiTietToaCapCuu chiTietToaCapCuu = db.ChiTietToaCapCuus.SingleOrDefault<ChiTietToaCapCuu>(c => c.ChiTietToaCapCuuGUID == cttcc.ChiTietToaCapCuuGUID);
                                        if (chiTietToaCapCuu != null)
                                        {
                                            chiTietToaCapCuu.KhoCapCuuGUID = cttcc.KhoCapCuuGUID;
                                            chiTietToaCapCuu.SoLuong = cttcc.SoLuong;
                                            chiTietToaCapCuu.Note = cttcc.Note;
                                            chiTietToaCapCuu.Status = (byte)Status.Actived;
                                            chiTietToaCapCuu.UpdatedDate = cttcc.UpdatedDate;
                                            chiTietToaCapCuu.UpdatedBy = cttcc.UpdatedBy;
                                            db.SubmitChanges();

                                            desc += string.Format("  + GUID: '{0}', Tên cấp cứu: '{1}', Số lượng: '{2}'\n", 
                                                chiTietToaCapCuu.ChiTietToaCapCuuGUID.ToString(), chiTietToaCapCuu.KhoCapCuu.TenCapCuu, chiTietToaCapCuu.SoLuong);
                                        }
                                    }
                                }
                            }

                            //Tracking
                            desc = desc.Substring(0, desc.Length - 1);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin toa cấp cứu";
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
