using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using System.Data;

namespace AvaExt.PagedSource
{
    public class PagedSourcePriceList : ImplPagedSource
    {
        public PagedSourcePriceList(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderPriceList(pEnv))
        {
          
        }
       
    }
}
