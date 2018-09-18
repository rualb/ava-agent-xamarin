using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.PagedSource;
using System.IO;
using AvaExt.TableOperation;

namespace AvaExt.Reporting
{
    public class ImplReport : IReport
    {

        IPagedSource _sourcePs = null;
        DataSet _sourceDs = null;
        DataSet _data;
        public void refreshSource()
        {
            if (_sourceDs != null)
                _data = _sourceDs;
            else
                if (_sourcePs != null)
                {
                    _sourcePs.getBuilder().reset();
                    DataTable table = _sourcePs.getAll();
                    ToolTable.fillNull(table);
                    _data = new DataSet();
                    _data.Tables.Add(table);
                }
        }

        public void setDataSource(object pDataSource)
        {
            _sourcePs = null;
            _sourceDs = null;
            if (typeof(IPagedSource).IsAssignableFrom(pDataSource.GetType()))
                _sourcePs = (IPagedSource)pDataSource;
            else
                if (typeof(DataSet).IsAssignableFrom(pDataSource.GetType()))
                    _sourceDs = (DataSet)pDataSource;
                else
                    if (typeof(DataTable).IsAssignableFrom(pDataSource.GetType()))
                        (_sourceDs = new DataSet()).Tables.Add((DataTable)pDataSource);
        }



        public DataSet getDataSet()
        {
            if (_data == null)
                refreshSource();
            return _data.Copy();
        }



        public virtual object getResult() 
        {
            return null;
        }



 
        public virtual RenderingInfo getRenderingInfo()
        {
            return null;
        }

       
    }
}
