using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingInnerExpr : RowColumnsBindingBase
    {

        public RowColumnsBindingInnerExpr(DataTable table, string[] colArr, string col, string expresion, IRowValidator pValidator)
            : base(table, colArr, col, expresion, pValidator)
        {

            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChangedForRow);

            if (ToolColumn.isColumnFullName(column))
                foreach (DataRow row in tableSource.Rows)
                    table_ColumnChangedForRow(tableSource, new DataColumnChangeEventArgs(row, tableSource.Columns[columns[0]], row[columns[0]]));
        }
        
        public override void activityForRow(DataColumnChangeEventArgs e)
        {
            
            for (int i = 0; i < columns.Length; ++i)
                evaluator.setVar(columns[i], e.Row[columns[i]]);
            ToolCell.set(e.Row, column, evaluator.getResult(column));
            
        }
    }

}

