using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation
{
    public class CellMath
    {


        public static void mult(DataRow row, string col, object val1, object val2, object coif)
        {
            if (!(ToolCell.isNull(val1) || ToolCell.isNull(val2) || ToolCell.isNull(coif)))
                ToolCell.set(row, col, (double)val1 * (double)val2 * (double)coif);
        }
        public static void mult(DataRow row, string col, object val1, object val2)
        {
            mult(row, col, val1, val2, 1);
        }
        public static void div(DataRow row, string col, object val1, object val2, object coif)
        {
            if (!(ToolCell.isNull(val1) || ToolCell.isNull(val2) || ToolCell.isNull(coif)))
                if (Math.Abs((double)val2 * (double)coif) > (double)Common.Const.ConstValues.minPositive)
                    ToolCell.set(row, col, (double)val1 / ((double)val2 * (double)coif));
                else
                    ToolCell.set(row, col, 0.0);
        }
        public static void div(DataRow row, string col, object val1, object val2)
        {
            div(row, col, val1, val2, 1);
        }
        public static void sum(DataRow row, string col, object val1, object val2, object coif)
        {
            if (!(ToolCell.isNull(val1) || ToolCell.isNull(val2) || ToolCell.isNull(coif)))
                ToolCell.set(row, col, (double)val1 + (double)val2 + (double)coif);
        }
        public static void sum(DataRow row, string col, object val1, object val2)
        {
            sum(row, col, val1, val2, 0.0);
        }
        public static void sub(DataRow row, string col, object val1, object val2, object coif)
        {
            if (!(ToolCell.isNull(val1) || ToolCell.isNull(val2) || ToolCell.isNull(coif)))
                ToolCell.set(row, col, (double)val1 - (double)val2 - (double)coif);
        }
        public static void sub(DataRow row, string col, object val1, object val2)
        {
            sub(row, col, val1, val2, 0.0);
        }
    }
}
