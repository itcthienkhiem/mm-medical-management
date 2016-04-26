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
using System.Data;
using System.Data.Linq;
using System.Text;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class UserBus : BusBase
    {
        public static Result GetAccountList(DateTime fromDate, DateTime toDate, string tenBenhNhan, bool isMaBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;

                //Hitachi917
                if (!isMaBenhNhan)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM AccountView WITH(NOLOCK) WHERE Archived = 'False' AND FullName LIKE N'%{0}%' AND CreatedDate BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM AccountView WITH(NOLOCK) WHERE Archived = 'False' AND CustomerId LIKE N'%{0}%' AND CreatedDate BETWEEN '{1}' AND '{2}'",
                    tenBenhNhan, fromDate.ToString("yyyy-MM-dd 00:00:00"), toDate.ToString("yyyy-MM-dd 23:59:59"));
                }

                result = ExcuteQuery(query);

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string maBenhNhan = row["CustomerId"].ToString();
                        string maCongTy = Utility.GetMaCongTy(maBenhNhan);
                        result = DiaChiCongTyBus.GetDiaChiCongTy(maCongTy);
                        if (!result.IsOK) return result;

                        row["Address"] = result.QueryResult.ToString();
                    }
                }

                result.QueryResult = dt;
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

        public static Result GetUser(string customerId)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                User user = db.Users.FirstOrDefault(u => u.CustomerId.ToLower() == customerId.ToLower());
                if (user == null)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                {
                    result.Error.Code = ErrorCode.EXIST;
                    result.QueryResult = user;
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

        public static Result AddUser(string customerId, string password)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    User user = db.Users.FirstOrDefault(u => u.CustomerId.ToLower() == customerId.ToLower());
                    if (user == null)
                    {
                        user = new User();
                        user.CustomerId = customerId;
                        user.Password = password;
                        user.CreatedDate = DateTime.Now;
                        db.Users.InsertOnSubmit(user);
                        db.SubmitChanges();
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
