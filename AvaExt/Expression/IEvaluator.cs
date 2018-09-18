using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Expression
{
    public interface IEvaluator
    {
        void setVar(string name, object val);
        void setVar(string[] name, object[] val);
        void setVar(object val);
        void setVarAll(object[] val); 
        object getResult();
 
    }
}
