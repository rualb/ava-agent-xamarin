using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Xml.Serialization;

namespace AvaExt.InfoClass
{
    public class InfoApplication
    {
        public Int16 firmId = 0;
        public String userName = string.Empty;
        public String password = string.Empty;
        public Int16 periodId = 0;
        public String firmName = string.Empty;
        public String firm = string.Empty;
        public String period = string.Empty;
        public Int16 userId;
        public Int16 groupId;
        public Int16 roleId;
        public DateTime periodBeginDate;
        public DateTime periodEndDate;
        public Int16 periodCurrencyNativeId = TableCURRENCY.USD;
        public Int16 periodCurrencyReportId = TableCURRENCY.USD;
    }
}
