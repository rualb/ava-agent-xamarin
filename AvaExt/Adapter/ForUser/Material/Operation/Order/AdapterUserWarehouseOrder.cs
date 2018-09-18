using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;
using AvaExt.Adapter.ForUser.Material.Operation.Slip;

namespace AvaExt.Adapter.ForUser.Material.Operation.Order
{
    public class AdapterUserWarehouseOrder : AdapterUserWarehouseSlip
    {
        public AdapterUserWarehouseOrder(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet)
        {
        }

 
    }
}
