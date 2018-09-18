using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.MyLog 
{
    public interface IHandlerLog
    {
        void error(string text);
        void set(string text);
        void set(string text,object[] data);
       
    }
}
