using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceFack : ImplPagedSource
    {
        public PagedSourceFack(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderFack(pEnv))
        {
          
        }
    }
}
