using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.Expression;
using AvaExt.Common;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class RowDeleteWatcher : BlockHandler
    {
        IRowValidator validator;
        string[] columns;
        object[] values;
        public RowDeleteWatcher(DataTable table, IRowValidator pValidator, string[] pCol, object[] pVal)
        {
            //

            columns = pCol;
            values = pVal;
            validator = (pValidator == null ? new RowValidatorTrue() : pValidator);

            table.RowDeleting += new DataRowChangeEventHandler(table_RowDeleting);

        }

        void table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            if (validator.check(e.Row))
            {
                if (block())
                {
                    try
                    {
                        for (int i = 0; i < columns.Length; ++i)
                            ToolCell.set(e.Row, columns[i], values[i]);
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

