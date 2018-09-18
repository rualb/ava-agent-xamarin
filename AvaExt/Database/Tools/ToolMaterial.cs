using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;

using AvaExt.Adapter.Tools;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using System.Data;
using AvaExt.Database.Const;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic;

namespace AvaExt.Database.Tools
{
    public class ToolMaterial
    {


        public static DataTable getMaterialPrices(int stock, DateTime date, IEnvironment env, ConstPriceType type)
        {
            IPagedSource s = new PagedSourcePriceList(env);
            s.getBuilder().addParameterValue(TablePRCLIST.PTYPE, (short)type);
            s.getBuilder().addParameterValue(TablePRCLIST.CARDREF, stock);
            s.getBuilder().addParameterValue(TablePRCLIST.BEGDATE, date, SqlTypeRelations.lessEqual);
            s.getBuilder().addParameterValue(TablePRCLIST.ENDDATE, date, SqlTypeRelations.greaterEqual);
            return s.getAll();
        }
        public static DataTable getMaterialUnits(int stock, IEnvironment env)
        {
            IPagedSource s = new PagedSourceMaterialUnits(env);
            s.getBuilder().addParameterValue(TableITMUNITA.ITEMREF, stock);
            return s.getAll();
        }

        public static DataTable getMaterialUnitsBase(int stock, IEnvironment env, DataTable matUnits)
        {
            IPagedSource s = new PagedSourceUnits(env);
            for (int i = 0; i < matUnits.Rows.Count; ++i)
                s.getBuilder().addParameterValue(TableUNITSETL.LOGICALREF, ToolCell.isNull(matUnits.Rows[i][TableITMUNITA.UNITLINEREF], LRef.undef), SqlTypeRelations.equal, SqlTypeRelations.boolOr);
            return s.getAll();
        }

        public static DataTable getMainUnit(DataTable tableMatUnits)
        {
            DataRow uRow = null;
            for (int i = 0; i < tableMatUnits.Rows.Count; ++i)
            {
                DataRow row = tableMatUnits.Rows[i];
                if ((short)row[TableITMUNITA.LINENR] == (short)1)
                {
                    uRow = row;
                    break;
                }
            }
            DataTable tab = tableMatUnits.Clone();
            ToolRow.copyRowToTable(uRow, tab);
            return tab;
        }
        public static DataTable getSmallUnit(DataTable tableMatUnits)
        {
            DataRow uRow = null;
            double cfLast = double.MaxValue;
            short nrLast = short.MaxValue;
            for (int i = 0; i < tableMatUnits.Rows.Count; ++i)
            {
                DataRow row = tableMatUnits.Rows[i];
                double cfCur = (double)row[TableITMUNITA.CONVFACT2] / (double)row[TableITMUNITA.CONVFACT1];
                short nrCur = (short)row[TableITMUNITA.LINENR];
                if ((cfLast > cfCur) || ((cfLast == cfCur) && (nrCur < nrLast)))
                {
                    uRow = row;
                    cfLast = cfCur;
                    nrLast = nrCur;
                }
            }
            DataTable tab = tableMatUnits.Clone();
            ToolRow.copyRowToTable(uRow, tab);
            return tab;
        }
        public static DataTable getLargeUnit(DataTable tableMatUnits)
        {
            DataRow uRow = null;
            double cfLast = double.MinValue;
            short nrLast = -1;
            for (int i = 0; i < tableMatUnits.Rows.Count; ++i)
            {
                DataRow row = tableMatUnits.Rows[i];
                double cfCur = (double)row[TableITMUNITA.CONVFACT2] / (double)row[TableITMUNITA.CONVFACT1];
                short nrCur = (short)row[TableITMUNITA.LINENR];
                if ((cfLast < cfCur) || ((cfLast == cfCur) && (nrCur > nrLast)))
                {
                    uRow = row;
                    cfLast = cfCur;
                    nrLast = nrCur;
                }
            }

            DataTable tab = tableMatUnits.Clone();
            ToolRow.copyRowToTable(uRow, tab);
            return tab;
        }

        public static DataTable getConvertedForNewMainUnit(int unit, DataTable tableMatUnits)
        {
            DataTable newData = tableMatUnits.Copy();
            double cf = getMatUnitCF(unit, tableMatUnits);
            for (int i = 0; i < newData.Rows.Count; ++i)
            {
                DataRow row = newData.Rows[i];
                row[TableITMUNITA.CONVFACT2] = (double)row[TableITMUNITA.CONVFACT1] / cf;
            }
            return newData;
        }

        public static double getMatUnitCF(int unit, DataTable tableMatUnits)
        {
            for (int i = 0; i < tableMatUnits.Rows.Count; ++i)
                if (((int)tableMatUnits.Rows[i][TableITMUNITA.UNITLINEREF] == unit))
                    return (double)tableMatUnits.Rows[i][TableITMUNITA.CONVFACT2] / (double)tableMatUnits.Rows[i][TableITMUNITA.CONVFACT1];
            return (int)1;
        }

        public static DataTable getMatUnitPrices(int unit, DateTime date, IEnvironment env, DataTable tablePrices, DataTable tableMatUnits)
        {
            DataTable resPrices = tablePrices.Clone();
            ToolColumn.add(resPrices, TablePRCLIST.E_DUMMY__PRICE, typeof(double));
            for (int i = 0; i < tablePrices.Rows.Count; ++i)
            {
                DataRow row = tablePrices.Rows[i];
                short convertable = (short)row[TablePRCLIST.UNITCONVERT];
                int curUnit = (int)row[TablePRCLIST.UOMREF];

                if ((convertable == (short)ConstBool.yes) || (curUnit == unit))
                    if (ToolDateTime.isBetween(date, (DateTime)row[TablePRCLIST.BEGDATE], (DateTime)row[TablePRCLIST.ENDDATE]))
                    {
                        DataRow curRow = ToolRow.copyRowToTable(row, resPrices);
                        double cf = getMatUnitCF(curUnit, tableMatUnits);
                        double cf_ = getMatUnitCF(unit, tableMatUnits);
                        double price = (double)curRow[TablePRCLIST.PRICE];
                        price = (price / cf) * cf_;
                        curRow[TablePRCLIST.PRICE] = price;
                        double exchange = ToolGeneral.getExchange((short)curRow[TablePRCLIST.CURRENCY], date, env);
                        curRow[TablePRCLIST.CURRENCY] = env.getInfoApplication().periodCurrencyNativeId;
                        if (exchange > ConstValues.minPositive)
                            price /= exchange;
                        else
                            price = 0;
                        curRow[TablePRCLIST.E_DUMMY__PRICE] = price;
                        curRow[TablePRCLIST.UOMREF] = unit;
                    }
            }
            return resPrices;
        }

    }
}
