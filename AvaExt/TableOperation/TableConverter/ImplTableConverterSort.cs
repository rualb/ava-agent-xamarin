using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.TableConverter
{
    class ImplTableConverterSort : ITableConverter
    {
        string sortStr = string.Empty;
        public ImplTableConverterSort(string pSortStr)
        {
            sortStr = pSortStr;
        }
        public ImplTableConverterSort(string[] pCols, bool isAsc)
        {
            sortStr = string.Empty;
            foreach (string col in pCols)
                sortStr += string.Format("{0} {1},", col, isAsc ? "ASC" : "DESC");
            sortStr = sortStr.Trim(',');
        }
        public DataTable convert(DataTable pTable)
        {
            DataTable tabCopy = pTable.Copy();
            pTable.Clear();
            tabCopy.DefaultView.Sort = sortStr;
            pTable.Load(tabCopy.DefaultView.ToTable().CreateDataReader());
            return pTable;
        }


    }
}
