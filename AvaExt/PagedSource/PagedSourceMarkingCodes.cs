using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using AvaExt.Common.Const;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Table;

namespace AvaExt.PagedSource
{
    public class PagedSourceMarkingCodes : ImplPagedSource
    {
        public PagedSourceMarkingCodes(IEnvironment pEnv, ConstMarkingCodesGroup pGroup, ConstMarkingCodesType pType)
            : base(pEnv, new SqlBuilderMarkingCodes(pEnv))
        {
            //
            getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableSPECODES.CODETYPE, (short)pGroup));
            getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableSPECODES.SPECODETYPE, (short)pType));
        }
    }
}
