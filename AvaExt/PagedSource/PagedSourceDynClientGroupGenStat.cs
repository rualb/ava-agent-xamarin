using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceDynClientGroupGenStat : ImplPagedSource
    {
        public PagedSourceDynClientGroupGenStat(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderDynClientGroupGenStat(pEnv))
        {
          
        }
    }
}
