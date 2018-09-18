using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderClient : ImplSqlBuilder
    {
        public SqlBuilderClient(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderClient, TableCLCARD.TABLE)
        {
            //addColumnToMeta(TableCLCARD.LOGICALREF, typeof(string));
            //addColumnToMeta(TableCLCARD.ACTIVE, typeof(short));
            //addColumnToMeta(TableCLCARD.CARDTYPE, typeof(short));
            //addColumnToMeta(TableCLCARD.CODE, typeof(string));
            //addColumnToMeta(TableCLCARD.DEFINITION_, typeof(string));
            //addColumnToMeta(TableCLCARD.SPECODE, typeof(string));
            //addColumnToMeta(TableCLCARD.CYPHCODE, typeof(string));
            //addColumnToMeta(TableCLCARD.DELIVERYFIRM, typeof(string));
            //addColumnToMeta(TableCLCARD.TRADINGGRP, typeof(string));
            //addColumnToMeta(TableCLCARD.DISCPER, typeof(double));
            //addColumnToMeta(TableCLCARD.PRCLIST, typeof(short));
            //addColumnToMeta(TableCLCARD.BARCODE, typeof(string));
        }


    }
}
