using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Settings;
using AvaExt.ObjectSource;
using AvaExt.TableOperation.RowsSelector;

using AvaExt.SQL.Dynamic;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Table;
using AvaExt.PagedSource;
using System.Data;
using AvaExt.Reporting;
using AvaExt.MobControl.Reporting.XmlReport;
using AvaGE.MobControl.Reporting.Renders;

namespace AvaGE.MobControl.Reporting
{
    public class ReportFactory
    {
        const string attrSql = "sql";
        const string attrRep = "rep";
        const string attrVars = "vars";
        const string attrName = "name";
         
        public static IActivity createActivity(IEnvironment pEnv, ISettings pSettings, string pName, IRowSource pRowSource)
        {
            IReport rep = createReport(pEnv, pSettings, pName, pRowSource);

            IReportRender render = new MobFormShowDataRender(pEnv);
            render.setReport(rep);
            return render;

        }

        public static IReport createReport(IEnvironment pEnv, ISettings pSettings, string pName, IRowSource pRowSource)
        {
            string sqlNode = pSettings.getStringAttr(pName, attrSql);
            string repNode = pSettings.getStringAttr(pName, attrRep);
            if (sqlNode != string.Empty && repNode != string.Empty)
            {
                string sqlText = pSettings.getString(sqlNode);
                string sqlVars = pSettings.getStringAttr(sqlNode, attrVars);
                if (sqlVars == string.Empty)
                    sqlVars = ToolString.joinList(new string[] { "@lref", TableDUMMY.LOGICALREF });
                string sqlName = pSettings.getStringAttr(sqlNode, attrName);
                ISqlBuilder builder = new ImplSqlBuilder(pEnv, sqlText, sqlName);
                IDictionary<string, string> dic = ToolString.explodeForParameters(sqlVars);
                IEnumerator<string> enumer = dic.Keys.GetEnumerator();
                enumer.Reset();
                while (enumer.MoveNext())
                {
                    builder.addPereparer(
                        new SqlBuilderPreparerObjectSourceFreePar(
                        enumer.Current,
                        new ImplObjectSourceRowCell(pRowSource, dic[enumer.Current])
                        )
                        );
                }
                IPagedSource ps = new ImplPagedSource(pEnv, builder);



                string repObj = pSettings.getString(repNode);
                IReport xmlRep = new ImplXmlReport(repObj, pEnv);

                xmlRep.setDataSource(ps);


                return xmlRep;
            }
            return null;

        }
    }
}
