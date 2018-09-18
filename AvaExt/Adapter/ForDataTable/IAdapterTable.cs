using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.Const;
using System.Data;

namespace AvaExt.Adapter.ForDataTable
{
    public interface IAdapterTable:IDisposable
    {
        object get(object[] parArr ); //get data by parameters
        object set(object pObj); //update data with return result
        object getNewId(); //get new record id
        String getName(); // get table name
    }
}
