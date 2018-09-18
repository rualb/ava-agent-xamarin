using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceSlip : ImplPagedSource
    {
        public PagedSourceSlip(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderSlip(pEnv))
        {
         
        }

    }
}
