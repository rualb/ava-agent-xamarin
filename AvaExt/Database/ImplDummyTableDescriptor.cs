using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common;

namespace AvaExt.Database
{
    public class ImplDummyTableDescriptor : ImplTableDescriptor
    {
        class _ColumnDescriptor
        {
            public string name;
            public int size;
            public Type type;


            public _ColumnDescriptor(string pName, int pSize, Type pType)
            {

                name = pName;
                size = pSize;
                type = pType;

            }
            public _ColumnDescriptor(string pName, Type pType)
            {

                name = pName;
                size = getSize(pType);
                type = pType;

            }

            int getSize(Type pType)
            {
                if (ToolType.isNumber(pType))
                    return floatSize;
                else
                    if (ToolType.isString(pType))
                        return stringSize;
                    else
                        if (ToolType.isDateTime(pType))
                            return dateSize;
                return 0;
            }

        }
        const int floatSize = 8;
        const int dateSize = 8;
        const int stringSize = 4000;

        public ImplDummyTableDescriptor()
            : base(TableDUMMY.TABLE, string.Empty, getColsName(), getColsSize(), getColsType())
        {

        }


        static _ColumnDescriptor[] getColsObj()
        {
            List<_ColumnDescriptor> list_ = new List<_ColumnDescriptor>();

            /////////////
            #region Declaration
            list_.Add(new _ColumnDescriptor(TableDUMMY.EXCHANGE, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.LANG, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.TYPE_, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.ACTIVE_, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.ACTIVE, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.ACTIVITY, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.BILLED, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PARENTID, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PARENTCODE, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PARENTNAME, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.PARENTREF, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.CHILDID, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CHILDREF, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.CHILDCODE, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.CHILDNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.ID, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.ID2, ToolTypeSet.helper.tLRef));
            list_.Add(new _ColumnDescriptor(TableDUMMY.BALANCE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.DEBIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CREDIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.AMOUNT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CONVFACT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CODE, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.NAME, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CANCELLED, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DUMMY, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CLIENTREF, ToolTypeSet.helper.tLRef));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TOTAL, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.PRICE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.PRICETYPE, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CURRENCY, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.RATE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CUR, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.CURCODE, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOTAL, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPPRICE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPRATE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPCUR, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.REPCURCODE, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOTAL, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANPRICE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANRATE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANCUR, ToolTypeSet.helper.tInt));
        //    list_.Add(new _ColumnDescriptor(TableDUMMY.TRCUR, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.TRANCURCODE, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.EXTTOTAL, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.EXTPRICE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.EXTRATE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.EXTCUR, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.EXTCURCODE, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CUSTTOTAL, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CUSTPRICE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CUSTRATE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CUSTCUR, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.CUSTCURCODE, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PRICE1, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PRICE2, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PRICE3, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PRICE4, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.PRICE5, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.UNITREF, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.STOCKREF, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.SOURCEINDEX, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DESTINDEX, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.SOURCEWHNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DESTWHNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.SOURCEFACKNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DESTFACKNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.SOURCEDIVNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DESTDIVNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.SOURCEDEPNAME, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DESTDEPNAME, ToolTypeSet.helper.tString));

            //list_.Add(new _ColumnDescriptor(TableDUMMY.DATA, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.FUNC_, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.ALIAS_, ToolTypeSet.helper.tString));

            list_.Add(new _ColumnDescriptor(TableDUMMY.QUANTITY, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.DISCOUNT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.VALUE, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.UNITLIST, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.UNITCF01, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.UNITCF02, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.UNITCF03, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.REST, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.RESTMAIN, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.FLAG, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.WEIGHT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.WIDTH, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.COMMENT, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.STATE, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.FLAGS, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.DATETIME, ToolTypeSet.helper.tDateTime));
            list_.Add(new _ColumnDescriptor(TableDUMMY.FIRM, ToolTypeSet.helper.tInt));
          //  list_.Add(new _ColumnDescriptor(TableDUMMY.PERIOD, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DOCNO, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DOCNOHIDDEN, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DOCID, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.CLIENT, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.ITEM, ToolTypeSet.helper.tLRef));
            list_.Add(new _ColumnDescriptor(TableDUMMY.LOCDEBIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.LOCCREDIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.LOCBALANCE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPDEBIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPCREDIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPBALANCE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANDEBIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANCREDIT, ToolTypeSet.helper.tFloat));
        //    list_.Add(new _ColumnDescriptor(TableDUMMY.TRDEBIT, ToolTypeSet.helper.tFloat));
         //   list_.Add(new _ColumnDescriptor(TableDUMMY.TRCREDIT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANBALANCE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.LOGICALREF, ToolTypeSet.helper.tLRef));
        //    list_.Add(new _ColumnDescriptor(TableDUMMY.LREF, ToolTypeSet.helper.tLRef));
            list_.Add(new _ColumnDescriptor(TableDUMMY.VOLUME_, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.USED, ToolTypeSet.helper.tInt));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.RELATIONMATH, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.RELATIONBOOL, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.SORT, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.FILENAME, ToolTypeSet.helper.tString));
            list_.Add(new _ColumnDescriptor(TableDUMMY.FILEDESC, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.UNIT, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.BARCODE, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.BARCODE2, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.BARCODE3, ToolTypeSet.helper.tString));

            //list_.Add(new _ColumnDescriptor(TableDUMMY.DYNEXPRESSION, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DYNEXPRESSION2, ToolTypeSet.helper.tString));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.DYNEXPRESSION3, ToolTypeSet.helper.tString));
            //
         //   list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_EXTCREATEDDATE, ToolTypeSet.helper.tDateTime));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_CREADEDDATE, ToolTypeSet.helper.tDateTime));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_CREATEDBY, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_CREATEDHOUR, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_CREATEDMIN, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_CREATEDSEC, ToolTypeSet.helper.tInt));

            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_MODIFIEDBY, ToolTypeSet.helper.tInt));
          //  list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_EXTMODIFIEDDATE, ToolTypeSet.helper.tDateTime));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_MODIFIEDDATE, ToolTypeSet.helper.tDateTime));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_MODIFIEDHOUR, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_MODIFIEDMIN, ToolTypeSet.helper.tInt));
            list_.Add(new _ColumnDescriptor(TableDUMMY.CAPIBLOCK_MODIFIEDSEC, ToolTypeSet.helper.tInt));
            //
            //list_.Add(new _ColumnDescriptor(TableDUMMY.STOCKFIS, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.STOCKREAL, ToolTypeSet.helper.tFloat));

            list_.Add(new _ColumnDescriptor(TableDUMMY.TOTSURCHARGE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TOTDISCOUNT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TOT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TOTTAX, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TOTVAT, ToolTypeSet.helper.tFloat));
         //   list_.Add(new _ColumnDescriptor(TableDUMMY.TOTADDTAX, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TOTNET, ToolTypeSet.helper.tFloat));

            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOTSURCHARGE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOTDISCOUNT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOTTAX, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOTVAT, ToolTypeSet.helper.tFloat));
      //      list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOTADDTAX, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.TRANTOTNET, ToolTypeSet.helper.tFloat));

            list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOTSURCHARGE, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOTDISCOUNT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOTTAX, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOTVAT, ToolTypeSet.helper.tFloat));
      //      list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOTADDTAX, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.REPTOTNET, ToolTypeSet.helper.tFloat));


            //list_.Add(new _ColumnDescriptor(TableDUMMY.REPDISTTOTAL, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.REPLINENET, ToolTypeSet.helper.tFloat));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.LINENET, ToolTypeSet.helper.tFloat));

            list_.Add(new _ColumnDescriptor(TableDUMMY.GROSSWEIGHT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.GROSSVOLUME, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.AREA, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.HEIGHT, ToolTypeSet.helper.tFloat));
            list_.Add(new _ColumnDescriptor(TableDUMMY.LENGTH, ToolTypeSet.helper.tFloat));


            list_.Add(new _ColumnDescriptor(TableDUMMY.PATTERN, ToolTypeSet.helper.tString));

            //list_.Add(new _ColumnDescriptor(TableDUMMY.ACCFICHEREF, ToolTypeSet.helper.tLRef));
            //list_.Add(new _ColumnDescriptor(TableDUMMY.ACCOUNTED, ToolTypeSet.helper.tInt));

            list_.Add(new _ColumnDescriptor(TableDUMMY.DATE_, ToolTypeSet.helper.tDateTime));


            #endregion

            //////////////

            return list_.ToArray();
        }
        static string[] getColsName()
        {
            List<string> list_ = new List<string>();
            foreach (_ColumnDescriptor desc_ in getColsObj())
                list_.Add(desc_.name);

            return list_.ToArray();
        }
        static int[] getColsSize()
        {
            List<int> list_ = new List<int>();
            foreach (_ColumnDescriptor desc_ in getColsObj())
                list_.Add(desc_.size);

            return list_.ToArray();
        }
        static Type[] getColsType()
        {
            List<Type> list_ = new List<Type>();
            foreach (_ColumnDescriptor desc_ in getColsObj())
                list_.Add(desc_.type);

            return list_.ToArray();
        }
    }


}
