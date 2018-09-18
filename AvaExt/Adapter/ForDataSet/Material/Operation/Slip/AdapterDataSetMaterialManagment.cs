using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic.Preparing;

namespace AvaExt.Adapter.ForDataSet.Material.Operation.Slip
{
    public abstract class AdapterDataSetMaterialManagment : AdapterDataSetSlip
    {

        public AdapterDataSetMaterialManagment(IEnvironment pEnv)
            : base(
            pEnv

            )
        {
            
        }

    }
}
