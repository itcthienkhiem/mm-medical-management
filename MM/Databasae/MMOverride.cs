using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MM.Common;

namespace MM.Databasae
{
    public class MMOverride : MMDataContext
    {
        #region Constructor
        public MMOverride() : base(Global.ConnectionInfo.ConnectionString)
        {
            this.CommandTimeout = 0;
        }
        #endregion
    }
}
