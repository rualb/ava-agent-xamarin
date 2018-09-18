using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceCashTrans : ImplPagedSource
    {
        public PagedSourceCashTrans(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderCashTrans(pEnv)) 
        {
          
        }
    }
}
