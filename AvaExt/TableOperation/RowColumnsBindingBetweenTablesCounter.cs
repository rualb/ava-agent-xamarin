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
    public class RowColumnsBindingBetweenTablesCounter : RowColumnsBindingBase
    {
        bool distinct = false;

        DataTable tableDest; 
        public RowColumnsBindingBetweenTablesCounter(DataTable tableS, DataTable tableD, string colS, string colD, bool pDistinct, IRowValidator pValidator)
            : base(tableS, new string[] { colS } , colD, pValidator)
        {
            distinct = pDistinct;
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
                    List<object> list = new List<object>();
                    for (int r = 0; r < tableSource.Rows.Count; ++r)
                    {
                        DataRow row = tableSource.Rows[r];
                        if (row.RowState != DataRowState.Deleted && validator.check(row))
                        {
                            object obj = row[columns[0]];
                            if (distinct)
                            {
                                if (list.IndexOf(obj) >= 0)
                                    continue;
                            }
                            list.Add(obj);
                        }
                    }
                    ToolColumn.setColumnValue(tableDest, column, list.Count );

                }
                finally
                {
                    unblock();
                }
            }
        }
    }

}

