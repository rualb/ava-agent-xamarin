using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourcePeriod : ImplPagedSource
    {
        public PagedSourcePeriod(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderPeriod(pEnv))
        {
          
        }
    }
}
