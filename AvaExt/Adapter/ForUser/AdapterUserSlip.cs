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
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.TableOperation.CellMathActions;

using AvaExt.Adapter.Tools.ImplRowValidator;
using AvaExt.Database.Const;
using AvaExt.PagedSource;
using AvaExt.Database.GL;
using System.Collections;
using AvaExt.TableOperation.RowValidator;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.Expression;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Settings;

using AvaExt.TableOperation.EventFiler;
using AvaExt.TableOperation.CellAutomation;



namespace AvaExt.Adapter.ForUser
{
    public class AdapterUserSlip : AdapterUserStockDocument
    {
        protected short docGroupCode;
        protected short docIOCode;

        protected short lineTrCode;
        protected short lineGroupCode;
        protected short lineIOCode;

        protected IRowValidator validatorLineGlobal = new RowValidatorInListInt(TableSTLINE.GLOBTRANS, new int[] { (int)ConstBool.yes });
        protected IRowValidator validatorLineLocal = new RowValidatorInListInt(TableSTLINE.GLOBTRANS, new int[] { (int)ConstBool.not });
        protected IRowValidator validatorLineMat = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.material });
        protected IRowValidator validatorLineDiscount = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.discount });
        protected IRowValidator validatorLineMatOrDiscount = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.material, (int)ConstLineType.discount });
        protected IRowValidator validatorLineSurcharge = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.surcharge });
        protected IRowValidator validatorLineMatOrSurcharge = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.material, (int)ConstLineType.surcharge });
        protected IRowValidator validatorLineMatOrPromo = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.material, (int)ConstLineType.promotion });
        protected IRowValidator validatorLinePromo = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.promotion });

        protected bool innerParamDistDiscToAcc = false;
        protected bool innerParamDistSurchToAcc = false;
        protected bool innerParamDistPromDiscToItm = false;

        protected ICellReltions cellBindingLines;
        protected ICellReltions cellBindingHeader;

        //
        BlockHandler bhDiscountLocalCalc = new BlockHandler();
        BlockHandler bhDiscountGlobalCalc = new BlockHandler();
        BlockHandler bhSurchargeLocalCalc = new BlockHandler();
        BlockHandler bhSurchargeGlobalCalc = new BlockHandler();
        BlockHandler bhTotsGlobalCalc = new BlockHandler();

        //


        IList<int> listCurIndx = new List<int>(new int[] { (int)ConstUsedCur.national, (int)ConstUsedCur.trans, (int)ConstUsedCur.report, (int)ConstUsedCur.euro, (int)ConstUsedCur.other });

        string[] arrPriceCols = new string[]{
                TableSTLINE.PRICE,
                TableSTLINE.E_DUMMY__REPPRICE
        };
        string[] arrTotalCols = new string[]{
                TableSTLINE.TOTAL,
                TableSTLINE.E_DUMMY__REPTOTAL
        };
        string[] arrRateCols = new string[]{
                string.Empty,
                TableSTLINE.E_DUMMY__REPRATE
        };
        const int count = 1;

        IBlockPoint[] bpArrMain = new IBlockPoint[count];
        IBlockPoint[] bpArrPriceXToLoc = new IBlockPoint[count];
        IBlockPoint[] bpArrPriceXFromLoc = new IBlockPoint[count];
        IBlockPoint[] bpArrTotalXToLoc = new IBlockPoint[count];
        IBlockPoint[] bpArrTotalXFromLoc = new IBlockPoint[count];
        //


        IBlockPoint globalRecalc = new BlockPoint();

        public AdapterUserSlip(IEnvironment pEnv, IAdapterDataSet pAdapter, String pHeaderTableName, String pLinesTableName)
            : base(pEnv, pAdapter, pHeaderTableName, pLinesTableName)
        {

        }

        public override void setSpeCode(String speCode)
        {
            setHeader(TableINVOICE.SPECODE, speCode);
        }
        public override String getSpeCode()
        {
            return Convert.ToString(getHeader(TableINVOICE.SPECODE, string.Empty));
        }

        public override void setDepId(int depId)
        {
            setHeader(TableINVOICE.DEPARTMENT, depId);
        }
        public override int getDepId()
        {
            return Convert.ToInt32(getHeader(TableINVOICE.DEPARTMENT, 0));
        }


        public override void setCancelled()
        {
            setHeader(TableINVOICE.CANCELLED, ConstBool.yes);
        }
        public override void setHeaderClient(object pClienRef)
        {
            setHeader(TableINVOICE.CLIENTREF, pClienRef);
        }
        public override int getHeaderClient()
        {
            return (int)getHeader(TableINVOICE.CLIENTREF, LRef.undef);
        }
        public override void setHeaderSourceIndex(short pSourceIndex)
        {
            setHeader(TableINVOICE.SOURCEINDEX, pSourceIndex);
        }
        public override short getHeaderSourceIndex()
        {
            return (short)getHeader(TableINVOICE.SOURCEINDEX, (short)0);
        }
        public override void setHeaderDestenationIndex(short pDestenationIndex)
        {
            setHeader(TableINVOICE.DESTINDEX, pDestenationIndex);
        }
        public override void setTime(DateTime pDateTime)
        {
            setHeader(TableINVOICE.FTIME, ToolGeneral.time2IntTime(pDateTime));
        }
        public override int getTime()
        {
            return (int)getHeader(TableINVOICE.FTIME, 0);
        }
        public override void setDateTime(object pValue)
        {
            setHeader(TableINVOICE.DATE_, pValue);
        }
        public override void setCancelled(object pValue)
        {
            setHeader(TableINVOICE.CANCELLED, pValue);
        }
        public override object getCancelled()
        {
            return getHeader(TableINVOICE.CANCELLED, ConstBool.not);
        }
        public override DateTime getDateTime()
        {
            return (DateTime)ToolCell.isNull(getHeader(TableINVOICE.DATE_, DateTime.Now), DateTime.Now);
        }
        public override void setDocNo(String docNo)
        {
            setHeader(TableINVOICE.DOCODE, docNo);
        }
        public override String getDocNo()
        {
            return (String)getHeader(TableINVOICE.DOCODE, String.Empty);
        }

        public override void setLineStock(object pStockRef)
        {
            setLine(TableSTLINE.STOCKREF, pStockRef);
        }
        public override void setLineUnit(object pUnit)
        {
            setLine(TableSTLINE.UOMREF, pUnit);
        }
        public override void setLinePrice(object pPrice)
        {
            setLine(TableSTLINE.PRICE, pPrice);
        }
        public override double getLinePrice()
        {
            return (double)getLine(TableSTLINE.PRICE);
        }
        public override void setLineAmount(object pAmout)
        {
            setLine(TableSTLINE.AMOUNT, pAmout);
        }
        public override void setLineType(ConstLineType lineType)
        {
            setLine(TableSTLINE.LINETYPE, (short)lineType);
        }
        public override void setLineAsGlobal()
        {
            setLine(TableSTLINE.GLOBTRANS, ConstBool.yes);
        }
        public override void setLineAsLocal()
        {
            setLine(TableSTLINE.GLOBTRANS, ConstBool.not);
        }
        public override void setLineDicountPercent(double discount)
        {
            setLine(TableSTLINE.DISCPER, discount);
        }
        public override void setLineTotal(double total)
        {
            setLine(TableSTLINE.TOTAL, total);
        }



        public override void setReportingCurrencyExch(double pExch)
        {
            setHeader(TableINVOICE.REPORTRATE, pExch);
        }
        public override double getReportingCurrencyExch()
        {
            return (double)getHeader(TableINVOICE.REPORTRATE, 0);
        }

        protected override void prepareBeforeUpdate(DataSet pDataSet)
        {
            base.prepareBeforeUpdate(pDataSet);
            DataTable tab;
            DataRow row;
            tab = pDataSet.Tables[TableINVOICE.TABLE];
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
                    //ToolGeneral.setReportingCurrInfo(row, reportCurencyExchange, TableINVOICE.REPORTRATE, TableINVOICE.NETTOTAL, TableINVOICE.REPORTNET);
                }
            }
            tab = pDataSet.Tables[TableSTLINE.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (isUsedRow(row)) // || isSlipLinesRow(row))
                {
                    if (row.RowState == DataRowState.Added)
                    {
                    }
                    else
                    {
                    }
                    ToolCell.set(row, TableSTLINE.MONTH_, getDateTime().Month);
                    ToolCell.set(row, TableSTLINE.YEAR_, getDateTime().Year);
                    //ToolGeneral.setReportingCurrInfo(row, reportCurencyExchange, TableSTLINE.REPORTRATE, null, null);
                }
            }
        }
        protected override void dataResived(DataSet pDataSet)
        {



            base.dataResived(pDataSet);
            sortStockLinesInResived(pDataSet);
            dataResivedForINVOICE(pDataSet);
            dataResivedForSTLINE(pDataSet);


        }

        protected virtual void dataResivedForINVOICE(DataSet pDataSet)
        {

            IPagedSource pagedSource;
            DataTable tab = pDataSet.Tables[TableINVOICE.TABLE];
            cellBindingHeader = new ImplCellReltions(tab);



            tab.ColumnChanged += new DataColumnChangeEventHandler(tableINVOICEColumnChanged);



            /////CURRENCY///////////////////////////////////////////////////////////////////////


            //ToolColumn.add(tab, TableSTLINE.E_DUMMY__RATE, typeof(double));
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__CUR, typeof(short));
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__CURCODE, typeof(string));

            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__REPRATE, typeof(double));



            //string _extp = "IIF({1} <> 0,{0}/{1},0)";

            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__TOTSURCHARGE, typeof(double));
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__TOTDISCOUNT, typeof(double));
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__TOT, typeof(double));
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__TOTNET, typeof(double));

            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__REPTOTSURCHARGE, typeof(double));
            //new RowColumnsBindingInnerExpr(tab, new string[] { TableINVOICE.E_DUMMY__TOTSURCHARGE, TableINVOICE.E_DUMMY__REPRATE }, TableINVOICE.E_DUMMY__REPTOTSURCHARGE, _extp, null);
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__REPTOTDISCOUNT, typeof(double));
            //new RowColumnsBindingInnerExpr(tab, new string[] { TableINVOICE.E_DUMMY__TOTDISCOUNT, TableINVOICE.E_DUMMY__REPRATE }, TableINVOICE.E_DUMMY__REPTOTDISCOUNT, _extp, null);
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__REPTOT, typeof(double));
            //new RowColumnsBindingInnerExpr(tab, new string[] { TableINVOICE.E_DUMMY__TOT, TableINVOICE.E_DUMMY__REPRATE }, TableINVOICE.E_DUMMY__REPTOT, _extp, null);
            //ToolColumn.add(tab, TableINVOICE.E_DUMMY__REPTOTNET, typeof(double));
            //new RowColumnsBindingInnerExpr(tab, new string[] { TableINVOICE.E_DUMMY__TOTNET, TableINVOICE.E_DUMMY__REPRATE }, TableINVOICE.E_DUMMY__REPTOTNET, _extp, null);


            ////////////////////////////////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////////////
            ToolColumn.add(tab, TableINVOICE.E_DUMMY__SOURCEWHNAME, typeof(string));
            pagedSource = new PagedSourceWarehouse(environment);
            new RowColumnsBindingDBRelations(tab, pagedSource,
               new string[] { TableINVOICE.SOURCEINDEX },
               new string[] { TableWHOUSE.NR },
               new string[] { TableINVOICE.E_DUMMY__SOURCEWHNAME },
               new string[] { TableWHOUSE.NAME },
               UpdateTypeFlags.activeOnRelColumn | UpdateTypeFlags.disableEditCancel | UpdateTypeFlags.setTypeDefaultToDrivedChild,
               null);

            //new RowColumnsBindingDBRelations(tab, new PagedSourceClient(environment),
            //  new string[] { TableINVOICE.CLIENTREF },
            //  new string[] { TableCLCARD.LOGICALREF },
            //  new string[] { string.Empty, string.Empty, TableINVOICE.DISCPER, TableINVOICE.PRCLIST },
            //  new string[] { TableCLCARD.CODE, TableCLCARD.DEFINITION_, TableCLCARD.DISCPER, TableCLCARD.PRCLIST },
            //  UpdateTypeFlags.activateIgnorLast2DrivedChilCols | UpdateTypeFlags.setTypeDefaultToDrivedChild | UpdateTypeFlags.resetIfAllCurrentRelColsAreDefaultOrNull,
            //  null);

            new RowColumnsBindingDBRelations(tab, new PagedSourceClient(environment),
              new string[] { TableINVOICE.CLIENTREF },
              new string[] { TableCLCARD.LOGICALREF },
              new string[] { },
              new string[] { TableCLCARD.CODE, TableCLCARD.DEFINITION_, TableCLCARD.BARCODE },
              UpdateTypeFlags.setTypeDefaultToDrivedChild | UpdateTypeFlags.resetIfAllCurrentRelColsAreDefaultOrNull,
              null);

            /////////////////////////////////////////////////////////

        }
        protected virtual void dataResivedForSTLINE(DataSet pDataSet)
        {
            //IPagedSource pagedSource;
            DataTable tab = pDataSet.Tables[TableSTLINE.TABLE];
            cellBindingLines = new ImplCellReltions(tab);



            tab.ColumnChanged += new DataColumnChangeEventHandler(tableSTLINEColumnChanged);
            tab.RowChanged += new DataRowChangeEventHandler(tableSTLINERowChanged);
            tab.RowDeleted += new DataRowChangeEventHandler(tableSTLINERowDeleted);
            /////CURRENCY///////////////////////////////////////////////////////////////////////
            ToolColumn.add(tab, TableSTLINE.E_DUMMY__TOTAL, typeof(double));
            ToolColumn.add(tab, TableSTLINE.E_DUMMY__PRICE, typeof(double));
            //ToolColumn.add(tab, TableSTLINE.E_DUMMY__RATE, typeof(double));

            //ToolColumn.add(tab, TableSTLINE.E_DUMMY__REPTOTAL, typeof(double));
            //ToolColumn.add(tab, TableSTLINE.E_DUMMY__REPPRICE, typeof(double));
            //ToolColumn.add(tab, TableSTLINE.E_DUMMY__REPRATE, typeof(double));

            ////////////BIND AND INHERIT/////////////////////////////////////////

            new TablesColumnsBinding(
                pDataSet.Tables[TableINVOICE.TABLE],
                pDataSet.Tables[TableSTLINE.TABLE],
                new string[]{
                            TableINVOICE.CANCELLED ,
                            TableINVOICE.CLIENTREF,  
                            TableINVOICE.DATE_,  
                            TableINVOICE.SOURCEINDEX,  
                            //TableINVOICE.E_DUMMY__REPRATE,
                            TableINVOICE.PRCLIST
                            }
                            , new string[] {
                            TableSTLINE.CANCELLED ,
                            TableSTLINE.CLIENTREF,  
                            TableSTLINE.DATE_,  
                            TableSTLINE.SOURCEINDEX,  
                            //TableSTLINE.E_DUMMY__REPRATE,
                            TableSTLINE.PRCLIST
                            }, true);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //MAIN//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < count; ++i) //Now 1
            {
                CellAutomationSimpleExp cellExpes = new CellAutomationSimpleExp(
                     new string[] { TableSTLINE.AMOUNT, arrPriceCols[i], arrTotalCols[i] },
                     new IEvaluator[] { new CalcDoubleDiv(2, 1), new CalcDoubleDiv(2, 0), new CalcDoubleMult(0, 1) },
                    // new string[] { "IIF({1}<>0,{2}/{1},0)", "IIF({0}<>0,{2}/{0},0)", "{0}*{1}" },
                     new string[] { TableSTLINE.AMOUNT, arrPriceCols[i], arrTotalCols[i] });
                cellBindingLines.addRelation(new string[] { TableSTLINE.AMOUNT, arrPriceCols[i], arrTotalCols[i] }, cellExpes, bpArrMain[i] = new BlockPoint(), new ImplValidRowStockPriced());
            }
            //PRICE FROM LOC//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 1; i < count; ++i)
            {
                CellAutomationSimpleExp cellExpes = new CellAutomationSimpleExp(
                     new string[] { arrPriceCols[i] },
                     new IEvaluator[] { new CalcDoubleMult(0, 1) },
                    // new string[] { "{0}*{1}" },
                     new string[] { TableSTLINE.PRICE, arrRateCols[i] });
                cellBindingLines.addRelation(new string[] { TableSTLINE.PRICE, arrRateCols[i] }, cellExpes, bpArrPriceXFromLoc[i] = new BlockPoint(), null);
            }
            //TOTAL FROM LOC//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 1; i < count; ++i)
            {
                CellAutomationSimpleExp cellExpes = new CellAutomationSimpleExp(
                     new string[] { arrTotalCols[i] },
                     new IEvaluator[] { new CalcDoubleMult(0, 1) },
                    // new string[] { "{0}*{1}" },
                     new string[] { TableSTLINE.TOTAL, arrRateCols[i] });
                cellBindingLines.addRelation(new string[] { TableSTLINE.TOTAL, arrRateCols[i] }, cellExpes, bpArrTotalXFromLoc[i] = new BlockPoint(), null);
            }
            ////////////////////////////////////////////////////////////////////////////////////
            setUsedCurrency(ConstUsedCur.national);


            ////////////////////////////////////////////////////////////////////////////////////
            CellAutomationDB dbAutomation;
            dbAutomation = new CellAutomationDB(tab, new PagedSourceMaterial(environment),
               new string[] { TableSTLINE.STOCKREF },
               new string[] { TableITEMS.LOGICALREF },
               new string[] { },
               new string[] { TableITEMS.CODE, TableITEMS.NAME },
               UpdateTypeFlags.resetIfAllCurrentRelColsAreDefaultOrNull,
               validatorLineMatOrPromo);

            cellBindingLines.addRelation(dbAutomation.getTriggerColumns(), dbAutomation, validatorLineMatOrPromo);
            dbAutomation = new CellAutomationDB(tab, new PagedSourceMaterial(environment),
               new string[] { TableSTLINE.STOCKREF },
               new string[] { TableITEMS.LOGICALREF },
               new string[] {/* TableSTLINE.DISCPER, */TableSTLINE.UINFO1, TableSTLINE.UINFO2, TableSTLINE.UNIT, TableSTLINE.UNITREF },
               new string[] {/* TableITEMS.DISCPER,*/ TableITEMS.UNITCF1, TableITEMS.UNITCF1, TableITEMS.UNIT1, TableITEMS.UNITREF1 },
               UpdateTypeFlags.activeOnRelColumn | UpdateTypeFlags.disableEditCancel | UpdateTypeFlags.setTypeDefaultToDrivedChild,
               validatorLineMatOrPromo);

            cellBindingLines.addRelation(dbAutomation.getTriggerColumns(), dbAutomation, validatorLineMatOrPromo);







            //
            //








            new RowDeleteWatcher(tab, null, new string[] { TableSTLINE.TOTAL }, new object[] { 0.0 });



            bindForLineType(tab);
            bindForDistributeSums(tab);
            bindForPromo(tab);




        }
        protected virtual bool setDefaultPriceList()
        {
            return false;
        }


        void blocksListUnblock(IBlockPoint[] pList)
        {
            for (int i = 0; i < pList.Length; ++i)
                if (pList[i] != null)
                    pList[i].unblock();
        }
        void blocksListBlock(IBlockPoint[] pList)
        {
            for (int i = 0; i < pList.Length; ++i)
                if (pList[i] != null)
                    pList[i].block();
        }



        public override void setUsedCurrency(ConstUsedCur pCur)
        {
            base.setUsedCurrency(pCur);



            blocksListBlock(bpArrPriceXToLoc);
            blocksListBlock(bpArrTotalXToLoc);

            blocksListBlock(bpArrPriceXFromLoc);
            blocksListBlock(bpArrTotalXFromLoc);



            //
            int curType = (int)pCur;
            int curIndex = listCurIndx.IndexOf(curType);
            //
            if (pCur == ConstUsedCur.national)
            {
                bpArrMain[curIndex].unblock();
                //
                blocksListUnblock(bpArrPriceXFromLoc);
                blocksListUnblock(bpArrTotalXFromLoc);
            }
            else if (pCur == ConstUsedCur.trans || pCur == ConstUsedCur.report || pCur == ConstUsedCur.other || pCur == ConstUsedCur.euro)
            {
                bpArrMain[curIndex].unblock();
                //
                bpArrPriceXToLoc[curIndex].unblock();
                bpArrTotalXToLoc[curIndex].unblock();
                //
                blocksListUnblock(bpArrPriceXFromLoc);
                blocksListUnblock(bpArrTotalXFromLoc);
                bpArrPriceXFromLoc[curIndex].block();
                bpArrTotalXFromLoc[curIndex].block();
                //
            }
            //


        }

        protected virtual void sortStockLinesInResived(DataSet pDataSet)
        {
            sortLines(pDataSet.Tables[TableSTLINE.TABLE], TableSTLINE.STDOCLNNO);
        }


        protected virtual void bindForDistributeSums(DataTable tab)
        {
            BlockHandler bh = new BlockHandler();
            bh.setBlockPoint(globalRecalc);
            bh.getGroupBlockPoints().Add(cellBindingLines.getMainBlockPoint());
            bh.getGroupBlockPoints().Add(cellBindingHeader.getMainBlockPoint());
            cellBindingLines.addRelation(
                 new string[] { TableSTLINE.PRICE, TableSTLINE.TOTAL, TableSTLINE.DISCPER },
                 new ImplTableColumnChangedWrap(new WorkerStart(distributeDocumentBalance)),
                 bh,
                 null);
            cellBindingHeader.addRelation(
                 new string[] { TableINVOICE.DISCPER },
                 new ImplTableColumnChangedWrap(new WorkerStart(distributeDocumentBalance)),
                 bh,
                 null);

        }
        protected virtual void bindForLineType(DataTable tab)
        {


        }
        protected virtual void tableSTLINEColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            DataTable tab = (DataTable)sender;
            DataSet dataSet = tab.DataSet;

            if (e.Row.RowState != DataRowState.Detached)
                switch (e.Column.ColumnName)
                {
                    case TableSTLINE.LINETYPE:
                        ToolCell.set(e.Row, TableSTLINE.DISCPER, (double)(ToolStockLine.isLinePromotion(e.Row) ? 100 : 0));
                        break;
                    case TableSTLINE.DISCPER:
                        if (ToolStockLine.isLinePromotion(e.Row))
                            ToolCell.set(e.Row, TableSTLINE.DISCPER, (double)100);
                        break;
                    case TableSTLINE.AMOUNT:
                    case TableSTLINE.PRICE:
                    case TableSTLINE.TOTAL:
                        break;
                }

        }
        protected virtual void tableSTLINERowDeleted(object sender, DataRowChangeEventArgs e)
        {
            // DataTable tab = (DataTable)sender;
            //DataSet dataSet = tab.DataSet;
            // distributeDocumentBalance(dataSet);
        }
        protected virtual void tableSTLINERowChanged(object sender, DataRowChangeEventArgs e)
        {

        }
        protected override void addDefaults(DataSet pDataSet)
        {
            base.addDefaults(pDataSet);
            for (int i = 0; i < pDataSet.Tables.Count; ++i)
            {
                DataTable tab = pDataSet.Tables[i];
                switch (tab.TableName)
                {
                    case TableINVOICE.TABLE:
                        if (tab.Rows.Count == 0)
                            addRowToTable(tab);
                        break;
                    case TableSTLINE.TABLE:
                        break;
                }

            }

        }
        protected virtual void distributeDocumentBalance()
        {
            InfoInvoiceSum infoSum = calcSlipSum(getDataSet());
            distributeDocumentBalance(infoSum, getDataSet().Tables[TableINVOICE.TABLE]);
        }
        protected virtual void distributeDocumentBalance(InfoInvoiceSum infoSum, DataTable tab)
        {
            ToolColumn.setColumnValue(tab, TableINVOICE.ADDDISCOUNTS, infoSum.sumOfGlobDisc);
            ToolColumn.setColumnValue(tab, TableINVOICE.TOTALDISCOUNTS, infoSum.sumOfLocDisc + infoSum.sumOfGlobDisc + infoSum.sumOfLocPromo + infoSum.sumOfGlobPromo);
            ToolColumn.setColumnValue(tab, TableINVOICE.TOTALDISCOUNTED, infoSum.sumOfTotal - infoSum.sumOfLocDisc);
            ToolColumn.setColumnValue(tab, TableINVOICE.ADDEXPENSES, infoSum.sumOfGlobSurch);
            ToolColumn.setColumnValue(tab, TableINVOICE.TOTALEXPENSES, infoSum.sumOfLocSurch + infoSum.sumOfGlobSurch);

            ToolColumn.setColumnValue(tab, TableINVOICE.TOTALDEPOZITO, 0);
            ToolColumn.setColumnValue(tab, TableINVOICE.TOTALPROMOTIONS, infoSum.sumOfLocPromo + infoSum.sumOfGlobPromo);

            ToolColumn.setColumnValue(tab, TableINVOICE.TOTALVAT, infoSum.sumOfLocVAT + infoSum.sumOfGlobVAT);
            ToolColumn.setColumnValue(tab, TableINVOICE.GROSSTOTAL, infoSum.sumOfTotal + infoSum.sumOfLocPromo + infoSum.sumOfGlobPromo);
            ToolColumn.setColumnValue(tab, TableINVOICE.NETTOTAL, infoSum.sumOfTotal - (infoSum.sumOfGlobDisc + infoSum.sumOfLocDisc) + (infoSum.sumOfGlobSurch + infoSum.sumOfLocSurch) + (infoSum.sumOfLocVAT + infoSum.sumOfGlobVAT) + (infoSum.sumOfLocTax + infoSum.sumOfGlobTax));
            ToolColumn.setColumnValue(tab, TableINVOICE.TOTALADDTAX, infoSum.sumOfLocTax + infoSum.sumOfGlobTax);

        }
        //
        protected virtual void tableINVOICEColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            DataTable tab = (DataTable)sender;
            DataSet dataSet = tab.DataSet;

            if (!ToolCell.isNull(e.ProposedValue))
                switch (e.Column.ColumnName)
                {
                    case TableINVOICE.DATE_:
                        break;


                }
        }
        protected override bool isUsedRow(DataRow pRow)
        {
            return !ToolRow.isDeleted(pRow);
        }
        double getLineRealTotal(DataRow row)
        {
            double tot = (double)ToolCell.isNull(row[TableSTLINE.TOTAL], (double)0.0);
            return tot;
        }

        double getLineNetTotal(DataRow row)
        {
            double tot = getLineRealTotal(row);
            double perc = (double)ToolCell.isNull(row[TableSTLINE.DISCPER], (double)0.0);
            return tot * (100 - perc) / 100;
        }

        public InfoInvoiceSum calcSlipSum(DataSet pDataSet)
        {
            DataTable pTabSTLINE = pDataSet.Tables[TableSTLINE.TABLE];
            DataTable pTabINVOICE = pDataSet.Tables[TableINVOICE.TABLE];
            InfoInvoiceSum infoSum = new InfoInvoiceSum();

            for (int i = 0; i < pTabSTLINE.Rows.Count; ++i)
            {
                DataRow row = pTabSTLINE.Rows[i];

                if (isUsedRow(row) && !ToolRow.isDeleted(row))
                {
                    if (ToolStockLine.isLineMaterial(row))
                    {
                        double tot = getLineRealTotal(row);
                        double totNet = getLineNetTotal(row);
                        infoSum.sumOfTotal += tot;
                        infoSum.sumOfLocDisc += (tot - totNet);
                    }
                    if (ToolStockLine.isLinePromotion(row))
                    {
                        double tot = getLineRealTotal(row);
                        infoSum.sumOfLocPromo += tot;
                    }
                }
            }

            double gDisc = (double)ToolColumn.getColumnLastValue(pTabINVOICE, TableINVOICE.DISCPER, 0.0);
            infoSum.sumOfGlobDisc = (infoSum.sumOfTotal - infoSum.sumOfLocDisc) * (gDisc / 100);
            return infoSum;
        }
        protected override bool isEmptyData(DataSet pDataSet)
        {
            DataTable table = pDataSet.Tables[TableSTLINE.TABLE];
            for (int i = 0; i < table.Rows.Count; ++i)
                if (table.Rows[i].RowState != DataRowState.Deleted)
                    if (ToolStockLine.isLineMaterialized(table.Rows[i]))
                        if ((Double)table.Rows[i][TableSTLINE.AMOUNT] > ConstValues.minPositive)
                            return false;
            return true;
        }
        protected override void deleteExcessData(DataSet pDataSet)
        {
            base.deleteExcessData(pDataSet);
            DataTable table = pDataSet.Tables[TableSTLINE.TABLE];
            for (int i = 0; i < table.Rows.Count; ++i)
                if (table.Rows[i].RowState != DataRowState.Deleted)
                    if (ToolStockLine.isLineMaterialized(table.Rows[i]))
                        if ((Double)table.Rows[i][TableSTLINE.AMOUNT] < ConstValues.minPositive)
                            table.Rows[i].Delete();
        }
        protected override void newRowInCollection(DataRow row)
        {
            base.newRowInCollection(row);



            switch (row.Table.TableName)
            {
                case TableINVOICE.TABLE:
                    row[TableINVOICE.GRPCODE] = docGroupCode;
                    row[TableINVOICE.IOCODE] = docIOCode;
                    row[TableINVOICE.TRCODE] = docTrCode;
                    row[TableINVOICE.DATE_] = DateTime.Now;
                    row[TableINVOICE.PRCLIST] = environment.getSysSettings().getInt(SettingsSysMob.MOB_SYS_DEF_PLIST, 0);
                    break;
                case TableSTLINE.TABLE:
                    row[TableSTLINE.IOCODE] = lineIOCode;
                    row[TableSTLINE.TRCODE] = lineTrCode;
                    break;




            }
        }

        protected virtual void bindForPromo(DataTable tab)
        {
            //
            //
            IRowValidator rowValidL = new RowValidatorGroup(new IRowValidator[] { validatorLineLocal, validatorLineMatOrPromo });

            //
            IRowsSelector topL = new RowsSelectorValidator(
                tab,
                validatorLineMat,
                validatorLineMat,
                new RowValidatorFalse(),
                new RowValidatorTrue(),
                rowValidL);
            IRowsSelector subL = new RowsSelectorValidator(
                tab,
                validatorLinePromo,
                validatorLineMat,
                validatorLinePromo,
                validatorLineMat,
                rowValidL);
            new RowLocalGroupKeeper(tab, topL, subL, rowValidL);
        }

    }

}
