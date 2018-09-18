using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaAgent.AvaExt.SQL.Resources;
using AvaExt.UI;
using System.IO;
using AvaExt.TableOperation;



namespace AvaExt.SQL.DBSupport
{
    public class DBSupportBase
    {
        protected static int version;
        protected static String productName;
        protected IEnvironment environment;
        protected string sqlText;
        protected static string sqlFromFile(string file)
        {
            //string obj = ToolMobile.curDir();
            //obj = Path.Combine(obj, "config");
            //obj = Path.Combine(obj, "sql");
            //obj = Path.Combine(obj, file);
            //return ToolMobile.readFileText(obj);

            string sql= ToolMobile.getFsOrResourceText("config/sql", file);

            return sql;
 

        }
        public DBSupportBase(IEnvironment e, int ver, String prodName, String pSqlText)
        {
            environment = e;
            version = ver;
            productName = prodName;
         
            //


            pSqlText = pSqlText.Replace("$FIRM$", environment.getInfoApplication().firmId.ToString().PadLeft(3, '0'));
            pSqlText = pSqlText.Replace("$PERIOD$", environment.getInfoApplication().periodId.ToString().PadLeft(2, '0'));
            sqlText = pSqlText;


            //
            update();
        }
        protected virtual bool needUpdate()
        {
            Object res = SqlExecute.executeScalar(environment,SqlExecute.translate(getReplaceList(), environment.prepareSqlText(SQLTextCollection.NeedUpdate)) );
            if (ToolCell.isNull(res, string.Empty).ToString() == "1")
                return true;
            return false;
        }
        protected virtual void update()
        {
            if (needUpdate())
            {
                SqlExecute.executeBatch(SqlExecute.translate(getReplaceList(), environment.prepareSqlText(sqlText)), environment);
                setVersion();
            }
        }

        protected virtual void setVersion()
        {
            SqlExecute.executeBatch(SqlExecute.translate(getReplaceList(), environment.prepareSqlText(SQLTextCollection.SetVersion)), environment);
        }

        protected virtual List<string[]> getReplaceList()
        {
            List<string[]> list = new List<string[]>();
            list.Add(new String[] { SqlTextConstant.BUILD, version.ToString() });
            list.Add(new String[] { SqlTextConstant.PRODUCTNAME, productName });
            return list;
        }
    }
}
