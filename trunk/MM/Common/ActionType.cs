using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public enum ActionType : byte
    {
        Add = 0,
        Edit,
        Delete
    }

    public enum TrackingType : byte
    {
        None = 0,
        Price,
        Count
    }
}
