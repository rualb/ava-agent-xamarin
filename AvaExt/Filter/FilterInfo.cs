using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.DataRefernce;

namespace AvaExt.Filter
{
    public class FilterInfo
    {
        public static FilterInfo getConstFilterInfo(string param, object val) 
        {
            FilterInfo fInfo = new FilterInfo();
            fInfo.type = fTypeConst;
            fInfo.sqlParameter = param;
            fInfo.sqlParameterValue = val;
            return fInfo;
        }
        public const string fTypeRef = "ref";
        public const string fTypeRefParam = "refParam";
        public const string fTypeConst = "const";
        public const string fTypeSysConst = "sysConst";

        public string type = null;
        public string code = null;
        public string desc = null;
        public IDataReference dataReference = null;
        public string[] valueColumns = null;
        public string[] showColumns = null;
        public string[] showColumnsDesc = null;
        public string[] sqlPlaces = null;
        public string[] sqlTables = null;
        public string[][] sqlColumns = null;
        public string[] sqlParameters = null;
        public string sqlParameter = null;
        public object sqlParameterValue = null;
        public bool flagRequire = false;
        public bool flagMulti = false;
    }
}
