using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Sale.Operation.Slip
{
    public class AdapterUserZeroSlip:AdapterUserSlip
    {

        public AdapterUserZeroSlip(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet, TableINVOICE.TABLE, TableSTLINE.TABLE)
        {
            innerVarMaterialModuleType = ConstMaterialModuleType.sale;
            innerVarPriceListType = ConstPriceType.matSale;


            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.undef;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.sales;
            lineIOCode = docIOCode = (short)ConstIOCode.undef;

        }
    }
}
