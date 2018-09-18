using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public enum ResetEvents:short
    {
        deleting = 1,
        changing = 2


    }
    
    public class RowReseter : RowColumnsBindingBase 
    { 
        string[] cols;

        public RowReseter(ResetEvents pEvent,DataTable table, string[] pCols, string[] pColsReset, IRowValidator pValidator)
            : base(table,pCols,  pValidator)
        {
            
            cols = pColsReset;
            switch (pEvent)
            {
                case ResetEvents.changing:
                    tableSource.ColumnChanging += new DataColumnChangeEventHandler(tableSource_ColumnChanging);
                    break;
                case ResetEvents.deleting:
                    tableSource.RowDeleting += new DataRowChangeEventHandler(tableSource_RowDeleting);
                    break;
            }
            
        }

        void tableSource_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                if ( validator.check(e.Row))
                {
                    if (block())
                    {
                        try
                        {

                            ToolRow.initTableNewRow(e.Row, cols);
                        }
                        finally
                        {
                            unblock();
                        }
                    }
                }
            }
        }

        void tableSource_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                if (isMyCollumn(e.Column.ColumnName) && validator.check(e.Row))
                {
                    if (block())
                    {
                        try
                        {

                            ToolRow.initTableNewRow(e.Row, cols);
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

}

