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
    public class Result
    {
        #region Members
        private Error _error = null;
        private object _queryResult = null;
        #endregion

        #region Constructor
        public Result()
        {
            _error = new Error();
        }

        public Result(object result, Error error)
        {
            _queryResult = result;
            _error = error;
        }
        #endregion

        #region Properties
        public bool IsOK
        {
            get
            {
                if (_error.Code == ErrorCode.OK) return true;
                return false;
            }
        }

        public Error Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public object QueryResult
        {
            get { return _queryResult; }
            set { _queryResult = value; }
        }
        #endregion

        #region Methods
        public string GetErrorAsString(string methods)
        {
            return string.Format("({0} ErrorCode: {1} : {2})", methods, _error.Code.ToString(), _error.Description);
        }
        #endregion
    }
}
