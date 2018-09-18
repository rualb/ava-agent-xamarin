using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Settings;
using AvaExt.SQL.Dynamic;
using System.Data;
using System.IO;
using AvaExt.Filter;
using AvaExt.PagedSource;
using AvaExt.SQL;
using AvaExt.MobControl.Reporting.XmlReport;
using AvaExt.TableOperation;

namespace AvaExt.Reporting
{

    public class ImplReportSource : IReportSource
    {
        string[] _sqlQueryes;
        DataSet _values = new DataSet("VALUES");
        DataSet _result = new DataSet("DS");
        List<IReport> _reports;
        List<string> _reportsNames;
        List<IFilter> _filters = new List<IFilter>();
        string[] _aliases;

        IEnvironment _environment;
        const string _fileInfo = "info.xml";
        const string _varFileDS = "ds";
        const string _varFileFilter = "filter";
        const string _varListRender = "render";
        const string _varListFilterData = "filterData";
        const string _varListAlias = "alias";

        const string _attrFType = "fType";
        const string _attrRef = "ref";
        const string _attrName = "name";
        const string _attrRefColsVal = "refColsVal";
        const string _attrRefColsShow = "refColsShow";
        const string _attrRefColsShowDesc = "refColsShowDesc";
        const string _attrSqlPlace = "sqlPlace";
        const string _attrSqlTable = "sqlTable";
        const string _attrSqlCols = "sqlCols";
        const string _attrSqlParams = "sqlParams";
        const string _attrSqlParam = "sqlParam";
        const string _attrSqlParamType = "type";
        const string _attrSqlParamValue = "value";

        const string _attrRequire = "require";
        const string _attrMulti = "multi";

        public ImplReportSource(IEnvironment pEnv, string pLocation)
        {
            //if (!Path.IsPathRooted(pLocation))
            //    pLocation = Path.Combine(ToolMobile.curDir(), pLocation);

            pLocation = ToolMobile.getFullPath(pLocation);
            _environment = pEnv;

            ISettings sInfo = new SettingsFromFileExt(pLocation, _fileInfo, _environment);
            initRenders(pLocation, sInfo);
            //var fileContent = ToolMobile.readFileText(Path.Combine(pLocation, sInfo.getString(_varFileDS)));
            var fileContent = ToolMobile.getFsOrResourceText(pLocation, sInfo.getString(_varFileDS));

            _sqlQueryes = getSqlQueryes(fileContent);
            _aliases = ToolString.explodeList(sInfo.getString(_varListAlias, "TABLE"));
            string filtersFile = sInfo.getString(_varFileFilter, string.Empty);
            if (filtersFile != string.Empty)
                initFilters(new SettingsFromFileExt(pLocation, filtersFile, _environment));
            else
                initFilters(null);


        }

        private void initRenders(string pLocation, ISettings sInfoFilters)
        {
            _reports = new List<IReport>();
            _reportsNames = new List<string>();
            string[][] repData = ToolString.explodeGroupList(sInfoFilters.getString(_varListRender));
            foreach (string[] grp in repData)
                if (grp.Length == 2)
                {
                    _reportsNames.Add(_environment.translate(grp[0]));
                    _reports.Add(new ImplXmlReport(Path.Combine(pLocation, grp[1]), _environment));

                }
        }


        void initFilters(ISettings sInfoFilters)
        {
            _filters.Clear();
            if (sInfoFilters != null)
            {
                string[] filters = sInfoFilters.getAllSettings();
                foreach (string filter in filters)
                {
                    FilterInfo fInfo = new FilterInfo();
                    //
                    fInfo.type = sInfoFilters.getStringAttr(filter, _attrFType);
                    fInfo.code = filter;
                    fInfo.desc = _environment.translate(sInfoFilters.getStringAttr(filter, _attrName));
                    fInfo.dataReference = _environment.getRefFactory().get(sInfoFilters.getStringAttr(filter, _attrRef));
                    fInfo.valueColumns = ToolString.explodeList(sInfoFilters.getStringAttr(filter, _attrRefColsVal));
                    fInfo.showColumns = ToolString.explodeList(sInfoFilters.getStringAttr(filter, _attrRefColsShow));
                    fInfo.showColumnsDesc = _environment.translate(ToolString.explodeList(sInfoFilters.getStringAttr(filter, _attrRefColsShowDesc)));

                    fInfo.sqlPlaces = ToolString.explodeList(sInfoFilters.getStringAttr(filter, _attrSqlPlace));
                    fInfo.sqlTables = ToolString.explodeList(sInfoFilters.getStringAttr(filter, _attrSqlTable));
                    fInfo.sqlColumns = ToolString.explodeGroupList(sInfoFilters.getStringAttr(filter, _attrSqlCols));
                    fInfo.sqlParameters = ToolString.explodeList(sInfoFilters.getStringAttr(filter, _attrSqlParams)); ;
                    fInfo.sqlParameter = sInfoFilters.getStringAttr(filter, _attrSqlParam);
                    Type type = ToolType.parse(sInfoFilters.getStringAttr(filter, _attrSqlParamType));
                    fInfo.sqlParameterValue = sInfoFilters.getAttr(filter, _attrSqlParamValue, type, DBNull.Value);

                    fInfo.flagRequire = sInfoFilters.getBoolAttr(filter, _attrRequire);
                    fInfo.flagMulti = sInfoFilters.getBoolAttr(filter, _attrMulti);
                    //
                    _filters.Add(new ImplFilter(_environment, this, fInfo));
                }
            }
        }
        string[] getSqlQueryes(string sqlText)
        {
            List<string> listSql = new List<string>();
            StringReader strReader = new StringReader(sqlText);
            string sqlCmd = string.Empty;
            string buf = string.Empty;
            while ((buf = strReader.ReadLine()) != null)
            {
                buf = buf.Trim();
                if (buf != String.Empty)
                {
                    if (buf == SqlTextConstant.EXECUTE && sqlCmd != string.Empty)
                    {
                        listSql.Add(sqlCmd);
                        sqlCmd = string.Empty;
                    }
                    else
                    {
                        sqlCmd += "\r\n" + buf;
                    }
                }
            }
            if (sqlCmd != string.Empty)
            {
                listSql.Add(sqlCmd);
                sqlCmd = string.Empty;
            }
            return listSql.ToArray();
        }
        public DataSet get()
        {
            //
            _result.Clear();
            for (int i = 0; i < _sqlQueryes.Length; ++i)
            {
                IPagedSource source =
                    new ImplPagedSource(
                    _environment,
                    new ImplSqlBuilder(_environment, _sqlQueryes[i], (_aliases.Length > i ? _aliases[i] : string.Empty)));

                foreach (IFilter filter in _filters)
                    filter.applyToBuilder(source.getBuilder());

                _result.Tables.Add(source.getAll());
            }
            foreach (DataTable tab in _result.Tables)
                ToolTable.fillNull(tab);
            return _result.Copy();
        }

        public DataSet getFiltersValues()
        {
            return _values;
        }


        public void addFilter(IFilter pFilter)
        {
            _filters.Add(pFilter);
        }

        public IReport[] getReports()
        {
            return _reports.ToArray();
        }


        public void Dispose()
        {

        }

    }
}
