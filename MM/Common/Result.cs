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
    }
}
