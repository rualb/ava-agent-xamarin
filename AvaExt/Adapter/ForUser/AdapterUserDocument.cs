using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using AvaExt.Database.Tools;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser
{
    public class AdapterUserDocument : AdapterUserHeaderedRecords
    {
         
        protected double reportCurencyExchange;
        ConstUsedCur innerVarUsedCurrency = ConstUsedCur.national;
        public AdapterUserDocument(IEnvironment pEnv, IAdapterDataSet pAdapter, String pHeaderTableName, String pLinesTableName)
            : base(pEnv, pAdapter, pHeaderTableName, pLinesTableName)
        {

        }

        protected override void prepareBeforeUpdate(DataSet pDataSet)
        {
            base.prepareBeforeUpdate(pDataSet);
            //if (getAdapterWorkState() != AvaExt.Adapter.Const.AdapterWorkState.stateDelete)
            //    reportCurencyExchange = ToolGeneral.getExchange(environment.getInfoApplication().periodCurrencyReportId, getDateTime(), environment);
        }

        protected virtual void prepareForGL(DataSet pDataSet)
        {
        }
        public void prepareForGL()
        {
            prepareForGL(getDataSet());
        }

        public virtual void setHeaderClient(object pClienRef)
        {
        }
        public virtual int getHeaderClient()
        {
            return int.MaxValue;
        }
        public virtual void setDateTime(object pValue) 
        {

        }
        public virtual void setCancelled(object pValue) 
        {

        }
        public virtual object getCancelled()
        {
            return ConstBool.not;
        }
        public virtual DateTime getDateTime()
        {
            return DateTime.Now;
        }
        public virtual void setTime(DateTime pDateTime)
        {

        }
        public virtual int getTime()
        {
            return 0;
        }
        public virtual void setDocNo(String docNo)
        {

        }
        public virtual String getDocNo()
        {
            return String.Empty;
        }
        public virtual void setDocCode(String docCode)
        {

        }
        public virtual String getDocCode()
        {
            return String.Empty;
        }
        public virtual int getModId()
        {
            return 0;
        }
        public virtual int getTrId()
        {
            return 0;
        }
        public virtual void setDivId(int divId)
        {

        }
        public virtual int getDivId()
        {
            return 0;
        }
        public virtual void setDepId(int depId)
        {

        }
        public virtual int getDepId()
        {
            return 0;
        }
        public virtual void setFacId(int facId)
        {

        }
        public virtual int getFacId()
        {
            return 0;
        }
        public virtual void setHeaderSourceIndex(short pSourceIndex)
        {

        }
        public virtual short getHeaderSourceIndex()
        {
            return short.MaxValue;
        }
        public virtual void setHeaderDestenationIndex(short pDestenationIndex)
        {

        }
        public virtual short getHeaderDestenationIndex()
        {
            return short.MaxValue;
        }
        public virtual void setSpeCode(String speCode)
        {

        }
        public virtual String getSpeCode()
        {
            return String.Empty;
        }
        public virtual void setCyphCode(String cyphCode)
        {

        }
        public virtual String getCyphCode()
        {
            return String.Empty;
        }
        public virtual void setReportingCurrencyExch(double pExch)
        {

        }
        public virtual double getReportingCurrencyExch()
        {
            return 0;
        }
        public virtual void setCancelled()
        {

        }

        public virtual void setUsedCurrency(ConstUsedCur pCur)
        {
            innerVarUsedCurrency = pCur;
        }
        public virtual ConstUsedCur getUsedCurrency()
        {
            return innerVarUsedCurrency;
        }

        public override void initCopy()
        {
            base.initCopy();
            setDateTime(DateTime.Now);

        }
    }
}
