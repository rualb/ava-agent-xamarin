using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.SQL.Dynamic;
using AvaExt.TableOperation.TableConverter;

namespace AvaExt.PagedSource
{
    public interface IPagedSource:IDisposable
    {
        ISqlBuilder getBuilder();
         DataTable get();
        DataTable getAll();
        DataTable getFirst();
        DataTable getPreviose();
        DataTable getNext();
        DataTable getLast();
        DataTable getCurrent();
        bool isSortedAsc();
        void setSortingMode(bool pIsAsc);
        void reverseSort();
        DataTable getTableInstance();
        void setBorder(DataRow row);
        string[] getIndex();
        void setIndex(string[] indx);
        void preparingForSort(string[] pIndexColumns);
        void preparingForSort();
        void preparingForPagedWhere(string[] pIndexColumns, object[] pBorderData);
        void preparingForPagedWhere();
        int addTableConverter(ITableConverter pConv);
        void deleteTableConverter(int key);
        void deleteTableConverter(int[] keys);
    }
}
