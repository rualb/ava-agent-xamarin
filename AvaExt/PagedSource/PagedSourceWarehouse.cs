using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceWarehouse : ImplPagedSource
    {
        public PagedSourceWarehouse(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderWarehouse(pEnv))
        {
          
        }
    }
}
