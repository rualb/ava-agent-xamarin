using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;
using System.Data;
using System.ComponentModel;
using AvaExt.Adapter.Tools;
using AvaExt.InfoClass;
using AvaExt.Common;
using AvaExt.Common.Const;
using AvaExt.Database.Tools;
using AvaExt.TableOperation;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic.Preparing;


namespace AvaExt.Adapter.ForUser.Material.Operation.Slip
{
    public class AdapterUserWarehouseSlip : AdapterUserSlip
    {


        public AdapterUserWarehouseSlip(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet, TableINVOICE.TABLE, TableSTLINE.TABLE)
        {
            innerVarMaterialModuleType = ConstMaterialModuleType.materialManagment;
            innerVarPriceListType = ConstPriceType.undef;


            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.warehouse;
            lineGroupCode = docGroupCode = (short)ConstDocGroupCode.materialManagement;
            lineIOCode = docIOCode = (short)ConstIOCode.inputFromWarehouse;


        }


    }
}
