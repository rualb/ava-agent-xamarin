using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;

namespace AvaExt.TableOperation.CellAutomation
{
    public interface ITableColumnChanged
    {
        void columnChanged(DataColumnChangeEventArgs e);
        void initForColumnChanged(DataRow pRow);
    }
}
