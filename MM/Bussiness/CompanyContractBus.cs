﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class CompanyContractBus : BusBase
    {
        public static Result GetContractList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyContractView WHERE ContractStatus={0} AND CompanyStatus={0} ORDER BY ContractName", (byte)Status.Actived);
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

        public static Result GetContractCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM CompanyContract";
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

        public static Result GetContractMemberList(string contractGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WHERE CompanyContractGUID='{0}' AND Status={1} AND CompanyMemberStatus={1} AND Archived='False' ORDER BY FirstName, FullName",
                    contractGUID, (byte)Status.Actived);
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

        public static Result GetCheckList(string contractMemberGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyCheckListView WHERE ContractMemberGUID='{0}' AND ServiceStatus={1} AND CheckListStatus={1} ORDER BY Name",
                    contractMemberGUID, (byte)Status.Actived);
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

        public static Result GetCheckListByPatient(string patientGUID)
        {
            Result result = null;

            try
            {
                string spName = "spGetCheckList";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@PatientGUID", patientGUID);
                sqlParams.Add(param);

                return ExcuteQuery(spName, sqlParams);
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

        public static Result GetCheckList(string contractGUID, string patientGUID)
        {
            Result result = null;

            try
            {
                string spName = "spGetCheckListByContract";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@ContractGUID", contractGUID);
                sqlParams.Add(param);
                param = new SqlParameter("@PatientGUID", patientGUID);
                sqlParams.Add(param);

                return ExcuteQuery(spName, sqlParams);
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

        public static Result GetDanhSachNhanVien(string contractGUID, int type)
        {
            Result result = null;

            try
            {
                string spName = "spGetDanhSachNhanVien";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@ContractGUID", contractGUID);
                sqlParams.Add(param);
                param = new SqlParameter("@Type", type);
                sqlParams.Add(param);

                return ExcuteQuery(spName, sqlParams);
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

        public static Result DeleteContract(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        CompanyContract c = db.CompanyContracts.SingleOrDefault<CompanyContract>(cc => cc.CompanyContractGUID.ToString() == key);
                        if (c != null)
                        {
                            c.DeletedDate = DateTime.Now;
                            c.DeletedBy = Guid.Parse(Global.UserGUID);
                            c.Status = (byte)Status.Deactived;

                            if (c.EndDate.HasValue)
                            {
                                desc += string.Format("- GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}'\n",
                                    c.CompanyContractGUID.ToString(), c.ContractCode, c.ContractName, c.Company.TenCty, c.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                    c.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                            }
                            else
                            {
                                desc += string.Format("- GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}'\n",
                                    c.CompanyContractGUID.ToString(), c.ContractCode, c.ContractName, c.Company.TenCty, c.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"));
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
                    tk.Action = "Xóa thông tin hợp đồng";
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

        public static Result CheckContractExistCode(string contractGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                CompanyContract contract = null;
                if (contractGUID == null || contractGUID == string.Empty)
                    contract = db.CompanyContracts.SingleOrDefault<CompanyContract>(c => c.ContractCode.ToLower() == code.ToLower());
                else
                    contract = db.CompanyContracts.SingleOrDefault<CompanyContract>(c => c.ContractCode.ToLower() == code.ToLower() &&
                                                                c.CompanyContractGUID.ToString() != contractGUID);

                if (contract == null)
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

        public static Result InsertContract(CompanyContract contract, CompanyInfo companyInfo)
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
                    if (contract.CompanyContractGUID == null || contract.CompanyContractGUID == Guid.Empty)
                    {
                        contract.CompanyContractGUID = Guid.NewGuid();
                        db.CompanyContracts.InsertOnSubmit(contract);
                        db.SubmitChanges();

                        if (contract.EndDate.HasValue)
                        {
                            desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}'\n",
                                contract.CompanyContractGUID.ToString(), contract.ContractCode, contract.ContractName, contract.Company.TenCty,
                                contract.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), contract.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                        }
                        else
                        {
                            desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}'\n",
                                contract.CompanyContractGUID.ToString(), contract.ContractCode, contract.ContractName, contract.Company.TenCty,
                                contract.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"));
                        }

                        //Members
                        if (companyInfo.AddedMembers != null && companyInfo.AddedMembers.Count > 0)
                        {
                            desc += "- Danh sách nhân viên được thêm:\n";
                            foreach (Member member in companyInfo.AddedMembers.Values)
                            {
                                ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == member.CompanyMemberGUID &&
                                                                                                    mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                if (m == null)
                                {
                                    m = new ContractMember();
                                    m.ContractMemberGUID = Guid.NewGuid();
                                    m.CompanyContractGUID = contract.CompanyContractGUID;
                                    m.CompanyMemberGUID = Guid.Parse(member.CompanyMemberGUID);
                                    m.CreatedDate = DateTime.Now;
                                    m.CreatedBy = Guid.Parse(Global.UserGUID);
                                    m.Status = (byte)Status.Actived;
                                    db.ContractMembers.InsertOnSubmit(m);
                                    
                                }
                                else
                                {
                                    m.Status = (byte)Status.Actived;
                                    m.UpdatedDate = DateTime.Now;
                                    m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                }

                                db.SubmitChanges();

                                var companyMember = db.CompanyMembers.SingleOrDefault<CompanyMember>(cm => cm.CompanyMemberGUID == m.CompanyMemberGUID);
                                string tenNhanVien = string.Empty;
                                if (companyMember != null) tenNhanVien = companyMember.Patient.Contact.FullName;
                                desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.ContractMemberGUID.ToString(), tenNhanVien);

                                //Check List
                                if (member.AddedServices != null && member.AddedServices.Count > 0)
                                {
                                    desc += string.Format("     Danh sách dịch vụ được thêm:\n");
                                    foreach (string key in member.AddedServices)
                                    {
                                        CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                            cc.ContractMemberGUID.ToString() == m.ContractMemberGUID.ToString());
                                        if (c == null)
                                        {
                                            c = new CompanyCheckList();
                                            c.CompanyCheckListGUID = Guid.NewGuid();
                                            c.ContractMemberGUID = m.ContractMemberGUID;
                                            c.ServiceGUID = Guid.Parse(key);
                                            c.CreatedDate = DateTime.Now;
                                            c.CreatedBy = Guid.Parse(Global.UserGUID);
                                            c.Status = (byte)Status.Actived;
                                            db.CompanyCheckLists.InsertOnSubmit(c);
                                            db.SubmitChanges();
                                        }
                                        else
                                        {
                                            c.Status = (byte)Status.Actived;
                                            c.UpdatedDate = DateTime.Now;
                                            c.UpdatedBy = Guid.Parse(Global.UserGUID);
                                            db.SubmitChanges();
                                        }

                                        desc += string.Format("     * GUID: '{0}', Dịch vụ: {1}\n", c.CompanyCheckListGUID.ToString(), c.Service.Name);
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
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin hợp đồng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);
                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        CompanyContract con = db.CompanyContracts.SingleOrDefault<CompanyContract>(c => c.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                        if (con != null)
                        {
                            con.CompanyGUID = contract.CompanyGUID;
                            con.ContractCode = contract.ContractCode;
                            con.ContractName = contract.ContractName;
                            con.BeginDate = contract.BeginDate;
                            con.EndDate = contract.EndDate;
                            con.Completed = contract.Completed;
                            con.CreatedDate = contract.CreatedDate;
                            con.CreatedBy = contract.CreatedBy;
                            con.UpdatedDate = contract.UpdatedDate;
                            con.UpdatedBy = contract.UpdatedBy;
                            con.DeletedDate = contract.DeletedDate;
                            con.DeletedBy = contract.DeletedBy;
                            con.Status = contract.Status;
                            db.SubmitChanges();

                            if (con.EndDate.HasValue)
                            {
                                desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}'\n",
                                    con.CompanyContractGUID.ToString(), con.ContractCode, con.ContractName, con.Company.TenCty,
                                    con.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), con.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                            }
                            else
                            {
                                desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}'\n",
                                    con.CompanyContractGUID.ToString(), con.ContractCode, con.ContractName, con.Company.TenCty,
                                    con.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"));
                            }

                            //Members
                            if (companyInfo.DeletedMembers != null && companyInfo.DeletedMembers.Count > 0)
                            {
                                desc += "- Danh sách nhân viên được xóa:\n";
                                foreach (string key in companyInfo.DeletedMembers)
                                {
                                    ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == key &&
                                                                            mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                    if (m != null)
                                    {
                                        m.Status = (byte)Status.Deactived;
                                        m.DeletedDate = DateTime.Now;
                                        m.DeletedBy = Guid.Parse(Global.UserGUID);

                                        var companyMember = db.CompanyMembers.SingleOrDefault<CompanyMember>(cm => cm.CompanyMemberGUID == m.CompanyMemberGUID);
                                        string tenNhanVien = string.Empty;
                                        if (companyMember != null) tenNhanVien = companyMember.Patient.Contact.FullName;
                                        desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.ContractMemberGUID.ToString(), tenNhanVien);
                                    }
                                }

                                db.SubmitChanges();
                            }

                            if (companyInfo.AddedMembers != null && companyInfo.AddedMembers.Count > 0)
                            {
                                desc += "- Danh sách nhân viên được thêm:\n";
                                foreach (Member member in companyInfo.AddedMembers.Values)
                                {
                                    ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == member.CompanyMemberGUID &&
                                                                                                        mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                    if (m == null)
                                    {
                                        m = new ContractMember();
                                        m.ContractMemberGUID = Guid.NewGuid();
                                        m.CompanyContractGUID = contract.CompanyContractGUID;
                                        m.CompanyMemberGUID = Guid.Parse(member.CompanyMemberGUID);
                                        m.CreatedDate = DateTime.Now;
                                        m.CreatedBy = Guid.Parse(Global.UserGUID);
                                        m.Status = (byte)Status.Actived;
                                        db.ContractMembers.InsertOnSubmit(m);
                                    }
                                    else
                                    {
                                        m.Status = (byte)Status.Actived;
                                        m.UpdatedDate = DateTime.Now;
                                        m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    }

                                    db.SubmitChanges();

                                    var companyMember = db.CompanyMembers.SingleOrDefault<CompanyMember>(cm => cm.CompanyMemberGUID == m.CompanyMemberGUID);
                                    string tenNhanVien = string.Empty;
                                    if (companyMember != null) tenNhanVien = companyMember.Patient.Contact.FullName;
                                    desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.ContractMemberGUID.ToString(), tenNhanVien);


                                    //Delete Service
                                    if (member.DeletedServices != null && member.DeletedServices.Count > 0)
                                    {
                                        desc += string.Format("     Danh sách dịch vụ được xóa:\n");

                                        foreach (string key in member.DeletedServices)
                                        {
                                            CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                                cc.ContractMemberGUID.ToString() == m.ContractMemberGUID.ToString());
                                            if (c != null)
                                            {
                                                c.Status = (byte)Status.Deactived;
                                                c.DeletedDate = DateTime.Now;
                                                c.DeletedBy = Guid.Parse(Global.UserGUID);

                                                desc += string.Format("     * GUID: '{0}', Dịch vụ: {1}\n", c.CompanyCheckListGUID.ToString(), c.Service.Name);
                                            }
                                        }

                                        db.SubmitChanges();
                                    }

                                    //Add Service
                                    if (member.AddedServices != null && member.AddedServices.Count > 0)
                                    {
                                        desc += string.Format("     Danh sách dịch vụ được thêm:\n");
                                        foreach (string key in member.AddedServices)
                                        {
                                            CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                                cc.ContractMemberGUID.ToString() == m.ContractMemberGUID.ToString());
                                            if (c == null)
                                            {
                                                c = new CompanyCheckList();
                                                c.CompanyCheckListGUID = Guid.NewGuid();
                                                c.ContractMemberGUID = m.ContractMemberGUID;
                                                c.ServiceGUID = Guid.Parse(key);
                                                c.CreatedDate = DateTime.Now;
                                                c.CreatedBy = Guid.Parse(Global.UserGUID);
                                                c.Status = (byte)Status.Actived;
                                                db.CompanyCheckLists.InsertOnSubmit(c);
                                                db.SubmitChanges();
                                            }
                                            else
                                            {
                                                c.Status = (byte)Status.Actived;
                                                c.UpdatedDate = DateTime.Now;
                                                c.UpdatedBy = Guid.Parse(Global.UserGUID);
                                                db.SubmitChanges();
                                            }

                                            desc += string.Format("     * GUID: '{0}', Dịch vụ: {1}\n", c.CompanyCheckListGUID.ToString(), c.Service.Name);
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
                            tk.Action = "Sửa thông tin hợp đồng";
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