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
using AvaExt.Common;
using AvaExt.TableOperation.ActivityTypes;

namespace AvaExt.TableOperation.EventFiler
{
    public class EventFilerColumnChanged : RowColumnsBindingBase
    {

        WorkerStart wkrAll;
        EventFilerActivityForRow wkrRow;
        EventFilerActivityForRowColumn wkrRowCol;
 

        public EventFilerColumnChanged(DataTable table, string[] trigerCols, WorkerStart worker, IRowValidator pValidator)
            : base(table, trigerCols, pValidator)
        {
            wkrAll = worker;
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChangedAll);
        } 
        public EventFilerColumnChanged(DataTable table, string[] trigerCols, EventFilerActivityForRow worker, IRowValidator pValidator)
            : base(table, trigerCols, pValidator)
        {
            wkrRow = worker;
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChangedRow);
        }
        public EventFilerColumnChanged(DataTable table, string[] trigerCols, EventFilerActivityForRowColumn worker, IRowValidator pValidator)
            : base(table, trigerCols, pValidator)
        {
            wkrRowCol = worker;
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChangedRowCol);
        }

 
        void table_ColumnChangedAll(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                if (block())
                {
                    try
                    {
                        string curColumn = e.Column.ColumnName;
                        if (isMyCollumn(curColumn) && validator.check(e.Row))
                        {
                            wkrAll.Invoke();
                        }
                    }

                    finally
                    {
                        unblock();
                    }
                }
                
            }
        }
        void table_ColumnChangedRow(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                if (block())
                {
                    try
                    {
                        string curColumn = e.Column.ColumnName;
                        if (isMyCollumn(curColumn) && validator.check(e.Row))
                        {
                            wkrRow.Invoke(e.Row);
                        }
                    }

                    finally
                    {
                        unblock();
                    }
                }

            }
        }
        void table_ColumnChangedRowCol(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                if (block())
                {
                    try
                    {
                        string curColumn = e.Column.ColumnName;
                        if (isMyCollumn(curColumn) && validator.check(e.Row))
                        {
                            wkrRowCol.Invoke(e.Row, curColumn);
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

}

