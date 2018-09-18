using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderMaterialTrans : ImplSqlBuilder
    {
        public SqlBuilderMaterialTrans(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderMaterialTrans, TableSTLINE.TABLE)
        {
            //addColumnToMeta(TableSTLINE.LOGICALREF, typeof(string));
            //addColumnToMeta(TableSTLINE.STOCKREF, typeof(string));
            //addColumnToMeta(TableSTLINE.CANCELLED, typeof(short));
            //addColumnToMeta(TableSTLINE.TRCODE, typeof(short));
            //addColumnToMeta(TableSTLINE.LINETYPE, typeof(short));
            //addColumnToMeta(TableSTLINE.STDOCREF, typeof(string));
           
        }


    }
}
