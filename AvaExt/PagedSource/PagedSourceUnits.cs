using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceUnits : ImplPagedSource
    {
        public PagedSourceUnits(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderUnits(pEnv))
        {
          
        }
    }
}
