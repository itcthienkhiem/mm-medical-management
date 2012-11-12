﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace MM.Common
{
    public class MySQLHelper
    {
        private static string _connectionString = "server=healthcare.com.vn;username=healthcare_user;password=dsfsd@$@#Rsdf;database=healthcare_report;persist security info=False";

        public static Result GetAllUsers()
        {
            Result result = new Result();

            try
            {
                string query = "select * from users";
                result = ExecuteQuery(query);
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result CheckUserExist(string customerId)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("select * from users where customer_id = '{0}'", customerId);
                result = ExecuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                {
                    result.Error.Code = ErrorCode.EXIST;
                    result.QueryResult = dt.Rows[0]["password"].ToString();
                }
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static Result InsertUser(string customerId, string password, string name)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("INSERT IGNORE INTO users SET customer_id = '{0}', password = '{1}', name = N'{2}'",
                    customerId, password, name);

                result = ExecuteNoneQuery(query);
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }

            return result;
        }

        public static MySqlConnection  CreateConnection()
        {
            try
            {
                MySqlConnection cnn = new MySqlConnection(_connectionString);
                cnn.Open();
                return cnn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Result ExecuteQuery(string query)
        {
            Result result = new Result();
            MySqlConnection cnn = null;
            MySqlDataAdapter adaper = null;
            
            try
            {
                cnn = CreateConnection();
                adaper = new MySqlDataAdapter(query, cnn);
                DataTable dt = new DataTable();
                adaper.Fill(dt);
                result.QueryResult = dt;
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.Message;
            }
            finally
            {
                if (adaper != null)
                {
                    adaper.Dispose();
                    adaper = null;
                }

                if (cnn != null)
                {
                    cnn.Clone();
                    cnn = null;
                }
            }

            return result;
        }

        public static Result ExecuteNoneQuery(string query)
        {
            Result result = new Result();
            MySqlConnection cnn = null;
            MySqlCommand cmd = null;

            try
            {
                cnn = CreateConnection();
                cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
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
                    cnn.Clone();
                    cnn = null;
                }
            }

            return result;
        }
    }
}