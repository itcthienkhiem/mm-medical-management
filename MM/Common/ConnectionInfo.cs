using System;
using System.Data;
using System.Data.OleDb;
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
			OleDbConnection con = null;
			try
			{
				con = new OleDbConnection(ConnectionStringOLEDB);				
				con.Open();
				return true;
			}
			catch
			{
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
