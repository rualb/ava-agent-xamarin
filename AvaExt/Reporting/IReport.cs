using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace AvaExt.Reporting
{
    public interface  IReport
    {
        void refreshSource();
        void setDataSource(object pDataSource);
        DataSet getDataSet();
        object getResult();
        RenderingInfo getRenderingInfo();
    }
}
