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
using AvaExt.MyLog;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForDataSet.Finance.Operation.Cash;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Order;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Adapter.ForUser;
using AvaAgent.Services;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using System.Data;
using System.Threading;
using AvaGE.MyLog;
using AvaExt.SQL;
using AvaExt.MyException;

namespace AvaAgent.FormMain
{
    public class DataSend : IActivity
    {
        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        static IUserHandlerLog log { get { return ToolMobile.getEnvironment().getUserHandlerLog(); } }

        private DataSend()
        {


        }

        public static DataSend createDataSend()
        {
            return new DataSend();
        }

        public object done()
        {
            log.exceuteInContext = _done;
            log.show();
            return null;
        }

        void _done()
        {
            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(doneAsync);
            t.Start();

        }

        void doneAsync()
        {
            lock (environment)
            {
                bool ok = false;
                try
                {


                    //

                    log.set(MessageCollection.T_MSG_OPERATION_STARTING);
                    //
                    makeDbReadonly();

                    //
                    string fileWorkDir = ToolMobile.getFullPath("data");
                    ToolMobile.createDir(fileWorkDir);
                    string fileOutputZip = Path.Combine(fileWorkDir, "toava.zip");
                    string fileOutputXml = Path.Combine(fileWorkDir, "toava.xml");
                    // bool res = false;

                    //Source
                    //if (environment.getAppSettings().getBool(AvaExt.Settings.SettingsAvaAgent.MOB_USE_LOCAL_EXPIMP_B))
                    //{
                    //    //SaveFileDialog sfd = new SaveFileDialog();
                    //    //if (sfd.ShowDialog() == DialogResult.OK)
                    //    //    fileOutputXml = sfd.FileName;
                    //    //else
                    //    //    throw new Exception(string.Empty);
                    //}

                    //
                    log.set(MessageCollection.T_MSG_DATA_READING);
                    //
                    XmlDocument doc = new XmlDocument();

                    string docID = "LOGICALREF";

                    AdapterUserDocument[] adapters = new AdapterUserDocument[]
                {
                    new AdapterUserWholesaleSlip(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetWholesaleSlip(environment))),
                    new AdapterUserWholesaleOrder(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetWholesaleOrder(environment))),
                    new AdapterUserCashClient(environment, new ImplAdapterDataSetStub(environment, new AdapterDataSetCashClient(environment))) 
                };
                    IPagedSource[] sources = new IPagedSource[] 
                {
                   new PagedSourceSlip(environment),
                    new PagedSourceOrder(environment),
                    new PagedSourceCashTrans(environment) 
                };
                    string[] arrDesc = new string[] 
                {
                   environment.translate(WordCollection.T_DOC_STOCK_TRANS),
                   environment.translate(WordCollection.T_DOC_STOCK_ORDERS),
                   environment.translate(WordCollection.T_DOC_FINANCE) 
                };
                    bool hasData = false;
                    for (int i = 0; i < sources.Length; ++i)
                    {
                        IPagedSource source = sources[i];
                        source.getBuilder().addParameterValue(TableDUMMY.CANCELLED, ConstBool.not);
                        AdapterUserDocument adapter = adapters[i];
                        log.set(MessageCollection.T_MSG_DATA_READING, new object[] { arrDesc[i] });
                        DataTable table = source.getAll();
                        log.set(MessageCollection.T_MSG_DATA_EXPORT, new object[] { arrDesc[i] });
                        foreach (DataRow row in table.Rows)
                        {
                            hasData = true;
                            adapter.edit(row[docID]);
                            adapter.export(doc);
                        }
                    }

                    if (!hasData)
                        throw new Exception(MessageCollection.T_MSG_ERROR_NO_DATA);
                    //
                    string[] expSettings = environment.getSysSettings().getAllSettings();
                    foreach (string settingsName in expSettings)
                        if (settingsName.StartsWith("MOB_SYS_"))
                        {
                            XmlAttribute attr = doc.CreateAttribute(settingsName);
                            attr.Value = environment.getSysSettings().getString(settingsName);
                            doc.DocumentElement.Attributes.Append(attr);
                        }


                    //
                    log.set(MessageCollection.T_MSG_DATA_WRITING);
                    //
                    doc.Save(fileOutputXml);
                    ToolZip.compress(fileOutputZip, fileOutputXml);
                    // if (!environment.getAppSettings().getBool(AvaExt.Settings.SettingsAvaAgent.MOB_USE_LOCAL_EXPIMP_B))
                    //  {
                    if (ToolMobile.existsFile(fileOutputZip))
                    {
                        //
                        log.set(MessageCollection.T_MSG_DATA_SENDING);
                        //
                        AgentData ad = new AgentData(environment);
                        ad.chackOperationResult(ad.sendData(ToolMobile.readFileData(fileOutputZip)));

                    }
                    // }
                    ok = true;
                }
                catch (Exception exc)
                {
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

        void makeDbReadonly()
        {

            string sql_ =
                @"
                UPDATE LG_$FIRM$_$PERIOD$_INVOICE SET READONLY = 1 WHERE READONLY <> 1 ;
                UPDATE LG_$FIRM$_$PERIOD$_ORFICHE SET READONLY = 1 WHERE READONLY <> 1 ;
                UPDATE LG_$FIRM$_$PERIOD$_KSLINES SET READONLY = 1 WHERE READONLY <> 1 ;
                ";

            SqlExecute.executeNonQuery(environment, sql_);

        }

        public void Dispose()
        {

        }
    }
}
