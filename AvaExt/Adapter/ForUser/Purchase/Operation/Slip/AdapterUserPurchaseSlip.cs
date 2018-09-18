using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Purchase.Operation.Slip
{
    public class AdapterUserPurchaseSlip:AdapterUserSlip
    {

        public AdapterUserPurchaseSlip(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet, TableINVOICE.TABLE, TableSTLINE.TABLE)
        {
            innerVarMaterialModuleType = ConstMaterialModuleType.purchase;
            innerVarPriceListType = ConstPriceType.matPurchase;


            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.materialPurchase;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.purchasing;
            lineIOCode = docIOCode = (short)ConstIOCode.input;

        }
    }
}
