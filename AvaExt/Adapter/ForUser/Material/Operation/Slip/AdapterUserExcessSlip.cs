using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Material.Operation.Slip
{
    public class AdapterUserExcessSlip : AdapterUserSlip
    {

        public AdapterUserExcessSlip(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet, TableINVOICE.TABLE, TableSTLINE.TABLE)
        {
            innerVarMaterialModuleType = ConstMaterialModuleType.materialManagment;
            innerVarPriceListType = ConstPriceType.matSale;


            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.materialExcess;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.materialManagement;
            lineIOCode = docIOCode = (short)ConstIOCode.input;


        }
    }
}
