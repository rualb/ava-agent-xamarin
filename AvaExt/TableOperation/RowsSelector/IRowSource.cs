using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;
using AvaExt.ObjectSource;

namespace AvaExt.TableOperation.RowsSelector
{
    public interface IRowSource 
    {

        DataRow get();

    }
}
