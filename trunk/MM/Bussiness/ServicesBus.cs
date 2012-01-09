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
    public class ServicesBus : BusBase
    {
        public static Result GetServicesList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE Type WHEN 0 THEN N'Lâm sàng' WHEN 1 THEN N'Cận lâm sàng' END AS TypeStr FROM Services WHERE Status={0} ORDER BY Name", (byte)Status.Actived);
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

        public static Result GetServiceCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Services";
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

        public static Result GetServicesListNotInCheckList(string constractGUID, string companyMemberGUID)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (constractGUID != string.Empty && constractGUID != Guid.Empty.ToString())
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WHERE Status={0} AND ServiceGUID NOT IN (SELECT L.ServiceGUID FROM CompanyCheckList L, ContractMember M WHERE M.ContractMemberGUID = L.ContractMemberGUID AND M.CompanyContractGUID = '{2}' AND M.companyMemberGUID = '{1}' AND L.Status = {0}) ORDER BY Name", 
                    (byte)Status.Actived, companyMemberGUID, constractGUID);
                else
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WHERE Status={0} ORDER BY Name", (byte)Status.Actived);

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

        public static Result DeleteServices(List<string> serviceKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in serviceKeys)
                    {
                        Service s = db.Services.SingleOrDefault<Service>(ss => ss.ServiceGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Loại: '{5}'\n",
                                s.ServiceGUID.ToString(), s.Code, s.Name, s.EnglishName, s.Price, s.Type == 0 ? "Lâm sàng" : "Cận lâm sàng");
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin dịch vụ";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
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

        public static Result CheckServicesExistCode(string srvGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Service srv = null;
                if (srvGUID == null || srvGUID == string.Empty)
                    srv = db.Services.SingleOrDefault<Service>(s => s.Code.ToLower() == code.ToLower());
                else
                    srv = db.Services.SingleOrDefault<Service>(s => s.Code.ToLower() == code.ToLower() && 
                                                                s.ServiceGUID.ToString() != srvGUID);

                if (srv == null)
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

        public static Result InsertService(Service service)
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
                    if (service.ServiceGUID == null || service.ServiceGUID == Guid.Empty)
                    {
                        service.ServiceGUID = Guid.NewGuid();
                        db.Services.InsertOnSubmit(service);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Loại: '{5}'",
                                service.ServiceGUID.ToString(), service.Code, service.Name, service.EnglishName, service.Price, service.Type == 0 ? "Lâm sàng" : "Cận lâm sàng");

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin dịch vụ";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        Service srv = db.Services.SingleOrDefault<Service>(s => s.ServiceGUID.ToString() == service.ServiceGUID.ToString());
                        if (srv != null)
                        {
                            double giaCu = srv.Price;
                            srv.Code = service.Code;
                            srv.Name = service.Name;
                            srv.EnglishName = service.EnglishName;
                            srv.Type = service.Type;
                            srv.Price = service.Price;
                            srv.Description = service.Description;
                            srv.CreatedDate = service.CreatedDate;
                            srv.CreatedBy = service.CreatedBy;
                            srv.UpdatedDate = service.UpdatedDate;
                            srv.UpdatedBy = service.UpdatedBy;
                            srv.DeletedDate = service.DeletedDate;
                            srv.DeletedBy = service.DeletedBy;
                            srv.Status = service.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Loại: '{5}'",
                                srv.ServiceGUID.ToString(), srv.Code, srv.Name, srv.EnglishName, srv.Price, srv.Type == 0 ? "Lâm sàng" : "Cận lâm sàng");

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin dịch vụ";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
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
