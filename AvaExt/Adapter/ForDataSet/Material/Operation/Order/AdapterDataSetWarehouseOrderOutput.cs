using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Common;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common.Const;
using AvaExt.Adapter.ForDataSet.Material.Operation.Slip;

namespace AvaExt.Adapter.ForDataSet.Material.Operation.Order
{
    public class AdapterDataSetWarehouseOrderOutput: AdapterDataSetWarehouseOrder  
    {

        public AdapterDataSetWarehouseOrderOutput(IEnvironment pEnv)
            : base(pEnv)
        {
            lineIOCode = docIOCode = (short)ConstIOCode.outputFromWarehouse;
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.marerialDefinedOutput20;
        }
       
    }
}
