using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceDiv : ImplPagedSource
    {
        public PagedSourceDiv(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderDiv(pEnv))
        {
          
        }
    }
}
