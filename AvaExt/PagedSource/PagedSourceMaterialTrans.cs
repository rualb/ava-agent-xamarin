using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceMaterialTrans : ImplPagedSource
    {
        public PagedSourceMaterialTrans(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderMaterialTrans(pEnv))
        {

        }

    }
}
