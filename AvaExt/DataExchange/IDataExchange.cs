using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.DataExchange
{
    public interface IDataExchange
    {
        string export(); 
        void export(XmlDocument pData);
        void import(string pData);
        void import(XmlDocument pData);
 
    }
}
