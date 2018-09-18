using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.Expression;
using AvaExt.MyException;
using AvaExt.TableOperation.RowValidator;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.TableOperation.CellAutomation.TableEvents;
using AvaExt.Common;

namespace AvaExt.TableOperation.CellAutomation
{
    public class CellAutomationSpace : ITableColumnChange
    {

        protected IEvaluator evalOnTopAny;
        protected IEvaluator evalOnTopAnyResult;
        protected IEvaluator evalOnSubCorrect;
        protected string[] columnsCalc;
        protected IEvaluator[] evalOnSubCalc;
        protected string[] columnsVars;
        protected object topOperDef;

        IRowsSelector selectorTop;
        IRowsSelector selectorBot;

        protected ITableTouchMemory toucher = new ImplTableTouchMemory();

        public CellAutomationSpace(
            string[] pColumnsVars,
            object pTopOperDef,
            IEvaluator pTopAny,
            IEvaluator pTopTotal,
            IEvaluator pSubCorrect,
            string[] pSubCols,
            IEvaluator[] pSubAny,
            IRowsSelector pSelectorTop,
            IRowsSelector pSelectorBot)
        {
            //
            topOperDef = pTopOperDef;

            evalOnTopAny = pTopAny;
            evalOnTopAnyResult = pTopTotal;
            evalOnSubCorrect = pSubCorrect;

            columnsCalc = pSubCols;
            evalOnSubCalc = pSubAny;

            selectorTop = pSelectorTop;
            selectorBot = pSelectorBot;

            columnsVars = pColumnsVars;
        }



        public virtual void columnChange(DataColumnChangeEventArgs e)
        {
            toucher.touchCell(e.Row, e.Column.ColumnName);
            table_DistributeValues(selectorTop.get(e.Row), selectorBot.get(e.Row));
        }

        protected void table_DistributeValues(DataRow[] topRows, DataRow[] subRows)
        {
            object topTotal = topOperDef;
            for (int i = 0; i < topRows.Length; ++i)
            {
                evalOnTopAny.setVarAll(ToolRow.copyRowToArr(columnsVars, topRows[i]));
                object tmpRes = evalOnTopAny.getResult();
                evalOnTopAnyResult.setVarAll(new object[] { topTotal, tmpRes });
                topTotal = evalOnTopAnyResult.getResult();
            }
            for (int i = 0; i < subRows.Length; ++i)
            {
                object[] vals;
                object tmp;
                DataRow row = subRows[i];
                string oldest = toucher.getOldest(columnsCalc, row);
                IEvaluator eval = evalOnSubCalc[Array.IndexOf<string>(columnsCalc, oldest)];
                vals = ToolRow.copyRowToArr(columnsVars, row);
                vals =ToolArray.resize<object>(vals, vals.Length + 1);
               
                vals[vals.Length - 1] = topTotal;
                eval.setVarAll(vals);
                tmp = eval.getResult();
                ToolCell.set(row, oldest, tmp);

                vals = ToolRow.copyRowToArr(columnsVars, row);
                vals = ToolArray.resize<object>(vals, vals.Length + 1);
                
                vals[vals.Length - 1] = topTotal;
                evalOnSubCorrect.setVarAll(vals);
                topTotal = evalOnSubCorrect.getResult();
            }
        }

        public virtual void initForColumnChanged(DataRow pRow)
        {

        }
    }

}

