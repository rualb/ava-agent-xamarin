using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaAgent.SQL.DBSupport;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Settings;
using System.IO;
using AvaExt.SQL;
using AvaExt.DataRefernce;
using AvaExt.Common.Const;

using AvaExt.Adapter.ForUser;
using AvaExt.Adapter;

using AvaExt.Adapter.ForUser.Finance.Operation.Cash;

using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForDataSet.Finance.Operation.Cash;

using AvaExt.Adapter.ForUser.Material.Operation.Slip;
using AvaExt.Adapter.ForDataSet.Material.Operation.Slip;
using AvaExt.Adapter.ForUser.Material.Operation.Order;
using AvaExt.Adapter.ForDataSet.Material.Operation.Order;

using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Order;

using AvaExt.Adapter.ForUser.Sale.Operation.Order;
using Android.Database.Sqlite;
using AvaGE.FormDataReference;
using AvaAgent.FormMain;

using AvaGE.MobControl.Reporting.Renders;
using AvaExt.Reporting;
using AvaGE.FormUserEditor.Const;
using AvaGE.FormUserEditor.Finance.Operations.Cash;
using AvaGE.FormUserEditor.Sale.Operations.Slip;
using AvaGE.FormUserEditor.Sale.Operations.Order;
using AvaGE.FormUserEditor.Material.Operations.Slip;
using AvaGE.FormUserEditor.Material.Operations.Order;
using AvaGE.MyLog;
using AvaExt.MyLog;
using AvaGE.FormUserEditor.Purchase.Operations.Order;
using AvaGE.FormUserEditor.Purchase.Operations.Slip;
using AvaExt.Adapter.ForUser.Purchase.Operation.Slip;
using AvaExt.Adapter.ForDataSet.Purchase.Operation.Slip;
using AvaExt.Adapter.ForDataSet.Purchase.Operation.Order;
using AvaExt.Adapter.ForUser.Purchase.Operation.Order;
using AvaExt.FileSystem;
using AvaExt.PagedSource;
using AvaExt.Manual.Table;

namespace AvaAgent.Common
{
    public class MobEnvironment : ImplEnvironment
    {

        public MobEnvironment()
            : base()
        {


        }



        public static void startEnv()
        {

            IEnvironment environment = null;

            try
            {
                environment = new MobEnvironment();
                ToolMobile.setEnvironment(environment);

                environment.setFileSystem(new ImplFileSystem());

                // environment.setLangSettings(new SettingsFromFileExt(Path.Combine(SettingsFileName.DIRECTORY_CONFIG, SettingsFileName.DIRECTORY_SYS), SettingsFileName.NAME_LANG, environment));
                environment.setAppSettings(new SettingsFromFileExt(Path.Combine(SettingsFileName.DIRECTORY_CONFIG, SettingsFileName.DIRECTORY_SYS), SettingsFileName.NAME_APP, environment));
                //  environment.setLoginSettings(new SettingsFromFileExt(Path.Combine(SettingsFileName.DIRECTORY_CONFIG, SettingsFileName.DIRECTORY_SYS), SettingsFileName.NAME_LOGIN, environment));
                // environment.setDsSettings(new SettingsFromFileExt(Path.Combine(SettingsFileName.DIRECTORY_CONFIG, SettingsFileName.DIRECTORY_SYS), SettingsFileName.NAME_DS, environment));

                environment.setSettingsStore(new SettingsStoreFromDirectory(Path.Combine(SettingsFileName.DIRECTORY_CONFIG, SettingsFileName.DIRECTORY_UI), environment));

                IUserImage list = new ImplUserImage();

                environment.setImages(list);






                ToolMobile.log("environment init starting");
                environment.init();
                //
                IPagedSource psSysSet = new PagedSourceFirmParams(environment);
                ToolMobile.log("set firm parameters");
                environment.setSysSettings(new SettingsFromTable(psSysSet.getAll(), TableFIRMPARAMS.CODE, TableFIRMPARAMS.VALUE));

                ToolMobile.log("environment started");



            }
            catch (Exception exc)
            {
                ToolMobile.log("environment start error: " + exc.Message);
                ToolMobile.setExceptionInner(exc);
                // environment.getExceptionHandler().setException(exc, delegate() { Close(); });
                ToolMobile.setEnvironment(null);
            }



        }





        bool isDbNew = false;
        public override void init()
        {
            try
            {
                base.init();
                try
                {
                    if (isDbNew)
                        fillDb();
                }
                catch (Exception exc)
                {
                    try
                    {
                        exit();
                        if (ToolMobile.existsFile(infoDataSource.dataSource))
                            ToolMobile.deleteFile(infoDataSource.dataSource);
                    }
                    catch { }
                    throw exc;
                }
                //
            }
            catch (Exception exc)
            {
                exit();
                throw exc;
            }
        }

        protected override void initSqlConstDb()
        {
            setSqlConst(SqlTextConstant.USER, infoDataSource.user);
            setSqlConst(SqlTextConstant.CATALOG + "..", string.Empty);

        }
        protected virtual void fillDb()
        {

            new AvaAgentDBApiSupport(this);
            //new AvaAgentDBApiSupportCommon(this);
            //new AvaAgentDBApiSupportFirm(this);
            //new AvaAgentDBApiSupportPeriod(this);

        }
        IDictionary<string, IDataReference> _refCollection = new Dictionary<string, IDataReference>();
        public override IDataReference getReference(string pCmd)
        {
            string refCode = ToolObjectName.getName(pCmd);


            IDataReference refer = null;
            if (_refCollection.ContainsKey(refCode))
                return _refCollection[refCode];
            else
            {
                switch (refCode)
                {
                    case ConstRefCode.material:
                        refer = new MobDataReferenceMaterial(pCmd);
                        break;
                    case ConstRefCode.promoMaterial:
                        refer = new MobDataReferencePromoMaterial(pCmd);
                        break;
                    case ConstRefCode.client:
                        refer = new MobDataReferenceClients(pCmd);
                        break;
                    case ConstRefCode.string_:
                        refer = new MobImplDataReferenceForStringSelect(pCmd);
                        break;

                    case ConstRefCode.date:
                        refer = new MobImplDataReferenceForDateSelect(pCmd);
                        break;
                    case ConstRefCode.number:
                        refer = new MobImplDataReferenceForValueSelectNum(pCmd);
                        break;
                    case ConstRefCode.numberPercent:
                        refer = new MobImplDataReferenceForValueSelectNumPercent(pCmd);
                        break;



                    case ConstRefCode.docCashCollection:
                        if (ToolMobile.canPayment())
                        {
                            refer = new MobDataReferenceCashCollections(pCmd);
                        }
                        break;

                    case ConstRefCode.docCashPayment:
                        if (ToolMobile.canPayment())
                        {
                            refer = new MobDataReferenceCashPayment(pCmd);
                        }
                        break;

                    case ConstRefCode.docSales:
                        refer = new MobDataReferenceSlipsSales(pCmd);
                        break;
                    case ConstRefCode.docSaleReturn:
                        refer = new MobDataReferenceSlipsReturns(pCmd);
                        break;
                    case ConstRefCode.docOrderSale:
                        refer = new MobDataReferenceOrdersSale(pCmd);
                        break;
                    case ConstRefCode.docOrderSaleReturn:
                        refer = new MobDataReferenceOrdersReturn(pCmd);
                        break;



                    case ConstRefCode.docPurchase:
                        refer = new MobDataReferenceSlipsPurchase(pCmd);
                        break;
                    case ConstRefCode.docPurchaseReturn:
                        refer = new MobDataReferenceSlipsPurchaseReturns(pCmd);
                        break;
                    case ConstRefCode.docOrderPurchase:
                        refer = new MobDataReferenceOrdersPurchase(pCmd);
                        break;
                    case ConstRefCode.docOrderPurchaseReturn:
                        refer = new MobDataReferenceOrdersPurchaseReturn(pCmd);
                        break;


                    case ConstRefCode.materialBarcode:
                        refer = new MobDataReferenceMaterialBarcode(pCmd);
                        break;
                    case ConstRefCode.promoMaterialBarcode:
                        refer = new MobDataReferenceMaterialPromoBarcode(pCmd);
                        break;

                    case ConstRefCode.orderZero:
                        refer = new MobDataReferenceOrdersZero(pCmd);
                        break;
                    case ConstRefCode.slipsExcess:
                        refer = new MobDataReferenceSlipsExcess(pCmd);
                        break;
                    case ConstRefCode.slipsDeficit:
                        refer = new MobDataReferenceSlipsDeficit(pCmd);
                        break;



                    case ConstRefCode.orderWarehouseIn:
                        refer = new MobDataReferenceOrdersWarehouseInput(pCmd);
                        break;
                    case ConstRefCode.orderWarehouseOut:
                        refer = new MobDataReferenceOrdersWarehouseOutput(pCmd);
                        break;

                    case ConstRefCode.orderWarehouseList:
                        refer = new MobDataReferenceOrdersWarehouseList(pCmd);
                        break;
                    case ConstRefCode.orderWarehouseCounting:
                        refer = new MobDataReferenceOrdersWarehouseCounting(pCmd);
                        break;


                    case ConstRefCode.slipWarehouseIn:
                        refer = new MobDataReferenceSlipsWarehouseInput(pCmd);
                        break;
                    case ConstRefCode.slipWarehouseOut:
                        refer = new MobDataReferenceSlipsWarehouseOutput(pCmd);
                        break;
                }

                _refCollection.Add(refCode, refer);
            }
            return refer;
        }
        IDictionary<string, EditingTools> _adpCollection = new Dictionary<string, EditingTools>();
        public override EditingTools getAdapter(string pCmd)
        {



            string adpCode = ToolObjectName.getName(pCmd);

            EditingTools tools = new EditingTools(this);
            if (_adpCollection.ContainsKey(adpCode))
                return _adpCollection[adpCode];
            else
            {
                switch (adpCode)
                {


                    case ConstAdapterNames.adp_fin_cash_client_input:
                        if (ToolMobile.canPayment())
                        {
                            tools.form = typeof(MobUserEditorFormCashCollection);
                            tools.adapter = new AdapterUserCashInputClient(this, new ImplAdapterDataSetStub(this, new AdapterDataSetCashInputClient((this))));
                        }
                        break;
                    case ConstAdapterNames.adp_fin_cash_client_output:
                        if (ToolMobile.canPayment())
                        {
                            tools.form = typeof(MobUserEditorFormCashPayment);
                            tools.adapter = new AdapterUserCashOutputClient(this, new ImplAdapterDataSetStub(this, new AdapterDataSetCashOutputClient((this))));
                        }
                        break;




                    case ConstAdapterNames.adp_sls_doc_wholesaleret:
                        tools.form = typeof(MobUserEditorFormWholesaleReturnSlip);
                        tools.adapter = new AdapterUserWholesaleReturnSlip(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWholesaleReturnSlip((this))));
                        break;
                    case ConstAdapterNames.adp_sls_doc_wholesale:
                        tools.form = typeof(MobUserEditorFormWholesaleSlip);
                        tools.adapter = new AdapterUserWholesaleSlip(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWholesaleSlip((this))));
                        break;
                    case ConstAdapterNames.adp_sls_doc_order_wholesale:
                        tools.form = typeof(MobUserEditorFormWholesaleOrder);
                        tools.adapter = new AdapterUserWholesaleOrder(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWholesaleOrder((this))));
                        break;
                    case ConstAdapterNames.adp_sls_doc_order_wholesaleret:
                        tools.form = typeof(MobUserEditorFormWholesaleReturnOrder);
                        tools.adapter = new AdapterUserWholesaleReturnOrder(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWholesaleReturnOrder((this))));
                        break;
                    case ConstAdapterNames.adp_sls_doc_order_zero:
                        tools.form = typeof(MobUserEditorFormZeroOrder);
                        tools.adapter = new AdapterUserZeroOrder(this, new ImplAdapterDataSetStub(this, new AdapterDataSetZeroOrder((this))));
                        break;


                    case ConstAdapterNames.adp_prch_doc_purchaseret:
                        tools.form = typeof(MobUserEditorFormPurchaseReturnSlip);
                        tools.adapter = new AdapterUserPurchaseReturnSlip(this, new ImplAdapterDataSetStub(this, new AdapterDataSetPurchaseReturnSlip((this))));
                        break;
                    case ConstAdapterNames.adp_prch_doc_purchase:
                        tools.form = typeof(MobUserEditorFormPurchaseSlip);
                        tools.adapter = new AdapterUserPurchaseSlip(this, new ImplAdapterDataSetStub(this, new AdapterDataSetPurchaseSlip((this))));
                        break;
                    case ConstAdapterNames.adp_prch_doc_order_purchase:
                        tools.form = typeof(MobUserEditorFormPurchaseOrder);
                        tools.adapter = new AdapterUserPurchaseOrder(this, new ImplAdapterDataSetStub(this, new AdapterDataSetPurchaseOrder((this))));
                        break;
                    case ConstAdapterNames.adp_prch_doc_order_purchaseret:
                        tools.form = typeof(MobUserEditorFormPurchaseReturnOrder);
                        tools.adapter = new AdapterUserPurchaseReturnOrder(this, new ImplAdapterDataSetStub(this, new AdapterDataSetPurchaseReturnOrder((this))));
                        break;



                    case ConstAdapterNames.adp_mm_doc_deficit:
                        tools.form = typeof(MobUserEditorFormDeficitSlip);
                        tools.adapter = new AdapterUserDeficitSlip(this, new ImplAdapterDataSetStub(this, new AdapterDataSetDeficitSlip((this))));
                        break;
                    case ConstAdapterNames.adp_mm_doc_excess:
                        tools.form = typeof(MobUserEditorFormExcessSlip);
                        tools.adapter = new AdapterUserExcessSlip(this, new ImplAdapterDataSetStub(this, new AdapterDataSetExcessSlip((this))));
                        break;


                    case ConstAdapterNames.adp_mm_doc_order_warehouse_list:
                        tools.form = typeof(MobUserEditorFormWarehouseOrderList);
                        tools.adapter = new AdapterUserWarehouseOrderList(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWarehouseOrderList((this))));
                        break;
                    case ConstAdapterNames.adp_mm_doc_order_warehouse_counting:
                        tools.form = typeof(MobUserEditorFormWarehouseOrderCounting);
                        tools.adapter = new AdapterUserWarehouseOrderCounting(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWarehouseOrderCounting((this))));
                        break;
                    case ConstAdapterNames.adp_mm_doc_order_warehouse_input:
                        tools.form = typeof(MobUserEditorFormWarehouseOrderInput);
                        tools.adapter = new AdapterUserWarehouseOrderInput(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWarehouseOrderInput((this))));
                        break;
                    case ConstAdapterNames.adp_mm_doc_order_warehouse_output:
                        tools.form = typeof(MobUserEditorFormWarehouseOrderOutput);
                        tools.adapter = new AdapterUserWarehouseOrderOutput(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWarehouseOrderOutput((this))));
                        break;
                    case ConstAdapterNames.adp_mm_doc_slip_warehouse_input:
                        tools.form = typeof(MobUserEditorFormWarehouseSlipInput);
                        tools.adapter = new AdapterUserWarehouseSlipInput(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWarehouseSlipInput((this))));
                        break;
                    case ConstAdapterNames.adp_mm_doc_slip_warehouse_output:
                        tools.form = typeof(MobUserEditorFormWarehouseSlipOutput);
                        tools.adapter = new AdapterUserWarehouseSlipOutput(this, new ImplAdapterDataSetStub(this, new AdapterDataSetWarehouseSlipOutput((this))));
                        break;
                }
                if (tools.adapter != null && tools.form != null)
                {
                    _adpCollection.Add(pCmd, tools);
                    return tools;
                }
            }
            return null;
        }
        protected override void connect()
        {



            if (!ToolMobile.existsFile(infoDataSource.dataSource))
                isDbNew = true;


            try
            {
               


                connection = SqlExecute.getConnection(infoDataSource.dataSource);

            }
            catch (Exception exc)
            {

                ToolMobile.setExceptionInner(exc);

                if (isDbNew && ToolMobile.existsFile(infoDataSource.dataSource))
                    ToolMobile.deleteFile(infoDataSource.dataSource);

                throw exc;
            }






        }



        public override IActivity toActivity(string pCmd, object[] pArgs)
        {


            string _name = ToolObjectName.getName(pCmd);
            string _type = ToolObjectName.getType(pCmd);
            switch (_name)
            {
                case "tool.data.import":
                    return DataReceive.createDataReceive();
                case "tool.data.export":
                    return DataSend.createDataSend();
            }



            return base.toActivity(pCmd, pArgs);
        }

        public override IReportRender gerReportRender()
        {
            return new MobFormShowDataRender(this);
        }

        public override IUserHandlerLog getUserHandlerLog()
        {
            return ImplUserHandlerLog.instance;
        }


        public override IServerAgent getServerAgent()
        {
            return new AvaAgent.Services.AgentData(this);
        }
    }
}
