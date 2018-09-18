using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderFirmParams : ImplSqlBuilder
    {
        public SqlBuilderFirmParams(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderFirmParams, TableFIRMPARAMS.TABLE)
        {
            addColumnToMeta(TableFIRMPARAMS.LOGICALREF, typeof(int));
            addColumnToMeta(TableFIRMPARAMS.CODE, typeof(string));
            addColumnToMeta(TableFIRMPARAMS.VALUE, typeof(string));
            addColumnToMeta(TableFIRMPARAMS.FIRMNR, typeof(short));
        }


    }
}
