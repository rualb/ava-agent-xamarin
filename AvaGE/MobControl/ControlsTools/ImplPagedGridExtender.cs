using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.PagedSource;
using System.Data;
using AvaExt.Common;
using AvaExt.TableOperation;
using AvaGE.MobControl;
using AvaExt.MyException;
using AvaExt.SQL.Dynamic;


using System.ComponentModel;
using AvaGE.MobControl.ControlsTools.UserMessanger;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Table;


namespace AvaGE.MobControl.ControlsTools
{
    public class ImplPagedGridExtender : IPagedDataAction
    {

        MobDataGrid grid;
        //MobVScrollBar scroll;
        IPagedSource source;

        BlockHandler mutex = new BlockHandler();
        string[] primaryKey;

        IUserMessanger messanger = new UserMessangerChildDummy();
        Dictionary<string, string[]> sortIndexes = new Dictionary<string, string[]>();
        //
        string searchedText = string.Empty;

        string globalId;
        string stateNameSort { get { return globalId + "/extender/sortstring"; } }

        public ImplPagedGridExtender(MobDataGrid pGrid, IPagedSource pSource, string pGlobalId)
        {
            globalId = pGlobalId;
            //
            grid = pGrid;
            source = pSource;
            //
            source.getBuilder().reset();
            int indx = -1;
            try
            {
                indx = source.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableDUMMY.ID, ToolLRef.dummyLRef));
                DataTable tab_ = source.getAll();
                initTable(tab_);
                grid.DataSource = tab_;
                tab_.DefaultView.ListChanged += DefaultView_ListChanged;
            }
            finally
            {
                if (indx > 0)
                    source.getBuilder().deletePreparer(indx);
            }

            //grid.KeyPress += new KeyPressEventHandler(grid_KeyPress_Search);
            //grid.KeyDown += new KeyEventHandler(grid_KeyDown_Search);


        }

        void initTable(DataTable pTable)
        {
            ToolTable.fillNull(pTable);

            string sort_ = ToolMobile.getEnvironment().getStateRuntime(stateNameSort) as string;
            if (sort_ != null && pTable.DefaultView.Sort != sort_)
                pTable.DefaultView.Sort = sort_;
        }

        void DefaultView_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.Reset)
            {
                grid.refreshBind();

                ToolMobile.getEnvironment().setStateRuntime(stateNameSort, grid.DataSourceDataView.Sort);
            }
        }
        //void grid_KeyPress_Search(object sender, KeyPressEventArgs e)
        //{
        //    if (char.IsLetterOrDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
        //    //if (source.getBuilder().getColumnType(grid.sortingColDataPropertyName) == typeof(string))
        //    {
        //        searchedText = string.Empty;
        //        searchedText += e.KeyChar;
        //        searchData(searchedText);
        //    }
        //}
        //void grid_KeyDown_Search(object sender, KeyEventArgs e)
        //{
        //    //if (e.KeyCode == Keys.Back && searchedText.Length > 0)
        //    //{
        //    //    searchedText = string.Empty;
        //    //    searchedText = searchedText.Substring(0, searchedText.Length - 1);
        //    //    searchData(searchedText);
        //    //}
        //}
        string getSortColumn()
        {
            if (getTable() != null && getTable().DefaultView.Sort != string.Empty)
            {
                string sort = getTable().DefaultView.Sort.Trim();
                string[] arr = sort.Split(' ');
                if (arr.Length > 0)
                {
                    if (getTable().Columns.Contains(arr[0]) &&
                        getTable().Columns[arr[0]].DataType == typeof(string))
                        return arr[0];
                }


            }
            return string.Empty;
        }
        public void searchData(object pSearchData)
        {
            if (pSearchData != null && pSearchData.GetType() == typeof(string))
                if (grid != null && getTable() != null)
                {
                    string searchStr = ((string)pSearchData).Trim();
                    if (searchStr != string.Empty)
                    {
                        string searchCol = getSortColumn();
                        if (searchCol != null && searchCol != string.Empty)
                        {
                            int colIndx = grid.DataSourceDataView.Table.Columns.IndexOf(searchCol);
                            foreach (DataRowView v in grid.DataSourceDataView)
                            {

                                if (((string)v[colIndx]).StartsWith(searchStr, StringComparison.OrdinalIgnoreCase))
                                {
                                    grid.ActiveRow = v.Row;
                                    return;
                                }
                            }

                        }
                    }
                }
        }
        //

        //public void setPrimaryKey(string[] pPrimKey)
        //{
        //    if (grid != null && grid.DataSourceDataView != null)
        //        ToolTable.addPrimaryKey(grid.DataSourceDataView.Table, primaryKey = pPrimKey);
        //}

        //public void addSortIndex(string column, string[] index)
        //{
        //    sortIndexes.Add(column, index);
        //}

        public DataTable getTable()
        {
            if (grid != null && grid.DataSourceDataView != null)
                return grid.DataSourceDataView.Table;

            return null;
        }


        //public void goToFirst()
        //{

        //}
        //public void goToLast()
        //{

        //}
        public void refresh(DataRow row)
        {

        }

        public void loadData(DataTable pTab)
        {
            if (getTable() == null)
                return;

            try
            {
                grid.beginDataLoad();
                getTable().BeginLoadData();
                //
                getTable().Clear();
                getTable().Load(pTab.CreateDataReader());
            }
            finally
            {
                getTable().EndLoadData();
                grid.endDataLoad();
            }

        }
        public DataTable refresh()
        {
            if (getTable() == null)
                return null;

            source.getBuilder().reset();

            if (startigDataLoad != null)
                startigDataLoad.Invoke(this, EventArgs.Empty);

            DataTable tab_ = source.getAll();
            ToolTable.fillNull(tab_);

            loadData(tab_);

            return tab_;

        }

        public void requiredSortedData(string column, bool sortAsc)
        {
            grid.sort(column, sortAsc);
        }

        public void setMessanger(IUserMessanger pMessanger)
        {
            messanger = pMessanger;
        }

        public void searchRecord(object obj)
        {

            if (obj == null)
                return;

            object recRef = null;

            if (recRef == null)
            {
                object val = obj as string;
                if (val != null)
                    recRef = val;
            }

            if (recRef == null)
            {
                DataRow val = obj as DataRow;
                if (val != null && val.RowState != DataRowState.Deleted)
                {
                    DataColumn col = val.Table.Columns[TableDUMMY.LOGICALREF];
                    recRef = col != null ? val[col] : null;
                }
            }


            if (recRef != null && getTable() != null)
            {
                DataColumn col = getTable().Columns[TableDUMMY.LOGICALREF];
                if (col != null)
                {
                    foreach (DataRowView v in getTable().DefaultView)
                    {
                        if (ToolType.isEqual(v.Row[col], recRef))
                        {
                            grid.ActiveRow = v.Row;
                            return;
                        }
                    }
                }
            }
        }






        public object getRecordId()
        {
            DataRow row = grid.ActiveRow;
            if (row != null)
                return row[TableDUMMY.LOGICALREF];
            return null;
        }




        public event EventHandler startigDataLoad;
    }
}
