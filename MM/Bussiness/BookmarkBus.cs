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
                string query = string.Format("SELECT * FROM Bookmark WHERE Type={0} ORDER BY [Value]", (int)type);
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
