using System;
using System.Collections.Generic;
using System.Text;


using System.Configuration;
using AvaExt.SQL;
using AvaExt.Settings;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.InfoClass;

using AvaExt.MyException;
using System.Globalization;
using System.Data.Common;
using AvaExt.Translating.Tools;
using System.Threading;
using AvaExt.PagedSource;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;

using AvaExt.MyLog;
using AvaExt.SQL.Dynamic.Preparing;
using System.Drawing;
using Mono.Data.Sqlite;
using System.IO;
using AvaExt.SQL.DBSupport;
using AvaExt.DataRefernce;
using AvaExt.Common.Const;
using AvaExt.SQL.Dynamic;
using AvaExt.FileSystem;
using AvaExt.Adapter.ForUser;
using AvaExt.Adapter;
using AvaExt.AndroidEnv.ControlsBase;
using Android.Views;
using AvaExt.Formating;
using System.Text.RegularExpressions;
using AvaExt.Database;
using AvaExt.Reporting;
using AvaExt.Filter;
using Ava_Ext.Common;


namespace AvaExt.Common
{
    public class ImplEnvironment : IEnvironment
    {
        Dictionary<string, object> stateRuntime_ = new Dictionary<string, object>();
        Dictionary<string, object> stateData_ = new Dictionary<string, object>();
        TranslaterGen _translater;
        IFileSystem _fileSystem;
        // DataTable _tabColumns;

        ToolStockRemain _stockRemain;
        DataSet _curDataSet;
        //
        IUserImage imageList = new ImplUserImage();
        Form topForm;
        //
        IDbDscriptor dbDescriptor;
        IHandlerException handlerException;
        IHandlerLog handlerLog;
        protected SqliteConnection connection = null;
        SqliteTransaction transaction = null;
        //
        protected InfoApplication infoApplication = new InfoApplication();
        protected InfoDataSource infoDataSource = new InfoDataSource();
        protected InfoUI infoUI = new InfoUI();
        //
        //

        //
        ISettings settingsExt;
        ISettings settingsSys;
        ISettings settingsLang;
        ISettings settingsApp;
        //ISettings settingsDs;
        //ISettings settingsLogin;
        ISettingsStore settingsStore;

        IReferenceFactory _referenceFactory;
        //
        protected IDictionary<string, string> _listSqlConst = new Dictionary<string, string>();

        //
        public ImplEnvironment()
        {

            handlerLog = new ImplHandlerLog(this);
            handlerException = new ImplHandlerException(this, handlerLog);
            _stockRemain = new ToolStockRemain(this);
        }


        public virtual void init()
        {
            try
            {

                exit();
                //


                //
                initSqlConstExtern();
                //
                infoDataSource.dataSource = CurrentVersion.ENV.nsPerfix + "ava.db";// getDsSettings().getString(SettingsNamesApp.DS_DATA_SOURCE_S);
                infoDataSource.user = "";// getDsSettings().getString(SettingsNamesApp.DS_USER_S);
                infoDataSource.password = "";// getDsSettings().getString(SettingsNamesApp.DS_PASSWORD_S);
                infoDataSource.initialCatalog = "";//getDsSettings().getString(SettingsNamesApp.DS_INITIAL_CATALOG_S);
                //
                connect();
                //
                initSqlConstDb();
                //
                infoApplication.firm = "0";// getLoginSettings().getString(SettingsNamesApp.LOGIN_USER_FIRM_S);
                infoApplication.userName = "ava";//  getLoginSettings().getString(SettingsNamesApp.LOGIN_USER_NAME_S);
                infoApplication.password = "ava";// getLoginSettings().getString(SettingsNamesApp.LOGIN_USER_PASSWORD_S);
                infoApplication.period = "0";// getLoginSettings().getString(SettingsNamesApp.LOGIN_USER_PERIOD_S);
                //

                infoUI.cultureUICode = CurrentVersion.ENV.getEnvString(SettingsNamesApp.USER_LANG_MAIN_S, "az");// //getAppSettings().getString(SettingsNamesApp.USER_LANG_MAIN_S); ;
                infoUI.cultureReportCode = CurrentVersion.ENV.getEnvString(SettingsNamesApp.USER_LANG_ADDITIONAL_S, "az");// getAppSettings().getString(SettingsNamesApp.USER_LANG_ADDITIONAL_S); ;
                //
                login();
                //
                initSqlConst();
                //
                //  fillColumnsInfo();
            }
            catch (Exception exc)
            {
                exit();
                throw exc;
            }
        }

        protected virtual void setSqlConst(string var, string val)
        {
            if (_listSqlConst.ContainsKey(var))
                _listSqlConst[var] = val;
            else
                _listSqlConst.Add(var, val);
        }

        protected virtual void initSqlConstExtern()
        {
            string cnfgConst = settingsApp.getString(SettingsNamesApp.APP_SQL_CONST_S, string.Empty);
            _listSqlConst = ToolString.explodeForParameters(cnfgConst, _listSqlConst);
        }
        protected virtual void initSqlConstDb()
        {
            setSqlConst(SqlTextConstant.USER, infoDataSource.user);
            setSqlConst(SqlTextConstant.CATALOG, infoDataSource.initialCatalog);

        }
        protected virtual void initSqlConst()
        {
            setSqlConst(SqlTextConstant.FIRM_NAME, infoApplication.firmName);
            setSqlConst(SqlTextConstant.FIRM, infoApplication.firmId.ToString().PadLeft(3, '0'));
            setSqlConst(SqlTextConstant.PERIOD, infoApplication.periodId.ToString().PadLeft(2, '0'));
        }
        protected virtual void login()
        {

            //IPagedSource psUser = new PagedSourceUser(this);
            //psUser.getBuilder().addParameterValue(TableUSER.NAME, infoApplication.userName);
            //DataTable tabUser = psUser.getAll();
            //if (!ToolTable.isEmpty(tabUser))
            //{
            //    IPagedSource psFirm = new PagedSourceFirm(this);
            //    psFirm.getBuilder().addParameterValue(TableFIRM.NR, short.Parse(infoApplication.firm));
            //    DataTable tabFirm = psFirm.getAll();
            //    if (!ToolTable.isEmpty(tabFirm))
            //    {
            //        IPagedSource psPeriod = new PagedSourcePeriod(this);
            //        psPeriod.getBuilder().addParameterValue(TablePERIOD.NR, short.Parse(infoApplication.period));
            //        psPeriod.getBuilder().addParameterValue(TablePERIOD.FIRMNR, short.Parse(infoApplication.firm));
            //        DataTable tabPeriod = psPeriod.getAll();
            //        if (!ToolTable.isEmpty(tabPeriod))
            //        {
            //            infoApplication.userId = (short)ToolColumn.getColumnLastValue(tabUser, TableUSER.NR, null);
            //            infoApplication.userName = (string)ToolColumn.getColumnLastValue(tabUser, TableUSER.NAME, null);

            //            infoApplication.firmId = (short)ToolColumn.getColumnLastValue(tabFirm, TableFIRM.NR, null);
            //            infoApplication.firmName = (string)ToolColumn.getColumnLastValue(tabFirm, TableFIRM.NAME, null);

            //            infoApplication.periodId = (short)ToolColumn.getColumnLastValue(tabPeriod, TablePERIOD.NR, null);
            //            infoApplication.periodBeginDate = (DateTime)ToolColumn.getColumnLastValue(tabPeriod, TablePERIOD.BEGDATE, null);
            //            infoApplication.periodEndDate = (DateTime)ToolColumn.getColumnLastValue(tabPeriod, TablePERIOD.ENDDATE, null);
            //            infoApplication.periodCurrencyNativeId = (short)ToolColumn.getColumnLastValue(tabPeriod, TablePERIOD.PERLOCALCTYPE, null);
            //            infoApplication.periodCurrencyReportId = (short)ToolColumn.getColumnLastValue(tabPeriod, TablePERIOD.PERREPCURR, null);

            //            //
            //            IPagedSource ps = new PagedSourceFirmParams(this);
            //            ps.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableFIRMPARAMS.FIRMNR, this.getInfoApplication().firmId));
            //            this.setSysSettings(new SettingsFromTable(ps.getAll(), TableFIRMPARAMS.CODE, TableFIRMPARAMS.VALUE));

            //            //

            //            return;
            //        }
            //    }
            //}
            //throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_LOGINING);






        }


        public virtual IServerAgent getServerAgent()
        {
            throw new NotImplementedException();
        }


        protected virtual void connect()
        {


            //SqlConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
            //builder.DataSource = infoDataSource.dataSource;
            //builder.InitialCatalog = infoDataSource.initialCatalog;
            //builder.UserID = infoDataSource.user;
            //builder.Password = infoDataSource.password;


            // connection.Open();
        }

        public SqliteConnection getConnection()
        {
            return connection;
        }

        public SqliteTransaction getTransaction()
        {
            return transaction;
        }

        public Int32 getSqlTimeOut()
        {
            return Int32.MaxValue;
        }
        public string prepareSqlText(String sqlText)
        {
            StringBuilder sqlbuilder = new StringBuilder(sqlText);
            IEnumerator<string> keys = _listSqlConst.Keys.GetEnumerator();
            keys.Reset();
            while (keys.MoveNext())
                if (keys.Current != string.Empty)
                    sqlbuilder.Replace(keys.Current, _listSqlConst[keys.Current]);
            return sqlbuilder.ToString();
        }

        public InfoApplication getInfoApplication()
        {
            return infoApplication;
        }

        public InfoDataSource getInfoDatabase()
        {
            return infoDataSource;
        }

        public void setTransaction(SqliteTransaction pTran)
        {
            transaction = pTran;
        }

        public void beginBatch()
        {
            SqlExecute.openConn();

            transaction = connection.BeginTransaction();
        }

        public void commitBatch()
        {
            var x = transaction;
            transaction = null;

            if (x != null)
            {
                x.Commit();
                x.Dispose();
            }

            SqlExecute.closeConn();

        }

        public void rollbackBatch()
        {
            if (transaction != null)
            {
                var x = transaction;
                transaction = null;
                x.Rollback();
                x.Dispose();
                //

                SqlExecute.closeConn();
            }
        }


        public virtual bool isDataSourceOk()
        {
            if (
                (getConnection() != null) &&
                (getConnection().State == ConnectionState.Open)
                )
                return true;
            return false;
        }


        public InfoUI getInfoUI()
        {
            return infoUI;
        }



        public SqliteCommand getNewSqlCommand(string sqlText)
        {
            return getNewSqlCommand(sqlText, null);
        }
        public SqliteCommand getNewSqlCommand(string sqlText, SqliteParameter[] par)
        {
            ToolMobile.log(sqlText);

            SqliteCommand cmd = new SqliteCommand();
            cmd.CommandText = this.prepareSqlText(sqlText);
            cmd.Connection = this.getConnection();
            cmd.Transaction = this.getTransaction();
            //cmd.CommandTimeout = this.getSqlTimeOut();
            if (par != null)
                cmd.Parameters.AddRange(par);
            return cmd;
        }

        public virtual void exit()
        {
            reset();
        }
        public string[] translate(string[] pArr)
        {
            string[] res = new string[pArr.Length];
            for (int i = 0; i < pArr.Length; ++i)
                res[i] = this.translate(pArr[i]);
            return res;
        }
        public string translate(string pText)
        {
            return translate(pText, null);
        }
        public void translate(object pObj)
        {

            translate(pObj, null);
        }
        string translate(string pText, ISettings pSettings)
        {

           // return (new TranslaterText(getInfoUI().cultureUICode, pSettings)).get(pText);
            return (new TranslaterText(getInfoUI().cultureUICode)).get(pText);
        }
        void translate(object pObj, ISettings pSettings)
        {
            if (pObj != null)
            {
                //ISettings[] lSet = null;
                //if (pSettings != null)
                //    lSet = new ISettings[] { pSettings, getLangSettings() };
                //else
                //    if (getLangSettings() != null)
                //        lSet = new ISettings[] { getLangSettings() };

                if (TranslaterControl.canTranslate(pObj))
                    TranslaterControl.set(pObj, getInfoUI().cultureUICode, pSettings);
                else
                    if (pObj.GetType() == typeof(DataTable))
                    {
                        DataTable tab = (DataTable)pObj;
                        for (int c = 0; c < tab.Columns.Count; ++c)
                            if (tab.Columns[c].DataType == typeof(string))
                                for (int r = 0; r < tab.Rows.Count; ++r)
                                    // tab.Rows[r][c] = translate((string)tab.Rows[r][c], pSettings);
                                    tab.Rows[r][c] = translate((string)tab.Rows[r][c]);
                    }
            }
        }
        public void setExtSettings(ISettings pExtSttings)
        {
            settingsExt = pExtSttings;
        }

        public ISettings getExtSettings()
        {
            return settingsExt;
        }
        public void setSysSettings(ISettings pSysSttings)
        {
            settingsSys = pSysSttings;
        }

        public ISettings getSysSettings()
        {
            return settingsSys;
        }
        //public void setLangSettings(ISettings pLangSttings)
        //{
        //    settingsLang = pLangSttings;
        //}

        //public ISettings getLangSettings()
        //{
        //    return settingsLang;
        //}
        public void setAppSettings(ISettings pAppSttings)
        {
            settingsApp = pAppSttings;
        }

        public ISettings getAppSettings()
        {
            return settingsApp;
        }


        //public void setLoginSettings(ISettings pLoginSttings)
        //{
        //    settingsLogin = pLoginSttings;
        //}

        //public ISettings getLoginSettings()
        //{
        //    return settingsLogin;
        //}

        //public void setDsSettings(ISettings pDsSttings)
        //{
        //    settingsDs = pDsSttings;
        //}

        //public ISettings getDsSettings()
        //{
        //    return settingsDs;
        //}

        public void saveSettings()
        {
            if (settingsApp != null)
                settingsApp.save();
            //if (settingsDs != null)
            //    settingsDs.save();
            //if (settingsLogin != null)
            //    settingsLogin.save();
            ////
            if (settingsStore != null)
                settingsStore.save();
        }


        public void refreshDbCommand(DbCommand cmd)
        {
            cmd.Connection = getConnection();
            cmd.Transaction = getTransaction();
        }

        public ISettingsStore getSettingsStore()
        {
            return settingsStore;
        }

        public void setSettingsStore(ISettingsStore store)
        {
            settingsStore = store;
        }


        //public Form getTopForm()
        //{
        //    return topForm;
        //}

        //public void setTopForm(Form form)
        //{
        //    topForm = form;
        //}







        public IHandlerException getExceptionHandler()
        {
            return handlerException;
        }
        public void setExceptionHandler(IHandlerException pHandler)
        {
            handlerException = pHandler;
        }

        public IHandlerLog getHandlerTrace()
        {
            return handlerLog;
        }
        public void setHandlerTrace(IHandlerLog pHandler)
        {
            handlerLog = pHandler;
        }
        public CultureInfo getCulture()
        {
            return new CultureInfo(getInfoUI().cultureUICode);
        }
        public CultureInfo getCultureAdditional()
        {
            return new CultureInfo(getInfoUI().cultureReportCode);
        }



        public IUserImage getImages() { return imageList; }
        public void setImages(IUserImage list) { imageList = list; }


        public virtual IDataReference getReference(string refCode)
        {
            return null;
        }

        public virtual EditingTools getAdapter(string pName)
        {
            return null;
        }
        public IReferenceFactory getRefFactory()
        {
            return _referenceFactory;
        }
        public void setRefFactory(IReferenceFactory pFactory)
        {
            _referenceFactory = pFactory;
        }


        public void docBegin(DataSet pDs)
        {
            _curDataSet = pDs;
            if (ToolSlip.isDsSlip(_curDataSet))
                _stockRemain.refreshIOTable();
        }
        public void docEnd()
        {
            if (ToolSlip.isDsSlip(_curDataSet))
                _stockRemain.refreshIOTable();
            _curDataSet = null;
        }
        public DataSet getCurDoc()
        {
            return _curDataSet;
        }
        public IFileSystem getFileSystem()
        {
            return _fileSystem;
        }
        public void setFileSystem(IFileSystem fs)
        {

            _fileSystem = fs;
        }

        public double getMatIOAmount(object lref)
        {
            double db = getMatIOAmountDb(lref);
            double doc = getMatIOAmountDoc(lref);
            return db + doc;
        }

        public double getMatIOAmountDoc(object lref)
        {
            double doc = _stockRemain.getMatDocIO(lref);
            return doc;
        }
        public double getMatIOAmountDb(object lref)
        {
            double db = _stockRemain.getMatDBIO(lref);
            return db;
        }
        public int getColumnLen(string table, string col)
        {
            //DataRow row = _tabColumns.Rows.Find(table + '.' + col);
            //if (row != null)
            //    return (int)row[TableDUMMY.LENGTH];
            //return short.MaxValue;

            return getDbDescriptor().getColumnSize(table, col, short.MaxValue);
        }

        //sql compact
        //void fillColumnsInfo()
        //{
        //    string firmId = prepareSqlText("$FIRM$");
        //    string periodId = prepareSqlText("$PERIOD$");

        //    string query = "select" + "\r\n" +
        //     "TABLE_NAME+'.'+COLUMN_NAME NAME,CHARACTER_MAXIMUM_LENGTH LENGTH" + "\r\n" +
        //     "from INFORMATION_SCHEMA.COLUMNS where" + "\r\n" +
        //     "(TABLE_NAME like 'LG_$FIRM$_$PERIOD$_%' or" + "\r\n" +
        //     "TABLE_NAME like 'LG_$FIRM$_%' or" + "\r\n" +
        //        // "TABLE_NAME like 'LG_$FIRM$_[A-Z]%' or" + "\r\n" +
        //     "TABLE_NAME like 'L_CAPI%'  ) and" + "\r\n" +
        //     "DATA_TYPE in ('nvarchar','varchar')";

        //    ISqlBuilder bu = new ImplSqlBuilder(this, query, string.Empty);
        //    IPagedSource ps = new ImplPagedSource(this, bu);
        //    _tabColsLen = ps.getAll();
        //    foreach (DataRow row in _tabColsLen.Rows)
        //    {
        //        string name = (string)row[TableDUMMY.NAME];

        //        if (name.StartsWith("L_CAPI"))
        //            name = name.Remove(0, 6);
        //        else
        //            if (name.StartsWith("LG_" + firmId + "_" + periodId + "_"))
        //                name = name.Remove(0, 10);
        //            else
        //                if (name.StartsWith("LG_" + firmId + "_"))
        //                    name = name.Remove(0, 7);

        //        row[TableDUMMY.NAME] = name;
        //    }

        //    _tabColsLen.PrimaryKey = new DataColumn[] { _tabColsLen.Columns[TableDUMMY.NAME] };

        //    int v = getColumnLen("CLCARD", "CODE");
        //}

        //sql lite
        //void fillColumnsInfo()
        //{
        //    string firmId = prepareSqlText("$FIRM$");
        //    string periodId = prepareSqlText("$PERIOD$");
        //    //
        //    //string query = "select" + "\r\n" +
        //    // "TABLE_NAME+'.'+COLUMN_NAME NAME,CHARACTER_MAXIMUM_LENGTH LENGTH" + "\r\n" +
        //    // "from INFORMATION_SCHEMA.COLUMNS where" + "\r\n" +
        //    // "(TABLE_NAME like 'LG_$FIRM$_$PERIOD$_%' or" + "\r\n" +
        //    // "TABLE_NAME like 'LG_$FIRM$_%' or" + "\r\n" +
        //    //    // "TABLE_NAME like 'LG_$FIRM$_[A-Z]%' or" + "\r\n" +
        //    // "TABLE_NAME like 'L_CAPI%'  ) and" + "\r\n" +
        //    // "DATA_TYPE in ('nvarchar','varchar')";

        //    //ISqlBuilder bu = new ImplSqlBuilder(this, query, string.Empty);
        //    //IPagedSource ps = new ImplPagedSource(this, bu);
        //    //_tabColsLen = ps.getAll();
        //    //
        //    _tabColumns = new DataTable();

        //    _tabColumns.Columns.Add(TableDUMMY.NAME, typeof(string));
        //    _tabColumns.Columns.Add(TableDUMMY.LENGTH, typeof(int));

        //    foreach (DataRow row1 in new ImplPagedSource(this, new ImplSqlBuilder(this, "pragma table_info", string.Empty)).getAll().Rows)
        //    {
        //        //type[table] name[..]

        //        string type1_ = ToolCell.isNull(row1["type"], "").ToString().ToLowerInvariant();
        //        string name1_ = ToolCell.isNull(row1["name"], "").ToString();
        //        if (type1_ == "table")
        //        {
        //            foreach (DataRow row2 in new ImplPagedSource(this, new ImplSqlBuilder(this, "pragma table_info(" + name1_ + ")", string.Empty)).getAll().Rows)
        //            {
        //                string type2_ = ToolCell.isNull(row2["type"], "").ToString().ToLowerInvariant();
        //                string name2_ = ToolCell.isNull(row2["name"], "").ToString();
        //                //store len as nvarchar(20)
        //                type1_ = type1_.Replace(" ", "").Trim().Trim(')');
        //                string prefix_ = "nvarchar(";
        //                if (type1_.StartsWith(prefix_))
        //                {

        //                    int len_ = XmlFormating.helper.parseInt(type1_.Substring(prefix_.Length));

        //                    if (name2_.StartsWith("L_CAPI"))
        //                        name2_ = name2_.Remove(0, 6);
        //                    else
        //                        if (name2_.StartsWith("LG_" + firmId + "_" + periodId + "_"))
        //                            name2_ = name2_.Remove(0, 10);
        //                        else
        //                            if (name2_.StartsWith("LG_" + firmId + "_"))
        //                                name2_ = name2_.Remove(0, 7);

        //                    _tabColumns.Rows.Add(name2_, len_);
        //                }
        //            }

        //        }
        //    }


        //    _tabColumns.PrimaryKey = new DataColumn[] { _tabColumns.Columns[TableDUMMY.NAME] };


        //}

        void reset()
        {
            if (connection != null)
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            //
            saveSettings();

            if (stateRuntime_ != null)
                stateRuntime_.Clear();
        }

        public virtual IDbDscriptor getDbDescriptor()
        {
            if (dbDescriptor == null)
                dbDescriptor = new ImplDbDscriptor();

            return dbDescriptor;

        }

        public void Dispose()
        {
            reset();
            //
            connection = null;
            topForm = null;
            //
            settingsApp = null;
            //settingsDs = null;
            //settingsLogin = null;
            settingsStore = null;
            //
            _translater = null;



        }


        public virtual IReportRender gerReportRender()
        {
            throw new NotImplementedException();

        }

        public virtual IActivity toActivity(string pCmd, object[] pArgs)
        {
            return new ActivityCmdWrap(pCmd, pArgs);
        }



        public class ActivityCmdWrap : IActivity
        {
            class ToolReportServer : IActivity
            {
                //IEnvironment _environment { get { return ToolMobile.getEnvironment(); } }
                IEnvironment _environment;
                string _cmd;
                ToolObjectName.ArguemntItem[] _args;
                string _name;
                int index = -1;

                bool step1filterInited = false;

                public ToolReportServer(IEnvironment pEnv, string pCmd)
                {
                    _environment = pEnv;
                    _cmd = pCmd;

                    //_cmd = "repserver loc::test filter_mat::ref,LOGICALREF,ref.gen.barcode.mat";
                    // _cmd = "repserver loc::test filter_mat::ref,VALUE,ref.gen.string";

                    _name = ToolObjectName.getName(_cmd);
                    _args = ToolObjectName.getArgs(_cmd);
                }

                public object done()
                {

                    try
                    {

                        if (!step1filterInited)
                        {
                            var hasFilter = false;

                            string filterType = null;
                            string filterCol = null;
                            string filterRef = null;

                            ++index;

                            for (; index < _args.Length; ++index)
                            {
                                var itm = _args[index];

                                if (itm.name.StartsWith("filter_"))
                                {

                                    hasFilter = true;

                                    break;

                                }
                            }

                            if (hasFilter)
                            {


                                var filterParts = ToolString.explodeList(_args[index].value);

                                filterType = filterParts.Length > 0 ? filterParts[0] : null;
                                filterCol = filterParts.Length > 1 ? filterParts[1] : null;
                                filterRef = filterParts.Length > 2 ? filterParts[2] : null;

                                var refObj_ = _environment.getReference(filterRef);
                                if (refObj_ == null)
                                    throw new Exception("Undefined ref: " + filterRef);

                                refObj_.begin(null, null, false, (s, a) =>
                                {
                                    try
                                    {
                                        var row = refObj_.getSelected();

                                        _args[index].value = XmlFormating.helper.format(row != null ? row[filterCol] : "");

                                        this.done();

                                    }
                                    catch (Exception exc)
                                    {
                                        _environment.getExceptionHandler().setException(exc);
                                    }
                                });

                                return null;

                            }

                            if (index >= _args.Length)
                                step1filterInited = true;


                        }



                        doneRender();


                    }
                    catch (Exception exc)
                    {
                        _environment.getExceptionHandler().setException(exc);
                    }

                    return null;

                }


                object doneRender()
                {

                    try
                    {
                        //ToolMsg.progressStart(null, "T_REPORT");
                        //var task = new System.Threading.Tasks.Task(() =>
                        //{

                        var cmdLine = ToolObjectName.create(_name, _args);

                        try
                        {
                            //
                            //var repSource = new ImplReportServerSource(_environment, _cmd);
                            //var dsgns = repSource.getReports();
                            //
                            var dsgn = new ImplReportServer(_environment, cmdLine);
                            var render = _environment.gerReportRender();
                            render.setReport(dsgn);
                            render.done();

                        }
                        catch (Exception exc)
                        {
                            _environment.getExceptionHandler().setException(exc);
                        }
                        finally
                        {
                            ToolMsg.progressStop();
                        }


                        //});

                        //task.Start();
                    }
                    catch (Exception exc)
                    {
                        _environment.getExceptionHandler().setException(exc);
                    }

                    return null;

                }



                public void Dispose()
                {

                }




            }


            Regex exp = new Regex("\\$[A-Z0-9]+\\$", RegexOptions.Compiled);

            //IAdapterObject _adp;
            IDataReference _ref;
            // IUserReport _rep;
            IEnvironment _environment { get { return ToolMobile.getEnvironment(); } }
            object[] _args;

            string _cmd;
            string _type;
            public ActivityCmdWrap(string pCmd, object[] pArgs)
            {
                _cmd = pCmd;
                _type = ToolObjectName.getType(pCmd);
                _args = pArgs;
            }

            public object done()
            {
                object result_ = null;
                try
                {

                    switch (_type)
                    {
                        case ConstObjectNamePrefix.prefAdapter:
                            //IAdapterObject adp_ = _environment.getFactoryAdapter().get(_cmd, _environment);
                            //IAdapterObjectExecuteResult res_ = adp_.execute(_cmd, null, false);
                            //if (res_ != null)
                            //{
                            //    try
                            //    {
                            //        res_.done(false);
                            //        result_ = res_.getExecResult();
                            //    }
                            //    catch (Exception exc)
                            //    {
                            //        res_.failed();
                            //        throw exc;
                            //    }
                            //}

                            var _editor = _environment.getAdapter(_cmd);
                            if (_editor != null)
                            {
                                _editor.adapter.add();
                                _editor.edit();
                            }

                            break;
                        case ConstObjectNamePrefix.prefReference:
                            //if (_ref == null)
                            //{
                            //    _ref = _environment.getFactoryReference().get(_cmd, _environment);
                            //    if (_ref != null)
                            //        _ref.getFlagStore().flagDisable(ReferenceFlags.dialog);
                            //}

                            //if (_ref != null)
                            //    _ref.begin();



                            if (_ref == null)
                                _ref = _environment.getReference(_cmd);

                            if (_ref != null)
                                _ref.begin(null, null, true, (_args != null && _args.Length > 0 ? _args[0] : null) as EventHandler);


                            break;
                        case ConstObjectNamePrefix.prefReport:
                            {
                                try
                                {

                                    FilterInfo[] filters_ = _args != null && _args.Length > 0 ? _args[0] as FilterInfo[] : null;

                                    string _location = ToolObjectName.getArgValue(_cmd, ConstCmdLine.loc);

                                    IReportSource repSource = new ImplReportSource(_environment, _location);

                                    if (filters_ != null)
                                        foreach (FilterInfo f in filters_)
                                        {
                                            IFilter filter = new ImplFilter(_environment, repSource, f);
                                            repSource.addFilter(filter);
                                        }


                                    repSource.getReports()[0].setDataSource(repSource.get());
                                    IReportRender render = _environment.gerReportRender();
                                    render.setReport(repSource.getReports()[0]);
                                    render.done();

                                }
                                catch (Exception exc)
                                {
                                    _environment.getExceptionHandler().setException(exc);
                                }
                            }
                            break;
                        case ConstObjectNamePrefix.prefReportServer:
                            {

                                var act = new ToolReportServer(_environment, _cmd);
                                act.done();






                            }
                            break;
                        case ConstObjectNamePrefix.prefTool:
                            {
                                try
                                {



                                }
                                catch (Exception exc)
                                {
                                    _environment.getExceptionHandler().setException(exc);
                                }
                            }
                            break;
                    }

                }
                catch (Exception exc)
                {
                    _environment.getExceptionHandler().setException(exc);
                }

                return result_;
            }

            //void finished_Disposed(object sender, EventArgs e)
            //{
            //    Form target = sender as Form;
            //    if (target != null)
            //    {
            //        target.FormClosed -= new FormClosedEventHandler(finished);
            //        target.Disposed -= new EventHandler(finished_Disposed);
            //    }
            //}

            //void executeForReport(IUserReport _rep, string _cmd)
            //{
            //    _rep.setLocation(ToolObjectName.getArgValue(_cmd, ConstCmdLine.loc));
            //    string[] filterArr = ToolString.explodeList(ToolObjectName.getArgValue(_cmd, "filter"));
            //    if (filterArr.Length == 2)
            //    {
            //        _rep.getUserFilter().importValues(new FilterValues[] { new FilterValues(filterArr[0], new object[] { replace(filterArr[1]) }) });
            //    }

            //}

            //void notiseUser(object sender, EventArgs e)
            //{
            //    _environment.getUserInteract().showInformation(MessageCollection.T_MSG_OPERATION_OK);
            //}

            //string replace(string pCmd)
            //{
            //    StringBuilder bu = new StringBuilder();
            //    int lastIndx = 0;
            //    foreach (Match m in exp.Matches(pCmd))
            //    {
            //        bu.Append(pCmd.Substring(lastIndx, m.Index - lastIndx));



            //        IDataReference tmpRef = _environment.getFactoryReference().get(ConstRefGEN.ref_gen_string, _environment);
            //        tmpRef.begin();
            //        if (tmpRef.isDataSelected())
            //        {
            //            bu.Append(tmpRef.getSelected()[TableDUMMY.VALUE].ToString());
            //        }
            //        else
            //            throw new Exception(string.Empty);

            //        lastIndx = m.Index;
            //    }
            //    if (bu.Length == 0)
            //        return pCmd;
            //    return bu.ToString();
            //}



            //void finished(object sender, EventArgs e)
            //{
            //    ((IDisposable)sender).Dispose();
            //}


            public virtual void Dispose()
            {


                //one ref for all env
                //  if (_ref != null)
                //     _ref.Dispose();




                _ref = null;

                //  _rep = null;

                _args = null;
            }



        }


        public virtual IUserHandlerLog getUserHandlerLog()
        {
            throw new NotImplementedException();
        }


        public void setStateRuntime(string pName, object pVal)
        {
            if (CurrentVersion.ENV.isDebugMode())
                return;

            stateRuntime_[pName] = pVal;
        }

        public object getStateRuntime(string pName)
        {
            if (stateRuntime_.ContainsKey(pName))
                return stateRuntime_[pName];

            return null;
        }

        public void setStateData(string pName, object pVal)
        {
            if (CurrentVersion.ENV.isDebugMode())
                return;

            stateData_[pName] = pVal;
        }

        public object getStateData(string pName)
        {
            if (stateData_.ContainsKey(pName))
                return stateData_[pName];

            return null;
        }

        public void clearStateData()
        {
            ToolDispose.dispose(stateData_.Values);
            stateData_.Clear();
        }
    }
}
