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
    public class LogonBus : BusBase
    {
        public static Result GetUserList()
        {
            Result result = null;

            try
            {
                string query = "SELECT * FROM UserView WHERE AvailableToWork = 'True' AND Status = 0 ORDER BY FirstName, Fullname";
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

        public static Result GetUserListWithoutAdmin()
        {
            Result result = null;

            try
            {
                string query = "SELECT CAST(0 AS Bit) AS Checked, * FROM UserView WHERE AvailableToWork = 'True' AND Status = 0 AND LogonGUID <> '00000000-0000-0000-0000-000000000000' ORDER BY FirstName, Fullname";
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

        public static Result GetPermission(string logonGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM PermissionView WITH(NOLOCK) WHERE LogonGUID = '{0}' ORDER BY FunctionName", logonGUID);
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
                string query = "SELECT * FROM [Function] ORDER BY FunctionName";
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

        public static Result DeleteUserLogon(List<string> keys)
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
                        Logon logon = db.Logons.SingleOrDefault<Logon>(l => l.LogonGUID.ToString() == key);
                        if (logon != null)
                        {
                            logon.DeletedDate = DateTime.Now;
                            logon.DeletedBy = Guid.Parse(Global.UserGUID);
                            logon.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Nhân viên: '{1}'\n", logon.LogonGUID.ToString(), logon.DocStaff.Contact.FullName);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin người sử dụng";
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

        public static Result CheckUserLogonExist(string logonGUID, string docStaffGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Logon logon = null;
                if (logonGUID == null || logonGUID == string.Empty)
                    logon = db.Logons.SingleOrDefault<Logon>(l => l.DocStaffGUID.ToString() == docStaffGUID && l.Status == 0);
                else
                    logon = db.Logons.SingleOrDefault<Logon>(l => l.DocStaffGUID.ToString() == docStaffGUID &&
                                                                l.LogonGUID.ToString() != logonGUID && l.Status == 0);

                if (logon == null)
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

        public static Result InsertUserLogon(Logon logon, DataTable dtPermission)
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
                    if (logon.LogonGUID == null || logon.LogonGUID == Guid.Empty)
                    {
                        string logonGUID = string.Empty;
                        Logon l = db.Logons.SingleOrDefault<Logon>(ll => ll.DocStaffGUID.ToString() == logon.DocStaffGUID.ToString());
                        if (l == null)
                        {
                            logon.LogonGUID = Guid.NewGuid();
                            db.Logons.InsertOnSubmit(logon);
                            db.SubmitChanges();
                            logonGUID = logon.LogonGUID.ToString();
                            db.SubmitChanges();

                            desc += string.Format("- GUID: '{0}', Nhân viên: '{1}'", logon.LogonGUID.ToString(), logon.DocStaff.Contact.FullName);
                        }
                        else
                        {
                            logon.LogonGUID = l.LogonGUID;
                            l.Password = logon.Password;
                            l.UpdatedDate = logon.UpdatedDate;
                            l.UpdatedBy = logon.UpdatedBy;
                            l.Status = (byte)Status.Actived;
                            logonGUID = l.LogonGUID.ToString();

                            var permissions = from p in db.Permissions
                                              where p.LogonGUID.ToString() == logonGUID
                                              select p;

                            db.Permissions.DeleteAllOnSubmit(permissions);
                            db.SubmitChanges();

                            desc += string.Format("- GUID: '{0}', Nhân viên: '{1}'", l.LogonGUID.ToString(), l.DocStaff.Contact.FullName);
                        }

                        //Permission
                        foreach (DataRow row in dtPermission.Rows)
                        {
                            Permission p = new Permission();
                            p.PermissionGUID = Guid.NewGuid();
                            p.LogonGUID = Guid.Parse(logonGUID);
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
                            p.CreatedDate = DateTime.Now;
                            p.CreatedBy = Guid.Parse(Global.UserGUID);
                            db.Permissions.InsertOnSubmit(p);
                        }
                        
                        //Tracking
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin người sử dụng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        Logon l = db.Logons.SingleOrDefault<Logon>(ll => ll.LogonGUID.ToString() == logon.LogonGUID.ToString());
                        if (l != null)
                        {
                            l.DocStaffGUID = logon.DocStaffGUID;
                            l.Password = logon.Password;
                            l.UpdatedDate = logon.UpdatedDate;
                            l.UpdatedBy = logon.UpdatedBy;
                            l.Status = logon.Status;

                            desc += string.Format("- GUID: '{0}', Nhân viên: '{1}'", l.LogonGUID.ToString(), l.DocStaff.Contact.FullName);

                            //Permission
                            foreach (DataRow row in dtPermission.Rows)
                            {
                                if (row["PermissionGUID"] != null && row["PermissionGUID"] != DBNull.Value)
                                {
                                    string permissionGUID = row["PermissionGUID"].ToString();
                                    Permission p = db.Permissions.SingleOrDefault<Permission>(pp => pp.PermissionGUID.ToString() == permissionGUID);
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
                                        p.UpdatedDate = DateTime.Now;
                                        p.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    }
                                }
                                else
                                {
                                    Permission p = new Permission();
                                    p.PermissionGUID = Guid.NewGuid();
                                    p.LogonGUID = logon.LogonGUID;
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
                                    p.CreatedDate = DateTime.Now;
                                    p.CreatedBy = Guid.Parse(Global.UserGUID);
                                    db.Permissions.InsertOnSubmit(p);
                                }
                            }

                            //Tracking
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin người sử dụng";
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

        public static Result ChangePassword(string password)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                RijndaelCrypto crypt = new RijndaelCrypto();
                password = crypt.Encrypt(password);
                db = new MMOverride();
                Logon logon = db.Logons.SingleOrDefault<Logon>(l => l.LogonGUID.ToString() == Global.LogonGUID);
                if (logon != null)
                {
                    logon.Password = password;
                    logon.UpdatedDate = DateTime.Now;
                    logon.UpdatedBy = Guid.Parse(Global.UserGUID);
                    db.SubmitChanges();
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
