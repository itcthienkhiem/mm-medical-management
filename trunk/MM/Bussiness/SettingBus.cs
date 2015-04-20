using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class SettingBus
    {
        public static Result GetValue(string key)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Setting settings = (from s in db.Settings
                                    where s.SettingKey == key
                                    select s).FirstOrDefault();

                result.QueryResult = settings;
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

        public static Result SetValue(string key, object val)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Setting settings = (from s in db.Settings
                                    where s.SettingKey == key
                                    select s).FirstOrDefault();

                if (settings == null)
                {
                    settings = new Setting();
                    settings.SettingKey = key;
                    settings.SettingValue = val.ToString();
                    db.Settings.InsertOnSubmit(settings);
                }
                else
                    settings.SettingValue = val.ToString();

                db.SubmitChanges();
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
