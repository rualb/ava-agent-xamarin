using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;
using AvaExt.Adapter.ForUser.Purchase.Operation.Slip;

namespace AvaExt.Adapter.ForUser.Purchase.Operation.Order
{
    public class AdapterUserPurchaseOrder : AdapterUserPurchaseSlip
    {
        public AdapterUserPurchaseOrder(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet)
        {
        }

 
    }
}
