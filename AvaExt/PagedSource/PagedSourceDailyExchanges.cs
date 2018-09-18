using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceDailyExchanges : ImplPagedSource
    {
        public PagedSourceDailyExchanges(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderDailyExchanges(pEnv))
        {
         
        }

    }
}
