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
    public class CalcDoubleDiv : CalcDouble
    {

        bool zeroYto1 = false; 
        public CalcDoubleDiv(int pIndxX, int pIndxY)
            : this(pIndxX, pIndxY, false)
        {

        }
        public CalcDoubleDiv(int pIndxX, int pIndxY, bool pZeroYto1)
            : base(pIndxX, pIndxY)
        {
            zeroYto1 = pZeroYto1;
        }
        public override object getResult()
        {
            double x_ = Convert.ToDouble(values[indxX]);
            double y_ = Convert.ToDouble(values[indxY]);

            y_ = (zeroYto1 && ToolDouble.isZero(y_)) ? 1 : y_; //if FlagY enabled Y will be set to 1

            return ToolDouble.isZero(y_) ? 0 : x_ / y_;
        }


    }
}
