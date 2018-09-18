using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;
using System.Data;
using System.ComponentModel;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using AvaExt.InfoClass;
using AvaExt.Database.Tools;
using AvaExt.TableOperation;
using AvaExt.Database.Const;
using System.Collections;
using AvaExt.Database.GL;
using AvaExt.Common.Const;


namespace AvaExt.Adapter.ForUser.Finance.Operation.Cash
{
    public class AdapterUserCash : AdapterUserDocument
    {

        protected short docSign;

        public AdapterUserCash(IEnvironment pEnv, IAdapterDataSet pAdapter, String pHeaderTableName, String pLinesTableName)
            : base(pEnv, pAdapter, pHeaderTableName, pLinesTableName)
        {

        }
        public override void setSpeCode(String speCode)
        {
            setHeader(TableKSLINES.SPECODE, speCode);
        }
        public override String getSpeCode()
        {
            return Convert.ToString(getHeader(TableKSLINES.SPECODE, string.Empty));
        }

        public override void setDepId(int depId)
        {
            setHeader(TableKSLINES.DEPARTMENT, depId);
        }
        public override int getDepId()
        {
            return Convert.ToInt32(getHeader(TableKSLINES.DEPARTMENT, 0));
        }

        public override void setCancelled(object pValue)
        {
            setHeader(TableKSLINES.CANCELLED, pValue);
        }
        public override object getCancelled()
        {
            return getHeader(TableKSLINES.CANCELLED, ConstBool.not);
        }
        public override void setDateTime(object pValue)
        {
            setHeader(TableKSLINES.DATE_, pValue);
        }
        public override void setCancelled()
        {
            setHeader(TableKSLINES.CANCELLED, ConstBool.yes);
        }
        public override void setReportingCurrencyExch(double pExch)
        {
            setHeader(TableKSLINES.REPORTRATE, pExch);
        }
        public override double getReportingCurrencyExch()
        {
            return (double)getHeader(TableKSLINES.REPORTRATE, 0);
        }
        public override void setTime(DateTime pDateTime)
        {
            setHeader(TableKSLINES.HOUR_, pDateTime.Hour);
            setHeader(TableKSLINES.MINUTE_, pDateTime.Minute);
        }

        public override DateTime getDateTime()
        {
            return (DateTime)getHeader(TableKSLINES.DATE_, DateTime.Now);
        }
        public virtual void setHeaderCash(object pCard)
        {
            setHeader(TableKSLINES.CARDREF, pCard);
        }
        public virtual void setHeaderAmount(object pAmout)
        {
            setHeader(TableKSLINES.AMOUNT, pAmout);

        }
        protected override void prepareBeforeUpdate(DataSet pDataSet)
        {
            base.prepareBeforeUpdate(pDataSet);
            DataTable tab;
            DataRow row;
            tab = pDataSet.Tables[TableKSLINES.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (isUsedRow(row))
                {
                    if (row.RowState == DataRowState.Added)
                    {

                    }
                    else
                    {

                    }
                    ToolGeneral.setReportingCurrInfo(row, reportCurencyExchange, TableKSLINES.REPORTRATE, TableKSLINES.AMOUNT, TableKSLINES.REPORTNET);
                }
            }
        }
        protected override void dataResived(DataSet pDataSet)
        {
            base.dataResived(pDataSet);
            for (int i = 0; i < pDataSet.Tables.Count; ++i)
            {
                DataTable tab = pDataSet.Tables[i];
                switch (tab.TableName)
                {
                    case TableKSLINES.TABLE:
                        tab.ColumnChanged += new DataColumnChangeEventHandler(tableKSLINESColumnChanged);
                        break;
                }
            }
        }
        protected override void newRowInCollection(DataRow pNewRow)
        {
            base.newRowInCollection(pNewRow);

            switch (pNewRow.Table.TableName)
            {
                case TableKSLINES.TABLE:
                    pNewRow[TableKSLINES.DATE_] = DateTime.Now;
                    pNewRow[TableKSLINES.TRCODE] = docTrCode;
                    pNewRow[TableKSLINES.SIGN] = docSign;
                    break;
            }


        }

        protected override void addDefaults(DataSet pDataSet)
        {
            base.addDefaults(pDataSet);
            for (int i = 0; i < pDataSet.Tables.Count; ++i)
            {
                DataTable tab = pDataSet.Tables[i];
                switch (tab.TableName)
                {
                    case TableKSLINES.TABLE:
                        if (tab.Rows.Count == 0)
                            addRowToTable(tab);
                        break;
                }

            }

        }
        //
        protected virtual void tableKSLINESColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            switch (e.Column.ColumnName)
            {
                case TableKSLINES.AMOUNT:
                    break;

            }
        }


    }
}
