using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowValidator
{
    public class RowValidatorFalse : IRowValidator
    {
         
        public bool check(DataRow row)
        {
            return false;
        }


    }
}
