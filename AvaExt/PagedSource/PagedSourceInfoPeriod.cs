using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceInfoPeriod : ImplPagedSource
    {
        public PagedSourceInfoPeriod(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderInfoPeriod(pEnv) )  
        {
         
        }

    }
}
