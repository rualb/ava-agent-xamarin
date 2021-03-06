using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Manual.Table
{
    public class TableCLCARD
    {
        public const String TABLE_RECORD_ID =  "LG_$FIRM$_CLCARDSEQ";
        public const String TABLE = "CLCARD";
        public const String TABLE_FULL_NAME = "LG_$FIRM$_CLCARD";
        public const String LOGICALREF = "LOGICALREF";
        public const String ACTIVE = "ACTIVE";
        public const String CARDTYPE = "CARDTYPE";
        public const String CODE = "CODE";
        public const String DEFINITION_ = "DEFINITION_";
        public const String SPECODE = "SPECODE";
        public const String CYPHCODE = "CYPHCODE";
        public const String ADDR1 = "ADDR1";
        public const String ADDR2 = "ADDR2";
        public const String CITY = "CITY";
        public const String COUNTRY = "COUNTRY";
        public const String POSTCODE = "POSTCODE";
        public const String TELNRS1 = "TELNRS1";
        public const String TELNRS2 = "TELNRS2";
        public const String FAXNR = "FAXNR";
        public const String TAXNR = "TAXNR";
        public const String TAXOFFICE = "TAXOFFICE";
        public const String INCHARGE = "INCHARGE";
        public const String DISCRATE = "DISCRATE";
        public const String EXTENREF = "EXTENREF";
        public const String PAYMENTREF = "PAYMENTREF";
        public const String EMAILADDR = "EMAILADDR";
        public const String WEBADDR = "WEBADDR";
        public const String WARNMETHOD = "WARNMETHOD";
        public const String WARNEMAILADDR = "WARNEMAILADDR";
        public const String WARNFAXNR = "WARNFAXNR";
        public const String CLANGUAGE = "CLANGUAGE";
        public const String VATNR = "VATNR";
        public const String BLOCKED = "BLOCKED";
        public const String BANKBRANCHS1 = "BANKBRANCHS1";
        public const String BANKBRANCHS2 = "BANKBRANCHS2";
        public const String BANKBRANCHS3 = "BANKBRANCHS3";
        public const String BANKBRANCHS4 = "BANKBRANCHS4";
        public const String BANKBRANCHS5 = "BANKBRANCHS5";
        public const String BANKBRANCHS6 = "BANKBRANCHS6";
        public const String BANKBRANCHS7 = "BANKBRANCHS7";
        public const String BANKACCOUNTS1 = "BANKACCOUNTS1";
        public const String BANKACCOUNTS2 = "BANKACCOUNTS2";
        public const String BANKACCOUNTS3 = "BANKACCOUNTS3";
        public const String BANKACCOUNTS4 = "BANKACCOUNTS4";
        public const String BANKACCOUNTS5 = "BANKACCOUNTS5";
        public const String BANKACCOUNTS6 = "BANKACCOUNTS6";
        public const String BANKACCOUNTS7 = "BANKACCOUNTS7";
        public const String DELIVERYMETHOD = "DELIVERYMETHOD";
        public const String DELIVERYFIRM = "DELIVERYFIRM";
        public const String CCURRENCY = "CCURRENCY";
        public const String TEXTINC = "TEXTINC";
        public const String SITEID = "SITEID";
        public const String RECSTATUS = "RECSTATUS";
        public const String ORGLOGICREF = "ORGLOGICREF";
        public const String EDINO = "EDINO";
        public const String TRADINGGRP = "TRADINGGRP";
        //public const String CAPIBLOCK_CREATEDBY = "CAPIBLOCK_CREATEDBY";
        //public const String CAPIBLOCK_CREADEDDATE = "CAPIBLOCK_CREADEDDATE";
        //public const String CAPIBLOCK_CREATEDHOUR = "CAPIBLOCK_CREATEDHOUR";
        //public const String CAPIBLOCK_CREATEDMIN = "CAPIBLOCK_CREATEDMIN";
        //public const String CAPIBLOCK_CREATEDSEC = "CAPIBLOCK_CREATEDSEC";
        //public const String CAPIBLOCK_MODIFIEDBY = "CAPIBLOCK_MODIFIEDBY";
        //public const String CAPIBLOCK_MODIFIEDDATE = "CAPIBLOCK_MODIFIEDDATE";
        //public const String CAPIBLOCK_MODIFIEDHOUR = "CAPIBLOCK_MODIFIEDHOUR";
        //public const String CAPIBLOCK_MODIFIEDMIN = "CAPIBLOCK_MODIFIEDMIN";
        //public const String CAPIBLOCK_MODIFIEDSEC = "CAPIBLOCK_MODIFIEDSEC";
        public const String PAYMENTPROC = "PAYMENTPROC";
        public const String CRATEDIFFPROC = "CRATEDIFFPROC";
        public const String WFSTATUS = "WFSTATUS";
        public const String PPGROUPCODE = "PPGROUPCODE";
        public const String PPGROUPREF = "PPGROUPREF";
        public const String TAXOFFCODE = "TAXOFFCODE";
        public const String TOWNCODE = "TOWNCODE";
        public const String TOWN = "TOWN";
        public const String DISTRICTCODE = "DISTRICTCODE";
        public const String DISTRICT = "DISTRICT";
        public const String CITYCODE = "CITYCODE";
        public const String COUNTRYCODE = "COUNTRYCODE";
        public const String ORDSENDMETHOD = "ORDSENDMETHOD";
        public const String ORDSENDEMAILADDR = "ORDSENDEMAILADDR";
        public const String ORDSENDFAXNR = "ORDSENDFAXNR";
        public const String DSPSENDMETHOD = "DSPSENDMETHOD";
        public const String DSPSENDEMAILADDR = "DSPSENDEMAILADDR";
        public const String DSPSENDFAXNR = "DSPSENDFAXNR";
        public const String INVSENDMETHOD = "INVSENDMETHOD";
        public const String INVSENDEMAILADDR = "INVSENDEMAILADDR";
        public const String INVSENDFAXNR = "INVSENDFAXNR";
        public const String SUBSCRIBERSTAT = "SUBSCRIBERSTAT";
        public const String SUBSCRIBEREXT = "SUBSCRIBEREXT";
        public const String AUTOPAIDBANK = "AUTOPAIDBANK";
        public const String PAYMENTTYPE = "PAYMENTTYPE";
        public const String LASTSENDREMLEV = "LASTSENDREMLEV";
        public const String EXTACCESSFLAGS = "EXTACCESSFLAGS";
        public const String ORDSENDFORMAT = "ORDSENDFORMAT";
        public const String DSPSENDFORMAT = "DSPSENDFORMAT";
        public const String INVSENDFORMAT = "INVSENDFORMAT";
        public const String REMSENDFORMAT = "REMSENDFORMAT";
        public const String STORECREDITCARDNO = "STORECREDITCARDNO";
        public const String CLORDFREQ = "CLORDFREQ";
        public const String ORDDAY = "ORDDAY";
        public const String LOGOID = "LOGOID";
        public const String LIDCONFIRMED = "LIDCONFIRMED";
        public const String EXPREGNO = "EXPREGNO";
        public const String EXPDOCNO = "EXPDOCNO";
        public const String EXPBUSTYPREF = "EXPBUSTYPREF";
        public const String INVPRINTCNT = "INVPRINTCNT";
        public const String PIECEORDINFLICT = "PIECEORDINFLICT";
        public const String COLLECTINVOICING = "COLLECTINVOICING";
        public const String EBUSDATASENDTYPE = "EBUSDATASENDTYPE";
        public const String INISTATUSFLAGS = "INISTATUSFLAGS";
        public const String SLSORDERSTATUS = "SLSORDERSTATUS";
        public const String SLSORDERPRICE = "SLSORDERPRICE";
        public const String LTRSENDMETHOD = "LTRSENDMETHOD";
        public const String LTRSENDEMAILADDR = "LTRSENDEMAILADDR";
        public const String LTRSENDFAXNR = "LTRSENDFAXNR";
        public const String LTRSENDFORMAT = "LTRSENDFORMAT";
        public const String IMAGEINC = "IMAGEINC";
        public const String CELLPHONE = "CELLPHONE";
        public const String SAMEITEMCODEUSE = "SAMEITEMCODEUSE";
        public const String STATECODE = "STATECODE";
        public const String STATENAME = "STATENAME";
        public const String WFLOWCRDREF = "WFLOWCRDREF";
        public const String PARENTCLREF = "PARENTCLREF";
        public const String LOWLEVELCODES1 = "LOWLEVELCODES1";
        public const String LOWLEVELCODES2 = "LOWLEVELCODES2";
        public const String LOWLEVELCODES3 = "LOWLEVELCODES3";
        public const String LOWLEVELCODES4 = "LOWLEVELCODES4";
        public const String LOWLEVELCODES5 = "LOWLEVELCODES5";
        public const String LOWLEVELCODES6 = "LOWLEVELCODES6";
        public const String LOWLEVELCODES7 = "LOWLEVELCODES7";
        public const String LOWLEVELCODES8 = "LOWLEVELCODES8";
        public const String LOWLEVELCODES9 = "LOWLEVELCODES9";
        public const String LOWLEVELCODES10 = "LOWLEVELCODES10";
        public const String TELCODES1 = "TELCODES1";
        public const String TELCODES2 = "TELCODES2";
        public const String FAXCODE = "FAXCODE";
        public const String PURCHBRWS = "PURCHBRWS";
        public const String SALESBRWS = "SALESBRWS";
        public const String IMPBRWS = "IMPBRWS";
        public const String EXPBRWS = "EXPBRWS";
        public const String FINBRWS = "FINBRWS";
        public const String ORGLOGOID = "ORGLOGOID";
        public const String PRCLIST = "PRCLIST";
        public const String DISCPER = "DISCPER";
        public const String BALANCE = "BALANCE";
        public const String BARCODE = "BARCODE";
    }
}
