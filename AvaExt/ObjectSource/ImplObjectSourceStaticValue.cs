using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowsSelector;
using System.Data;
using AvaExt.Common;

namespace AvaExt.ObjectSource
{
    public class ImplObjectSourceStaticValue:IObjectSource 
    {
        object val;
        public ImplObjectSourceStaticValue(object pVal)
        {
            val = pVal;
        }
        public object get()
        {
            return val;
        }

        public void set(object pVal)
        {
            val = pVal;
        }
    }
}
