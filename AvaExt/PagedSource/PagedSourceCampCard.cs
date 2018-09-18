using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceCampCard : ImplPagedSource
    {
        public PagedSourceCampCard(IEnvironment pEnv)
            :base(pEnv,new SqlBuilderCompCard(pEnv))
        {
          
        }

        public const short constTypePurchase = 1;
        public const short constTypeSale = 2;
        //
        public const short constActiveTrue = 0;
        public const short constActiveFalse = 1;
    }
}
