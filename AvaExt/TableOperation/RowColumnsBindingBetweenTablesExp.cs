using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.Const;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingBetweenTablesExp : RowColumnsBindingBase
    {
        ConstMathOperation oper;
        DataTable tableDest;
        public RowColumnsBindingBetweenTablesExp(DataTable tableS, DataTable tableD, string colS, string colD, IRowValidator pValidator)
            : base(tableS, new string[] { colS }, colD, colS, pValidator)
        {
            oper = ConstMathOperation.sum;
            tableDest = tableD;

            tableSource.RowDeleted += new DataRowChangeEventHandler(table_RowDeleted);
            tableSource.RowChanged += new DataRowChangeEventHandler(table_RowChanged);
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChanged);
        }
        public RowColumnsBindingBetweenTablesExp(DataTable tableS, DataTable tableD, string[] colS, string colD, string expresion, ConstMathOperation pOper, IRowValidator pValidator)
            : base(tableS, colS, colD, expresion, pValidator)
        {
            oper = pOper;
            tableDest = tableD;

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

            string curColumn = e.Column.ColumnName;
            if (isMyCollumn(curColumn))
                recalculate();

        }
        protected     void table_RowChanged(object sender, DataRowChangeEventArgs e)
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

                    double result = 0;
                    for (int r = 0; r < tableSource.Rows.Count; ++r)
                    {
                        DataRow row = tableSource.Rows[r];
                        if (row.RowState != DataRowState.Deleted && validator.check(row))
                        {
                            for (int i = 0; i < columns.Length; ++i)
                                evaluator.setVar(columns[i], row[columns[i]]);

                            if (oper == ConstMathOperation.sum)
                                result += (double)evaluator.getResult(column);


                        }
                    }
                    ToolColumn.setColumnValue(tableDest, column, result);

                }
 
                finally
                {
                    unblock();
                }
            }
        }
    }

}

