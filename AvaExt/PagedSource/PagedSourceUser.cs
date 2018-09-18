using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceUser : ImplPagedSource
    {
        public PagedSourceUser(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderUser(pEnv))
        {
          
        }
    }
}
