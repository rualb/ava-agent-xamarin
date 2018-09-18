using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceOrder : ImplPagedSource
    {
        public PagedSourceOrder(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderOrder(pEnv))
        {
          
        }
    }
}
