using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceClientPayTrans : ImplPagedSource
    {
        public PagedSourceClientPayTrans(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderClientPayTrans(pEnv))
        {
          
        }
    }
}
