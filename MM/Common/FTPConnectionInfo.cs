using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class FTPConnectionInfo
    {
        #region Members
        private string _serverName = "healthcare.com.vn";
        private string _username = "healthcare";
        private string _password = "dsfsd@$@#Rsdf";
        #endregion

        #region Constructor
        public FTPConnectionInfo()
        {

        }
        #endregion

        #region Properties
        public string ServerName
        {
            get { return _serverName; }
            set { _serverName = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        #endregion

        #region Public Methods
        public Result TestConnection()
        {
            return FTP.Connect(_serverName, _username, _password);
        }
        #endregion
    }
}
