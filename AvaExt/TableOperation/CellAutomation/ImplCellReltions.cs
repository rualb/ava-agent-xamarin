using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowValidator;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common;
using AvaExt.TableOperation.CellAutomation.TableEvents;

namespace AvaExt.TableOperation.CellAutomation
{

    public class ImplCellReltions : ICellReltions
    {
        const ColumnChangeEventType defaultEvent = ColumnChangeEventType.changed;
        IRowValidator validatorMain = new RowValidatorTrue();
        string colTYPE = "TYPE";
        string colNAME = "NAME";
        string colACTIVITY = "ACTIVITY";
        string colVALIDATOR = "VALIDATOR";
        string colBLOCK = "BLOCK";
        DataTable tableRel = new DataTable();
        DataTable tableRelDistinct = new DataTable();
        DataTable tableTarget;
        IBlockPoint blockPointMain = new BlockPoint();
        List<string> activeColumns = new List<string>();
        public ImplCellReltions(DataTable pTab)
        {
            tableTarget = pTab;

            tableRel.Columns.Add(colTYPE, typeof(ColumnChangeEventType));
            tableRel.Columns.Add(colNAME, typeof(string));
            tableRel.Columns.Add(colACTIVITY, typeof(List<ITableColumnChange>));
            tableRel.Columns.Add(colVALIDATOR, typeof(List<IRowValidator>));
            tableRel.Columns.Add(colBLOCK, typeof(List<IBlockPoint>));
            tableRel.PrimaryKey = new DataColumn[] { tableRel.Columns[colTYPE], tableRel.Columns[colNAME] };
            tableTarget.ColumnChanged += new DataColumnChangeEventHandler(columnChanged);
            tableTarget.ColumnChanging += new DataColumnChangeEventHandler(columnChanging);

            tableRelDistinct.Columns.Add(colACTIVITY, typeof(ITableColumnChange));
            tableRelDistinct.Columns.Add(colVALIDATOR, typeof(IRowValidator));
        }
        public void setMainValidator(IRowValidator pValidator)
        {
            validatorMain = (pValidator == null) ? new RowValidatorTrue() : pValidator;
        }

        DataRow getRow(ColumnChangeEventType pEvent, string col)
        {
            return tableRel.Rows.Find(new object[] { pEvent, col });
        }

        void columnChanged(object sender, DataColumnChangeEventArgs e)
        {
            columnEvent(ColumnChangeEventType.changed, sender, e);
        }

        void columnChanging(object sender, DataColumnChangeEventArgs e)
        {
            columnEvent(ColumnChangeEventType.changing, sender, e);
        }

        void columnEvent(ColumnChangeEventType pEvent, object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                if (!blockPointMain.isBlocked())
                {
                    string col = e.Column.ColumnName;
                    DataRow row = getRow(pEvent, e.Column.ColumnName);
                    if (row != null)
                    {
                        List<ITableColumnChange> listA = (List<ITableColumnChange>)row[colACTIVITY];
                        List<IRowValidator> listV = (List<IRowValidator>)row[colVALIDATOR];
                        List<IBlockPoint> listB = (List<IBlockPoint>)row[colBLOCK];
                        for (int i = 0; i < listA.Count; ++i)
                        {
                            if (listB[i].block())
                            {
                                try
                                {
                                    if (listV[i].check(e.Row))
                                        listA[i].columnChange(e);
                                }
                                finally
                                {
                                    listB[i].unblock();
                                }
                            }
                        }
                    }
                }
            }
        }



        public void addRelation(string[] pCols, ITableColumnChange pActivity, IRowValidator pValidator)
        {
            addRelation(pCols, pActivity, new BlockPoint(), pValidator, defaultEvent);
        }
        public void addRelation(string[] pCols, ITableColumnChange pActivity, IRowValidator pValidator, ColumnChangeEventType pEvent)
        {
            addRelation(pCols, pActivity, new BlockPoint(), pValidator, pEvent);
        }
        public void addRelation(string[] pCols, ITableColumnChange pActivity, IBlockPoint pBlock, IRowValidator pValidator)
        {
            addRelation(pCols, pActivity, pBlock, pValidator, defaultEvent);
        }
        public void addRelation(string[] pCols, ITableColumnChange pActivity, IBlockPoint pBlock, IRowValidator pValidator, ColumnChangeEventType pEvent)
        {
            pValidator = (pValidator == null) ? new RowValidatorTrue() : pValidator;

            try
            {
                blockPointMain.block();
                for (int i = 0; i < tableTarget.Rows.Count; ++i)
                {
                    DataRow row = tableTarget.Rows[i];
                    if (pValidator.check(row))
                        pActivity.initForColumnChanged(row);
                }

            }
            finally
            {
                blockPointMain.unblock();
            }

            foreach (string col in pCols)
                add(col, pActivity, pBlock, pValidator, pEvent);
        }


        void add(string col, ITableColumnChange pActivity, IBlockPoint pBlock, IRowValidator pValidator, ColumnChangeEventType pEvent)
        {

 
            {
                DataRow row = getRow(pEvent, col);
                if (row == null)
                    row = tableRel.Rows.Add(
                    new object[] { 
                        pEvent,
                        col,
                        new List<ITableColumnChange>(), 
                        new List<IRowValidator>(),
                        new List<IBlockPoint>(),
                    });

                ((List<ITableColumnChange>)row[colACTIVITY]).Add(pActivity);
                ((List<IRowValidator>)row[colVALIDATOR]).Add((pValidator == null) ? new RowValidatorTrue() : pValidator);
                ((List<IBlockPoint>)row[colBLOCK]).Add(pBlock);
            }
            {

                tableRelDistinct.Rows.Add(
                    new object[] { 
                        pActivity,
                        pValidator
                    });
            }
        }

        public IBlockPoint getMainBlockPoint()
        {
            return blockPointMain;
        }




        public void reinit()
        {
            try
            {
                blockPointMain.block();

                //foreach (DataRow rowData in tableTarget.Rows)
                //{
                //    foreach (DataRow rowAction in tableRelDistinct.Rows)
                //    {
                //        if (((IRowValidator)rowAction[colVALIDATOR]).check(rowData))
                //            ((ITableColumnChange)rowAction[colACTIVITY]).initForColumnChanged(rowData);
                //    }
                //}


                foreach (DataRowView rowDataView in tableTarget.DefaultView)
                {
                    foreach (DataRow rowAction in tableRelDistinct.Rows)
                    {
                        if (((IRowValidator)rowAction[colVALIDATOR]).check(rowDataView.Row))
                            ((ITableColumnChange)rowAction[colACTIVITY]).initForColumnChanged(rowDataView.Row);
                    }
                }


            }
            finally
            {
                blockPointMain.unblock();
            }
        }

    }
}
