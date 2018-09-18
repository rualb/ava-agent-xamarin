using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaExt.TableOperation;

namespace AvaExt.Adapter.ForDataSet.Material.Operation.Slip
{
    public class AdapterDataSetWarehouse : AdapterDataSetMaterialManagment
    {

        public AdapterDataSetWarehouse(IEnvironment pEnv)
            : base(pEnv)
        {
            lineTrCode = docTrCode = (short)ConstDocTypeMaterial.warehouse;
            lineGroupCode =  docGroupCode = (short)ConstDocGroupCode.materialManagement;
            lineIOCode = docIOCode = (short)ConstIOCode.inputFromWarehouse;
 
            docNumModule = (short)ConstDocNumModule.materialSlips;
            docNumDocType = docTrCode;
        }


     
     
 


    }
}
