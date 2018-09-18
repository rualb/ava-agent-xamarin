using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.Const;
using AvaExt.Expression;
using AvaExt.MyException;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingBetweenTablesExpExt : RowColumnsBindingBase
    {

        DataTable tableDest;
        public const string colCurValue = "__CURRENT__VALUE__";
        public const string colPrevValue = "__PREV__VALUE__";
        public const string colAmangValue = "__AMANG__VALUE__";
        ImplRowEvaluator evaluatorRow;
        ImplRowEvaluator evaluatorAmangRow;
        ImplRowEvaluator evaluatorFinal;
        object defaultAmang;
        public RowColumnsBindingBetweenTablesExpExt(DataTable tableS, DataTable tableD, string[] colS, string colD, string expresionRow, Type typeRow, string expresionAmangRow, Type typeAngRow, string expresionFinal, Object prevDef, IRowValidator filter)
            : base(tableS, filter)
        {
            columns = colS;
            column = colD;
            tableDest = tableD;
            defaultAmang = prevDef;
            evaluatorRow = new ImplRowEvaluator();
            for (int i = 0; i < columns.Length; ++i)
                evaluatorRow.addVar(columns[i], tableSource.Columns[columns[i]].DataType);
            evaluatorRow.addExpression(expresionRow, typeRow);

            evaluatorAmangRow = new ImplRowEvaluator();
            evaluatorAmangRow.addVar(colPrevValue, typeAngRow);
            evaluatorAmangRow.addVar(colCurValue, typeAngRow);
            evaluatorAmangRow.addExpression(expresionAmangRow, typeAngRow);

            evaluatorFinal = new ImplRowEvaluator();
            evaluatorFinal.addVar(colAmangValue, typeAngRow);
            evaluatorFinal.addExpression(expresionFinal, tableD.Columns[column].DataType);

            tableSource.RowDeleted += new DataRowChangeEventHandler(table_RowDeleted);
            tableSource.RowChanged += new DataRowChangeEventHandler(table_RowChanged);
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChanged);
        }

        protected   void table_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            recalculate();
        }

        protected   void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {

            if (isMyCollumn(e.Column.ColumnName))
                recalculate();

        }
        protected   void table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
                recalculate();
        }

        void recalculate()
        {
            if (block())
            {
                try
                {

                    object result = DBNull.Value;
                    object curVal;
                    object prevAmngVal = defaultAmang;
                    for (int r = 0; r < tableSource.Rows.Count; ++r)
                    {
                        DataRow row = tableSource.Rows[r];
                        if ((row.RowState != DataRowState.Deleted) && validator.check(row))
                        {
                            //
                            evaluatorRow.setVar(row);
                            curVal = evaluatorRow.getResult();
                            //
                            evaluatorAmangRow.setVar(colPrevValue, prevAmngVal);
                            evaluatorAmangRow.setVar(colCurValue, curVal);
                            prevAmngVal = evaluatorAmangRow.getResult();
                            //
                        }
                    }
                    evaluatorFinal.setVar(colAmangValue, prevAmngVal);
                    ToolColumn.setColumnValue(tableDest, column, evaluatorFinal.getResult());

                }
                finally
                {
                    unblock();
                }
            }
        }
    }

}

