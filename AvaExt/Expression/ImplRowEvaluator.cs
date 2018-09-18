using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Translating.Tools;

namespace AvaExt.Expression
{
    public class ImplRowEvaluator : IRowEvaluator
    {
        DataTable evalContext = null;
        DataRow evalData = null;
        string unnamedExp = "_____COLUMN" + DateTime.Now.Ticks.ToString();
        string[] cols = null;
        public ImplRowEvaluator()
        {
            init(null, null);
        }
        public ImplRowEvaluator(DataTable context)
        {
            init(context, null);
        }
        public ImplRowEvaluator(DataTable context, string[] vars)
        {
            init(context, vars);
        }
        void init(DataTable context, string[] vars)
        {

            evalContext = (context == null ? new DataTable() : context.Clone());
            if (context != null && vars != null)
            {
                evalContext = new DataTable();
                foreach (string col in vars)
                    evalContext.Columns.Add(col, context.Columns[col].DataType);
            }
            evalContext.Rows.Add(evalData = evalContext.NewRow());
            if (vars != null && vars.Length > 0)
                cols = vars;
        }
        public void addExpression(string exprName, string expr, Type type)
        {
            try
            {
                evalContext.Columns.Add(exprName, type, expr);
            }
            catch (Exception exc)
            {
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
            }
        }
        public void addExpression(string expr, Type type)
        {
            evalContext.Columns.Add(unnamedExp, type, expr);

        }

        public void addVar(string name, Type type)
        {
            try
            {
                evalContext.Columns.Add(name, type);
            }
            catch (Exception exc)
            {
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
            }
        }
        public void setVar(string name, object val)
        {
            try
            {
                ToolCell.set(evalData, name, ToolCell.isNull(val, DBNull.Value));
            }
            catch (Exception exc)
            {
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
            }
        }
        public void setVar(DataRow row)
        {
            try
            {
                if (cols == null)
                {
                    for (int i = 0; i < row.Table.Columns.Count; ++i)
                        setVar(row.Table.Columns[i].ColumnName, row[i]);
                }
                else
                {
                    for (int i = 0; i < cols.Length; ++i)
                        setVar(cols[i], row[cols[i]]);
                }
            }
            catch (Exception exc)
            {
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
            }
        }

        public object getResult(string exprName)
        {
            try
            {
                return evalData[exprName];
            }
            catch (Exception exc)
            {
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_DYNAMIC_EXPRESSION, exc);
            }
        }
        public object getResult()
        {
            return getResult(unnamedExp);
        }



        public static object eval(DataRow row, string exp, Type resType)
        {

            ImplRowEvaluator e = new ImplRowEvaluator(row.Table);
            e.addExpression(exp, resType);
            e.setVar(row);
            return e.getResult();

        }
    }
}
