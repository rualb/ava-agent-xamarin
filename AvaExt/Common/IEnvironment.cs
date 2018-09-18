using System;
using System.Collections.Generic;
using System.Text;

using AvaExt.InfoClass;

using AvaExt.Settings;
using System.Data.Common;
using System.Globalization;
using AvaExt.MyException;
using AvaExt.MyLog;
using System.Drawing;
using Mono.Data.Sqlite;
using AvaExt.DataRefernce;
using AvaExt.Common.Const;
using System.Data;
using AvaExt.FileSystem;
using AvaExt.Adapter.ForUser;
using AvaExt.Adapter;
using AvaExt.AndroidEnv.ControlsBase;
using Android.Content;
using AvaExt.Database;
using AvaExt.Reporting;


namespace AvaExt.Common
{
    public interface IEnvironment : IDisposable
    {

        SqliteConnection getConnection();
        SqliteTransaction getTransaction();
        void setTransaction(SqliteTransaction pTran);
        void beginBatch();
      
        void commitBatch();
        void rollbackBatch();
        Int32 getSqlTimeOut();
        String prepareSqlText(String sqlText);
        InfoApplication getInfoApplication();
        InfoDataSource getInfoDatabase();
        InfoUI getInfoUI();

        void init();
        bool isDataSourceOk();
        //
        void setSysSettings(ISettings pSysSttings);
        ISettings getSysSettings();
        //void setExtSettings(ISettings pExtSttings);
        //ISettings getExtSettings();
       //void setLangSettings(ISettings pLangSttings);
       // ISettings getLangSettings();
        void setAppSettings(ISettings pAppSttings);
        ISettings getAppSettings();
        //void setLoginSettings(ISettings pLoginSttings);
        //ISettings getLoginSettings();
        //void setDsSettings(ISettings pDsSttings);
        //ISettings getDsSettings();
        void saveSettings();
        ISettingsStore getSettingsStore();
        void setSettingsStore(ISettingsStore store);

        //
        // Form getTopForm();
        // void setTopForm(Form form);

        IReportRender gerReportRender();

        IDbDscriptor getDbDescriptor();

        IUserImage getImages();
        void setImages(IUserImage images);
        //
        SqliteCommand getNewSqlCommand(string sqlText);
        SqliteCommand getNewSqlCommand(string sqlText, SqliteParameter[] par);

        string[] translate(string[] pArr);
        string translate(string pText);
        void translate(object pObj);
      //  string translate(string pText, ISettings pSettings);
       // void translate(object pObj, ISettings pSettings);

        void refreshDbCommand(DbCommand cmd);
        void exit();

        //


        IHandlerException getExceptionHandler();
        void setExceptionHandler(IHandlerException pHandler);

        IHandlerLog getHandlerTrace();
        void setHandlerTrace(IHandlerLog pHandler);

        CultureInfo getCulture();
        CultureInfo getCultureAdditional();

        IDataReference getReference(string refCode);
        EditingTools getAdapter(string pName);

        IReferenceFactory getRefFactory();
        void setRefFactory(IReferenceFactory pFactory);

        IServerAgent getServerAgent();
 
        void docBegin(DataSet pDs);
        void docEnd();
        DataSet getCurDoc();

        double getMatIOAmount(object lref);
        double getMatIOAmountDoc(object lref);
        double getMatIOAmountDb(object lref); 

        int getColumnLen(string table, string col);

        IFileSystem getFileSystem();
        void setFileSystem(IFileSystem fs);


        IActivity toActivity(string pCmd, object[] pArgs);

        IUserHandlerLog getUserHandlerLog();

        void setStateRuntime(string pName, object pVal);
        object getStateRuntime(string pName );

        void setStateData(string pName, object pVal);
        object getStateData(string pName);
        void clearStateData(); 
    }
}
