using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.DataRefernce
{
    public interface IDataReferenceHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns>is adding finised ok</returns>
        bool addData(DataRow row);
        void clear();
    }
}
