using System;
using System.Collections.Generic;
using System.Text;

namespace AvaAgent.Common
{
    public enum ConstProtResult : int
    {
        undef = 0,
        no = 1,
        ok = 2,
        noData = 3,
        innerErrorClient = 4,
        innerErrorSerrver = 5,
        loginError = 6,
        dataIsLong = 7
    }
}
