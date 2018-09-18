using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForDataSet.Sale.Operation.Slip
{
    public class AdapterDataSetZeroSlip : AdapterDataSetSlip
    {

        public AdapterDataSetZeroSlip(IEnvironment pEnv)
            :base(pEnv)
        {
            lineTrCode  = docTrCode = (short)ConstDocTypeMaterial.undef;
            lineGroupCode  = docGroupCode = (short)ConstDocGroupCode.sales;
            lineIOCode = docIOCode = (short)ConstIOCode.undef;

    

           docNumModule = (short)ConstDocNumModule.saleDispatch;
           docNumDocType = docTrCode;

   

        }
        
        
    }
}
