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
    public class TrackingBus : BusBase
    {
        public static Result GetTrackingList(DateTime fromDate, DateTime toDate, string docStaffGUID, bool isAdd, bool isEdit, bool isDelete)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                string actionTypes = string.Empty;

                if (isAdd) actionTypes += "0,";
                if (isEdit) actionTypes += "1,";
                if (isDelete) actionTypes += "2,";
                actionTypes = actionTypes.Substring(0, actionTypes.Length - 1);

                if (docStaffGUID == Guid.Empty.ToString())
                {
                    query = string.Format("SELECT * FROM TrackingView WITH(NOLOCK) WHERE TrackingDate BETWEEN '{0}' AND '{1}' AND ActionType IN ({2}) ORDER BY TrackingDate DESC",
                        fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), actionTypes);
                }
                else
                {
                    query = string.Format("SELECT * FROM TrackingView WITH(NOLOCK) WHERE TrackingDate BETWEEN '{0}' AND '{1}' AND ActionType IN ({2}) AND DocStaffGUID = '{3}' ORDER BY TrackingDate DESC",
                        fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), actionTypes, docStaffGUID);
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
    }
}
