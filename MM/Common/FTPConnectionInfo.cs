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
