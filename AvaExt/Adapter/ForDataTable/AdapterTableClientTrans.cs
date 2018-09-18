using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Adapter.Const;
using AvaExt.Common;

using AvaExt.Adapter.Tools;
using AvaExt.PagedSource;
using AvaExt.SQL;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForDataTable
{
    public class AdapterTableClientTrans : ImplAdapterTableExt
    {
        public AdapterTableClientTrans(IEnvironment env, string col, ConstOperationType mod)

            : base(
                    env,
                    new PagedSourceClientTrans(env),
                    new string[] { col },
                    TableCLFLINE.TABLE_RECORD_ID,
                    new ISqlBuilderPreparer[] { new SqlBuilderPreparerFixedCondition(TableCLFLINE.MODULENR, (short)mod) }
                    )
        {

        }


    }
}
