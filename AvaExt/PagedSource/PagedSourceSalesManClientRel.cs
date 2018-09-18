using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceSalesManClientRel : ImplPagedSource
    {
        public PagedSourceSalesManClientRel(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderSalesManClientRel(pEnv))
        {
          
        }
    }
}
