using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Adapter.ForUser.Purchase.Operation.Slip;

namespace AvaExt.Adapter.ForUser.Purchase.Operation.Order
{
    public class AdapterUserPurchaseReturnOrder : AdapterUserPurchaseReturnSlip
    {
        public AdapterUserPurchaseReturnOrder(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet)
        {
        }
   
       
    }
}
