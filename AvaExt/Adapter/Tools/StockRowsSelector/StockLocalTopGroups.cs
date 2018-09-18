using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.TableOperation.RowsSelector;

namespace AvaExt.Adapter.Tools.StockRowsSelector
{
    public class StockLocalTopGroups : IRowsSelector
    {


        public DataRow[] get(DataRow row)
        {
            DataRow[] rows = new DataRow[0];
            DataTable table = row.Table;
            //
            if (row != null && row.RowState != DataRowState.Deleted)
                if (!ToolStockLine.isLineStlineGlobal(row))
                    if (ToolStockLine.isLinePureMoney(row))
                    {
                        for (int i = table.Rows.IndexOf(row) - 1; i >= 0; --i)
                        {
                            DataRow locRow = table.Rows[i];
                            if (locRow.RowState != DataRowState.Deleted)
                                if (ToolStockLine.isLineMonetary(locRow) && (!ToolStockLine.isLineStlineGlobal(locRow)))
                                {
                                    rows = new DataRow[] { locRow };
                                    break;
                                }
                        }
                    }
                    else
                        if (ToolStockLine.isLineMonetary(row))
                        {
                            rows = new DataRow[] { row };
                        }
            return rows;
        }



    }
}
