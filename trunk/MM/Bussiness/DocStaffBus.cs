using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class DocStaffBus : BusBase
    {
        public static Result GetDocStaffList(int type)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (type == 0)
                    query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView ORDER BY FirstName, FullName";
                else if (type == 1)
                    query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WHERE AvailableToWork = 'True' ORDER BY FirstName, FullName";
                else if (type == 2)
                    query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WHERE AvailableToWork = 'False' ORDER BY FirstName, FullName";
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

        public static Result GetNhanVienTrungLapList()
        {
            Result result = null;

            try
            {
                string query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView pv1 WHERE pv1.AvailableToWork = 'True' And pv1.FullName in (select pv2.FullName from DocStaffView pv2 where pv2.FullName =pv1.FullName and pv2.Gender = pv1.Gender and pv2.AvailableToWork='True' and pv2.ContactGUID != pv1.ContactGUID) ORDER BY FirstName, FullName";

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

        public static Result GetChuKy(string docStaffGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DocStaff docStaff = (from d in db.DocStaffs
                                    where d.DocStaffGUID.ToString() == docStaffGUID
                                    select d).FirstOrDefault();

                if (docStaff != null && docStaff.ChuKy != null)
                    result.QueryResult = docStaff.ChuKy.ToArray();
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

        public static Result GetDocStaffListWithoutLogon(string docStaffGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WITH(NOLOCK) WHERE AvailableToWork = 'True' AND DocStaffGUID NOT IN (SELECT DocStaffGUID FROM Logon WHERE DocStaffGUID IS NOT NULL AND Status = 0 AND DocStaffGUID <> '{0}') ORDER BY FirstName, FullName", docStaffGUID);
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

        public static Result GetDocStaffList(List<byte> staffTypes)
        {
            Result result = null;

            try
            {
                string staffTypeStr = string.Empty;
                foreach (byte type in staffTypes)
                {
                    staffTypeStr += string.Format("{0},", type);       
                }

                if (staffTypeStr != string.Empty)
                    staffTypeStr = staffTypeStr.Substring(0, staffTypeStr.Length - 1);

                staffTypeStr = string.Format("({0})", staffTypeStr);

                string query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WITH(NOLOCK) WHERE AvailableToWork = 'True' AND StaffType IN {0} ORDER BY FirstName, FullName", staffTypeStr);
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

        public static Result GetDocStaffList(List<byte> staffTypes, string docStaffGUID)
        {
            Result result = null;

            try
            {
                string staffTypeStr = string.Empty;
                foreach (byte type in staffTypes)
                {
                    staffTypeStr += string.Format("{0},", type);
                }

                if (staffTypeStr != string.Empty)
                    staffTypeStr = staffTypeStr.Substring(0, staffTypeStr.Length - 1);

                staffTypeStr = string.Format("({0})", staffTypeStr);

                string query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WITH(NOLOCK) WHERE AvailableToWork = 'True' AND StaffType IN {0} AND DocStaffGUID = '{1}' ORDER BY FirstName, FullName", 
                    staffTypeStr, docStaffGUID);
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

        public static Result DeleteDocStaff(List<String> docStaffKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;

                    foreach (string key in docStaffKeys)
                    {
                        DocStaff docStaff = db.DocStaffs.SingleOrDefault<DocStaff>(d => d.DocStaffGUID.ToString() == key);
                        if (docStaff != null)
                        {
                            docStaff.AvailableToWork = false;
                            Contact contact = docStaff.Contact;
                            if (contact != null)
                            {
                                contact.Archived = true;
                                contact.DateArchived = DateTime.Now;
                                contact.DeletedBy = Guid.Parse(Global.UserGUID);
                                contact.DeletedDate = DateTime.Now;

                                string genderStr = string.Empty;
                                if (contact.Gender == 0) genderStr = "Nam";
                                else if (contact.Gender == 1) genderStr = "Nữ";
                                else genderStr = "Không xác định";

                                desc += string.Format("- GUID: '{0}', Tên nhân viên: '{1}', Ngày sinh: '{2}', Giới tính: '{3}', CMND: '{4}'\n",
                                    docStaff.DocStaffGUID.ToString(), contact.FullName, contact.DobStr, genderStr, contact.IdentityCard);
                            }
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin nhân viên";
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

        public static Result InsertDocStaff(Contact contact, DocStaff docStaff)
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
                    if (contact.ContactGUID == null || contact.ContactGUID == Guid.Empty)
                    {
                        contact.ContactGUID = Guid.NewGuid();
                        db.Contacts.InsertOnSubmit(contact);
                        db.SubmitChanges();
                        docStaff.DocStaffGUID = Guid.NewGuid();
                        docStaff.ContactGUID = contact.ContactGUID;
                        db.DocStaffs.InsertOnSubmit(docStaff);
                        db.SubmitChanges();

                        string genderStr = string.Empty;
                        if (contact.Gender == 0) genderStr = "Nam";
                        else if (contact.Gender == 1) genderStr = "Nữ";
                        else genderStr = "Không xác định";

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Tên nhân viên: '{1}', Ngày sinh: '{2}', Giới tính: '{3}', CMND: '{4}'",
                                    docStaff.DocStaffGUID.ToString(), contact.FullName, contact.DobStr, genderStr, contact.IdentityCard);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin nhân viên";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        Contact ct = db.Contacts.SingleOrDefault<Contact>(cc => cc.ContactGUID.ToString() == contact.ContactGUID.ToString());
                        if (ct != null)
                        {
                            ct.Address = contact.Address;
                            ct.AliasFirstName = contact.AliasFirstName;
                            ct.AliasSurName = contact.AliasSurName;
                            ct.Archived = contact.Archived;
                            ct.City = contact.City;
                            ct.CreatedBy = contact.CreatedBy;
                            ct.CreatedDate = contact.CreatedDate;
                            ct.DateArchived = contact.DateArchived;
                            ct.DeletedBy = contact.DeletedBy;
                            ct.DeletedDate = contact.DeletedDate;
                            ct.District = contact.District;
                            ct.Dob = contact.Dob;
                            ct.DobStr = contact.DobStr;
                            ct.Email = contact.Email;
                            ct.FAX = contact.FAX;
                            ct.FullName = contact.FullName;
                            ct.FirstName = contact.FirstName;
                            ct.Gender = contact.Gender;
                            ct.HomePhone = contact.HomePhone;
                            ct.IdentityCard = contact.IdentityCard;
                            ct.KnownAs = contact.KnownAs;
                            ct.MiddleName = contact.MiddleName;
                            ct.Mobile = contact.Mobile;
                            ct.Note = contact.Note;
                            ct.Occupation = contact.Occupation;
                            ct.PreferredName = contact.PreferredName;
                            ct.SurName = contact.SurName;
                            ct.Title = contact.Title;
                            ct.UpdatedBy = contact.UpdatedBy;
                            ct.UpdatedDate = contact.UpdatedDate;
                            ct.Ward = contact.Ward;
                            ct.WorkPhone = contact.WorkPhone;

                            DocStaff doc = db.DocStaffs.SingleOrDefault<DocStaff>(d => d.DocStaffGUID.ToString() == docStaff.DocStaffGUID.ToString());
                            if (doc != null)
                            {
                                doc.AvailableToWork = docStaff.AvailableToWork;
                                doc.PrescriberNum = docStaff.PrescriberNum;
                                doc.Qualifications = docStaff.Qualifications;
                                doc.SpecialityGUID = docStaff.SpecialityGUID;
                                doc.StaffType = docStaff.StaffType;
                                doc.WorkType = docStaff.WorkType;
                                doc.ChuKy = docStaff.ChuKy;
                            }

                            string genderStr = string.Empty;
                            if (ct.Gender == 0) genderStr = "Nam";
                            else if (ct.Gender == 1) genderStr = "Nữ";
                            else genderStr = "Không xác định";

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Tên nhân viên: '{1}', Ngày sinh: '{2}', Giới tính: '{3}', CMND: '{4}'",
                                        doc.DocStaffGUID.ToString(), ct.FullName, ct.DobStr, genderStr, ct.IdentityCard);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin nhân viên";
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

        public static Result Merge2DocStaffs(string keepDocStaffGuid, string mergeDocStaffGuid)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Guid keepGUID = Guid.Parse(keepDocStaffGuid);
                    Guid mergeGUID = Guid.Parse(mergeDocStaffGuid);
                    Guid userGUID = Guid.Parse(Global.UserGUID);

                    DocStaff keepDocStaff = (from b in db.DocStaffs
                                             where b.DocStaffGUID == keepGUID
                                             select b).FirstOrDefault();

                    DocStaff mergeDocStaff = (from b in db.DocStaffs
                                             where b.DocStaffGUID == mergeGUID
                                             select b).FirstOrDefault();

                    #region Bệnh nhân thân thuộc
                    var benhNhanThanThuocList = from b in db.BenhNhanThanThuocs
                                                   where b.DocStaffGUID == mergeGUID
                                                   select b;

                    if (benhNhanThanThuocList != null)
                    {
                        foreach (var bnngk in benhNhanThanThuocList)
                        {
                            bnngk.DocStaffGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- BenhNhanThanThuocGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (BenhNhanThanThuoc)",
                                bnngk.BenhNhanThanThuocGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Cân đo
                    var canDoList = from c in db.CanDos
                                    where c.DocStaffGUID.Value == mergeGUID
                                    select c;

                    if (canDoList != null)
                    {
                        foreach (var cd in canDoList)
                        {
                            cd.DocStaffGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- CanDoGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (CanDo)",
                                cd.CanDoGuid.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Chỉ định
                    var chiDinhList = from c in db.ChiDinhs
                                      where c.BacSiChiDinhGUID == mergeGUID
                                      select c;

                    if (chiDinhList != null)
                    {
                        foreach (var cd in chiDinhList)
                        {
                            cd.BacSiChiDinhGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- ChiDinhGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (ChiDinh)",
                                cd.ChiDinhGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Công tác ngoài giờ
                    var congTacNgoaiGioList = from c in db.CongTacNgoaiGios
                                              where c.NguoiDeXuatGUID == mergeGUID
                                              select c;

                    if (congTacNgoaiGioList != null)
                    {
                        foreach (var cd in congTacNgoaiGioList)
                        {
                            cd.NguoiDeXuatGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- CongTacNgoaiGioGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (CongTacNgoaiGio)",
                                cd.CongTacNgoaiGioGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Kết luận
                    var ketLuanList = from k in db.KetLuans
                                      where k.DocStaffGUID == mergeGUID
                                      select k;

                    if (ketLuanList != null)
                    {
                        foreach (var kl in ketLuanList)
                        {
                            kl.DocStaffGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetLuanGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (KetLuan)",
                                kl.KetLuanGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Kết quả cận lâm sàng
                    var ketQuaCanLamSangList = from k in db.KetQuaCanLamSangs
                                               where k.BacSiThucHienGUID.Value == mergeGUID
                                               select k;

                    if (ketQuaCanLamSangList != null)
                    {
                        foreach (var kqcls in ketQuaCanLamSangList)
                        {
                            kqcls.BacSiThucHienGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetQuaCanLamSangGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (KetQuaCanLamSang)",
                                kqcls.KetQuaCanLamSangGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Kết quả lâm sàng
                    var ketQuaLamSangList = from k in db.KetQuaLamSangs
                                            where k.DocStaffGUID == mergeGUID
                                            select k;

                    if (ketQuaLamSangList != null)
                    {
                        foreach (var kqls in ketQuaLamSangList)
                        {
                            kqls.DocStaffGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetQuaLamSangGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (KetQuaLamSang)",
                                kqls.KetQuaLamSangGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Kết quả nội soi
                    var ketQuaNoiSoiList = from k in db.KetQuaNoiSois
                                           where k.BacSiChiDinh.Value == mergeGUID
                                           select k;

                    if (ketQuaNoiSoiList != null)
                    {
                        foreach (var kqns in ketQuaNoiSoiList)
                        {
                            kqns.BacSiChiDinh = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetQuaNoiSoiGUID: '{0}': BacSiChiDinhGUID: '{1}' ==> '{2}' (KetQuaNoiSoi)",
                                kqns.KetQuaNoiSoiGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    ketQuaNoiSoiList = from k in db.KetQuaNoiSois
                                           where k.BacSiSoi == mergeGUID
                                           select k;

                    if (ketQuaNoiSoiList != null)
                    {
                        foreach (var kqns in ketQuaNoiSoiList)
                        {
                            kqns.BacSiSoi = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetQuaNoiSoiGUID: '{0}': BacSiSoiGUID: '{1}' ==> '{2}' (KetQuaNoiSoi)",
                                kqns.KetQuaNoiSoiGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Kết quả siêu âm
                    var ketQuaSieuAmList = from k in db.KetQuaSieuAms
                                           where k.BacSiChiDinhGUID.Value == mergeGUID
                                           select k;

                    if (ketQuaSieuAmList != null)
                    {
                        foreach (var kqsa in ketQuaSieuAmList)
                        {
                            kqsa.BacSiChiDinhGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetQuaSieuAmGUID: '{0}': BacSiChiDinhGUID: '{1}' ==> '{2}' (KetQuaSieuAm)",
                                kqsa.KetQuaSieuAmGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    ketQuaSieuAmList = from k in db.KetQuaSieuAms
                                       where k.BacSiSieuAmGUID == mergeGUID
                                       select k;

                    if (ketQuaSieuAmList != null)
                    {
                        foreach (var kqsa in ketQuaSieuAmList)
                        {
                            kqsa.BacSiSieuAmGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetQuaSieuAmGUID: '{0}': BacSiSieuAmGUID: '{1}' ==> '{2}' (KetQuaSieuAm)",
                                kqsa.KetQuaSieuAmGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Kết quả soi CTC
                    var ketQuaSoiCTCList = from k in db.KetQuaSoiCTCs
                                           where k.BacSiSoi == mergeGUID
                                           select k;

                    if (ketQuaSoiCTCList != null)
                    {
                        foreach (var kqsctc in ketQuaSoiCTCList)
                        {
                            kqsctc.BacSiSoi = keepGUID;
                            //Tracking
                            string desc = string.Format("- KetQuaSoiCTCGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (KetQuaSoiCTC)",
                                kqsctc.KetQuaSoiCTCGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Lời khuyên
                    var loiKhuyenList = from l in db.LoiKhuyens
                                        where l.DocStaffGUID == mergeGUID
                                        select l;

                    if (loiKhuyenList != null)
                    {
                        foreach (var lk in loiKhuyenList)
                        {
                            lk.DocStaffGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- LoiKhuyenGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (LoiKhuyen)",
                                lk.LoiKhuyenGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Nhật kí liên hệ công ty
                    var nhatKiLienHeCongTyList = from l in db.NhatKyLienHeCongTies
                                        where l.DocStaffGUID == mergeGUID
                                        select l;

                    if (nhatKiLienHeCongTyList != null)
                    {
                        foreach (var lk in nhatKiLienHeCongTyList)
                        {
                            lk.DocStaffGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- NhatKyLienHeCongTyGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (NhatKyLienHeCongTy)",
                                lk.NhatKyLienHeCongTyGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    nhatKiLienHeCongTyList = from l in db.NhatKyLienHeCongTies
                                             where l.CreatedBy.Value == mergeGUID
                                             select l;

                    if (nhatKiLienHeCongTyList != null)
                    {
                        foreach (var lk in nhatKiLienHeCongTyList)
                        {
                            lk.CreatedBy = keepGUID;
                            //Tracking
                            string desc = string.Format("- NhatKyLienHeCongTyGUID: '{0}': CreatedByGUID: '{1}' ==> '{2}' (NhatKyLienHeCongTy)",
                                lk.NhatKyLienHeCongTyGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    nhatKiLienHeCongTyList = from l in db.NhatKyLienHeCongTies
                                             where l.UpdatedBy.Value == mergeGUID
                                             select l;

                    if (nhatKiLienHeCongTyList != null)
                    {
                        foreach (var lk in nhatKiLienHeCongTyList)
                        {
                            lk.UpdatedBy = keepGUID;
                            //Tracking
                            string desc = string.Format("- NhatKyLienHeCongTyGUID: '{0}': UpdatedByGUID: '{1}' ==> '{2}' (NhatKyLienHeCongTy)",
                                lk.NhatKyLienHeCongTyGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Service History
                    var serviceHistoryList = from s in db.ServiceHistories
                                             where s.DocStaffGUID.Value == mergeGUID
                                             select s;

                    if (serviceHistoryList != null)
                    {
                        foreach (var sh in serviceHistoryList)
                        {
                            sh.DocStaffGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- ServiceHistoryGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (ServiceHistory)",
                                sh.ServiceHistoryGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Thông báo
                    var thongBaoList = from s in db.ThongBaos
                                             where s.NguoiDuyet1GUID.Value == mergeGUID
                                             select s;

                    if (thongBaoList != null)
                    {
                        foreach (var sh in thongBaoList)
                        {
                            sh.NguoiDuyet1GUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- ThongBaoGUID: '{0}': NguoiDuyet1GUID: '{1}' ==> '{2}' (ThongBao)",
                                sh.ThongBaoGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    thongBaoList = from s in db.ThongBaos
                                   where s.NguoiDuyet2GUID.Value == mergeGUID
                                   select s;

                    if (thongBaoList != null)
                    {
                        foreach (var sh in thongBaoList)
                        {
                            sh.NguoiDuyet2GUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- ThongBaoGUID: '{0}': NguoiDuyet2GUID: '{1}' ==> '{2}' (ThongBao)",
                                sh.ThongBaoGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    thongBaoList = from s in db.ThongBaos
                                   where s.NguoiDuyet3GUID.Value == mergeGUID
                                   select s;

                    if (thongBaoList != null)
                    {
                        foreach (var sh in thongBaoList)
                        {
                            sh.NguoiDuyet3GUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- ThongBaoGUID: '{0}': NguoiDuyet3GUID: '{1}' ==> '{2}' (ThongBao)",
                                sh.ThongBaoGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    thongBaoList = from s in db.ThongBaos
                                   where s.CreatedBy.Value == mergeGUID
                                   select s;

                    if (thongBaoList != null)
                    {
                        foreach (var sh in thongBaoList)
                        {
                            sh.CreatedBy = keepGUID;
                            //Tracking
                            string desc = string.Format("- ThongBaoGUID: '{0}': CreatedByGUID: '{1}' ==> '{2}' (ThongBao)",
                                sh.ThongBaoGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    thongBaoList = from s in db.ThongBaos
                                   where s.UpdatedBy.Value == mergeGUID
                                   select s;

                    if (thongBaoList != null)
                    {
                        foreach (var sh in thongBaoList)
                        {
                            sh.UpdatedBy = keepGUID;
                            //Tracking
                            string desc = string.Format("- ThongBaoGUID: '{0}': UpdatedByGUID: '{1}' ==> '{2}' (ThongBao)",
                                sh.ThongBaoGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Toa cấp cứu
                    var toaCapCuuList = from c in db.ToaCapCuus
                                        where c.BacSiKeToaGUID.Value == mergeGUID
                                        select c;

                    if (toaCapCuuList != null)
                    {
                        foreach (var tcc in toaCapCuuList)
                        {
                            tcc.BacSiKeToaGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- ToaCapCuuGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (ToaCapCuu)",
                                tcc.ToaCapCuuGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Toa Thuốc
                    var toaThuocList = from tt in db.ToaThuocs
                                       where tt.BacSiKeToa == mergeGUID
                                       select tt;

                    if (toaThuocList != null)
                    {
                        foreach (var tt in toaThuocList)
                        {
                            tt.BacSiKeToa = keepGUID;
                            //Tracking
                            string desc = string.Format("- ToaThuocGUID: '{0}': DocStaffGUID: '{1}' ==> '{2}' (ToaThuoc)",
                                tt.ToaThuocGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Ý kiến khách hàng
                    var yKienKhachHangList = from y in db.YKienKhachHangs
                                             where y.BacSiPhuTrachGUID.Value == mergeGUID
                                             select y;

                    if (yKienKhachHangList != null)
                    {
                        foreach (var ykkh in yKienKhachHangList)
                        {
                            ykkh.BacSiPhuTrachGUID = keepGUID;
                            //Tracking
                            string desc = string.Format("- YKienKhachHangGUID: '{0}': BacSiPhuTrachGUID: '{1}' ==> '{2}' (YKienKhachHang)",
                                ykkh.YKienKhachHangGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    yKienKhachHangList = from y in db.YKienKhachHangs
                                         where y.ContactBy.Value == mergeGUID
                                         select y;

                    if (yKienKhachHangList != null)
                    {
                        foreach (var ykkh in yKienKhachHangList)
                        {
                            ykkh.ContactBy = keepGUID;
                            //Tracking
                            string desc = string.Format("- YKienKhachHangGUID: '{0}': ContactByGUID: '{1}' ==> '{2}' (YKienKhachHang)",
                                ykkh.YKienKhachHangGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }

                    yKienKhachHangList = from y in db.YKienKhachHangs
                                         where y.UpdatedBy.Value == mergeGUID
                                         select y;

                    if (yKienKhachHangList != null)
                    {
                        foreach (var ykkh in yKienKhachHangList)
                        {
                            ykkh.UpdatedBy = keepGUID;
                            //Tracking
                            string desc = string.Format("- YKienKhachHangGUID: '{0}': UpdatedByGUID: '{1}' ==> '{2}' (YKienKhachHang)",
                                ykkh.YKienKhachHangGUID.ToString(), mergeDocStaffGuid, keepDocStaffGuid);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = userGUID;
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Merge thông tin nhân viên";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);
                        }
                    }
                    #endregion

                    #region Delete Merge DocStaff
                    mergeDocStaff.AvailableToWork = false;
                    mergeDocStaff.Contact.Archived = true;
                    mergeDocStaff.Contact.DeletedDate = DateTime.Now;
                    mergeDocStaff.Contact.DeletedBy = userGUID;
                    mergeDocStaff.Contact.Note += string.Format(" Merge with docstaff: '{0}'", keepDocStaffGuid);
                    #endregion

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
    }
}
