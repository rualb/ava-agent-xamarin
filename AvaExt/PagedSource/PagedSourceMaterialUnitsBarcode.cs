using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceMaterialUnitsBarcode : ImplPagedSource
    {
        public PagedSourceMaterialUnitsBarcode(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderMaterialUnitsBarcode(pEnv))
        {
          
        }
    }
}
