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
    public class BookmarkBus : BusBase
    {
        public static Result GetBookmark(BookMarkType type)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM Bookmark WITH(NOLOCK) WHERE Type={0} ORDER BY [Value]", (int)type);
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

        public static Result InsertBookmark(Bookmark bookmark)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Bookmark bm = db.Bookmarks.SingleOrDefault(b => b.Type == bookmark.Type &&
                    b.Value.Trim().ToUpper() == bookmark.Value.Trim().ToUpper());

                if (bm == null)
                {
                    bookmark.BookmarkGUID = Guid.NewGuid();
                    db.Bookmarks.InsertOnSubmit(bookmark);
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

        public static Result InsertBookmark(List<Bookmark> bookmarkList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                foreach (Bookmark bookmark in bookmarkList)
                {
                    Bookmark bm = db.Bookmarks.SingleOrDefault(b => b.Type == bookmark.Type &&
                        b.Value.Trim().ToUpper() == bookmark.Value.Trim().ToUpper());

                    if (bm == null)
                    {
                        bookmark.BookmarkGUID = Guid.NewGuid();
                        db.Bookmarks.InsertOnSubmit(bookmark);
                        db.SubmitChanges();
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
