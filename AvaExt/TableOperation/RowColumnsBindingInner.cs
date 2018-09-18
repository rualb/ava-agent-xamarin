using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;
using AvaExt.MyException;

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingInner : RowColumnsBindingBase
    {

        public RowColumnsBindingInner(DataTable table, string[] colArr, double coif, ICellMath pForward, ICellMath pBackward, IRowValidator pValidator)
            : base(table, colArr, coif, pForward, pBackward, pValidator)
        {

            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChangedForRow);
        }

 
        public override void activityForRow(DataColumnChangeEventArgs e)
        {
            string curColumn = e.Column.ColumnName;
            Dublet<string, string> pair = new Dublet<string, string>(string.Empty, string.Empty);
            touchCell(e.Row, curColumn);
            Stack<string> stack = toucher.sort(columns, e.Row);
            //Stack<Dublet<string, DataRow>> stack =
            //toucher.sort(new Dublet<string, DataRow>[]{
            //new Dublet<string,DataRow>(columns[0],e.Row),
            //new Dublet<string,DataRow>(columns[1],e.Row),
            //new Dublet<string,DataRow>(columns[2],e.Row)
            //});
            if (stack.Count >= 2)
            {

                pair.first = stack.Pop();
                pair.second = stack.Pop();

                if ((pair.first == columns[0] && pair.second == columns[1]) || (pair.first == columns[1] && pair.second == columns[0]))
                    forward.doMath(e.Row, columns[2], e.Row[columns[0]], e.Row[columns[1]], padCoif);
                else
                    if ((pair.first == columns[1] && pair.second == columns[2]) || (pair.first == columns[2] && pair.second == columns[1]))
                        backward.doMath(e.Row, columns[0], e.Row[columns[2]], e.Row[columns[1]], padCoif);
                    else
                        if ((pair.first == columns[2] && pair.second == columns[0]) || (pair.first == columns[0] && pair.second == columns[2]))
                            backward.doMath(e.Row, columns[1], e.Row[columns[2]], e.Row[columns[0]], padCoif);

            }
        }
    }

}

