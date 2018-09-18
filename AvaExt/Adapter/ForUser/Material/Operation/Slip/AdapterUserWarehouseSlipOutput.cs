using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Material.Operation.Slip
{
    public class AdapterUserWarehouseSlipOutput : AdapterUserWarehouseSlip 
    {

        public AdapterUserWarehouseSlipOutput(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet )
        {
            lineIOCode = docIOCode = (short)ConstIOCode.outputFromWarehouse;
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.marerialDefinedOutput20;
        }
    }
}
