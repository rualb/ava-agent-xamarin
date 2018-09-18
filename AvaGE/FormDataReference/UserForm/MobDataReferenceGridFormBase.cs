using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.DataRefernce;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaGE.MobControl.ControlsTools;
using AvaGE.MobControl.ControlsTools.UserMessanger;
using AvaExt.Settings;
using AvaExt.Translating.Tools;
using AvaGE.Common;

using AvaExt.SQL;
using AvaExt.SQL.Dynamic;
using AvaExt.Manual.Table;
using System.IO;
using AvaGE.FormUserEditor;
using AvaExt.TableOperation;
using AvaGE.MobControl;
using AvaExt.Common.Const;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Adapter.ForUser;
using Android.App;
using Android.Views;
using AvaExt.Reporting;
using AvaGE.MobControl.Reporting.Renders;
using AvaExt.Filter;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.Common;
using Ava_Ext.Common;
using AvaExt.ObjectSource;
using AvaExt.Database;
using AvaExt.MyException;
using AvaGE.MobControl.Tools;
using AvaAgent;

namespace AvaGE.FormDataReference.UserForm
{
    /// <summary>
    /// cTree - filter - cListTopFilter1_AfterSelect
    /// select - cButSelect_Click
    /// cancel - cButCancel_Click
    /// grid 
    /// </summary>

    public class MobDataReferenceGridFormBase : MobFormBase
    {


        bool stopRefreshOnFilterChanged = false;

        string[] _FilterCols = null;
        string[] FilterCols
        {
            get
            {

                if (_FilterCols != null)
                    return _FilterCols;

                string _nodeTopFilter = "TOP_FILTER_" + getGrid().Name;
                //string _attrTopFilterName = "name";
                string _attrTopFilterColumns = "cols";

                string list_ = settings.getStringAttr(_nodeTopFilter, _attrTopFilterColumns).Trim();

                if (list_ != string.Empty)
                    _FilterCols = ToolString.explodeList(list_);
                else
                    _FilterCols = new string[0];

                return _FilterCols;
            }
        }

        bool HasFilter { get { return (FilterCols != null && FilterCols.Length > 0); } }

        object[] _FilterValues = new object[] { };
        object[] FilterValues
        {
            get
            {
                return _FilterValues;
            }
            set
            {
                try
                {
                    if (value == null)
                        value = new object[] { };

                    if (ToolArray.compare(_FilterValues, value) != 0)
                    {
                        _FilterValues = value;

                        saveFilter();
                    }
                }
                catch (Exception exc)
                {
                    ToolMobile.setException(exc);
                }
            }
        }

        IFilterWrap filterWrap;


        public bool showMode { get { return reference == null ? true : reference.getFlagStore().isFlagEnabled(ReferenceFlags.showMode); } } //only show no select
        protected ImplDataReference reference;
        protected IPagedSource source { get { return reference == null ? null : reference.getPagedSource(); } }
        public IPagedDataAction extender;
        protected IUserTopMessanger topMessanger;
        protected MobUserEditorFormBase formEditor = null;
        CmdMenuItem[] menuItems = null;



        StateHelper stateHelper;

        public MobDataReferenceGridFormBase(int pLayout)
            : base(null, pLayout > 0 ? pLayout : Resource.Layout.MobDataReferenceGridFormBase)
        {

            Closed += MobDataReferenceGridFormBase_Closed;

            //

            stateHelper = new StateHelper(globalStoreName());
        }


        string[] quiqFilterColumns_ = null;
        protected virtual string[] getQuiqFilterColumns()
        {
            if (quiqFilterColumns_ == null)
            {
                var s_ = settings.getString(SettingsAvaAgent.MOB_QSEARCH_COLS_S, null);
                quiqFilterColumns_ = string.IsNullOrEmpty(s_) ? new string[0] : ToolString.explodeList(s_);
            }

            if (quiqFilterColumns_.Length > 0)
                return quiqFilterColumns_;

            return null;
        }
        protected virtual string getQuiqFilterColumn(string pPattern)
        {
            var a_ = getQuiqFilterColumns();
            return a_ != null && a_.Length > 0 ? a_[0] : null;
        }
        protected virtual bool cleanQuickFilterAfterSearch(string pPattern)
        {
            return false;
        }
        void MobDataReferenceGridFormBase_Closed(object sender, EventArgs e)
        {
            reference = null;

            extender = null;
            if (topMessanger != null) topMessanger.Dispose();
            topMessanger = null;
            formEditor = null;
            menuItems = null;
            stateHelper = null;
        }



        protected override void initAfterSettings()
        {
            base.initAfterSettings();



            getGrid().isSortEnabled = true;
            getGrid().userDesc = "Main Grid";
            //
            topMessanger = new UserMessangerParentControlText(this);

            //check take from referense

            ImplDataReference ref_ = ToolMobile.getEnvironment().getReference(this.Intent.GetStringExtra(ConstCmdLine.cmd)) as ImplDataReference;
            if (ref_ != null)
            {
                this.setSource(ref_.getPagedSource());

                reference = ref_;

                if (reference.getReferenceMode() != null && reference.getReferenceMode().batchModeIndexes != null)
                {
                    string[] indx_ = reference.getReferenceMode().batchModeIndexes;
                    if (indx_.Length > 0 && !string.IsNullOrEmpty(indx_[0]))
                        getBtnOk().Text = indx_[0];
                }
            }

            getBtnCancel().Click += BtnCancel_Click;
            getBtnOk().Click += BtnOk_Click;

            getBtnClean().Click += BtnClean_Click;
            getBtnSearch().Click += BtnSearch_Click;


            getBtnAdd().Click += BtnAdd_Click;



            //////////////////////////////




            getBtnOk().Visible = !showMode;
            getBtnAdd().Visible = canAdd();

            try
            {
                stopRefreshOnFilterChanged = true;
                getGrid().beginDataLoad();

                loadFilterStruct();
                loadFilterData();

                refresh();

                loadRecordId();

            }
            finally
            {
                stopRefreshOnFilterChanged = false;
                getGrid().endDataLoad();
            }


            getGrid().RowClick += MobDataReferenceGridFormBase_RowClick;
            getGrid().RowLongClick += MobDataReferenceGridFormBase_RowLongClick;
            getGrid().RowSelected += MobDataReferenceGridFormBase_RowSelected;
            //

            //
            RegisterForContextMenu(getGrid()); //before adapter
            getBtnMenu().Click += MobDataReferenceGridFormBase_Click;

            //
            string col_ = Intent.GetStringExtra(TableDUMMY.COLUMN);
            string val_ = Intent.GetStringExtra(TableDUMMY.VALUE);
            if (col_ != null && !string.IsNullOrEmpty(val_))
            {
                this.extender.requiredSortedData(col_, true);
                this.extender.searchData(val_);
            }


            getQuiqFilterPanel().Visible = (getQuiqFilterColumn(null) != null);

            getQuiqFilterText().KeyPress += QuiqFilterText_KeyPress;
        }

        void QuiqFilterText_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;

            if (e.Event.Action == KeyEventActions.Down)
                if (ToolControl.isDone(e.KeyCode, e.Event.Number))
                {
                    e.Handled = true;

                    doQuickFilter();



                }
        }

        void doQuickFilterClean()
        {
           
            try
            {
                

                var ctrl_ = getQuiqFilterText();
                if (ctrl_ == null)
                    return  ;

                var f = ctrl_.Text = string.Empty;

              

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
     

           
        }

        bool _doQuickFilter = false;
        bool doQuickFilter()
        {
            if (_doQuickFilter)
                return false;

            try
            {
                _doQuickFilter = true;

                var ctrl_ = getQuiqFilterText();
                if (ctrl_ == null)
                    return false;

                var f = ctrl_.Text.Trim();

                var c = getQuiqFilterColumn(f);
                if (c == null)
                    return false;

                var hasFilter_ = !string.IsNullOrEmpty(f);

                getFilterGridPanel().Visible = !hasFilter_;


                refresh();

                if (cleanQuickFilterAfterSearch(f))
                    doQuickFilterClean();

                return true;

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
            finally
            {
                _doQuickFilter = false;
            }

            return false;
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {


            if (char.IsDigit(e.Number))
            {
                var ctrl_ = getQuiqFilterText();
                if (ctrl_ != null && ctrl_.Visibility == ViewStates.Visible)
                {
                    ctrl_.Text = ctrl_.Text + e.Number;
                    if (!ctrl_.IsFocused)
                        ctrl_.RequestFocus();
                    return true;
                }
            }

    


            return base.OnKeyDown(keyCode, e);




        }



        void MobDataReferenceGridFormBase_RowSelected(object sender, MobDataGrid.EventArgsGrid e)
        {
            stateHelper.recordIdSave(e.Row);
        }





        void MobDataReferenceGridFormBase_Click(object sender, EventArgs e)
        {
            this.OpenContextMenu(getGrid());
        }

        void MobDataReferenceGridFormBase_RowLongClick(object sender, MobDataGrid.EventArgsGrid e)
        {
            this.OpenContextMenu(getGrid());
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            //  base.OnCreateContextMenu(menu, v, menuInfo);

            foreach (CmdMenuItem itm in getMenuItems(getGrid().ActiveRow))
            {

                IMenuItem m = menu.Add(0, (int)itm.CmdType, 0, itm.Text);

                switch (itm.CmdType)
                {
                    case CmdType.refresh:
                        //  int id_ = this.Resources.GetIdentifier("ic_menu_refresh", "drawable", "android");
                        //  m.SetIcon(id_);
                        break;


                }

            }

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {



            return base.OnCreateOptionsMenu(menu);


        }



        protected virtual void fillMenuItems(IContextMenu menu)
        {







        }

        public override bool OnContextItemSelected(IMenuItem item)
        {

            doCmd((CmdType)item.ItemId);


            return base.OnContextItemSelected(item);


        }



        void MobDataReferenceGridFormBase_RowClick(object sender, MobDataGrid.EventArgsGrid e)
        {

        }



        protected virtual void returnData(DataRow row)
        {
            try
            {
                if (row != null && reference != null)
                {
                    if (reference.addData(row))
                        userCommitDone(true);

                }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
        }
        void BtnSearch_Click(object sender, EventArgs e)
        {
            doQuickFilter();
        }
        void BtnClean_Click(object sender, EventArgs e)
        {

            doQuickFilterClean();
        }
        void BtnOk_Click(object sender, EventArgs e)
        {
            userRequireCommit();

        }
        void BtnAdd_Click(object sender, EventArgs e)
        {
            doAdd();

        }

        protected override void userRequireCommit()
        {
            if (!showMode)
                returnData(getGrid().ActiveRow);
        }



        void BtnCancel_Click(object sender, EventArgs e)
        {
            userRequireCancel();
        }

        protected override void userCancelDone()
        {
            reference.clear();

            base.userCancelDone();
        }

        public override void OnBackPressed()
        {
            userRequireCancel();
        }




        public MobButton getBtnMenu()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnMenu);
        }
        public MobDataGrid getGrid()
        {
            return FindViewById<MobDataGrid>(Resource.Id.cGrid);
        }
        //
        public MobTreeView getFilterTree()
        {
            return FindViewById<MobTreeView>(Resource.Id.cTreeF); ;
        }
        public MobPanel getFilterTreePanel()
        {
            return FindViewById<MobPanel>(Resource.Id.cPanelFilter); ;
        }
        //
        public MobDataGrid getFilterGrid1()
        {
            return FindViewById<MobDataGrid>(Resource.Id.cGridFL1); ;
        }
        public MobDataGrid getFilterGrid2()
        {
            return FindViewById<MobDataGrid>(Resource.Id.cGridFL2); ;
        }
        public MobPanel getFilterGridPanel()
        {
            return FindViewById<MobPanel>(Resource.Id.cPanelFilter2); ;
        }
        //
        public MobButton getBtnSearch()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnSearch);
        }
        public MobButton getBtnClean()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnClean);
        }
        public MobButton getBtnOk()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnOk);
        }
        public MobButton getBtnAdd()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnAdd);
        }
        public MobButton getBtnCancel()
        {
            return FindViewById<MobButton>(Resource.Id.cBtnCancel);
        }

        public MobPanel getQuiqFilterPanel()
        {
            return FindViewById<MobPanel>(Resource.Id.cPanelFilter3); ;
        }
        //
        public MobTextBox getQuiqFilterText()
        {
            return FindViewById<MobTextBox>(Resource.Id.cFilterText);
        }

        protected DataTable getTable()
        {
            if (getGrid() != null && getGrid().DataSourceDataView != null)
                return getGrid().DataSourceDataView.Table;
            return null;
        }

        protected virtual void setSource(IPagedSource pSource)
        {

            getGrid().initForPaging(pSource, globalStoreName());
            extender = getGrid().getPagingExtender();
            extender.startigDataLoad += extender_startigDataLoad;
            //


        }

        void extender_startigDataLoad(object sender, EventArgs e)
        {
            bool useTreeFilter = true;


            {

                var ctrl_ = getQuiqFilterText();
                if (ctrl_ != null)
                {
                    var text_ = ctrl_.Text.Trim();

                    var col_ = getQuiqFilterColumn(text_);

                    if (col_ != null)
                    {

                        if (string.IsNullOrEmpty(text_))
                        {
                            useTreeFilter = true;
                        }
                        else
                        {
                            useTreeFilter = false;
                            //

                            source.getBuilder().beginWhereGroup();
                            source.getBuilder().addParameterValue(col_, "%" + text_ + "%", SqlTypeRelations.like);
                            source.getBuilder().endWhereGroup();
                        }
                    }
                }
            }

            if (HasFilter && useTreeFilter)
            {

                source.getBuilder().beginWhereGroup();

                for (int i = 0; i < FilterCols.Length; ++i)
                    source.getBuilder().addParameterValue(FilterCols[i], i < FilterValues.Length ? FilterValues[i] : DBNull.Value);

                source.getBuilder().endWhereGroup();
            }
        }



        protected override void OnResume()
        {
            base.OnResume();

            if (reference != null && getGrid() != null && getGrid().ActiveRow != null)
            {
                if (reference.getFlagStore().isFlagEnabled(ReferenceFlags.formBatchMode))
                {
                    _fillSpeColumns(getGrid().ActiveRow);
                }
            }
        }



        protected DataRow getGridCurDataRow()
        {
            if (getGrid() != null)
                return getGrid().ActiveRow;
            return null;
        }


        public virtual void loadFilterStruct()
        {

            try
            {
                getGrid().beginDataLoad();

                if (!HasFilter)
                {

                    getFilterTreePanel().Visible = false;
                    getFilterGridPanel().Visible = false;
                }
                else
                {
                    getFilterTreePanel().Visible = false;
                    List<MobDataGrid> list = new List<MobDataGrid>();
                    {
                        if (FilterCols.Length > 0)
                            list.Add(getFilterGrid1());
                        if (FilterCols.Length > 1)
                            list.Add(getFilterGrid2());
                        else
                            getFilterGrid2().Visible = false;
                    }

                    filterWrap = new ImplFilterWrapGrid(list.ToArray());
                    //filterWrap = new ImplFilterWrapTree(getFilterTree());
                    filterWrap.FilterChanged += filterWrap_FilterChanged;
                    //getFilter().NodeClick += MobDataReferenceGridFormBase_NodeClick;

                    string stateNameFilterTree = globalStoreName() + "/filtertree";

                    object[][] tree_ = environment.getStateData(stateNameFilterTree) as object[][];

                    if (tree_ == null)
                    {
                        string name_ = getDsName();
                        ITableDescriptor td_ = environment.getDbDescriptor().getTable(name_);
                        if (td_ == null)
                            throw new MyExceptionError("Cant find table [" + name_ + "] descriptor");

                        string tabFullName_ = td_.getNameFull();

                        string sql_ = string.Format("SELECT DISTINCT {0} FROM {1}", ToolString.joinList(FilterCols), tabFullName_);
                        DataTable tab_ = SqlExecute.execute(environment, sql_, null);
                        tree_ = ToolTable.getGroups(tab_, FilterCols);

                    }

                    filterWrap.fill(tree_);
                    // getFilter().fillTree(tree_);


                    environment.setStateData(stateNameFilterTree, tree_);

                }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
            finally
            {
                getGrid().endDataLoad();
            }
        }



        protected virtual string getDsName()
        {
            return source.getBuilder().getName();
        }
        public virtual void refresh(object obj)
        {

           

            try
            {
                getGrid().beginDataLoad();
                //

                //
                DataTable table = (DataTable)getGrid().DataSource;

                if (extender != null && environment != null)
                {

                    extender.refresh();
                    //
                    convertData(table);
                    //
                    //
                    _fillSpeColumns(null);
                    //
                    getGrid().refreshBind(); //sql columns

                    extender.searchRecord(obj);
                }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
            finally
            {
                getGrid().endDataLoad();
            }

        }






        protected virtual void convertData(DataTable table)
        {

        }



        protected virtual void convertData(DataRow row)
        {

        }


        private string[] compareData(DataRowView row, DataColumn[] cols, string[] values)
        {
            bool sameData = true;
            string[] curValsStr = new string[values.Length];
            object[] curValues = ToolRow.copyRowToArr(cols, row.Row);
            for (int i = 0; i < curValues.Length; ++i)
            {
                curValsStr[i] = (string)curValues[i];
                if (sameData)
                    if ((string)curValues[i] != values[i])
                        sameData = false;
            }
            return (sameData ? null : curValsStr);
        }



        public virtual void refresh()
        {
            refresh(null);
        }


        protected virtual void doRefresh()
        {
            try
            {

                refresh();

            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
        }
        protected virtual void doRefresh(object pRecId)
        {
            try
            {

                refresh(pRecId);

            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
        }
        protected virtual object doAdd()
        {
            reference.getReferenceMode().lastBatchModeIndex = 0;

            object res = null;
            try
            {
                if (canAdd())
                {

                    res = add();
                    //  refresh(res);
                }
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
            return res;
        }
        protected virtual void doView(DataRow dataRow)
        {
            try
            {
                if (canView(dataRow, false))
                {
                    view(dataRow);
                }
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
        }
        protected virtual object doEdit(DataRow dataRow)
        {
            object res = null;
            try
            {
                if (canEdit(dataRow, false))
                {

                    res = edit(dataRow);
                    //  refresh(res);
                }
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
            return res;
        }
        protected virtual object doCopy(DataRow dataRow)
        {
            object res = null; try
            {
                if (canCopy(dataRow, false))
                {
                    res = copy(dataRow);
                    //  refresh(res);
                }
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
            return res;
        }
        protected virtual void doDelete(DataRow dataRow)
        {

            if (canDelete(dataRow, false))
                ToolMsg.confirm(this, MessageCollection.T_MSG_COMMIT_DELETE, delegate() { _doDelete(dataRow); }, null);

        }
        protected virtual void _doDelete(DataRow dataRow)
        {
            try
            {
                if (canDelete(dataRow, false))
                {
                    delete(dataRow);
                    // refresh();
                }
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
        }
        protected virtual void doInfo(DataRow dataRow)
        {
            DataRow _row = getRecordInDB(dataRow);
            ToolRow.copyRowToRow(_row, dataRow);
        }
        protected virtual DataRow getRecordInDB(DataRow dataRow)
        {
            int prepIndx = -1;
            if (dataRow != null)
            {
                try
                {
                    if (source != null)
                    {
                        source.getBuilder().reset();
                        prepIndx = source.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableDUMMY.LOGICALREF, dataRow[TableDUMMY.LOGICALREF]));
                        DataTable tab = source.get();
                        if (tab.Rows.Count > 0)
                        {
                            DataRow rowInDb = tab.Rows[0];
                            convertData(rowInDb);
                            saveSpeCols(dataRow, rowInDb);
                            return rowInDb;
                        }
                    }
                }
                finally
                {
                    if (source != null && prepIndx >= 0)
                        source.getBuilder().deletePreparer(prepIndx);

                }
            }
            return null;
        }

        private void saveSpeCols(DataRow dataRow, DataRow rowInDb)
        {
            if (rowInDb != null && dataRow != null)
            {
                foreach (DataColumn col in dataRow.Table.Columns)
                    if (col.ColumnName.StartsWith("SPE_"))
                        ToolCell.set(rowInDb, col.ColumnName, dataRow[col]);
            }
        }
        protected virtual bool canAdd()
        {
            if (ToolMobile.isReader())
                return false;

            return true;
        }
        protected virtual bool canView(DataRow dataRow, bool pDbRow)
        {
            return (dataRow != null);
        }

        protected virtual bool canEdit(DataRow dataRow, bool pDbRow)
        {
            DataRow _row = pDbRow ? dataRow : getRecordInDB(dataRow);
            if (_row == null || isReadOnly(_row))
                return false;
            return true;
        }
        protected virtual bool canCopy(DataRow dataRow, bool pDbRow)
        {
            return (dataRow != null);
        }
        protected virtual bool canDelete(DataRow dataRow, bool pDbRow)
        {
            DataRow _row = pDbRow ? dataRow : getRecordInDB(dataRow);
            if (_row == null || isReadOnly(_row))
                return false;
            return true;
        }
        protected virtual object add()
        {
            return null;
        }
        protected virtual void view(DataRow dataRow)
        {

            if (!isValidRecord(dataRow))
                throw new AvaExt.MyException.MyExceptionProcessStoped(MessageCollection.T_MSG_INVALID_RECODR);
        }
        protected virtual object edit(DataRow dataRow)
        {
            if (isReadOnly(dataRow))
                throw new AvaExt.MyException.MyExceptionProcessStoped(MessageCollection.T_MSG_ERROR_EDIT);
            if (!isValidRecord(dataRow))
                throw new AvaExt.MyException.MyExceptionProcessStoped(MessageCollection.T_MSG_INVALID_RECODR);
            return null;
        }
        protected virtual object copy(DataRow dataRow)
        {
            if (!isValidRecord(dataRow))
                throw new AvaExt.MyException.MyExceptionProcessStoped(MessageCollection.T_MSG_INVALID_RECODR);
            return null;
        }
        protected virtual void delete(DataRow dataRow)
        {
            if (isReadOnly(dataRow))
                throw new AvaExt.MyException.MyExceptionProcessStoped(MessageCollection.T_MSG_ERROR_EDIT);

        }

        protected virtual void info(DataRow dataRow)
        {

        }
        protected virtual bool isReadOnly(DataRow dataRow)
        {
            if (dataRow == null)
                return true;
            //
            {

                DataColumn col_ = dataRow.Table.Columns[TableDUMMY.READONLY];
                if (col_ != null)
                {
                    if (Convert.ToInt16(dataRow[col_]) == (short)ConstBool.yes)
                        return true;
                }

            }
            //
            {

                DataColumn col_ = dataRow.Table.Columns[TableDUMMY.RECVERS];
                if (col_ != null)
                {
                    if (Convert.ToInt16(dataRow[col_]) == short.MaxValue)
                        return true;
                }

            }
            return false;

        }
        protected virtual bool isValidRecord(DataRow dataRow)
        {
            return true;
        }




        protected virtual DataRow getCurentRec()
        {

            return getGridCurDataRow();
        }



        private void grid_DoubleClick(object sender, EventArgs e)
        {
            //check
            //if (getGrid().getLastHitTestInfo() != null && getGrid().getLastHitTestInfo().Type == DataGrid.HitTestType.Cell)
            //{
            //    if (!showMode)
            //        returnData(getGrid().getGridCurDataRow());
            //    else
            //        doEdit(getGrid().getGridCurDataRow());
            //}
        }

        private void cListTopFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cTopFilter.SelectedValue != null)
            //{
            //    string col = (string)cTopFilter.Tag;
            //    DataTable table = (DataTable)getGrid().DataSource;
            //    table.DefaultView.RowFilter = string.Format("{0}='{1}'", col, cTopFilter.SelectedValue);
            //}
        }
        private void cListTopFilter_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        void filterWrap_FilterChanged(object sender, FilterWrapArgs e)
        {


            if (FilterCols != null && FilterCols.Length > 0 && e.args != null)
            {
                FilterValues = e.args;

                if (!stopRefreshOnFilterChanged)
                    doRefresh();

            }
        }

        //void MobDataReferenceGridFormBase_NodeClick(object sender, MobTreeView.EventArgsNode e)
        //{

        //    if (treeFilterStoped)
        //        return;

        //    if (FilterCols != null && FilterCols.Length > 0 && e.Node != null && e.Node.getChildCount() == 0)
        //    {
        //        setFilter(e.Node);

        //        doRefresh();

        //    }
        //}
        void setFilter(MobTreeView.Node pNode)
        {
            if (pNode != null)
                FilterValues = MobTreeView.getNodeTagTree(pNode);
        }


        void saveFilter()
        {
            stateHelper.filterSave(FilterCols, FilterValues);
        }

        void loadRecordId()
        {
            try
            {
                object val = stateHelper.recordIdLoad();

                if (extender != null)
                    extender.searchRecord(val);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
        }


        void loadFilterData()
        {
            try
            {
                object[] vals_ = stateHelper.filterLoad(FilterCols);

                if (filterWrap != null)
                    filterWrap.Selected = vals_;

                //if (vals_ == null || vals_.Length == 0)
                //    return;

                //var tree_ = getFilter();

                //var node_ = vals_.Length > 0 ? tree_.RootNode.Search(vals_[0].ToString()) : null;
                //if (node_ != null && vals_.Length > 1)
                //    node_ = node_.Search(vals_[1].ToString());

                //if (node_ != null)
                //    tree_.SelectedNode = node_;

                //setFilter(node_);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
        }




        class StateHelper
        {
            string stateFilterCols;
            string stateFilterVal;
            string stateRecordId;
            public StateHelper(string pPrefix)
            {

                stateFilterCols = pPrefix + "/filter/columns";
                stateFilterVal = pPrefix + "/filter/values";
                stateRecordId = pPrefix + "/activerecord/id";
            }

            public void filterSave(string[] cols, object[] vals)
            {
                ToolMobile.getEnvironment().setStateRuntime(stateFilterCols, cols);
                ToolMobile.getEnvironment().setStateRuntime(stateFilterVal, vals);
            }

            public object[] filterLoad(string[] pCols)
            {
                string[] cols_ = ToolMobile.getEnvironment().getStateRuntime(stateFilterCols) as string[];
                object[] vals_ = ToolMobile.getEnvironment().getStateRuntime(stateFilterVal) as object[];


                if (cols_ != null && pCols != null && ToolArray.compare(cols_, pCols) == 0)
                    return vals_;

                return null;
            }


            public void recordIdSave(DataRow pRow)
            {
                object val = null;

                if (pRow != null && pRow.RowState != DataRowState.Deleted && pRow.Table.Columns[TableDUMMY.LOGICALREF] != null)
                    val = pRow[TableDUMMY.LOGICALREF];

                ToolMobile.getEnvironment().setStateRuntime(stateRecordId, val);
            }

            public object recordIdLoad()
            {
                object val_ = ToolMobile.getEnvironment().getStateRuntime(stateRecordId) as object;
                return val_;
            }

        }
        protected void _fillSpeColumns(DataRow pRow)
        {

            try
            {
                this.getGrid().beginDataLoad();

                fillSpeColumns(pRow);

            }
            finally
            {
                this.getGrid().endDataLoad();
            }
        }


        protected virtual void fillSpeColumns(DataRow pRow)
        {

        }


        protected virtual bool canDo(CmdType pCmdType)
        {

            return true;

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (menuItems != null)
            {
                ToolDispose.dispose(menuItems);
            }

            menuItems = null;

            if (filterWrap != null)
                filterWrap.Dispose();

            filterWrap = null;

        }

        protected enum CmdType
        {
            refresh = 1,
            add = 2,
            view = 3,
            edit = 4,
            copy = 5,
            delete = 6,
            info1 = 10,
            info2 = 11,
            info3 = 12,
            info4 = 13,
            info5 = 14,

            doc1 = 15,
            doc2 = 16,
            doc3 = 17,
            doc4 = 18,
            doc5 = 19,

            batch1 = 20,
            batch2 = 21,
            batch3 = 22,
            batch4 = 23,
            batch5 = 24
        }



        protected class CmdMenuItem : IDisposable
        {
            public string Text;
            public CmdType CmdType;
            public IActivity activity;

            public void Dispose()
            {
                if (activity != null)
                    activity.Dispose();

                activity = null;
            }
        }

        protected virtual CmdMenuItem[] createMenuItems()
        {
            List<CmdMenuItem> list = new List<CmdMenuItem>();



            list = new List<CmdMenuItem>();
            //

            if (reference != null && reference.getReferenceMode() != null && reference.getReferenceMode().batchModeIndexes != null)
            {
                string[] indx_ = reference.getReferenceMode().batchModeIndexes;
                //0 bound to Ok button
                //if (indx_.Length > 0)
                //    list.Add(new CmdMenuItem() { CmdType = CmdType.batch1, Text = ToolMobile.getEnvironment().translate(indx_[0]) });
                if (indx_.Length > 1)
                    list.Add(new CmdMenuItem() { CmdType = CmdType.batch2, Text = ToolMobile.getEnvironment().translate(indx_[1]) });
                if (indx_.Length > 2)
                    list.Add(new CmdMenuItem() { CmdType = CmdType.batch3, Text = ToolMobile.getEnvironment().translate(indx_[2]) });
                if (indx_.Length > 3)
                    list.Add(new CmdMenuItem() { CmdType = CmdType.batch4, Text = ToolMobile.getEnvironment().translate(indx_[3]) });
                if (indx_.Length > 4)
                    list.Add(new CmdMenuItem() { CmdType = CmdType.batch5, Text = ToolMobile.getEnvironment().translate(indx_[4]) });

            }
            //
            list.Add(new CmdMenuItem() { CmdType = CmdType.refresh, Text = ToolMobile.getEnvironment().translate(WordCollection.T_REFRESH) });
            list.Add(new CmdMenuItem() { CmdType = CmdType.add, Text = ToolMobile.getEnvironment().translate(WordCollection.T_ADD) });
            list.Add(new CmdMenuItem() { CmdType = CmdType.view, Text = ToolMobile.getEnvironment().translate(WordCollection.T_VIEW) });
            list.Add(new CmdMenuItem() { CmdType = CmdType.edit, Text = ToolMobile.getEnvironment().translate(WordCollection.T_EDIT) });
            list.Add(new CmdMenuItem() { CmdType = CmdType.copy, Text = ToolMobile.getEnvironment().translate(WordCollection.T_COPY) });
            list.Add(new CmdMenuItem() { CmdType = CmdType.delete, Text = ToolMobile.getEnvironment().translate(WordCollection.T_DELETE) });



            string infoList = settings.getString("INFO_SOURCE");
            if (infoList != string.Empty)
            {

                string[] arr = ToolString.explodeList(infoList);
                int indx_ = 0;
                foreach (string srcName in arr)
                {


                    string text = ToolMobile.getEnvironment().translate(settings.getStringAttr(srcName, "name"));
                    string location = settings.getStringAttr(srcName, "location");
                    string[] arrParm = ToolString.explodeList(settings.getStringAttr(srcName, "params"));
                    string[] arrCols = ToolString.explodeList(settings.getStringAttr(srcName, "cols"));

                    IActivity activity_ = new RefMenuItemActivity(ToolMobile.getEnvironment(), (IRowSource)getGrid(), location, arrParm, arrCols);

                    list.Add(new CmdMenuItem() { CmdType = (CmdType)((int)CmdType.info1 + indx_), Text = text, activity = activity_ });
                    ++indx_;
                }
            }



            return list.ToArray();

        }

        CmdMenuItem[] getMenuItems()
        {
            if (menuItems == null)
            {

                menuItems = createMenuItems();

            }

            return menuItems;

        }

        protected virtual void doCmd(CmdType pCmd)
        {
            DataRow row_ = getGrid().ActiveRow;
            //
            CmdMenuItem[] arr_ = getMenuItems(row_);

            

            foreach (CmdMenuItem itm in arr_)
                if (pCmd == itm.CmdType)
                {

                    ToolMobile.logRuntime(string.Format("Try do command [{0}] on form [{1}]", itm.CmdType.ToString(), this.getDsName()));

                    switch (itm.CmdType)
                    {
                        case CmdType.refresh:
                            doRefresh();
                            break;
                        case CmdType.add:
                            doAdd();
                            break;
                        case CmdType.view:
                            doView(row_);
                            break;
                        case CmdType.edit:
                            doEdit(row_);
                            break;
                        case CmdType.copy:
                            doCopy(row_);
                            break;
                        case CmdType.delete:
                            doDelete(row_);
                            break;
                        case CmdType.info1:
                        case CmdType.info2:
                        case CmdType.info3:
                        case CmdType.info4:
                        case CmdType.info5:
                            if (itm.activity != null)
                                itm.activity.done();
                            break;
                        case CmdType.batch1:
                        case CmdType.batch2:
                        case CmdType.batch3:
                        case CmdType.batch4:
                        case CmdType.batch5:
                            doCmdBatch(itm, row_);
                            break;
                        default:
                            if (itm.activity != null)
                                itm.activity.done();
                            else
                                doCmdUser(itm, row_);
                            break;
                    }
                }
        }

        protected virtual void doCmdUser(CmdMenuItem pMenuItem, DataRow pRow)
        {

        }
        protected virtual void doCmdBatch(CmdMenuItem pMenuItem, DataRow pRow)
        {
            reference.getReferenceMode().lastBatchModeIndex = 0;
            //
            switch (pMenuItem.CmdType)
            {
                //case CmdType.batch1:
                //    reference.getReferenceMode().lastBatchModeIndex = 0;
                //    break;
                case CmdType.batch2:
                    reference.getReferenceMode().lastBatchModeIndex = 1;
                    break;
                case CmdType.batch3:
                    reference.getReferenceMode().lastBatchModeIndex = 2;
                    break;
                case CmdType.batch4:
                    reference.getReferenceMode().lastBatchModeIndex = 3;
                    break;
                case CmdType.batch5:
                    reference.getReferenceMode().lastBatchModeIndex = 4;
                    break;
            }
            //
            if (reference.getReferenceMode().lastBatchModeIndex > 0)
                userRequireCommit();
        }
        protected virtual bool useMenuItem(CmdMenuItem pItem, DataRow pDbRow)
        {
            bool use_ = false;
            switch (pItem.CmdType)
            {
                case CmdType.refresh:
                    use_ = true;
                    break;
                case CmdType.add:
                    use_ = canAdd();
                    break;
                case CmdType.view:
                    use_ = (pDbRow != null && canView(pDbRow, true));
                    break;
                case CmdType.edit:
                    use_ = (pDbRow != null && canEdit(pDbRow, true));
                    break;
                case CmdType.copy:
                    use_ = (pDbRow != null && canCopy(pDbRow, true));
                    break;
                case CmdType.delete:
                    use_ = (pDbRow != null && canDelete(pDbRow, true));
                    break;
                case CmdType.info1:
                case CmdType.info2:
                case CmdType.info3:
                case CmdType.info4:
                case CmdType.info5:
                    use_ = (pDbRow != null);
                    break;
                case CmdType.batch1:
                case CmdType.batch2:
                case CmdType.batch3:
                case CmdType.batch4:
                case CmdType.batch5:
                    use_ = (pDbRow != null);
                    break;
            }

            return use_;
        }

        protected CmdMenuItem[] getMenuItems(DataRow pRow)
        {

            DataRow rowInDb = getRecordInDB(pRow);

            List<CmdMenuItem> list = new List<CmdMenuItem>();

            foreach (CmdMenuItem itm in getMenuItems())
            {
                bool use_ = useMenuItem(itm, rowInDb);

                if (use_)
                    list.Add(itm);


            }


            return list.ToArray();
        }


        class RefMenuItemActivity : IActivity
        {
            IEnvironment _environment { get { return ToolMobile.getEnvironment(); } set { } }
            IRowSource _valSource;
            string _location;
            string[] _params;
            string[] _cols;

            public RefMenuItemActivity(IEnvironment pEnv, IRowSource pValSource, string pLocation, string[] pParams, string[] pCols)
            {
                _environment = pEnv;
                _valSource = pValSource;
                _location = pLocation;
                _params = pParams;
                _cols = pCols;
            }




            public object done()
            {

                try
                {
                    List<FilterInfo> list = new List<FilterInfo>();
                    DataRow row_ = _valSource.get();
                    if (row_ != null)
                    {
                        for (int i = 0; i < _params.Length; ++i)
                            if (_params[i] != string.Empty && _cols[i] != string.Empty)
                                list.Add(FilterInfo.getConstFilterInfo(_params[i], row_[_cols[i]]));

                        IActivity a_ = _environment.toActivity(_location, new object[] { list.ToArray() });
                        a_.done();
                    }
                }
                catch (Exception exc)
                {
                    _environment.getExceptionHandler().setException(exc);
                }


                return null;
            }




            public void Dispose()
            {
                _valSource = null;
            }
        }

    }
}

