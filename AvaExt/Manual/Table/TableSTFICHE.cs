using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation;

namespace AvaExt.Manual.Table
{
    public class TableINVOICE
    {
        public const String TABLE_RECORD_ID = "LG_$FIRM$_$PERIOD$_INVOICESEQ";
        public const String TABLE = "INVOICE";
 
        public const String TABLE_FULL_NAME = "LG_$FIRM$_$PERIOD$_INVOICE";
        public const String LOGICALREF = "LOGICALREF";
        public const String GRPCODE = "GRPCODE";
        public const String TRCODE = "TRCODE";
        public const String IOCODE = "IOCODE";
        public const String FICHENO = "FICHENO";

        public const String DATE_ = "DATE_";
        public const String FTIME = "FTIME";
        public const String DOCODE = "DOCODE";
        public const String INVNO = "INVNO";
        public const String SPECODE = "SPECODE";
        public const String CYPHCODE = "CYPHCODE";
        public const String INVOICEREF = "INVOICEREF";
        public const String CLIENTREF = "CLIENTREF";
        public const String RECVREF = "RECVREF";
        public const String ACCOUNTREF = "ACCOUNTREF";
        public const String CENTERREF = "CENTERREF";
        public const String PRODORDERREF = "PRODORDERREF";
        public const String PORDERFICHENO = "PORDERFICHENO";
        public const String SOURCETYPE = "SOURCETYPE";
        public const String SOURCEINDEX = "SOURCEINDEX";
        public const String SOURCEWSREF = "SOURCEWSREF";
        public const String SOURCEPOLNREF = "SOURCEPOLNREF";
        public const String SOURCECOSTGRP = "SOURCECOSTGRP";
        public const String DESTTYPE = "DESTTYPE";
        public const String DESTINDEX = "DESTINDEX";
        public const String DESTWSREF = "DESTWSREF";
        public const String DESTPOLNREF = "DESTPOLNREF";
        public const String DESTCOSTGRP = "DESTCOSTGRP";
        public const String FACTORYNR = "FACTORYNR";
        public const String BRANCH = "BRANCH";
        public const String DEPARTMENT = "DEPARTMENT";
        public const String COMPBRANCH = "COMPBRANCH";
        public const String COMPDEPARTMENT = "COMPDEPARTMENT";
        public const String COMPFACTORY = "COMPFACTORY";
        public const String PRODSTAT = "PRODSTAT";
        public const String DEVIR = "DEVIR";
        public const String CANCELLED = "CANCELLED";
        public const String BILLED = "BILLED";
        public const String ACCOUNTED = "ACCOUNTED";
        public const String UPDCURR = "UPDCURR";
        public const String INUSE = "INUSE";
        public const String INVKIND = "INVKIND";
        public const String ADDDISCOUNTS = "ADDDISCOUNTS";
        public const String TOTALDISCOUNTS = "TOTALDISCOUNTS";
        public const String TOTALDISCOUNTED = "TOTALDISCOUNTED";
        public const String ADDEXPENSES = "ADDEXPENSES";
        public const String TOTALEXPENSES = "TOTALEXPENSES";
        public const String TOTALDEPOZITO = "TOTALDEPOZITO";
        public const String TOTALPROMOTIONS = "TOTALPROMOTIONS";
        public const String TOTALVAT = "TOTALVAT";
        public const String GROSSTOTAL = "GROSSTOTAL";
        public const String NETTOTAL = "NETTOTAL";
        public const String DISCPER = "DISCPER";
        public const String GENEXP1 = "GENEXP1";
        public const String GENEXP2 = "GENEXP2";
        public const String GENEXP3 = "GENEXP3";
        public const String GENEXP4 = "GENEXP4";
        public const String REPORTRATE = "REPORTRATE";
        public const String REPORTNET = "REPORTNET";
        public const String EXTENREF = "EXTENREF";
        public const String PAYDEFREF = "PAYDEFREF";
        public const String PRINTCNT = "PRINTCNT";
        public const String FICHECNT = "FICHECNT";
        public const String ACCFICHEREF = "ACCFICHEREF";
        public const String CAPIBLOCK_CREATEDBY = "CAPIBLOCK_CREATEDBY";
        public const String CAPIBLOCK_CREADEDDATE = "CAPIBLOCK_CREADEDDATE";
        public const String CAPIBLOCK_CREATEDHOUR = "CAPIBLOCK_CREATEDHOUR";
        public const String CAPIBLOCK_CREATEDMIN = "CAPIBLOCK_CREATEDMIN";
        public const String CAPIBLOCK_CREATEDSEC = "CAPIBLOCK_CREATEDSEC";
        public const String CAPIBLOCK_MODIFIEDBY = "CAPIBLOCK_MODIFIEDBY";
        public const String CAPIBLOCK_MODIFIEDDATE = "CAPIBLOCK_MODIFIEDDATE";
        public const String CAPIBLOCK_MODIFIEDHOUR = "CAPIBLOCK_MODIFIEDHOUR";
        public const String CAPIBLOCK_MODIFIEDMIN = "CAPIBLOCK_MODIFIEDMIN";
        public const String CAPIBLOCK_MODIFIEDSEC = "CAPIBLOCK_MODIFIEDSEC";
        public const String SALESMANREF = "SALESMANREF";
        public const String CANCELLEDACC = "CANCELLEDACC";
        public const String SHPTYPCOD = "SHPTYPCOD";
        public const String SHPAGNCOD = "SHPAGNCOD";
        public const String TRACKNR = "TRACKNR";
        public const String GENEXCTYP = "GENEXCTYP";
        public const String LINEEXCTYP = "LINEEXCTYP";
        public const String TRADINGGRP = "TRADINGGRP";
        public const String TEXTINC = "TEXTINC";
        public const String SITEID = "SITEID";
        public const String RECSTATUS = "RECSTATUS";
        public const String ORGLOGICREF = "ORGLOGICREF";
        public const String WFSTATUS = "WFSTATUS";
        public const String SHIPINFOREF = "SHIPINFOREF";
        public const String DISTORDERREF = "DISTORDERREF";
        public const String SENDCNT = "SENDCNT";
        public const String DLVCLIENT = "DLVCLIENT";
        public const String DOCTRACKINGNR = "DOCTRACKINGNR";
        public const String ADDTAXCALC = "ADDTAXCALC";
        public const String TOTALADDTAX = "TOTALADDTAX";
        public const String UGIRTRACKINGNO = "UGIRTRACKINGNO";
        public const String QPRODFCREF = "QPRODFCREF";
        public const String VAACCREF = "VAACCREF";
        public const String VACENTERREF = "VACENTERREF";
        public const String ORGLOGOID = "ORGLOGOID";
        public const String FROMEXIM = "FROMEXIM";
        public const String FRGTYPCOD = "FRGTYPCOD";
        public const String TRCURR = "TRCURR";
        public const String TRRATE = "TRRATE";
        public const String TRNET = "TRNET";
        public const String EXIMWHFCREF = "EXIMWHFCREF";
        public const String EXIMFCTYPE = "EXIMFCTYPE";
        public const String MAINSTFCREF = "MAINSTFCREF";
        public const String FROMORDWITHPAY = "FROMORDWITHPAY";
        public const String PROJECTREF = "PROJECTREF";
        public const String WFLOWCRDREF = "WFLOWCRDREF";
        public const String STATUS = "STATUS";
        public const String UPDTRCURR = "UPDTRCURR";
        public const String TOTALEXADDTAX = "TOTALEXADDTAX";
        public const String PRCLIST = "PRCLIST";

        //////////////////////////////////////////////////   
 
        public static readonly String E_CLCARD__CODE = ToolColumn.getColumnFullName(TableCLCARD.TABLE, TableCLCARD.CODE);
        public static readonly String E_CLCARD__DEFINITION_ = ToolColumn.getColumnFullName(TableCLCARD.TABLE, TableCLCARD.DEFINITION_);
        public static readonly String E_DUMMY__DATETIME = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.DATETIME);

        public static readonly String E_DUMMY__SOURCEWHNAME = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.SOURCEWHNAME);
 
        public static readonly String E_DUMMY__RATE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.RATE);
   
        public static readonly String E_DUMMY__REPRATE = REPORTRATE;
      

        


        public static readonly String E_DUMMY__TOTSURCHARGE = TableINVOICE.TOTALEXPENSES;
        public static readonly String E_DUMMY__TOTDISCOUNT = TableINVOICE.TOTALDISCOUNTS;
        public static readonly String E_DUMMY__TOT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TOT);
        public static readonly String E_DUMMY__TOTNET = TableINVOICE.NETTOTAL;

        public static readonly String E_DUMMY__REPTOTSURCHARGE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOTSURCHARGE);
        public static readonly String E_DUMMY__REPTOTDISCOUNT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOTDISCOUNT);
        public static readonly String E_DUMMY__REPTOT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOT);
        public static readonly String E_DUMMY__REPTOTNET = TableINVOICE.REPORTNET;

 
    }
}
