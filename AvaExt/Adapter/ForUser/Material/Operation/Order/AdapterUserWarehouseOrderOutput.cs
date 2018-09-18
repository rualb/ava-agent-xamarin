using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;
using AvaExt.Adapter.ForUser.Material.Operation.Slip;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Material.Operation.Order
{
    public class AdapterUserWarehouseOrderOutput : AdapterUserWarehouseOrder
    {
        public AdapterUserWarehouseOrderOutput(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet)
        {
            lineIOCode = docIOCode = (short)ConstIOCode.outputFromWarehouse;
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.marerialDefinedOutput20;
        }


    }
}
