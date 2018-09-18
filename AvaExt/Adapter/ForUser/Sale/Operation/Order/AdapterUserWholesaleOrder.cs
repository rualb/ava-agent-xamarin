using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;

namespace AvaExt.Adapter.ForUser.Sale.Operation.Order
{
    public class AdapterUserWholesaleOrder : AdapterUserWholesaleSlip
    {
        public AdapterUserWholesaleOrder(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet)
        {
        }

 
    }
}
