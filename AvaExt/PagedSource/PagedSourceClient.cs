using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceClient : ImplPagedSource
    {
        public PagedSourceClient(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderClient(pEnv))
        {
          
        }
    }
}
