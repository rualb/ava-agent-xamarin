using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceFirmParams : ImplPagedSource
    {
        public PagedSourceFirmParams(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderFirmParams(pEnv))
        {
          
        }
    }
}
