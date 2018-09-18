using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using AvaExt.Common;
using System.Data;
using System.Collections;
 
using Mono.Data.Sqlite;

namespace AvaExt.SQL
{
    public class SqlPF
    {
        public static SqliteParameter get(String name, SqlDbType type, object value)
        {
          SqliteParameter p = new SqliteParameter(name,type);
          p.Value = value;
          return p;
        }
    }
}
