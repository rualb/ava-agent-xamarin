using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceCurrencyList : ImplPagedSource
    {
        public PagedSourceCurrencyList(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderCurrencyList(pEnv))
        {
          
        }
    }
}
