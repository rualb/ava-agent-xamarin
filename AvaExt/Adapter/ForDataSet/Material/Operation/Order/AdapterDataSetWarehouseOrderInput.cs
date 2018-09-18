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
    public class AdapterDataSetWarehouseOrderInput: AdapterDataSetWarehouseOrder  
    {

        public AdapterDataSetWarehouseOrderInput(IEnvironment pEnv)
            : base(pEnv)
        {

            lineIOCode = docIOCode = (short)ConstIOCode.inputFromWarehouse;
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.marerialDefinedInput15;
        }
       
    }
}
