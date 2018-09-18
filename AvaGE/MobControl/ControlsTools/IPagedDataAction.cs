using System;
using System.Collections.Generic;
using System.Text;
using AvaGE.MobControl;

using System.Data;
using AvaGE.MobControl.ControlsTools.UserMessanger;

namespace AvaGE.MobControl.ControlsTools
{
    public interface IPagedDataAction
    {
        void setMessanger(IUserMessanger pMessanger);
        void requiredSortedData(string column, bool sortAsc);
        //void goToFirst();
        //void goToLast();
        void refresh(DataRow row);
        DataTable refresh();
        void loadData(DataTable pTab);
        // void setPrimaryKey(string[] pPrimKey);
        //  void addSortIndex(string column, string[] index);
        void searchData(object pSearchData);
        void searchRecord(object id);
        object getRecordId();
        DataTable getTable();
        event EventHandler startigDataLoad;
    }
}
