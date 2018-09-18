using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Expression
{
    public interface IClassEvaluator
    {
 
        object invoke(string name,object[] vars);
        object invoke(object[] vars);
    }
}
