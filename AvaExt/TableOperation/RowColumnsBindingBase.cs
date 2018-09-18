using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.Expression;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public abstract class RowColumnsBindingBase : RowEventBindable
    {
        protected ICellMath forward;
        protected ICellMath backward;
        protected string column;
        protected string[] columns;
        protected IRowEvaluator evaluator;
        protected double padCoif;
        public RowColumnsBindingBase(DataTable table, IRowValidator pValidator)
            : base(table, pValidator)
        {
        }

        public RowColumnsBindingBase(DataTable table, string[] colArr, IRowValidator pValidator)
            : base(table, pValidator)
        {
            columns = colArr;
        }
        public RowColumnsBindingBase(DataTable table, string[] colArr, string col, IRowValidator pValidator)
            : base(table, pValidator)
        {
            columns = colArr;
            column = col;
        }
        public RowColumnsBindingBase(DataTable table, string[] colArr, double coif, ICellMath pForward, ICellMath pBackward, IRowValidator pValidator)
            : base(table, pValidator)
        {
            columns = colArr;
            padCoif = coif;
            backward = pBackward;
            forward = pForward;
        }

        public RowColumnsBindingBase(DataTable table, string[] colArr, string col, string pExpr, IRowValidator pValidator)
            : base(table, pValidator)
        {
            pExpr = string.Format(pExpr, colArr);
            columns = colArr;
            column = col;
            evaluator = new ImplRowEvaluator();
            for (int i = 0; i < columns.Length; ++i)
                evaluator.addVar(columns[i], table.Columns[columns[i]].DataType);
            evaluator.addExpression(column, pExpr, typeof(double));
        }

       
        protected virtual bool isMyCollumn(string col)
        {
            for (int i = 0; i < columns.Length; ++i)
                if (columns[i] == col)
                    return true;
            return false;
        }
 
        protected void table_ColumnChangedForRow(object sender, DataColumnChangeEventArgs e)
        {
  
            if (block())
            {
                try
                {
                    if (e.Row.RowState != DataRowState.Detached)
                    {
                        if (isMyCollumn(e.Column.ColumnName) && validator.check(e.Row))
                        {

                            activityForRow(e);

                        }
                    }
                }

                finally
                {
                    unblock();
                }
            }


        }

    }

}

