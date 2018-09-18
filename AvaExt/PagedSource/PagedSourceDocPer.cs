using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceDocPer : ImplPagedSource
    {
        public PagedSourceDocPer(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderDocPer(pEnv))
        {
          
        }

 
    }
}
