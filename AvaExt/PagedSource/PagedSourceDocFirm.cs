using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceDocFirm : ImplPagedSource
    {
        public PagedSourceDocFirm(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderDocFirm(pEnv))
        {
          
        }

 
    }
}
