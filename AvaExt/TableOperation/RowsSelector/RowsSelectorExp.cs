using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Expression;
using AvaExt.TableOperation.Const;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation.RowsSelector
{
    public class RowsSelectorExp : IRowsSelector
    {
        protected ImplRowEvaluator evalU;
        protected ImplRowEvaluator evalB;
        protected string expU;
        protected string expB;
        protected string expName = "___LOCAL___VALUE___";
        protected IRowValidator validator;

        public RowsSelectorExp(string pExpU, string pExpB, DataTable pTable, IRowValidator pValidator)
        {
            evalU = new ImplRowEvaluator(pTable);
            evalU.addExpression(expName, expU = pExpU, typeof(string));
            evalB = new ImplRowEvaluator(pTable);
            evalB.addExpression(expName, expB = pExpB, typeof(string));
            validator = pValidator;
        }

        public DataRow[] get(DataRow row)
        {
            List<DataRow> rows = new List<DataRow>();
            DataTable table = row.Table;
            string state;

            for (int i = table.Rows.IndexOf(row); i >= 0; --i)
            {
                DataRow locRow = table.Rows[i];
                if (!ToolRow.isDeleted(locRow))
                    if (validator.check(locRow))
                    {
                        evalU.setVar(locRow);
                        state = (string)evalU.getResult(expName);
                        if (state.IndexOf(ConstSelectAction.yes) >= 0)
                            rows.Insert(0, locRow);
                        if (state.IndexOf(ConstSelectAction.stop)>=0)
                            break;
                    }
            }
            for (int i = table.Rows.IndexOf(row) + 1; i < table.Rows.Count; ++i)
            {
                DataRow locRow = table.Rows[i];
                if ((!ToolRow.isDeleted(locRow)))
                    if (validator.check(locRow))
                    {
                        evalB.setVar(locRow);
                        state = (string)evalB.getResult(expName);
                        if (state.IndexOf(ConstSelectAction.yes) >=0 )
                            rows.Add(locRow);
                        if (state.IndexOf(ConstSelectAction.stop) >= 0)
                            break;
                    }
            }
            return rows.ToArray();
        }
 
 
    }
}
