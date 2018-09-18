using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowsSelector;
using System.Data;
using AvaExt.Adapter.ForUser;
using AvaExt.TableOperation;

namespace AvaExt.ObjectSource
{
    public class ImplObjectSourceAdapterRow : IRowSource
    {
        IAdapterUser adapter;
        string table;
        public ImplObjectSourceAdapterRow(IAdapterUser pAdapter, string pTable)
        {
            adapter = pAdapter;
             table = pTable;
        }

        public void setAdapter(IAdapterUser pAdapter)
        { 
            adapter = pAdapter;
        }
        public DataRow get()
        {
            return ToolRow.getLastRealRow(adapter.getDataSet().Tables[table]);
        }


    }
}
