using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using MobExt.Common;


using MobExt.TableOperation;
using MobExt.Adapter.ForDataSet;

using System.IO;
using MobAgent.Services;
using MobAgent.Common;
using MobExt.Adapter.ForUser.Material.Records;
using MobExt.Adapter.ForDataSet.Material.Records;
using MobExt.DataExchange;
using System.Xml;
using MobExt.DataRefernce;
using MobGE.FormDataReference;
using MobGE.MobControl;
using MobExt.ControlOperation;
using MobExt.ObjectSource;
using MobGE.MobControl.ControlsTools;

using MobExt.Common.Const;
using MobExt.SQL;

using System.Data.SqlServerCe;
using MobExt.Manual.Table;
using MobExt.Adapter.ForUser;
using MobExt.Adapter.ForUser.Finance.Records;
using MobExt.Adapter.ForUser.Admin.Reference;
using MobExt.Adapter.ForDataSet.Finance.Records;
using MobExt.Adapter.ForDataSet.Admin.Records;
using MobExt.Settings;
using MobExt.PagedSource;
using MobExt.Adapter.ForUser.Sale.Operation.Slip;
using MobExt.Adapter.ForDataSet.Sale.Operation.Slip;
using MobExt.Translating.Tools;
using MobExt.Adapter.ForUser.Sale.Operation.Order;
using MobExt.Adapter.ForUser.Finance.Operation.Cash;
using MobExt.Adapter.ForDataSet.Finance.Operation.Cash;
using MobExt.Adapter.ForDataSet.Sale.Operation.Order;
using MobExt.MyLog;
using MobExt.MyException;
using System.Threading;
using System.Reflection;
using MobExt.Adapter.ForDataSet.Info.Records;
using MobExt.Adapter.ForUser.Info.Records;

namespace MobAgent.FormMain
{

    public partial class MobFormMain : MobGE.MobControl.MobForm
    {
        protected override string globalStoreName()
        {
            return "form.main";
        }

  
        public MobFormMain()
        {
            InitializeComponent();
        }

        public MobFormMain(IEnvironment pEnv)
            : base(pEnv)
        { 

            InitializeComponent();
            Text = environment.getExtSettings().getString(TableDUMMY.FIRM, Text);
            cTree.DoubleClick += new EventHandler(cTree_DoubleClick);
 
        }
        protected override void initBeforeSettings()
        {
            base.initBeforeSettings();
            string nodesList = environment.getAppSettings().getString(MobExt.Settings.SettingsMobAgent.MOBAGENT_MENU_ITEMS_S);
            string[] nodesArr = ToolString.explodeListLogical(nodesList);
            cTree.addNode(nodesArr);

        }
        protected override void initAfterSettings()
        {

            base.initAfterSettings();



            #region MainMenuNodeCode
            #region MainMenuNodeCode MM
            //Rec
            const string nodeCodeMMMaterials = "001000001";
            //Trans

            const string nodeCodeMMSlipExcess = "001000005";
            const string nodeCodeMMSlipDeficit = "001000006";

            const string nodeCodeMMOrderWarehouseInput = "001000004";
            const string nodeCodeMMOrderWarehouseOutput = "001000008";
            const string nodeCodeMMSlipWarehouseInput = "001000009";
            const string nodeCodeMMSlipWarehouseOutput = "001000010";
            #endregion
            #region MainMenuNodeCode S
            //Rec
            //Trans
            const string nodeCodeSReceiptsSale = "003000011";
            const string nodeCodeSReceiptsReturn = "003000012";
            const string nodeCodeSOrdersSale = "003000013";
            const string nodeCodeSOrdersReturn = "003000014";
            const string nodeCodeSOrdersZero = "003000015";
            #endregion
            #region MainMenuNodeCode F
            //Rec
            const string nodeCodeFARAP = "004000001";
            //Trans
            //const string nodeCodeFARAPSlips = "004000009";
            const string nodeCodeFCashCollection = "004000012";
            const string nodeCodeFCashPayment = "004000013";
            #endregion
            //
            #endregion
            //char nodeSepChar = ',';

            //nodeCodeMMMaterials + nodeSepChar +
            //nodeCodeMMSlips + nodeSepChar +
            //nodeCodeSOrdersSale + nodeSepChar +
            //nodeCodeSOrdersReturn + nodeSepChar +
            //nodeCodeSReceiptsSale + nodeSepChar +
            //nodeCodeSReceiptsReturn + nodeSepChar +
            //nodeCodeFARAP + nodeSepChar +
            //nodeCodeFSafeDepositTrans;






            //InitForGlobal.readSettings(this, environment, settings);



            foreach (string curNodeCode in cTree.getNodes())
            {

                ConstRefCode pReference = ConstRefCode.undef;

                switch (curNodeCode)
                {
                    #region MainMenu

                    #region MainMenuMM
                    case nodeCodeMMMaterials:
                        pReference = ConstRefCode.material;
                        break;
                    case nodeCodeMMSlipExcess:
                        pReference = ConstRefCode.slipsExcess;
                        break;
                    case nodeCodeMMSlipDeficit:
                        pReference = ConstRefCode.slipsDeficit;
                        break;


                    case nodeCodeMMOrderWarehouseInput:
                        pReference = ConstRefCode.orderWarehouseIn;
                        break;
                    case nodeCodeMMOrderWarehouseOutput:
                        pReference = ConstRefCode.orderWarehouseOut;
                        break;
                    case nodeCodeMMSlipWarehouseInput:
                        pReference = ConstRefCode.slipWarehouseIn;
                        break;
                    case nodeCodeMMSlipWarehouseOutput:
                        pReference = ConstRefCode.slipWarehouseOut;
                        break;
                    #endregion
                    #region MainMenuPurchase

                    #endregion
                    #region MainMenuSale
                    case nodeCodeSOrdersSale:
                        pReference = ConstRefCode.docOrderSale;
                        break;
                    case nodeCodeSOrdersReturn:
                        pReference = ConstRefCode.docOrderSaleReturn;
                        break;
                    case nodeCodeSReceiptsSale:
                        pReference = ConstRefCode.docSales;
                        break;
                    case nodeCodeSReceiptsReturn:
                        pReference = ConstRefCode.docSaleReturn;
                        break;
                    case nodeCodeSOrdersZero:
                        pReference = ConstRefCode.orderZero;
                        break;
                    #endregion
                    #region MainMenuFinance
                    case nodeCodeFARAP:
                        pReference = ConstRefCode.client;
                        break;
                    case nodeCodeFCashCollection:
                        pReference = ConstRefCode.docCashCollection;
                        break;
                    case nodeCodeFCashPayment:
                        pReference = ConstRefCode.docCashPayment;
                        break;
                    #endregion
                    #region MainMenuGL

                    #endregion
                    #region MainMenuManagment

                    #endregion

                    #endregion
                    //
                }

                if (pReference != ConstRefCode.undef)
                {
                    // pReference.getFlagStore().flagEnable(ReferenceFlags.showMode);
                    bind(curNodeCode, new TryNodeClik(environment, pReference));
                }


            }


        }







        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (ToolMsg.confirm(environment, MessageCollection.T_MSG_COMMIT_EXIT))
            {
                environment.getTopForm().DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void mobButton1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                e.Cancel = true;
        }






        void grid_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void menuSend_Click(object sender, EventArgs e)
        {
            DataSend s = new DataSend(environment);
            s.done();
        }

        private void menuReceive_Click(object sender, EventArgs e)
        {
            DataReceive r = new DataReceive(environment);
            r.done();
        }
        private void mobButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //IDataReference refer =
                //    new MobDataReferenceClients(environment);
                //refer.begin();



            }
            catch //(Exception exc)
            {
            }

        }





        private void cCMTOpen_Click(object sender, EventArgs e)
        {
            if (cTree.SelectedNode != null)
            {
                if (typeof(MobTreeNodeActive).IsAssignableFrom(cTree.SelectedNode.GetType()))
                    if (((MobTreeNodeActive)cTree.SelectedNode).activity != null)
                        ((MobTreeNodeActive)cTree.SelectedNode).activity.done();
            }
        }

        private void cCMTOpen_Popup(object sender, EventArgs e)
        {

            bool val = false;
            if (cTree.SelectedNode != null)
            {
                if (typeof(MobTreeNodeActive).IsAssignableFrom(cTree.SelectedNode.GetType()))
                    if (((MobTreeNodeActive)cTree.SelectedNode).activity != null)
                        val = true;
            }
            cCMTOpen.Enabled = val;

        }

        private void menuItem6_Click(object sender, EventArgs e)
        {

        }

        private void cTree_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("cTree_KeyDown" + e.KeyCode.ToString());
        }

        private void cTree_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show("cTree_KeyPress" + e.KeyChar.ToString()  );
        }

        private void cTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show("cTree_AfterSelect");
        }

        void cTree_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("cTree_DoubleClick");
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {

        }

        private void cTree_Validating(object sender, CancelEventArgs e)
        {

        }








    }


   
}

