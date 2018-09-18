using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.MyException;

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingSpace : RowColumnsBindingBase
    {

        IRowsSelector forTop;
        IRowsSelector forSub;

        public RowColumnsBindingSpace(DataTable table, string[] col, double coif, ICellMath pForward, ICellMath pBackward, IRowsSelector pForTop, IRowsSelector pForSub, IRowValidator pValidator)
            : base(table, col, coif, pForward, pBackward,  pValidator)
        {
            //
            forTop = pForTop;
            forSub = pForSub;

            tableSource.RowDeleting += new DataRowChangeEventHandler(table_RowDeleting);
            tableSource.RowChanged += new DataRowChangeEventHandler(table_RowChanged);
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChanged);
        }
        protected   void table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add && validator.check(e.Row))
            {
               
                if (block())
                {
                    try
                    {
                        table_DistributeValues(forTop.get(e.Row), forSub.get(e.Row));
                    }
 
                    finally
                    {
                        unblock();
                    }

                }

            }
        }
        protected   void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (validator.check(e.Row))
            {
                string curColumn = e.Column.ColumnName;
                if (isMyCollumn(curColumn))
                    if (block())
                    {
                        try
                        {
                            touchCell(e.Row, curColumn);
                            table_DistributeValues(forTop.get(e.Row), forSub.get(e.Row));
                        }
 
                        finally
                        {
                            unblock();
                        }

                    }
            }
        }
        protected   void table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            if (validator.check(e.Row))
            {
                if (block())
                {
                    try
                    {

                        List<DataRow> topRowsTmp = new List<DataRow>((IEnumerable<DataRow>)forTop.get(e.Row));
                        List<DataRow> subRowsTmp = new List<DataRow>((IEnumerable<DataRow>)forSub.get(e.Row));
                        topRowsTmp.Remove(e.Row);
                        subRowsTmp.Remove(e.Row);
                        DataRow[] topRows = topRowsTmp.ToArray();
                        DataRow[] subRows = subRowsTmp.ToArray();
                        table_DistributeValues(topRows, subRows);
                    }
 
                    finally
                    {
                        unblock();
                    }

                }
            }
        }

 

        protected void table_DistributeValues(DataRow[] topRows, DataRow[] subRows)
        {
            Dublet<string, string> pair = new Dublet<string, string>(string.Empty, string.Empty);

            double topSum = ColumnMath.sum(topRows, columns[2]);
            for (int i = 0; i < subRows.Length; ++i)
            {
                DataRow curSubRow = subRows[i];
                Stack<Dublet<string, DataRow>> stack =
                toucher.sort(new Dublet<string, DataRow>[]{
                                new Dublet<string,DataRow>(columns[0],curSubRow),
                                new Dublet<string,DataRow>(columns[1],curSubRow),
                                });
                if (stack.Count >= 1)
                {
                    pair.second = stack.Pop().first;
                    if (pair.second == columns[0])
                        forward.doMath(curSubRow, columns[1], topSum, curSubRow[columns[0]], padCoif);
                    else
                        if (pair.second == columns[1])
                            backward.doMath(curSubRow, columns[0], curSubRow[columns[1]], topSum, padCoif);
                }
            }


        }


    }

}

