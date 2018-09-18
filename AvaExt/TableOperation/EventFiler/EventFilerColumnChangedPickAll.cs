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
    public class EventFilerColumnChangedPickAll : RowColumnsBindingBase
    {


        EventFilerActivityForRow wkrRow;

        IRowValidator validatorMainRow;


        public EventFilerColumnChangedPickAll(DataTable table, string[] trigerCols, EventFilerActivityForRow worker, IRowValidator pValidatorMainRow, IRowValidator pValidator)
            : base(table, trigerCols, pValidator)
        {
            wkrRow = worker;
            validatorMainRow = pValidatorMainRow;
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChangedRow);
        }


        void table_ColumnChangedRow(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                string curColumn = e.Column.ColumnName;
                if (isMyCollumn(curColumn) && validator.check(e.Row))
                {
                    if (block())
                    {
                        try
                        {
                            for (int i = 0; i < e.Row.Table.Rows.Count; ++i)
                            {
                                DataRow row = e.Row.Table.Rows[i];
                                if (row.RowState != DataRowState.Deleted)
                                    if (validatorMainRow.check(row))
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
        }




    }

}

