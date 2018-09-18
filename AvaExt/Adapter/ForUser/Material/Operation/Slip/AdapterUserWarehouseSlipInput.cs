using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Material.Operation.Slip
{
    public class AdapterUserWarehouseSlipInput : AdapterUserWarehouseSlip 
    {

        public AdapterUserWarehouseSlipInput(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet )
        {
            lineIOCode = docIOCode = (short)ConstIOCode.inputFromWarehouse;
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.marerialDefinedInput15;
        }
    }
}
