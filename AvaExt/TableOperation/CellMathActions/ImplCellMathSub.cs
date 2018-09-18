using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.CellMathActions
{
    public class ImplCellMathSub : ICellMath
    {
        public void doMath(DataRow row, string col, object val1, object val2, object coif)
        {
            CellMath.sub(row, col, val1, val2, coif);
        }
    }
}


