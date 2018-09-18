using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation.RowIniter
{
    public class ImplRowIniterDefaults : IRowIniter
    {
        string[] cols;

        public ImplRowIniterDefaults(string[] pExcludeCols)
        {
            cols = pExcludeCols;
        }
        public void set(DataRow pRow)
        {
            if (pRow != null)
                foreach (DataColumn col in pRow.Table.Columns)
                    if (Array.IndexOf<string>(cols, col.ColumnName) < 0)
                        ToolCell.set(pRow, col.ColumnName, ToolCell.getCellTypeDefaulValue(col.DataType));
        }


    }
}
