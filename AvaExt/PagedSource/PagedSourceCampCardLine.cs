using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;

namespace AvaExt.PagedSource
{
    public class PagedSourceCampCardLine : ImplPagedSource
    {
        public PagedSourceCampCardLine(IEnvironment pEnv)
            : base(pEnv, new SqlBuilderCompCardLine(pEnv))
        {

        }

        public const short constApplyTypeLocal = 0;
        public const short constApplyTypeGlobal = 1; 
        public const short constCompTypeDiscount = 1;
        public const short constCompTypeSurcharge = 2;
        public const short constCompTypePromotion = 3; 
    }
}
