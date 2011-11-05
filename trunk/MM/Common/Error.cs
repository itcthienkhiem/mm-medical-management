using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class Error
    {
        #region Members
        private ErrorCode _code;
        private string _description;
        #endregion

        #region Constructor
        public Error() : this(ErrorCode.OK, string.Empty)
        {
        }

        public Error(ErrorCode code, string description)
        {
            this._code = code;
            this._description = description;
        }
        #endregion

        #region Properties
        public ErrorCode Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion
    }
}
