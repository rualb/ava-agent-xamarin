using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForDataSet.Purchase.Operation.Slip
{
    public class AdapterDataSetPurchaseSlip : AdapterDataSetSlip
    {

        public AdapterDataSetPurchaseSlip(IEnvironment pEnv)
            :base(pEnv)
        {
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.materialPurchase;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.purchasing;
            lineIOCode = docIOCode = (short)ConstIOCode.input;



            docNumModule = (short)ConstDocNumModule.purchaseReceipts;
           docNumDocType = docTrCode;

   

        }
        
        
    }
}
