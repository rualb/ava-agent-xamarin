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
    public class AdapterDataSetExcessSlip : AdapterDataSetSlip
    {

        public AdapterDataSetExcessSlip(IEnvironment pEnv)
            :base(pEnv)
        {
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.materialExcess;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.materialManagement;
            lineIOCode = docIOCode = (short)ConstIOCode.input;


            docNumModule = (short)ConstDocNumModule.materialSlips;
            docNumDocType = docTrCode;

   

        }
        
        
    }
}
