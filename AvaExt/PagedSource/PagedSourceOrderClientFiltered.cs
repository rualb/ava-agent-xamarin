using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceOrderClientFiltered : ImplPagedSource
    {
        public PagedSourceOrderClientFiltered(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderOrderClientFiltered(pEnv))
        {
          
        }
    }
}
