using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFind.Constants
{
    public enum StatusCodeType
    {
        SUCCESS = 1000,
        HAS_EXCEPTION = 1001,
        ALREADY_HASEMAIL=1002,
        USER_NOTFOUND=1003,
        ALREADY_HASCV = 1004,
        FIRM_NOTFOUND = 1005

    }
}
