using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceDocNum : ImplPagedSource
    {
        public PagedSourceDocNum(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderDocNum(pEnv))
        {
          
        }
    }
}
