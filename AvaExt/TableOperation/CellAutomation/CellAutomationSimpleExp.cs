using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;
using AvaExt.MyException;
using AvaExt.Expression;

namespace AvaExt.TableOperation.CellAutomation
{
    public class CellAutomationSimpleExp : CellAutomationSimple
    {
 

        

        //public CellAutomationSimpleExp(string[] pColumns, string[] pExpressions, string[] pVars)  
        //    : base(pColumns, null, pVars)
        //{
        //    string[] expressions = pExpressions;
        //    evals = new IEvaluator[pExpressions.Length];
        //    for (int i = 0; i < expressions.Length; ++i)
        //        evals[i] = new ImplExpEvaluator( string.Format(expressions[i],columnsVars) , columnsVars);
        //}



        public CellAutomationSimpleExp(string[] pColumns, IEvaluator[] pExpressions, string[] pVars)
            : base(pColumns, pExpressions, pVars)
        {

        }
    }

}

