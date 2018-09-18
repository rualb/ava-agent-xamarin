using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.Expression
{
    public interface IRowEvaluator
    {
        void addExpression(string exprName, string expr,Type type);
        void addExpression(string expr, Type type);
        void addVar(string name, Type type);
        void setVar(string name, object val);
        void setVar(DataRow row);
        object getResult(string exprName);
        object getResult();
        
    }
}
