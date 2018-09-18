using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Reporting;

namespace AvaExt.Reporting
{
    public interface IReportRender : IActivity, IDisposable
    {
        void setReport(IReport pReport);
    }
}
