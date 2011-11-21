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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Company WHERE Status={0} ORDER BY TenCty", (byte)Status.Actived);
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

        public static Result GetCompanyMemberList(string companyGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyMemberView WHERE CompanyGUID='{0}' AND Status={1} AND Archived='True' ORDER BY FullName",
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

        public static Result DeleteCompany(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        Company c = db.Companies.SingleOrDefault<Company>(cc => cc.CompanyGUID.ToString() == key);
                        if (c != null)
                        {
                            c.DeletedDate = DateTime.Now;
                            c.DeletedBy = Guid.Parse(Global.UserGUID);
                            c.Status = (byte)Status.Deactived;
                        }
                    }

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
                                                                                    mm.Status == (byte)Status.Actived);

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

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (com.CompanyGUID == null || com.CompanyGUID == Guid.Empty)
                    {
                        com.CompanyGUID = Guid.NewGuid();
                        db.Companies.InsertOnSubmit(com);
                        db.SubmitChanges();

                        //Members
                        if (addedMembers != null && addedMembers.Count > 0)
                        {
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
                                }
                                else
                                {
                                    m.Status = (byte)Status.Actived;
                                    m.UpdatedDate = DateTime.Now;
                                    m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                }
                            }

                            db.SubmitChanges();
                        }
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

                            //Members
                            if (deletedMembers != null && deletedMembers.Count > 0)
                            {
                                foreach (string key in deletedMembers)
                                {
                                    CompanyMember m = db.CompanyMembers.SingleOrDefault<CompanyMember>(mm => mm.PatientGUID.ToString() == key &&
                                                                                                        mm.CompanyGUID.ToString() == com.CompanyGUID.ToString());
                                    if (m != null)
                                    {
                                        m.Status = (byte)Status.Deactived;
                                        m.DeletedDate = DateTime.Now;
                                        m.DeletedBy = Guid.Parse(Global.UserGUID);
                                    }
                                }

                                db.SubmitChanges();
                            }

                            if (addedMembers != null && addedMembers.Count > 0)
                            {
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
                                    }
                                    else
                                    {
                                        m.Status = (byte)Status.Actived;
                                        m.UpdatedDate = DateTime.Now;
                                        m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    }
                                }

                                db.SubmitChanges();
                            }

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
