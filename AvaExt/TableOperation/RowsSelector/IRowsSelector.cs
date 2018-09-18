using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowsSelector
{
    public interface IRowsSelector
    {
       
        DataRow[] get(DataRow row);
      
    }
}
