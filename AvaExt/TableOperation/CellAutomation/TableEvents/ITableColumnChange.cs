using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;

namespace AvaExt.TableOperation.CellAutomation.TableEvents
{
    public interface ITableColumnChange
    {
        void columnChange(DataColumnChangeEventArgs e); 
        void initForColumnChanged(DataRow pRow);
    }
}
