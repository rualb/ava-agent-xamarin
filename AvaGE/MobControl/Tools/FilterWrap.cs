using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;

namespace AvaGE.MobControl.Tools
{
    public class FilterWrapArgs : EventArgs
    {

        public FilterWrapArgs(object[] pArgs)
        {
            args = pArgs;
        }

        public object[] args;

    }
    public interface IFilterWrap : IDisposable
    {

        event EventHandler<FilterWrapArgs> FilterChanged;

        void fill(object[][] pValues);

        object[] Selected { get; set; }
    }

    public class ImplFilterWrapGrid : IFilterWrap
    {

        //  public bool isStoped = false;

        public event EventHandler<FilterWrapArgs> FilterChanged;

        // MobTreeView tree;

        MobDataGrid[] grids;
        object[][] data;


        bool updating = false;
        public ImplFilterWrapGrid(MobDataGrid[] pGrids)
        {
            grids = pGrids;

            foreach (MobDataGrid g in grids)
            {
                g.RowSelected += g_RowSelected;
                g.isSortEnabled = false;
                g.isNewRowActivated = false;
                g.isColHeaderVisible = false;

                try
                {
                    g.beginDataLoad();
                    g.Columns.Add(TableDUMMY.LOGICALREF, string.Empty);
                    DataTable tab = new DataTable();
                    tab.Columns.Add(TableDUMMY.LOGICALREF, ToolTypeSet.helper.tObject);
                    g.DataSource = tab;
                }
                finally
                {
                    g.endDataLoad();
                }
            }

        }



        int getGridLevel(MobDataGrid pGrid)
        {
            int indx = -1;
            foreach (object o in grids)
            {
                ++indx;
                if (object.ReferenceEquals(pGrid, o))
                    return indx;
            }

            return -1;
        }
        void g_RowSelected(object sender, MobDataGrid.EventArgsGrid e)
        {
            int indx_ = getGridLevel(sender as MobDataGrid);
            if (indx_ < 0)
                return;

            object[] keys_ = getGridsSeletion(indx_);
            Selected = keys_;
        }

        object[] getGridsSeletion(int pMaxLevel)
        {

            List<object> list = new List<object>();

            for (int i = 0; i < Math.Min(pMaxLevel+1, grids.Length); ++i)
            {
                MobDataGrid g = grids[i];
                var r = g.ActiveRow as DataRow;
                if (r == null) //not selected
                    break;

                list.Add(r[0]);
            }

            return list.ToArray();
        }



        void OnFilterChanged(FilterWrapArgs pArgs)
        {
            if (FilterChanged != null)
                FilterChanged.Invoke(this, pArgs);
        }

        public void fill(object[][] pValues)
        {
            if (pValues == null)
                return;

            data = pValues;

            Selected = new object[0];

        }



        object[] selectValues(object[] pKeys)
        {
            if (pKeys == null)
                pKeys = new object[0];

            List<object> list = new List<object>();

            foreach (object[] arr in data)
                if (pKeys.Length < arr.Length)
                {
                    bool select_ = ToolArray.isEqual(pKeys, ToolArray.resize<object>(arr, pKeys.Length));
                    if (select_)
                    {
                        if (pKeys.Length < arr.Length)
                        {
                            object val_ = arr[pKeys.Length];

                            if (!list.Contains(val_))
                                list.Add(val_);
                        }

                    }
                }

            return list.ToArray();
        }

        DataRow searchRecord(DataTable pTab, object pKey)
        {
            return ToolRow.search(pTab, TableDUMMY.LOGICALREF, pKey);
        }

        bool isLastLevel(int pLev)
        {
            return pLev == grids.Length - 1;
        }

        object[] _Selected;
        public object[] Selected
        {
            get
            {
                return _Selected;
            }
            set
            {

                if (value == null)
                    return;


                if (updating)
                    return;

                try
                {
                    updating = true;



                    for (int level_ = 0; level_ < grids.Length; ++level_)
                    {
                        MobDataGrid g = grids[level_];

                        try
                        {
                            g.beginDataLoad();

                            DataTable tab_ = g.DataSource as DataTable;
                            if (tab_ == null)
                                return;

                            bool refresh_ = true;
                            bool hasPath_ = (level_ <= value.Length);
                            bool hasKey_ = (level_ < value.Length);
                            object key_ = hasKey_ ? value[level_] : null;

                            if (hasKey_)
                            {
                                //
                                DataRow row_ = searchRecord(tab_, key_);
                                if (row_ != null)
                                    refresh_ = false;
                            }


                            if (refresh_)
                            {
                                tab_.Clear();
                                g.SetActiveRow(-1);

                                if (hasPath_)
                                {
                                    object[] vals_ = selectValues(ToolArray.resize<object>(value, level_));
                                    foreach (object o in vals_)
                                        tab_.Rows.Add(o);
                                }
                            }

                            if (hasKey_)
                            {
                                //
                                DataRow row_ = searchRecord(tab_, key_);
                                if (row_ != null && !object.ReferenceEquals(row_, g.ActiveRow))
                                    g.ActiveRow = row_;



                                if (isLastLevel(level_))
                                    OnFilterChanged(new FilterWrapArgs(value));
                            }


                        }
                        finally
                        {
                            g.endDataLoad();

                            _Selected = value;
                        }


                    }

                }
                finally
                {
                    updating = false;
                }


            }
        }





        public void Dispose()
        {
            grids = null;


            data = null;
        }
    }


    public class ImplFilterWrapTree : IFilterWrap
    {

        //  public bool isStoped = false;

        public event EventHandler<FilterWrapArgs> FilterChanged;

        MobTreeView tree;

        public ImplFilterWrapTree(MobTreeView pTree)
        {
            tree = pTree;
            //
            tree.NodeClick += tree_NodeClick;
        }

        private void tree_NodeClick(object sender, MobTreeView.EventArgsNode e)
        {
            if (e.Node == null)
                return;

            if (e.Node.getChildCount() > 0)
                return;

            object[] val_ = MobTreeView.getNodeTagTree(e.Node);


            Selected = val_;

        }

        void OnFilterChanged(FilterWrapArgs pArgs)
        {
            if (FilterChanged != null)
                FilterChanged.Invoke(this, pArgs);
        }

        public void fill(object[][] pValues)
        {
            tree.fillTree(pValues);
        }

        object[] _Selected;
        public object[] Selected
        {
            get
            {
                return _Selected;
            }
            set
            {

                MobTreeView.Node lastNode_ = tree.RootNode;
                if (lastNode_ != null && value != null)
                {
                    foreach (object v in value)
                        lastNode_ = lastNode_.Search(v.ToString());

                    if (lastNode_ != null && !object.ReferenceEquals(tree.SelectedNode, lastNode_))
                    {
                        _Selected = value;

                        tree.SelectedNode = lastNode_;

                        OnFilterChanged(new FilterWrapArgs(value));
                    }
                }
            }
        }





        public void Dispose()
        {
            tree = null;
        }
    }
}