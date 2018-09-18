using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;
using System.CodeDom.Compiler;
 
using System.Reflection;
using AvaExt.Translating.Tools;
using AvaExt.Common;

namespace AvaExt.Expression
{
    public class CalcDoubleCoif : CalcDouble
    {

        double coif = 1;
        public CalcDoubleCoif(int pIndx, double pCoif)
            : base(pIndx, 0)
        {
            coif = pCoif;
        }
        public CalcDoubleCoif(int pIndx)
            : this(pIndx, 1)
        {

        }
        public override object getResult()
        {
            double x_ = Convert.ToDouble(values[indxX]);

            return x_ * coif;
        }


    }
}
