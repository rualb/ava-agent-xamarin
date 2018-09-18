using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class RowDefaulter : RowEventBindable
    { 
        string[] cols;
        object[] values;
        public RowDefaulter(DataTable table, string[] pCols, object[] pValues, IRowValidator pValidator)
            : base(table,  pValidator)
        {
             cols = pCols; 
             values = pValues;
            tableSource.RowChanged += new DataRowChangeEventHandler(table_RowChanged);
        }

        protected void table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action != DataRowAction.Add)
                return;
            if (validator.check(e.Row))
            {
                if (block())
                {
                    try
                    {
                        for (int i = 0; i < cols.Length; ++i)
                            ToolCell.set(e.Row, cols[i], values[i]);
                    }
 
                    finally
                    {
                        unblock();
                    }
                }
            }
        } 

       

    }

}

