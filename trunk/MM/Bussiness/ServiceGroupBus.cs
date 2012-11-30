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
    public class ServiceGroupBus : BusBase
    {
        public static Result GetServicesGroupList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ServiceGroup WITH(NOLOCK) WHERE Status={0} ORDER BY Name", (byte)Status.Actived);
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

        public static Result GetServiceListByGroup(string serviceGroupGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Service_ServiceGroupView WITH(NOLOCK) WHERE Service_ServiceGroupStatus={0} AND ServiceStatus={0} AND ServiceGroupStatus={0} AND ServiceGroupGUID = '{1}' ORDER BY Name", 
                    (byte)Status.Actived, serviceGroupGUID);
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

        public static Result GetServiceListNotInGroup()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} AND ServiceGUID NOT IN (SELECT ServiceGUID FROM Service_ServiceGroupView WHERE Service_ServiceGroupStatus={0} AND ServiceStatus={0} AND ServiceGroupStatus={0}) ORDER BY Name",
                    (byte)Status.Actived);
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

        public static Result GetServiceListNotInGroup(string tenDichVu)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (tenDichVu.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} AND ServiceGUID NOT IN (SELECT ServiceGUID FROM Service_ServiceGroupView WITH(NOLOCK) WHERE Service_ServiceGroupStatus={0} AND ServiceStatus={0} AND ServiceGroupStatus={0}) ORDER BY Name",
                        (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM Services WITH(NOLOCK) WHERE Status={0} AND ServiceGUID NOT IN (SELECT ServiceGUID FROM Service_ServiceGroupView WITH(NOLOCK) WHERE Service_ServiceGroupStatus={0} AND ServiceStatus={0} AND ServiceGroupStatus={0}) AND Name LIKE N'%{1}%' ORDER BY Name",
                        (byte)Status.Actived, tenDichVu);
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

        public static Result GetServiceGroupCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM ServiceGroup WITH(NOLOCK)";
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

        

        public static Result CheckServiceGroupExistCode(string srvGroupGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ServiceGroup srvGroup = null;
                if (srvGroupGUID == null || srvGroupGUID == string.Empty)
                    srvGroup = db.ServiceGroups.SingleOrDefault<ServiceGroup>(s => s.Code.ToLower() == code.ToLower());
                else
                    srvGroup = db.ServiceGroups.SingleOrDefault<ServiceGroup>(s => s.Code.ToLower() == code.ToLower() &&
                                                                s.ServiceGroupGUID.ToString() != srvGroupGUID);

                if (srvGroup == null)
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

        public static Result CheckServiceExist(string serviceGUID)
        {
            Result result = new Result();

            MMOverride db = null;

            try
            {
                db = new MMOverride();

                Service_ServiceGroupView s = db.Service_ServiceGroupViews.SingleOrDefault<Service_ServiceGroupView>(ss => ss.ServiceGUID.ToString() == serviceGUID &&
                                                                                    ss.ServiceStatus == (byte)Status.Actived &&
                                                                                    ss.ServiceGroupStatus == (byte)Status.Actived &&
                                                                                    ss.Service_ServiceGroupStatus == (byte)Status.Actived);

                if (s == null)
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

        public static Result DeleteServiceGroup(List<string> serviceGroupKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in serviceGroupKeys)
                    {
                        ServiceGroup g = db.ServiceGroups.SingleOrDefault<ServiceGroup>(gg => gg.ServiceGroupGUID.ToString() == key);
                        if (g != null)
                        {
                            g.DeletedDate = DateTime.Now;
                            g.DeletedBy = Guid.Parse(Global.UserGUID);
                            g.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã nhóm: '{1}', Tên nhóm: '{2}'\n", g.ServiceGroupGUID.ToString(), g.Code, g.Name);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin nhóm dịch vụ";
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

        public static Result InsertServiceGroup(ServiceGroup serviceGroup, List<Service_ServiceGroup> addedList, List<string> deletedKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (serviceGroup.ServiceGroupGUID == null || serviceGroup.ServiceGroupGUID == Guid.Empty)
                    {
                        serviceGroup.ServiceGroupGUID = Guid.NewGuid();
                        db.ServiceGroups.InsertOnSubmit(serviceGroup);
                        db.SubmitChanges();

                        desc += string.Format("- Nhóm dịch vụ: GUID: '{0}', Mã nhóm: '{1}', Tên nhóm: '{2}'\n",
                                serviceGroup.ServiceGroupGUID.ToString(), serviceGroup.Code, serviceGroup.Name);

                        if (addedList != null && addedList.Count > 0)
                        {
                            desc += "- Danh sách dịch vụ được thêm:\n";

                            //Danh sách dịch vụ
                            foreach (Service_ServiceGroup ssg in addedList)
                            {
                                ssg.Service_GroupServiceGUID = Guid.NewGuid();
                                ssg.ServiceGroupGUID = serviceGroup.ServiceGroupGUID;
                                db.Service_ServiceGroups.InsertOnSubmit(ssg);
                                db.SubmitChanges();

                                desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}'\n", ssg.Service_GroupServiceGUID.ToString(), ssg.Service.Name);
                            }
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin nhóm dịch vụ";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        ServiceGroup srvGroup = db.ServiceGroups.SingleOrDefault<ServiceGroup>(o => o.ServiceGroupGUID == serviceGroup.ServiceGroupGUID);
                        if (srvGroup != null)
                        {
                            srvGroup.Code = serviceGroup.Code;
                            srvGroup.Name = serviceGroup.Name;
                            srvGroup.EnglishName = serviceGroup.EnglishName;
                            srvGroup.CreatedDate = serviceGroup.CreatedDate;
                            srvGroup.CreatedBy = serviceGroup.CreatedBy;
                            srvGroup.UpdatedDate = serviceGroup.UpdatedDate;
                            srvGroup.UpdatedBy = serviceGroup.UpdatedBy;
                            srvGroup.DeletedDate = serviceGroup.DeletedDate;
                            srvGroup.DeletedBy = serviceGroup.DeletedBy;
                            srvGroup.Status = serviceGroup.Status;
                            db.SubmitChanges();

                            desc += string.Format("- Nhóm dịch vụ: GUID: '{0}', Mã nhóm: '{1}', Tên nhóm: '{2}'\n",
                                srvGroup.ServiceGroupGUID.ToString(), srvGroup.Code, serviceGroup.Name);

                            //Delete dịch vụ
                            if (deletedKeys != null && deletedKeys.Count > 0)
                            {
                                desc += "- Danh sách dịch vụ được xóa:\n";
                                foreach (string key in deletedKeys)
                                {
                                    Service_ServiceGroup ssg = db.Service_ServiceGroups.SingleOrDefault<Service_ServiceGroup>(c => c.ServiceGUID.ToString() == key &&
                                                                                                                                c.Status == (byte)Status.Actived);
                                    if (ssg != null)
                                    {
                                        ssg.DeletedDate = DateTime.Now;
                                        ssg.DeletedBy = Guid.Parse(Global.UserGUID);
                                        ssg.Status = (byte)Status.Deactived;

                                        desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}'\n", ssg.Service_GroupServiceGUID.ToString(), ssg.Service.Name);
                                    }
                                }

                                db.SubmitChanges();
                            }


                            //Add dịch vụ
                            if (addedList != null && addedList.Count > 0)
                            {
                                desc += "- Danh sách dịch vụ được thêm:\n";
                                foreach (Service_ServiceGroup ssg in addedList)
                                {
                                    ssg.Service_GroupServiceGUID = Guid.NewGuid();
                                    ssg.ServiceGroupGUID = serviceGroup.ServiceGroupGUID;
                                    db.Service_ServiceGroups.InsertOnSubmit(ssg);
                                    db.SubmitChanges();

                                    desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}'\n", ssg.Service_GroupServiceGUID.ToString(), ssg.Service.Name);
                                }
                            }

                            //Tracking
                            desc = desc.Substring(0, desc.Length - 1);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin nhóm dịch vụ";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

                    tnx.Complete();
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

        public static Result GetServiceGroup(string serviceGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Service_ServiceGroup service_ServiceGroup = db.Service_ServiceGroups.SingleOrDefault<Service_ServiceGroup>(s => s.ServiceGUID.ToString() == serviceGUID && 
                                                                                                                           s.Status == (byte)Status.Actived);
                if (service_ServiceGroup != null)
                {
                    if (service_ServiceGroup.ServiceGroup.Status == (byte)Status.Actived)
                        result.QueryResult = service_ServiceGroup.ServiceGroup;
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
