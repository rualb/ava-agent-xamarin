using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceMatsIO : ImplPagedSource
    {
        public PagedSourceMatsIO(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderMatsIO(pEnv))
        {
          
        }

 
    }
}
