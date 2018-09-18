using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Manual.Table;
using AvaExt.Adapter.Tools;
using AvaExt.MyException;

namespace AvaExt.Adapter.ForDataSet
{
    public class ImplAdapterDataSetStub : IAdapterDataSet
    {
        IAdapterDataSet adapter;
        IEnvironment environment;
        public ImplAdapterDataSetStub(IEnvironment pEnv, IAdapterDataSet pAdapter)
        {
            environment = pEnv;
            adapter = pAdapter;
        }

        public virtual object get(object pId)
        {
            return ReturnHandler.set(adapter.get(pId));
        }

        public virtual object set(object pData)
        {
            return ReturnHandler.set(adapter.set((DataSet)pData));
            //  return ReturnHandler.set(adapter.set(pData)); 
        }

        public virtual string getCode()
        {
            return adapter.getCode();
        }


    }
}
