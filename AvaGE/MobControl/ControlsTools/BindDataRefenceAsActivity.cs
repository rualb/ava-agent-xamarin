using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.DataRefernce;
using AvaExt.Common;
using AvaExt.AndroidEnv.ControlsBase;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.ObjectSource;
using AvaExt.TableOperation.RowValidator;

namespace AvaGE.MobControl.ControlsTools
{
    public class BindDataRefenceAsActivity : IActivity
    {
        IDataReference reference;
        string[] colSource;
        string[] colDest;
        IRowSource rowSource;
        string filterName;
        IObjectSource filterDataSource;

        public BindDataRefenceAsActivity(IDataReference pReference, IRowSource pRowSource, string pFilterName, IObjectSource pFilterDataSource, string[] pColSource, string[] pColDest)
        {
            reference = pReference;
            colSource = pColSource;
            colDest = pColDest;
            rowSource = pRowSource;
            filterName = pFilterName;
            filterDataSource = pFilterDataSource;

        }


        public object done()
        {
            reference.begin(filterName, filterDataSource.get(), false, delegate(object o, EventArgs a)
            {

                DataRow row = rowSource.get();
                if (row != null && row.RowState != DataRowState.Deleted)
                    for (int i = 0; i < colSource.Length; ++i)
                        ToolCell.set(row, colDest[i], reference.getSelected()[colSource[i]]);

            });

            return null;
        }


        public void Dispose()
        {
            reference = null;
            rowSource = null;
            filterDataSource = null;
        }
    }
}
