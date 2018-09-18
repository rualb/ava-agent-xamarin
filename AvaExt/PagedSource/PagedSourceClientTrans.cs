using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceClientTrans : ImplPagedSource
    {
        public PagedSourceClientTrans(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderClientTrans(pEnv))
        {
          
        }
    }
}
