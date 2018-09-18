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
    public class AdapterDataSetDeficitSlip : AdapterDataSetSlip
    {

        public AdapterDataSetDeficitSlip(IEnvironment pEnv)
            :base(pEnv)
        {
            lineTrCode  = docTrCode = (short)ConstDocTypeMaterial.materialDeficit;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.materialManagement;
            lineIOCode = docIOCode = (short)ConstIOCode.output;
 

           docNumModule = (short)ConstDocNumModule.materialSlips;
           docNumDocType = docTrCode;

   

        }
        
        
    }
}
