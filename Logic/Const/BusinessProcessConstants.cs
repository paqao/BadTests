using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Const
{
    public static class BusinessProcessConstants
    {
        public enum Status
        {
            New = 0,
            Approved = 1,
            Closed = 2,
            Rejected = 3,
            OutOfDate = 4
        }
    }
}
