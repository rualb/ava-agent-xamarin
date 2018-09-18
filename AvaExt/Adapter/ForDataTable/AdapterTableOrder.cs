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
    public class AdapterTableOrder : ImplAdapterTableExt
    {
        public AdapterTableOrder(IEnvironment env, string col)

            : base(
                    env,
                    new PagedSourceOrder(env),
                    new string[] { col },
                    TableORFICHE.TABLE_RECORD_ID,
                    new ISqlBuilderPreparer[] {}
                    )
        {

        }


    }
}
