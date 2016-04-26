/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
    public class UserGroupBus : BusBase
    {
        public static Result GetUserGroupList()
        {
            Result result = null;

            try
            {
                string query = "SELECT CAST(0 AS Bit) AS Checked, * FROM UserGroup WITH(NOLOCK) WHERE Status = 0 ORDER BY GroupName";
                result = ExcuteQuery(query);
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


        public static Result GetPermission(string userGroupGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM UserGroup_PermissionView WITH(NOLOCK) WHERE UserGroupGUID = '{0}' AND FunctionCode <> 'InKetQuaKhamSucKhoeTongQuat' ORDER BY FunctionName", userGroupGUID);
                result = ExcuteQuery(query);
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

        public static Result GetFunction()
        {
            Result result = null;

            try
            {
                string query = "SELECT * FROM [Function] WITH(NOLOCK) WHERE FunctionCode <> 'InKetQuaKhamSucKhoeTongQuat' ORDER BY FunctionName";
                result = ExcuteQuery(query);
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

        public static Result DeleteUserGroup(List<string> keys)
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
                        UserGroup userGroup = db.UserGroups.SingleOrDefault<UserGroup>(l => l.UserGroupGUID.ToString() == key);
                        if (userGroup != null)
                        {
                            userGroup.DeletedDate = DateTime.Now;
                            userGroup.DeletedBy = Guid.Parse(Global.UserGUID);
                            userGroup.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Tên nhóm người sử dụng: '{1}'\n", userGroup.UserGroupGUID.ToString(), userGroup.GroupName);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin nhóm người sử dụng";
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

        public static Result CheckUserGroupExist(string userGroupGUID, string groupName)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                UserGroup userGroup = null;
                if (userGroupGUID == null || userGroupGUID == string.Empty)
                    userGroup = db.UserGroups.SingleOrDefault<UserGroup>(l => l.GroupName.ToLower() == groupName.ToLower() && l.Status == 0);
                else
                    userGroup = db.UserGroups.SingleOrDefault<UserGroup>(l => l.GroupName.ToLower() == groupName.ToLower() &&
                                                                l.UserGroupGUID.ToString() != userGroupGUID && l.Status == 0);

                if (userGroup == null)
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

        public static Result InsertUserGroup(UserGroup userGroup, DataTable dtPermission)
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
                    if (userGroup.UserGroupGUID == null || userGroup.UserGroupGUID == Guid.Empty)
                    {
                        UserGroup usrgr = db.UserGroups.SingleOrDefault<UserGroup>(g => g.GroupName.ToLower() == userGroup.GroupName.ToLower());
                        if (usrgr == null)
                        {
                            userGroup.UserGroupGUID = Guid.NewGuid();
                            db.UserGroups.InsertOnSubmit(userGroup);
                            db.SubmitChanges();

                            desc += string.Format("- GUID: '{0}', Tên nhóm người sử dụng: '{1}'\n", userGroup.UserGroupGUID.ToString(), userGroup.GroupName);
                        }
                        else
                        {
                            userGroup.UserGroupGUID = usrgr.UserGroupGUID;
                            usrgr.GroupName = userGroup.GroupName;
                            usrgr.UpdatedDate = userGroup.UpdatedDate;
                            usrgr.UpdatedBy = userGroup.UpdatedBy;
                            usrgr.Status = (byte)Status.Actived;

                            var permissions = from p in db.UserGroup_Permissions
                                              where p.UserGroupGUID == usrgr.UserGroupGUID
                                              select p;

                            db.UserGroup_Permissions.DeleteAllOnSubmit(permissions);
                            db.SubmitChanges();

                            desc += string.Format("- GUID: '{0}', Tên nhóm người sử dụng: '{1}'\n", usrgr.UserGroupGUID.ToString(), usrgr.GroupName);
                        }

                        desc += string.Format("- Permission:\n");
                        //Permission
                        foreach (DataRow row in dtPermission.Rows)
                        {
                            UserGroup_Permission p = new UserGroup_Permission();
                            p.UserGroup_PermissionGUID = Guid.NewGuid();
                            p.UserGroupGUID = userGroup.UserGroupGUID;
                            p.FunctionGUID = Guid.Parse(row["FunctionGUID"].ToString());
                            p.IsView = Convert.ToBoolean(row["IsView"]);
                            p.IsAdd = Convert.ToBoolean(row["IsAdd"]);
                            p.IsEdit = Convert.ToBoolean(row["IsEdit"]);
                            p.IsDelete = Convert.ToBoolean(row["IsDelete"]);
                            p.IsPrint = Convert.ToBoolean(row["IsPrint"]);
                            p.IsExport = Convert.ToBoolean(row["IsExport"]);
                            p.IsImport = Convert.ToBoolean(row["IsImport"]);
                            p.IsConfirm = Convert.ToBoolean(row["IsConfirm"]);
                            p.IsLock = Convert.ToBoolean(row["IsLock"]);
                            p.IsExportAll = Convert.ToBoolean(row["IsExportAll"]);
                            p.IsCreateReport = Convert.ToBoolean(row["IsCreateReport"]);
                            p.IsUpload = Convert.ToBoolean(row["IsUpload"]);
                            p.IsSendSMS = Convert.ToBoolean(row["IsSendSMS"]);
                            p.CreatedDate = DateTime.Now;
                            p.CreatedBy = Guid.Parse(Global.UserGUID);
                            db.UserGroup_Permissions.InsertOnSubmit(p);
                            db.SubmitChanges();

                            desc += string.Format("   + {0}: Xem:{1}, Thêm:{2}, Sửa:{3}, Xóa:{4}, In:{5}, Xuất:{6}, Nhập:{7}, Duyệt:{8}, Khóa:{9}, Xuất hết:{10}, Tạo hồ sơ: {11}, Upload hồ sơ:{12}, Gửi SMS:{13}\n",
                                p.Function.FunctionName, p.IsView, p.IsAdd, p.IsEdit, p.IsDelete, p.IsPrint, p.IsExport, p.IsImport, p.IsConfirm, p.IsLock, 
                                p.IsExportAll, p.IsCreateReport, p.IsUpload, p.IsSendSMS);
                        }
                        
                        //Tracking
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin nhóm người sử dụng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        UserGroup usrgr = db.UserGroups.SingleOrDefault<UserGroup>(g => g.UserGroupGUID == userGroup.UserGroupGUID);
                        if (usrgr != null)
                        {
                            usrgr.GroupName = userGroup.GroupName;
                            usrgr.UpdatedDate = userGroup.UpdatedDate;
                            usrgr.UpdatedBy = userGroup.UpdatedBy;
                            usrgr.Status = userGroup.Status;

                            desc += string.Format("- GUID: '{0}', Tên nhóm người sử dụng: '{1}'\n", usrgr.UserGroupGUID.ToString(), usrgr.GroupName);
                            
                            //Permission
                            desc += string.Format("- Permission:\n");
                            foreach (DataRow row in dtPermission.Rows)
                            {
                                UserGroup_Permission p = null;
                                if (row["UserGroup_PermissionGUID"] != null && row["UserGroup_PermissionGUID"] != DBNull.Value)
                                {
                                    string permissionGUID = row["UserGroup_PermissionGUID"].ToString();
                                    p = db.UserGroup_Permissions.SingleOrDefault<UserGroup_Permission>(pp => pp.UserGroup_PermissionGUID.ToString() == permissionGUID);
                                    if (p != null)
                                    {
                                        p.IsView = Convert.ToBoolean(row["IsView"]);
                                        p.IsAdd = Convert.ToBoolean(row["IsAdd"]);
                                        p.IsEdit = Convert.ToBoolean(row["IsEdit"]);
                                        p.IsDelete = Convert.ToBoolean(row["IsDelete"]);
                                        p.IsPrint = Convert.ToBoolean(row["IsPrint"]);
                                        p.IsExport = Convert.ToBoolean(row["IsExport"]);
                                        p.IsImport = Convert.ToBoolean(row["IsImport"]);
                                        p.IsConfirm = Convert.ToBoolean(row["IsConfirm"]);
                                        p.IsLock = Convert.ToBoolean(row["IsLock"]);
                                        p.IsExportAll = Convert.ToBoolean(row["IsExportAll"]);
                                        p.IsCreateReport = Convert.ToBoolean(row["IsCreateReport"]);
                                        p.IsUpload = Convert.ToBoolean(row["IsUpload"]);
                                        p.IsSendSMS = Convert.ToBoolean(row["IsSendSMS"]);
                                        p.UpdatedDate = DateTime.Now;
                                        p.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    }
                                }
                                else
                                {
                                    p = new UserGroup_Permission();
                                    p.UserGroup_PermissionGUID = Guid.NewGuid();
                                    p.UserGroupGUID = userGroup.UserGroupGUID;
                                    p.FunctionGUID = Guid.Parse(row["FunctionGUID"].ToString());
                                    p.IsView = Convert.ToBoolean(row["IsView"]);
                                    p.IsAdd = Convert.ToBoolean(row["IsAdd"]);
                                    p.IsEdit = Convert.ToBoolean(row["IsEdit"]);
                                    p.IsDelete = Convert.ToBoolean(row["IsDelete"]);
                                    p.IsPrint = Convert.ToBoolean(row["IsPrint"]);
                                    p.IsExport = Convert.ToBoolean(row["IsExport"]);
                                    p.IsImport = Convert.ToBoolean(row["IsImport"]);
                                    p.IsConfirm = Convert.ToBoolean(row["IsConfirm"]);
                                    p.IsLock = Convert.ToBoolean(row["IsLock"]);
                                    p.IsExportAll = Convert.ToBoolean(row["IsExportAll"]);
                                    p.IsCreateReport = Convert.ToBoolean(row["IsCreateReport"]);
                                    p.IsUpload = Convert.ToBoolean(row["IsUpload"]);
                                    p.IsSendSMS = Convert.ToBoolean(row["IsSendSMS"]);
                                    p.CreatedDate = DateTime.Now;
                                    p.CreatedBy = Guid.Parse(Global.UserGUID);
                                    db.UserGroup_Permissions.InsertOnSubmit(p);
                                }

                                db.SubmitChanges();

                                desc += string.Format("   + {0}: Xem:{1}, Thêm:{2}, Sửa:{3}, Xóa:{4}, In:{5}, Xuất:{6}, Nhập:{7}, Duyệt:{8}, Khóa:{9}, Xuất hết:{10}, Tạo hồ sơ: {11}, Upload hồ sơ:{12}, Gửi SMS:{13}\n",
                                p.Function.FunctionName, p.IsView, p.IsAdd, p.IsEdit, p.IsDelete, p.IsPrint, p.IsExport, p.IsImport, p.IsConfirm, p.IsLock,
                                p.IsExportAll, p.IsCreateReport, p.IsUpload, p.IsSendSMS);
                            }

                            //Tracking
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin nhóm người sử dụng";
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

        public static Result GetNhomNguoiSuDung(string logonGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<UserGroup> userGroups = (from g in db.UserGroup_Logons
                                              join u in db.UserGroups on g.UserGroupGUID equals u.UserGroupGUID
                                              where g.LogonGUID.ToString() == logonGUID && u.Status == (byte)Status.Actived
                                              select u).ToList();

                result.QueryResult = userGroups;
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

        public static Result CheckIgnorePermission(string userGroupdGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {

                db = new MMOverride();
                UserGroup_Permission userGroup = (from g in db.UserGroup_Permissions
                                                 where g.UserGroupGUID.ToString() == userGroupdGUID &&
                                                 Global.IgnorePermissions.Contains(g.FunctionGUID.ToString().ToUpper()) &&
                                                 (g.IsView == true || g.IsAdd == true || g.IsEdit == true ||
                                                 g.IsDelete == true || g.IsPrint == true || g.IsImport == true ||
                                                 g.IsExport == true || g.IsConfirm == true || g.IsLock == true ||
                                                 g.IsExportAll == true)
                                                 select g).FirstOrDefault();

                if (userGroup == null)
                    result.QueryResult = true;
                else
                    result.QueryResult = false;
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
