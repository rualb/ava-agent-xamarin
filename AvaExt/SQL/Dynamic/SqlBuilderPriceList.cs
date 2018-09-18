using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderPriceList : ImplSqlBuilder
    {
        public SqlBuilderPriceList(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderPriceList, TablePRCLIST.TABLE)
        {
            addColumnToMeta(TablePRCLIST.LOGICALREF, typeof(int));
            addColumnToMeta(TablePRCLIST.CARDREF, typeof(int));
            addColumnToMeta(TablePRCLIST.ACTIVE, typeof(short));
            addColumnToMeta(TablePRCLIST.PTYPE, typeof(short));
            addColumnToMeta(TablePRCLIST.CODE, typeof(string));
            addColumnToMeta(TablePRCLIST.DEFINITION_, typeof(string));
            addColumnToMeta(TablePRCLIST.BEGDATE, typeof(DateTime));
            addColumnToMeta(TablePRCLIST.ENDDATE, typeof(DateTime));
            addColumnToMeta(TablePRCLIST.BEGTIME, typeof(int));
            addColumnToMeta(TablePRCLIST.ENDTIME, typeof(int));
            addColumnToMeta(TablePRCLIST.CLIENTCODE, typeof(string));
            addColumnToMeta(TablePRCLIST.CLSPECODE, typeof(string));
            addColumnToMeta(TablePRCLIST.CYPHCODE, typeof(string));
            addColumnToMeta(TablePRCLIST.TRADINGGRP, typeof(string));
            addColumnToMeta(TablePRCLIST.PRICE, typeof(double));
            addColumnToMeta(TablePRCLIST.UOMREF, typeof(int));
        }


    }
}
