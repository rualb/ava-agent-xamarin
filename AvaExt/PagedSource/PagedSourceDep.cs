using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceDep : ImplPagedSource
    {
        public PagedSourceDep(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderDep(pEnv))
        {
          
        }
    }
}
