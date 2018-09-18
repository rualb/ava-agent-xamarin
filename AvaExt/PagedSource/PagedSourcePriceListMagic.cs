using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;

namespace AvaExt.PagedSource
{
    public class PagedSourcePriceListMagic : PagedSourcePriceList
    {
        public PagedSourcePriceListMagic(IEnvironment pEnv)
            : base(pEnv)
        {
            getBuilder().addColumnToMeta(TablePRCLIST.E_DUMMY__PRICE, typeof(double));
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
            int stock = (int)ToolCell.isNull(getBuilder().getParameterValue(TablePRCLIST.CARDREF), (int)0);
            int unit = (int)ToolCell.isNull(getBuilder().getParameterValue(TablePRCLIST.UOMREF), (int)0);
            DateTime date = (DateTime)ToolCell.isNull(getBuilder().getParameterValue(TablePRCLIST.BEGDATE), DateTime.Now);
            getBuilder().deleteParameter(TablePRCLIST.TABLE, TablePRCLIST.UOMREF);
            getBuilder().deleteParameter(TablePRCLIST.TABLE, TablePRCLIST.BEGDATE);
            DataTable prices = base.get();
            IPagedSource source = new PagedSourceMaterialUnits(environment);
            source.getBuilder().addParameterValue(TableITMUNITA.ITEMREF, stock);
            return ToolMaterial.getMatUnitPrices(unit, date, environment, prices, source.get());
        }
    }
}
