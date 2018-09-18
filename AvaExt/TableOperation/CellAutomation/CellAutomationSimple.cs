using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;
using AvaExt.MyException;
using AvaExt.Expression;
using AvaExt.TableOperation.CellAutomation.TableEvents;

namespace AvaExt.TableOperation.CellAutomation
{
    public class CellAutomationSimple : ITableColumnChange 
    {
        protected string[] columnsCalc;
        protected IEvaluator[] evals;
        protected string[] columnsVars;

        protected ITableTouchMemory toucher = new ImplTableTouchMemory();


        public CellAutomationSimple(string[] pColumns, IEvaluator[] pEval, string[] pVars)
        {
            columnsCalc = pColumns;
            evals = pEval;
            columnsVars = pVars;
        }


        public virtual void columnChange(DataColumnChangeEventArgs e)
        {
            string curColumn = e.Column.ColumnName;

            toucher.touchCell(e.Row, curColumn);

            string oldest;
            if (columnsCalc.Length > 1)
                oldest = toucher.getOldest(columnsCalc, e.Row);
            else
                oldest = columnsCalc[0];

            calcColumn(e.Row, oldest);
        }

        public virtual void initForColumnChanged(DataRow pRow)
        {
            for (int i = 0; i < columnsCalc.Length; ++i)
                if (ToolCell.isNull(pRow[columnsCalc[i]]))
                    calcColumn(pRow, columnsCalc[i]);
        }

        void calcColumn(DataRow pRow, string pCol)
        {
            IEvaluator eval = evals[Array.IndexOf<string>(columnsCalc, pCol)];
            object[] vals = ToolRow.copyRowToArr(columnsVars, pRow);
            eval.setVarAll(vals);
            ToolCell.set(pRow, pCol, eval.getResult());
        }
    }

}

