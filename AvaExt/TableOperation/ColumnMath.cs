using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Expression;

namespace AvaExt.TableOperation
{
    public class ColumnMath
    {
        public static double sum(DataTable table, string col)
        {
            double res = 0;
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                if (!table.Rows[i].IsNull(col))
                    res += (double)table.Rows[i][col];
            }
            return res;
        }
        public static double sum(DataRow[] rows, string col)
        {
            double res = 0;
            for (int i = 0; i < rows.Length; ++i)
            {
                if (!rows[i].IsNull(col))
                    res += (double)rows[i][col];
            }
            return res;
        }
        public static double sumExp(DataRow[] rows, string exp)
        {
            double res = 0;
            if ((rows != null) && (rows.Length > 0))
            {
                ImplRowEvaluator eval = new ImplRowEvaluator(rows[0].Table);
                eval.addExpression(exp, typeof(double));
                for (int i = 0; i < rows.Length; ++i)
                {
                    eval.setVar(rows[i]);
                    res += (double)ToolCell.isNull(eval.getResult(), 0.0);
                }
            }
            return res;
        }
    }
}
