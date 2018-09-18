using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowValidator
{
    public interface IRowValidator
    {
       
        bool check(DataRow row);
      
    }
}
