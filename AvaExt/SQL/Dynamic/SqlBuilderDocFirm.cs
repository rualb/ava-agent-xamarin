using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderDocFirm : ImplSqlBuilder
    {
        public SqlBuilderDocFirm(IEnvironment env)
            : base(env,
            string.Format(AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilder, TableFIRMDOC.TABLE_LONG, TableFIRMDOC.TABLE),
            TableFIRMDOC.TABLE)
        {
            addColumnToMeta(TableFIRMDOC.LREF, typeof(int));
            addColumnToMeta(TableFIRMDOC.DOCNR, typeof(int));
            addColumnToMeta(TableFIRMDOC.DOCTYP, typeof(int));
            addColumnToMeta(TableFIRMDOC.INFOREF, typeof(int));
            addColumnToMeta(TableFIRMDOC.INFOTYP, typeof(short));
            addColumnToMeta(TableFIRMDOC.LDATA, typeof(object));
        }


    }
}
