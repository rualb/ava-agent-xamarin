using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using AvaExt.SQL;

namespace AvaExt.Adapter.Tools
{
    public class ToolStockLine
    {

        public static bool isLineMaterial(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.LINETYPE], (short)ConstLineType.undef) == (short)ConstLineType.material);
        }
        public static bool isLinePromotion(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.LINETYPE], (short)ConstLineType.undef) == (short)ConstLineType.promotion);
        }
        public static bool isLineSurcharge(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.LINETYPE], (short)ConstLineType.undef) == (short)ConstLineType.surcharge);
        }
        public static bool isLineDiscount(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.LINETYPE], (short)ConstLineType.undef) == (short)ConstLineType.discount);
        }
        public static bool isLineDeposit(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.LINETYPE], (short)ConstLineType.undef) == (short)ConstLineType.deposit);
        }
        public static bool isLineMixed(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.LINETYPE], (short)ConstLineType.undef) == (short)ConstLineType.mixedCase);
        }
        public static bool isLineService(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.LINETYPE], (short)ConstLineType.undef) == (short)ConstLineType.service);
        }
        public static bool isLineMaterialized(DataRow row)
        {
            return
                ToolStockLine.isLineMaterial(row) ||
                ToolStockLine.isLinePromotion(row) ||
                ToolStockLine.isLineDeposit(row) ||
                ToolStockLine.isLineMixed(row) ||
                ToolStockLine.isLineService(row)
                ;
        }
        public static bool isLineStockMaterial(DataRow row)
        {
            return

                ToolStockLine.isLineMaterial(row) ||
                 ToolStockLine.isLinePromotion(row) ||
                ToolStockLine.isLineDeposit(row)

                ;
        }
        //
        public static bool isLineMonetary(DataRow row)
        {
            return

                ToolStockLine.isLineMaterial(row) ||
                ToolStockLine.isLineDeposit(row) ||
                ToolStockLine.isLineMixed(row) ||
                ToolStockLine.isLineService(row)
                ;
        }
        public static bool isLinePureMoney(DataRow row)
        {
            return

                ToolStockLine.isLineDiscount(row) ||
                ToolStockLine.isLineSurcharge(row)
                ;
        }
        public static bool isLineStlineGlobal(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.GLOBTRANS], (short)ConstBool.undef) == (short)ConstBool.yes);
        }
        public static bool isLineStlineLocal(DataRow row)
        {
            return ((short)ToolCell.isNull(row[TableSTLINE.GLOBTRANS], (short)ConstBool.undef) == (short)ConstBool.not);
        }
        public static bool isLineParent(DataRow row)
        {
            return isLineMaterial(row);
        }

        public static DataRow getLineParent(DataRow row)
        {
            for (int i = row.Table.Rows.IndexOf(row); i >= 0; --i)
                if (!ToolRow.isDeleted(row.Table.Rows[i]))
                {
                    if (isLineMaterial(row.Table.Rows[i]))
                        return row.Table.Rows[i];
                }
            return null;
        }


        public static DataRow getItemData(IEnvironment pEnv, object lref)
        {
            DataTable tab_ = SqlExecute.execute(pEnv, "SELECT * FROM LG_$FIRM$_ITEMS WHERE LOGICALREF = @P1", new object[] { lref });
            tab_.TableName = TableITEMS.TABLE;
            return ToolRow.getFirstRealRow(tab_);

            //IPagedSource ps = new PagedSourceMaterial(pEnv);
            //ps.getBuilder().addParameterValue(TableITEMS.LOGICALREF, lref);
            //DataTable table = ps.getAll();
            //return ToolRow.getFirstRealRow(table);
        }
        public static double itemAmountInDoc(object lref, DataTable docLines)
        {
            double fix = 0;
            foreach (DataRow lRow in docLines.Rows)
                if (lRow.RowState != DataRowState.Deleted)
                    if (ToolType.isEqual(lref, lRow[TableSTLINE.STOCKREF]))
                        fix += getStockDocLineAmount(lRow);
            return fix;
        }
        public static double itemAmountInDB(IEnvironment pEnv, object lref, object docRef)
        {
            IPagedSource ps = new PagedSourceMaterialTrans(pEnv);
            ps.getBuilder().addParameterValue(TableSTLINE.STOCKREF, lref);
            ps.getBuilder().addParameterValue(TableSTLINE.STDOCREF, docRef, SqlTypeRelations.notEqual);
            return itemAmountInDoc(lref, ps.getAll());
        }
        static double getStockDocLineAmount(DataRow docLine)
        {
            double amount = 0;
            ConstBool isCancelled = (ConstBool)(short)ToolCell.isNull(docLine[TableSTLINE.CANCELLED], (short)ConstBool.yes);
            ConstLineType lineType = (ConstLineType)(short)ToolCell.isNull(docLine[TableSTLINE.LINETYPE], (short)ConstLineType.undef);


            if (isCancelled == ConstBool.not && (lineType == ConstLineType.material || lineType == ConstLineType.promotion))
            {
                amount = (double)ToolCell.isNull(docLine[TableSTLINE.AMOUNT], 0.0);
                ConstIOCode ioCode = (ConstIOCode)(short)ToolCell.isNull(docLine[TableSTLINE.IOCODE], (short)0);
                switch (ioCode)
                {
                    case ConstIOCode.input:
                    case ConstIOCode.inputFromWarehouse:
                        amount = +amount;
                        break;
                    case ConstIOCode.output:
                    case ConstIOCode.outputFromWarehouse:
                        amount = -amount;
                        break;
                    default:
                        amount = 0;
                        break;
                }
            }
            return amount;
        }

    }
}
