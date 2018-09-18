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
    public class CalcDoubleMult : CalcDouble
    {
        double _coif = 1.0;
        bool zeroYto1 = false;
        public CalcDoubleMult(int pIndxX, int pIndxY)
            : this(pIndxX, pIndxY, false)
        {

        }
        public CalcDoubleMult(int pIndxX, int pIndxY, double pCoif)
            : this(pIndxX, pIndxY)
        {
            _coif = pCoif;
        }
        public CalcDoubleMult(int pIndxX, int pIndxY, bool pZeroYto1)
            : base(pIndxX, pIndxY)
        {
            zeroYto1 = pZeroYto1;
        }
        public CalcDoubleMult(int pIndxX, int pIndxY, double pCoif, bool pZeroYto1)
            : this(pIndxX, pIndxY, pZeroYto1)
        {
            _coif = pCoif;
        }

        public override object getResult()
        {
            double x_ = Convert.ToDouble(values[indxX]);
            double y_ = Convert.ToDouble(values[indxY]);

            y_ = (zeroYto1 && ToolDouble.isZero(y_)) ? 1 : y_;

            return x_ * y_ * _coif;
        }


    }
}
