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
    public class AdapterTableWarehouse : ImplAdapterTableExt
    {
        public AdapterTableWarehouse(IEnvironment env, string col)

            : base(
                    env,
                    new PagedSourceWarehouse(env),
                    new string[] { col },
                    TableWHOUSE.TABLE_RECORD_ID,
                    new ISqlBuilderPreparer[] {}
                    )
        {

        }


    }
}
