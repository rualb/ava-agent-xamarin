using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForDataSet.Material.Operation.Slip
{
    public class AdapterDataSetWarehouseSlipInput : AdapterDataSetWarehouse
    {

        public AdapterDataSetWarehouseSlipInput(IEnvironment pEnv)
            :base(pEnv)
        {
            lineIOCode = docIOCode = (short)ConstIOCode.inputFromWarehouse;
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.marerialDefinedInput15;
   

        }
        
        
    }
}
