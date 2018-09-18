using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowValidator
{
    public class RowValidatorTrue : IRowValidator
    {

        public bool check(DataRow row)
        {
           return true;
        }


    }
}
