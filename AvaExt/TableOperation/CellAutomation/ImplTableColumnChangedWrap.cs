using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;
using AvaExt.TableOperation.CellAutomation.TableEvents;

namespace AvaExt.TableOperation.CellAutomation
{
    public class ImplTableColumnChangedWrap : ITableColumnChange
    {
        WorkerStart worker;
        public ImplTableColumnChangedWrap(WorkerStart pWorker)
        {
            worker = pWorker;
        }
        public void columnChange(DataColumnChangeEventArgs e)
        {
             worker.Invoke();
        }

        public virtual void initForColumnChanged(DataRow pRow)
        {

        }
    }
}
