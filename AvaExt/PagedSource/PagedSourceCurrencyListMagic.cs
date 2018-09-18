using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;

namespace AvaExt.PagedSource
{
    public class PagedSourceCurrencyListMagic : PagedSourceCurrencyList
    {
        public PagedSourceCurrencyListMagic(IEnvironment pEnv)
            : base(pEnv)
        {

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
             
           
            short curr = (short)ToolCell.isNull(getBuilder().getParameterValue(TableCURRENCYLIST.CURTYPE), (short)-100);
            if (curr == 0)
            {
                getBuilder().deleteParameter(TableCURRENCYLIST.CURTYPE);
                getBuilder().addParameterValue(TableCURRENCYLIST.CURTYPE, environment.getInfoApplication().periodCurrencyNativeId);
            }

            return base.get();

        }


    }
}
