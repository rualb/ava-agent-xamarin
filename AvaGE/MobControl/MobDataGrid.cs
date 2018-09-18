#define GRIDEXT

using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.ControlOperation;
using AvaExt.Common;
using AvaExt.Settings;
using AvaExt.PagedSource;
using AvaExt.TableOperation;
using System.Data;
using AvaExt.SQL.Dynamic;
using AvaGE.MobControl.ControlsTools;
using AvaExt.TableOperation.CellAutomation;
using AvaExt.Manual.Table;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Graphics;
using AvaExt.Translating.Tools;
using AvaExt.MyException;
using Android.Content.Res;
using AvaExt.ObjectSource;
using AvaExt.Common.Const;
using AvaExt.Formating;
using AvaAgent;




namespace AvaGE.MobControl
{

    public class MobDataGrid : DataGrid, IControlGlobalInit, ISelfDestructable
    {




        public enum ServiceCmd
        {
            invalidate = 0,
            getTopView = 1,
            context = 3,
            environment = 4
        }


        public class EventArgsGrid : EventArgs
        {
            public EventArgsGrid(DataRow pRow, Column pColumn, object pValue)
            {

                Row = pRow;
                Column = pColumn;
                Value = pValue;
            }
            public EventArgsGrid(DataRow pRow, Column pColumn)
            {

                Row = pRow;
                Column = pColumn;
            }
            public EventArgsGrid(DataRow pRow)
            {
                Row = pRow;
            }

            public DataRow Row { get; set; }

            public Column Column { get; set; }

            public object Value { get; set; }
        }

        public event EventHandler<EventArgsGrid> RowClick;
        public event EventHandler<EventArgsGrid> RowLongClick;
        public event EventHandler<EventArgsGrid> RowSelected;
        public event EventHandler<EventArgsGrid> CellClick;
        public event EventHandler<EventArgsGrid> CellLongClick;
        public event EventHandler<EventArgsGrid> CellFormating;

        public delegate object RunService(ServiceCmd pCmd, object[] pArgs);

        public MobDataGrid(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init(context);


        }

        public string userDesc;

        public bool Visible
        {

            get { return this.Visibility == ViewStates.Visible; }
            set { this.Visibility = value ? ViewStates.Visible : ViewStates.Gone; }

        }

        int _DataLoadCount = 0;

        public bool isDataLoad()
        {
            return _DataLoadCount > 0;
        }
        public void beginDataLoad()
        {
            ++_DataLoadCount;
        }
        public void endDataLoad()
        {
            --_DataLoadCount;

            if (_DataLoadCount == 0)
                GetAdapterExt()._DataSetChanged();

            if (_DataLoadCount < 0)
                throw new Exception("DataLoad is out of range");
        }


        IPagedDataAction extender;
        ImplCellReltions dbRealations = null;
        string _lastSortCol = string.Empty;
        bool _sortAsc = false;
        public bool isSortEnabled = false;
        public bool isZeroAsEmpty = true;
        public bool isNewRowActivated = true;
        public bool isColHeaderVisible = true;
        IEnvironment _environment;
        ISettings _settings;

        #region ActiveRow


        #endregion


        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

        }




        void init(Context context)
        {
            this.columns = new ColumnsCollection();
            this.columns.runService = runService;
            //
            this.ItemClick += itemClick;
            this.ItemLongClick += itemLongClick;
            //

            this.StretchMode = Android.Widget.StretchMode.StretchColumnWidth;
            this.SetVerticalSpacing(1);
            this.SetHorizontalSpacing(1);
        }


#if GRIDEXT

        internal void itemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            //TODO Click require column
            if (GetAdapterExt() == null)
                return;

            var cellItem = e.View as MobDataGrid.AdapterExt.MobLabelGrid;

            if (cellItem == null)
                return;


            int r = GetAdapterExt().getRowFromPos(e.Position);
            int c = cellItem.columnIndex;// GetAdapterExt().getColFromPos(e.Position);

            if (r >= 0)
            {
                ActiveRow = GetAdapterExt().getRowAt(r);

                if (RowLongClick != null)
                    RowLongClick.Invoke(this, new EventArgsGrid(ActiveRow));
            }

            if (CellLongClick != null)
                CellLongClick.Invoke(this, new EventArgsGrid(r >= 0 ? ActiveRow : null, this.Columns.getAt(c)));
        }


        internal void itemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //TODO Sort require column
            if (GetAdapterExt() == null)
                return;


            var cellItem = e.View as MobDataGrid.AdapterExt.MobLabelGrid;

            if (cellItem == null)
                return;


            int r = GetAdapterExt().getRowFromPos(e.Position);
            int c = cellItem.columnIndex; // GetAdapterExt().getColFromPos(e.Position);

            if (r >= 0)
            {

               // var x = -1;
               // var y = -1;
               // var firstVisiblePosition=-1;

                try
                {

                 //   firstVisiblePosition = this.FirstVisiblePosition;

                }
                catch (Exception exc)
                {


                }


                ActiveRow = GetAdapterExt().getRowAt(r);




                if (RowClick != null)
                    RowClick.Invoke(this, new EventArgsGrid(ActiveRow));



                try
                {
                    //restore posision by X Y after click

                  //  if (x >= 0 && y >= 0)
                  //      this.ScrollTo(x, y);

                 //   if (firstVisiblePosition >= 0)
                  //      this.SmoothScrollToPosition(firstVisiblePosition);
                }
                catch (Exception exc)
                {


                }
            }
            else
            {
                //sort by col
                // click on header 
                if (isSortEnabled && DataSource != null)
                {
                    Column col_ = this.Columns.getAt(c);
                    if (col_ != null)
                    {
                        object recId = extender.getRecordId();
                        sortClick(col_.Code);
                        extender.searchRecord(recId);
                    }
                }
            }



            if (CellClick != null)
                CellClick.Invoke(this, new EventArgsGrid(r >= 0 ? ActiveRow : null, this.Columns.getAt(c)));

        }

#else 
  void itemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            if (GetAdapterExt() == null)
                return;

            int r = GetAdapterExt().getRowFromPos(e.Position);
            int c = GetAdapterExt().getColFromPos(e.Position);
            if (r >= 0)
            {
                ActiveRow = GetAdapterExt().getRowAt(r);

                if (RowLongClick != null)
                    RowLongClick.Invoke(this, new EventArgsGrid(ActiveRow));
            }

            if (CellLongClick != null)
                CellLongClick.Invoke(this, new EventArgsGrid(r >= 0 ? ActiveRow : null, this.Columns.getAt(c)));
        }
 

        void itemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (GetAdapterExt() == null)
                return;

            int r = GetAdapterExt().getRowFromPos(e.Position);
            int c = GetAdapterExt().getColFromPos(e.Position);

            if (r >= 0)
            {
                ActiveRow = GetAdapterExt().getRowAt(r);

                if (RowClick != null)
                    RowClick.Invoke(this, new EventArgsGrid(ActiveRow));
            }
            else
            {
                //sort by col
                // click on header 
                if (isSortEnabled && DataSource != null)
                {
                    Column col_ = this.Columns.getAt(c);
                    if (col_ != null)
                    {
                        object recId = extender.getRecordId();
                        sortClick(col_.Code);
                        extender.searchRecord(recId);
                    }
                }
            }



            if (CellClick != null)
                CellClick.Invoke(this, new EventArgsGrid(r >= 0 ? ActiveRow : null, this.Columns.getAt(c)));

        }
#endif







#if GRIDEXT

        void Refresh()
        {
            if (Columns == null || GetAdapterExt() == null)
                return;



            this.SetNumColumns(1);

            GetAdapterExt().NotifyDataSetChanged();

            //GetAdapterExt().NotifyDataSetInvalidated();

        }

#else 

 void Refresh()
        {
            if (Columns == null || GetAdapterExt() == null)
                return;

           

            this.SetNumColumns(Columns.getCount());

            // this.InvalidateViews();

            GetAdapterExt().NotifyDataSetInvalidated();


        }
#endif




        internal string FormatValue(DataRow pRow, Column pColumn, object pVal)
        {

            if (ToolCell.isNull(pVal))
                return string.Empty;

            if (CellFormating != null)
            {
                EventArgsGrid a = new EventArgsGrid(pRow, pColumn, pVal);
                CellFormating.Invoke(this, a);
                pVal = a.Value;
            }
            {
                var c = pColumn.FormatSetConverter;
                if (c != null)
                {
                    var v = c.convert(pVal);
                    pVal = v != null ? v : pVal;
                }
            }
            {//if string

                var s = pVal as string;
                if (s != null)
                    return s;
            }
            {//if num
                if (isZeroAsEmpty)
                    if (ToolType.isNumber(pVal) && ToolDouble.isZero(Convert.ToDouble(pVal)))
                        return string.Empty;
            }

            if (!string.IsNullOrEmpty(pColumn.Format))
                return string.Format("{0:" + pColumn.Format + "}", pVal);

            if (ToolCell.isNull(pVal))
                return string.Empty;

            return pVal.ToString();
        }


        object runService(ServiceCmd pCmd, object[] pArgs)
        {

            switch (pCmd)
            {
                case ServiceCmd.getTopView:
                    return this;
                case ServiceCmd.invalidate:
                    Refresh();
                    break;

            }


            return null;
        }



        ColumnsCollection columns;
        public ColumnsCollection Columns { get { return columns; } }

        DataRow activeRow;

        int activeRowPosition = -1;
        void setActivePosition(DataRow pRow)
        {
            if (GetAdapterExt() == null)
                return;
            if (pRow == null)
                return;

            int p = GetAdapterExt().getRowPos(pRow);

            setActivePosition(p);
        }

#if GRIDEXT

        void setActivePosition(int pPos)
        {
            int old = activeRowPosition;

            if (GetAdapterExt() == null)
                return;

            pPos = Math.Max(0, pPos);
            int recCount_ = GetAdapterExt().getRowsCount();
            pPos = Math.Min(pPos, recCount_ - 1); //may be -1

            activeRowPosition = pPos;

            if (old != activeRowPosition)
            {
                Refresh();

                OnRowSelected(ActiveRow);


            }
        }

#else 

  void setActivePosition(int pPos)
        {
            int old = activeRowPosition;

            if (GetAdapterExt() == null)
                return;

            pPos = Math.Max(0, pPos);
            int recCount_ = GetAdapterExt().getRowsCount();
            pPos = Math.Min(pPos, recCount_ - 1); //may be -1

            activeRowPosition = pPos;

            if (old != activeRowPosition)
            {
                Refresh();

                OnRowSelected(ActiveRow);
            }
        }
#endif



        DataRow getActivePosition()
        {
            if (GetAdapterExt() == null)
                return null;

            if (activeRowPosition < 0)
                return null;

            int recCount_ = GetAdapterExt().getRowsCount();
            activeRowPosition = Math.Min(activeRowPosition, recCount_ - 1);

            if (activeRowPosition >= 0)
                return GetAdapterExt().getRowAt(activeRowPosition);

            return null;
        }

        public static bool isInView(DataRow row, DataView view)
        {
            if (row != null && view != null)
                foreach (DataRowView v in view)
                {
                    if (object.ReferenceEquals(row, v.Row))
                        return true;
                }

            return false;

        }

        public int getColumnsCount()
        {
            if (Columns == null)
                return 0;

            return Columns.getCount();
        }
        public int getRowsCount()
        {
            if (GetAdapterExt() == null)
                return 0;

            return GetAdapterExt().getRowsCount();
        }

        public void SetActiveRow(int pRowIndex)
        {
            setActivePosition(Math.Max(pRowIndex, -1));


        }


        public override DataRow ActiveRow
        {
            get
            {


                return getActivePosition();
            }
            set
            {
                setActivePosition(value);



            }


        }

        void OnRowSelected(DataRow pRow)
        {


            if (pRow == null)
                return;
            if (RowSelected != null)
                RowSelected.Invoke(this, new EventArgsGrid(pRow));



        }





        object dataSource;
        protected DataView dataSourceDataView;
        public DataView DataSourceDataView
        {
            get { return dataSourceDataView; }

        }
        public override object DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {

                if (dataSourceDataView == null)
                {
                    DataTable t = value as DataTable;
                    if (t != null)
                        dataSourceDataView = t.DefaultView;
                }
                if (dataSourceDataView == null)
                {
                    DataView v = dataSource as DataView;
                    if (v != null)
                        dataSourceDataView = v;
                }
                if (dataSourceDataView == null)
                {
                    throw new ArgumentException("DataSource is not valid type");
                }

                dataSource = value;

                if (Adapter != null)
                    this.Adapter.Dispose();

                initDataSourceColumns();

                this.Adapter = new AdapterExt(this.Context, this);

                runService(ServiceCmd.invalidate, null);
            }
        }

        protected void DataSetChanged()
        {

        }

        protected void DataSetInvalidated()
        {

        }

        public AdapterExt GetAdapterExt()
        {
            return Adapter as AdapterExt;
        }


        public void initForPaging(IPagedSource pPagedSource, string pGlobalId)
        {

            extender = new ImplPagedGridExtender(this, pPagedSource, pGlobalId);

        }
        public void refreshBind()
        {

            try
            {
                this.beginDataLoad();

                if (dbRealations != null && DataSource != null)
                    dbRealations.reinit();

            }
            finally
            {

                this.endDataLoad();
            }
        }
        public IPagedDataAction getPagingExtender()
        {
            return extender;
        }



        string getColName(string dp)
        {
            return string.Format("{0}_{1}", this.Name, dp);
        }

        void initSettings(IEnvironment pEnv, ISettings pSettings)
        {
            _environment = pEnv;
            _settings = pSettings;
            initVisibleColumns();
            initDataSourceColumns();
        }
        void initDataSourceColumns()
        {

            if (_settings == null || _environment == null || dataSourceDataView == null)
                return;

            if (dataSourceDataView.Table.ExtendedProperties.ContainsKey(getGlobalObjactName()))
                return;
            else
                dataSourceDataView.Table.ExtendedProperties.Add(getGlobalObjactName(), null);


            string attrColumnsExp = "ColumnsExp";
            string attrColumnsSql = "ColumnsSql";


            //string att_list = "List";
            string att_name = "Name";
            string att_bind = "Bind";
            string att_data = "Data";
            string att_sql = "Sql";
            string att_exp = "Exp";

            string[] userColsExp = ToolString.trim(ToolString.explodeList(_settings.getStringAttr(this.Name, attrColumnsExp)));
            foreach (string colNameExp in userColsExp)
                if (colNameExp != string.Empty)
                {
                    string name = _settings.getStringAttr(colNameExp, att_name);
                    string exp = _settings.getStringAttr(colNameExp, att_exp);
                    dataSourceDataView.Table.Columns.Add(name, typeof(object), exp);
                }



            dbRealations = new ImplCellReltions((DataTable)this.DataSource);

            string[] userExt = ToolString.explodeList(_settings.getStringAttr(this.Name, attrColumnsSql));
            foreach (string extName in userExt)
                if (extName != string.Empty)
                {
                    DataTable tab = (DataTable)this.DataSource;
                    //

                    string val_name = _settings.getStringAttr(extName, att_name);
                    string val_bind = _settings.getStringAttr(extName, att_bind);
                    string val_data = _settings.getStringAttr(extName, att_data);
                    string val_sql = _settings.getStringAttr(extName, att_sql);


                    string[] bindCols = ToolString.explodeList(val_bind);
                    string[] dataCols = ToolString.explodeList(val_data);


                    for (int dc = 0; dc < dataCols.Length; ++dc)
                    {

                        string colName = dataCols[dc];

                        string colFullName = ToolColumn.getColumnFullName(val_name, colName);
                        DataColumn colTab = new DataColumn(colFullName);
                        colTab.DataType = typeof(object);
                        tab.Columns.Add(colTab);

                        //MobDataGridTextBoxColumn colGrid = new MobDataGridTextBoxColumn();
                        //colGrid.Name = getColName(ToolColumn.getColumnFullName(val_name, colName));
                        //colGrid.HeaderText = string.Empty;
                        //colGrid.MappingName = colFullName;
                        //// colGrid.ReadOnly = true;
                        //curStyle.GridColumnStyles.Add(colGrid);
                        ////colGrid.globalRead(environment, pSettings);
                    }

                    IPagedSource ps = new ImplPagedSource(_environment, new ImplSqlBuilder(_environment, val_sql, val_name));
                    foreach (string col in dataCols)
                        ps.getBuilder().addColumnToMeta(col, typeof(object));
                    CellAutomationDB dbRel = new CellAutomationDB(tab, ps,
                    bindCols,
                    ToolString.createArray(bindCols.Length, string.Empty),
                    new string[] { },
                    dataCols,
                    UpdateTypeFlags.updateIgnoreRelColumn | UpdateTypeFlags.activeOnRelColumn | UpdateTypeFlags.alwaysIncludeRelColumn | UpdateTypeFlags.setTypeDefaultToDrivedChild | UpdateTypeFlags.disableEditCancel,
                    null);
                    dbRealations.addRelation(bindCols, dbRel, null);
                }

        }

        void initVisibleColumns()
        {
            if (_settings == null || _environment == null)
                return;

            string attrColumnsShow = "ColumnsShow";


            string[] userCols = ToolString.trim(ToolString.explodeList(_settings.getStringAttr(getGlobalObjactName(), attrColumnsShow)));
            foreach (string colName in userCols)
                if (colName != string.Empty && this.Columns.Search(colName) == null)
                {
                    Column col = this.Columns.Add(colName, string.Empty);
                    col.Name = getColName(colName);
                }

            this.Refresh();

        }


        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            //
            initSettings(pEnv, pSettings);
            //
            //
            InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);

        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
        }

        public virtual string getGlobalObjactName()
        {
            return this.Name;
        }

        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }



        string getSortString(string col, bool sortAsc)
        {
            return string.Format(sortAsc ? "{0} ASC" : "{0} DESC", col);
        }
        public void sort(string col, bool sortAsc)
        {
            if (col != string.Empty && dataSourceDataView != null)
            {
                string sort = getSortString(col, sortAsc);
                if (sort != dataSourceDataView.Sort)
                    dataSourceDataView.Sort = sort;
            }
        }
        void sortClick(string col)
        {
            if (col != string.Empty && dataSourceDataView != null)
            {
                if (_lastSortCol == string.Empty)
                    _lastSortCol = col;

                if (_lastSortCol == col)
                    _sortAsc = !_sortAsc;
                else
                    _sortAsc = false;

                _lastSortCol = col;


                dataSourceDataView.Sort = getSortString(_lastSortCol, _sortAsc);
            }
        }




        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);


                if (columns != null)
                    columns.Dispose();

                columns = null;

                if (this.Adapter != null)
                    this.Adapter.Dispose();



                dataSource = null;
                dataSourceDataView = null;

                activeRow = null;

            }
            catch
            {


            }
        }





        public abstract class Element : IDisposable
        {
            public RunService runService;


            protected void cmdInvalidate(Column pSender)
            {
                if (runService != null)
                    runService(ServiceCmd.invalidate, new object[] { pSender });
            }


            public virtual MobDataGrid Grid
            {
                get
                {
                    if (runService != null)
                        return runService(ServiceCmd.getTopView, null) as MobDataGrid;

                    return null;
                }

            }

            public virtual void Dispose()
            {
                runService = null;
            }
        }
        public class Column : Element, IControlGlobalInit, ITranslateable
        {

            public Column(string pCode, string pText)
            {
                Text = pText;
                Code = pCode;
            }


            public double Width
            {
                get
                {

                    return ToolMobile.getDpFromPx(WidthPx) / 2.0;
                }
                set
                {
                    WidthPx = ToolMobile.getPxFromDp(value) * 2;
                }
            }

            int widthPx;
            public int WidthPx
            {
                get
                {

                    return widthPx;
                }
                set
                {
                    widthPx = value;
                }
            }

            string code;
            public string Code
            {
                get
                {
                    return code == null ? string.Empty : code;
                }
                set
                {
                    code = value;
                }
            }


            string text;
            public string Text
            {
                get
                {
                    return text == null ? string.Empty : text;
                }
                set
                {
                    text = value; cmdInvalidate(this);
                }
            }

            string format;
            public string Format
            {
                get
                {
                    return format;
                }
                set
                {
                    format = value;
                }
            }

            public string FormatSet { get; set; }

            IObjectConverter _FormatSetConverter;
            internal IObjectConverter FormatSetConverter
            {
                get
                {
                    try
                    {
                        if (_FormatSetConverter != null)
                            return _FormatSetConverter;

                        if (string.IsNullOrEmpty(FormatSet))
                            return null;

                        if (Grid != null && Grid.dataSourceDataView != null)
                        {
                            DataColumn c = Grid.dataSourceDataView.Table.Columns[this.Code];
                            if (c != null)
                            {
                                Type type_ = c.DataType;

                                string list_ = ToolObjectName.getArgValue(FormatSet, ConstCmdLine.list);
                                string listVal_ = ToolMobile.getEnvironment().getAppSettings().getString(list_, string.Empty);
                                if (!string.IsNullOrEmpty(listVal_))
                                {
                                    string[] arr_ = ToolString.explodeList(listVal_);
                                    Dictionary<object, object> dic_ = new Dictionary<object, object>();
                                    for (int i = 0; i < arr_.Length / 2; ++i)
                                    {
                                        dic_[XmlFormating.helper.parse(arr_[2 * i], type_)] = ToolMobile.getEnvironment().translate(arr_[2 * i + 1]);
                                    }

                                    _FormatSetConverter = new ObjectConverterByDic(dic_);

                                }
                                else
                                {
                                    FormatSet = null;
                                }

                            }

                        }

                    }
                    catch (Exception exc)
                    {
                        ToolMobile.setException(exc);
                    }

                    return _FormatSetConverter;
                }

                set { _FormatSetConverter = value; }
            }

            public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
            {
                _isGlobalInited = true;
                InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);
                //Text = pEnv.translate(Text, pSettings);
            }

            public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
            {
                InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
            }

            public virtual string getGlobalObjactName()
            {
                return this.Name;
            }

            string name;
            public string Name
            {
                get { return name == null ? string.Empty : name; }
                set { name = value; }
            }

            bool _isGlobalInited = false;
            public bool isGlobalInited()
            {
                return _isGlobalInited;
            }


            public string getTranslatingText()
            {
                return Text;
            }

            public void setTranslatingText(string pText)
            {
                Text = pText;
            }


        }

        public class ColumnsCollection : Element
        {

            List<Column> list = new List<Column>();

            public Column Add(string pCode, string pText)
            {
                return Add(new Column(pCode, pText));
            }
            public Column Add(Column pColumn)
            {
                if (pColumn == null)
                    throw new ArgumentNullException();

                list.Add(pColumn); //add to collection
                pColumn.runService = runService; //service

                cmdInvalidate(pColumn);

                return pColumn;
            }




            public void Remove(Column pColumn)
            {
                pColumn.Dispose();

                list.Remove(pColumn);

                cmdInvalidate(pColumn);
            }

            public Column Search(string pCode)
            {
                if (pCode == null)
                    throw new ArgumentNullException();

                foreach (Column n in list)
                    if (n.Code.ToLowerInvariant() == pCode.ToLowerInvariant())
                        return n;

                return null;
            }
            public Column[] getAll()
            {
                return list.ToArray();
            }

            public int getCount()
            {
                return list.Count;
            }

            public Column getAt(int pIndx)
            {
                return pIndx >= 0 && pIndx < list.Count ? list[pIndx] : null;
            }



            public override void Dispose()
            {
                base.Dispose();

                if (list.Count > 0)
                    foreach (Column n in list)
                        n.Dispose();

                list.Clear();
            }



        }

        //public class CellView : TextView
        //{

        //    public CellView(Context context)
        //        : base(context)
        //    {
        //        this.SetWidth(100);
        //        this.SetHeight(25);
        //    }
        //}
        //public class CellViewHeader : CellView
        //{
        //    public CellViewHeader(Context context)
        //        : base(context)
        //    {

        //    }

        //}
        //public class CellViewData : CellView
        //{
        //    public CellViewData(Context context)
        //        : base(context)
        //    {

        //    }

        //}
        public class AdapterExt : BaseAdapter
        {
            MobDataGrid _grid;
            Context _context;


            Color colorGridHeaderBackground;
            Color colorGridSelectedBackground;
            Color colorGridCellBackground;

            Color colorGridHeaderText;
            Color colorGridSelectedText;
            Color colorGridCellText;


            Dictionary<string, Color> _textColors = null;

            Dictionary<string, Color> getTextColors()
            {

                if (_textColors == null)
                {
                    _textColors = new Dictionary<string, Color>();

                    var markerList = ToolMobile.getEnvironment().getSysSettings().getString("MOB_DESC_COLOR_MARKER");

                    if (markerList != null && markerList != "")
                    {
                        var arr = ToolString.explodeList(markerList);

                        if (arr.Length > 0)
                            for (int i = 0; i < arr.Length; i += 2)
                            {
                                var marker = arr[i];
                                var colorStr = i < arr.Length ? arr[i + 1] : null;

                                if (colorStr != null)
                                {
                                    try
                                    {
                                        _textColors[marker] = Color.ParseColor(colorStr);
                                    }
                                    catch (Exception exc)
                                    {
                                        ToolMobile.setExceptionInner(new Exception(marker, exc));
                                    }
                                }

                            }

                    }



                }

                return _textColors;
            }


            public AdapterExt(Context context, MobDataGrid grid)
            {
                _grid = grid;
                _context = context;

                bindDataSource();



                Resources res_ = _grid.Context.Resources;


                colorGridHeaderBackground = res_.GetColor(Resource.Color.colorGridHeaderBackground);
                colorGridSelectedBackground = res_.GetColor(Resource.Color.colorGridSelectedBackground);
                colorGridCellBackground = res_.GetColor(Resource.Color.colorGridCellBackground);

                colorGridHeaderText = res_.GetColor(Resource.Color.colorGridHeaderText);
                colorGridSelectedText = res_.GetColor(Resource.Color.colorGridSelectedText);
                colorGridCellText = res_.GetColor(Resource.Color.colorGridCellText);


            }


            internal void ScrollTo(DataRow pRow)
            {


            }

            public bool HasColHeader()
            {
                return _grid.isColHeaderVisible;
            }


#if GRIDEXT


            public override int Count
            {
                get { return (getRowsCount() + (HasColHeader() ? 1 : 0)) * 1; }
            }
#else 
  public override int Count
            {
                get { return (getRowsCount() + (HasColHeader() ? 1 : 0)) * getColumnsCount(); }
            }
 
#endif




            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }

            public override long GetItemId(int position)
            {
                return position;
            }



            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                try
                {
                    MobPanel p = convertView as MobPanel;
                    if (p == null)
                        p = getView(position, parent);

                    initGridView(p, position, parent);

                    return p;

                }
                catch (Exception exc)
                {
                    ToolMobile.setExceptionInner(exc);
                }

                return null;
            }



#if GRIDEXT



            void initGridView(MobPanel pView, int pPosition, ViewGroup pParent)
            {
                var rowViewBox = pView as MobPanelGrid;

                if (rowViewBox == null)
                    return;

                rowViewBox.position = pPosition;

                int r = getRowFromPos(pPosition);

                bool isHeader = isColumnHeaderPos(pPosition);
                bool isSelected = isPosActive(pPosition);


                if (isHeader)
                {

                    rowViewBox.SetBackgroundColor(colorGridHeaderBackground);
                }
                else
                {
                    rowViewBox.SetBackgroundColor((isSelected ? colorGridSelectedBackground : colorGridCellBackground));
                }



                var childsCount = rowViewBox.ChildCount;


                for (int i = 0; i < childsCount; ++i)
                {
                    var textBox = rowViewBox.GetChildAt(i) as MobLabelGrid;

                    if (textBox == null || textBox.columnIndex < 0)
                        continue;

                    textBox.position = pPosition;

                    var colIndx = (textBox.columnIndex);

                    Column column = getColumnAt(colIndx);
                    //

                    string newText_ = getValueFormatted(r, colIndx);
                    string oldText_ = textBox.Text;

                    if (newText_ != oldText_)
                        textBox.Text = newText_;





                    if (isHeader)
                    {

                        textBox.SetTextColor(colorGridHeaderText);

                        if (textBox.Gravity != GravityFlags.Center)
                            textBox.Gravity = GravityFlags.Center;
                    }
                    else
                    {
                        Color textColor = isSelected ? colorGridSelectedText :
                             colorGridCellText;


                        {


                            //set color by text market

                            var colors = getTextColors();
                            if (colors != null && colors.Count > 0)
                            {
                                if (column.Code == "NAME" || column.Code == "DEFINITION_")
                                {
                                    foreach (var k in colors)
                                    {

                                        if (newText_.Contains(k.Key))
                                        {
                                            textColor = k.Value;

                                            break;

                                        }

                                    }

                                }

                            }



                        }



                        textBox.SetTextColor(textColor);

                        if (textBox.Gravity != GravityFlags.Fill)
                            textBox.Gravity = GravityFlags.Fill;
                    }





                    //textBox.LineCount

                }









                //

            }



#else 
   void initGridView(MobPanel pView, int pPosition,  ViewGroup pParent)
            {
                int r = getRowFromPos(pPosition);
                int c = getColFromPos(pPosition);

                bool isHeader = isColumnHeaderPos(pPosition);
                bool isSelected = isPosActive(pPosition);
                Column column = getColumnAt(c);

                var textBox = pView.FindViewById<TextView>(Resource.Id.cText);

                if (textBox == null)
                    throw new MyExceptionError("Grid layout hasn Text element");

                Resources res_ = _grid.Context.Resources;
                if (isHeader)
                {

                    pView.SetBackgroundColor(res_.GetColor(Resource.Color.colorGridHeaderBackground));
                    textBox.SetTextColor(res_.GetColor(Resource.Color.colorGridHeaderText));
                    textBox.Gravity = GravityFlags.Center;
                }
                else
                {
                    pView.SetBackgroundColor(res_.GetColor(isSelected ? Resource.Color.colorGridSelectedBackground : Resource.Color.colorGridCellBackground));
                    textBox.SetTextColor(res_.GetColor(isSelected ? Resource.Color.colorGridSelectedText : Resource.Color.colorGridCellText));
                    textBox.Gravity = GravityFlags.Center;
                }

                //dotnwork
                //  textBox.SetWidth((int)column.WidthPx);

                string newText_ = getValueFormatted(pPosition);
                string oldText_ = textBox.Text;

                if (newText_ != oldText_)
                    textBox.Text = newText_;
                //

            }
           
            
 
#endif




            //void initGridCell(View pView, int pPosition)
            //{









            //}
            //void initGridHeader(View pView, int pPosition)
            //{
            //    var textBox = pView.FindViewById<TextView>(Resource.Id.cText);

            //    if (textBox == null)
            //        throw new MyExceptionError("Grid layout hasn Text element");

            //    string newText_ = getValueFormatted(pPosition);
            //    string oldText_ = textBox.Text;

            //    if (newText_ != oldText_)
            //        textBox.Text = newText_;

            //}




#if GRIDEXT

            public class MobLabelGrid : MobLabel
            {

                public int columnIndex = -1;
                public int position = -1;

                protected MobLabelGrid(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer)
                    : base(javaReference, transfer)
                {

                }


                public MobLabelGrid(Context context, IAttributeSet attrs, int defStyle)
                    : base(context, attrs, defStyle)
                {

                    Click += (s, a) =>
                    {

                        var grid = searchGrid(this);
                        if (grid != null)
                        {
                            grid.itemClick(grid, new AdapterView.ItemClickEventArgs(
                                grid, this, this.position, this.position)
                                );

                        }

                    };

                    LongClick += (s, a) =>
                    {
                        var grid = searchGrid(this);
                        if (grid != null)
                        {
                            grid.itemLongClick(grid, new AdapterView.ItemLongClickEventArgs(
                                false,
                               grid, this, this.position, this.position)
                               );

                        }
                    };
                }



                static MobDataGrid searchGrid(View pView)
                {
                    var parent = pView.Parent;
                    while (parent != null)
                    {


                        var grid = parent as MobDataGrid;
                        if (grid != null)
                        {

                            return grid;


                        }

                        parent = parent.Parent;

                    }

                    return null;
                }




                //private void MobLabelGrid_ViewAttachedToWindow(object sender, ViewAttachedToWindowEventArgs e)
                //{

                //}

            }

            public class MobPanelGrid : MobPanel
            {
                public int position = -1;


                protected MobPanelGrid(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer)
                    : base(javaReference, transfer)
                {

                }


                public MobPanelGrid(Context context, IAttributeSet attrs, int defStyle)
                    : base(context, attrs, defStyle)
                {

                }




            }

            MobPanel getView(int pPosition, ViewGroup pParent)
            {

                MobPanel row = null;
                var cellBoxStyle = 0;
                var cellItemStyle = 0;
                var isHeader = isColumnHeaderPos(pPosition);

                var minHeight = isHeader ? 110 : 110;


                row = new MobPanelGrid(_context, null, cellBoxStyle);
                row.SetGravity(GravityFlags.Fill);
                row.SetPadding(0, 0, 0, 0);

                row.Orientation = Android.Widget.Orientation.Horizontal;



                if (isHeader)
                {

                    cellBoxStyle = Resource.Style.GridHeader;
                    cellItemStyle = Resource.Style.GridHeaderText;



                }
                else
                {

                    cellBoxStyle = Resource.Style.GridCell;
                    cellItemStyle = Resource.Style.GridCellText;


                }

                {


                    for (int colIndx = 0; colIndx < getColumnsCount(); ++colIndx)
                    {
                        var col = getColumnAt(colIndx);

                        var text = new MobLabelGrid(_context, null, cellItemStyle);
                        //text.SetMaxLines(2);

                        //   text.SetSingleLine();


                        text.columnIndex = colIndx;
                        text.SetPadding(4, 0, 4, 0);



                        //!!!!
                        var weight = col.WidthPx;
                        if (weight <= 0)
                            weight = 1;

                        var layout_item = new LinearLayout.LayoutParams(0, minHeight, weight);


                        text.LayoutParameters = layout_item;

                        row.AddView(text);


                    }

                    row.SetMinimumWidth(minHeight);
                    row.SetMinimumHeight(minHeight);


                }


                return row;



            }

#else 

 

   MobPanel getView(int pPosition, ViewGroup pParent)
            {

               

                int id_ = isColumnHeaderPos(pPosition) ? Resource.Layout.MobDataGridHeader : Resource.Layout.MobDataGridCell;

                LayoutInflater inf = (_context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater);
                if (inf != null)
                    return inf.Inflate(id_, null) as MobPanel;

                return null;

            }
#endif




            public int getColumnsCount()
            {
                if (_grid == null)
                    return 0;

                return _grid.Columns.getCount();
            }

            public int getRowsCount()
            {
                if (_grid == null || _grid.dataSourceDataView == null)
                    return 0;

                return _grid.dataSourceDataView.Count;
            }







#if GRIDEXT

            bool isColumnHeaderPos(int pPos)
            {
                return pPos == 0 && HasColHeader();
            }
            internal int getRowFromPos(int pPos)
            {
                return (pPos / 1) - (HasColHeader() ? 1 : 0);
            }
            internal int getColFromPos(int pPos)
            {
                return (0);
            }






            string getValueFormatted(int pR, int pC)
            {

                int r = pR;
                int c = pC;

                string res_ = string.Empty;
                object val_ = getValue(r, c);
                if (r == -1)
                    res_ = val_.ToString();
                else
                {

                    Column col_ = getColumnAt(c);
                    DataRow row_ = getRowAt(r);
                    res_ = _grid.FormatValue(row_, col_, val_);


                    //

                }

                return res_;
            }










#else 
   bool isColumnHeaderPos(int pPos)
            {
                return getRowFromPos(pPos) < 0;
            }
    public int getRowFromPos(int pPos)
            {
                return (pPos / getColumnsCount()) - (HasColHeader() ? 1 : 0);
            }
            public int getColFromPos(int pPos)
            {
                return (pPos % getColumnsCount());
            }

                public object getValue(int pPos)
            {
                int r = getRowFromPos(pPos);
                int c = getColFromPos(pPos);

                return getValue(r, c);
            }


            
            public string getValueFormatted(int pPos)
            {
                int r = getRowFromPos(pPos);
                int c = getColFromPos(pPos);

                string res_ = string.Empty;
                object val_ = getValue(r, c);
                if (r == -1)
                    res_ = val_.ToString();
                else
                {

                    Column col_ = getColumnAt(c);
                    DataRow row_ = getRowAt(r);
                    res_ = _grid.FormatValue(row_, col_, val_);

                }

                return res_;
            }

#endif




            bool isPosActive(int pPos)
            {
                int r = getRowFromPos(pPos);

                if (r >= 0)
                {
                    DataRow curr_ = _grid.ActiveRow;

                    if (curr_ != null && object.ReferenceEquals(curr_, getRowAt(r)))
                        return true;


                }

                return false;

            }

            public Column[] getColumns()
            {
                return _grid.Columns.getAll();
            }

            public Column getColumnAt(int pIndex)
            {
                return _grid.Columns.getAt(pIndex);
            }
            public DataRow getRowAt(int pRowIndex)
            {
                if (
                    _grid != null &&
                    _grid.dataSourceDataView != null &&

                    pRowIndex >= 0 &&
                    pRowIndex < getRowsCount()
                    )
                {
                    return _grid.dataSourceDataView[pRowIndex].Row;
                }

                return null;


            }
            public int getRowPos(DataRow pRow)
            {

                if (
                    pRow != null &&

                    _grid != null &&
                    _grid.dataSourceDataView != null


                    )
                {
                    int indx = -1;
                    foreach (DataRowView v in _grid.dataSourceDataView)
                    {
                        ++indx;
                        if (object.ReferenceEquals(v.Row, pRow))
                            return indx;
                    }
                }

                return -1;


            }
            public object getValue(int pR, int pC)
            {
                if (
                    _grid != null &&
                    _grid.dataSourceDataView != null &&
                    pC >= 0 &&
                    pC < getColumnsCount() &&
                    pR >= -1 &&
                    pR < getRowsCount()
                    )
                {


                    Column col_ = getColumnAt(pC);
                    if (pR == -1)
                    {
                        return col_.Text;
                    }
                    else
                    {
                        DataRow row_ = getRowAt(pR);
                        return row_[col_.Code];
                    }
                }

                throw new Exception("Index out of range Row=[" + pR + "] Column=[" + pC + "");
            }

            public override bool IsEnabled(int position)
            {
                return true;// !isColumnHeaderPos(position);
            }


            void Source_ColumnChanged(object sender, DataColumnChangeEventArgs e)
            {
                if (_grid.Columns.Search(e.Column.ColumnName) != null)
                    _DataSetChanged();
            }
            private void Source_RowDeleted(object sender, DataRowChangeEventArgs e)
            {
                _DataSetChanged();
            }
            void Source_RowChanged(object sender, DataRowChangeEventArgs e)
            {
                //bug: dont envoked on delete

                if (e.Action == DataRowAction.Add)
                    if (this._grid.isNewRowActivated)
                        _grid.ActiveRow = e.Row;

                _DataSetChanged();
            }

            void Source_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
            {
                //reset = sort
                if (e.ListChangedType == System.ComponentModel.ListChangedType.Reset) //filter changed
                    _DataSetChanged();
            }
            internal void _DataSetChanged()
            {
                if (_grid != null && _grid.isDataLoad())
                    return;

                if (_grid != null) _grid.DataSetChanged();

                this.NotifyDataSetChanged();// Refresh();

            }


            //void _DataSetInvalidated()
            //{
            //    if (_grid != null) _grid.DataSetInvalidated();

            //    this.NotifyDataSetInvalidated();// Refresh();
            //}

            void unbindDataSource()
            {
                if (_grid != null && _grid.dataSourceDataView == null)
                    return;


                _grid.dataSourceDataView.Table.RowChanged -= Source_RowChanged;
                _grid.dataSourceDataView.Table.ColumnChanged -= Source_ColumnChanged;
                _grid.dataSourceDataView.Table.RowDeleted -= Source_RowDeleted;
                _grid.dataSourceDataView.ListChanged -= Source_ListChanged;
            }


            void bindDataSource()
            {
                if (_grid.dataSourceDataView == null)
                    return;

                _grid.dataSourceDataView.Table.RowChanged += Source_RowChanged;
                _grid.dataSourceDataView.Table.ColumnChanged += Source_ColumnChanged;
                _grid.dataSourceDataView.Table.RowDeleted += Source_RowDeleted;
                _grid.dataSourceDataView.ListChanged += Source_ListChanged;

            }



            protected override void Dispose(bool disposing)
            {
                try
                {
                    base.Dispose(disposing);

                    unbindDataSource();

                    _grid = null;
                    _context = null;
                }
                catch
                {

                }
            }

        }


        public object[] selfDestruct()
        {
            return this.Columns.getAll();
        }
    }


}
