using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.MyException
{
    public class MyExceptionProcessStoped : MyBaseException
    {
        public MyExceptionProcessStoped(string msg)
            : base(msg)
        { }
    }
}
