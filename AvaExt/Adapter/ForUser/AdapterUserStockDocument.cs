using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Database.Const;
using AvaExt.Common.Const;
using AvaExt.Translating.Tools;
using AvaExt.Manual.Table;

namespace AvaExt.Adapter.ForUser
{
    public class AdapterUserStockDocument : AdapterUserDocument
    {


         protected virtual string getHeaderName()
        {
            return TableINVOICE.TABLE;
        }
         protected virtual string getHeaderNameLong()
        {
            return TableINVOICE.TABLE_FULL_NAME;
        }
         protected virtual string getHeaderNameSeq()
        {
            return TableINVOICE.TABLE_RECORD_ID;
        }
         protected virtual string getLinesName()
        {
            return TableSTLINE.TABLE;
        }
         protected virtual string getLinesNameLong()
        {
            return TableSTLINE.TABLE_LONG;
        }
         protected virtual string getLinesNameSeq()
        {
            return TableSTLINE.TABLE_RECORD_ID;
        }


        protected ConstMaterialModuleType innerVarMaterialModuleType = ConstMaterialModuleType.undef;
        protected ConstPriceType innerVarPriceListType = ConstPriceType.undef;


        public AdapterUserStockDocument(IEnvironment pEnv, IAdapterDataSet pAdapter, String pHeaderTableName, String pLinesTableName)
            : base(pEnv, pAdapter, pHeaderTableName, pLinesTableName)
        {

        }

        public virtual void setLineStock(object pStockRef)
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLineUnit(object pUnit)
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual int getLineUnit()
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLinePrice(object pPrice)
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual double getLinePrice()
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLineAmount(object pAmout)
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLineType(ConstLineType lineType)
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLineAsGlobal()
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLineAsLocal()
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLineDicountPercent(double discount)
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }
        public virtual void setLineTotal(double total)
        {
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_UNDEF);
        }



    }
}
