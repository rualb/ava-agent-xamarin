using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;
using System.IO;



namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderFromDs : ImplSqlBuilder
    {
        public SqlBuilderFromDs(IEnvironment env, string pTableName, string pDsName)
            : base(env, getSQL(env, pDsName), pTableName)
        {


        }


        static string getSQL(IEnvironment env, string pDsName)
        {
            //string file_ = Path.Combine(Path.Combine("config/ref/", pDsName), "ds.sql");
            //return ToolMobile.readFileTextByCache(file_);

            //TODO use cachec ToolMobile.readFileTextByCache
            var dir = Path.Combine("config/ref/", pDsName);

            return ToolMobile.getFsOrResourceText(dir, "ds.sql");

          
        }

    }
}
