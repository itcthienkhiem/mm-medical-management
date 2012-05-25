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

        public static Result GetNhatKyLienHeCongTyList(int type, DateTime fromDate, DateTime toDate, string tenBenhNhan, string tenNguoiTao, int thang)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (type == 0)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND NgayGioLienHe BETWEEN '{1}' AND '{2}' ORDER BY NgayGioLienHe DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                else if (type == 1)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND CongTyLienHe LIKE N'%{1}%' ORDER BY NgayGioLienHe DESC", 
                        (byte)Status.Actived, tenBenhNhan);
                else if (type == 2)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND FullName LIKE N'{1}%' ORDER BY NgayGioLienHe DESC",
                        (byte)Status.Actived, tenNguoiTao);
                else if (type == 3)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND CongTyLienHe IN (SELECT CongTyLienHe FROM NhatKyLienHeCongTy WHERE Status = 0 GROUP BY CongTyLienHe HAVING Count(CongTyLienHe) >= 2) ORDER BY CongTyLienHe",
                        (byte)Status.Actived, tenNguoiTao);
                else
                {
                    DateTime date = new DateTime(2000, thang, 1);
                    string monthStr1 = date.ToString("MMM");
                    string monthStr2 = date.ToString("MMMM");

                    //query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND (REPLACE(REPLACE(REPLACE(ThangKham, '11', 'Nov'), '12', 'Dec'), '10', 'Oct') LIKE N'%{1}%' OR REPLACE(REPLACE(REPLACE(ThangKham, '11', 'Nov'), '12', 'Dec'), '10', 'Oct') LIKE N'%{2}%' OR REPLACE(REPLACE(REPLACE(ThangKham, '11', 'Nov'), '12', 'Dec'), '10', 'Oct') LIKE N'%{3}%') ORDER BY NgayGioLienHe DESC",
                    //      (byte)Status.Actived, thang, monthStr1, monthStr2);

                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM NhatKyLienHeCongTyView WHERE Status={0} AND (REPLACE(REPLACE(REPLACE(ThangKham, '11', 'Nov'), '12', 'Dec'), '10', 'Oct') LIKE N'%{1}%' OR REPLACE(REPLACE(REPLACE(ThangKham, '11', 'Nov'), '12', 'Dec'), '10', 'Oct') LIKE N'%{2}%') ORDER BY NgayGioLienHe DESC",
                          (byte)Status.Actived, monthStr1, monthStr2);
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

                            desc += string.Format("- GUID: '{0}', Mã nhân viên: '{1}', Tên nhân viên: '{2}', Tên công ty liên hệ: '{3}', Tên người liên hệ: '{4}', Đia chỉ (Quận): '{5}', Số ĐT liên hệ: '{6}', Email liên hệ: '{7}', Số người khám: '{8}', Tháng khám: '{9}', Nội dung liên hệ: '{10}', Ghi chú: '{11}', Highlight: '{12}', Số ngày: '{13}'\n",
                                s.NhatKyLienHeCongTyGUID.ToString(), docStaffGUID, fullName, s.CongTyLienHe, s.TenNguoiLienHe, s.DiaChi, s.SoDienThoaiLienHe,
                                s.Email, s.SoNguoiKham, s.ThangKham, s.NoiDungLienHe, s.Note, s.Highlight, s.SoNgay);
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

                        desc += string.Format("- GUID: '{0}', Mã nhân viên: '{1}', Tên nhân viên: '{2}', Tên công ty liên hệ: '{3}', Tên người liên hệ: '{4}', Đia chỉ (Quận): '{5}', Số ĐT liên hệ: '{6}', Email liên hệ: '{7}', Số người khám: '{8}', Tháng khám: '{9}', Nội dung liên hệ: '{10}', Ghi chú: '{11}', Highlight: '{12}'",
                                 nhatKyLienHeCongTy.NhatKyLienHeCongTyGUID.ToString(), docStaffGUID, fullName,
                                 nhatKyLienHeCongTy.CongTyLienHe, nhatKyLienHeCongTy.TenNguoiLienHe, nhatKyLienHeCongTy.DiaChi, nhatKyLienHeCongTy.SoDienThoaiLienHe, 
                                 nhatKyLienHeCongTy.Email, nhatKyLienHeCongTy.SoNguoiKham, nhatKyLienHeCongTy.ThangKham, nhatKyLienHeCongTy.NoiDungLienHe, nhatKyLienHeCongTy.Note, nhatKyLienHeCongTy.Highlight);

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
                            nklhct.DiaChi = nhatKyLienHeCongTy.DiaChi;
                            nklhct.Email = nhatKyLienHeCongTy.Email;
                            nklhct.Highlight = nhatKyLienHeCongTy.Highlight;
                            nklhct.SoNgay = nhatKyLienHeCongTy.SoNgay;

                            //Tracking
                            string docStaffGUID = string.Empty;
                            string fullName = "Admin";
                            if (nklhct.DocStaffGUID != null)
                            {
                                docStaffGUID = nklhct.DocStaffGUID.ToString();
                                fullName = nklhct.DocStaff.Contact.FullName;
                            }

                            desc += string.Format("- GUID: '{0}', Mã nhân viên: '{1}', Tên nhân viên: '{2}', Tên công ty liên hệ: '{3}', Tên người liên hệ: '{4}', Đia chỉ (Quận): '{5}', Số ĐT liên hệ: '{6}', Email liên hệ: '{7}', Số người khám: '{8}', Tháng khám: '{9}', Nội dung liên hệ: '{10}', Ghi chú: '{11}', Highlight: '{12}', Số ngày: '{13}'",
                                  nklhct.NhatKyLienHeCongTyGUID.ToString(), docStaffGUID, fullName, nklhct.CongTyLienHe, nklhct.TenNguoiLienHe, nklhct.DiaChi,
                                  nklhct.SoDienThoaiLienHe, nklhct.Email, nklhct.SoNguoiKham, nklhct.ThangKham, nklhct.NoiDungLienHe, nklhct.Note, nklhct.Highlight, nklhct.SoNgay);

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

        public static Result CheckCongTyLienHeExist2(string congTy, string nhatKyLienHeCongTyGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                NhatKyLienHeCongTy nklhct = null;
                if (nhatKyLienHeCongTyGUID == null || nhatKyLienHeCongTyGUID == string.Empty)
                    nklhct = db.NhatKyLienHeCongTies.SingleOrDefault<NhatKyLienHeCongTy>(n => n.CongTyLienHe.ToLower() == congTy.ToLower() &&
                        n.CreatedBy.Value.ToString() != Global.UserGUID && n.Status == (byte)Status.Actived);
                else
                    nklhct = db.NhatKyLienHeCongTies.SingleOrDefault<NhatKyLienHeCongTy>(n => n.CongTyLienHe.ToLower() == congTy.ToLower() &&
                        n.CreatedBy.Value.ToString() != Global.UserGUID && n.NhatKyLienHeCongTyGUID.ToString() != nhatKyLienHeCongTyGUID &&
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
