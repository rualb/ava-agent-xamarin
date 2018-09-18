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
        public const String LOGICALREF = "LOGICALREF";
        public const String GRPCODE = "GRPCODE";
        public const String TRCODE = "TRCODE";
        public const String FICHENO = "FICHENO";

        public const String DATE_ = "DATE_";
        public const String TIME_ = "TIME_";
        public const String DOCODE = "DOCODE";
        public const String SPECODE = "SPECODE";
        public const String CYPHCODE = "CYPHCODE";
        public const String CLIENTREF = "CLIENTREF";
        public const String RECVREF = "RECVREF";
        public const String CENTERREF = "CENTERREF";
        public const String ACCOUNTREF = "ACCOUNTREF";
        public const String SOURCEINDEX = "SOURCEINDEX";
        public const String SOURCECOSTGRP = "SOURCECOSTGRP";
        public const String CANCELLED = "CANCELLED";
        public const String ACCOUNTED = "ACCOUNTED";
        public const String PAIDINCASH = "PAIDINCASH";
        public const String FROMKASA = "FROMKASA";
        public const String ENTEGSET = "ENTEGSET";
        public const String VAT = "VAT";
        public const String ADDDISCOUNTS = "ADDDISCOUNTS";
        public const String TOTALDISCOUNTS = "TOTALDISCOUNTS";
        public const String TOTALDISCOUNTED = "TOTALDISCOUNTED";
        public const String ADDEXPENSES = "ADDEXPENSES";
        public const String TOTALEXPENSES = "TOTALEXPENSES";
        public const String DISTEXPENSE = "DISTEXPENSE";
        public const String TOTALDEPOZITO = "TOTALDEPOZITO";
        public const String TOTALPROMOTIONS = "TOTALPROMOTIONS";
        public const String VATINCGROSS = "VATINCGROSS";
        public const String TOTALVAT = "TOTALVAT";
        public const String GROSSTOTAL = "GROSSTOTAL";
        public const String NETTOTAL = "NETTOTAL";
        public const String GENEXP1 = "GENEXP1";
        public const String GENEXP2 = "GENEXP2";
        public const String GENEXP3 = "GENEXP3";
        public const String GENEXP4 = "GENEXP4";
        public const String INTERESTAPP = "INTERESTAPP";
        public const String TRCURR = "TRCURR";
        public const String TRRATE = "TRRATE";
        public const String TRNET = "TRNET";
        public const String REPORTRATE = "REPORTRATE";
        public const String REPORTNET = "REPORTNET";
        public const String ONLYONEPAYLINE = "ONLYONEPAYLINE";
        public const String KASTRANSREF = "KASTRANSREF";
        public const String PAYDEFREF = "PAYDEFREF";
        public const String PRINTCNT = "PRINTCNT";
        public const String GVATINC = "GVATINC";
        public const String BRANCH = "BRANCH";
        public const String DEPARTMENT = "DEPARTMENT";
        public const String ACCFICHEREF = "ACCFICHEREF";
        public const String ADDEXPACCREF = "ADDEXPACCREF";
        public const String ADDEXPCENTREF = "ADDEXPCENTREF";
        public const String DECPRDIFF = "DECPRDIFF";
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
        public const String FACTORYNR = "FACTORYNR";
        public const String WFSTATUS = "WFSTATUS";
        public const String SHIPINFOREF = "SHIPINFOREF";
        public const String DISTORDERREF = "DISTORDERREF";
        public const String SENDCNT = "SENDCNT";
        public const String DLVCLIENT = "DLVCLIENT";
        public const String COSTOFSALEFCREF = "COSTOFSALEFCREF";
        public const String OPSTAT = "OPSTAT";
        public const String DOCTRACKINGNR = "DOCTRACKINGNR";
        public const String TOTALADDTAX = "TOTALADDTAX";
        public const String PAYMENTTYPE = "PAYMENTTYPE";
        public const String INFIDX = "INFIDX";
        public const String ACCOUNTEDCNT = "ACCOUNTEDCNT";
        public const String ORGLOGOID = "ORGLOGOID";
        public const String FROMEXIM = "FROMEXIM";
        public const String FRGTYPCOD = "FRGTYPCOD";
        public const String EXIMFCTYPE = "EXIMFCTYPE";
        public const String FROMORDWITHPAY = "FROMORDWITHPAY";
        public const String PROJECTREF = "PROJECTREF";
        public const String WFLOWCRDREF = "WFLOWCRDREF";
        public const String STATUS = "STATUS";
        public const String DEDUCTIONPART1 = "DEDUCTIONPART1";
        public const String DEDUCTIONPART2 = "DEDUCTIONPART2";
        public const String TOTALEXADDTAX = "TOTALEXADDTAX";
        public const String EXACCOUNTED = "EXACCOUNTED";
        public const String BNTRANSREF = "BNTRANSREF";
        public const String FROMBANK = "FROMBANK";


        //////////////////////////////////////////////////  

        public static readonly String E_DUMMY__DOCNOHIDDEN = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.DOCNOHIDDEN);

        public static readonly String E_CLCARD__CODE = ToolColumn.getColumnFullName(TableCLCARD.TABLE, TableCLCARD.CODE);
        public static readonly String E_CLCARD__DEFINITION_ = ToolColumn.getColumnFullName(TableCLCARD.TABLE, TableCLCARD.DEFINITION_);
        public static readonly String E_DUMMY__DATETIME = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.DATETIME);
        public static readonly String E_DUMMY__SOURCEDIVNAME = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.SOURCEDIVNAME);
        public static readonly String E_DUMMY__SOURCEDEPNAME = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.SOURCEDEPNAME);
        public static readonly String E_DUMMY__SOURCEFACKNAME = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.SOURCEFACKNAME);
        public static readonly String E_DUMMY__SOURCEWHNAME = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.SOURCEWHNAME);

        public static readonly String E_DUMMY__RATE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.RATE);
        public static readonly String E_DUMMY__CUR = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUR);
        public static readonly String E_DUMMY__CURCODE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CURCODE);

        public static readonly String E_DUMMY__REPRATE = REPORTRATE;
        public static readonly String E_DUMMY__REPCUR = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPCUR);
        public static readonly String E_DUMMY__REPCURCODE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPCURCODE);
        public static readonly String E_DUMMY__TRANRATE = TRRATE;
        public static readonly String E_DUMMY__TRANCUR = TRCURR;
        public static readonly String E_DUMMY__TRANCURCODE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TRANCURCODE);
        public static readonly String E_DUMMY__CUSTRATE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTRATE);
        public static readonly String E_DUMMY__CUSTCUR = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTCUR);
        public static readonly String E_DUMMY__CUSTCURCODE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTCURCODE);




        public static readonly String E_DUMMY__TOTSURCHARGE = TableINVOICE.TOTALEXPENSES;
        public static readonly String E_DUMMY__TOTDISCOUNT = TableINVOICE.TOTALDISCOUNTS;
        public static readonly String E_DUMMY__TOT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TOT);
        public static readonly String E_DUMMY__TOTTAX = TableINVOICE.TOTALADDTAX;
        public static readonly String E_DUMMY__TOTVAT = TableINVOICE.TOTALVAT;
        public static readonly String E_DUMMY__TOTNET = TableINVOICE.NETTOTAL;

        public static readonly String E_DUMMY__REPTOTSURCHARGE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOTSURCHARGE);
        public static readonly String E_DUMMY__REPTOTDISCOUNT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOTDISCOUNT);
        public static readonly String E_DUMMY__REPTOT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOT);
        public static readonly String E_DUMMY__REPTOTTAX = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOTTAX);
        public static readonly String E_DUMMY__REPTOTVAT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.REPTOTVAT);
        public static readonly String E_DUMMY__REPTOTNET = TableINVOICE.REPORTNET;

        public static readonly String E_DUMMY__TRANTOTSURCHARGE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TRANTOTSURCHARGE);
        public static readonly String E_DUMMY__TRANTOTDISCOUNT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TRANTOTDISCOUNT);
        public static readonly String E_DUMMY__TRANTOT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TRANTOT);
        public static readonly String E_DUMMY__TRANTOTTAX = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TRANTOTTAX);
        public static readonly String E_DUMMY__TRANTOTVAT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.TRANTOTVAT);
        public static readonly String E_DUMMY__TRANTOTNET = TableINVOICE.TRNET;

        public static readonly String E_DUMMY__CUSTTOTSURCHARGE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTTOTSURCHARGE);
        public static readonly String E_DUMMY__CUSTTOTDISCOUNT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTTOTDISCOUNT);
        public static readonly String E_DUMMY__CUSTTOT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTTOT);
        public static readonly String E_DUMMY__CUSTTOTTAX = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTTOTTAX);
        public static readonly String E_DUMMY__CUSTTOTVAT = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTTOTVAT);
        public static readonly String E_DUMMY__CUSTTOTNET = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CUSTTOTNET);



    }
}
