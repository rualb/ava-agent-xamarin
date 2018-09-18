using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceTradingGroup : ImplPagedSource
    {
        public PagedSourceTradingGroup(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderTradingGroup(pEnv))
        {
          
        }
    }
}
