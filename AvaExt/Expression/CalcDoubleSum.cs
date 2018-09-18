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
    public class CalcDoubleSum : CalcDouble
    {


        public CalcDoubleSum(int pIndxX, int pIndxY)
            : base(pIndxX, pIndxY)
        {
          
        }
        public override object getResult()
        {
            double x_ = Convert.ToDouble(values[indxX]);
            double y_ = Convert.ToDouble(values[indxY]);
 
            return x_ + y_;
        }


    }
}
