using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Translating.Tools;
using System.IO;
using AvaExt.Adapter.ForUser.Finance.Operation.Cash;
using AvaExt.Adapter.ForUser.Sale.Operation.Order;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.PagedSource;
using System.Xml;
using AvaExt.Manual.Table;
using AvaExt.Adapter.ForUser.Info.Records;
using AvaExt.Adapter.ForUser.Material.Records;
using AvaExt.Adapter.ForUser.Finance.Records;
using AvaExt.Adapter.ForUser.Admin.Reference;
using AvaExt.Settings;
using AvaExt.SQL;
using AvaExt.MyLog;
using AvaAgent.Services;
using AvaExt.Adapter.ForUser;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForDataSet.Admin.Records;
using AvaExt.Adapter.ForDataSet.Info.Records;
using AvaExt.Adapter.ForDataSet.Material.Records;
using AvaExt.Adapter.ForDataSet.Finance.Records;

using System.Threading;
using Mono.Data.Sqlite;
using AvaGE.MyLog;
using System.Data;
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.MyException;
using AvaExt.Formating;
using AvaExt.AndroidEnv.ApplicationBase;


namespace AvaAgent.FormMain
{
    public class DataReceive : IActivity
    {




        static IUserHandlerLog log { get { return ToolMobile.getEnvironment().getUserHandlerLog(); } }

        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        private DataReceive()
        {


        }

        public static DataReceive createDataReceive()
        {
            return new DataReceive();
        }



        void deleteTables(string[] arrTables, string[] arrDesc)
        {
            for (int i = 0; i < arrTables.Length; ++i)
            {
                ToolMobile.log("Delete Data:" + arrDesc[i]);

                //
                log.set(MessageCollection.T_MSG_DATA_DELETING, new object[] { arrDesc[i] });
                //
                SqlExecute.executeNonQuery(environment, string.Format("DELETE FROM {0}", arrTables[i]));
            }
        }
        void deleteDocTables(string[][] arrTablesGroup, string[] arrDesc, DateTime limDate)
        {
            for (int i = 0; i < arrTablesGroup.Length; ++i)
            {
                //
                log.set(MessageCollection.T_MSG_DOCS_DELETING, new object[] { arrDesc[i] });
                //
                for (int t = 0; t < arrTablesGroup[i].Length; ++t)
                    SqlExecute.executeNonQuery(environment, string.Format("DELETE FROM {0} WHERE {1} < @P1", arrTablesGroup[i][t], TableDUMMY.DATE_), new object[] { limDate });
            }
        }

        void importRecords(XmlDocument doc, AdapterUserRecords[] arrAdapters, string[] arrDesc)
        {

            for (int i = 0; i < arrAdapters.Length; ++i)
            {
                try
                {
                   // ToolMobile.setRuntimeMsg("importRecords : starting: " + arrDesc[i]);

                    log.set(MessageCollection.T_MSG_DATA_IMPORT, new object[] { arrDesc[i] });


                    //


                    //
                    AdapterUserRecords adp = arrAdapters[i];
                    adp.import(doc);

                    
                  //  ToolMobile.setRuntimeMsg("importRecords : finished: " + arrDesc[i]);

                }
                catch (Exception exc)
                {

                    ToolMobile.setRuntimeMsg(exc.ToString());

                    throw new Exception(arrDesc[i], exc);
                }

            }

        }
        ISettings getSysSettings()
        {
            IPagedSource psSysSet = new PagedSourceFirmParams(environment);
            return new SettingsFromTable(psSysSet.getAll(), TableFIRMPARAMS.CODE, TableFIRMPARAMS.VALUE);
        }

        public object done()
        {

            Action startJob_ = () =>
            {

                log.exceuteInContext = _done;
                log.show();


            };

            Action askFirm_ = () =>
            {
                string firms_ = CurrentVersion.ENV.getFirms();

                if (firms_ == string.Empty)
                {
                    startJob_.Invoke();

                }
                else
                {
                    List<string> lNr = new List<string>();
                    List<string> lDesc = new List<string>();
                    string[] arr_ = ToolString.explodeList(firms_);
                    //
                    for (int i = 0; i < arr_.Length; i += 2)
                    {
                        lNr.Add(arr_[i]);
                        lDesc.Add(arr_[i + 1]);
                    }
                    //

                    ToolMsg.askList(null, lDesc.ToArray(), (s, e) =>
                    {
                        int nr_ = XmlFormating.helper.parseInt(lNr[e.Which]);

                        int port_ = CurrentVersion.getPortByFirmNr(nr_);

                        CurrentVersion.ENV.setEnv(CurrentVersion.ENV.PORT, XmlFormating.helper.format(port_));

                        startJob_.Invoke();
                    }

                    );

                }

            };
            ToolMsg.confirm(null, string.Format("{0} - {1}", MessageCollection.T_MSG_COMMIT_BEGIN, MessageCollection.T_MSG_DATA_RECEIVING), () =>
            {
                askFirm_();
                //log.exceuteInContext = _done;
                //log.show();

            }, null);


            return null;
        }

        void _done()
        {
            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(doneAsync);
            t.Start();

            //var t = new DataTaskTask(this);
            //t.Execute();
        }



        void doneAsync()
        {
            lock (environment)
            {



                ISettings oldSysSettings = environment.getSysSettings();

                try
                {
                    //
                    bool localDebugImport = false;

#if DEBUG
                    localDebugImport = false;
#endif


                    log.set(MessageCollection.T_MSG_OPERATION_STARTING);
                    //

                    ToolMobile.setRuntimeMsg("Data import starting");

                    if (!localDebugImport)
                        checkDataSource();

                    //
                    string fileWorkDir = Path.Combine(ToolMobile.curDir(), "data");
                    ToolMobile.createDir(fileWorkDir);
                    string fileInputZip = Path.Combine(fileWorkDir, "fromava.zip");
                    string fileInputXml = Path.Combine(fileWorkDir, "fromava.xml");
                    // bool state = false;
                    //Source
                    if (localDebugImport)//CurrentVersion.ENV.isLocalExim()
                    {

                        //!!//
                    }
                    else
                    {
 
                        {
                            //
                            log.set(MessageCollection.T_MSG_DATA_RECEIVING);
                            //
                            AgentData ad = new AgentData(environment);
                            ServerResult sr;
                            ad.chackOperationResult(sr = ad.resiveData());
                            byte[] data = sr.data;
                            //
                            log.set(MessageCollection.T_MSG_DATA_WRITING);
                            //
                            ToolMobile.writeFileData(fileInputZip, data);
                            ToolZip.decompress(fileInputZip, Path.GetDirectoryName(fileInputZip));

                        }
                    }

                    //Get
                    log.set(MessageCollection.T_MSG_DATA_READING);
                    //
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileInputXml);
                    //
                    if (isSameData(doc, oldSysSettings))
                        throw new Exception(MessageCollection.T_MSG_DATA_OLD);
                    //
                    var commited = false;
                    try
                    {

                        environment.beginBatch();
                        //
                        #region delete firm prm


                        log.set(MessageCollection.T_MSG_DATA_DELETING);
                        //
                        {
                            string[] arrTables = new string[] 
                        {
                            TableFIRMPARAMS.TABLE_FULL_NAME
                        };
                            string[] arrDesc = new string[] 
                        {
                           environment.translate(WordCollection.T_ADDITIONAL)  
                        };
                            deleteTables(arrTables, arrDesc);
                        }
                        //
                        //
                        #endregion
                        //
                        #region import firm prm

                        //
                        log.set(MessageCollection.T_MSG_DATA_IMPORT);
                        //
                        {
                            AdapterUserRecords[] arrAdapters = new AdapterUserRecords[] 
                    {
                        new AdapterUserFirmParams(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetFirmParams(environment)))
                    };
                            string[] arrDesc = new string[] 
                    {
                        environment.translate(WordCollection.T_ADDITIONAL) 
                    };
                            importRecords(doc, arrAdapters, arrDesc);

                        }
                        #endregion
                        //
                        #region sys settings
                        environment.setSysSettings(getSysSettings());
                        //environment.getSysSettings().set(SettingsSysMob.MOB_SYS_LAST_IMPORT, DateTime.Now);
                        #endregion
                        //
                        #region delete rec


                        log.set(MessageCollection.T_MSG_DATA_DELETING);
                        //
                        {
                            string[] arrTables = new string[] 
                        {
                            TableCLCARD.TABLE_FULL_NAME,
                            TableITEMS.TABLE_FULL_NAME,
                            TableWHOUSE.TABLE_FULL_NAME,
                            TableINFOFIRM.TABLE_FULL_NAME,
                            TableINFOPERIOD.TABLE_FULL_NAME,
                            TableINFODOCSAVE.TABLE_FULL_NAME  
                        };
                            string[] arrDesc = new string[] 
                        {
                           environment.translate(WordCollection.T_CLIENT),
                           environment.translate(WordCollection.T_MATERIAL),
                           environment.translate(WordCollection.T_WAREHOUSE),
                           environment.translate(WordCollection.T_INFO) ,
                           environment.translate(WordCollection.T_INFO) ,
                           environment.translate(WordCollection.T_INFO)  
                        };
                            deleteTables(arrTables, arrDesc);
                        }
                        //
                        //
                        #endregion
                        //
                        #region docs
                        log.set(MessageCollection.T_MSG_DOCS_DELETING);
                        //
                        DateTime limDate = environment.getSysSettings().getDateTime(SettingsSysMob.MOB_SYS_CMD_DELETE_DOCS_BEFORE, new DateTime(DateTime.Now.Year, 1, 1));
                        {
                            string[][] arrTablesGroup = new string[][] 
                        {
                          new string[] {TableINVOICE.TABLE_FULL_NAME,TableSTLINE.TABLE_LONG},
                          new string[]{TableORFICHE.TABLE_FULL_NAME,TableORFLINE.TABLE_LONG},
                           new string[] {TableKSLINES.TABLE_FULL_NAME}
                        };
                            string[] arrDesc = new string[] 
                        {
                           environment.translate(WordCollection.T_DOC_STOCK_TRANS),
                           environment.translate(WordCollection.T_DOC_STOCK_ORDERS),
                           environment.translate(WordCollection.T_DOC_FINANCE) 
                        };
                            deleteDocTables(arrTablesGroup, arrDesc, limDate);

                        }
                        #endregion
                        //
                        #region import

                        //
                        log.set(MessageCollection.T_MSG_DATA_IMPORT);
                        //
                        {
                            AdapterUserRecords[] arrAdapters = new AdapterUserRecords[] 
                    {
                        new AdapterUserClient(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetClient(environment))),
                        new AdapterUserMaterial(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetMaterial(environment))),
                        new AdapterUserWarehouse(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetWarehouse(environment))),
                        
                        new AdapterUserInfoFirm(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetInfoFirm(environment))),
                        new AdapterUserInfoPeriod(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetInfoPeriod(environment))),
                        new AdapterUserInfoDocSave(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetInfoDocSave(environment)))
                   
                    
                    };
                            string[] arrDesc = new string[] 
                    {
                        environment.translate(WordCollection.T_CLIENT),
                        environment.translate(WordCollection.T_MATERIAL) ,
                        environment.translate(WordCollection.T_WAREHOUSE) ,
                        environment.translate(WordCollection.T_INFO) ,
                        environment.translate(WordCollection.T_INFO) ,
                        environment.translate(WordCollection.T_INFO) 
                    };
                            importRecords(doc, arrAdapters, arrDesc);

                        }
                        #endregion
                        //clear



                        //
                        environment.clearStateData();
                        //

                        //ToolMobile.setRuntimeMsg("Data importded, commit started");

                         environment.commitBatch();
                        // state = true;
                        commited = true;

                       // ToolMobile.setRuntimeMsg("Data importded, commited");




                        #region files
                        {

                            try
                            {


                                var files = doc.SelectSingleNode("DATA/ITEM[@ei_code='ADP_FILES']");

                                if (files != null)
                                    foreach (var file in files)
                                    {
                                        var el = file as XmlElement;
                                        if (el != null)
                                        {
                                            var dir = ToolXml.getAttribValue(el, "dir", null);
                                            var name = ToolXml.getAttribValue(el, "name", null);
                                            var value_ = ToolXml.getAttribValue(el, "value", null);

                                            if (dir != null && name != null && value_ != null)
                                            {

                                                if (Path.IsPathRooted(dir))
                                                {
                                                    log.error(WordCollection.T_FILE + ": Path rooted: " + dir);
                                                }
                                                else
                                                {

                                                    if (!ToolMobile.existsDir(dir))
                                                        ToolMobile.createDir(dir);

                                                    var path = Path.Combine(dir, name);

                                                    log.set(WordCollection.T_FILE + ": " + path);

                                                    var data = Convert.FromBase64String(value_);

                                                    ToolMobile.writeFileData(path, data);

                                                }
                                            }
                                        }
                                    }
                            }
                            catch (Exception exc)
                            {

                                ToolMobile.setExceptionInner(exc);


                                log.error("Files import error");

                                ToolMobile.setException(exc);
                            }





                        }
                        #endregion

                    }
                    catch (Exception exc)
                    {
                        try
                        {
                            ToolMobile.setExceptionInner(exc);
                        }
                        catch { }


                        try
                        {
                            if (!commited)
                            {

                                ToolMobile.setRuntimeMsg("Data import error, rollback started");

                                  environment.rollbackBatch();

                                ToolMobile.setRuntimeMsg("Data import error, rollback ended");

                            }

                        }
                        catch { }


                        environment.setSysSettings(oldSysSettings);

                        throw new Exception(exc.Message, exc);
                    }

                    //if (commited)
                    //{
                    //    var dir = "cache";
                    //    if (ToolMobile.existsDir(dir))
                    //        ToolMobile.deleteDir(dir);
                    //}

                }
                catch (Exception exc)
                {
                    ToolMobile.setExceptionInner(exc);


                    var err = ToolException.unwrap(exc);


                    ToolMobile.setRuntimeMsg(err);




                    ToolMobile.getContextLast().RunOnUiThread(() =>
                    {
                        try
                        {
                            Android.Widget.Toast.MakeText(Android.App.Application.Context, err, Android.Widget.ToastLength.Long).Show();
                        }
                        catch { }
                    });

                    // ActivityExt.errMessageOnResume = err;


                    log.error(ToolException.unwrap(exc));
                    environment.getExceptionHandler().setException(exc);
                }
                finally
                {
                    log.set(MessageCollection.T_MSG_OPERATION_FINISHING);

                    Thread.Sleep(1000);
                    log.hide();

                }

            }
        }
        void checkDataSource()
        {

            //ToolMobile.setRuntimeMsg("checkDataSource starting");

            var s = SettingsFromXmlDoc.createDummy();
            string old_ = s.format();

            fillDataForRequest(s);


            string new_ = s.format();

            if (old_ != new_)
            {
                AgentData ad = new AgentData(environment);
                string res_ = ad.sendText(new_);
                if (!string.IsNullOrEmpty(res_))
                {
                    var newS_ = new SettingsFromXmlDoc(res_);
                    //
                    checkDataFromRespose(newS_);
                }
            }

            if (getUnSyncedDocs().Rows.Count > 0)
            {
                log.set(MessageCollection.T_MSG_RECORD_NOT_SYNCED, new object[] { getRecDesc(getUnSyncedDocs().Rows[0]) });

                throw new MyExceptionError(MessageCollection.T_MSG_RECORD_NOT_SYNCED);
            }

            ToolMobile.setRuntimeMsg("checkDataSource finished");

        }
        void fillDataForRequest(ISettings pSettings)
        {
            DataTable tab_ = getUnSyncedDocs();

            foreach (DataRow row_ in tab_.Rows)
            {
                pSettings.add();
                pSettings.setEnumer(ConstCmdLine.cmd, "IMPHIS");
                pSettings.setEnumer(ConstCmdLine.type, ToolCell.isNull(row_[TableDUMMY.TYPE], string.Empty));
                pSettings.setEnumer(ConstCmdLine.lref, ToolCell.isNull(row_[TableDUMMY.LOGICALREF], string.Empty));
            }

            {

                pSettings.add();
                pSettings.setEnumer(ConstCmdLine.cmd, "GUID");
                pSettings.setEnumer(ConstCmdLine.value, environment.getSysSettings().getString(SettingsSysMob.MOB_USR_DATA_ID, string.Empty));
            }

        }
        void checkDataFromRespose(ISettings settings)
        {
            bool ok = false;
            try
            {
                 environment.beginBatch();

                settings.enumarate();

                while (settings.moveNext())
                {
                    switch (settings.getStringAttrEnumer(ConstCmdLine.cmd))
                    {
                        case "IMPHIS":
                            {
                                string type_ = settings.getStringAttrEnumer(ConstCmdLine.type);
                                string id_ = settings.getStringAttrEnumer(ConstCmdLine.lref);

                                bool val_ = settings.getBoolEnumer();
                                if (val_)
                                    markImported(id_, type_);
                            }
                            break;
                    }

                }

                ok = true;
            }
            finally
            {
                 if (ok)
                     environment.commitBatch();
                  else
                     environment.rollbackBatch();
            }
        }


        void markImported(string pId, string pTab)
        {
            string tab_ = null;
            switch (pTab)
            {
                case TableINVOICE.TABLE:
                    tab_ = TableINVOICE.TABLE_FULL_NAME;
                    break;
                case TableORFICHE.TABLE:
                    tab_ = TableORFICHE.TABLE_FULL_NAME;
                    break;
                case TableKSLINES.TABLE:
                    tab_ = TableKSLINES.TABLE_FULL_NAME;
                    break;

            }

            if (tab_ == null)
                return;

            string sql_ = "UPDATE " + tab_ + " SET RECVERS = @P1, READONLY = 1 WHERE LOGICALREF = @P2";

            SqlExecute.executeNonQuery(environment, sql_, new object[] { short.MaxValue, pId });
        }
        DataTable getUnSyncedDocs()
        {
            short recVers_ = short.MaxValue;
            string sql_ =
                @"
                SELECT 'INVOICE' TYPE_,LOGICALREF,DATE_,TRCODE FROM LG_$FIRM$_$PERIOD$_INVOICE WHERE RECVERS < @P1 AND CANCELLED = 0
                UNION
                SELECT 'ORFICHE' TYPE_,LOGICALREF,DATE_,TRCODE FROM LG_$FIRM$_$PERIOD$_ORFICHE WHERE RECVERS < @P1 AND CANCELLED = 0
                UNION
                SELECT 'KSLINES' TYPE_,LOGICALREF,DATE_,TRCODE FROM LG_$FIRM$_$PERIOD$_KSLINES WHERE RECVERS < @P1 AND CANCELLED = 0
                ";

            DataTable tab_ = SqlExecute.execute(environment, sql_, new object[] { recVers_ });
            return tab_;

        }

        string getRecDesc(DataRow pRow)
        {
            string tab_ = null;
            string trcode_ = string.Empty;

            switch (pRow[TableDUMMY.TYPE].ToString())
            {
                case TableINVOICE.TABLE:
                    tab_ = WordCollection.T_INVOICE;
                    trcode_ = XmlFormating.helper.format(pRow[TableINVOICE.TRCODE]);
                    break;
                case TableORFICHE.TABLE:
                    tab_ = WordCollection.T_ORDER;
                    trcode_ = XmlFormating.helper.format(pRow[TableORFICHE.TRCODE]);
                    break;
                case TableKSLINES.TABLE:
                    tab_ = WordCollection.T_CASH;
                    trcode_ = XmlFormating.helper.format(pRow[TableKSLINES.TRCODE]);
                    break;
                default:
                    tab_ = "UNDEF";
                    break;
            }
            string date_ = ToolString.left(XmlFormating.helper.format(pRow[TableDUMMY.DATE_]), 10 + 1 + 8);


            return environment.translate(tab_) + "/" + trcode_ + "/" + date_;
        }

        bool isSameData(XmlDocument doc, ISettings oldSysSettings)
        {
            string newId = ToolXml.getAttribValue(doc.DocumentElement, "guid", string.Empty);
            string oldId = oldSysSettings.getString(SettingsSysMob.MOB_USR_DATA_ID, string.Empty);

            if (newId != string.Empty)
                return (newId == oldId);

            return true;

        }

        public void Dispose()
        {

        }
    }
}
