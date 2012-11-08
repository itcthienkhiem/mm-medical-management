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
    public class CompanyBus : BusBase
    {
        public static Result GetCompanyList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Company WITH(NOLOCK) WHERE Status={0} ORDER BY TenCty", (byte)Status.Actived);
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

        public static Result GetCompanyCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Company WITH(NOLOCK)";
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

        public static Result GetCompanyMemberList(string companyGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyMemberView WITH(NOLOCK) WHERE CompanyGUID='{0}' AND Status={1} AND Archived='False' ORDER BY FirstName, FullName",
                    companyGUID, (byte)Status.Actived);
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

        public static Result GetCompanyMemberListNotInContractMember(string companyGUID, string contractGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyMemberView WITH(NOLOCK) WHERE CompanyGUID='{0}' AND Status={1} AND Archived='False' AND CompanyMemberGUID NOT IN (SELECT CompanyMemberGUID FROM ContractMember WHERE CompanyContractGUID = '{2}' AND Status = {1}) ORDER BY FirstName, FullName",
                    companyGUID, (byte)Status.Actived, contractGUID);
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

        public static Result DeleteCompany(List<string> keys)
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
                        Company c = db.Companies.SingleOrDefault<Company>(cc => cc.CompanyGUID.ToString() == key);
                        if (c != null)
                        {
                            c.DeletedDate = DateTime.Now;
                            c.DeletedBy = Guid.Parse(Global.UserGUID);
                            c.Status = (byte)Status.Deactived;

                            foreach (var cm in c.CompanyMembers)
                                cm.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã cty: '{1}', Tên cty: '{2}', Địa chỉ: '{3}', Điện thoại: '{4}', Fax: '{5}', Website: '{6}'\n",
                                c.CompanyGUID.ToString(), c.MaCty, c.TenCty, c.DiaChi, c.Dienthoai, c.Fax, c.Website);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin công ty";
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

        public static Result CheckCompanyExistCode(string companyGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Company com = null;
                if (companyGUID == null || companyGUID == string.Empty)
                    com = db.Companies.SingleOrDefault<Company>(c => c.MaCty.ToLower() == code.ToLower());
                else
                    com = db.Companies.SingleOrDefault<Company>(c => c.MaCty.ToLower() == code.ToLower() &&
                                                                c.CompanyGUID.ToString() != companyGUID);

                if (com == null)
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

        public static Result CheckMemberExist(string patientGUID)
        {
            Result result = new Result();

            MMOverride db = null;

            try
            {
                db = new MMOverride();

                CompanyMember m = db.CompanyMembers.SingleOrDefault<CompanyMember>(mm => mm.PatientGUID.ToString() == patientGUID &&
                                                                                    mm.Status == (byte)Status.Actived && 
                                                                                    mm.Company.Status == (byte)Status.Actived);

                if (m == null)
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

        public static Result InsertCompany(Company com, List<string> addedMembers, List<string> deletedMembers)
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
                    if (com.CompanyGUID == null || com.CompanyGUID == Guid.Empty)
                    {
                        com.CompanyGUID = Guid.NewGuid();
                        db.Companies.InsertOnSubmit(com);
                        db.SubmitChanges();

                        desc += string.Format("- Công ty: GUID: '{0}', Mã cty: '{1}', Tên cty: '{2}', Địa chỉ: '{3}', Điện thoại: '{4}', Fax: '{5}', Website: '{6}'\n",
                                com.CompanyGUID.ToString(), com.MaCty, com.TenCty, com.DiaChi, com.Dienthoai, com.Fax, com.Website);

                        //Members
                        if (addedMembers != null && addedMembers.Count > 0)
                        {
                            desc += "- Danh sách nhân viên:\n";
                            foreach (string key in addedMembers)
                            {
                                CompanyMember m = db.CompanyMembers.SingleOrDefault<CompanyMember>(mm => mm.PatientGUID.ToString() == key &&
                                                                                                    mm.CompanyGUID.ToString() == com.CompanyGUID.ToString());
                                if (m == null)
                                {
                                    m = new CompanyMember();
                                    m.CompanyMemberGUID = Guid.NewGuid();
                                    m.CompanyGUID = com.CompanyGUID;
                                    m.PatientGUID = Guid.Parse(key);
                                    m.CreatedDate = DateTime.Now;
                                    m.CreatedBy = Guid.Parse(Global.UserGUID);
                                    m.Status = (byte)Status.Actived;
                                    db.CompanyMembers.InsertOnSubmit(m);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    m.Status = (byte)Status.Actived;
                                    m.UpdatedDate = DateTime.Now;
                                    m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                }

                                desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.CompanyMemberGUID.ToString(), m.Patient.Contact.FullName);
                            }

                            db.SubmitChanges();
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin công ty";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);
                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        Company company = db.Companies.SingleOrDefault<Company>(c => c.CompanyGUID.ToString() == com.CompanyGUID.ToString());
                        if (company != null)
                        {
                            company.MaCty = com.MaCty;
                            company.TenCty = com.TenCty;
                            company.DiaChi = com.DiaChi;
                            company.Dienthoai = com.Dienthoai;
                            company.Fax = com.Fax;
                            company.Website = com.Website;
                            company.CreatedDate = com.CreatedDate;
                            company.CreatedBy = com.CreatedBy;
                            company.UpdatedDate = com.UpdatedDate;
                            company.UpdatedBy = com.UpdatedBy;
                            company.DeletedDate = com.DeletedDate;
                            company.DeletedBy = com.DeletedBy;
                            company.Status = com.Status;

                            desc += string.Format("- Công ty: GUID: '{0}', Mã cty: '{1}', Tên cty: '{2}', Địa chỉ: '{3}', Điện thoại: '{4}', Fax: '{5}', Website: '{6}'\n",
                                company.CompanyGUID.ToString(), company.MaCty, company.TenCty, company.DiaChi, company.Dienthoai, company.Fax, company.Website);

                            //Members
                            if (deletedMembers != null && deletedMembers.Count > 0)
                            {
                                desc += "- Danh sách nhân viên được xóa:\n";
                                foreach (string key in deletedMembers)
                                {
                                    CompanyMember m = db.CompanyMembers.SingleOrDefault<CompanyMember>(mm => mm.PatientGUID.ToString() == key &&
                                                                                                        mm.CompanyGUID.ToString() == com.CompanyGUID.ToString());
                                    if (m != null)
                                    {
                                        m.Status = (byte)Status.Deactived;
                                        m.DeletedDate = DateTime.Now;
                                        m.DeletedBy = Guid.Parse(Global.UserGUID);

                                        desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.CompanyMemberGUID.ToString(), m.Patient.Contact.FullName);
                                    }
                                }
                                    
                                db.SubmitChanges();
                            }

                            if (addedMembers != null && addedMembers.Count > 0)
                            {
                                string addedStr = string.Empty;
                                bool isAdd = false;

                                foreach (string key in addedMembers)
                                {
                                    CompanyMember m = db.CompanyMembers.SingleOrDefault<CompanyMember>(mm => mm.PatientGUID.ToString() == key &&
                                                                                                    mm.CompanyGUID.ToString() == com.CompanyGUID.ToString());
                                    if (m == null)
                                    {
                                        m = new CompanyMember();
                                        m.CompanyMemberGUID = Guid.NewGuid();
                                        m.CompanyGUID = com.CompanyGUID;
                                        m.PatientGUID = Guid.Parse(key);
                                        m.CreatedDate = DateTime.Now;
                                        m.CreatedBy = Guid.Parse(Global.UserGUID);
                                        m.Status = (byte)Status.Actived;
                                        db.CompanyMembers.InsertOnSubmit(m);
                                        db.SubmitChanges();

                                        addedStr += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.CompanyMemberGUID.ToString(), m.Patient.Contact.FullName);
                                        isAdd = true;
                                    }
                                    else
                                    {
                                        if (m.Status == (byte)Status.Deactived)
                                        {
                                            addedStr += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.CompanyMemberGUID.ToString(), m.Patient.Contact.FullName);
                                            isAdd = true;
                                        }

                                        m.Status = (byte)Status.Actived;
                                        m.UpdatedDate = DateTime.Now;
                                        m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    }
                                }

                                db.SubmitChanges();

                                if (isAdd) addedStr = "- Danh sách nhân viên được thêm:\n" + addedStr;
                                desc += addedStr;
                            }

                            //Tracking
                            desc = desc.Substring(0, desc.Length - 1);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin công ty";
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

        public static Result GetTenCongTy(string patientGUID)
        {
            Result result = new Result();

            MMOverride db = null;

            try
            {
                db = new MMOverride();

                CompanyMember m = db.CompanyMembers.SingleOrDefault<CompanyMember>(mm => mm.PatientGUID.ToString() == patientGUID &&
                                                                                    mm.Status == (byte)Status.Actived &&
                                                                                    mm.Company.Status == (byte)Status.Actived);
                if (m != null)
                {
                    result.QueryResult = m.Company.TenCty;
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
