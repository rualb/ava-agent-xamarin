using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.Const;
using System.Data;
using AvaExt.Adapter.ForDataSet;
using AvaExt.TableOperation.CellAutomation;

namespace AvaExt.Adapter.ForUser
{
    public interface IAdapterUser
    {
        void add();
        void edit(object pId);
        void delete(object pId);
        void delete();
        object update();
        void clear();
        void flagEnable(AdapterUserFlags flag);
        void flagDisable(AdapterUserFlags flag);
        bool isFlagEnabled(AdapterUserFlags flag);
        AdapterWorkState getAdapterWorkState();
        DataSet getDataSet();
        IAdapterDataSet getAdapterDataSet();
        DataRow addRowToTable(DataTable table);
        void resetRefs();
        void setAdded();
        void initCopy();  
    }
}
