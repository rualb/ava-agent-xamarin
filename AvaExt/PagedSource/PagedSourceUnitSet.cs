using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceUnitSet : ImplPagedSource
    {
        public PagedSourceUnitSet(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderUnitSet(pEnv))
        {
          
        }
    }
}
