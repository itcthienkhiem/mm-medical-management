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
    public class NhatKyLienHeCongTyBus : BusBase
    {
        public static Result GetNhatKyLienHeCongTyList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} ORDER BY NgayGioLienHe DESC", (byte)Status.Actived);
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

        public static Result GetNhatKyLienHeCongTyList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND NgayGioLienHe BETWEEN '{1}' AND '{2}' ORDER BY NgayGioLienHe DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                else
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND CongTyLienHe LIKE N'%{1}%' ORDER BY NgayGioLienHe DESC", 
                        (byte)Status.Actived, tenBenhNhan);
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

        public static Result GetCongTyLienHeList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT CongTyLienHe FROM NhatKyLienHeCongTy ORDER BY CongTyLienHe", (byte)Status.Actived);
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

        public static Result DeleteNhatKyLienHeCongTy(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    int index = 0;
                    foreach (string key in keys)
                    {
                        NhatKyLienHeCongTy s = db.NhatKyLienHeCongTies.SingleOrDefault<NhatKyLienHeCongTy>(ss => ss.NhatKyLienHeCongTyGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;

                            string docStaffGUID = string.Empty;
                            string fullName = "Admin";
                            if (s.DocStaffGUID != null)
                            {
                                docStaffGUID = s.DocStaffGUID.ToString();
                                fullName = s.DocStaff.Contact.FullName;
                            }

                            string thangKham = string.Empty;
                            if (s.ThangKham.HasValue)
                                thangKham = s.ThangKham.Value.ToString("MM/yyyy");

                            desc += string.Format("- GUID: '{0}', Mã nhân viên: '{1}', Tên nhân viên: '{2}', Tên công ty liên hệ: '{3}', Tên người liên hệ: '{4}', Số ĐT liên hệ: '{5}', Số người khám: '{6}', Tháng khám: '{7}', Nội dung liên hệ: '{8}', Ghi chú: '{9}'\n",
                                s.NhatKyLienHeCongTyGUID.ToString(), docStaffGUID, fullName, s.CongTyLienHe, s.TenNguoiLienHe, s.SoDienThoaiLienHe,
                                s.SoNguoiKham, thangKham, s.NoiDungLienHe, s.Note);
                        }

                        index++;
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa nhật ký liên hệ công ty";
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

        public static Result InsertNhatKyLienHeCongTy(NhatKyLienHeCongTy nhatKyLienHeCongTy)
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
                    if (nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID == null || nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID == Guid.Empty)
                    {
                        nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID = Guid.NewGuid();
                        db.NhatKyLienHeCongTies.InsertOnSubmit(nhatKyLienHeCongTy);
                        db.SubmitChanges();

                        //Tracking
                        string docStaffGUID = string.Empty;
                        string fullName = "Admin";
                        if (nhatKyLienHeCongTy.DocStaffGUID != null)
                        {
                            docStaffGUID = nhatKyLienHeCongTy.DocStaffGUID.ToString();
                            fullName = nhatKyLienHeCongTy.DocStaff.Contact.FullName;
                        }

                        string thangKham = string.Empty;
                        if (nhatKyLienHeCongTy.ThangKham.HasValue)
                            thangKham = nhatKyLienHeCongTy.ThangKham.Value.ToString("MM/yyyy");

                        desc += string.Format("- GUID: '{0}', Mã nhân viên: '{1}', Tên nhân viên: '{2}', Tên công ty liên hệ: '{3}', Tên người liên hệ: '{4}', Số ĐT liên hệ: '{5}', Số người khám: '{6}', Tháng khám: '{7}', Nội dung liên hệ: '{8}', Ghi chú: '{9}'",
                                 nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID.ToString(), docStaffGUID, fullName,
                                 nhatKyLienHeCongTy.CongTyLienHe, nhatKyLienHeCongTy.TenNguoiLienHe, nhatKyLienHeCongTy.SoDienThoaiLienHe, nhatKyLienHeCongTy.SoNguoiKham,
                                 thangKham, nhatKyLienHeCongTy.NoiDungLienHe, nhatKyLienHeCongTy.Note);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm nhật ký liên hệ công ty";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        NhatKyLienHeCongTy nklhct = db.NhatKyLienHeCongTies.SingleOrDefault<NhatKyLienHeCongTy>(s => s.NhatKyLienHeCongTyGUID == nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID);
                        if (nklhct != null)
                        {
                            nklhct.DocStaffGUID = nhatKyLienHeCongTy.DocStaffGUID;
                            nklhct.NgayGioLienHe = nhatKyLienHeCongTy.NgayGioLienHe;
                            nklhct.CongTyLienHe = nhatKyLienHeCongTy.CongTyLienHe;
                            nklhct.NoiDungLienHe = nhatKyLienHeCongTy.NoiDungLienHe;
                            nklhct.Note = nhatKyLienHeCongTy.Note;
                            nklhct.CreatedDate = nhatKyLienHeCongTy.CreatedDate;
                            nklhct.CreatedBy = nhatKyLienHeCongTy.CreatedBy;
                            nklhct.UpdatedDate = nhatKyLienHeCongTy.UpdatedDate;
                            nklhct.UpdatedBy = nhatKyLienHeCongTy.UpdatedBy;
                            nklhct.DeletedDate = nhatKyLienHeCongTy.DeletedDate;
                            nklhct.DeletedBy = nhatKyLienHeCongTy.DeletedBy;
                            nklhct.Status = nhatKyLienHeCongTy.Status;
                            nklhct.TenNguoiLienHe = nhatKyLienHeCongTy.TenNguoiLienHe;
                            nklhct.SoDienThoaiLienHe = nhatKyLienHeCongTy.SoDienThoaiLienHe;
                            nklhct.SoNguoiKham = nhatKyLienHeCongTy.SoNguoiKham;
                            nklhct.ThangKham = nhatKyLienHeCongTy.ThangKham;

                            //Tracking
                            string docStaffGUID = string.Empty;
                            string fullName = "Admin";
                            if (nklhct.DocStaffGUID != null)
                            {
                                docStaffGUID = nklhct.DocStaffGUID.ToString();
                                fullName = nklhct.DocStaff.Contact.FullName;
                            }

                            string thangKham = string.Empty;
                            if (nhatKyLienHeCongTy.ThangKham.HasValue)
                                thangKham = nklhct.ThangKham.Value.ToString("MM/yyyy");

                            desc += string.Format("- GUID: '{0}', Mã nhân viên: '{1}', Tên nhân viên: '{2}', Tên công ty liên hệ: '{3}', Tên người liên hệ: '{4}', Số ĐT liên hệ: '{5}', Số người khám: '{6}', Tháng khám: '{7}', Nội dung liên hệ: '{8}', Ghi chú: '{9}'",
                                  nklhct.NhatKyLienHeCongTyGUID.ToString(), docStaffGUID, fullName, nklhct.CongTyLienHe, nklhct.TenNguoiLienHe, nklhct.SoDienThoaiLienHe, 
                                  nklhct.SoNguoiKham, thangKham, nklhct.NoiDungLienHe, nklhct.Note);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa nhật ký liên hệ công ty";
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

        public static Result CheckCongTyLienHeExist(string congTy, string nhatKyLienHeCongTyGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                NhatKyLienHeCongTy nklhct = null;
                if (nhatKyLienHeCongTyGUID == null || nhatKyLienHeCongTyGUID == string.Empty)
                    nklhct = db.NhatKyLienHeCongTies.SingleOrDefault<NhatKyLienHeCongTy>(n => n.CongTyLienHe.ToLower() == congTy.ToLower() &&
                        n.CreatedBy.Value.ToString() == Global.UserGUID && n.Status == (byte)Status.Actived);
                else
                    nklhct = db.NhatKyLienHeCongTies.SingleOrDefault<NhatKyLienHeCongTy>(n => n.CongTyLienHe.ToLower() == congTy.ToLower() &&
                        n.CreatedBy.Value.ToString() == Global.UserGUID && n.NhatKyLienHeCongTyGUID.ToString() != nhatKyLienHeCongTyGUID &&
                        n.Status == (byte)Status.Actived);

                if (nklhct == null)
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
    }
}
