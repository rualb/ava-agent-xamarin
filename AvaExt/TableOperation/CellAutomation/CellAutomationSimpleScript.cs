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
    public class CellAutomationSimpleScript : CellAutomationSimple
    {
 
        protected Type[] varsTypes;
        protected string[] expressions;

        public CellAutomationSimpleScript(string[] pColumns, string[] pExpressions, string[] pVars, Type[] pTypes) 
            : base(pColumns, null, pVars)
        {
            varsTypes = pTypes;
            expressions = pExpressions;
            evals = new IEvaluator[pExpressions.Length];
 
        }

        public CellAutomationSimpleScript(string[] pColumns, string[] pExpressions, string[] pVars, DataTable pTable)
            : base(pColumns, null, pVars)
        {
            
            varsTypes = ToolColumn.getTypes(pTable, columnsVars);
            expressions = pExpressions;
            evals = new IEvaluator[pExpressions.Length];

        }

    }

}

