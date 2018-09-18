using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Database
{
    public interface IDbDscriptor : IDisposable
    {
        ITableDescriptor getTable(string pTableName);
        Type getColumnType(string pTableName, string pColName, Type pDef);
        int getColumnSize(string pTableName, string pColName, int pDef);
        Type getColumnType(string pTableName, string pColName);
    }
}
