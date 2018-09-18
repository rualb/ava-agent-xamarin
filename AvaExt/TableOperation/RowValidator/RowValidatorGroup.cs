using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowValidator
{
    public class RowValidatorGroup : IRowValidator
    {
        IRowValidator[] arr;
        public RowValidatorGroup(IRowValidator[] pArr)
        {
            arr = pArr;
        }
        public bool check(DataRow row)
        {
            for (int i = 0; i < arr.Length; ++i)
                if (!arr[i].check(row))
                    return false;
            return true;
        }


    }
}
