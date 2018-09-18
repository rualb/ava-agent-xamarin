using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;

namespace AvaExt.TableOperation
{
    public interface ITableTouchMemory  
    {
         void touchCell(DataRow row, string col);
         long getCellTouch(DataRow row, string col);
         //Stack<Dublet<string, DataRow>> sort(Dublet<string, DataRow>[] pairs);
        // Stack<string> sort(string[] columns, DataRow row);
        string getOldest(string[] columns, DataRow row);
    }
}
