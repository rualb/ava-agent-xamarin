using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceInfoDocSave : ImplPagedSource
    {
        public PagedSourceInfoDocSave(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderInfoDocSave(pEnv) )  
        {
         
        }

    }
}
