using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class ImplPagedSourceFromDs : ImplPagedSource
    {
        public ImplPagedSourceFromDs(IEnvironment pEnv,string pTableName,string pDsName)
            : base(pEnv, new SqlBuilderFromDs(pEnv, pTableName, pDsName)) 
        {
          
        }
    }
}
