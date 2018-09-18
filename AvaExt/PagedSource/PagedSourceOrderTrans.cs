using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceOrderTrans : ImplPagedSource
    {
        public PagedSourceOrderTrans(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderOrderTrans(pEnv))
        {
          
        }
    }
}
