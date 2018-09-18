using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.Expression;
using AvaExt.Common;


namespace AvaExt.TableOperation
{
    public class TablesColumnsBinding
    {
        protected DataTable tableS_;
        protected DataTable tableD_;
        protected string[] columnS_;
        protected string[] columnD_;

        public TablesColumnsBinding(DataTable tableS, DataTable tableD, string[] columnS, string[] columnD, bool inherit)
        {
            tableS_ = tableS;
            columnS_ = columnS;
            tableD_ = tableD;
            columnD_ = columnD;
            tableS_.ColumnChanged += new DataColumnChangeEventHandler(tableS__ColumnChanged);

            if (inherit)
                tableD_.RowChanged += new DataRowChangeEventHandler(tableD__ColumnChanged);

            for (int d = 0; d < columnD_.Length; ++d)
                if (ToolColumn.isColumnFullName(columnD_[d]))
                    ToolColumn.setColumnValue(tableD_, columnD_[d], ToolColumn.getColumnLastValue(tableS_, columnS_[d], ToolCell.getCellTypeDefaulValue(tableS_.Columns[columnS_[d]].DataType)));
        }

        void tableD__ColumnChanged(object sender, DataRowChangeEventArgs e) 
        {
            if (e.Action == DataRowAction.Add)
                for (int d = 0; d < columnD_.Length; ++d)
                    ToolCell.set(e.Row, columnD_[d], ToolColumn.getColumnLastValue(tableS_, columnS_[d], ToolCell.getCellTypeDefaulValue(tableS_.Columns[columnS_[d]].DataType)));
        
        }


        void tableS__ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            for (int s = 0; s < columnS_.Length; ++s)
                if (e.Column.ColumnName == columnS_[s])
                    ToolColumn.setColumnValue(tableD_, columnD_[s], e.ProposedValue);
        }
    }

}

