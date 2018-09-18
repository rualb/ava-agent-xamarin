using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.MyException
{
    public class MyExceptionUndefined : MyBaseException
    {
        public MyExceptionUndefined(string msg)
            : base(msg)
        { }
        public MyExceptionUndefined(string msg, Exception innerExc)
            : base(msg, innerExc)
        { }
    }
}
