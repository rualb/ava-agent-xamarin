using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceMaterialGenStat : ImplPagedSource
    {
        public PagedSourceMaterialGenStat(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderMaterialGenStat(pEnv))
        {
          
        }
    }
}
