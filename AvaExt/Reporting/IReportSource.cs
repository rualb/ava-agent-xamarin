using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Filter;

namespace AvaExt.Reporting
{
    public interface IReportSource:IDisposable
    {
        DataSet get();
        DataSet getFiltersValues();
        void addFilter(IFilter pFilter);
        IReport[] getReports();
    }
}
