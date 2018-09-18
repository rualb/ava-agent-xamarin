using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using System.Data;

namespace AvaExt.Adapter.ForDataSet
{
    public interface IAdapterDataSet
    {
        object get(object pId);
        object set(object pData);
        //object beginEplisitBatch();
        //object commitEplisitBatch();
        string getCode();
    }
}
