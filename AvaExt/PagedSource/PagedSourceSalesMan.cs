using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceSalesMan : ImplPagedSource
    {
        public PagedSourceSalesMan(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderSalesMan(pEnv))
        {
          
        }
    }
}
