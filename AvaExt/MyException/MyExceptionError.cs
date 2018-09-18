using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.MyException
{
    public class MyExceptionError : MyBaseException
    {
        public MyExceptionError(string msg)
            : base(msg)
        { }
        public MyExceptionError(string msg, Exception innerExc)
            : base(msg, innerExc)
        { }
        public MyExceptionError(string msg, object[] args)
            : base(msg, args)
        { }
        public MyExceptionError(string msg, object[] args, Exception innerExc)
            : base(msg, args, innerExc)
        { }
    }
}
