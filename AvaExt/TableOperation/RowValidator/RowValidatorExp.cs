using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Expression;

namespace AvaExt.TableOperation.RowValidator
{
    public class RowValidatorExp : IRowValidator 
    {
        ImplRowEvaluator eval;
        string exp;
        public RowValidatorExp(DataTable pTable, string pExp)
        {
            exp = pExp;
            if (pTable != null)
            {
                eval = new ImplRowEvaluator(pTable);
                eval.addExpression(exp, typeof(bool));
            }
        }
        public bool check(DataRow row)
        {
            if (eval == null)
            {
                eval = new ImplRowEvaluator(row.Table);
                eval.addExpression(exp, typeof(bool));
            }
            eval.setVar(row);
            return (bool)ToolCell.isNull(eval.getResult(), false);
        }


    }
}
