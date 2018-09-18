using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.DataExchange
{
    public interface IDataExchangeExt : IDataExchange 
    {
        IDictionary<string, string[]>   getExportDescriptor(); 
        void setExportDescriptor(IDictionary<string, string[]> pExpDesc);
        void setRowValidator(string pTable, IRowValidator pRowValidator); 
    }
}
