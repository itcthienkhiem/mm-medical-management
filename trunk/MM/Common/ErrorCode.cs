using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public enum ErrorCode : int
    {
        OK = 0,
        NO_DATA,
        INVALID_SQL_STATEMENT,
        NO_AUTHORIZED,
        NO_DATA_TO_ANALYZE,
        SQL_QUERY_TIMEOUT,
        UNKNOWN_ERROR,
        INVALID_CONNECTION_INFO,
        INVALID_SERVERNAME,
        INVALID_DATABASENAME,
        INVALID_USERNAME,
        INVALID_PASSWORD,
        DATA_INCONSISTENT,
        LOCK,
        DELETED,
        NO_UPDATED,
        CANCEL_UPDATED
    };
}
