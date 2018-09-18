using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.PagedSource;

namespace AvaGE.FormDataReference.UserForm
{
    public class MobDataReferenceForOrderForm : MobDataReferenceWithAdapterForm
    {
        public MobDataReferenceForOrderForm(int pLayout)
            : base(pLayout)
        {


        }

        //protected override DataRow getRecordInDB(DataRow dataRow)
        //{
        //    IPagedSource ps = new PagedSourceOrder(environment);
        //    ps.getBuilder().addParameterValue(TableDUMMY.LOGICALREF, dataRow[TableDUMMY.LOGICALREF]);
        //    DataTable tab = ps.getAll();
        //    if (tab.Rows.Count > 0)
        //        return tab.Rows[0];
        //    return null;
        //}
    }
}

