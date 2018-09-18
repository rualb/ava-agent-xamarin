using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceFirm : ImplPagedSource
    {
        public PagedSourceFirm(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderFirm(pEnv))
        {
          
        }
    }
}
