using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.ControlOperation;
using AvaGE.MobControl.ControlsTools;
using AvaExt.Common.Const;
using AvaExt.Manual.Table;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Adapter.ForUser;
using AvaGE.FormUserEditor;

using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.TableOperation;
using AvaExt.Translating.Tools;
using AvaExt.ObjectSource;
using AvaExt.Adapter.ForUser.Finance.Operation.Cash;
using AvaExt.Adapter.ForDataSet.Finance.Operation.Cash;

using AvaExt.DataRefernce;
using AvaGE.FormUserEditor.Const;
using AvaExt.Adapter;
using Android.App;


namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferencePurchaseForm : MobDataReferenceForMoveForm
    {

        protected override string globalStoreName()
        {
            return ConstRefCode.docPurchase;// "ref.sls.wholesale";
        }
        public MobDataReferencePurchaseForm()
            : base(0)
        {


        }
        protected override string getAdapterCode()
        {
            return ConstAdapterNames.adp_prch_doc_purchase;
        }


        protected override void setSource(IPagedSource pSource)
        {
            base.setSource(pSource);
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.GRPCODE, ConstDocGroupCode.purchasing));
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.TRCODE, ConstDocTypeMaterial.materialPurchase));

        }



 
        protected override bool useMenuItem(CmdMenuItem pItem, DataRow pDbRow)
        {
            switch (pItem.CmdType)
            {
                case CmdType.doc1:
                    return canCash(pDbRow, true);

            }

            return base.useMenuItem(pItem, pDbRow);


        }

        protected override void doCmdUser(CmdMenuItem pMenuItem, DataRow pRow)
        {
            if (pMenuItem.CmdType == CmdType.doc1)
                doCash(pRow);
            else
                base.doCmdUser(pMenuItem, pRow);
        }

        object doCash(DataRow pRow)
        {
            object res = null;
            try
            {
                if (canCash(pRow, false))
                {
                    res = cash(pRow);
                }
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
            return res;
        }

        private object cash(DataRow pRow)
        {
            if (pRow == null)
                return null;

            EditingTools _editor = environment.getAdapter(ConstAdapterNames.adp_fin_cash_client_input);

            if (_editor == null)
                return null;

            _editor.adapter.add();
            DataSet ds = _editor.adapter.getDataSet();
            DataTable tab = ds.Tables[TableKSLINES.TABLE];

            ToolColumn.setColumnValue(tab, TableKSLINES.AMOUNT, pRow[TableINVOICE.NETTOTAL]);
            ToolColumn.setColumnValue(tab, TableKSLINES.CLIENTREF, pRow[TableINVOICE.CLIENTREF]);
            ToolColumn.setColumnValue(tab, TableKSLINES.CANCELLED, pRow[TableINVOICE.CANCELLED]);
            ToolColumn.setColumnValue(tab, TableKSLINES.DATE_, pRow[TableINVOICE.DATE_]);

            object invLref_ = pRow[TableINVOICE.LOGICALREF];

            _editor.handlerReferenceInformer = (EditingTools pTool, object pLref) =>
            {
                refresh(invLref_);
            };

            _editor.edit();

            return invLref_;
        }

        private bool canCash(DataRow dataRow, bool pDbRow)
        {
            if (!ToolMobile.canPayment())
                return false;

            if (ToolMobile.isReader())
                return false;

            return (dataRow != null);
        }




        protected override MobDataReferenceGridFormBase.CmdMenuItem[] createMenuItems()
        {
            List<CmdMenuItem> list = new List<CmdMenuItem>();
            list.AddRange(base.createMenuItems());

            list.Add(new CmdMenuItem() { CmdType = CmdType.doc1, Text = ToolMobile.getEnvironment().translate(WordCollection.T_CASH) });

            return list.ToArray();
        }



    }
}

