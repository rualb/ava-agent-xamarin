using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceInfoFirm : ImplPagedSource
    {
        public PagedSourceInfoFirm(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderInfoFirm(pEnv) )  
        {
         
        }

    }
}
