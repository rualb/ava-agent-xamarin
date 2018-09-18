using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowValidator
{
    public class RowValidatorInListInt : IRowValidator
    {
        string colInTable;
        int[] arr;
        public RowValidatorInListInt(string pColInTable, int[] pArr)
        {
            colInTable = pColInTable;
            arr = pArr;
        }
        public bool check(DataRow row)
        {
            if (row == null || row.RowState == DataRowState.Deleted)
                return false;

            return (Array.IndexOf<int>(arr, Convert.ToInt32(row[colInTable])) >= 0);
        }


    }
}
