using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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

        public static Result GetContractMemberList(string contractGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WHERE CompanyContractGUID='{0}' AND Status={1} AND CompanyMemberStatus={1} ORDER BY FullName",
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

        public static Result GetCheckList(string contractGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyCheckListView WHERE CompanyContractGUID='{0}' AND ServiceStatus={1} AND CheckListStatus={1} ORDER BY Name",
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

        public static Result DeleteContract(List<string> keys)
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
                        CompanyContract c = db.CompanyContracts.SingleOrDefault<CompanyContract>(cc => cc.CompanyContractGUID.ToString() == key);
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

        public static Result InsertContract(CompanyContract contract, List<string> addedMembers, List<string> deletedMembers,
            List<string> addedServices, List<string> deletedServices)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                //Insert
                if (contract.CompanyContractGUID == null || contract.CompanyContractGUID == Guid.Empty)
                {
                    contract.CompanyContractGUID = Guid.NewGuid();
                    db.CompanyContracts.InsertOnSubmit(contract);
                    db.SubmitChanges();

                    //Members
                    if (addedMembers != null && addedMembers.Count > 0)
                    {
                        foreach (string key in addedMembers)
                        {
                            ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == key &&
                                                                                                mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                            if (m == null)
                            {
                                m = new ContractMember();
                                m.ContractMemberGUID = Guid.NewGuid();
                                m.CompanyContractGUID = contract.CompanyContractGUID;
                                m.CompanyMemberGUID = Guid.Parse(key);
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
                        }

                        db.SubmitChanges();
                    }

                    //Check List
                    if (addedServices != null && addedServices.Count > 0)
                    {
                        foreach (string key in addedServices)
                        {
                            CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                cc.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                            if (c == null)
                            {
                                c = new CompanyCheckList();
                                c.CompanyCheckListGUID = Guid.NewGuid();
                                c.CompanyContractGUID = contract.CompanyContractGUID;
                                c.ServiceGUID = Guid.Parse(key);
                                c.CreatedDate = DateTime.Now;
                                c.CreatedBy = Guid.Parse(Global.UserGUID);
                                c.Status = (byte)Status.Actived;
                                db.CompanyCheckLists.InsertOnSubmit(c);
                            }
                            else
                            {
                                c.Status = (byte)Status.Actived;
                                c.UpdatedDate = DateTime.Now;
                                c.UpdatedBy = Guid.Parse(Global.UserGUID);
                            }
                        }

                        db.SubmitChanges();
                    }
                }
                else //Update
                {
                    CompanyContract con = db.CompanyContracts.SingleOrDefault<CompanyContract>(c => c.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                    if (con != null)
                    {
                        var contractMembers = from c in db.CompanyContracts
                                              join m in db.ContractMembers on c.CompanyContractGUID equals m.CompanyContractGUID
                                              where c.CompanyGUID != contract.CompanyGUID &&
                                              c.CompanyContractGUID == contract.CompanyContractGUID
                                              select m;

                        foreach (var c in contractMembers)
                        {
                            c.Status = (byte)Status.Deactived;
                            c.DeletedDate = DateTime.Now;
                            c.DeletedBy = Guid.Parse(Global.UserGUID);
                        }

                        con.CompanyGUID = contract.CompanyGUID;
                        con.ContractCode = contract.ContractCode;
                        con.ContractName = contract.ContractName;
                        con.BeginDate = contract.BeginDate;
                        con.Completed = contract.Completed;
                        con.CreatedDate = contract.CreatedDate;
                        con.CreatedBy = contract.CreatedBy;
                        con.UpdatedDate = contract.UpdatedDate;
                        con.UpdatedBy = contract.UpdatedBy;
                        con.DeletedDate = contract.DeletedDate;
                        con.DeletedBy = contract.DeletedBy;
                        con.Status = contract.Status;
                        db.SubmitChanges();

                        //Members

                        if (deletedMembers != null && deletedMembers.Count > 0)
                        {
                            foreach (string key in deletedMembers)
                            {
                                ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == key &&
                                                                        mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
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
                                ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == key &&
                                                                                                    mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                if (m == null)
                                {
                                    m = new ContractMember();
                                    m.ContractMemberGUID = Guid.NewGuid();
                                    m.CompanyContractGUID = contract.CompanyContractGUID;
                                    m.CompanyMemberGUID = Guid.Parse(key);
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
                            }

                            db.SubmitChanges();
                        }

                        //Check List
                        if (deletedServices != null && deletedServices.Count > 0)
                        {
                            foreach (string key in deletedServices)
                            {
                                CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                    cc.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                if (c != null)
                                {
                                    c.Status = (byte)Status.Deactived;
                                    c.DeletedDate = DateTime.Now;
                                    c.DeletedBy = Guid.Parse(Global.UserGUID);
                                }
                            }

                            db.SubmitChanges();
                        }

                        if (addedServices != null && addedServices.Count > 0)
                        {
                            foreach (string key in addedServices)
                            {
                                CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                    cc.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                if (c == null)
                                {
                                    c = new CompanyCheckList();
                                    c.CompanyCheckListGUID = Guid.NewGuid();
                                    c.CompanyContractGUID = contract.CompanyContractGUID;
                                    c.ServiceGUID = Guid.Parse(key);
                                    c.CreatedDate = DateTime.Now;
                                    c.CreatedBy = Guid.Parse(Global.UserGUID);
                                    c.Status = (byte)Status.Actived;
                                    db.CompanyCheckLists.InsertOnSubmit(c);
                                }
                                else
                                {
                                    c.Status = (byte)Status.Actived;
                                    c.UpdatedDate = DateTime.Now;
                                    c.UpdatedBy = Guid.Parse(Global.UserGUID);
                                }
                            }

                            db.SubmitChanges();
                        }

                    }
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
