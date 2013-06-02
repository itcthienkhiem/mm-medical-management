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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE Type WHEN 1 THEN N'Lâm sàng' WHEN 0 THEN N'Cận lâm sàng' END AS TypeStr FROM ServiceView WITH(NOLOCK) WHERE Status={0} ORDER BY Name", (byte)Status.Actived);
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

        public static Result GetServicesAndThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, Code AS MaDichVu, [Name] AS TenDichVu, N'Lần' AS DonViTinh, CAST(0 AS tinyint) AS Loai FROM ServiceView WITH(NOLOCK) WHERE Status={0} UNION SELECT CAST(0 AS Bit) AS Checked, MaThuoc AS MaDichVu, TenThuoc AS TenDichVu, DonViTinh, CAST(1 AS tinyint) AS Loai FROM Thuoc WITH(NOLOCK) WHERE Status={0} ORDER BY TenDichVu", (byte)Status.Actived);
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

        public static Result GetServicesList(string name)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (name.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE Type WHEN 1 THEN N'Lâm sàng' WHEN 0 THEN N'Cận lâm sàng' END AS TypeStr FROM ServiceView WITH(NOLOCK) WHERE Status={0} ORDER BY Name",
                    (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE Type WHEN 1 THEN N'Lâm sàng' WHEN 0 THEN N'Cận lâm sàng' END AS TypeStr FROM ServiceView WITH(NOLOCK) WHERE Name LIKE N'%{0}%' AND Status={1} ORDER BY Name",
                    name, (byte)Status.Actived);
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

        public static Result GetServicesList(ServiceType type)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE Type WHEN 1 THEN N'Lâm sàng' WHEN 0 THEN N'Cận lâm sàng' END AS TypeStr FROM ServiceView WITH(NOLOCK) WHERE Status={0} AND [Type]={1} ORDER BY Name", 
                    (byte)Status.Actived, (byte)type);
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
                string query = "SELECT Count(*) FROM Services WITH(NOLOCK)";
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
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} AND ServiceGUID NOT IN (SELECT L.ServiceGUID FROM CompanyCheckList L WITH(NOLOCK), ContractMember M WITH(NOLOCK) WHERE M.ContractMemberGUID = L.ContractMemberGUID AND M.CompanyContractGUID = '{2}' AND M.companyMemberGUID = '{1}' AND L.Status = {0}) ORDER BY Name", 
                    (byte)Status.Actived, companyMemberGUID, constractGUID);
                else
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} ORDER BY Name", (byte)Status.Actived);

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

        public static Result GetServicesListNotInCheckList(string constractGUID, string companyMemberGUID, string tenDichVu)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (tenDichVu.Trim() == string.Empty)
                {
                    if (constractGUID != string.Empty && constractGUID != Guid.Empty.ToString())
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} AND ServiceGUID NOT IN (SELECT L.ServiceGUID FROM CompanyCheckList L WITH(NOLOCK), ContractMember M WITH(NOLOCK) WHERE M.ContractMemberGUID = L.ContractMemberGUID AND M.CompanyContractGUID = '{2}' AND M.companyMemberGUID = '{1}' AND L.Status = {0}) ORDER BY Name",
                        (byte)Status.Actived, companyMemberGUID, constractGUID);
                    else
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} ORDER BY Name", (byte)Status.Actived);
                }
                else
                {
                    if (constractGUID != string.Empty && constractGUID != Guid.Empty.ToString())
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} AND ServiceGUID NOT IN (SELECT L.ServiceGUID FROM CompanyCheckList L WITH(NOLOCK), ContractMember M WITH(NOLOCK) WHERE M.ContractMemberGUID = L.ContractMemberGUID AND M.CompanyContractGUID = '{2}' AND M.companyMemberGUID = '{1}' AND L.Status = {0}) AND Name LIKE N'%{3}%' ORDER BY Name",
                        (byte)Status.Actived, companyMemberGUID, constractGUID, tenDichVu);
                    else
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} AND Name LIKE N'%{1}%' ORDER BY Name", (byte)Status.Actived, tenDichVu);
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

        public static Result DeleteServices(List<string> serviceKeys, List<string> noteList)
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
                    foreach (string key in serviceKeys)
                    {
                        Service s = db.Services.SingleOrDefault<Service>(ss => ss.ServiceGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;
                            s.Description = noteList[index];

                            string staffTypeStr = string.Empty;
                            if (s.StaffType.HasValue) staffTypeStr = Utility.ParseStaffTypeEnumToName((StaffType)s.StaffType.Value);

                            desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Loại: '{5}', Loại nhân viên: '{6}', Ghi chú: '{7}'\n",
                                s.ServiceGUID.ToString(), s.Code, s.Name, s.EnglishName, s.Price, s.Type == 0 ? "Lâm sàng" : "Cận lâm sàng", staffTypeStr, noteList[index]);
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
                        string staffTypeStr = string.Empty;
                        if (service.StaffType.HasValue) staffTypeStr = Utility.ParseStaffTypeEnumToName((StaffType)service.StaffType.Value);

                        desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Loại: '{5}', Loại nhân viên: '{6}', Ghi chú: '{7}'",
                                service.ServiceGUID.ToString(), service.Code, service.Name, service.EnglishName, service.Price, 
                                service.Type == 0 ? "Lâm sàng" : "Cận lâm sàng", staffTypeStr, service.Description);

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
                            srv.StaffType = service.StaffType;
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
                            string staffTypeStr = string.Empty;
                            if (srv.StaffType.HasValue) staffTypeStr = Utility.ParseStaffTypeEnumToName((StaffType)srv.StaffType.Value);

                            desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Loại: '{5}', Loại nhân viên: '{6}', Ghi chú: '{7}'",
                                srv.ServiceGUID.ToString(), srv.Code, srv.Name, srv.EnglishName, srv.Price,
                                srv.Type == 0 ? "Lâm sàng" : "Cận lâm sàng", staffTypeStr, srv.Description);

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
