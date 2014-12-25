using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MM.Common;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Transactions;

namespace SonoOnlineResult
{
    public class MySQL
    {
        public static Result CheckUserExist(string email)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT * FROM Users WHERE Email = '{0}'", email);
                result = MySQLHelper.ExecuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result AddUser(string email, string password, string code, List<ResultFileInfo> resultFileInfos)
        {
            Result result = new Result();
            MySqlCommand cmd = null;
            MySqlConnection cnn = null;

            try
            {
                //Check User Exist
                result = CheckUserExist(email);
                if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                    return result;

                //Add User
                string query = string.Empty;
                if (result.Error.Code == ErrorCode.NOT_EXIST)
                    query = string.Format("INSERT INTO Users(Email, Password) VALUES('{0}', '{1}')", email, password);
                else
                    query = string.Format("UPDATE Users SET Password = '{0}' WHERE Email = '{1}'", password, email);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    result.Error.Code = ErrorCode.OK;
                    cnn = MySQLHelper.CreateConnection();
                    cmd = cnn.CreateCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                    //Clear Last Upload
                    query = string.Format("DELETE a1, a2 FROM LastUpload AS a1 INNER JOIN LastUploadDetail AS a2 WHERE a1.LastUploadKey=a2.LastUploadKey AND a1.Email='{0}'",
                        email);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                    //Insert Last Upload
                    query = string.Format("INSERT INTO LastUpload(Email, Code) VALUES('{0}', '{1}')", email, code);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                    //Get Last Upload Key
                    query = "SELECT LAST_INSERT_ID()";
                    cmd.CommandText = query;
                    object obj = cmd.ExecuteScalar();
                    int lastUploadKey = Convert.ToInt32(obj);

                    //Insert Last Upload Detail
                    foreach (var info in resultFileInfos)
                    {
                        query = string.Format("INSERT INTO LastUploadDetail(LastUploadKey, FileName) VALUES({0}, '{1}')",
                            lastUploadKey, Path.GetFileName(info.FileName));

                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

                if (cnn != null)
                {
                    cnn.Close();
                    cnn = null;
                }
            }

            return result;
        }

        public static Result GenerateCode(string email)
        {
            Result result = new Result();

            try
            {
                string code = Guid.NewGuid().ToString();

                //Delete Last Upload
                string query = string.Format("DELETE FROM ");

                //result = CheckUserExist(email);
                //if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                //    return result;

                //string query = string.Empty;
                //if (result.Error.Code == ErrorCode.NOT_EXIST)
                //{
                //    query = string.Format("INSERT INTO Users(Email, Password) VALUES('{0}', '{1}')",
                //    email, password);
                //}
                //else
                //{
                //    query = string.Format("UPDATE Users SET Password = '{0}' WHERE Email = '{1}'",
                //        password, email);
                //}

                //result = MySQLHelper.ExecuteNoneQuery(query);
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result GetBranchList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT * FROM Branch ORDER BY BranchName");
                result = MySQLHelper.ExecuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null)
                    {
                        DataColumn col = new DataColumn("Check", typeof(bool));
                        col.DefaultValue = false;
                        dt.Columns.Add(col);
                    }
                }
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result CheckBranchExist(string branchName, int branchKey)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT * FROM Branch WHERE BranchName = '{0}' AND (BranchKey <> {1} OR BranchKey = 0)", 
                    branchName, branchKey);
                result = MySQLHelper.ExecuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result InsertBranch(int branchKey, string branchName, string address, string telephone, string fax, string website, string note)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (branchKey <= 0) //Insert
                {
                    query = string.Format("INSERT INTO Branch(BranchName, Address, Telephone, Fax, Website, Note) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                        branchName, address, telephone, fax, website, note);
                    result = MySQLHelper.ExecuteNoneQueryWithGetLastKey(query);
                }
                else //Update
                {
                    query = string.Format("UPDATE Branch SET BranchName = '{0}', Address = '{1}', Telephone = '{2}', Fax = '{3}', Website = '{4}', Note = '{5}' WHERE BranchKey = {6}", 
                        branchName, address, telephone, fax, website, note, branchKey);
                    result = MySQLHelper.ExecuteNoneQuery(query);
                }
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result DeleteBranch(int branchKey)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("DELETE FROM Branch WHERE BranchKey = {0}", branchKey);
                result = MySQLHelper.ExecuteNoneQuery(query);
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }
    }
}
