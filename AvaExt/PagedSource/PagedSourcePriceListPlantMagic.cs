using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;
using AvaExt.ObjectSource;
using AvaExt.Common.Const;

namespace AvaExt.PagedSource
{
    public class PagedSourcePriceListPlantMagic : PagedSourceMaterial
    {

        public readonly static string EXTPRICECOL = TableDUMMY.PRICE;
        public readonly static string EXTPRICEID = TableDUMMY.PRICETYPE;


        public PagedSourcePriceListPlantMagic(IEnvironment pEnv)
            : base(pEnv)
        {

            getBuilder().addColumnToMeta(EXTPRICECOL, typeof(double));
            getBuilder().addColumnToMeta(EXTPRICEID, typeof(short));
        }
        public override DataTable getFirst()
        {
            return get();
        }
        public override DataTable getFirst(bool prepareForWhere)
        {
            return get();
        }
        public override DataTable getNext()
        {
            return get();
        }
        public override DataTable getPreviose()
        {
            return get();
        }
        public override DataTable getCurrent()
        {
            return get();
        }
        public override DataTable getLast()
        {
            return get();
        }
        public override DataTable getAll()
        {
            return get();
        }
        public override DataTable get()
        {
            string stock = (string)ToolCell.isNull(getBuilder().getParameterValue(TableITEMS.LOGICALREF), string.Empty);
            short priceid = (short)ToolCell.isNull(getBuilder().getParameterValue(EXTPRICEID), (short)0);


            getBuilder().deleteParameter(EXTPRICEID);


            DataTable prices = base.get();
            string priceCol = getPriceCol(priceid);
            prices.Columns.Add(EXTPRICECOL, typeof(double));
            if (priceid > 0 && prices.Columns.Contains(priceCol))
                ToolColumn.copyColumn(prices, priceCol, EXTPRICECOL);
            else
                ToolColumn.setColumnValue(prices, EXTPRICECOL, 0.0);
            //


            //
            return prices;
        }

        string getPriceCol(int indx)
        {
            return TableDUMMY.PRICE + ToolString.shrincDigit(indx.ToString());
        }
    }
}
