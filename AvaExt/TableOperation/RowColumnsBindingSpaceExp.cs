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

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingSpaceExp : RowColumnsBindingBase
    {

        public const string _TOP_VALUE_ = "_____TOP__VALUE__";
        IRowsSelector selectorTop;
        IRowsSelector selectorBot;
        string topExp;
        string subCoorectExp;
        string nameTopExp = "_____EXP__T";
        string nameSubCoorectExp = "_____EXP__S";
        string[] subCols;
        string[] subExp;

        string getName(string id)
        {
            return "_____EXP__COL__" + id;
        }

        public RowColumnsBindingSpaceExp(DataTable table, IRowEvaluator pEvaluator, string[] trigerCols, string pTopExp, string pSubCorrectExp, string[] pSubCols, string[] pSubExp, IRowsSelector pSelectorTop, IRowsSelector pSelectorBot, IRowValidator pValidator)
            : base(table, pValidator)
        {
            //
            columns = trigerCols;

            selectorTop = pSelectorTop;
            selectorBot = pSelectorBot;
            subCols = pSubCols;
            subExp = pSubExp;

            topExp = string.Format(pTopExp, _TOP_VALUE_);
            subCoorectExp = string.Format(pSubCorrectExp, _TOP_VALUE_);
            for (int i = 0; i < subExp.Length; ++i)
                subExp[i] = string.Format(subExp[i], _TOP_VALUE_);

            //

            //
            evaluator = pEvaluator;
            evaluator.addVar(_TOP_VALUE_, typeof(double));
            evaluator.addExpression(nameTopExp, topExp, typeof(double));
            evaluator.addExpression(nameSubCoorectExp, subCoorectExp, typeof(double));
            for (int i = 0; i < pSubExp.Length; ++i)
                evaluator.addExpression(getName(subCols[i]), subExp[i], typeof(double));
            //

            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChangedForRow);
        }

  
        public override void activityForRow(DataColumnChangeEventArgs e)
        {
            touchCell(e.Row, e.Column.ColumnName);
            table_DistributeValues(selectorTop.get(e.Row), selectorBot.get(e.Row));
        }

        protected void table_DistributeValues(DataRow[] topRows, DataRow[] subRows)
        {
            double topSum = 0;
            for (int i = 0; i < topRows.Length; ++i)
            {
                evaluator.setVar(topRows[i]);
                topSum += (double)evaluator.getResult(nameTopExp);
            }
            for (int i = 0; i < subRows.Length; ++i)
            {
                DataRow curSubRow = subRows[i];
                Stack<string> stack = toucher.sort(subCols, curSubRow);
                string column;
                if (stack.Count > 0)
                    column = stack.ToArray()[stack.Count - 1];
                else
                    column = subCols[subCols.Length - 1];

                for (int s = 0; s < subCols.Length; ++s)
                    if (subCols[s] == column)
                    {
                        object tmpRes;
                        //
                        evaluator.setVar(_TOP_VALUE_, topSum);
                        evaluator.setVar(curSubRow);
                        tmpRes = evaluator.getResult(getName(subCols[s]));
                        ToolCell.set(curSubRow, column, tmpRes);
                        //
                        topSum = (double)evaluator.getResult(nameSubCoorectExp);
                        // topSum = (double)ToolCell.isNull(tmpRes, 0.0);
                        //
                    }

            }
        }


    }

}

