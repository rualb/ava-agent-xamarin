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
    public class AdapterDataSetWholesaleReturnSlip:AdapterDataSetSlip
    {

        public AdapterDataSetWholesaleReturnSlip(IEnvironment pEnv)
            :base(pEnv)
        {
            lineTrCode  = docTrCode = (short)ConstDocTypeMaterial.wholeSaleReturn;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.sales;
            lineIOCode = docIOCode = (short)ConstIOCode.input;

  

           docNumModule = (short)ConstDocNumModule.saleDispatch;
           docNumDocType = docTrCode;

         }
        
        
    }
}
