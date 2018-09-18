using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderInfoDocSave : ImplSqlBuilder
    {
        public SqlBuilderInfoDocSave(IEnvironment env)
            : base(env,
            string.Format(  AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilder, TableINFODOCSAVE.TABLE_FULL_NAME, TableINFODOCSAVE.TABLE),
            TableINFODOCSAVE.TABLE)
        {
          //  addColumnToMeta(TableINFODOCSAVE.LOGICALREF, typeof(string)); 
      
        }


    }
}
