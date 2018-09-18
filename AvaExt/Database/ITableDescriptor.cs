using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Database
{
    public interface ITableDescriptor:IDisposable
    {
        string getNameShort();
        string getNameFull(); 
        ColumnDescriptor getColumn(string col);
        ColumnDescriptor[] getColumns();
    }
}
