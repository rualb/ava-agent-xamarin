using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderTradingGroup : ImplSqlBuilder
    {
        public SqlBuilderTradingGroup(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderTradingGroup, TableTRADGRP.TABLE)
        {

            addColumnToMeta(TableTRADGRP.LOGICALREF, typeof(int));
            addColumnToMeta(TableTRADGRP.GCODE, typeof(string));
            addColumnToMeta(TableTRADGRP.GDEF, typeof(string));
 
        }


    }
}
