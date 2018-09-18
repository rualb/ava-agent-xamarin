using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Purchase.Operation.Slip
{
    public class AdapterUserPurchaseReturnSlip:AdapterUserSlip
    {

        public AdapterUserPurchaseReturnSlip(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet, TableINVOICE.TABLE, TableSTLINE.TABLE)
        {
            innerVarMaterialModuleType = ConstMaterialModuleType.purchase;
            innerVarPriceListType = ConstPriceType.matPurchase;


            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.purchaseReturn;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.purchasing;
            lineIOCode = docIOCode = (short)ConstIOCode.output;

  

  
        }
    }
}
