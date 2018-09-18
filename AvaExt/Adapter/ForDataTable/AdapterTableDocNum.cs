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
    public class AdapterTableDocNum : ImplAdapterTableExt
    {
        public AdapterTableDocNum(IEnvironment env, string col)

            : base(
                    env,
                    new PagedSourceDocNum(env),
                    new string[] { col },
                    TableDOCNUM.TABLE_RECORD_ID,
                    new ISqlBuilderPreparer[] { }
                    )
        {

        }


    }
}
