using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowValidator
{
    public class RowValidatorNotInListInt : IRowValidator
    {
        string colInTable;
        int[] arr;
        public RowValidatorNotInListInt(string pColInTable, int[] pArr)
        {
            colInTable = pColInTable;
            arr = pArr;
        }
        public bool check(DataRow row)
        {
            return !(Array.IndexOf<int>(arr, Convert.ToInt32(row[colInTable])) >= 0);
        }


    }
}
