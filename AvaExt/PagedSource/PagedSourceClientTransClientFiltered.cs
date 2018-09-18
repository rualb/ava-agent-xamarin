using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceClientTransClientFiltered : ImplPagedSource
    {
        public PagedSourceClientTransClientFiltered(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderClientTransClientFiltered(pEnv))
        {
          
        }
    }
}
