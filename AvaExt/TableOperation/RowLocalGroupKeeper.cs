using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.Common;
using AvaExt.TableOperation.RowValidator;
using AvaExt.TableOperation.RowsSelector;

namespace AvaExt.TableOperation
{
    public class RowLocalGroupKeeper : BlockHandler
    {

        IRowsSelector forTop;
        IRowsSelector forSub;
        IRowValidator validator;
        public RowLocalGroupKeeper(DataTable table, IRowsSelector pForTop, IRowsSelector pForSub, IRowValidator pValidator)
        {
            //
            forTop = pForTop;
            forSub = pForSub;
            validator = pValidator;
            table.RowDeleting += new DataRowChangeEventHandler(table_RowDeleting);

        }

        void table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            if (validator.check(e.Row))
                if (block())
                {
                    try
                    {

                        DataTable table = e.Row.Table;
                        DataRow[] topRows = forTop.get(e.Row);
                        DataRow[] subRows = forSub.get(e.Row);
                        if (topRows != null && subRows != null)
                            for (int i = 0; i < topRows.Length; ++i)
                                if (object.ReferenceEquals(topRows[i], e.Row))
                                    for (int r = 0; r < subRows.Length; ++r)
                                        subRows[r].Delete();
                    }
 
                    finally
                    {
                        unblock();
                    }
                }
        }













    }

}

