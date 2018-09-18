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
    public class AdapterUserWarehouseOrderList : AdapterUserWarehouseSlip
    {
        public AdapterUserWarehouseOrderList(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet)
        {
            lineIOCode = docIOCode = (short)ConstIOCode.inputFromWarehouse;
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.marerialDefinedInput18;
        }


    }
}
