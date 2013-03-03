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
                    query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView ORDER BY FullName";
                else if (type == 1)
                    query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WHERE AvailableToWork = 'True' ORDER BY FullName";
                else if (type == 2)
                    query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WHERE AvailableToWork = 'False' ORDER BY FullName";
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
                string query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WITH(NOLOCK) WHERE AvailableToWork = 'True' AND DocStaffGUID NOT IN (SELECT DocStaffGUID FROM Logon WHERE DocStaffGUID IS NOT NULL AND Status = 0 AND DocStaffGUID <> '{0}') ORDER BY FullName", docStaffGUID);
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

                string query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM DocStaffView WITH(NOLOCK) WHERE AvailableToWork = 'True' AND StaffType IN {0} ORDER BY FullName", staffTypeStr);
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
