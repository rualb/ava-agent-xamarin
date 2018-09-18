using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;
using System.CodeDom.Compiler;

using System.Reflection;
using AvaExt.Translating.Tools;

namespace AvaExt.Expression
{
    public class CalcDouble : IEvaluator
    {


        protected object[] values;
        protected int indxX;
        protected int indxY;

        public CalcDouble(int pIndxX, int pIndxY)
        {

            indxX = pIndxX;
            indxY = pIndxY;
        }

        public virtual object getResult()
        {
            throw new NotImplementedException();
        }

        public void setVar(string pName, object pValue)
        {
            throw new NotImplementedException();
        }
        public void setVar(string[] pNames, object[] pValues)
        {
            throw new NotImplementedException();
        }
        public void setVar(object pValue)
        {

            throw new NotImplementedException();

        }
        public void setVarAll(object[] pValues)
        {
            values = new object[pValues.Length];
            for (int i = 0; i < pValues.Length; ++i)
                values[i] = pValues[i];

        }
    }
}
