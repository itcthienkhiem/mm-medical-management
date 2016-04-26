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
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace MM.Common
{
	public class ConnectionInfo
	{
		#region Members
		private string _serverName = "(local)";
        private string _databaseName = "MM";
		private string _userName = "sa";
		private string _password = "12345678";
        private string _authentication = "SQL Server Authentication";
		#endregion

		#region Constructor
		public ConnectionInfo()
		{

		}

		public ConnectionInfo(string serverName, string databaseName, string userName, string password)
			: this(serverName, databaseName, userName, password, "SQL Server Authentication")
		{
		}

		public ConnectionInfo(string serverName, string databaseName, string userName, string password, string authentication)
		{
			_serverName = serverName;
			_databaseName = databaseName;
			_userName = userName;
			_password = password;
			_authentication = authentication;
		}
		#endregion

		#region Properties
		public string ServerName
		{
			get { return _serverName; }
			set { _serverName = value; }
		}

		public string DatabaseName
		{
			get { return _databaseName; }
			set { _databaseName = value; }
		}

		public string UserName
		{
			get { return _userName; }
			set { _userName = value; }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		public string Authentication
		{
			get { return _authentication; }
			set { _authentication = value; }
		}

        public string ConnectionStringOLEDB
        {
            get
            {
                if (Authentication == "SQL Server Authentication")
                    return string.Format("Provider=SQLOLEDB;Data Source={0};Initial Catalog={1};User Id={2};Password={3};", ServerName, DatabaseName, UserName, Password);
                else
                    return string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1}; Integrated Security=SSPI", DatabaseName, ServerName);
            }
        }

        public string ConnectionString
        {
            get
            {
                if (Authentication == "SQL Server Authentication")
                    return string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", ServerName, DatabaseName, UserName, Password);
                else
                    return string.Format("Initial Catalog={0};Data Source={1}; Integrated Security=SSPI", DatabaseName, ServerName);
            }
        }
		#endregion

		#region Public Methods
		public bool TestConnection()
		{
			SqlConnection con = null;
			try
			{
                con = new SqlConnection(ConnectionString);				
				con.Open();
				return true;
			}
			catch (Exception ex)
			{
                Utility.WriteToTraceLog(ex.Message);
				return false;
			}
			finally
			{
				if(con != null)
				{
					if(con.State == ConnectionState.Open)
						con.Close();

					con.Dispose();
					con = null;
				}
			}
		}
		#endregion
	}
}
