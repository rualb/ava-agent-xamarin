using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.SQL.Dynamic;
using AvaExt.Common;
using AvaExt.Reporting;
using AvaExt.Formating;
using System.Data;

namespace AvaExt.Filter
{
    public class ImplFilter : IFilter
    {
        IEnvironment _environment;
        IReportSource _repSource;
        FilterInfo _info;

        public ImplFilter(IEnvironment pEnv, IReportSource pRepSource, FilterInfo pInfo)
        {
            _environment = pEnv;
            _repSource = pRepSource;
            _info = pInfo;
            switch (_info.type)
            {
                case FilterInfo.fTypeRef:
                    break;
                case FilterInfo.fTypeRefParam:
                    break;
                case FilterInfo.fTypeConst:
                case FilterInfo.fTypeSysConst:
                    begin();
                    break;
            }
        }
        public void begin()
        {
            DataSet ds = _repSource.getFiltersValues();
            DataTable tab = null;
            if (ds.Tables.Contains(getCode()))
                tab = ds.Tables[getCode()];
            else
                ds.Tables.Add(tab = newTable());

            switch (_info.type)
            {
                case FilterInfo.fTypeRef:

                    break;
                case FilterInfo.fTypeRefParam:

                    break;
                case FilterInfo.fTypeConst:
                    tab.Clear();
                    tab.Rows.Add(new object[] { _info.sqlParameterValue });
                    break;
                case FilterInfo.fTypeSysConst:

                    break;
            }
        }

        private DataTable newTable()
        {
            DataTable tab = new DataTable(getCode());
            switch (_info.type)
            {
                case FilterInfo.fTypeRef:

                    break;
                case FilterInfo.fTypeRefParam:

                    break;
                case FilterInfo.fTypeConst:
                    tab.Columns.Add(_info.sqlParameter, _info.sqlParameterValue.GetType());
                    break;
                case FilterInfo.fTypeSysConst:

                    break;
            }
            return tab;
        }

        public void applyToBuilder(ISqlBuilder pBuilder)
        {

            switch (_info.type)
            {
                case FilterInfo.fTypeRef:

                    break;
                case FilterInfo.fTypeRefParam:

                    break;
                case FilterInfo.fTypeConst:
                    pBuilder.addFreeParameterValue(_info.sqlParameter, _info.sqlParameterValue);
                    break;
                case FilterInfo.fTypeSysConst:

                    break;
            }
        }


        public string getCode()
        {
            return _info.code;
        }

        public string getDescription()
        {
            return _info.desc;
        }

        bool isActivated()
        {
            return _repSource.getFiltersValues().Tables.Contains(getCode());
        }
    }
}
