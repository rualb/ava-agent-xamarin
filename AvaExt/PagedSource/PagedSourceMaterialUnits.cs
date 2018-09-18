using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceMaterialUnits : ImplPagedSource
    {
        public PagedSourceMaterialUnits(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderMaterialUnits(pEnv))
        {
          
        }
    }
}
