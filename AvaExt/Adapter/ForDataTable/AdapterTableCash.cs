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

namespace AvaExt.Adapter.ForDataTable
{
    public class AdapterTableCash : ImplAdapterTableExt
    {
        public AdapterTableCash(IEnvironment env, string col)

            : base(
                    env,
                    new PagedSourceCashTrans(env),
                    new string[] { col },
                    TableKSLINES.TABLE_RECORD_ID,
                    new ISqlBuilderPreparer[] { }
                    )
        {

        }


    }
}
