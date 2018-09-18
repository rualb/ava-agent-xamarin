using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using AvaExt.Common;
using System.Data;
using System.Collections;
using AvaExt.MyException;
using AvaExt.Translating.Tools;

using AvaExt.SQL.Dynamic;
using AvaExt.PagedSource;
using Mono.Data.Sqlite;
using System.Data.Common;

namespace AvaExt.SQL
{
    public class SqlExecute
    {

        public static SqliteConnection getConnection(string pFile)
        {


            pFile = ToolMobile.getFullPath(pFile);

            string ds_ = "Data Source=" + pFile;

            if (!ToolMobile.existsFile(pFile))
                ds_ = ds_ + ";New=True;";

            SqliteConnection conn = null;


           // SqliteConnection.SetConfig(SQLiteConfig.MultiThread);

            conn = new SqliteConnection();
            ds_ = ds_ + "";

            conn.ConnectionString = ds_;

            conn.Open();

            //   conn.Open();
            //   conn.Close();

            return conn;



        }
        public static DataTable execute(IEnvironment env, string sql, object[] parameters)
        {

            ImplSqlBuilder b = new ImplSqlBuilder(env, sql, string.Empty);
            if (parameters != null)
                foreach (object p in parameters)
                    b.addFreeParameterValue(p);
            ImplPagedSource ps = new ImplPagedSource(env, b);
            return ps.getAll();

        }

        public static void executeBatch(String SqlText, IEnvironment env)
        {

            string buf = String.Empty;
            string sqlcmd = String.Empty;

            //  SqliteCommand SqlCmd;
            StringReader strreader;

            //  SqlCmd = env.getNewSqlCommand("");

            strreader = new StringReader(SqlText);
            while ((buf = strreader.ReadLine()) != null)
            {
                buf = buf.Trim();
                if (buf != String.Empty)
                {
                    if (buf == SqlTextConstant.EXECUTE)
                    {
                        if (sqlcmd != string.Empty)
                        {
                            executeNonQuery(env, sqlcmd);
                            sqlcmd = string.Empty;

                            //SqlCmd.CommandText = sqlcmd;
                            //sqlcmd = string.Empty;
                            // SqlCmd.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        sqlcmd += "\r\n" + buf;
                    }
                }
            }

        }

        public static string translate(List<string[]> trans, string sqlTest)
        {
            StringBuilder sqlbuilder = new StringBuilder(sqlTest);
            if (trans != null)
                for (int i = 0; i < trans.Count; ++i)
                    sqlbuilder.Replace(trans[i][0], trans[i][1]);
            return sqlbuilder.ToString();
        }

        public static object executeScalar(IEnvironment env, string sqlText)
        {
            return executeScalar(env, sqlText, null);
        }
        public static void executeNonQuery(IEnvironment env, string sqlText)
        {
            executeNonQuery(env, sqlText, null);
        }



        public static object executeScalar(IEnvironment env, string sqlText, object[] par)
        {

            try
            {
                openConn();
                //
                DbCommand c = null;
                try
                {
                    c = env.getNewSqlCommand(sqlText, convert(par));
                    return c.ExecuteScalar();
                }
                finally
                {
                    if (c != null) c.Dispose();
                }
            }
            finally
            {
                closeConn();
            }
        }
        public static void executeNonQuery(IEnvironment env, string sqlText, object[] par)
        {
            try
            {
                openConn();
                //
                DbCommand c = null;
                try
                {
                    c = env.getNewSqlCommand(sqlText, convert(par));
                    c.ExecuteNonQuery();

                }
                finally
                {
                    if (c != null) c.Dispose();
                }
            }
            finally
            {
                closeConn();
            }
        }
        public static void fill(ISqlBuilder pBuilder, DataTable pTab)
        {
            try
            {
                openConn();
                //
                SqliteDataAdapter adp = null;
                SqliteCommand cmd = null;
                try
                {
                    cmd = pBuilder.get();
                    adp = new SqliteDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(pTab);

                    //

                    //DbDataReader r_ = null;

                    //try
                    //{

                    //    r_ = cmd.ExecuteReader();
                    //    //
                    //    for (int i = 0; i < r_.FieldCount; ++i)
                    //    {
                    //        string name_ = r_.GetName(i);
                    //        Type type_ = r_.GetFieldType(i);

                    //        pTab.Columns.Add(name_, type_);
                    //    }

                    //    while (r_.NextResult())
                    //    {
                    //        DataRow row_ = pTab.NewRow();

                    //        for (int i = 0; i < r_.FieldCount; ++i)
                    //            row_[i] = r_.GetValue(i);

                    //        pTab.Rows.Add(row_);

                    //    }

                    //    pTab.AcceptChanges();
                    //}
                    //finally
                    //{
                    //    if (r_ != null)
                    //        r_.Close();

                    //    if (r_ != null)
                    //        r_.Dispose();
                    //}




                }
                finally
                {
                    if (adp != null)
                        adp.SelectCommand = null;

                    if (cmd != null)
                        cmd.Dispose();

                    if (adp != null)
                        adp.Dispose();

                }
            }
            finally
            {
                closeConn();
            }
        }

        public static void openConn()
        {
            // if (ToolMobile.getEnvironment().getTransaction() == null) //if out trans
            //     ToolMobile.getEnvironment().getConnection().Open();
        }

        public static void closeConn()
        {

            // if (ToolMobile.getEnvironment().getTransaction() == null) //if out trans
            //     ToolMobile.getEnvironment().getConnection().Close();
        }


        static SqliteParameter[] convert(object[] par)
        {
            if (par == null)
                par = new object[] { };
            List<SqliteParameter> list = new List<SqliteParameter>();
            foreach (object v in par)
                list.Add(new SqliteParameter("@P" + (list.Count + 1), v));

            return list.ToArray();

        }
    }
}
