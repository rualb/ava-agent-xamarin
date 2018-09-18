using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceMaterial : ImplPagedSource
    {
        public PagedSourceMaterial(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderMaterial(pEnv))
        {
          
        }
    }
}
