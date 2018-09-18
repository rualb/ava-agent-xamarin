using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.MyException
{
    public class MyExceptionInner : MyBaseException
    {
        public MyExceptionInner(string msg)
            : base(msg)
        { }
        public MyExceptionInner(string msg, Exception innerExc)
            : base(msg, innerExc)
        { }
    }
}
