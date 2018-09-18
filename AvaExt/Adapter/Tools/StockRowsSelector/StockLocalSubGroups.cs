using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.Common;

namespace AvaExt.Adapter.Tools.StockRowsSelector
{
    public class StockLocalSubGroups : IRowsSelector
    {


        public DataRow[] get(DataRow row)
        {
            DataRow[] rows = new DataRow[0];
            DataTable table = row.Table;
            //
            if (row != null && row.RowState != DataRowState.Deleted)
                if (!ToolStockLine.isLineStlineGlobal(row))
                    if (ToolStockLine.isLineMonetary(row))
                    {
                        for (int i = table.Rows.IndexOf(row) + 1; i < table.Rows.Count; ++i)
                        {
                            DataRow locRow = table.Rows[i];
                            if (locRow.RowState != DataRowState.Deleted)
                            {
                                if (ToolStockLine.isLineMonetary(locRow))
                                    break;
                                if ((!ToolStockLine.isLineStlineGlobal(locRow)) && ToolStockLine.isLinePureMoney(locRow))
                                {
                                    if (rows == null)
                                        rows = new DataRow[] { locRow };
                                    else
                                    {
                                        rows =ToolArray.resize<DataRow>(rows, rows.Length + 1);
                                        
                                        rows[rows.Length - 1] = locRow;
                                    }
                                }
                            }
                        }
                    }
                    else
                        if (ToolStockLine.isLinePureMoney(row))
                        {
                            rows = new DataRow[] { row };
                        }
            return rows;
        }



    }
}
